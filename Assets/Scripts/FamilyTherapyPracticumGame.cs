using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public sealed class FamilyTherapyPracticumGame : MonoBehaviour
{
    private readonly List<TherapyTheory> theories = new List<TherapyTheory>();
    private readonly List<FamilyCase> cases = new List<FamilyCase>();
    private readonly List<PlayerChoiceLog> logs = new List<PlayerChoiceLog>();
    private readonly List<SessionSelection> currentSelections = new List<SessionSelection>();
    private readonly List<SupervisorProfile> supervisors = new List<SupervisorProfile>();
    private readonly List<VnCharacterProfile> vnCharacters = new List<VnCharacterProfile>();
    private readonly Dictionary<string, VnCaseScript> vnScripts = new Dictionary<string, VnCaseScript>();

    private Canvas canvas;
    private Font appFont;
    private FamilyCase currentCase;
    private TherapyTheory selectedTheory;
    private VnCaseScript currentVnScript;
    private int currentTurn;
    private int currentVnLineIndex;
    private int sessionScore;
    private int trustScore;
    private int safetyScore;
    private int insightScore;
    private int caseBrowserPage;
    private string exportFolder;
    private bool aiSupervisorEnabled;

    private const int SessionTurnCount = 5;
    private const int CasesPerBrowserPage = 10;
    private const int CommercialAssetTarget = 750;
    private const int DefaultWindowWidth = 1600;
    private const int DefaultWindowHeight = 900;
    private const string StyleTestFolder = "Assets/ConceptArt/StyleTest_2026-06-08";
    private const bool UseDecorativeUiSkins = true;

    private static readonly string[] RequiredVnAssetPaths =
    {
        "VN/Backgrounds/counseling_room_day",
        "VN/Characters/FT001/ft001_mother_neutral",
        "VN/Characters/FT001/ft001_child_neutral",
        "VN/Characters/FT001/ft001_grandmother_neutral",
        "VN/Characters/FT001/ft001_teacher_neutral",
        "VN/Characters/Supervisors/supervisor_system_neutral",
        "VN/Characters/Supervisors/supervisor_bowen_neutral",
        "VN/Characters/Supervisors/supervisor_strategic_neutral",
        "VN/Characters/Supervisors/supervisor_structural_neutral",
        "VN/Characters/Supervisors/supervisor_satir_neutral",
        "VN/Characters/Supervisors/supervisor_psychodynamic_neutral",
        "VN/Characters/Supervisors/supervisor_cbft_neutral",
        "VN/Characters/Supervisors/supervisor_solution_neutral",
        "VN/Characters/Supervisors/supervisor_narrative_neutral",
        "VN/Backgrounds/counseling_room_evening",
        "VN/Backgrounds/counseling_room_tense",
        "VN/Backgrounds/supervision_room_day",
        "VN/Characters/FT001/ft001_mother_anxious",
        "VN/Characters/FT001/ft001_mother_defensive",
        "VN/Characters/FT001/ft001_mother_exhausted",
        "VN/Characters/FT001/ft001_mother_softened",
        "VN/Characters/FT001/ft001_mother_worried",
        "VN/Characters/FT001/ft001_child_anxious",
        "VN/Characters/FT001/ft001_child_hesitant",
        "VN/Characters/FT001/ft001_child_quiet",
        "VN/Characters/FT001/ft001_child_scared",
        "VN/Characters/FT001/ft001_child_withdrawn",
        "VN/Characters/FT001/ft001_grandmother_critical",
        "VN/Characters/FT001/ft001_grandmother_defensive",
        "VN/Characters/FT001/ft001_grandmother_softened",
        "VN/Characters/FT001/ft001_teacher_concerned",
        "VN/Characters/FT001/ft001_teacher_procedural",
        "VN/Characters/Supervisors/supervisor_system_approving",
        "VN/Characters/Supervisors/supervisor_system_explaining",
        "VN/Characters/Supervisors/supervisor_system_questioning",
        "VN/Characters/Supervisors/supervisor_system_reflective",
        "VN/UI/dialogue_box",
        "VN/UI/speaker_nameplate",
        "VN/UI/choice_card_question",
        "VN/UI/choice_card_intervention",
        "VN/UI/supervisor_note_panel",
        "VN/UI/case_file_panel",
        "VN/UI/metrics_hud",
        "VN/UI/session_result_sheet"
    };

    private static readonly string[] KnownExpressionIds =
    {
        "neutral", "anxious", "defensive", "exhausted", "softened", "worried", "tearful", "listening",
        "withdrawn", "scared", "quiet", "relieved", "hesitant", "critical", "stubborn", "concerned",
        "procedural", "explaining", "questioning", "warning", "approving", "reflective", "supportive"
    };

    private static readonly Color Background = new Color32(21, 24, 29, 255);
    private static readonly Color Panel = new Color32(34, 38, 47, 255);
    private static readonly Color Paper = new Color32(238, 232, 218, 255);
    private static readonly Color Ink = new Color32(38, 35, 31, 255);
    private static readonly Color MutedInk = new Color32(96, 91, 82, 255);
    private static readonly Color Accent = new Color32(74, 135, 161, 255);
    private static readonly Color Warm = new Color32(183, 114, 67, 255);
    private static readonly Color Good = new Color32(83, 145, 105, 255);
    private static readonly Color Warn = new Color32(185, 151, 70, 255);
    private static readonly Color Bad = new Color32(172, 82, 76, 255);

    private void Awake()
    {
        ConfigureStartupWindow();
        exportFolder = Path.Combine(Application.persistentDataPath, "FamilyTherapyPracticumExports");
        Directory.CreateDirectory(exportFolder);
        appFont = Resources.Load<Font>("Fonts/Paperlogy-6SemiBold");
        BuildData();
        LoadSaveSlot(1, false);
        if (HasArg("-familyTherapySmokeTest"))
        {
            RunSmokeTestAndQuit();
            return;
        }
        BuildCanvas();
        if (HasArg("-familyTherapyUiSmokeTest"))
        {
            RunUiSmokeTestAndQuit();
            return;
        }
        if (HasArg("-familyTherapyVisualAudit"))
        {
            StartCoroutine(RunVisualAuditAndQuit());
            return;
        }
        ShowMainMenu();
    }

    private static bool HasArg(string arg)
    {
        return Environment.GetCommandLineArgs().Any(a => string.Equals(a, arg, StringComparison.OrdinalIgnoreCase));
    }

    private static bool HasArgPrefix(string prefix)
    {
        return Environment.GetCommandLineArgs().Any(a => a.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));
    }

    private void ConfigureStartupWindow()
    {
        bool explicitResolution = HasArgPrefix("-screen-width") || HasArgPrefix("-screen-height") || HasArgPrefix("-screen-fullscreen");
        bool headless = HasArg("-batchmode") || HasArg("-nographics");
        if (!explicitResolution && !headless)
        {
            Screen.SetResolution(DefaultWindowWidth, DefaultWindowHeight, FullScreenMode.Windowed);
            Debug.Log("Family Therapy Practicum startup resolution forced to " + DefaultWindowWidth + "x" + DefaultWindowHeight + " windowed");
        }
    }

    private void RunSmokeTestAndQuit()
    {
        logs.Clear();
        currentCase = cases[0];
        selectedTheory = theories.First(t => t.id == currentCase.recommendedTheoryId);
        currentVnScript = GetVnScript(currentCase.id);
        currentSelections.Clear();
        currentSelections.Add(new SessionSelection { turn = 1, choice = "초기 합류와 문제 정의", theoryId = selectedTheory.id, quality = 90, feedback = "smoke", interventionType = "joining", familyReaction = "smoke reaction", reactionSpeakerId = "ft001_mother" });
        currentSelections.Add(new SessionSelection { turn = 2, choice = "가족역동 개념화", theoryId = selectedTheory.id, quality = 92, feedback = "smoke", interventionType = "circular_mapping", familyReaction = "smoke reaction", reactionSpeakerId = "ft001_mother" });
        currentSelections.Add(new SessionSelection { turn = 3, choice = "정서/구조 단서 확인", theoryId = selectedTheory.id, quality = 91, feedback = "smoke", interventionType = "emotion_reflection", familyReaction = "smoke reaction", reactionSpeakerId = "ft001_grandmother" });
        currentSelections.Add(new SessionSelection { turn = 4, choice = currentCase.recommendedIntervention, theoryId = selectedTheory.id, quality = 95, feedback = "smoke", interventionType = "circular_question", familyReaction = "smoke reaction", reactionSpeakerId = "supervisor_system" });
        currentSelections.Add(new SessionSelection { turn = 5, choice = "다음 주 과제와 안전 계획", theoryId = selectedTheory.id, quality = 93, feedback = "smoke", interventionType = "feedback_task", familyReaction = "smoke reaction", reactionSpeakerId = "ft001_mother" });
        sessionScore = 461;
        trustScore = 82;
        safetyScore = 80;
        insightScore = 84;
        SaveSessionLog();
        ExportAll();

        string smokePath = Path.Combine(exportFolder, "family_therapy_practicum_smoke_result.json");
        string exeReady = File.Exists(Path.Combine(Application.dataPath, "app.info")) ? "true" : "unknown";
        var missingAssets = GetMissingVnAssets();
        var json = "{\n" +
                   "  \"completed\": true,\n" +
                   "  \"caseCount\": " + cases.Count + ",\n" +
                   "  \"chapterOneHandcraftedCount\": " + cases.Count(c => c.chapter == 1 && c.isHandcrafted) + ",\n" +
                   "  \"theoryCount\": " + theories.Count + ",\n" +
                   "  \"supervisorCount\": " + supervisors.Count + ",\n" +
                   "  \"sessionTurnCount\": " + SessionTurnCount + ",\n" +
                   "  \"logCount\": " + logs.Count + ",\n" +
                   "  \"visualNovelMode\": " + HasVnScript(currentCase).ToString().ToLowerInvariant() + ",\n" +
                   "  \"vnScriptCount\": " + vnScripts.Count + ",\n" +
                   "  \"vnPlayableCaseCount\": " + cases.Count(HasVnScript) + ",\n" +
                   "  \"commercialCoreVnScriptCount\": " + cases.OrderBy(c => c.id).Take(24).Count(HasVnScript) + ",\n" +
                   "  \"trainingVnScriptCount\": " + cases.OrderBy(c => c.id).Skip(24).Count(HasVnScript) + ",\n" +
                   "  \"vnCharacterProfileCount\": " + vnCharacters.Count + ",\n" +
                   "  \"vnRequiredAssetCount\": " + RequiredVnAssetPaths.Length + ",\n" +
                   "  \"vnAvailableAssetCount\": " + (RequiredVnAssetPaths.Length - missingAssets.Count) + ",\n" +
                   "  \"missingVnAssets\": " + JsonStringArray(missingAssets) + ",\n" +
                   "  \"ft001VnReady\": " + HasVnScript(cases.First(c => c.id == "FT-001")).ToString().ToLowerInvariant() + ",\n" +
                   "  \"commercialAssetTarget\": " + CommercialAssetTarget + ",\n" +
                   "  \"commercialAssetCurrent\": " + CountCommercialVnAssets() + ",\n" +
                   "  \"styleTestAssetCount\": " + CountStyleTestAssets() + ",\n" +
                   "  \"saveSystemReady\": " + File.Exists(GetSaveSlotPath(1)).ToString().ToLowerInvariant() + ",\n" +
                   "  \"exportFolder\": " + JsonString(exportFolder) + ",\n" +
                   "  \"hasDashboardHtml\": " + File.Exists(Path.Combine(exportFolder, "dashboard.html")).ToString().ToLowerInvariant() + ",\n" +
                   "  \"hasPlayerCsv\": " + File.Exists(Path.Combine(exportFolder, "player_choice_log.csv")).ToString().ToLowerInvariant() + ",\n" +
                   "  \"hasCaseDataset\": " + File.Exists(Path.Combine(exportFolder, "case_dataset.json")).ToString().ToLowerInvariant() + ",\n" +
                   "  \"playerDataReady\": " + JsonString(exeReady) + "\n" +
                   "}\n";
        File.WriteAllText(smokePath, json, new UTF8Encoding(false));
        Debug.Log("FAMILY_THERAPY_PRACTICUM_SMOKE completed=true path=" + smokePath);
        Application.Quit(0);
    }

    private void RunUiSmokeTestAndQuit()
    {
        string smokePath = Path.Combine(exportFolder, "family_therapy_practicum_ui_smoke_result.json");
        bool completed = false;
        string error = "";
        int hudCount = 0;
        int dialogueCount = 0;
        int characterImageCount = 0;
        int characterHolderCount = 0;
        try
        {
            BeginVnCase(cases.First(c => c.id == "FT-001"));
            var transforms = canvas.GetComponentsInChildren<Transform>(true);
            hudCount = transforms.Count(t => t.name.Contains("VN HUD"));
            dialogueCount = transforms.Count(t => t.name.Contains("Dialogue Box"));
            characterImageCount = transforms.Count(t => t.name.Contains("Character Image"));
            characterHolderCount = transforms.Count(t => t.name.StartsWith("Character ", StringComparison.OrdinalIgnoreCase));
            completed = hudCount > 0 && dialogueCount > 0 && (characterImageCount > 0 || characterHolderCount > 0);
        }
        catch (Exception ex)
        {
            error = ex.GetType().Name + ": " + ex.Message;
            Debug.LogException(ex);
        }

        var json = "{\n" +
                   "  \"completed\": " + completed.ToString().ToLowerInvariant() + ",\n" +
                   "  \"hudCount\": " + hudCount + ",\n" +
                   "  \"dialogueCount\": " + dialogueCount + ",\n" +
                   "  \"characterImageCount\": " + characterImageCount + ",\n" +
                   "  \"characterHolderCount\": " + characterHolderCount + ",\n" +
                   "  \"error\": " + JsonString(error) + "\n" +
                   "}\n";
        File.WriteAllText(smokePath, json, new UTF8Encoding(false));
        Debug.Log("FAMILY_THERAPY_PRACTICUM_UI_SMOKE completed=" + completed.ToString().ToLowerInvariant() + " path=" + smokePath);
        Application.Quit(completed ? 0 : 1);
    }

    private IEnumerator RunVisualAuditAndQuit()
    {
        var entries = new List<string>();
        string auditFolder = Path.Combine(exportFolder, "visual_audit_" + Screen.width + "x" + Screen.height);
        Directory.CreateDirectory(auditFolder);

        ShowMainMenu();
        yield return CaptureVisualAuditFrame("01_main_menu", auditFolder, entries);

        caseBrowserPage = 0;
        ShowCaseBrowser();
        yield return CaptureVisualAuditFrame("02_case_browser_page_1", auditFolder, entries);

        caseBrowserPage = 5;
        ShowCaseBrowser();
        yield return CaptureVisualAuditFrame("03_case_browser_page_6", auditFolder, entries);

        BeginCaseIntake(cases.First(c => c.id == "FT-001"));
        yield return CaptureVisualAuditFrame("04_ft001_intake", auditFolder, entries);

        BeginVnCase(cases.First(c => c.id == "FT-001"));
        yield return CaptureVisualAuditFrame("05_ft001_dialogue", auditFolder, entries);

        currentVnLineIndex = 1;
        ShowVnSessionTurn();
        yield return CaptureVisualAuditFrame("06_ft001_supervisor_line", auditFolder, entries);

        ShowVnChoiceDeck(currentVnScript.turns[currentTurn]);
        yield return CaptureVisualAuditFrame("07_ft001_choice_deck", auditFolder, entries);

        ApplyVnChoice(currentVnScript.turns[currentTurn].choices[0]);
        yield return CaptureVisualAuditFrame("08_ft001_reaction", auditFolder, entries);

        BeginVnCase(cases.First(c => c.id == "FT-012"));
        yield return CaptureVisualAuditFrame("09_ft012_corecase_dialogue", auditFolder, entries);

        ShowVnChoiceDeck(currentVnScript.turns[currentTurn]);
        yield return CaptureVisualAuditFrame("10_ft012_corecase_choices", auditFolder, entries);

        ShowSaveLoad();
        yield return CaptureVisualAuditFrame("11_save_load", auditFolder, entries);

        SeedCompletedSessionForVisualAudit();
        ShowDashboard();
        yield return CaptureVisualAuditFrame("12_dashboard", auditFolder, entries);

        ShowSupervision();
        yield return CaptureVisualAuditFrame("13_supervision_report", auditFolder, entries);

        string resultPath = Path.Combine(auditFolder, "visual_audit_result.json");
        string json = "{\n" +
                      "  \"completed\": true,\n" +
                      "  \"screenWidth\": " + Screen.width + ",\n" +
                      "  \"screenHeight\": " + Screen.height + ",\n" +
                      "  \"entries\": [\n" + string.Join(",\n", entries) + "\n  ]\n" +
                      "}\n";
        File.WriteAllText(resultPath, json, new UTF8Encoding(false));
        Debug.Log("FAMILY_THERAPY_PRACTICUM_VISUAL_AUDIT completed=true path=" + resultPath);
        Application.Quit(0);
    }

    private IEnumerator CaptureVisualAuditFrame(string label, string auditFolder, List<string> entries)
    {
        Canvas.ForceUpdateCanvases();
        yield return new WaitForEndOfFrame();
        string screenshotPath = Path.Combine(auditFolder, label + ".png");
        ScreenCapture.CaptureScreenshot(screenshotPath);
        yield return new WaitForSeconds(0.2f);
        Canvas.ForceUpdateCanvases();
        entries.Add(BuildVisualAuditEntry(label, screenshotPath));
    }

    private string BuildVisualAuditEntry(string label, string screenshotPath)
    {
        var textIssues = new List<string>();
        int offscreenRectCount = 0;
        int textOverflowCount = 0;
        int tinyTextCount = 0;
        int imageCount = 0;
        int rawImageCount = 0;

        foreach (var rect in canvas.GetComponentsInChildren<RectTransform>(true))
        {
            if (!rect.gameObject.activeInHierarchy) continue;
            Vector3[] corners = new Vector3[4];
            rect.GetWorldCorners(corners);
            float minX = corners.Min(c => c.x);
            float maxX = corners.Max(c => c.x);
            float minY = corners.Min(c => c.y);
            float maxY = corners.Max(c => c.y);
            if (maxX < -1 || minX > Screen.width + 1 || maxY < -1 || minY > Screen.height + 1)
            {
                offscreenRectCount++;
            }
        }

        foreach (var image in canvas.GetComponentsInChildren<Image>(true))
        {
            if (image.gameObject.activeInHierarchy) imageCount++;
        }

        foreach (var raw in canvas.GetComponentsInChildren<RawImage>(true))
        {
            if (raw.gameObject.activeInHierarchy) rawImageCount++;
        }

        foreach (var text in canvas.GetComponentsInChildren<Text>(true))
        {
            if (!text.gameObject.activeInHierarchy) continue;
            var rect = text.GetComponent<RectTransform>();
            float width = Mathf.Abs(rect.rect.width);
            float height = Mathf.Abs(rect.rect.height);
            float preferredHeight = text.preferredHeight;
            float preferredWidth = text.preferredWidth;
            float scaledFontSize = text.fontSize * canvas.scaleFactor;
            if (scaledFontSize < 13f) tinyTextCount++;
            bool verticalOverflow = height > 1f && preferredHeight > height + 3f;
            bool horizontalOverflow = width > 1f && text.horizontalOverflow == HorizontalWrapMode.Overflow && preferredWidth > width + 3f;
            if (verticalOverflow || horizontalOverflow)
            {
                textOverflowCount++;
                if (textIssues.Count < 14)
                {
                    textIssues.Add("{\"object\":" + JsonString(GetHierarchyPath(text.transform)) +
                                   ",\"text\":" + JsonString(CompactForAudit(text.text)) +
                                   ",\"rectWidth\":" + Mathf.RoundToInt(width) +
                                   ",\"rectHeight\":" + Mathf.RoundToInt(height) +
                                   ",\"preferredWidth\":" + Mathf.RoundToInt(preferredWidth) +
                                   ",\"preferredHeight\":" + Mathf.RoundToInt(preferredHeight) + "}");
                }
            }
        }

        return "    {\n" +
               "      \"label\": " + JsonString(label) + ",\n" +
               "      \"screenshot\": " + JsonString(screenshotPath.Replace("\\", "/")) + ",\n" +
               "      \"activeScreen\": " + JsonString(canvas.transform.childCount == 0 ? "" : canvas.transform.GetChild(canvas.transform.childCount - 1).name) + ",\n" +
               "      \"textCount\": " + canvas.GetComponentsInChildren<Text>(true).Count(t => t.gameObject.activeInHierarchy) + ",\n" +
               "      \"imageCount\": " + imageCount + ",\n" +
               "      \"rawImageCount\": " + rawImageCount + ",\n" +
               "      \"offscreenRectCount\": " + offscreenRectCount + ",\n" +
               "      \"tinyTextCount\": " + tinyTextCount + ",\n" +
               "      \"textOverflowCount\": " + textOverflowCount + ",\n" +
               "      \"textOverflowSamples\": [" + string.Join(",", textIssues) + "]\n" +
               "    }";
    }

    private void SeedCompletedSessionForVisualAudit()
    {
        currentCase = cases.First(c => c.id == "FT-001");
        selectedTheory = theories.First(t => t.id == currentCase.recommendedTheoryId);
        currentVnScript = GetVnScript(currentCase.id);
        currentSelections.Clear();
        currentSelections.Add(new SessionSelection { turn = 1, choice = "각 구성원의 걱정과 원하는 변화를 한 문장씩 말하게 한다.", theoryId = selectedTheory.id, quality = 95, feedback = "합류와 관계 단서 수집이 균형을 이룹니다.", interventionType = "joining", familyReaction = "가족이 방어를 조금 낮추고 말하기 시작합니다.", reactionSpeakerId = "ft001_mother" });
        currentSelections.Add(new SessionSelection { turn = 2, choice = "등교 거부 장면을 가족 상호작용 순환으로 옮겨 본다.", theoryId = selectedTheory.id, quality = 92, feedback = "IP 고정을 줄이고 반복 패턴을 보게 합니다.", interventionType = "circular_mapping", familyReaction = "아이와 보호자가 서로를 덜 탓하며 장면을 설명합니다.", reactionSpeakerId = "ft001_child" });
        currentSelections.Add(new SessionSelection { turn = 3, choice = "외조모의 비판 아래 걱정을 반영한다.", theoryId = "satir", quality = 80, feedback = "정서적 안전감이 올라가지만 구조화가 더 필요합니다.", interventionType = "emotion_reflection", familyReaction = "외조모가 비난 대신 걱정을 말합니다.", reactionSpeakerId = "ft001_grandmother" });
        currentSelections.Add(new SessionSelection { turn = 4, choice = currentCase.recommendedIntervention, theoryId = selectedTheory.id, quality = 98, feedback = "핵심 순환 패턴을 회기 안에서 다룹니다.", interventionType = "circular_question", familyReaction = "가족이 아침 장면에서 서로의 반응을 새롭게 봅니다.", reactionSpeakerId = "supervisor_system" });
        currentSelections.Add(new SessionSelection { turn = 5, choice = "다음 주 아침 루틴 실험을 정한다.", theoryId = selectedTheory.id, quality = 93, feedback = "실제 행동 과제로 이어집니다.", interventionType = "feedback_task", familyReaction = "가족은 해볼 수 있는 작은 변화를 합의합니다.", reactionSpeakerId = "ft001_mother" });
        sessionScore = 91;
        trustScore = 82;
        safetyScore = 80;
        insightScore = 84;
        if (logs.Count == 0 || logs.Last().caseId != currentCase.id)
        {
            SaveSessionLog();
        }
    }

    private static string CompactForAudit(string value)
    {
        if (string.IsNullOrEmpty(value)) return "";
        string compact = value.Replace("\r", " ").Replace("\n", " ");
        return compact.Length <= 80 ? compact : compact.Substring(0, 80) + "...";
    }

    private static string GetHierarchyPath(Transform transform)
    {
        var parts = new List<string>();
        while (transform != null)
        {
            parts.Add(transform.name);
            transform = transform.parent;
        }
        parts.Reverse();
        return string.Join("/", parts);
    }

    private void BuildCanvas()
    {
        var canvasObject = new GameObject("App Canvas");
        canvasObject.transform.SetParent(transform, false);
        canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        var scaler = canvasObject.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        scaler.matchWidthOrHeight = 0.5f;
        canvasObject.AddComponent<GraphicRaycaster>();

        if (FindObjectOfType<EventSystem>() == null)
        {
            var eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }
    }

    private void BuildData()
    {
        theories.Clear();
        theories.Add(new TherapyTheory("system", "가족체계 기본", "상호작용, 피드백, 항상성", "개인의 문제가 아니라 관계 패턴과 체계 균형이 증상을 유지한다.", "문제 유지 순환을 보이게 하고 새로운 피드백 루프를 만든다.", new[] { "순환질문", "상호작용 지도", "피드백 재구성" }));
        theories.Add(new TherapyTheory("bowen", "Bowen 다세대", "분화, 삼각관계, 정서적 단절", "가족 불안과 낮은 분화가 세대를 넘어 반복된다.", "분화를 높이고 삼각관계에서 빠져나와 자기 입장을 말하게 한다.", new[] { "가계도", "과정질문", "I-position" }));
        theories.Add(new TherapyTheory("strategic", "전략적 가족치료", "시도된 해결책, 가족규칙, 역설", "가족이 반복하는 해결 시도가 오히려 문제를 유지한다.", "문제 유지 규칙을 깨는 구체적 과제를 설계한다.", new[] { "지시", "재명명", "역설적 과제" }));
        theories.Add(new TherapyTheory("structural", "구조적 가족치료", "하위체계, 경계, 위계", "경계와 위계의 혼란이 증상을 만든다.", "가족 구조를 재배열하고 건강한 경계를 만든다.", new[] { "합류", "실연", "경계 만들기" }));
        theories.Add(new TherapyTheory("satir", "경험적/Satir", "빙산, 자아존중감, 의사소통 유형", "억눌린 감정과 방어적 의사소통이 관계를 굳게 만든다.", "안전감 속에서 진짜 감정과 욕구를 표현하게 한다.", new[] { "빙산탐색", "가족조각", "일치형 소통 연습" }));
        theories.Add(new TherapyTheory("psychodynamic", "정신역동 가족치료", "투사, 대상관계, 미해결 애도", "무의식적 방어와 과거 관계 표상이 현재 가족관계에 반복된다.", "투사와 반복을 자각하고 관계 안의 감정을 다룬다.", new[] { "전이 탐색", "방어 해석", "애도 작업" }));
        theories.Add(new TherapyTheory("cbft", "인지행동 가족치료", "귀인, 강화, 기술훈련", "왜곡된 신념과 상호강화가 갈등 행동을 반복시킨다.", "생각-감정-행동 연결을 바꾸고 소통 기술을 연습한다.", new[] { "행동계약", "의사소통 훈련", "문제해결 훈련" }));
        theories.Add(new TherapyTheory("solution", "해결중심 가족치료", "예외, 강점, 작은 변화", "문제 설명보다 이미 작동한 예외와 자원이 변화의 단서다.", "가족이 원하는 미래와 다음 작은 행동을 구체화한다.", new[] { "기적질문", "척도질문", "예외질문" }));
        theories.Add(new TherapyTheory("narrative", "이야기치료", "외재화, 지배담론, 재저작", "가족은 문제에 대한 지배적 이야기 속에 갇힌다.", "문제를 사람과 분리하고 대안적 이야기를 두껍게 만든다.", new[] { "외재화 질문", "독특한 결과", "재저작 대화" }));

        supervisors.Clear();
        supervisors.Add(new SupervisorProfile("system", "김혜성 슈퍼바이저", "가족체계 기본", "관계의 선후보다 반복 순환을 먼저 보세요. 한 사람을 문제로 고정하는 순간 자료가 좁아집니다."));
        supervisors.Add(new SupervisorProfile("bowen", "안우진 슈퍼바이저", "Bowen 다세대", "가족 불안 속에서 누가 누구와 삼각관계를 만들고 있는지 조용히 추적하세요."));
        supervisors.Add(new SupervisorProfile("strategic", "김윤하 슈퍼바이저", "전략적 가족치료", "가족이 이미 해온 해결책이 문제를 어떻게 붙잡고 있는지 확인해야 합니다."));
        supervisors.Add(new SupervisorProfile("structural", "이정후 슈퍼바이저", "구조적 가족치료", "경계와 위계가 보이면 회기의 초점이 흔들리지 않습니다."));
        supervisors.Add(new SupervisorProfile("satir", "김연주 슈퍼바이저", "경험적/Satir", "말투 아래의 두려움, 수치심, 인정 욕구를 놓치지 마세요."));
        supervisors.Add(new SupervisorProfile("psychodynamic", "송성문 슈퍼바이저", "정신역동 가족치료", "현재 대화 안에 과거 대상관계와 미해결 애도가 어떻게 반복되는지 보세요."));
        supervisors.Add(new SupervisorProfile("cbft", "정세영 슈퍼바이저", "인지행동 가족치료", "서로에 대한 귀인과 강화 패턴을 행동 단위로 좁히면 개입이 선명해집니다."));
        supervisors.Add(new SupervisorProfile("solution", "송지후 슈퍼바이저", "해결중심 가족치료", "이미 작게 성공한 예외를 찾으면 가족이 해볼 수 있는 다음 행동이 생깁니다."));
        supervisors.Add(new SupervisorProfile("narrative", "박병호 슈퍼바이저", "이야기치료", "문제가 사람을 덮어버린 이름을 갖고 있다면, 먼저 그 이름을 밖으로 꺼내야 합니다."));

        cases.Clear();
        AddHandcraftedChapterOneCases();
        string[] familyTypes =
        {
            "한부모 초등 자녀 가족", "조손 청소년 가족", "맞벌이 특수교육 자녀 가족", "이민 배경 다문화 가족", "재혼가족",
            "장기질환 자녀 가족", "성인자녀 원가족 재결합 가족", "학교 부적응 청소년 가족", "고립된 산후 가족", "형제 돌봄 과부하 가족"
        };
        string[] presenting =
        {
            "등교 거부와 보호자의 소진", "야간 귀가 지연과 조부모 갈등", "치료 일정과 부부 갈등", "통번역 장벽과 보육 탈락", "새 부모-자녀 경계 갈등",
            "입원 아동과 형제자매의 외로움", "성인자녀의 경제 의존과 부모 분노", "학교폭력 이후 가족 침묵", "산후 우울과 친족 지원망 부재", "청소년의 과도한 돌봄 역할"
        };
        string[] hidden =
        {
            "표면 증상보다 가족의 불안 조절 방식이 더 중요하다.",
            "한 사람이 문제로 지목되지만 실제로는 가족 전체의 역할 고착이 핵심이다.",
            "서류상 정보보다 말해지지 않는 감정과 욕구가 회기에서 반복된다.",
            "가족 외부의 제도 장벽이 가족 내부 비난으로 번역되고 있다.",
            "과거 상실과 미해결 애도가 현재 갈등 장면에서 재활성화된다.",
            "부모 하위체계가 약해지고 자녀가 중재자 역할을 떠맡는다.",
            "가족은 이미 작게 성공한 예외 경험을 갖고 있지만 문제 이야기만 반복한다.",
            "가족 구성원은 서로를 보호하려 하지만 그 방식이 침묵과 회피로 굳어졌다.",
            "문제 이름이 가족 구성원을 압도해 대안적 정체성이 보이지 않는다.",
            "소통은 논리적으로 보이지만 실제 감정 접촉은 거의 일어나지 않는다."
        };
        string[][] tags =
        {
            new[] { "삼각관계", "정서적 단절", "분화" },
            new[] { "위계 약화", "부모화", "경계 혼란" },
            new[] { "시도된 해결책", "가족규칙", "통제" },
            new[] { "빙산", "비난형", "회유형" },
            new[] { "투사", "미해결 애도", "대상관계" },
            new[] { "귀인", "상호강화", "행동계약" },
            new[] { "예외", "강점", "작은 변화" },
            new[] { "외재화", "지배담론", "재저작" },
            new[] { "피드백", "항상성", "순환인과" }
        };

        for (int i = cases.Count; i < 60; i++)
        {
            TherapyTheory theory = theories[i % theories.Count];
            int typeIndex = i % familyTypes.Length;
            int chapter = (i / 10) + 1;
            string id = "FT-" + (i + 1).ToString("000", CultureInfo.InvariantCulture);
            int risk = 35 + ((i * 11) % 61);
            cases.Add(new FamilyCase
            {
                id = id,
                chapter = chapter,
                title = "제" + chapter + "장 사례 " + (i % 10 + 1),
                familyType = familyTypes[typeIndex],
                presentingProblem = presenting[typeIndex],
                context = "센터 접수 메모: " + familyTypes[typeIndex] + "이(가) " + presenting[typeIndex] + " 문제로 상담을 신청했다. 이전 지원 경험은 제한적이며 가족은 서로 다른 설명을 내놓고 있다.",
                hiddenPattern = hidden[i % hidden.Length],
                recommendedTheoryId = theory.id,
                recommendedIntervention = theory.methods[i % theory.methods.Length],
                dynamicsTags = tags[i % tags.Length],
                riskLevel = risk,
                familyMap = "관계도: 보호자-자녀 긴장, 외부 기관과의 거리감, 문제로 지목된 구성원 주변에 반복 대화가 몰림.",
                learningObjective = "자동 생성 실습 사례: 이론 렌즈와 기본 개입을 빠르게 적용한다.",
                supervisorCue = "증상 설명에 머물지 말고 가족이 반복하는 상호작용을 추적하세요.",
                reflectionQuestion = "내 선택은 누구를 IP로 고정했고, 어떤 관계 정보를 놓쳤는가?",
                isHandcrafted = false,
                familyDialogue = BuildDialogue(typeIndex, i)
            });
        }

        BuildVnData();
    }

    private void AddHandcraftedChapterOneCases()
    {
        cases.Add(MakeCase("FT-001", "1장 사례 1", "한부모 초등 자녀 가족", "등교 거부와 보호자의 소진",
            "센터 접수 메모: 야간 근무를 하는 박성빈은 초등 4학년 이주형의 등교 거부가 심해졌다고 호소한다. 학교는 이주형의 지각을 문제로 보지만, 이주형은 아침마다 박성빈이 집을 나간 뒤 혼자 남는 시간이 무섭다고 말한다.",
            "박성빈의 생계 불안과 이주형의 분리 불안이 서로를 자극하며, 등교 거부가 가족의 불안을 조절하는 방식으로 굳어지고 있다.",
            "system", "순환질문", 68, new[] { "피드백", "항상성", "순환인과" },
            "관계도: 박성빈-이주형 밀착, 학교와 박성빈 사이의 비난 루프, 오선진은 돕고 싶지만 야간 근무를 비판해 박성빈이 더 고립됨.",
            "IP를 아이로 고정하지 않고 아침 루틴 전체를 관계 패턴으로 읽는다.",
            "아이의 등교 거부가 가족에게 어떤 기능을 하고 있는지 먼저 보세요.",
            "나는 아이의 행동을 문제로만 보았는가, 아니면 가족 불안을 조절하는 신호로 보았는가?",
            new[]
            {
                "박성빈: 아침마다 실랑이하다 보면 저도 모르게 소리를 질러요. 지각하면 학교에서 또 전화가 옵니다.",
                "이주형: 엄마가 나가면 집이 너무 조용해요. 학교 가면 괜찮은 척해야 해서 더 싫어요.",
                "서건창 메모: 이주형은 교실에 오면 조용하지만, 박성빈 이야기가 나오면 울음을 참습니다.",
                "오선진: 애가 약해서 그래요. 성빈이가 일을 줄이면 해결될 텐데 말을 안 듣습니다.",
                "상담자 메모: 등교 문제는 아이 개인 증상보다 아침 분리 장면의 반복 패턴으로 보인다."
            }));

        cases.Add(MakeCase("FT-002", "1장 사례 2", "조손 청소년 가족", "야간 귀가 지연과 조부모 갈등",
            "센터 접수 메모: 중학교 2학년 손자는 밤늦게 귀가하고, 조부모는 매일 휴대폰 검사와 잔소리로 대응한다. 손자는 아버지 이야기가 나오면 대화를 끊고 방으로 들어간다.",
            "부모 부재로 인한 상실감이 말해지지 못한 채 통제와 도피의 반복으로 나타난다. 조부모와 손자는 서로를 걱정하지만 말의 표면은 비난이다.",
            "bowen", "I-position", 74, new[] { "삼각관계", "정서적 단절", "분화" },
            "관계도: 조모-손자 갈등, 조부는 중재자 역할, 부재한 아버지 이야기가 가족 불안을 높이는 삼각점.",
            "정서적 단절과 삼각관계를 찾아 손자가 자기 입장을 말할 수 있게 돕는다.",
            "조부모의 통제와 손자의 도피가 어떻게 서로를 강화하는지 추적하세요.",
            "나는 조부모의 통제를 사랑의 표현으로도 읽었는가?",
            new[]
            {
                "조모: 얘가 밤에 들어올 때까지 심장이 내려앉아요. 그래서 휴대폰을 볼 수밖에 없어요.",
                "손자: 집에 오면 또 검사니까 늦게 들어가는 게 낫죠. 어차피 믿지도 않잖아요.",
                "조부: 둘 다 그만하라고 해도 안 됩니다. 저는 중간에서 말리는 역할만 해요.",
                "손자: 아빠 얘기는 하지 마세요. 그 사람은 우리 가족 아니에요.",
                "상담자 메모: 아버지에 대한 정서적 단절이 조모-손자 갈등으로 우회되는 양상."
            }));

        cases.Add(MakeCase("FT-003", "1장 사례 3", "맞벌이 특수교육 자녀 가족", "치료 일정과 부부 갈등",
            "센터 접수 메모: 발달지연 진단을 받은 자녀의 치료 일정을 두고 부부가 매주 다툰다. 아버지는 계획표를 더 엄격히 지키자고 하고, 어머니는 아이가 지쳤다고 말한다.",
            "치료 성과를 둘러싼 부부 하위체계의 갈등이 자녀의 치료 참여 문제로 표현된다. 부모-자녀 경계와 부부 위계가 흔들리고 있다.",
            "structural", "실연", 72, new[] { "위계 약화", "경계 혼란", "하위체계" },
            "관계도: 부부 하위체계 긴장, 어머니-자녀 밀착, 아버지는 일정표와 성과 언어로 가족 안에 들어옴.",
            "부부가 자녀 앞에서 치료 목표를 협상하는 구조를 재배열한다.",
            "치료 일정 자체보다 누가 가족 의사결정의 주도권을 갖는지 보세요.",
            "나는 치료 성과라는 말 뒤에 있는 부부의 위계 문제를 보았는가?",
            new[]
            {
                "아버지: 계획대로 해야 좋아집니다. 빠지면 다 원점으로 돌아가는 것 같아요.",
                "어머니: 아이가 차에서 잠들 정도로 지쳐요. 그런데 제가 쉬자고 하면 제가 포기한 사람이 됩니다.",
                "자녀: 오늘은 아무 데도 안 가고 집에 있고 싶어요.",
                "아버지: 그래서 더 단호해야 합니다. 아이 말만 들으면 아무것도 못 해요.",
                "상담자 메모: 치료 일정 논쟁 속에서 부부 하위체계와 부모-자녀 경계가 동시에 흔들림."
            }));

        cases.Add(MakeCase("FT-004", "1장 사례 4", "이민 배경 다문화 가족", "통번역 장벽과 보육 탈락",
            "센터 접수 메모: 한국어가 서툰 보호자는 보육 신청에서 여러 번 탈락했고, 배우자는 '서류를 제대로 못 챙긴다'고 비난한다. 보호자는 상담 중에도 짧게 웃으며 괜찮다고 말한다.",
            "제도 장벽이 가족 내부 비난으로 번역되고, 보호자는 회유형 의사소통으로 갈등을 낮추려 하지만 실제 욕구는 말해지지 않는다.",
            "satir", "빙산탐색", 61, new[] { "빙산", "회유형", "의사소통" },
            "관계도: 배우자-보호자 비난/회유 패턴, 보육기관과의 정보 장벽, 아이의 돌봄 공백이 부부갈등을 증폭.",
            "회유형 표면 아래의 수치심, 두려움, 도움 욕구를 안전하게 드러낸다.",
            "괜찮다는 말이 실제 괜찮음을 뜻하는지, 안전을 위한 방어인지 살피세요.",
            "나는 언어 장벽을 개인 무능력처럼 해석하지 않았는가?",
            new[]
            {
                "보호자: 괜찮아요. 제가 더 배워야죠. 다시 해보면 됩니다.",
                "배우자: 매번 괜찮다고만 하니까 똑같은 일이 반복되는 거예요.",
                "보호자: 제가 말하면 더 복잡해져요. 그냥 조용히 있는 게 낫습니다.",
                "아이: 엄마가 전화 끝나면 한숨을 쉬어요.",
                "상담자 메모: 회유형 소통과 제도 접근성 문제가 겹쳐 보호자의 욕구가 보이지 않음."
            }));

        cases.Add(MakeCase("FT-005", "1장 사례 5", "재혼가족", "새 부모-자녀 경계 갈등",
            "센터 접수 메모: 재혼 후 8개월, 새아버지는 중학생 자녀가 자신을 무시한다고 말한다. 어머니는 두 사람 사이에서 중재하려 하지만, 자녀는 '엄마도 이제 내 편이 아니다'라고 말한다.",
            "새 가족 구조가 안정되기 전 어머니가 중재자 역할에 과부하되고, 자녀는 충성심 갈등을 경험한다.",
            "structural", "경계 만들기", 70, new[] { "하위체계", "경계 혼란", "삼각관계" },
            "관계도: 어머니가 새아버지-자녀 사이 중재자, 친부 이야기는 금기, 새 부모 권한과 정서적 신뢰가 분리됨.",
            "부부 하위체계와 부모-자녀 하위체계를 분리하고 새아버지의 역할을 단계적으로 조정한다.",
            "새아버지의 권한 부여보다 관계 형성 순서가 먼저입니다.",
            "나는 재혼가족의 충성심 갈등을 반항으로만 보지 않았는가?",
            new[]
            {
                "새아버지: 가족이면 최소한 인사는 해야 하는 거 아닙니까?",
                "자녀: 갑자기 가족이라고 하면 제가 네 해야 하나요?",
                "어머니: 둘 다 조금만 양보하면 되는데 왜 이렇게 힘든지 모르겠어요.",
                "자녀: 엄마는 이제 그 사람 편이에요.",
                "상담자 메모: 어머니의 중재 과부하와 자녀의 충성심 갈등이 핵심."
            }));

        cases.Add(MakeCase("FT-006", "1장 사례 6", "장기질환 자녀 가족", "입원 아동과 형제자매의 외로움",
            "센터 접수 메모: 장기입원 중인 첫째를 돌보느라 부모는 병원에 머무는 시간이 많다. 둘째는 학교에서 말수가 줄고, '나는 괜찮다'는 말만 반복한다.",
            "가족의 모든 이야기가 아픈 자녀를 중심으로 조직되며, 둘째의 외로움과 죄책감은 말해지지 않는다.",
            "satir", "가족조각", 79, new[] { "빙산", "초이성형", "정서 억압" },
            "관계도: 부모-입원 아동 밀착, 둘째는 주변화, 조부모가 돌봄을 돕지만 정서 대화는 거의 없음.",
            "가족조각과 빙산탐색으로 둘째의 위치와 숨은 욕구를 안전하게 드러낸다.",
            "괜찮다는 말 뒤에 있는 죄책감과 외로움을 서두르지 말고 만나세요.",
            "나는 '아픈 아이'라는 명확한 문제 때문에 형제자매의 정서를 놓치지 않았는가?",
            new[]
            {
                "둘째: 저는 괜찮아요. 병원에 있는 언니가 더 힘들잖아요.",
                "어머니: 얘가 이렇게 말해줘서 고맙지만, 가끔 너무 어른 같아서 걱정돼요.",
                "아버지: 지금은 첫째 치료가 우선입니다. 둘째도 이해할 거예요.",
                "둘째: 이해는 하는데, 집에 오면 아무도 제 얘기를 안 물어봐요.",
                "상담자 메모: 둘째의 성숙한 태도는 정서 억압과 연결될 수 있음."
            }));

        cases.Add(MakeCase("FT-007", "1장 사례 7", "성인자녀 원가족 재결합 가족", "경제 의존과 부모 분노",
            "센터 접수 메모: 취업 실패 후 성인자녀가 부모 집으로 돌아왔다. 아버지는 '게으르다'고 말하고, 자녀는 방에서 나오지 않는다. 어머니는 몰래 용돈을 주며 갈등을 피한다.",
            "부모의 기대와 자녀의 수치심이 투사와 회피로 반복된다. 어머니의 비밀 지원은 갈등을 잠시 낮추지만 가족 규칙을 더 불명확하게 만든다.",
            "psychodynamic", "방어 해석", 66, new[] { "투사", "수치심", "방어" },
            "관계도: 아버지-자녀 비난/회피, 어머니-자녀 비밀 동맹, 경제 문제 뒤에 인정 욕구와 실패감이 있음.",
            "게으름이라는 표면 판단 아래의 수치심, 투사, 방어를 탐색한다.",
            "경제 문제를 다루되, 가족이 서로에게 투사하는 자기상을 함께 보세요.",
            "나는 성인자녀의 의존을 도덕적 실패로만 읽지 않았는가?",
            new[]
            {
                "아버지: 저 나이까지 부모 집에 있으면 부끄러운 줄 알아야 합니다.",
                "자녀: 아버지는 제가 뭘 해도 실패자라고 생각하잖아요.",
                "어머니: 둘이 부딪히는 게 싫어서 제가 중간에서 조금 도와줬어요.",
                "아버지: 그러니까 더 버릇이 없어지는 겁니다.",
                "상담자 메모: 경제 갈등 속에 수치심, 투사, 비밀 동맹이 반복됨."
            }));

        cases.Add(MakeCase("FT-008", "1장 사례 8", "학교 부적응 청소년 가족", "학교폭력 이후 가족 침묵",
            "센터 접수 메모: 학교폭력 이후 청소년은 전학을 원하지만 부모는 '버텨야 한다'고 말한다. 가족은 사건을 자세히 말하지 않고, 식사 자리에서도 학교 이야기를 피한다.",
            "사건 이후 가족은 침묵으로 안정성을 유지하려 하지만, 침묵이 청소년의 고립과 부모의 무력감을 강화한다.",
            "narrative", "외재화 질문", 83, new[] { "외재화", "지배담론", "침묵" },
            "관계도: 청소년은 문제 이름에 압도됨, 부모는 버티기 담론에 묶임, 학교는 절차 언어로 가족과 거리감.",
            "학교폭력 경험이 청소년의 정체성을 덮지 않도록 문제를 외재화하고 대안 이야기를 찾는다.",
            "가족이 침묵을 선택한 이유를 존중하되, 침묵이 만든 비용을 함께 보세요.",
            "나는 이 가족의 '버텨야 한다'는 이야기가 누구에게 도움이 되고 누구를 고립시키는지 물었는가?",
            new[]
            {
                "청소년: 학교 이름만 들어도 속이 안 좋아요. 그런데 집에서도 말하면 분위기가 이상해져요.",
                "어머니: 다시 꺼내면 아이가 더 힘들까 봐 말하지 않았어요.",
                "아버지: 전학하면 도망친 것처럼 남을까 봐 걱정됩니다.",
                "청소년: 저는 이미 도망친 사람처럼 느껴져요.",
                "상담자 메모: 침묵과 버티기 담론이 가족의 대안 선택을 좁힘."
            }));

        cases.Add(MakeCase("FT-009", "1장 사례 9", "고립된 산후 가족", "산후 우울과 친족 지원망 부재",
            "센터 접수 메모: 출산 후 보호자는 잠을 거의 못 자고, 배우자는 '무엇을 도와야 할지 모르겠다'고 말한다. 친족은 멀리 살고, 보호자는 도움을 요청하는 것을 실패로 여긴다.",
            "도움 요청을 실패로 보는 신념과 수면 부족이 상호작용하며, 부부는 서로 무능하다는 귀인을 강화한다.",
            "cbft", "행동계약", 77, new[] { "귀인", "상호강화", "행동계약" },
            "관계도: 부부 모두 소진, 친족 지원망 약함, 도움 요청에 대한 부정적 신념이 행동을 막음.",
            "비난적 귀인을 줄이고 작고 관찰 가능한 돌봄 행동계약을 만든다.",
            "감정 해석만으로는 부족합니다. 오늘 밤 누가 무엇을 할지 행동 단위로 좁히세요.",
            "나는 도움 요청을 실패로 보는 신념이 실제 행동을 어떻게 막는지 보았는가?",
            new[]
            {
                "보호자: 제가 못 버티는 걸 인정하면 엄마 자격이 없는 것 같아요.",
                "배우자: 도와주려고 하면 방식이 틀렸다고 해서 손을 못 대겠어요.",
                "보호자: 말하지 않아도 알아서 해주면 좋겠어요.",
                "배우자: 저는 뭘 해야 하는지 정확히 말해주면 할 수 있어요.",
                "상담자 메모: 귀인 갈등을 행동계약으로 바꿀 여지가 큼."
            }));

        cases.Add(MakeCase("FT-010", "1장 사례 10", "형제 돌봄 과부하 가족", "청소년의 과도한 돌봄 역할",
            "센터 접수 메모: 고등학생 누나는 초등학생 동생의 등하교와 식사를 챙긴다. 보호자는 건강 문제로 누워 있는 시간이 많고, 누나는 '제가 안 하면 집이 안 돌아간다'고 말한다.",
            "청소년의 부모화가 가족 안정성을 지탱하지만, 장기적으로 자율성과 발달 과업을 압박한다. 가족은 이미 작은 협력 자원을 갖고 있다.",
            "solution", "척도질문", 81, new[] { "예외", "강점", "부모화" },
            "관계도: 누나가 사실상 실행 부모, 보호자는 죄책감, 동생은 누나에게 의존, 이웃 교사가 가끔 도움.",
            "가족의 생존 노력을 인정하고, 이미 작동한 예외를 찾아 돌봄 부담을 조금씩 분산한다.",
            "누나를 희생자로만 보지 말고 가족이 이미 해낸 협력의 단서를 찾으세요.",
            "나는 강점을 보면서도 부모화의 위험을 함께 보았는가?",
            new[]
            {
                "누나: 제가 안 하면 동생이 밥을 못 먹어요. 그래서 동아리는 그만뒀어요.",
                "보호자: 미안하다는 말밖에 못 하니까 더 미안합니다.",
                "동생: 누나는 엄마처럼 잘 챙겨줘요. 그런데 가끔 엄청 화를 내요.",
                "누나: 저도 가끔은 누가 저한테 괜찮냐고 물어봤으면 좋겠어요.",
                "상담자 메모: 부모화 위험과 가족 강점이 동시에 보이는 복합 사례."
            }));
    }

    private static FamilyCase MakeCase(string id, string title, string familyType, string presentingProblem, string context, string hiddenPattern, string recommendedTheoryId, string recommendedIntervention, int riskLevel, string[] dynamicsTags, string familyMap, string learningObjective, string supervisorCue, string reflectionQuestion, string[] familyDialogue)
    {
        return new FamilyCase
        {
            id = id,
            chapter = 1,
            title = title,
            familyType = familyType,
            presentingProblem = presentingProblem,
            context = context,
            hiddenPattern = hiddenPattern,
            recommendedTheoryId = recommendedTheoryId,
            recommendedIntervention = recommendedIntervention,
            dynamicsTags = dynamicsTags,
            riskLevel = riskLevel,
            familyMap = familyMap,
            learningObjective = learningObjective,
            supervisorCue = supervisorCue,
            reflectionQuestion = reflectionQuestion,
            isHandcrafted = true,
            familyDialogue = familyDialogue
        };
    }

    private static string[] BuildDialogue(int typeIndex, int offset)
    {
        string[] guardian = { "보호자", "조모", "부", "모", "새아버지", "부모", "아버지", "어머니", "보호자", "누나" };
        string[] child = { "자녀", "청소년", "자녀", "아이", "자녀", "형제", "성인자녀", "학생", "영아의 보호자", "동생" };
        string g = guardian[typeIndex % guardian.Length];
        string c = child[typeIndex % child.Length];
        return new[]
        {
            g + ": 저희는 할 만큼 했어요. 그런데 상담만 시작하면 다 제 탓처럼 흘러갑니다.",
            c + ": 저는 말을 해도 달라지는 게 없어서 그냥 조용히 있는 편이 나아요.",
            g + ": 선생님이 정답을 말해주면 좋겠어요. 누가 문제인지 빨리 확인하고 싶습니다.",
            c + ": 사실 문제라고 불리는 게 저라는 생각이 들어서 여기 오는 것도 싫었습니다.",
            "상담자 메모: " + (offset % 2 == 0 ? "가족이 서로를 걱정하지만 비난과 침묵으로 표현한다." : "한 구성원에게 증상이 집중되어 가족 전체 패턴이 가려진다.")
        };
    }

    private void BuildVnData()
    {
        vnCharacters.Clear();
        vnScripts.Clear();

        AddVnCharacter("ft001_mother", "박성빈", "야간 근무와 자녀 등교 거부 사이에서 소진된 보호자", "VN/Characters/FT001/ft001_mother", "left");
        AddVnCharacter("ft001_child", "이주형", "등교 거부 뒤에 분리 불안을 숨기고 있는 초등학생", "VN/Characters/FT001/ft001_child", "center");
        AddVnCharacter("ft001_grandmother", "오선진", "돕고 싶지만 비판적 언어로 개입하는 외조모", "VN/Characters/FT001/ft001_grandmother", "right");
        AddVnCharacter("ft001_teacher", "서건창", "학교 절차와 아이 걱정 사이에 있는 담임", "VN/Characters/FT001/ft001_teacher", "right");
        AddVnCharacter("supervisor_system", "김혜성", "가족체계 기본 슈퍼바이저", "VN/Characters/Supervisors/supervisor_system", "supervisor");
        AddVnCharacter("supervisor_bowen", "안우진", "Bowen 다세대 슈퍼바이저", "VN/Characters/Supervisors/supervisor_bowen", "supervisor");
        AddVnCharacter("supervisor_strategic", "김윤하", "전략적 가족치료 슈퍼바이저", "VN/Characters/Supervisors/supervisor_strategic", "supervisor");
        AddVnCharacter("supervisor_structural", "이정후", "구조적 가족치료 슈퍼바이저", "VN/Characters/Supervisors/supervisor_structural", "supervisor");
        AddVnCharacter("supervisor_satir", "김연주", "경험적/Satir 슈퍼바이저", "VN/Characters/Supervisors/supervisor_satir", "supervisor");
        AddVnCharacter("supervisor_psychodynamic", "송성문", "정신역동 가족치료 슈퍼바이저", "VN/Characters/Supervisors/supervisor_psychodynamic", "supervisor");
        AddVnCharacter("supervisor_cbft", "정세영", "인지행동 가족치료 슈퍼바이저", "VN/Characters/Supervisors/supervisor_cbft", "supervisor");
        AddVnCharacter("supervisor_solution", "송지후", "해결중심 가족치료 슈퍼바이저", "VN/Characters/Supervisors/supervisor_solution", "supervisor");
        AddVnCharacter("supervisor_narrative", "박병호", "이야기치료 슈퍼바이저", "VN/Characters/Supervisors/supervisor_narrative", "supervisor");
        AddVnCharacter("generic_guardian", "보호자", "사례별 보호자 역할", "VN/Characters/Generic/generic_guardian", "left");
        AddVnCharacter("generic_child", "자녀", "사례별 자녀/청소년 역할", "VN/Characters/Generic/generic_child", "center");
        AddVnCharacter("generic_other", "가족 구성원", "사례별 가족/기관 인물", "VN/Characters/Generic/generic_other", "right");
        AddCaseCharactersFromResourceFolders();

        var ft001 = new VnCaseScript
        {
            caseId = "FT-001",
            chapter = 1,
            backgroundId = "VN/Backgrounds/counseling_room_day",
            characters = new[] { "ft001_mother", "ft001_child", "ft001_teacher", "ft001_grandmother", "supervisor_system" },
            turns = new List<VnTurn>
            {
                new VnTurn(
                    "초기 합류와 문제 정의",
                    new[]
                    {
                        new VnDialogueLine("ft001_mother", "neutral", "left", "아침마다 실랑이하다 보면 저도 모르게 소리를 질러요. 저는 밤새 일하고 와서 버틸 힘이 없어요.", "처음부터 누구 잘못인지 정하지 말고, 반복되는 아침 장면을 함께 보세요."),
                        new VnDialogueLine("ft001_child", "anxious", "center", "엄마가 나가면 집이 너무 조용해요. 학교에 가면 괜찮은 척해야 해서 더 싫어요.", "증상 뒤의 가족 불안 신호가 드러나고 있습니다."),
                        new VnDialogueLine("ft001_teacher", "concerned", "right", "교실에 오면 조용히 앉아 있습니다. 그런데 지각이 반복되니 학교도 계속 연락할 수밖에 없습니다.", "학교도 가족 체계 밖의 압력으로 작동합니다.")
                    },
                    new[]
                    {
                        new VnChoice("각자가 이 상담에서 바라는 변화와 걱정을 한 문장씩 말하게 한다.", "system", "joining", 90, "초기 합류와 순환 관찰이 모두 살아납니다.", "박성빈이 숨을 고르며 고개를 끄덕입니다. 이주형도 상담자가 자신을 문제로만 보지 않는다고 느낍니다.", "ft001_mother", "softened"),
                        new VnChoice("이주형의 등교 거부 행동을 먼저 고쳐야 한다고 선언한다.", "cbft", "ip_fixing", 35, "IP를 고정해 가족 전체 패턴을 놓칩니다.", "이주형은 시선을 내리고, 박성빈은 다시 자신이 실패한 보호자처럼 느낍니다.", "ft001_child", "withdrawn"),
                        new VnChoice("학교와 보호자가 각각 무엇을 더 강하게 요구했는지 절차부터 따진다.", "procedure", "paperwork", 45, "정보 확인은 필요하지만 합류 전 절차화는 방어를 키웁니다.", "서건창은 대답하지만 가족의 정서는 더 닫힙니다.", "ft001_teacher", "procedural")
                    }),
                new VnTurn(
                    "가족역동 개념화",
                    new[]
                    {
                        new VnDialogueLine("ft001_mother", "defensive", "left", "학교에서 전화가 오면 제가 더 다그치게 돼요. 그러면 주형이는 더 굳어버립니다.", "비난이 아니라 순환을 보이게 만들어야 합니다."),
                        new VnDialogueLine("ft001_child", "quiet", "center", "엄마가 화내면 저는 아무 말도 안 하고 싶어요. 그러면 엄마가 더 화내요.", "아이의 침묵도 상호작용의 일부입니다."),
                        new VnDialogueLine("supervisor_system", "explaining", "supervisor", "지금 핵심은 누가 시작했는지가 아니라, 서로의 반응이 다음 반응을 어떻게 부르는지입니다.", "가족체계 기본 렌즈를 명확히 유지하세요.")
                    },
                    new[]
                    {
                        new VnChoice("아침 장면을 순서대로 그리며 누가 누구에게 어떤 반응을 유도하는지 표시한다.", "system", "circular_mapping", 94, "가족체계 개념화가 회기 안에서 보이는 선택입니다.", "박성빈은 '제가 화낼수록 더 굳는 거였네요'라고 말하며 패턴을 보기 시작합니다.", "ft001_mother", "softened"),
                        new VnChoice("박성빈에게 양육 태도를 더 단호히 바꾸라고 조언한다.", "structural", "parent_directive", 52, "구조화 의도는 있으나 현재는 보호자 책임만 강조됩니다.", "박성빈의 어깨가 굳고, 이주형은 상담자가 엄마 편을 든다고 느낍니다.", "ft001_mother", "defensive"),
                        new VnChoice("이주형에게 엄마를 걱정시키지 말라고 직접 설득한다.", "strategic", "pressure", 25, "아이에게 책임을 전가해 회기가 닫힙니다.", "이주형은 대답하지 않고 의자 깊숙이 몸을 넣습니다.", "ft001_child", "withdrawn")
                    }),
                new VnTurn(
                    "감정과 구조 단서 확인",
                    new[]
                    {
                        new VnDialogueLine("ft001_grandmother", "critical", "right", "성빈이가 일을 줄이면 되잖아요. 애가 약해서 그렇지, 집이 안정되면 나아질 겁니다.", "오선진은 비판처럼 말하지만 불안과 걱정도 함께 표현하고 있습니다."),
                        new VnDialogueLine("ft001_mother", "exhausted", "left", "엄마가 그렇게 말하면 제가 가족을 망친 사람 같아요. 그래도 일을 안 할 수는 없잖아요.", "보호자 고립과 세대 간 비난 루프가 드러납니다."),
                        new VnDialogueLine("ft001_child", "scared", "center", "할머니가 오면 엄마가 더 조용해져요. 그럼 저도 말하면 안 될 것 같아요.", "아이의 증상은 어른들 사이 긴장도 감지합니다.")
                    },
                    new[]
                    {
                        new VnChoice("오선진의 걱정과 박성빈의 고립감을 동시에 반영한다.", "satir", "emotion_reflection", 84, "정서적 안전감을 높이면서 순환 관찰을 유지합니다.", "오선진의 목소리가 낮아지고, 박성빈은 처음으로 자신이 혼자가 아니길 바란다고 말합니다.", "ft001_grandmother", "softened"),
                        new VnChoice("오선진에게 비판적 말투를 즉시 고치라고 지적한다.", "cbft", "correction", 40, "맞는 방향처럼 보이지만 합류 전 직접 지적은 방어를 키웁니다.", "오선진은 입을 다물고, 박성빈은 더 난처해합니다.", "ft001_grandmother", "defensive"),
                        new VnChoice("가족이 이미 달라졌던 아침이 있었는지 예외 장면을 찾는다.", "solution", "exception", 78, "다음 과제 설계에 좋은 단서입니다.", "이주형이 '엄마가 먼저 깨워주지 않은 날은 덜 무서웠다'고 작은 예외를 꺼냅니다.", "ft001_child", "hesitant")
                    }),
                new VnTurn(
                    "핵심 개입 선택",
                    new[]
                    {
                        new VnDialogueLine("supervisor_system", "questioning", "supervisor", "이제 개입은 멋진 기법보다 가족이 자기 패턴을 볼 수 있게 돕는 질문이어야 합니다.", "순환질문을 사용할 타이밍입니다."),
                        new VnDialogueLine("ft001_mother", "worried", "left", "제가 화내지 않으면 학교에서 더 뭐라고 할까 봐 무서워요.", "박성빈의 행동도 불안 조절 방식입니다."),
                        new VnDialogueLine("ft001_child", "quiet", "center", "제가 안 가면 엄마가 집에 조금 더 있어요.", "등교 거부의 기능이 드러납니다.")
                    },
                    new[]
                    {
                        new VnChoice("순환질문으로 '주형이가 멈추면 성빈은 무엇을 하고, 그때 학교 연락은 어떻게 변하는지' 묻는다.", "system", "circular_question", 97, "이론과 사례 단서가 가장 잘 맞는 핵심 개입입니다.", "가족은 등교 거부가 누군가의 잘못이 아니라 아침 불안을 묶어두는 방식이었음을 보기 시작합니다.", "supervisor_system", "approving"),
                        new VnChoice("박성빈에게 내일부터 지각을 절대 허용하지 말라고 행동계약을 바로 제안한다.", "cbft", "premature_contract", 50, "행동계약은 가능하지만 현재는 기능 이해보다 통제 강화가 앞섭니다.", "박성빈은 실행 가능성을 걱정하고, 이주형은 다시 몸을 움츠립니다.", "ft001_mother", "anxious"),
                        new VnChoice("가족에게 이 문제는 분리불안이니 아이 치료를 먼저 받으라고 정리한다.", "procedure", "diagnostic_closure", 30, "교육적으로도 실제 회기적으로도 너무 성급한 판정입니다.", "가족은 답을 들은 듯하지만 서로의 관계 패턴은 그대로 남습니다.", "ft001_child", "withdrawn")
                    }),
                new VnTurn(
                    "다음 주 과제와 복기",
                    new[]
                    {
                        new VnDialogueLine("ft001_teacher", "concerned", "right", "학교에서도 아침 연락 방식을 조정할 수 있다면 해보겠습니다. 가족이 덜 몰리게 하는 게 중요해 보입니다.", "외부 체계도 작은 피드백 루프 조정에 참여할 수 있습니다."),
                        new VnDialogueLine("ft001_mother", "softened", "left", "제가 혼자 해결해야 한다고 생각해서 더 몰아붙였던 것 같아요.", "보호자의 자기비난이 패턴 이해로 이동합니다."),
                        new VnDialogueLine("supervisor_system", "reflective", "supervisor", "마지막 선택은 가족이 다음 주 실제로 해볼 수 있는 작고 관찰 가능한 루틴이어야 합니다.", "과제는 작아야 유지됩니다.")
                    },
                    new[]
                    {
                        new VnChoice("아침 루틴에서 박성빈, 이주형, 학교가 각각 한 가지씩 다르게 반응할 실험을 정한다.", "system", "feedback_task", 92, "회기 안 통찰을 다음 주 피드백 루프 실험으로 연결합니다.", "가족은 완벽한 해결보다 다음 아침에 해볼 수 있는 한 가지 변화를 합의합니다.", "ft001_mother", "softened"),
                        new VnChoice("이주형에게 다음 주부터 무조건 지각하지 않겠다고 약속하게 한다.", "strategic", "compliance_promise", 32, "가족 체계 과제가 아니라 아이 책임 약속으로 축소됩니다.", "이주형은 작게 대답하지만 표정은 더 굳습니다.", "ft001_child", "scared"),
                        new VnChoice("가족에게 오늘 배운 이론명을 정리해오라고 숙제를 낸다.", "procedure", "academic_homework", 28, "수련생 학습과 가족 회기 과제를 혼동한 선택입니다.", "가족은 무엇을 해야 할지 알지 못한 채 상담실을 나섭니다.", "ft001_teacher", "procedural")
                    })
            }
        };
        vnScripts[ft001.caseId] = ft001;

        foreach (var caseData in cases.Where(c => !vnScripts.ContainsKey(c.id)))
        {
            vnScripts[caseData.id] = CreateTrainingVnScript(caseData);
        }
    }

    private VnCaseScript CreateTrainingVnScript(FamilyCase caseData)
    {
        TherapyTheory recommended = theories.First(t => t.id == caseData.recommendedTheoryId);
        string supervisorId = "supervisor_" + recommended.id;
        if (recommended.id == "system") supervisorId = "supervisor_system";
        var sceneCharacters = GetCaseVnCharacterIds(caseData);
        if (sceneCharacters.Count == 0)
        {
            sceneCharacters.AddRange(new[] { "generic_guardian", "generic_child", "generic_other" });
        }
        var turns = new List<VnTurn>();
        string[] lines = caseData.familyDialogue != null && caseData.familyDialogue.Length > 0 ? caseData.familyDialogue : new[] { caseData.context };
        for (int i = 0; i < SessionTurnCount; i++)
        {
            string sceneLine = lines[Mathf.Min(i, lines.Length - 1)];
            string activeSpeaker = sceneCharacters[i % sceneCharacters.Count];
            turns.Add(new VnTurn(
                GetSceneTitleForTurn(i),
                new[]
                {
                    new VnDialogueLine(activeSpeaker, "neutral", i % 3 == 0 ? "left" : i % 3 == 1 ? "center" : "right", sceneLine, caseData.supervisorCue),
                    new VnDialogueLine(supervisorId, "neutral", "supervisor", "이 사례의 핵심 단서는 " + string.Join(", ", caseData.dynamicsTags) + "입니다. 선택한 렌즈가 가족을 한 사람 문제로 좁히지 않는지 확인하세요.", recommended.name + " 관점으로 개입의 초점을 점검합니다.")
                },
                BuildGenericVnChoices(caseData, recommended, sceneCharacters, i)));
        }

        return new VnCaseScript
        {
            caseId = caseData.id,
            chapter = caseData.chapter,
            backgroundId = "VN/Backgrounds/counseling_room_day",
            characters = sceneCharacters.Concat(new[] { supervisorId }).ToArray(),
            turns = turns
        };
    }

    private List<string> GetCaseVnCharacterIds(FamilyCase caseData)
    {
        string prefix = caseData.id.Replace("-", "").ToLowerInvariant() + "_";
        return vnCharacters
            .Where(c => c.id.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            .OrderBy(c => c.id)
            .Select(c => c.id)
            .ToList();
    }

    private IEnumerable<VnChoice> BuildGenericVnChoices(FamilyCase caseData, TherapyTheory recommended, IReadOnlyList<string> sceneCharacters, int turn)
    {
        string guardian = PickSceneCharacter(sceneCharacters, 0, "generic_guardian");
        string child = PickSceneCharacter(sceneCharacters, 1, "generic_child");
        string other = PickSceneCharacter(sceneCharacters, 2, "generic_other");
        if (turn == 0)
        {
            return new[]
            {
                new VnChoice("각 구성원이 상담에서 원하는 변화와 걱정을 한 문장씩 말하게 한다.", recommended.id, "joining", 88, "초기 합류와 사례 단서 확인이 균형을 이룹니다.", "가족은 상담자가 누군가를 바로 탓하지 않는다고 느끼며 조금 더 말하기 시작합니다.", guardian, "neutral"),
                new VnChoice("가장 문제가 커 보이는 사람을 먼저 정하고 행동 수정을 요구한다.", "procedure", "ip_fixing", 34, "IP 고정으로 가족 체계 정보를 잃습니다.", "문제로 지목된 구성원이 침묵하고 다른 가족도 방어적으로 변합니다.", child, "neutral"),
                new VnChoice("서류 정보와 위험 단서를 확인하되 가족의 표현을 끊지 않는다.", "structural", "structured_intake", caseData.riskLevel >= 70 ? 82 : 72, "위험 사례에서는 구조화가 필요하지만 합류가 함께 유지되어야 합니다.", "가족은 절차가 있다는 점에 안정감을 느끼지만 아직 조심스럽게 반응합니다.", other, "neutral")
            };
        }
        if (turn == 1)
        {
            return new[]
            {
                new VnChoice("반복 장면을 관계도로 옮기며 " + caseData.dynamicsTags[0] + " 단서가 어디서 나타나는지 확인한다.", recommended.id, "case_mapping", 92, "사례의 숨은 역동을 회기 안에서 공유 가능한 언어로 바꿉니다.", "가족은 문제 행동만 보던 시선에서 반복 패턴을 보기 시작합니다.", guardian, "neutral"),
                new VnChoice("가족에게 올바른 이론명을 설명하고 암기하게 한다.", "procedure", "lecture", 28, "학습 과제를 회기 과제로 바꿔버린 선택입니다.", "가족은 상담이 자신들의 이야기와 멀어진다고 느낍니다.", child, "neutral"),
                new VnChoice("가족이 이미 시도한 해결책이 문제를 어떻게 유지했는지 묻는다.", "strategic", "attempted_solution", 80, "시도된 해결책을 점검하는 좋은 중간 개입입니다.", "가족은 해왔던 노력이 오히려 갈등을 키웠을 수 있음을 조심스럽게 검토합니다.", other, "neutral")
            };
        }
        if (turn == 2)
        {
            return new[]
            {
                new VnChoice("감정이 올라온 구성원의 말 아래에 있는 두려움과 욕구를 반영한다.", "satir", "emotion_reflection", 82, "정서적 안전감이 높아지고 방어가 낮아집니다.", "가족은 처음으로 비난 뒤에 있던 걱정을 말로 꺼냅니다.", guardian, "neutral"),
                new VnChoice("깊은 과거 기억을 바로 말하게 해 원인을 밝히려 한다.", "psychodynamic", "premature_depth", 42, "안전감이 충분하지 않은 상태에서 깊이를 밀어붙입니다.", "가족은 부담을 느끼고 현재 대화를 닫습니다.", child, "neutral"),
                new VnChoice("가족이 한 번이라도 다르게 반응했던 예외 장면을 찾는다.", "solution", "exception_search", 80, "다음 과제 설계에 쓸 수 있는 자원 탐색입니다.", "가족은 작은 성공 경험을 떠올리며 해결 가능성을 조금 봅니다.", other, "neutral")
            };
        }
        if (turn == 3)
        {
            return new[]
            {
                new VnChoice(recommended.methods[0] + "을(를) 사용해 가족이 감당할 수 있는 개입을 실행한다.", recommended.id, "core_intervention", 95, "추천 이론과 사례 단서가 가장 잘 연결된 선택입니다.", "가족은 문제가 한 사람 안에만 있지 않다는 점을 더 분명히 이해합니다.", "supervisor_" + (recommended.id == "system" ? "system" : recommended.id), "neutral"),
                new VnChoice("상담자가 가족 대신 결론을 내려 빠르게 처방한다.", "procedure", "therapist_directive", 30, "가족의 주체성과 치료 동맹을 약화합니다.", "가족은 답을 들은 듯하지만 실제로 해볼 행동을 자기 것으로 만들지 못합니다.", guardian, "neutral"),
                new VnChoice("위험 수준에 맞춰 안전 계획과 다음 회기 구조를 먼저 정한다.", "structural", "safety_structure", caseData.riskLevel >= 70 ? 88 : 70, "위험도가 높은 사례에서는 특히 중요한 선택입니다.", "가족은 다음 만남까지 무엇을 해야 하는지 더 명확히 압니다.", other, "neutral")
            };
        }

        return new[]
        {
            new VnChoice("다음 주까지 시도할 작은 행동 실험과 실패했을 때 다시 모일 규칙을 정한다.", recommended.id, "home_practice", 90, "회기 안 통찰을 실제 가족 과제로 연결합니다.", "가족은 완벽한 해결보다 해볼 수 있는 한 가지 변화를 합의합니다.", guardian, "neutral"),
            new VnChoice("오늘 점수는 상담자가 정하고 가족은 결과만 듣게 한다.", "procedure", "score_closure", 26, "수련 평가와 가족 회기를 혼동한 선택입니다.", "가족은 평가받았다고 느끼며 회기 참여감이 낮아집니다.", child, "neutral"),
            new VnChoice("문제를 가족 밖에 이름 붙이고 다음 주 그 문제가 약해지는 순간을 관찰하게 한다.", "narrative", "externalizing_task", 78, "이야기치료적 과제로 여러 사례에 적용 가능한 마무리입니다.", "가족은 문제와 사람을 조금 분리해서 말하기 시작합니다.", other, "neutral")
        };
    }

    private static string PickSceneCharacter(IReadOnlyList<string> sceneCharacters, int index, string fallback)
    {
        if (sceneCharacters == null || sceneCharacters.Count == 0) return fallback;
        return sceneCharacters[Mathf.Min(index, sceneCharacters.Count - 1)];
    }

    private void AddVnCharacter(string id, string displayName, string role, string baseAssetPath, string defaultPosition)
    {
        if (vnCharacters.Any(c => c.id == id)) return;
        vnCharacters.Add(new VnCharacterProfile
        {
            id = id,
            displayName = displayName,
            role = role,
            baseAssetPath = baseAssetPath,
            defaultExpression = "neutral",
            defaultPosition = defaultPosition
        });
    }

    private void AddCaseCharactersFromResourceFolders()
    {
        string[] caseCharacterRoots =
        {
            "VN/Characters/Chapter01",
            "VN/Characters/CoreCases"
        };

        foreach (var caseData in cases)
        {
            string folder = caseData.id.Replace("-", "");
            foreach (string root in caseCharacterRoots)
            {
                string resourceFolder = root + "/" + folder;
                Texture2D[] textures = Resources.LoadAll<Texture2D>(resourceFolder);
                var baseIds = textures
                    .Select(t => StripExpressionSuffix(t.name))
                    .Where(id => !string.IsNullOrEmpty(id))
                    .Distinct()
                    .OrderBy(id => id)
                    .ToList();

                for (int i = 0; i < baseIds.Count; i++)
                {
                    string id = baseIds[i];
                    string position = i == 0 ? "left" : i == 1 ? "center" : "right";
                    AddVnCharacter(id, BuildDisplayNameFromAssetId(id), BuildRoleFromAssetId(id), resourceFolder + "/" + id, position);
                }
            }
        }
    }

    private static string StripExpressionSuffix(string textureName)
    {
        foreach (string expression in KnownExpressionIds.OrderByDescending(e => e.Length))
        {
            string suffix = "_" + expression;
            if (textureName.EndsWith(suffix, StringComparison.OrdinalIgnoreCase))
            {
                return textureName.Substring(0, textureName.Length - suffix.Length);
            }
        }
        return textureName;
    }

    private static string BuildDisplayNameFromAssetId(string id)
    {
        string[] parts = id.Split('_');
        if (parts.Length <= 1) return id.ToUpperInvariant();
        return parts[0].ToUpperInvariant() + " " + string.Join(" ", parts.Skip(1).Select(ToTitleToken).ToArray());
    }

    private static string BuildRoleFromAssetId(string id)
    {
        string lower = id.ToLowerInvariant();
        if (lower.Contains("mother")) return "어머니";
        if (lower.Contains("father")) return "아버지";
        if (lower.Contains("grandmother")) return "조모/외조모";
        if (lower.Contains("grandfather")) return "조부/외조부";
        if (lower.Contains("son")) return "아들";
        if (lower.Contains("daughter")) return "딸";
        if (lower.Contains("child")) return "자녀";
        if (lower.Contains("teacher")) return "교사";
        if (lower.Contains("counselor")) return "상담자";
        if (lower.Contains("worker") || lower.Contains("manager")) return "기관 관계자";
        return "사례 인물";
    }

    private static string ToTitleToken(string value)
    {
        if (string.IsNullOrEmpty(value)) return value;
        return char.ToUpperInvariant(value[0]) + value.Substring(1);
    }

    private void ShowMainMenu()
    {
        ClearCanvas();
        var root = CreateVnRoot("main-title", "VN/Backgrounds/counseling_room_day");

        CreateAbsolutePanel(root.transform, "Top Rule", new Color32(238, 232, 218, 230), new Vector2(0f, 0.962f), new Vector2(1f, 0.968f), 0, 0, 0, 0);
        CreateAbsolutePanel(root.transform, "Bottom Rule", new Color32(74, 135, 161, 225), new Vector2(0f, 0.035f), new Vector2(1f, 0.041f), 0, 0, 0, 0);

        var titleBlock = CreateAbsolutePanel(root.transform, "Title Block", new Color32(0, 0, 0, 0), new Vector2(0.055f, 0.63f), new Vector2(0.8f, 0.91f), 0, 0, 0, 0);
        var titleLayout = titleBlock.AddComponent<VerticalLayoutGroup>();
        titleLayout.spacing = 2;
        titleLayout.childControlWidth = true;
        titleLayout.childControlHeight = true;
        titleLayout.childForceExpandWidth = true;
        titleLayout.childForceExpandHeight = false;
        var title = CreateText(titleBlock.transform, "가족치료 수련센터", 64, FontStyle.Bold, Color.white);
        title.alignment = TextAnchor.UpperLeft;
        title.gameObject.AddComponent<Shadow>().effectColor = new Color32(0, 0, 0, 210);
        var subtitle = CreateText(titleBlock.transform, "사례 파일 · 슈퍼비전 · 회기 시뮬레이션", 28, FontStyle.Bold, new Color32(255, 241, 212, 255));
        subtitle.gameObject.AddComponent<Shadow>().effectColor = new Color32(0, 0, 0, 180);

        var commandPanel = CreateAbsoluteSkinnedPanel(root.transform, "Command Menu", "VN/UI/case_file_panel", new Color32(18, 21, 27, 226), new Vector2(0.055f, 0.13f), new Vector2(0.345f, 0.58f), 0, 0, 0, 0);
        var commandLayout = commandPanel.AddComponent<VerticalLayoutGroup>();
        commandLayout.padding = new RectOffset(22, 22, 22, 22);
        commandLayout.spacing = 12;
        commandLayout.childControlWidth = true;
        commandLayout.childControlHeight = true;
        commandLayout.childForceExpandWidth = true;
        commandLayout.childForceExpandHeight = false;
        CreateText(commandPanel.transform, "메뉴", 20, FontStyle.Bold, new Color32(187, 218, 228, 255));
        CreateSkinnedButton(commandPanel.transform, "캠페인 시작", "VN/UI/choice_card_intervention", Good, StartCampaignRoute);
        CreateSkinnedButton(commandPanel.transform, "사례 파일", "VN/UI/choice_card_question", Accent, ShowCaseBrowser);
        CreateSkinnedButton(commandPanel.transform, "이어하기", "VN/UI/choice_card_question", Warm, ContinueFromLastCase);
        CreateSkinnedButton(commandPanel.transform, "저장 / 불러오기", "VN/UI/choice_card_question", MutedInk, ShowSaveLoad);
        CreateSkinnedButton(commandPanel.transform, "학습 기록", "VN/UI/choice_card_question", MutedInk, ShowDashboard);
        CreateSkinnedButton(commandPanel.transform, aiSupervisorEnabled ? "AI 슈퍼바이저 켜짐" : "AI 슈퍼바이저 꺼짐", "VN/UI/choice_card_question", aiSupervisorEnabled ? Good : Accent, () =>
        {
            aiSupervisorEnabled = !aiSupervisorEnabled;
            WriteSaveSlot(1);
            ShowMainMenu();
        });

        var dossier = CreateAbsoluteSkinnedPanel(root.transform, "Dossier Panel", "VN/UI/case_file_panel", new Color32(238, 232, 218, 238), new Vector2(0.61f, 0.16f), new Vector2(0.945f, 0.82f), 0, 0, 0, 0);
        var dossierLayout = dossier.AddComponent<VerticalLayoutGroup>();
        dossierLayout.padding = new RectOffset(24, 24, 22, 22);
        dossierLayout.spacing = 10;
        dossierLayout.childControlWidth = true;
        dossierLayout.childControlHeight = true;
        dossierLayout.childForceExpandWidth = true;
        dossierLayout.childForceExpandHeight = false;
        FamilyCase featured = currentCase ?? cases.First(c => c.id == "FT-001");
        TherapyTheory featuredTheory = theories.First(t => t.id == featured.recommendedTheoryId);
        CreateText(dossier.transform, "현재 사례", 19, FontStyle.Bold, Accent);
        CreateText(dossier.transform, featured.id + "  " + featured.familyType, 31, FontStyle.Bold, Ink);
        CreateText(dossier.transform, featured.presentingProblem, 22, FontStyle.Bold, Warm);
        CreateText(dossier.transform, "추천 렌즈: " + featuredTheory.name + "\n위험도: " + featured.riskLevel + "\n진행 기록: " + logs.Count + "건", 20, FontStyle.Normal, MutedInk);
        CreateSpacer(dossier.transform, 8);
        CreateText(dossier.transform, "슈퍼바이저", 19, FontStyle.Bold, Accent);
        SupervisorProfile supervisor = GetSupervisorForTheory(featured.recommendedTheoryId);
        CreateText(dossier.transform, supervisor.name + "\n" + supervisor.openingLine, 19, FontStyle.Normal, MutedInk);
        CreateSpacer(dossier.transform, 8);
        CreateSkinnedButton(dossier.transform, "사례 파일 열기", "VN/UI/choice_card_intervention", Accent, () => BeginCaseIntake(featured));

        var progress = CreateAbsoluteSkinnedPanel(root.transform, "Progress Strip", "VN/UI/metrics_hud", new Color32(23, 27, 34, 230), new Vector2(0.37f, 0.13f), new Vector2(0.585f, 0.43f), 0, 0, 0, 0);
        var progressLayout = progress.AddComponent<VerticalLayoutGroup>();
        progressLayout.padding = new RectOffset(18, 18, 16, 16);
        progressLayout.spacing = 8;
        progressLayout.childControlWidth = true;
        progressLayout.childControlHeight = true;
        progressLayout.childForceExpandWidth = true;
        progressLayout.childForceExpandHeight = false;
        int completedCases = logs.Select(l => l.caseId).Distinct().Count();
        int completedCoreCases = logs.Select(l => l.caseId).Distinct().Count(id => cases.OrderBy(c => c.id).Take(24).Any(c => c.id == id));
        CreateText(progress.transform, "진행 현황", 20, FontStyle.Bold, new Color32(187, 218, 228, 255));
        CreateBar(progress.transform, "완료 사례 " + completedCases + "/" + cases.Count, completedCases, cases.Count, Good);
        CreateBar(progress.transform, "핵심 사례 " + completedCoreCases + "/24", completedCoreCases, 24, Accent);
        CreateText(progress.transform, "회기 스크립트 " + cases.Count(HasVnScript) + "/" + cases.Count + "\n적용 이미지 " + CountCommercialVnAssets() + "/" + CommercialAssetTarget, 18, FontStyle.Bold, new Color32(238, 232, 218, 255));

        var footer = CreateAbsolutePanel(root.transform, "Footer", new Color32(18, 21, 27, 205), new Vector2(0.055f, 0.045f), new Vector2(0.945f, 0.095f), 0, 0, 0, 0);
        var footerText = CreateText(footer.transform, "자동 저장 슬롯 1 · 진행 기록 " + logs.Count + "건", 18, FontStyle.Bold, new Color32(226, 228, 232, 255));
        footerText.alignment = TextAnchor.MiddleLeft;
        Stretch(footerText.gameObject, 20, 0, 20, 0);
    }

    private void ContinueFromLastCase()
    {
        if (currentCase != null)
        {
            BeginCaseIntake(currentCase);
            return;
        }
        ShowCaseBrowser();
    }

    private void ShowCaseBrowser()
    {
        ClearCanvas();
        var root = CreateScreenRoot("case-browser");
        int maxPage = Mathf.Max(0, Mathf.CeilToInt(cases.Count / (float)CasesPerBrowserPage) - 1);
        caseBrowserPage = Mathf.Clamp(caseBrowserPage, 0, maxPage);
        CreateHeader(root, "센터 로비: 사례 파일", "사례를 직접 선택해 VN 회기 또는 훈련 회기로 진입합니다.");

        var columns = CreateHorizontal(root, "Browser Columns");
        columns.padding = new RectOffset(30, 30, 22, 30);
        columns.spacing = 22;

        var list = CreateCard(columns.transform, "Case Files", Paper);
        SetLayout(list, 0, 1, 1180, -1);
        CreateText(list.transform, "사례 파일 " + (caseBrowserPage + 1) + "/" + (maxPage + 1), 34, FontStyle.Bold, Ink);
        CreateMetricRow(list.transform, "총 사례", cases.Count.ToString(CultureInfo.InvariantCulture), "VN 가능", cases.Count(HasVnScript).ToString(CultureInfo.InvariantCulture), "완료 로그", logs.Count.ToString(CultureInfo.InvariantCulture));
        foreach (var caseData in cases.Skip(caseBrowserPage * CasesPerBrowserPage).Take(CasesPerBrowserPage))
        {
            FamilyCase captured = caseData;
            string theoryName = theories.First(t => t.id == caseData.recommendedTheoryId).name;
            string status = HasVnScript(caseData) ? "VN" : "훈련";
            CreateButton(list.transform, caseData.id + "  " + caseData.familyType + "  |  " + theoryName + "  |  " + status, caseData.isHandcrafted ? Good : Accent, () => BeginCaseIntake(captured));
        }

        var nav = CreateHorizontal(list, "Case Browser Nav");
        nav.spacing = 10;
        CreateButton(nav.transform, "이전 페이지", caseBrowserPage > 0 ? Accent : MutedInk, () =>
        {
            if (caseBrowserPage > 0) caseBrowserPage--;
            ShowCaseBrowser();
        });
        CreateButton(nav.transform, "다음 페이지", caseBrowserPage < maxPage ? Accent : MutedInk, () =>
        {
            if (caseBrowserPage < maxPage) caseBrowserPage++;
            ShowCaseBrowser();
        });
        CreateButton(nav.transform, "메인 메뉴", MutedInk, ShowMainMenu);

        var detail = CreateCard(columns.transform, "Lobby Status", new Color32(34, 38, 47, 255));
        SetLayout(detail, 0, 1, 620, -1);
        CreateText(detail.transform, "수련 기록", 32, FontStyle.Bold, Color.white);
        CreateText(detail.transform, "자동 저장 슬롯: 1\n저장 상태: 진행 기록 " + logs.Count + "건", 19, FontStyle.Normal, new Color32(226, 228, 232, 255));
        CreateSpacer(detail.transform, 12);
        foreach (var group in cases.GroupBy(c => c.chapter).OrderBy(g => g.Key))
        {
            int completed = logs.Select(l => l.caseId).Distinct().Count(id => cases.First(c => c.id == id).chapter == group.Key);
            CreateBar(detail.transform, "Chapter " + group.Key + " 완료 " + completed + "/" + group.Count(), completed, group.Count(), Good);
        }
        CreateSpacer(detail.transform, 12);
        CreateButton(detail.transform, "저장 / 불러오기", Warm, ShowSaveLoad);
        CreateButton(detail.transform, "Export", Good, () =>
        {
            ExportAll();
            ShowDashboard();
        });
    }

    private void ShowEthics()
    {
        ClearCanvas();
        var root = CreateScreenRoot("ethics");
        CreateHeader(root, "윤리·합성 데이터 고지", "이 게임은 교육용 가족치료 학습 시뮬레이션입니다.");
        var card = CreateCard(root.transform, "Ethics Card", Paper);
        SetLayout(card, 1, 1, -1, -1);
        CreateText(card.transform, "중요 고지", 34, FontStyle.Bold, Ink);
        CreateText(card.transform,
            "이 게임의 가족명, 사례, 대화, 위험도, 상담 기록은 모두 창작된 합성 데이터입니다. 실제 상담 원문, 개인정보, 실제 가족 사례, 영화/드라마 원작 사례를 포함하지 않습니다.\n\n" +
            "게임의 점수와 슈퍼비전 해설은 가족치료 개념 학습을 돕기 위한 교육용 피드백입니다. 실제 상담, 의료, 법률, 복지 판단을 대체하지 않습니다.\n\n" +
            "선택형 AI 슈퍼바이저는 추후 확장 지점이며, 활성화하더라도 공식 점수 판정에는 영향을 주지 않는 참고 코멘트로만 사용합니다.", 26, FontStyle.Normal, Ink);
        CreateSpacer(card.transform, 22);
        CreateButton(card.transform, "메인 메뉴", Accent, ShowMainMenu);
    }

    private void ShowSaveLoad()
    {
        ClearCanvas();
        var root = CreateScreenRoot("save-load");
        CreateHeader(root, "저장 / 불러오기", "플레이 로그, 마지막 사례, 설정을 로컬 슬롯에 저장합니다.");
        var columns = CreateHorizontal(root, "Save Columns");
        columns.padding = new RectOffset(30, 30, 22, 30);
        columns.spacing = 22;

        var left = CreateCard(columns.transform, "Save Slots", Paper);
        SetLayout(left, 0, 1, 1100, -1);
        CreateText(left.transform, "저장 슬롯", 34, FontStyle.Bold, Ink);
        CreateText(left.transform, "슬롯 1은 회기 종료 때 자동 저장됩니다. 슬롯 2~3은 사용자가 수동으로 백업할 수 있습니다.", 21, FontStyle.Normal, MutedInk);
        for (int slot = 1; slot <= 3; slot++)
        {
            int captured = slot;
            GameSaveData preview = ReadSaveSlot(slot);
            string label = preview == null
                ? "Slot " + slot + "  빈 슬롯"
                : "Slot " + slot + "  " + preview.savedAt + "  |  로그 " + (preview.logs == null ? 0 : preview.logs.Count) + "건  |  마지막 " + preview.lastCaseId;
            CreateText(left.transform, label, 21, FontStyle.Bold, preview == null ? MutedInk : Ink);
            var row = CreateHorizontal(left, "Save Slot " + slot);
            row.spacing = 10;
            CreateButton(row.transform, "저장", Good, () =>
            {
                WriteSaveSlot(captured);
                ShowSaveLoad();
            });
            CreateButton(row.transform, "불러오기", preview == null ? MutedInk : Accent, () =>
            {
                if (LoadSaveSlot(captured, true)) ShowMainMenu();
                else ShowSaveLoad();
            });
        }

        var right = CreateCard(columns.transform, "Save Status", new Color32(34, 38, 47, 255));
        SetLayout(right, 0, 1, 720, -1);
        CreateText(right.transform, "현재 진행", 32, FontStyle.Bold, Color.white);
        CreateMetricRow(right.transform, "로그", logs.Count.ToString(CultureInfo.InvariantCulture), "완료 사례", logs.Select(l => l.caseId).Distinct().Count().ToString(CultureInfo.InvariantCulture), "AI", aiSupervisorEnabled ? "ON" : "OFF");
        CreateText(right.transform, "마지막 현재 사례:\n" + (currentCase == null ? "없음" : currentCase.id + " · " + currentCase.familyType), 20, FontStyle.Normal, new Color32(226, 228, 232, 255));
        CreateSpacer(right.transform, 12);
        CreateButton(right.transform, "센터 로비", Accent, ShowCaseBrowser);
        CreateButton(right.transform, "메인 메뉴", MutedInk, ShowMainMenu);
    }

    private string GetSaveSlotPath(int slot)
    {
        return Path.Combine(Application.persistentDataPath, "family_therapy_save_slot_" + slot + ".json");
    }

    private GameSaveData ReadSaveSlot(int slot)
    {
        string path = GetSaveSlotPath(slot);
        if (!File.Exists(path)) return null;
        try
        {
            return JsonUtility.FromJson<GameSaveData>(File.ReadAllText(path, Encoding.UTF8));
        }
        catch (Exception ex)
        {
            Debug.LogWarning("Failed to read save slot " + slot + ": " + ex.Message);
            return null;
        }
    }

    private void WriteSaveSlot(int slot)
    {
        var data = new GameSaveData
        {
            savedAt = DateTime.Now.ToString("s", CultureInfo.InvariantCulture),
            lastCaseId = currentCase == null ? "" : currentCase.id,
            aiSupervisorEnabled = aiSupervisorEnabled,
            logs = logs.ToList()
        };
        File.WriteAllText(GetSaveSlotPath(slot), JsonUtility.ToJson(data, true), new UTF8Encoding(false));
    }

    private bool LoadSaveSlot(int slot, bool report)
    {
        GameSaveData data = ReadSaveSlot(slot);
        if (data == null) return false;
        logs.Clear();
        if (data.logs != null) logs.AddRange(data.logs);
        aiSupervisorEnabled = data.aiSupervisorEnabled;
        if (!string.IsNullOrEmpty(data.lastCaseId))
        {
            currentCase = cases.FirstOrDefault(c => c.id == data.lastCaseId);
        }
        if (report) Debug.Log("Loaded Family Therapy save slot " + slot + " logs=" + logs.Count);
        return true;
    }

    private void ShowCaseIntake()
    {
        if (cases.Count == 0) return;
        BeginCaseIntake(cases[logs.Count % cases.Count]);
    }

    private void StartCampaignRoute()
    {
        BeginVnCase(cases.First(c => c.id == "FT-001"));
    }

    private void BeginCaseIntake(FamilyCase caseData)
    {
        bool isNewCase = currentCase == null || currentCase.id != caseData.id;
        currentCase = caseData;
        if (isNewCase || selectedTheory == null) selectedTheory = theories.First(t => t.id == currentCase.recommendedTheoryId);
        if (isNewCase)
        {
            currentTurn = 0;
            sessionScore = 0;
            trustScore = 50;
            safetyScore = 50;
            insightScore = 50;
            currentSelections.Clear();
        }

        ClearCanvas();
        var root = CreateScreenRoot("case-intake");
        CreateHeader(root, "사례 접수: " + currentCase.id, "Chapter " + currentCase.chapter + " · " + currentCase.title);
        var columns = CreateHorizontal(root, "Intake Columns");
        columns.padding = new RectOffset(30, 30, 22, 30);
        columns.spacing = 22;

        var file = CreateCard(columns.transform, "Case File", Paper);
        SetLayout(file, 0, 1, 1040, -1);
        CreateText(file.transform, currentCase.familyType, 34, FontStyle.Bold, Ink);
        CreateText(file.transform, "주호소: " + currentCase.presentingProblem, 25, FontStyle.Bold, Warm);
        CreateText(file.transform, currentCase.context, 23, FontStyle.Normal, Ink);
        CreateSpacer(file.transform, 12);
        CreateText(file.transform, "가족 관계도", 27, FontStyle.Bold, Ink);
        CreateText(file.transform, currentCase.familyMap, 21, FontStyle.Normal, MutedInk);
        CreateSpacer(file.transform, 10);
        CreateText(file.transform, "수련 목표", 27, FontStyle.Bold, Ink);
        CreateText(file.transform, currentCase.learningObjective, 21, FontStyle.Normal, MutedInk);
        CreateSpacer(file.transform, 12);
        CreateText(file.transform, "초기 대화 발췌", 27, FontStyle.Bold, Ink);
        foreach (string line in currentCase.familyDialogue.Take(4))
        {
            CreateText(file.transform, line, 21, FontStyle.Normal, MutedInk);
        }
        CreateSpacer(file.transform, 12);
        CreateMetricRow(file.transform, "위험도", currentCase.riskLevel.ToString(CultureInfo.InvariantCulture), "관계 태그", string.Join(", ", currentCase.dynamicsTags), "현재 로그", logs.Count.ToString(CultureInfo.InvariantCulture));

        var lens = CreateCard(columns.transform, "Theory Lens", new Color32(34, 38, 47, 255));
        SetLayout(lens, 0, 1, 760, -1);
        CreateText(lens.transform, "이론 렌즈 선택", 32, FontStyle.Bold, Color.white);
        CreateText(lens.transform, "먼저 이 가족을 어떤 이론으로 개념화할지 선택하세요. 정답은 하나로 고정되지 않지만, 사례의 핵심 단서와 가장 잘 맞는 렌즈가 있습니다.", 21, FontStyle.Normal, new Color32(226, 228, 232, 255));
        foreach (var theory in theories)
        {
            TherapyTheory captured = theory;
            CreateButton(lens.transform, theory.name, theory == selectedTheory ? Good : Accent, () =>
            {
                selectedTheory = captured;
                BeginCaseIntake(currentCase);
            });
        }
        CreateSpacer(lens.transform, 8);
        CreateText(lens.transform, "현재 선택: " + selectedTheory.name, 22, FontStyle.Bold, new Color32(248, 242, 222, 255));
        var supervisor = GetSupervisorForTheory(selectedTheory.id);
        CreateText(lens.transform, "담당 슈퍼바이저: " + supervisor.name + "\n" + supervisor.openingLine, 19, FontStyle.Normal, new Color32(226, 228, 232, 255));
        CreateButton(lens.transform, HasVnScript(currentCase) ? "VN 회기 시작" : "훈련 회기 시작", Warm, StartCurrentCaseSession);
        CreateButton(lens.transform, "메인 메뉴", MutedInk, ShowMainMenu);
    }

    private void StartCurrentCaseSession()
    {
        if (HasVnScript(currentCase))
        {
            BeginVnCase(currentCase);
            return;
        }

        currentVnScript = null;
        ShowSessionTurn();
    }

    private bool HasVnScript(FamilyCase caseData)
    {
        if (caseData == null) return false;
        return vnScripts.TryGetValue(caseData.id, out VnCaseScript script) && script.turns != null && script.turns.Count >= SessionTurnCount;
    }

    private void BeginVnCase(FamilyCase caseData)
    {
        currentCase = caseData;
        selectedTheory = theories.First(t => t.id == currentCase.recommendedTheoryId);
        currentVnScript = GetVnScript(currentCase.id);
        currentTurn = 0;
        currentVnLineIndex = 0;
        sessionScore = 0;
        trustScore = 50;
        safetyScore = 50;
        insightScore = 50;
        currentSelections.Clear();

        if (currentVnScript == null || currentVnScript.turns == null || currentVnScript.turns.Count == 0)
        {
            ShowSessionTurn();
            return;
        }

        ShowVnSessionTurn();
    }

    private VnCaseScript GetVnScript(string caseId)
    {
        return vnScripts.TryGetValue(caseId, out VnCaseScript script) ? script : null;
    }

    private VnCharacterProfile GetVnCharacter(string characterId)
    {
        return vnCharacters.FirstOrDefault(c => c.id == characterId);
    }

    private void ShowVnSessionTurn()
    {
        if (currentVnScript == null || currentTurn >= currentVnScript.turns.Count)
        {
            SaveSessionLog();
            ShowSupervision();
            return;
        }

        VnTurn turn = currentVnScript.turns[currentTurn];
        if (turn.setupLines == null || turn.setupLines.Count == 0)
        {
            ShowVnChoiceDeck(turn);
            return;
        }

        currentVnLineIndex = Mathf.Clamp(currentVnLineIndex, 0, turn.setupLines.Count - 1);
        VnDialogueLine line = turn.setupLines[currentVnLineIndex];
        var root = CreateVnRoot("vn-session", currentVnScript.backgroundId);
        CreateVnHud(root.transform, currentCase.id, turn.title, "회기 " + (currentTurn + 1) + "/" + SessionTurnCount);
        CreateVnStage(root.transform, line.speakerId, line.expressionId);
        if (!string.IsNullOrEmpty(line.supervisorNote))
        {
            CreateVnSupervisorNote(root.transform, line.supervisorNote);
        }
        CreateVnDialogueBox(root.transform, line.speakerId, line.text, currentVnLineIndex + 1, turn.setupLines.Count, () =>
        {
            if (currentVnLineIndex < turn.setupLines.Count - 1)
            {
                currentVnLineIndex++;
                ShowVnSessionTurn();
            }
            else
            {
                ShowVnChoiceDeck(turn);
            }
        });
    }

    private void ShowVnChoiceDeck(VnTurn turn)
    {
        string activeSpeaker = turn.setupLines != null && turn.setupLines.Count > 0 ? turn.setupLines.Last().speakerId : "supervisor_system";
        var root = CreateVnRoot("vn-choices", currentVnScript.backgroundId);
        CreateVnHud(root.transform, currentCase.id, turn.title, "개입 선택");
        CreateVnStage(root.transform, activeSpeaker, "neutral");
        CreateVnSupervisorNote(root.transform, "김혜성: 선택지는 모두 그럴듯합니다. 지금 가족이 감당할 수 있고, 사례의 순환 패턴을 더 잘 보이게 하는 개입을 고르세요.");

        var panel = CreateAbsoluteSkinnedPanel(root.transform, "Choice Panel", "VN/UI/case_file_panel", new Color32(238, 232, 218, 246), new Vector2(0.56f, 0.19f), new Vector2(0.96f, 0.83f), 0, 0, 0, 0);
        var layout = panel.AddComponent<VerticalLayoutGroup>();
        layout.padding = new RectOffset(18, 18, 16, 16);
        layout.spacing = 12;
        layout.childControlWidth = true;
        layout.childControlHeight = true;
        layout.childForceExpandWidth = true;
        layout.childForceExpandHeight = false;
        CreateText(panel.transform, "상담자 개입 선택", 27, FontStyle.Bold, Ink);
        CreateText(panel.transform, "현재 렌즈: " + selectedTheory.name + "\n가족 반응과 점수 로그에 기록됩니다.", 18, FontStyle.Normal, MutedInk);
        foreach (VnChoice choice in turn.choices)
        {
            VnChoice captured = choice;
            CreateSkinnedButton(panel.transform, choice.label, "VN/UI/choice_card_intervention", Accent, () => ApplyVnChoice(captured));
        }
    }

    private void ApplyVnChoice(VnChoice choice)
    {
        var sessionChoice = new SessionChoice(choice.label, choice.theoryId, choice.quality, choice.feedback, choice.interventionType, choice.familyReaction, choice.reactionSpeakerId, choice.reactionExpressionId);
        int beforeTrust = trustScore;
        int beforeSafety = safetyScore;
        int beforeInsight = insightScore;
        ApplyChoiceScores(sessionChoice);

        currentSelections.Add(new SessionSelection
        {
            turn = currentTurn + 1,
            choice = choice.label,
            theoryId = choice.theoryId,
            quality = Mathf.Clamp(choice.quality + (choice.theoryId == selectedTheory.id ? 8 : 0) + (choice.theoryId == currentCase.recommendedTheoryId ? 12 : 0), 0, 100),
            feedback = choice.feedback,
            interventionType = choice.interventionType,
            familyReaction = choice.familyReaction,
            reactionSpeakerId = choice.reactionSpeakerId,
            reactionExpressionId = choice.reactionExpressionId,
            trustDelta = trustScore - beforeTrust,
            safetyDelta = safetyScore - beforeSafety,
            insightDelta = insightScore - beforeInsight
        });

        bool completed = currentTurn + 1 >= SessionTurnCount;
        ShowVnReaction(choice, completed);
    }

    private void ShowVnReaction(VnChoice choice, bool completed)
    {
        var root = CreateVnRoot("vn-reaction", currentVnScript.backgroundId);
        CreateVnHud(root.transform, currentCase.id, "가족 반응", completed ? "회기 종료 전 복기" : "다음 장면으로");
        CreateVnStage(root.transform, choice.reactionSpeakerId, choice.reactionExpressionId);
        CreateVnSupervisorNote(root.transform, choice.feedback);
        CreateVnDialogueBox(root.transform, choice.reactionSpeakerId, choice.familyReaction, currentTurn + 1, SessionTurnCount, () =>
        {
            currentTurn++;
            currentVnLineIndex = 0;
            if (currentTurn >= SessionTurnCount)
            {
                SaveSessionLog();
                ShowSupervision();
            }
            else
            {
                ShowVnSessionTurn();
            }
        });
    }

    private void ApplyChoiceScores(SessionChoice choice)
    {
        int lensBonus = choice.theoryId == selectedTheory.id ? 8 : 0;
        int caseBonus = choice.theoryId == currentCase.recommendedTheoryId ? 12 : 0;
        int gained = Mathf.Clamp(choice.quality + lensBonus + caseBonus, 0, 100);
        sessionScore += gained;
        trustScore = Mathf.Clamp(trustScore + (gained - 50) / 4, 0, 100);
        safetyScore = Mathf.Clamp(safetyScore + (gained - 45) / 5, 0, 100);
        insightScore = Mathf.Clamp(insightScore + (gained - 40) / 4, 0, 100);
    }

    private void ShowSessionTurn()
    {
        ClearCanvas();
        var root = CreateScreenRoot("session");
        CreateHeader(root, "회기 " + (currentTurn + 1) + "/" + SessionTurnCount, currentCase.familyType + " · 선택 렌즈: " + selectedTheory.name);
        var columns = CreateHorizontal(root, "Session Columns");
        columns.padding = new RectOffset(30, 30, 22, 30);
        columns.spacing = 22;

        var scene = CreateCard(columns.transform, "Visual Novel Scene", new Color32(30, 34, 42, 255));
        SetLayout(scene, 0, 1, 1040, -1);
        CreateText(scene.transform, GetSceneTitle(), 34, FontStyle.Bold, Color.white);
        CreateText(scene.transform, GetSceneNarration(), 24, FontStyle.Normal, new Color32(235, 237, 241, 255));
        CreateSpacer(scene.transform, 14);
        CreateText(scene.transform, currentCase.familyDialogue[Mathf.Min(currentTurn, currentCase.familyDialogue.Length - 1)], 27, FontStyle.Bold, new Color32(255, 241, 212, 255));
        CreateText(scene.transform, "슈퍼바이저 노트: " + currentCase.supervisorCue, 20, FontStyle.Normal, new Color32(210, 214, 220, 255));
        CreateSpacer(scene.transform, 14);
        CreateMetricRow(scene.transform, "신뢰", trustScore.ToString(CultureInfo.InvariantCulture), "안전감", safetyScore.ToString(CultureInfo.InvariantCulture), "통찰", insightScore.ToString(CultureInfo.InvariantCulture));

        var choices = CreateCard(columns.transform, "Choices", Paper);
        SetLayout(choices, 0, 1, 760, -1);
        CreateText(choices.transform, "상담자 선택", 32, FontStyle.Bold, Ink);
        CreateText(choices.transform, "아래 선택지는 모두 그럴듯하지만, 사례 단서와 선택한 이론 렌즈에 얼마나 맞는지가 다릅니다.", 21, FontStyle.Normal, Ink);
        foreach (var choice in BuildChoicesForCurrentTurn())
        {
            SessionChoice captured = choice;
            CreateButton(choices.transform, choice.label, choice.quality >= 80 ? Good : choice.quality >= 55 ? Warn : Bad, () => ApplyChoice(captured));
        }
    }

    private string GetSceneTitle()
    {
        return GetSceneTitleForTurn(currentTurn);
    }

    private string GetSceneTitleForTurn(int turn)
    {
        if (turn == 0) return "초기 합류와 문제 정의";
        if (turn == 1) return "가족역동 개념화";
        if (turn == 2) return "감정과 구조 단서 확인";
        if (turn == 3) return "핵심 개입 선택";
        return "다음 주 과제와 복기";
    }

    private string GetSceneNarration()
    {
        if (currentTurn == 0) return "가족은 아직 상담자를 신뢰하지 못합니다. 누가 문제인지 빨리 판정받고 싶어 하지만, 섣부른 편들기는 회기를 닫아버릴 수 있습니다.";
        if (currentTurn == 1) return "대화가 조금 열리자 반복되는 상호작용 패턴이 보이기 시작합니다. 지금은 증상을 개인 안에만 놓지 않고 관계 안에서 읽어야 합니다.";
        if (currentTurn == 2) return "이제 표면 행동 아래의 감정, 관계 구조, 가족 규칙을 더 구체적으로 확인해야 합니다. 개입보다 관찰이 먼저일 수 있습니다.";
        if (currentTurn == 3) return "핵심 개입은 이론명보다 사례 단서와 맞아야 합니다. 가족이 감당할 수 없는 멋진 기법은 좋은 선택이 아닙니다.";
        return "마지막 선택은 다음 주까지 가족이 실제로 해볼 과제와 회고 질문입니다. 회기가 끝난 뒤에도 변화가 이어져야 합니다.";
    }

    private List<SessionChoice> BuildChoicesForCurrentTurn()
    {
        TherapyTheory recommended = theories.First(t => t.id == currentCase.recommendedTheoryId);
        var list = new List<SessionChoice>();
        if (currentTurn == 0)
        {
            list.Add(new SessionChoice("먼저 각자가 이 상담에 기대하는 변화와 걱정을 한 문장씩 말하게 한다.", "system", 78, "초기 합류와 순환적 관찰에 도움이 됩니다."));
            list.Add(new SessionChoice("가장 문제가 큰 사람을 정해 그 사람의 행동부터 고치자고 제안한다.", "cbft", 35, "IP를 고정해 가족 전체 패턴을 놓칠 위험이 큽니다."));
            list.Add(new SessionChoice(recommended.name + " 관점으로 사례의 핵심 단서를 확인하는 질문을 던진다.", recommended.id, 88, "선택한 이론과 사례 단서를 연결하는 좋은 출발입니다."));
            list.Add(new SessionChoice("가족이 이미 실패했다고 말한 해결책을 더 강하게 반복하도록 권한다.", "strategic", 42, "시도된 해결책을 검토하기 전 반복을 강화하면 방어가 커집니다."));
        }
        else if (currentTurn == 1)
        {
            list.Add(new SessionChoice("가족의 반복 패턴을 관계도에 표시하고 누가 누구에게 어떤 반응을 유도하는지 확인한다.", "system", 82, "가족체계 관점의 핵심 기술입니다."));
            list.Add(new SessionChoice("현재 문제를 " + string.Join("/", currentCase.dynamicsTags) + " 단서와 연결해 개념화한다.", recommended.id, 92, "이 사례의 숨은 패턴과 가장 잘 맞는 개념화입니다."));
            list.Add(new SessionChoice("가족의 말을 교정하며 올바른 가족치료 이론명을 암기하게 한다.", "cbft", 30, "학습 게임 안에서는 가능하지만 실제 회기 장면으로는 부적절합니다."));
            list.Add(new SessionChoice("감정이 올라온 구성원에게 잠시 침묵하게 하고 서류 정보만 다시 확인한다.", "procedure", 38, "안전한 구조화가 아니라 정서 접촉을 회피하는 선택입니다."));
        }
        else if (currentTurn == 2)
        {
            list.Add(new SessionChoice("가족 관계도에서 " + currentCase.dynamicsTags[0] + " 단서가 반복되는 장면을 가족에게 천천히 되짚어 보인다.", recommended.id, 90, "핵심 단서를 회기 안에서 공유 가능한 언어로 바꾸는 선택입니다."));
            list.Add(new SessionChoice("가장 감정이 격한 구성원에게 바로 깊은 과거 기억을 말하게 한다.", "psychodynamic", 42, "안전감이 충분하지 않을 때 깊은 정서를 밀어붙이면 방어가 커질 수 있습니다."));
            list.Add(new SessionChoice("각자 오늘 대화에서 피하고 싶은 주제가 무엇인지 말하게 한다.", "satir", 78, "안전감과 정서 단서를 함께 확인하는 중간 개입입니다."));
            list.Add(new SessionChoice("가족이 이미 한 번이라도 다르게 반응했던 예외 장면을 찾는다.", "solution", 80, "해결중심 관점뿐 아니라 다음 과제 설계에도 도움이 됩니다."));
        }
        else if (currentTurn == 3)
        {
            list.Add(new SessionChoice(recommended.methods[0] + "을(를) 사용해 다음 주까지의 구체적 연습 과제를 정한다.", recommended.id, 95, "이론과 개입이 잘 맞고 실천 가능성도 있습니다."));
            list.Add(new SessionChoice("가족 전체에게 오늘부터 서로 화내지 않겠다는 약속만 받는다.", "solution", 45, "목표는 좋지만 관찰 가능한 행동과 조건이 부족합니다."));
            list.Add(new SessionChoice("위험 단서가 있으므로 가족의 안전 계획과 다음 회기 구조를 먼저 정한다.", "structural", currentCase.riskLevel >= 70 ? 86 : 68, "고위험 사례에서는 특히 적절합니다."));
            list.Add(new SessionChoice("상담자가 가족 대신 결론을 내려 가족에게 통보한다.", "strategic", 25, "가족의 주체성과 치료 동맹을 약화시킵니다."));
        }
        else
        {
            list.Add(new SessionChoice("가족이 다음 주까지 시도할 한 가지 행동과 실패했을 때 다시 모일 규칙을 함께 정한다.", recommended.id, 90, "회기 안 통찰을 실제 가족 과제로 연결합니다."));
            list.Add(new SessionChoice("오늘 점수는 상담자가 정할 테니 가족은 결과만 듣고 가게 한다.", "system", 28, "수련 평가와 가족 회기를 혼동한 선택입니다."));
            list.Add(new SessionChoice("각자 오늘 새롭게 알게 된 가족의 마음을 한 문장으로 말하게 한다.", "satir", 82, "정서적 마무리와 복기에 도움이 됩니다."));
            list.Add(new SessionChoice("문제 이름을 가족 밖에 적어두고, 다음 주 그 문제가 약해지는 순간을 관찰하게 한다.", "narrative", 80, "이야기치료적 회고 과제로 적절합니다."));
        }
        return list.OrderBy(c => c.label).ToList();
    }

    private void ApplyChoice(SessionChoice choice)
    {
        int beforeTrust = trustScore;
        int beforeSafety = safetyScore;
        int beforeInsight = insightScore;
        ApplyChoiceScores(choice);
        int gained = Mathf.Clamp(choice.quality + (choice.theoryId == selectedTheory.id ? 8 : 0) + (choice.theoryId == currentCase.recommendedTheoryId ? 12 : 0), 0, 100);

        currentSelections.Add(new SessionSelection
        {
            turn = currentTurn + 1,
            choice = choice.label,
            theoryId = choice.theoryId,
            quality = gained,
            feedback = choice.feedback,
            interventionType = choice.interventionType,
            familyReaction = choice.familyReaction,
            reactionSpeakerId = choice.reactionSpeakerId,
            reactionExpressionId = choice.reactionExpressionId,
            trustDelta = trustScore - beforeTrust,
            safetyDelta = safetyScore - beforeSafety,
            insightDelta = insightScore - beforeInsight
        });

        currentTurn++;
        if (currentTurn >= SessionTurnCount)
        {
            SaveSessionLog();
            ShowSupervision();
        }
        else
        {
            ShowSessionTurn();
        }
    }

    private void SaveSessionLog()
    {
        TherapyTheory recommended = theories.First(t => t.id == currentCase.recommendedTheoryId);
        int average = Mathf.RoundToInt(sessionScore / (float)SessionTurnCount);
        logs.Add(new PlayerChoiceLog
        {
            sessionId = DateTime.Now.ToString("yyyyMMdd-HHmmss", CultureInfo.InvariantCulture) + "-" + currentCase.id,
            caseId = currentCase.id,
            chapter = currentCase.chapter,
            familyType = currentCase.familyType,
            selectedTheory = selectedTheory.name,
            recommendedTheory = recommended.name,
            matchedRecommendedTheory = selectedTheory.id == recommended.id,
            score = average,
            trust = trustScore,
            safety = safetyScore,
            insight = insightScore,
            riskLevel = currentCase.riskLevel,
            missedConcepts = BuildMissedConceptSummary(),
            selectedInterventions = string.Join(" | ", currentSelections.Select(s => s.choice).ToArray()),
            vnMode = currentVnScript != null,
            vnChoicePath = string.Join(" | ", currentSelections.Select(s => s.interventionType + ":" + s.choice).ToArray()),
            vnReactionSummary = string.Join(" | ", currentSelections.Where(s => !string.IsNullOrEmpty(s.familyReaction)).Select(s => s.reactionSpeakerId + ":" + s.familyReaction).ToArray()),
            turnMetricDeltas = string.Join(" | ", currentSelections.Select(s => "T" + s.turn + " trust " + Signed(s.trustDelta) + " safety " + Signed(s.safetyDelta) + " insight " + Signed(s.insightDelta)).ToArray()),
            createdAt = DateTime.Now.ToString("s", CultureInfo.InvariantCulture)
        });
        WriteSaveSlot(1);
    }

    private string BuildMissedConceptSummary()
    {
        TherapyTheory recommended = theories.First(t => t.id == currentCase.recommendedTheoryId);
        var misses = new List<string>();
        if (selectedTheory.id != recommended.id) misses.Add("추천 렌즈: " + recommended.name);
        foreach (string tag in currentCase.dynamicsTags)
        {
            if (!currentSelections.Any(s => s.choice.Contains(tag))) misses.Add(tag);
        }
        if (currentSelections.Any(s => s.quality < 50)) misses.Add("성급한 판정/비구조화 선택");
        return misses.Count == 0 ? "핵심 개념 대부분 포착" : string.Join(", ", misses.Distinct().ToArray());
    }

    private SupervisorProfile GetSupervisorForTheory(string theoryId)
    {
        return supervisors.FirstOrDefault(s => s.theoryId == theoryId) ?? supervisors.First();
    }

    private void ShowSupervision()
    {
        var last = logs.Last();
        TherapyTheory recommended = theories.First(t => t.id == currentCase.recommendedTheoryId);
        SupervisorProfile supervisor = GetSupervisorForTheory(recommended.id);
        ClearCanvas();
        var root = CreateScreenRoot("supervision");
        CreateHeader(root, "슈퍼비전 리포트", currentCase.id + " · " + currentCase.familyType);

        var columns = CreateHorizontal(root, "Supervision Columns");
        columns.padding = new RectOffset(30, 30, 22, 30);
        columns.spacing = 22;

        var report = CreateSkinnedCard(columns.transform, "Report", "VN/UI/session_result_sheet", Paper);
        SetLayout(report, 0, 1, 1120, -1);
        CreateText(report.transform, "점수 " + last.score + " / 100", 42, FontStyle.Bold, last.score >= 80 ? Good : last.score >= 60 ? Warn : Bad);
        CreateMetricRow(report.transform, "신뢰", trustScore.ToString(CultureInfo.InvariantCulture), "안전감", safetyScore.ToString(CultureInfo.InvariantCulture), "통찰", insightScore.ToString(CultureInfo.InvariantCulture));
        CreateSpacer(report.transform, 12);
        CreateText(report.transform, "추천 이론 렌즈: " + recommended.name, 30, FontStyle.Bold, Ink);
        CreateText(report.transform, recommended.problemView + "\n목표: " + recommended.goal, 23, FontStyle.Normal, Ink);
        CreateSpacer(report.transform, 10);
        CreateText(report.transform, supervisor.name + " 코멘트", 28, FontStyle.Bold, Ink);
        CreateText(report.transform, supervisor.openingLine + "\n복기 질문: " + currentCase.reflectionQuestion, 21, FontStyle.Normal, MutedInk);
        CreateSpacer(report.transform, 10);
        CreateText(report.transform, "선택 해설", 28, FontStyle.Bold, Ink);
        foreach (var selection in currentSelections)
        {
            CreateText(report.transform, "T" + selection.turn + " · " + selection.quality + "점 · " + selection.feedback, 21, FontStyle.Normal, MutedInk);
        }
        CreateSpacer(report.transform, 10);
        CreateText(report.transform, "놓친 개념: " + last.missedConcepts, 22, FontStyle.Bold, Warm);
        if (aiSupervisorEnabled)
        {
            CreateText(report.transform, "AI 슈퍼바이저 참고: 현재 빌드는 API 호출 없이 안전한 placeholder 코멘트를 표시합니다. 실제 연결 시에도 공식 점수에는 반영하지 않습니다.", 20, FontStyle.Normal, Bad);
        }

        var actions = CreateSkinnedCard(columns.transform, "Actions", "VN/UI/supervisor_note_panel", new Color32(34, 38, 47, 255));
        SetLayout(actions, 0, 1, 680, -1);
        CreateText(actions.transform, "다음 행동", 32, FontStyle.Bold, Color.white);
        CreateSkinnedButton(actions.transform, "다음 사례 접수", "VN/UI/choice_card_question", Accent, ShowCaseIntake);
        CreateSkinnedButton(actions.transform, "대시보드 보기", "VN/UI/choice_card_intervention", Warm, ShowDashboard);
        CreateSkinnedButton(actions.transform, "로그 Export", "VN/UI/choice_card_intervention", Good, () =>
        {
            ExportAll();
            ShowDashboard();
        });
        CreateSkinnedButton(actions.transform, "메인 메뉴", "VN/UI/choice_card_question", MutedInk, ShowMainMenu);
    }

    private void ShowDashboard()
    {
        ClearCanvas();
        var root = CreateScreenRoot("dashboard");
        CreateHeader(root, "사례·학습 로그 대시보드", "아동가족트렌드와 빅데이터 분석 과목용 evidence layer");

        var columns = CreateHorizontal(root, "Dashboard Columns");
        columns.padding = new RectOffset(30, 30, 22, 30);
        columns.spacing = 22;

        var left = CreateCard(columns.transform, "Learning Analytics", Paper);
        SetLayout(left, 0, 1, 980, -1);
        CreateText(left.transform, "학습 로그 분석", 34, FontStyle.Bold, Ink);
        if (logs.Count == 0)
        {
            CreateText(left.transform, "아직 플레이 로그가 없습니다. 사례를 1건 이상 진행하면 선택 패턴과 이론 적용 결과가 표시됩니다.", 24, FontStyle.Normal, Ink);
        }
        else
        {
            float avg = (float)logs.Average(l => l.score);
            float match = logs.Count(l => l.matchedRecommendedTheory) * 100f / logs.Count;
            CreateMetricRow(left.transform, "플레이 세션", logs.Count.ToString(CultureInfo.InvariantCulture), "평균 점수", avg.ToString("0.0", CultureInfo.InvariantCulture), "추천 렌즈 일치", match.ToString("0.0", CultureInfo.InvariantCulture) + "%");
            foreach (var group in logs.GroupBy(l => l.recommendedTheory).OrderByDescending(g => g.Count()))
            {
                CreateBar(left.transform, group.Key, group.Count(), logs.Count, Good);
            }
            CreateSpacer(left.transform, 10);
            CreateText(left.transform, "최근 세션", 28, FontStyle.Bold, Ink);
            foreach (var log in logs.Skip(Math.Max(0, logs.Count - 5)).Reverse())
            {
                CreateText(left.transform, log.caseId + " · " + log.selectedTheory + " · " + log.score + "점 · 놓친 개념: " + log.missedConcepts, 20, FontStyle.Normal, MutedInk);
            }
        }

        var right = CreateCard(columns.transform, "Case Analytics", new Color32(34, 38, 47, 255));
        SetLayout(right, 0, 1, 820, -1);
        CreateText(right.transform, "사례 데이터 분석", 34, FontStyle.Bold, Color.white);
        CreateMetricRow(right.transform, "총 사례", cases.Count.ToString(CultureInfo.InvariantCulture), "캠페인 장", "6", "이론", theories.Count.ToString(CultureInfo.InvariantCulture));
        foreach (var group in cases.GroupBy(c => theories.First(t => t.id == c.recommendedTheoryId).name).OrderBy(g => g.Key))
        {
            CreateBar(right.transform, group.Key, group.Count(), cases.Count, Accent);
        }
        CreateSpacer(right.transform, 12);
        CreateButton(right.transform, "CSV/JSON/HTML Export", Good, () =>
        {
            ExportAll();
            ShowDashboard();
        });
        CreateText(right.transform, "Export folder:\n" + exportFolder, 18, FontStyle.Normal, new Color32(228, 230, 234, 255));
        CreateButton(right.transform, "메인 메뉴", MutedInk, ShowMainMenu);
    }

    private void ExportAll()
    {
        Directory.CreateDirectory(exportFolder);
        File.WriteAllText(Path.Combine(exportFolder, "player_choice_log.csv"), BuildLogCsv(), new UTF8Encoding(false));
        File.WriteAllText(Path.Combine(exportFolder, "case_dataset.csv"), BuildCaseCsv(), new UTF8Encoding(false));
        File.WriteAllText(Path.Combine(exportFolder, "player_choice_log.json"), JsonUtility.ToJson(new PlayerChoiceLogCollection { logs = logs }, true), new UTF8Encoding(false));
        File.WriteAllText(Path.Combine(exportFolder, "case_dataset.json"), JsonUtility.ToJson(new FamilyCaseCollection { cases = cases }, true), new UTF8Encoding(false));
        File.WriteAllText(Path.Combine(exportFolder, "dashboard.html"), BuildDashboardHtml(), new UTF8Encoding(false));
    }

    private string BuildLogCsv()
    {
        var b = new StringBuilder();
        b.AppendLine("session_id,case_id,chapter,family_type,selected_theory,recommended_theory,matched,score,trust,safety,insight,risk_level,missed_concepts,selected_interventions,vn_mode,vn_choice_path,vn_reaction_summary,turn_metric_deltas,created_at");
        foreach (var log in logs)
        {
            b.AppendLine(string.Join(",", new[]
            {
                Csv(log.sessionId), Csv(log.caseId), log.chapter.ToString(CultureInfo.InvariantCulture), Csv(log.familyType), Csv(log.selectedTheory), Csv(log.recommendedTheory),
                log.matchedRecommendedTheory ? "true" : "false", log.score.ToString(CultureInfo.InvariantCulture), log.trust.ToString(CultureInfo.InvariantCulture),
                log.safety.ToString(CultureInfo.InvariantCulture), log.insight.ToString(CultureInfo.InvariantCulture), log.riskLevel.ToString(CultureInfo.InvariantCulture),
                Csv(log.missedConcepts), Csv(log.selectedInterventions), log.vnMode ? "true" : "false", Csv(log.vnChoicePath), Csv(log.vnReactionSummary), Csv(log.turnMetricDeltas), Csv(log.createdAt)
            }));
        }
        return b.ToString();
    }

    private string BuildCaseCsv()
    {
        var b = new StringBuilder();
        b.AppendLine("case_id,chapter,is_handcrafted,family_type,presenting_problem,recommended_theory,risk_level,dynamics_tags,recommended_intervention,hidden_pattern,family_map,learning_objective,supervisor_cue,reflection_question");
        foreach (var c in cases)
        {
            string theory = theories.First(t => t.id == c.recommendedTheoryId).name;
            b.AppendLine(string.Join(",", new[] { Csv(c.id), c.chapter.ToString(CultureInfo.InvariantCulture), c.isHandcrafted ? "true" : "false", Csv(c.familyType), Csv(c.presentingProblem), Csv(theory), c.riskLevel.ToString(CultureInfo.InvariantCulture), Csv(string.Join("|", c.dynamicsTags)), Csv(c.recommendedIntervention), Csv(c.hiddenPattern), Csv(c.familyMap), Csv(c.learningObjective), Csv(c.supervisorCue), Csv(c.reflectionQuestion) }));
        }
        return b.ToString();
    }

    private string BuildDashboardHtml()
    {
        var b = new StringBuilder();
        b.AppendLine("<!doctype html><html lang=\"ko\"><meta charset=\"utf-8\"><title>가족치료 수련센터 대시보드</title>");
        b.AppendLine("<style>body{font-family:system-ui,Malgun Gothic,sans-serif;background:#15181d;color:#f2eee4;margin:0;padding:32px}section{background:#22262f;border-radius:10px;padding:24px;margin:0 0 24px}h1,h2{margin-top:0}.bar{height:22px;background:#4a87a1;border-radius:4px;margin:6px 0 16px}.muted{color:#c9c4b8}table{width:100%;border-collapse:collapse}td,th{border-bottom:1px solid #3d4350;padding:8px;text-align:left}</style>");
        b.AppendLine("<h1>가족치료 수련센터 대시보드</h1>");
        b.AppendLine("<p class=\"muted\">합성 사례와 플레이어 선택 로그를 가족치료 이론 적용 데이터로 변환한 분석 산출물입니다.</p>");
        b.AppendLine("<section><h2>빌드 콘텐츠</h2><p>총 사례: " + cases.Count + " / 1장 수작업 에피소드형 사례: " + cases.Count(c => c.chapter == 1 && c.isHandcrafted) + " / 가족치료 이론: " + theories.Count + " / 슈퍼바이저: " + supervisors.Count + " / 회기 턴: " + SessionTurnCount + "</p></section>");
        b.AppendLine("<section><h2>학습 로그 요약</h2>");
        b.AppendLine("<p>총 플레이 세션: " + logs.Count + "</p>");
        if (logs.Count > 0)
        {
            b.AppendLine("<p>평균 점수: " + logs.Average(l => l.score).ToString("0.0", CultureInfo.InvariantCulture) + " / 추천 렌즈 일치율: " + (logs.Count(l => l.matchedRecommendedTheory) * 100f / logs.Count).ToString("0.0", CultureInfo.InvariantCulture) + "%</p>");
            foreach (var group in logs.GroupBy(l => l.recommendedTheory).OrderByDescending(g => g.Count()))
            {
                float width = Mathf.Max(4f, group.Count() * 100f / logs.Count);
                b.AppendLine("<div>" + Html(group.Key) + " (" + group.Count() + ")</div><div class=\"bar\" style=\"width:" + width.ToString("0.0", CultureInfo.InvariantCulture) + "%\"></div>");
            }
        }
        b.AppendLine("</section>");
        b.AppendLine("<section><h2>사례 데이터셋</h2><table><tr><th>이론</th><th>사례 수</th><th>평균 위험도</th></tr>");
        foreach (var group in cases.GroupBy(c => theories.First(t => t.id == c.recommendedTheoryId).name).OrderBy(g => g.Key))
        {
            b.AppendLine("<tr><td>" + Html(group.Key) + "</td><td>" + group.Count() + "</td><td>" + group.Average(c => c.riskLevel).ToString("0.0", CultureInfo.InvariantCulture) + "</td></tr>");
        }
        b.AppendLine("</table></section>");
        b.AppendLine("<section><h2>최근 선택 로그</h2><table><tr><th>사례</th><th>선택 이론</th><th>추천 이론</th><th>점수</th><th>놓친 개념</th></tr>");
        foreach (var log in logs.Skip(Math.Max(0, logs.Count - 20)).Reverse())
        {
            b.AppendLine("<tr><td>" + Html(log.caseId) + "</td><td>" + Html(log.selectedTheory) + "</td><td>" + Html(log.recommendedTheory) + "</td><td>" + log.score + "</td><td>" + Html(log.missedConcepts) + "</td></tr>");
        }
        b.AppendLine("</table></section></html>");
        return b.ToString();
    }

    private static string Csv(string value)
    {
        if (value == null) return "\"\"";
        return "\"" + value.Replace("\"", "\"\"") + "\"";
    }

    private static string Html(string value)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        return value.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;");
    }

    private static string JsonString(string value)
    {
        if (value == null) return "null";
        return "\"" + value.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "\\r") + "\"";
    }

    private static string JsonStringArray(IEnumerable<string> values)
    {
        return "[" + string.Join(",", values.Select(JsonString).ToArray()) + "]";
    }

    private static string Signed(int value)
    {
        return value >= 0 ? "+" + value.ToString(CultureInfo.InvariantCulture) : value.ToString(CultureInfo.InvariantCulture);
    }

    private List<string> GetMissingVnAssets()
    {
        return RequiredVnAssetPaths.Where(path => LoadVnTexture(path) == null).ToList();
    }

    private int CountStyleTestAssets()
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), StyleTestFolder.Replace("/", Path.DirectorySeparatorChar.ToString()));
        return Directory.Exists(path) ? Directory.GetFiles(path, "*.png", SearchOption.TopDirectoryOnly).Length : 0;
    }

    private int CountCommercialVnAssets()
    {
        string path = Path.Combine(Application.dataPath, "Resources", "VN");
        int fileCount = Directory.Exists(path) ? Directory.GetFiles(path, "*.png", SearchOption.AllDirectories).Length : 0;
        int runtimeResourceCount = Resources.LoadAll<Texture2D>("VN").Length;
        int runtimeAvailable = RequiredVnAssetPaths.Length - GetMissingVnAssets().Count;
        return Mathf.Max(fileCount, runtimeResourceCount, runtimeAvailable);
    }

    private void ClearCanvas()
    {
        if (canvas == null) return;
        for (int i = canvas.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(canvas.transform.GetChild(i).gameObject);
        }
    }

    private GameObject CreateVnRoot(string name, string backgroundPath)
    {
        ClearCanvas();
        var wrapper = new GameObject(name + "-wrapper");
        wrapper.transform.SetParent(canvas.transform, false);
        var wrapperRect = wrapper.AddComponent<RectTransform>();
        wrapperRect.anchorMin = Vector2.zero;
        wrapperRect.anchorMax = Vector2.one;
        wrapperRect.offsetMin = Vector2.zero;
        wrapperRect.offsetMax = Vector2.zero;
        var wrapperImage = wrapper.AddComponent<Image>();
        wrapperImage.color = new Color32(12, 14, 18, 255);

        var root = new GameObject(name);
        root.transform.SetParent(wrapper.transform, false);
        var rect = root.AddComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.sizeDelta = new Vector2(1920, 1080);
        rect.anchoredPosition = Vector2.zero;
        var fitter = root.AddComponent<AspectRatioFitter>();
        fitter.aspectMode = AspectRatioFitter.AspectMode.FitInParent;
        fitter.aspectRatio = 16f / 9f;
        var image = root.AddComponent<Image>();
        image.color = new Color32(16, 18, 22, 255);

        Texture2D backgroundTexture = LoadVnTexture(backgroundPath);
        if (backgroundTexture != null)
        {
            var bg = new GameObject("VN Background");
            bg.transform.SetParent(root.transform, false);
            var bgRect = bg.AddComponent<RectTransform>();
            bgRect.anchorMin = Vector2.zero;
            bgRect.anchorMax = Vector2.one;
            bgRect.offsetMin = Vector2.zero;
            bgRect.offsetMax = Vector2.zero;
            var raw = bg.AddComponent<RawImage>();
            raw.texture = backgroundTexture;
            raw.color = Color.white;
            var bgFitter = bg.AddComponent<AspectRatioFitter>();
            bgFitter.aspectMode = AspectRatioFitter.AspectMode.EnvelopeParent;
            bgFitter.aspectRatio = backgroundTexture.width / Mathf.Max(1f, backgroundTexture.height);
        }
        else
        {
            var fallback = CreateAbsolutePanel(root.transform, "Missing Background", new Color32(31, 37, 45, 255), Vector2.zero, Vector2.one, 0, 0, 0, 0);
            var label = CreateText(fallback.transform, "Missing background\n" + backgroundPath, 24, FontStyle.Bold, new Color32(214, 218, 224, 255));
            label.alignment = TextAnchor.MiddleCenter;
            Stretch(label.gameObject, 0, 0, 0, 0);
        }

        var shade = CreateAbsolutePanel(root.transform, "VN Shade", new Color32(0, 0, 0, 72), Vector2.zero, Vector2.one, 0, 0, 0, 0);
        shade.transform.SetAsLastSibling();
        return root;
    }

    private void CreateVnHud(Transform parent, string caseId, string title, string progress)
    {
        var hud = CreateAbsoluteSkinnedPanel(parent, "VN HUD", "VN/UI/metrics_hud", new Color32(23, 27, 34, 224), new Vector2(0.025f, 0.9f), new Vector2(0.975f, 0.975f), 0, 0, 0, 0);
        var layout = hud.AddComponent<HorizontalLayoutGroup>();
        layout.padding = new RectOffset(24, 24, 10, 10);
        layout.spacing = 14;
        layout.childControlWidth = true;
        layout.childControlHeight = true;
        layout.childForceExpandWidth = false;
        layout.childForceExpandHeight = true;
        var left = CreateText(hud.transform, caseId + " · " + title, 23, FontStyle.Bold, Color.white);
        left.alignment = TextAnchor.MiddleLeft;
        SetLayout(left.gameObject, 1, 1, -1, -1);
        var right = CreateText(hud.transform, progress + "  |  신뢰 " + trustScore + " · 안전 " + safetyScore + " · 통찰 " + insightScore, 18, FontStyle.Bold, new Color32(238, 232, 218, 255));
        right.alignment = TextAnchor.MiddleRight;
        SetLayout(right.gameObject, 0, 1, 720, -1);
    }

    private void CreateVnStage(Transform parent, string activeSpeakerId, string expressionId)
    {
        var ids = GetVisibleVnCharacterIds(activeSpeakerId);
        CreateVnCastStrip(parent, ids);
        float[] centers = ids.Count == 1 ? new[] { 0.5f } : ids.Count == 2 ? new[] { 0.34f, 0.66f } : new[] { 0.24f, 0.5f, 0.76f };
        for (int i = 0; i < ids.Count; i++)
        {
            string id = ids[i];
            VnCharacterProfile profile = GetVnCharacter(id);
            if (profile == null) continue;
            bool active = id == activeSpeakerId;
            string expression = active ? expressionId : profile.defaultExpression;
            float width = active ? 0.27f : 0.23f;
            float center = centers[Mathf.Min(i, centers.Length - 1)];
            var holder = CreateAbsolutePanel(parent, "Character " + id, new Color32(0, 0, 0, 0), new Vector2(center - width / 2f, 0.14f), new Vector2(center + width / 2f, 0.88f), 0, 0, 0, 0);
            Texture2D texture = LoadVnTexture(profile.baseAssetPath + "_" + expression) ?? LoadVnTexture(profile.baseAssetPath + "_" + profile.defaultExpression);
            if (texture != null)
            {
                var holderImage = holder.GetComponent<Image>();
                holderImage.color = new Color32(0, 0, 0, 0);
                holderImage.raycastTarget = false;
                var spriteObject = new GameObject("Character Image");
                spriteObject.transform.SetParent(holder.transform, false);
                var spriteRect = spriteObject.AddComponent<RectTransform>();
                spriteRect.anchorMin = Vector2.zero;
                spriteRect.anchorMax = Vector2.one;
                spriteRect.offsetMin = Vector2.zero;
                spriteRect.offsetMax = Vector2.zero;
                var raw = spriteObject.AddComponent<RawImage>();
                raw.texture = texture;
                raw.color = active ? Color.white : new Color32(190, 194, 200, 210);
                raw.raycastTarget = false;
                var fitter = spriteObject.AddComponent<AspectRatioFitter>();
                fitter.aspectMode = AspectRatioFitter.AspectMode.FitInParent;
                fitter.aspectRatio = texture.width / Mathf.Max(1f, texture.height);
            }
            else
            {
                var fallback = holder.GetComponent<Image>();
                fallback.color = active ? new Color32(74, 135, 161, 210) : new Color32(54, 60, 70, 180);
                CreateCharacterFallbackCard(holder.transform, profile, active);
            }
            CreateCharacterIdentityBadge(holder.transform, profile, active);
        }
    }

    private void CreateVnCastStrip(Transform parent, List<string> visibleIds)
    {
        if (visibleIds == null || visibleIds.Count == 0) return;
        string cast = string.Join("\n", visibleIds
            .Select(GetVnCharacter)
            .Where(profile => profile != null)
            .Select(FormatCharacterNameForRoster)
            .ToArray());
        if (string.IsNullOrEmpty(cast)) return;

        var strip = CreateAbsolutePanel(parent, "VN Cast Strip", new Color32(15, 18, 24, 218), new Vector2(0.035f, 0.50f), new Vector2(0.19f, 0.66f), 0, 0, 0, 0);
        var layout = strip.AddComponent<VerticalLayoutGroup>();
        layout.padding = new RectOffset(10, 10, 8, 8);
        layout.spacing = 3;
        layout.childControlWidth = true;
        layout.childControlHeight = true;
        layout.childForceExpandWidth = true;
        layout.childForceExpandHeight = false;
        var title = CreateText(strip.transform, "등장인물", 16, FontStyle.Bold, new Color32(255, 241, 212, 255));
        title.alignment = TextAnchor.MiddleLeft;
        var label = CreateText(strip.transform, cast, 14, FontStyle.Bold, new Color32(238, 232, 218, 255));
        label.alignment = TextAnchor.UpperLeft;
    }

    private void CreateCharacterFallbackCard(Transform parent, VnCharacterProfile profile, bool active)
    {
        var layoutRoot = CreateAbsolutePanel(parent, "Character Role Card", new Color32(0, 0, 0, 0), new Vector2(0.08f, 0.39f), new Vector2(0.92f, 0.75f), 0, 0, 0, 0);
        var layout = layoutRoot.AddComponent<VerticalLayoutGroup>();
        layout.padding = new RectOffset(14, 14, 16, 16);
        layout.spacing = 6;
        layout.childControlWidth = true;
        layout.childControlHeight = true;
        layout.childForceExpandWidth = true;
        layout.childForceExpandHeight = false;

        var relation = CreateText(layoutRoot.transform, GetShortRelationLabel(profile), active ? 27 : 23, FontStyle.Bold, Color.white);
        relation.alignment = TextAnchor.MiddleCenter;
        var name = CreateText(layoutRoot.transform, profile.displayName, active ? 34 : 28, FontStyle.Bold, Color.white);
        name.alignment = TextAnchor.MiddleCenter;
        var role = CreateText(layoutRoot.transform, CompactCharacterRole(profile), active ? 17 : 15, FontStyle.Normal, new Color32(226, 232, 236, 255));
        role.alignment = TextAnchor.MiddleCenter;
    }

    private void CreateCharacterIdentityBadge(Transform parent, VnCharacterProfile profile, bool active)
    {
        var badge = CreateAbsolutePanel(parent, "Character Identity", active ? new Color32(15, 18, 24, 232) : new Color32(15, 18, 24, 188), new Vector2(0.02f, 0.24f), new Vector2(0.98f, 0.38f), 0, 0, 0, 0);
        var layout = badge.AddComponent<VerticalLayoutGroup>();
        layout.padding = new RectOffset(10, 10, 5, 5);
        layout.spacing = 1;
        layout.childControlWidth = true;
        layout.childControlHeight = true;
        layout.childForceExpandWidth = true;
        layout.childForceExpandHeight = false;

        var identity = CreateText(badge.transform, FormatCharacterNameWithRelation(profile), active ? 18 : 16, FontStyle.Bold, Color.white);
        identity.alignment = TextAnchor.MiddleCenter;
        if (active)
        {
            var detail = CreateText(badge.transform, CompactCharacterRole(profile), 13, FontStyle.Normal, new Color32(216, 225, 229, 255));
            detail.alignment = TextAnchor.MiddleCenter;
        }
    }

    private List<string> GetVisibleVnCharacterIds(string activeSpeakerId)
    {
        var ids = new List<string>();
        if (currentVnScript != null && currentVnScript.characters != null)
        {
            string[] anchors;
            if (currentVnScript.characters.Contains("ft001_mother"))
            {
                anchors = new[] { "ft001_mother", "ft001_child" };
            }
            else if (currentVnScript.characters.Any(id => id.StartsWith("generic_", StringComparison.OrdinalIgnoreCase)))
            {
                anchors = new[] { "generic_guardian", "generic_child" };
            }
            else
            {
                anchors = currentVnScript.characters
                    .Where(id => !id.StartsWith("supervisor_", StringComparison.OrdinalIgnoreCase))
                    .Take(2)
                    .ToArray();
            }
            foreach (string id in anchors)
            {
                if (currentVnScript.characters.Contains(id)) ids.Add(id);
            }
        }
        if (!string.IsNullOrEmpty(activeSpeakerId) && !ids.Contains(activeSpeakerId)) ids.Add(activeSpeakerId);
        if (ids.Count == 0 && currentVnScript != null && currentVnScript.characters != null) ids.AddRange(currentVnScript.characters.Take(3));
        return ids.Take(3).ToList();
    }

    private void CreateVnSupervisorNote(Transform parent, string note)
    {
        var panel = CreateAbsoluteSkinnedPanel(parent, "Supervisor Note", "VN/UI/supervisor_note_panel", new Color32(34, 38, 47, 218), new Vector2(0.035f, 0.69f), new Vector2(0.19f, 0.86f), 0, 0, 0, 0);
        var layout = panel.AddComponent<VerticalLayoutGroup>();
        layout.padding = new RectOffset(10, 10, 9, 9);
        layout.spacing = 4;
        CreateText(panel.transform, "슈퍼바이저 노트", 16, FontStyle.Bold, new Color32(255, 241, 212, 255));
        CreateText(panel.transform, note, 13, FontStyle.Normal, new Color32(230, 232, 236, 255));
    }

    private void CreateVnDialogueBox(Transform parent, string speakerId, string content, int index, int total, Action next)
    {
        VnCharacterProfile speaker = GetVnCharacter(speakerId);
        string speakerName = speaker != null ? FormatCharacterNameWithRelation(speaker) : "상담실";
        var box = CreateAbsoluteSkinnedPanel(parent, "Dialogue Box", "VN/UI/dialogue_box", new Color32(238, 232, 218, 248), new Vector2(0.035f, 0.035f), new Vector2(0.965f, 0.29f), 0, 0, 0, 0);
        var layout = box.AddComponent<VerticalLayoutGroup>();
        layout.padding = new RectOffset(24, 24, 18, 18);
        layout.spacing = 8;
        layout.childControlWidth = true;
        layout.childControlHeight = true;
        layout.childForceExpandWidth = true;
        layout.childForceExpandHeight = false;
        var nameplate = CreateSkinnedInlinePanel(box.transform, "Speaker Nameplate", "VN/UI/speaker_nameplate", new Color32(218, 231, 231, 255), 1, 0, -1, 38);
        var nameText = CreateText(nameplate.transform, speakerName + "  " + index + "/" + total, 21, FontStyle.Bold, Accent);
        nameText.alignment = TextAnchor.MiddleLeft;
        Stretch(nameText.gameObject, 16, 0, 16, 0);
        CreateText(box.transform, content, 27, FontStyle.Bold, Ink);
        CreateSkinnedButton(box.transform, index >= total ? "개입 선택 / 계속" : "다음", "VN/UI/choice_card_question", Accent, next);
    }

    private static string FormatCharacterNameWithRelation(VnCharacterProfile profile)
    {
        if (profile == null) return "상담실";
        string relation = GetShortRelationLabel(profile);
        return string.IsNullOrEmpty(relation) ? profile.displayName : profile.displayName + " · " + relation;
    }

    private static string FormatCharacterNameForRoster(VnCharacterProfile profile)
    {
        if (profile == null) return "";
        string relation = GetShortRelationLabel(profile);
        return string.IsNullOrEmpty(relation) ? profile.displayName : profile.displayName + "(" + relation + ")";
    }

    private static string GetShortRelationLabel(VnCharacterProfile profile)
    {
        if (profile == null || string.IsNullOrEmpty(profile.id)) return "";
        string id = profile.id.ToLowerInvariant();
        if (id == "ft001_mother") return "어머니";
        if (id == "ft001_child") return "자녀";
        if (id == "ft001_grandmother") return "외조모";
        if (id == "ft001_teacher") return "담임";
        if (id.StartsWith("supervisor_", StringComparison.OrdinalIgnoreCase))
        {
            string role = profile.role ?? "";
            int marker = role.IndexOf(" 슈퍼바이저", StringComparison.Ordinal);
            return marker > 0 ? role.Substring(0, marker) + " 슈퍼바이저" : "슈퍼바이저";
        }

        string[] checks =
        {
            "grandmother:조모/외조모",
            "grandfather:조부/외조부",
            "stepmother:새어머니",
            "stepfather:새아버지",
            "biological_father:친부",
            "mother:어머니",
            "father:아버지",
            "daughter:딸",
            "son:아들",
            "child:자녀",
            "teen:청소년",
            "teacher:교사",
            "counselor:상담자",
            "worker:기관 담당자",
            "manager:기관 담당자",
            "mediator:중재자",
            "nurse:간호사",
            "visitor:방문 지원자",
            "interpreter:통역 상담자"
        };
        foreach (string check in checks)
        {
            string[] parts = check.Split(':');
            if (id.Contains(parts[0])) return parts[1];
        }
        return profile.role;
    }

    private static string CompactCharacterRole(VnCharacterProfile profile)
    {
        if (profile == null || string.IsNullOrEmpty(profile.role)) return "";
        string role = profile.role;
        if (role.Length <= 24) return role;
        return role.Substring(0, 24) + "...";
    }

    private GameObject CreateAbsolutePanel(Transform parent, string name, Color color, Vector2 anchorMin, Vector2 anchorMax, float left, float bottom, float right, float top)
    {
        var panel = new GameObject(name);
        panel.transform.SetParent(parent, false);
        var rect = panel.AddComponent<RectTransform>();
        rect.anchorMin = anchorMin;
        rect.anchorMax = anchorMax;
        rect.offsetMin = new Vector2(left, bottom);
        rect.offsetMax = new Vector2(-right, -top);
        var image = panel.AddComponent<Image>();
        image.color = color;
        return panel;
    }

    private GameObject CreateAbsoluteSkinnedPanel(Transform parent, string name, string resourcePath, Color fallbackColor, Vector2 anchorMin, Vector2 anchorMax, float left, float bottom, float right, float top)
    {
        var panel = new GameObject(name);
        panel.transform.SetParent(parent, false);
        var rect = panel.AddComponent<RectTransform>();
        rect.anchorMin = anchorMin;
        rect.anchorMax = anchorMax;
        rect.offsetMin = new Vector2(left, bottom);
        rect.offsetMax = new Vector2(-right, -top);
        Texture2D texture = LoadVnTexture(resourcePath);
        if (UseDecorativeUiSkins && texture != null)
        {
            var raw = panel.AddComponent<RawImage>();
            raw.texture = texture;
            raw.color = Color.white;
        }
        else
        {
            var image = panel.AddComponent<Image>();
            image.color = fallbackColor;
        }
        return panel;
    }

    private GameObject CreateSkinnedInlinePanel(Transform parent, string name, string resourcePath, Color fallbackColor, float flexibleWidth, float flexibleHeight, float preferredWidth, float preferredHeight)
    {
        var panel = new GameObject(name);
        panel.transform.SetParent(parent, false);
        Texture2D texture = LoadVnTexture(resourcePath);
        if (UseDecorativeUiSkins && texture != null)
        {
            var raw = panel.AddComponent<RawImage>();
            raw.texture = texture;
            raw.color = Color.white;
        }
        else
        {
            var image = panel.AddComponent<Image>();
            image.color = fallbackColor;
        }
        SetLayout(panel, flexibleWidth, flexibleHeight, preferredWidth, preferredHeight);
        return panel;
    }

    private static void Stretch(GameObject obj, float left, float bottom, float right, float top)
    {
        var rect = obj.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = new Vector2(left, bottom);
        rect.offsetMax = new Vector2(-right, -top);
    }

    private static Texture2D LoadVnTexture(string resourcePath)
    {
        if (string.IsNullOrEmpty(resourcePath)) return null;
        Texture2D texture = Resources.Load<Texture2D>(resourcePath);
        if (texture != null) return texture;

        texture = Resources.Load<Texture2D>(resourcePath + "_phase1");
        if (texture != null) return texture;

        texture = Resources.Load<Texture2D>(resourcePath + "_phase0");
        if (texture != null) return texture;

        if (resourcePath == "VN/Backgrounds/counseling_room_day")
        {
            return Resources.Load<Texture2D>("VN/Backgrounds/phase0_counseling_room_day_v2");
        }
        if (resourcePath == "VN/Characters/FT001/ft001_mother_neutral")
        {
            return Resources.Load<Texture2D>("VN/Characters/FT001/ft001_mother_neutral_phase0");
        }
        return null;
    }

    private GameObject CreateScreenRoot(string name)
    {
        var root = new GameObject(name);
        root.transform.SetParent(canvas.transform, false);
        var rect = root.AddComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
        var image = root.AddComponent<Image>();
        image.color = Background;
        var layout = root.AddComponent<VerticalLayoutGroup>();
        layout.childControlWidth = true;
        layout.childControlHeight = true;
        layout.childForceExpandWidth = true;
        layout.childForceExpandHeight = false;
        return root;
    }

    private void CreateHeader(GameObject root, string title, string subtitle)
    {
        var header = new GameObject("Header");
        header.transform.SetParent(root.transform, false);
        var image = header.AddComponent<Image>();
        image.color = new Color32(27, 31, 38, 255);
        var layout = header.AddComponent<VerticalLayoutGroup>();
        layout.padding = new RectOffset(34, 34, 20, 16);
        layout.spacing = 4;
        SetLayout(header, 1, 0, -1, 124);
        CreateText(header.transform, title, 42, FontStyle.Bold, Color.white);
        CreateText(header.transform, subtitle, 21, FontStyle.Normal, new Color32(210, 214, 220, 255));
    }

    private HorizontalLayoutGroup CreateHorizontal(GameObject root, string name)
    {
        var group = new GameObject(name);
        group.transform.SetParent(root.transform, false);
        SetLayout(group, 1, 1, -1, -1);
        var layout = group.AddComponent<HorizontalLayoutGroup>();
        layout.childControlWidth = true;
        layout.childControlHeight = true;
        layout.childForceExpandWidth = false;
        layout.childForceExpandHeight = true;
        return layout;
    }

    private GameObject CreateCard(Transform parent, string name, Color color)
    {
        var card = new GameObject(name);
        card.transform.SetParent(parent, false);
        var image = card.AddComponent<Image>();
        image.color = color;
        var layout = card.AddComponent<VerticalLayoutGroup>();
        layout.padding = new RectOffset(24, 24, 22, 22);
        layout.spacing = 10;
        layout.childControlWidth = true;
        layout.childControlHeight = true;
        layout.childForceExpandWidth = true;
        layout.childForceExpandHeight = false;
        var fitter = card.AddComponent<ContentSizeFitter>();
        fitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
        return card;
    }

    private GameObject CreateSkinnedCard(Transform parent, string name, string resourcePath, Color fallbackColor)
    {
        var card = new GameObject(name);
        card.transform.SetParent(parent, false);
        Texture2D texture = LoadVnTexture(resourcePath);
        if (UseDecorativeUiSkins && texture != null)
        {
            var raw = card.AddComponent<RawImage>();
            raw.texture = texture;
            raw.color = Color.white;
        }
        else
        {
            var image = card.AddComponent<Image>();
            image.color = fallbackColor;
        }
        var layout = card.AddComponent<VerticalLayoutGroup>();
        layout.padding = new RectOffset(24, 24, 22, 22);
        layout.spacing = 10;
        layout.childControlWidth = true;
        layout.childControlHeight = true;
        layout.childForceExpandWidth = true;
        layout.childForceExpandHeight = false;
        var fitter = card.AddComponent<ContentSizeFitter>();
        fitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
        return card;
    }

    private Text CreateText(Transform parent, string content, int size, FontStyle style, Color color)
    {
        var textObject = new GameObject("Text");
        textObject.transform.SetParent(parent, false);
        var text = textObject.AddComponent<Text>();
        text.text = content;
        text.font = appFont != null ? appFont : Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        text.fontSize = size;
        text.fontStyle = appFont != null ? FontStyle.Normal : style;
        text.color = color;
        text.alignment = TextAnchor.UpperLeft;
        text.horizontalOverflow = HorizontalWrapMode.Wrap;
        text.verticalOverflow = VerticalWrapMode.Overflow;
        SetLayout(textObject, 1, 0, -1, -1);
        return text;
    }

    private void CreateButton(Transform parent, string label, Color color, Action action)
    {
        var buttonObject = new GameObject("Button");
        buttonObject.transform.SetParent(parent, false);
        var image = buttonObject.AddComponent<Image>();
        image.color = color;
        var button = buttonObject.AddComponent<Button>();
        button.onClick.AddListener(() => action());
        SetLayout(buttonObject, 1, 0, -1, 56);
        var text = CreateText(buttonObject.transform, label, 21, FontStyle.Bold, Color.white);
        text.alignment = TextAnchor.MiddleCenter;
        var rect = text.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = new Vector2(12, 4);
        rect.offsetMax = new Vector2(-12, -4);
    }

    private void CreateSkinnedButton(Transform parent, string label, string resourcePath, Color fallbackColor, Action action)
    {
        var buttonObject = new GameObject("Button");
        buttonObject.transform.SetParent(parent, false);
        Graphic targetGraphic;
        Texture2D texture = LoadVnTexture(resourcePath);
        if (UseDecorativeUiSkins && texture != null)
        {
            var raw = buttonObject.AddComponent<RawImage>();
            raw.texture = texture;
            raw.color = Color.white;
            targetGraphic = raw;
        }
        else
        {
            var image = buttonObject.AddComponent<Image>();
            image.color = fallbackColor;
            targetGraphic = image;
        }
        var button = buttonObject.AddComponent<Button>();
        button.targetGraphic = targetGraphic;
        button.onClick.AddListener(() => action());
        SetLayout(buttonObject, 1, 0, -1, 62);
        Color textColor = UseDecorativeUiSkins && texture != null ? Ink : Color.white;
        var text = CreateText(buttonObject.transform, label, 21, FontStyle.Bold, textColor);
        text.alignment = TextAnchor.MiddleCenter;
        text.gameObject.AddComponent<Shadow>().effectColor = UseDecorativeUiSkins && texture != null ? new Color32(255, 255, 255, 120) : new Color32(0, 0, 0, 180);
        var rect = text.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = new Vector2(14, 5);
        rect.offsetMax = new Vector2(-14, -5);
    }

    private void CreateMetricRow(Transform parent, string label1, string value1, string label2, string value2, string label3, string value3)
    {
        var row = new GameObject("Metric Row");
        row.transform.SetParent(parent, false);
        SetLayout(row, 1, 0, -1, 86);
        var layout = row.AddComponent<HorizontalLayoutGroup>();
        layout.spacing = 10;
        layout.childControlWidth = true;
        layout.childControlHeight = true;
        layout.childForceExpandWidth = true;
        layout.childForceExpandHeight = true;
        CreateMetric(row.transform, label1, value1);
        CreateMetric(row.transform, label2, value2);
        CreateMetric(row.transform, label3, value3);
    }

    private void CreateMetric(Transform parent, string label, string value)
    {
        var card = CreateCard(parent, "Metric", new Color32(245, 240, 227, 255));
        SetLayout(card, 1, 1, -1, -1);
        CreateText(card.transform, label, 16, FontStyle.Normal, MutedInk);
        CreateText(card.transform, value, 22, FontStyle.Bold, Ink);
    }

    private void CreateBar(Transform parent, string label, int count, int total, Color color)
    {
        CreateText(parent, label + " · " + count, 18, FontStyle.Bold, parent.GetComponent<Image>() != null && parent.GetComponent<Image>().color == Paper ? Ink : Color.white);
        var holder = new GameObject("Bar Holder");
        holder.transform.SetParent(parent, false);
        var holderImage = holder.AddComponent<Image>();
        holderImage.color = new Color32(70, 73, 82, 255);
        SetLayout(holder, 1, 0, -1, 20);
        var bar = new GameObject("Bar");
        bar.transform.SetParent(holder.transform, false);
        var barImage = bar.AddComponent<Image>();
        barImage.color = color;
        var rect = bar.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0, 0);
        rect.anchorMax = new Vector2(Mathf.Clamp01(count / Mathf.Max(1f, total)), 1);
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
    }

    private void CreateSpacer(Transform parent, int height)
    {
        var spacer = new GameObject("Spacer");
        spacer.transform.SetParent(parent, false);
        SetLayout(spacer, 1, 0, -1, height);
    }

    private static void SetLayout(GameObject obj, float flexibleWidth, float flexibleHeight, float preferredWidth, float preferredHeight)
    {
        var layout = obj.GetComponent<LayoutElement>();
        if (layout == null) layout = obj.AddComponent<LayoutElement>();
        layout.flexibleWidth = flexibleWidth;
        layout.flexibleHeight = flexibleHeight;
        if (preferredWidth >= 0) layout.preferredWidth = preferredWidth;
        if (preferredHeight >= 0) layout.preferredHeight = preferredHeight;
    }

    [Serializable]
    private sealed class TherapyTheory
    {
        public string id;
        public string name;
        public string focus;
        public string problemView;
        public string goal;
        public string[] methods;

        public TherapyTheory(string id, string name, string focus, string problemView, string goal, string[] methods)
        {
            this.id = id;
            this.name = name;
            this.focus = focus;
            this.problemView = problemView;
            this.goal = goal;
            this.methods = methods;
        }
    }

    [Serializable]
    private sealed class FamilyCase
    {
        public string id;
        public int chapter;
        public string title;
        public string familyType;
        public string presentingProblem;
        public string context;
        public string hiddenPattern;
        public string recommendedTheoryId;
        public string recommendedIntervention;
        public string[] dynamicsTags;
        public int riskLevel;
        public string familyMap;
        public string learningObjective;
        public string supervisorCue;
        public string reflectionQuestion;
        public bool isHandcrafted;
        public string[] familyDialogue;
    }

    [Serializable]
    private sealed class SupervisorProfile
    {
        public string theoryId;
        public string name;
        public string specialty;
        public string openingLine;

        public SupervisorProfile(string theoryId, string name, string specialty, string openingLine)
        {
            this.theoryId = theoryId;
            this.name = name;
            this.specialty = specialty;
            this.openingLine = openingLine;
        }
    }

    [Serializable]
    private sealed class SessionChoice
    {
        public string label;
        public string theoryId;
        public int quality;
        public string feedback;
        public string interventionType;
        public string familyReaction;
        public string reactionSpeakerId;
        public string reactionExpressionId;

        public SessionChoice(string label, string theoryId, int quality, string feedback, string interventionType = "training", string familyReaction = "", string reactionSpeakerId = "", string reactionExpressionId = "neutral")
        {
            this.label = label;
            this.theoryId = theoryId;
            this.quality = quality;
            this.feedback = feedback;
            this.interventionType = interventionType;
            this.familyReaction = familyReaction;
            this.reactionSpeakerId = reactionSpeakerId;
            this.reactionExpressionId = reactionExpressionId;
        }
    }

    [Serializable]
    private sealed class SessionSelection
    {
        public int turn;
        public string choice;
        public string theoryId;
        public int quality;
        public string feedback;
        public string interventionType;
        public string familyReaction;
        public string reactionSpeakerId;
        public string reactionExpressionId;
        public int trustDelta;
        public int safetyDelta;
        public int insightDelta;
    }

    [Serializable]
    private sealed class PlayerChoiceLog
    {
        public string sessionId;
        public string caseId;
        public int chapter;
        public string familyType;
        public string selectedTheory;
        public string recommendedTheory;
        public bool matchedRecommendedTheory;
        public int score;
        public int trust;
        public int safety;
        public int insight;
        public int riskLevel;
        public string missedConcepts;
        public string selectedInterventions;
        public bool vnMode;
        public string vnChoicePath;
        public string vnReactionSummary;
        public string turnMetricDeltas;
        public string createdAt;
    }

    [Serializable]
    private sealed class VnCharacterProfile
    {
        public string id;
        public string displayName;
        public string role;
        public string baseAssetPath;
        public string defaultExpression;
        public string defaultPosition;
    }

    [Serializable]
    private sealed class VnDialogueLine
    {
        public string speakerId;
        public string expressionId;
        public string position;
        public string text;
        public string supervisorNote;

        public VnDialogueLine(string speakerId, string expressionId, string position, string text, string supervisorNote = "")
        {
            this.speakerId = speakerId;
            this.expressionId = expressionId;
            this.position = position;
            this.text = text;
            this.supervisorNote = supervisorNote;
        }
    }

    [Serializable]
    private sealed class VnChoice
    {
        public string label;
        public string theoryId;
        public string interventionType;
        public int quality;
        public string feedback;
        public string familyReaction;
        public string reactionSpeakerId;
        public string reactionExpressionId;

        public VnChoice(string label, string theoryId, string interventionType, int quality, string feedback, string familyReaction, string reactionSpeakerId, string reactionExpressionId)
        {
            this.label = label;
            this.theoryId = theoryId;
            this.interventionType = interventionType;
            this.quality = quality;
            this.feedback = feedback;
            this.familyReaction = familyReaction;
            this.reactionSpeakerId = reactionSpeakerId;
            this.reactionExpressionId = reactionExpressionId;
        }
    }

    [Serializable]
    private sealed class VnTurn
    {
        public string title;
        public List<VnDialogueLine> setupLines;
        public List<VnChoice> choices;

        public VnTurn(string title, IEnumerable<VnDialogueLine> setupLines, IEnumerable<VnChoice> choices)
        {
            this.title = title;
            this.setupLines = setupLines.ToList();
            this.choices = choices.ToList();
        }
    }

    [Serializable]
    private sealed class VnCaseScript
    {
        public string caseId;
        public int chapter;
        public string backgroundId;
        public string[] characters;
        public List<VnTurn> turns;

        public static VnCaseScript CreatePlaceholder(string caseId, int chapter)
        {
            return new VnCaseScript
            {
                caseId = caseId,
                chapter = chapter,
                backgroundId = "VN/Backgrounds/counseling_room_day",
                characters = new[] { "supervisor_system" },
                turns = new List<VnTurn>()
            };
        }
    }

    [Serializable]
    private sealed class PlayerChoiceLogCollection
    {
        public List<PlayerChoiceLog> logs;
    }

    [Serializable]
    private sealed class GameSaveData
    {
        public string savedAt;
        public string lastCaseId;
        public bool aiSupervisorEnabled;
        public List<PlayerChoiceLog> logs;
    }

    [Serializable]
    private sealed class FamilyCaseCollection
    {
        public List<FamilyCase> cases;
    }
}
