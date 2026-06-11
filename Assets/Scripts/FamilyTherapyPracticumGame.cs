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
    private readonly List<VnDialogueLine> currentVnIntroLines = new List<VnDialogueLine>();
    private int currentVnIntroLineIndex;
    private int sessionScore;
    private int trustScore;
    private int safetyScore;
    private int insightScore;
    private int caseBrowserPage;
    private string exportFolder;
    private bool aiSupervisorEnabled;
    private bool suppressVnCharacterSprites;

    private const int SessionTurnCount = 5;
    private const int CasesPerBrowserPage = 5;
    private const int CommercialAssetTarget = 750;
    private const int DefaultWindowWidth = 1600;
    private const int DefaultWindowHeight = 900;
    private const string StyleTestFolder = "Assets/ConceptArt/StyleTest_2026-06-08";
    private const bool UseDecorativeUiSkins = false;

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
        "VN/Characters/FT001/ft001_mother_listening",
        "VN/Characters/FT001/ft001_mother_softened",
        "VN/Characters/FT001/ft001_mother_tearful",
        "VN/Characters/FT001/ft001_mother_worried",
        "VN/Characters/FT001/ft001_child_anxious",
        "VN/Characters/FT001/ft001_child_hesitant",
        "VN/Characters/FT001/ft001_child_listening",
        "VN/Characters/FT001/ft001_child_quiet",
        "VN/Characters/FT001/ft001_child_relieved",
        "VN/Characters/FT001/ft001_child_scared",
        "VN/Characters/FT001/ft001_child_withdrawn",
        "VN/Characters/FT001/ft001_grandmother_critical",
        "VN/Characters/FT001/ft001_grandmother_defensive",
        "VN/Characters/FT001/ft001_grandmother_softened",
        "VN/Characters/FT001/ft001_grandmother_stubborn",
        "VN/Characters/FT001/ft001_grandmother_worried",
        "VN/Characters/FT001/ft001_teacher_concerned",
        "VN/Characters/FT001/ft001_teacher_procedural",
        "VN/Characters/FT001/ft001_teacher_softened",
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
    private static readonly Color Panel = new Color32(22, 27, 34, 235);
    private static readonly Color Paper = new Color32(235, 233, 226, 246);
    private static readonly Color Ink = new Color32(26, 28, 31, 255);
    private static readonly Color MutedInk = new Color32(87, 92, 98, 255);
    private static readonly Color Accent = new Color32(80, 153, 171, 255);
    private static readonly Color Warm = new Color32(176, 116, 72, 255);
    private static readonly Color Good = new Color32(88, 150, 118, 255);
    private static readonly Color Warn = new Color32(177, 146, 75, 255);
    private static readonly Color Bad = new Color32(165, 86, 82, 255);

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
        if (HasArg("-familyTherapyVnDataAudit"))
        {
            RunVnDataAuditAndQuit();
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

    private void RunVnDataAuditAndQuit()
    {
        string auditPath = Path.Combine(exportFolder, "family_therapy_practicum_vn_data_audit.json");
        var builder = new StringBuilder();
        builder.Append("{\n");
        builder.Append("  \"completed\": true,\n");
        builder.Append("  \"caseCount\": ").Append(cases.Count).Append(",\n");
        builder.Append("  \"vnScriptCount\": ").Append(vnScripts.Count).Append(",\n");
        builder.Append("  \"focusedCaseIds\": [\"FT-002\",\"FT-003\",\"FT-004\",\"FT-005\",\"FT-006\",\"FT-007\",\"FT-008\",\"FT-009\",\"FT-010\"],\n");
        builder.Append("  \"cases\": [\n");
        string[] targetIds = { "FT-002", "FT-003", "FT-004", "FT-005", "FT-006", "FT-007", "FT-008", "FT-009", "FT-010" };
        for (int i = 0; i < targetIds.Length; i++)
        {
            string id = targetIds[i];
            vnScripts.TryGetValue(id, out VnCaseScript script);
            int turnCount = script?.turns?.Count ?? 0;
            int lineCount = script?.turns?.Sum(t => t.setupLines?.Count ?? 0) ?? 0;
            int choiceCount = script?.turns?.Sum(t => t.choices?.Count ?? 0) ?? 0;
            int characterCount = script?.characters?.Length ?? 0;
            string scriptKind = script == null ? "missing" : string.IsNullOrEmpty(script.scriptKind) ? "unknown" : script.scriptKind;
            string expectedScriptKind = id == "FT-002" ? "full_case_specific_v1" : "focused_case_specific_v1";
            bool focused = scriptKind == expectedScriptKind && turnCount == SessionTurnCount && choiceCount == SessionTurnCount * 3;
            string[] presentRouteTokens = CollectRouteTokens(script);
            string[] requiredRouteTokens = GetRequiredFocusedRouteTokens(id);
            string[] missingRouteTokens = requiredRouteTokens.Except(presentRouteTokens).ToArray();
            string[] requiredCgSlots = CollectRequiredCgSlots(script);
            string[] missingCgSlots = requiredCgSlots.Where(path => LoadVnTexture(path) == null).ToArray();
            builder.Append("    { ");
            builder.Append("\"id\": ").Append(JsonString(id)).Append(", ");
            builder.Append("\"hasScript\": ").Append((script != null).ToString().ToLowerInvariant()).Append(", ");
            builder.Append("\"scriptKind\": ").Append(JsonString(scriptKind)).Append(", ");
            builder.Append("\"expectedScriptKind\": ").Append(JsonString(expectedScriptKind)).Append(", ");
            builder.Append("\"turnCount\": ").Append(turnCount).Append(", ");
            builder.Append("\"lineCount\": ").Append(lineCount).Append(", ");
            builder.Append("\"choiceCount\": ").Append(choiceCount).Append(", ");
            builder.Append("\"characterCount\": ").Append(characterCount).Append(", ");
            builder.Append("\"focusedScriptShape\": ").Append(focused.ToString().ToLowerInvariant()).Append(", ");
            builder.Append("\"usesGenericFallback\": ").Append((scriptKind == "generic_fallback").ToString().ToLowerInvariant()).Append(", ");
            builder.Append("\"requiredRouteTokenCount\": ").Append(requiredRouteTokens.Length).Append(", ");
            builder.Append("\"coveredRequiredRouteTokenCount\": ").Append(requiredRouteTokens.Length - missingRouteTokens.Length).Append(", ");
            builder.Append("\"missingRequiredRouteTokens\": ").Append(JsonStringArray(missingRouteTokens)).Append(", ");
            builder.Append("\"endingScenePresenter\": ").Append(HasEndingScenePresenter(id).ToString().ToLowerInvariant()).Append(", ");
            builder.Append("\"requiredCgSlotCount\": ").Append(requiredCgSlots.Length).Append(", ");
            builder.Append("\"availableCgSlotCount\": ").Append(requiredCgSlots.Length - missingCgSlots.Length).Append(", ");
            builder.Append("\"missingCgSlotCount\": ").Append(missingCgSlots.Length).Append(", ");
            builder.Append("\"missingCgSlotExamples\": ").Append(JsonStringArray(missingCgSlots.Take(6)));
            builder.Append(" }");
            if (i < targetIds.Length - 1) builder.Append(",");
            builder.Append("\n");
        }
        builder.Append("  ],\n");
        builder.Append("  \"routeSimulationAudit\": ");
        bool routeAuditPassed = AppendRouteSimulationAudit(builder, targetIds);
        builder.Append("\n");
        builder.Append("}\n");
        File.WriteAllText(auditPath, builder.ToString(), new UTF8Encoding(false));
        string cgManifestPath = Path.Combine(exportFolder, "family_therapy_practicum_cg_slot_manifest.json");
        File.WriteAllText(cgManifestPath, BuildCgSlotManifestJson(targetIds), new UTF8Encoding(false));
        Debug.Log("FAMILY_THERAPY_PRACTICUM_VN_DATA_AUDIT path=" + auditPath);
        Debug.Log("FAMILY_THERAPY_PRACTICUM_CG_SLOT_MANIFEST path=" + cgManifestPath);
        Application.Quit(routeAuditPassed ? 0 : 1);
    }

    private string BuildCgSlotManifestJson(IEnumerable<string> caseIds)
    {
        var builder = new StringBuilder();
        builder.Append("{\n");
        builder.Append("  \"completed\": true,\n");
        builder.Append("  \"manifestVersion\": 3,\n");
        builder.Append("  \"imageFormat\": \"1600x900 PNG, no UI/text/watermark, bottom 25-30 percent clean for dialogue UI\",\n");
        builder.Append("  \"productionMode\": \"commercial visual novel event CG, one required slot per dialogue/reaction/choice/ending\",\n");
        builder.Append("  \"globalNegativePrompt\": \"no square canvas, no stretched image, no subtitle, no speech bubble, no UI, no watermark, no random extra people, no changed seating layout, no cropped essential character, no distorted hands\",\n");
        builder.Append("  \"cases\": [\n");
        string[] ids = caseIds.ToArray();
        for (int i = 0; i < ids.Length; i++)
        {
            string id = ids[i];
            vnScripts.TryGetValue(id, out VnCaseScript script);
            List<CgSlotManifestEntry> slots = BuildCgSlotManifestEntries(id, script);
            int available = slots.Count(slot => slot.exists);
            builder.Append("    {\n");
            builder.Append("      \"id\": ").Append(JsonString(id)).Append(",\n");
            builder.Append("      \"title\": ").Append(JsonString(cases.FirstOrDefault(c => c.id == id)?.title ?? "")).Append(",\n");
            builder.Append("      \"familyType\": ").Append(JsonString(cases.FirstOrDefault(c => c.id == id)?.familyType ?? "")).Append(",\n");
            builder.Append("      \"recommendedTheoryId\": ").Append(JsonString(cases.FirstOrDefault(c => c.id == id)?.recommendedTheoryId ?? "")).Append(",\n");
            builder.Append("      \"scriptKind\": ").Append(JsonString(script == null ? "missing" : script.scriptKind)).Append(",\n");
            builder.Append("      \"caseSpecificVisualBrief\": ").Append(JsonString(GetCaseSpecificVisualBrief(id))).Append(",\n");
            builder.Append("      \"keyProps\": ").Append(JsonStringArray(GetCaseKeyProps(id))).Append(",\n");
            builder.Append("      \"safetyNegativePrompt\": ").Append(JsonString(GetCaseSafetyNegativePrompt(id))).Append(",\n");
            builder.Append("      \"masterShotPolicy\": ").Append(JsonString(GetCaseMasterShotPolicy(id))).Append(",\n");
            builder.Append("      \"targetFolder\": ").Append(JsonString("Assets/Resources/VN/EventCG/" + id.Replace("-", "").ToUpperInvariant() + "/")).Append(",\n");
            builder.Append("      \"requiredCgSlotCount\": ").Append(slots.Count).Append(",\n");
            builder.Append("      \"availableCgSlotCount\": ").Append(available).Append(",\n");
            builder.Append("      \"missingCgSlotCount\": ").Append(slots.Count - available).Append(",\n");
            builder.Append("      \"slots\": [\n");
            for (int j = 0; j < slots.Count; j++)
            {
                AppendCgSlotManifestEntry(builder, slots[j], "        ");
                if (j < slots.Count - 1) builder.Append(",");
                builder.Append("\n");
            }
            builder.Append("      ]\n");
            builder.Append("    }");
            if (i < ids.Length - 1) builder.Append(",");
            builder.Append("\n");
        }
        builder.Append("  ]\n");
        builder.Append("}\n");
        return builder.ToString();
    }

    private List<CgSlotManifestEntry> BuildCgSlotManifestEntries(string caseId, VnCaseScript script)
    {
        var entries = new List<CgSlotManifestEntry>();
        if (script == null || script.turns == null || string.IsNullOrEmpty(caseId)) return entries;

        FamilyCase caseData = cases.FirstOrDefault(c => c.id == caseId);
        string visualBrief = GetCaseSpecificVisualBrief(caseId);
        string keyProps = string.Join("; ", GetCaseKeyProps(caseId));
        string safetyNegative = GetCaseSafetyNegativePrompt(caseId);
        var seen = new HashSet<string>(StringComparer.Ordinal);
        for (int turnIndex = 0; turnIndex < script.turns.Count; turnIndex++)
        {
            VnTurn turn = script.turns[turnIndex];
            if (turn == null) continue;
            string turnSlug = "t" + (turnIndex + 1).ToString("00", CultureInfo.InvariantCulture);
            AddCgSlotManifestEntry(entries, seen, new CgSlotManifestEntry
            {
                caseId = caseId,
                caseTitle = caseData?.title ?? "",
                familyType = caseData?.familyType ?? "",
                recommendedTheoryId = caseData?.recommendedTheoryId ?? "",
                caseSpecificVisualBrief = visualBrief,
                keyProps = keyProps,
                safetyNegativePrompt = safetyNegative,
                resourcePath = BuildConventionCgPath(caseId, turnSlug + "_choice_idle"),
                slotType = "choice_idle",
                turnNumber = turnIndex + 1,
                turnTitle = turn.title,
                composition = "choice deck background: all session participants seated in the stable counseling-room master shot, emotionally matching this turn before the player chooses",
                promptHint = "Show the whole family/session group in a locked 1600x900 counseling-room composition. Keep bottom dialogue area visually simple."
            });

            if (turn.setupLines != null)
            {
                for (int lineIndex = 0; lineIndex < turn.setupLines.Count; lineIndex++)
                {
                    VnDialogueLine line = turn.setupLines[lineIndex];
                    if (line == null || string.IsNullOrEmpty(line.cgResourcePath)) continue;
                    AddCgSlotManifestEntry(entries, seen, new CgSlotManifestEntry
                    {
                        caseId = caseId,
                        caseTitle = caseData?.title ?? "",
                        familyType = caseData?.familyType ?? "",
                        recommendedTheoryId = caseData?.recommendedTheoryId ?? "",
                        caseSpecificVisualBrief = visualBrief,
                        keyProps = keyProps,
                        safetyNegativePrompt = safetyNegative,
                        resourcePath = line.cgResourcePath,
                        slotType = "dialogue",
                        turnNumber = turnIndex + 1,
                        lineNumber = lineIndex + 1,
                        turnTitle = turn.title,
                        speakerId = line.speakerId,
                        expressionId = line.expressionId,
                        position = line.position,
                        text = line.text,
                        supervisorNote = line.supervisorNote,
                        composition = IsSupervisorSpeaker(line.speakerId) ? "supervisor close/master shot across from the family" : "family/session master shot with speaker emphasized but other participants still seated consistently",
                        promptHint = BuildCgPromptHint(caseId, line.speakerId, line.expressionId, line.text)
                    });
                }
            }

            if (turn.choices != null)
            {
                for (int choiceIndex = 0; choiceIndex < turn.choices.Count; choiceIndex++)
                {
                    VnChoice choice = turn.choices[choiceIndex];
                    if (choice == null || string.IsNullOrEmpty(choice.reactionCgResourcePath)) continue;
                    AddCgSlotManifestEntry(entries, seen, new CgSlotManifestEntry
                    {
                        caseId = caseId,
                        caseTitle = caseData?.title ?? "",
                        familyType = caseData?.familyType ?? "",
                        recommendedTheoryId = caseData?.recommendedTheoryId ?? "",
                        caseSpecificVisualBrief = visualBrief,
                        keyProps = keyProps,
                        safetyNegativePrompt = safetyNegative,
                        resourcePath = choice.reactionCgResourcePath,
                        slotType = "reaction",
                        turnNumber = turnIndex + 1,
                        choiceNumber = choiceIndex + 1,
                        turnTitle = turn.title,
                        choiceLabel = choice.label,
                        theoryId = choice.theoryId,
                        interventionType = choice.interventionType,
                        quality = choice.quality,
                        feedback = choice.feedback,
                        familyReaction = choice.familyReaction,
                        reactionSpeakerId = choice.reactionSpeakerId,
                        reactionExpressionId = choice.reactionExpressionId,
                        composition = IsSupervisorSpeaker(choice.reactionSpeakerId) ? "supervisor reaction shot" : "family/session reaction shot with the reacting participant emphasized",
                        promptHint = BuildReactionCgPromptHint(caseId, choice)
                    });
                }
            }
        }

        foreach (string endingPath in GetRequiredEndingCgSlots(caseId))
        {
            string endingKey = ExtractEndingKey(endingPath);
            AddCgSlotManifestEntry(entries, seen, new CgSlotManifestEntry
            {
                caseId = caseId,
                caseTitle = caseData?.title ?? "",
                familyType = caseData?.familyType ?? "",
                recommendedTheoryId = caseData?.recommendedTheoryId ?? "",
                caseSpecificVisualBrief = visualBrief,
                keyProps = keyProps,
                safetyNegativePrompt = safetyNegative,
                resourcePath = endingPath,
                slotType = "ending",
                endingKey = endingKey,
                endingLabel = BuildManifestEndingLabel(endingKey),
                routeEndingVisualState = BuildRouteEndingVisualState(caseId, endingKey),
                composition = "ending CG showing the final emotional state of the family/session after this route",
                promptHint = "Create a route ending CG for " + caseId + ". Ending state: " + BuildManifestEndingLabel(endingKey) + ". Visual state: " + BuildRouteEndingVisualState(caseId, endingKey) + ". Keep it commercial visual novel quality, 1600x900, no UI/text."
            });
        }

        return entries;
    }

    private static void AddCgSlotManifestEntry(List<CgSlotManifestEntry> entries, HashSet<string> seen, CgSlotManifestEntry entry)
    {
        if (entry == null || string.IsNullOrEmpty(entry.resourcePath) || !seen.Add(entry.resourcePath)) return;
        entry.filePath = "Assets/Resources/" + entry.resourcePath + ".png";
        entry.exists = LoadVnTexture(entry.resourcePath) != null;
        entries.Add(entry);
    }

    private static void AppendCgSlotManifestEntry(StringBuilder builder, CgSlotManifestEntry entry, string indent)
    {
        builder.Append(indent).Append("{ ");
        AppendJsonField(builder, "resourcePath", entry.resourcePath, true);
        AppendJsonField(builder, "filePath", entry.filePath, true);
        builder.Append("\"exists\": ").Append(entry.exists.ToString().ToLowerInvariant()).Append(", ");
        AppendJsonField(builder, "slotType", entry.slotType, true);
        AppendJsonField(builder, "caseId", entry.caseId, true);
        AppendJsonField(builder, "caseTitle", entry.caseTitle, true);
        AppendJsonField(builder, "familyType", entry.familyType, true);
        AppendJsonField(builder, "recommendedTheoryId", entry.recommendedTheoryId, true);
        AppendJsonField(builder, "caseSpecificVisualBrief", entry.caseSpecificVisualBrief, true);
        AppendJsonField(builder, "keyProps", entry.keyProps, true);
        AppendJsonField(builder, "safetyNegativePrompt", entry.safetyNegativePrompt, true);
        if (entry.turnNumber > 0) builder.Append("\"turnNumber\": ").Append(entry.turnNumber).Append(", ");
        if (entry.lineNumber > 0) builder.Append("\"lineNumber\": ").Append(entry.lineNumber).Append(", ");
        if (entry.choiceNumber > 0) builder.Append("\"choiceNumber\": ").Append(entry.choiceNumber).Append(", ");
        AppendJsonField(builder, "turnTitle", entry.turnTitle, true);
        AppendJsonField(builder, "speakerId", entry.speakerId, true);
        AppendJsonField(builder, "expressionId", entry.expressionId, true);
        AppendJsonField(builder, "position", entry.position, true);
        AppendJsonField(builder, "text", entry.text, true);
        AppendJsonField(builder, "supervisorNote", entry.supervisorNote, true);
        AppendJsonField(builder, "choiceLabel", entry.choiceLabel, true);
        AppendJsonField(builder, "theoryId", entry.theoryId, true);
        AppendJsonField(builder, "interventionType", entry.interventionType, true);
        if (entry.quality != 0) builder.Append("\"quality\": ").Append(entry.quality).Append(", ");
        AppendJsonField(builder, "feedback", entry.feedback, true);
        AppendJsonField(builder, "familyReaction", entry.familyReaction, true);
        AppendJsonField(builder, "reactionSpeakerId", entry.reactionSpeakerId, true);
        AppendJsonField(builder, "reactionExpressionId", entry.reactionExpressionId, true);
        AppendJsonField(builder, "endingKey", entry.endingKey, true);
        AppendJsonField(builder, "endingLabel", entry.endingLabel, true);
        AppendJsonField(builder, "routeEndingVisualState", entry.routeEndingVisualState, true);
        AppendJsonField(builder, "composition", entry.composition, true);
        AppendJsonField(builder, "promptHint", entry.promptHint, false);
        builder.Append(" }");
    }

    private static void AppendJsonField(StringBuilder builder, string name, string value, bool trailingComma)
    {
        builder.Append(JsonString(name)).Append(": ").Append(JsonString(value ?? ""));
        if (trailingComma) builder.Append(", ");
    }

    private static bool IsSupervisorSpeaker(string speakerId)
    {
        return !string.IsNullOrEmpty(speakerId) && speakerId.StartsWith("supervisor_", StringComparison.OrdinalIgnoreCase);
    }

    private static string BuildCgPromptHint(string caseId, string speakerId, string expressionId, string text)
    {
        string shot = IsSupervisorSpeaker(speakerId) ? "Use the supervisor shot, seated across from the family." : "Use the stable family/session master shot; keep all required participants seated in the same positions.";
        return shot + " Emphasize speaker " + speakerId + " with expression " + expressionId + ". Dialogue meaning: " + text;
    }

    private static string BuildReactionCgPromptHint(string caseId, VnChoice choice)
    {
        if (choice == null) return "";
        string shot = IsSupervisorSpeaker(choice.reactionSpeakerId) ? "Use the supervisor reaction shot." : "Use the stable family/session reaction shot.";
        return shot + " Player choice: " + choice.label + " Reaction: " + choice.familyReaction + " Feedback meaning: " + choice.feedback;
    }

    private static string ExtractEndingKey(string resourcePath)
    {
        if (string.IsNullOrEmpty(resourcePath)) return "";
        int index = resourcePath.IndexOf("_ending_", StringComparison.OrdinalIgnoreCase);
        return index < 0 ? "" : resourcePath.Substring(index + "_ending_".Length);
    }

    private static string BuildManifestEndingLabel(string endingKey)
    {
        switch (endingKey)
        {
            case "a_integrated": return "A route: integrated therapeutic plan, family leaves with a concrete next-week experiment";
            case "b_repaired": return "B repaired route: earlier rupture is named and partially restored";
            case "b_partial": return "B partial route: useful insight exists but key risk remains unfinished";
            case "c_key_risk_unrepaired": return "C route: core case risk remains unrepaired";
            case "d_closed_or_harmful": return "D route: intervention closes the family or worsens the pattern";
            case "d_safety_unresolved": return "D safety route: urgent safety/risk issue remains unresolved";
            default: return endingKey;
        }
    }

    private static string GetCaseSpecificVisualBrief(string caseId)
    {
        switch (caseId)
        {
            case "FT-002": return "Bowen 조손가족. 조모의 통제는 악역성이 아니라 상실 불안에서 나온다. 손자는 반항아처럼 과장하지 말고 감시받는 청소년의 거리두기와 방어를 보여준다. 조부는 가운데 해결자가 아니라 회피적 완충자처럼 가장자리에서 긴장한다.";
            case "FT-003": return "구조적 가족치료. 파란 치료가방과 일정표가 부모 하위체계의 부담을 상징한다. 아이가 결정권자처럼 중앙에 놓이면 위험 경로, 부모가 나란히 기준을 드는 구도면 좋은 경로다.";
            case "FT-004": return "Satir 사례. 보호자의 굳은 웃음, 손의 긴장, 도움 요청을 삼키는 표정이 중요하다. 배우자의 비난은 분노 밑의 불안으로 표현하고, 기관 담당자는 해결자가 아니라 외부 압력/자원으로 배치한다.";
            case "FT-005": return "구조적 재혼가족. 빈 의자, 닫히는 방문, 어머니가 사이에 끼는 삼각구도, 새아버지가 권한을 앞세우는 위험을 시각화한다. 좋은 경로는 새아버지 권위보다 작고 조심스러운 직접 접촉이다.";
            case "FT-006": return "Satir 질병 형제 사례. 병원 일정이 둘째의 학교가방/일상 위로 덮이는 장면을 피하지 말고 보여준다. 둘째가 부모를 달래는 위로자 자리와 부모 죄책감이 방을 차지하는 구도를 위험으로 표현한다.";
            case "FT-007": return "정신역동 가족 사례. 수치심과 방어의 순서를 공격적으로 해석하지 말고, 닫힌 방문/구직 실패/돈 봉투/계약서가 가족 권력과 자존심을 어떻게 흔드는지 보여준다.";
            case "FT-008": return "이야기치료 학교폭력 사례. 피해자를 사건 자체로 고정하지 않는다. 강제 폭로/버티기 미화는 위험 구도이며, 좋은 경로는 청소년이 문제 이름과 증인 범위를 직접 정하는 모습이다.";
            case "FT-009": return "CBFT 산후 안전 사례. 따뜻한 위로보다 오늘 밤 안전, 교대, 수면, 위기 연락이 시각적으로 선명해야 한다. 산후우울을 게으름/무능으로 보이게 하지 않는다.";
            case "FT-010": return "해결중심 부모화 사례. 청소년을 영웅이나 작은 부모처럼 미화하지 않는다. 좋은 경로는 부담 8을 7로 낮추는 작은 역할 재분배와 자기시간 회복이다.";
            default: return "상담실 안에서 가족/기관 인물의 자리와 정서 변화를 일관되게 보여준다.";
        }
    }

    private static IEnumerable<string> GetCaseKeyProps(string caseId)
    {
        switch (caseId)
        {
            case "FT-002": return new[] { "11:37 휴대폰 화면", "조부의 접힌 신문", "거실/상담실 거리감", "아버지 이야기를 피하는 침묵" };
            case "FT-003": return new[] { "파란 치료가방", "냉장고 일정표", "헤드폰과 물병", "기관 클립보드", "부모가 함께 든 일정표" };
            case "FT-004": return new[] { "접힌 보육 안내문", "빨간 누락 표시", "스피커폰", "서류봉투", "체크리스트와 도움 요청 문장" };
            case "FT-005": return new[] { "빈 식탁 의자", "닫힌 방문", "친아버지 메시지", "어머니의 중간 자리", "30초 직접 인사 장면" };
            case "FT-006": return new[] { "병원 가방", "닫힌 학교가방", "치료 일정표", "병원 알람", "어머니 휴지", "조부모 열쇠고리", "10분 타이머" };
            case "FT-007": return new[] { "닫힌 방문", "구직 탈락 이메일", "숨긴 현금 봉투", "식탁 장부", "낡은 작업화", "모욕 금지 계약서" };
            case "FT-008": return new[] { "상담실 메모지의 문제 이름", "학교 절차 서류", "동의 범위 체크", "안전계획 카드", "증인 이름이 적힌 빈 카드" };
            case "FT-009": return new[] { "오늘 밤 안전계획 카드", "아기 침대/모니터", "교대 시간표", "위기 연락처", "물컵과 수면 알람", "지원 인물 문자" };
            case "FT-010": return new[] { "물통", "동생 가방", "30분 자기시간 타이머", "보호자 10분 알람", "부담 척도 8에서 7 메모" };
            default: return Array.Empty<string>();
        }
    }

    private static string GetCaseSafetyNegativePrompt(string caseId)
    {
        switch (caseId)
        {
            case "FT-002": return "do not portray the grandson as delinquent villain, do not make grandmother abusive caricature, do not resolve loss with a sentimental hug";
            case "FT-003": return "do not make the child the sole decision maker, do not glorify professional authority replacing parents, do not hide the blue therapy bag burden";
            case "FT-004": return "do not turn placating into simple happiness, do not make paperwork the hero, do not shame the caregiver for needing help";
            case "FT-005": return "do not romanticize instant stepfather authority, do not force respect visually, do not erase loyalty to biological father";
            case "FT-006": return "do not make the healthy sibling cheerful helper, do not let illness props erase sibling needs, do not center parent guilt so strongly that the child comforts adults";
            case "FT-007": return "do not show interpretation as an attack, do not make money contract look punitive, do not turn father into a villain or child into proof object";
            case "FT-008": return "do not blame the victim, do not force disclosure, do not glorify endurance, do not let school procedure take over the teen's story";
            case "FT-009": return "do not minimize postpartum risk, do not depict the mother as lazy or incompetent, do not replace safety planning with warmth only, do not show unsafe infant handling";
            case "FT-010": return "do not heroize parentification, do not make the teen a family manager, do not turn rest into another performance task";
            default: return "do not stereotype or blame the family member showing symptoms";
        }
    }

    private static string GetCaseMasterShotPolicy(string caseId)
    {
        return "For this case, lock one family/session master shot and one supervisor master shot before producing variations. Keep seats, character scale, room geometry, lens, and bottom dialogue-safe area consistent across all dialogue/reaction/choice CGs.";
    }

    private static string BuildRouteEndingVisualState(string caseId, string endingKey)
    {
        string brief = GetCaseSpecificVisualBrief(caseId);
        switch (endingKey)
        {
            case "a_integrated": return "participants are not magically fixed, but they share one concrete next-step object or written plan connected to the case anchors. " + brief;
            case "b_repaired": return "a visible rupture has softened; one person remains cautious while another repairs with a specific sentence or small action. " + brief;
            case "b_partial": return "some insight is present, but a key person or risk object still sits unresolved in the frame. " + brief;
            case "c_key_risk_unrepaired": return "the main risk anchor remains visually dominant and unaddressed; avoid melodrama, show quiet unfinished tension. " + brief;
            case "d_closed_or_harmful": return "the family/session closes down: bodies turn away, paperwork/control/diagnosis dominates, and the symptom-bearer is isolated. " + brief;
            case "d_safety_unresolved": return "safety remains unresolved; show concrete missing safety supports without sensationalizing danger. " + brief;
            default: return brief;
        }
    }

    private sealed class CgSlotManifestEntry
    {
        public string resourcePath;
        public string filePath;
        public bool exists;
        public string slotType;
        public string caseId;
        public string caseTitle;
        public string familyType;
        public string recommendedTheoryId;
        public string caseSpecificVisualBrief;
        public string keyProps;
        public string safetyNegativePrompt;
        public int turnNumber;
        public int lineNumber;
        public int choiceNumber;
        public string turnTitle;
        public string speakerId;
        public string expressionId;
        public string position;
        public string text;
        public string supervisorNote;
        public string choiceLabel;
        public string theoryId;
        public string interventionType;
        public int quality;
        public string feedback;
        public string familyReaction;
        public string reactionSpeakerId;
        public string reactionExpressionId;
        public string endingKey;
        public string endingLabel;
        public string routeEndingVisualState;
        public string composition;
        public string promptHint;
    }

    private static string ClassifyCgSlot(string resourcePath)
    {
        if (string.IsNullOrEmpty(resourcePath)) return "unknown";
        if (resourcePath.IndexOf("_ending_", StringComparison.OrdinalIgnoreCase) >= 0) return "ending";
        if (resourcePath.IndexOf("_choice_idle", StringComparison.OrdinalIgnoreCase) >= 0) return "choice_idle";
        if (resourcePath.IndexOf("_reaction_", StringComparison.OrdinalIgnoreCase) >= 0) return "reaction";
        return "dialogue";
    }

    private static string[] CollectRouteTokens(VnCaseScript script)
    {
        if (script == null || script.turns == null) return Array.Empty<string>();
        return script.turns
            .Where(t => t != null && t.choices != null)
            .SelectMany(t => t.choices)
            .Where(c => c != null && !string.IsNullOrEmpty(c.interventionType))
            .SelectMany(c => SplitRouteTokens(c.interventionType))
            .Distinct()
            .OrderBy(token => token, StringComparer.Ordinal)
            .ToArray();
    }

    private static IEnumerable<string> SplitRouteTokens(string interventionType)
    {
        if (string.IsNullOrEmpty(interventionType)) yield break;
        foreach (string token in interventionType.Split(new[] { '|', ',', ';', ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
        {
            string trimmed = token.Trim();
            if (!string.IsNullOrEmpty(trimmed)) yield return trimmed;
        }
    }

    private static string[] CollectRequiredCgSlots(VnCaseScript script)
    {
        if (script == null || script.turns == null || string.IsNullOrEmpty(script.caseId)) return Array.Empty<string>();
        var slots = new List<string>();
        for (int turnIndex = 0; turnIndex < script.turns.Count; turnIndex++)
        {
            VnTurn turn = script.turns[turnIndex];
            string turnSlug = "t" + (turnIndex + 1).ToString("00", CultureInfo.InvariantCulture);
            slots.Add(BuildConventionCgPath(script.caseId, turnSlug + "_choice_idle"));
            if (turn.setupLines != null)
            {
                slots.AddRange(turn.setupLines.Where(line => line != null && !string.IsNullOrEmpty(line.cgResourcePath)).Select(line => line.cgResourcePath));
            }
            if (turn.choices != null)
            {
                slots.AddRange(turn.choices.Where(choice => choice != null && !string.IsNullOrEmpty(choice.reactionCgResourcePath)).Select(choice => choice.reactionCgResourcePath));
            }
        }
        slots.AddRange(GetRequiredEndingCgSlots(script.caseId));
        return slots.Distinct().OrderBy(path => path, StringComparer.Ordinal).ToArray();
    }

    private static IEnumerable<string> GetRequiredEndingCgSlots(string caseId)
    {
        string[] endingKeys =
        {
            "a_integrated",
            "b_repaired",
            "b_partial",
            "c_key_risk_unrepaired",
            "d_closed_or_harmful",
            "d_safety_unresolved"
        };
        foreach (string endingKey in endingKeys)
        {
            yield return BuildConventionCgPath(caseId, "ending_" + endingKey);
        }
    }

    private static string[] GetRequiredFocusedRouteTokens(string caseId)
    {
        switch (caseId)
        {
            case "FT-002":
                return new[] { "joining", "circular_mapping", "loss_reflection", "i_position", "feedback_task", "ip_fixing", "premature_correction", "shift_burden", "control_escalation", "exception", "premature_depth", "premature_contract", "diagnostic_closure", "compliance_promise", "symptom_check_closure" };
            case "FT-003":
                return new[] { "parental_alignment", "implementation_burden_seen", "rest_standard_defined", "professional_used_as_resource", "home_practice_parent_team", "child_made_decider", "schedule_as_command", "mother_overfunctioning", "father_excluded", "father_blamed", "professional_authority_outsourced", "child_decision_hidden", "outsourced_closure" };
            case "FT-004":
                return new[] { "iceberg_named", "checklist_connected_to_help", "congruent_pair_statement", "voice_practiced_with_support", "congruent_help_plan", "institution_as_only_solution", "placating_accepted", "caregiver_alone_responsible", "emotion_bypassed", "placating_deepened", "compliance_trap", "blame_reversed", "voice_removed", "family_bypass", "institution_final_fix" };
            case "FT-005":
                return new[] { "staged_contact", "authority_speed_acknowledged", "loyalty_named", "direct_30sec_contact", "home_practice_boundary", "respect_rule_imposed", "respect_rule_final", "stepfather_excluded", "authority_push", "mother_bridge_reinforced", "premature_parent_claim", "mediation_burden", "triangulation_reinforced", "avoidant_closure" };
            case "FT-006":
                return new[] { "two_feelings_named", "family_sculpture", "emotion_reflection_balanced", "ten_minute_ritual", "home_ritual", "guilt_flood", "illness_totalizing", "cheerful_mask_reinforced", "sibling_centered_only", "outsourced_emotion", "parent_guilt_centered", "delayed_sibling_need" };
            case "FT-007":
                return new[] { "shame_named", "triangle_named", "defense_sequence_seen", "respect_contract", "money_contract_written", "premature_interpretation", "premature_depth", "interpretation_attack", "father_control_shift", "mother_triangle_locked", "child_must_prove", "cutoff_contract", "performance_report", "triangulated_closure", "contract_before_shame" };
            case "FT-008":
                return new[] { "safety_check_before_story", "silence_externalized", "problem_name_teen_authored", "unique_outcome_thickened", "outsider_witness_consent", "alternative_story_task", "forced_disclosure", "disclosure_homework", "endurance_story", "procedure_closure", "disclosure_pressure", "parent_action_over_teen_voice", "school_takes_story", "silence_kept", "decision_before_story" };
            case "FT-009":
                return new[] { "safety_screen_started", "crisis_contact_named", "baby_safety_check", "automatic_thought_linked", "first_cry_contract", "support_network_contacted", "safety_plan_written", "empathy_before_safety", "chore_plan_without_risk", "mindreading_contract", "permission_loop", "support_replaces_spouse", "spouse_overpromise", "chores_only_closure", "mother_must_initiate_crisis", "warmth_without_plan", "thought_homework_without_sleep" };
            case "FT-010":
                return new[] { "scale_one_point", "exception_scaled", "strength_reduces_burden", "shared_micro_tasks", "one_point_relief_plan", "hero_burden", "parentification_organized", "guilt_centered", "resource_takeover", "teen_optimizes_rest", "apology_only" };
            default:
                return Array.Empty<string>();
        }
    }

    private bool AppendRouteSimulationAudit(StringBuilder builder, IEnumerable<string> caseIds)
    {
        var allResults = new List<RouteSimulationResult>();
        foreach (string caseId in caseIds ?? Array.Empty<string>())
        {
            if (!vnScripts.TryGetValue(caseId, out VnCaseScript script) || script == null)
            {
                allResults.Add(new RouteSimulationResult
                {
                    caseId = caseId,
                    routeName = "script_missing",
                    applicable = false,
                    expectedEndingSuffix = "",
                    actualEndingId = "script_missing",
                    passed = false
                });
                continue;
            }
            allResults.AddRange(BuildRouteSimulationResults(caseId, script));
        }

        int applicableCount = allResults.Count(r => r.applicable);
        int passedApplicableCount = allResults.Count(r => r.applicable && r.passed);
        bool allPassed = allResults.All(r => !r.applicable || r.passed);
        builder.Append("{\n");
        builder.Append("    \"routeCount\": ").Append(allResults.Count).Append(",\n");
        builder.Append("    \"applicableRouteCount\": ").Append(applicableCount).Append(",\n");
        builder.Append("    \"passedRouteCount\": ").Append(passedApplicableCount).Append(",\n");
        builder.Append("    \"passedApplicableRouteCount\": ").Append(passedApplicableCount).Append(",\n");
        builder.Append("    \"allRoutesPassed\": ").Append(allPassed.ToString().ToLowerInvariant()).Append(",\n");
        builder.Append("    \"routes\": [\n");
        for (int i = 0; i < allResults.Count; i++)
        {
            AppendRouteSimulationResult(builder, allResults[i], "      ");
            if (i < allResults.Count - 1) builder.Append(",");
            builder.Append("\n");
        }
        builder.Append("    ]\n");
        builder.Append("  }");
        return allPassed;
    }

    private List<RouteSimulationResult> BuildRouteSimulationResults(string caseId, VnCaseScript script)
    {
        var results = new List<RouteSimulationResult>
        {
            SimulateRoute(caseId, script, "all_good", BestRouteIndexes(script), "_A_integrated"),
            SimulateRoute(caseId, script, "first_bad_then_good", OneChangedRouteIndexes(script, 0, 1), IsSafetyCriticalCase(caseId) ? "_D_safety_unresolved" : "_B_repaired"),
            SimulateRoute(caseId, script, "middle_bad_then_good", OneChangedRouteIndexes(script, 1, 1), "_B_repaired"),
            SimulateRoute(caseId, script, "all_worst", WorstRouteIndexes(script), IsSafetyCriticalCase(caseId) ? "_D_safety_unresolved" : "_D_closed_or_harmful")
        };

        if (IsSafetyCriticalCase(caseId))
        {
            results.Add(SimulateRoute(caseId, script, "safety_missing", OneChangedRouteIndexes(script, 0, 1), "_D_safety_unresolved"));
        }
        else
        {
            results.Add(new RouteSimulationResult
            {
                caseId = caseId,
                routeName = "safety_missing",
                applicable = false,
                expectedEndingSuffix = "",
                actualEndingId = "",
                passed = true
            });
        }

        return results;
    }

    private static int[] BestRouteIndexes(VnCaseScript script)
    {
        if (script?.turns == null) return Array.Empty<int>();
        var indexes = new int[script.turns.Count];
        for (int i = 0; i < script.turns.Count; i++)
        {
            VnTurn turn = script.turns[i];
            indexes[i] = BestChoiceIndex(turn);
        }
        return indexes;
    }

    private static int[] WorstRouteIndexes(VnCaseScript script)
    {
        if (script?.turns == null) return Array.Empty<int>();
        var indexes = new int[script.turns.Count];
        for (int i = 0; i < script.turns.Count; i++)
        {
            VnTurn turn = script.turns[i];
            indexes[i] = WorstChoiceIndex(turn);
        }
        return indexes;
    }

    private static int[] OneChangedRouteIndexes(VnCaseScript script, int turnIndex, int choiceIndex)
    {
        int[] indexes = BestRouteIndexes(script);
        if (indexes.Length == 0) return indexes;
        int clampedTurn = Mathf.Clamp(turnIndex, 0, indexes.Length - 1);
        VnTurn turn = script.turns[clampedTurn];
        int choiceCount = turn?.choices?.Count ?? 0;
        indexes[clampedTurn] = choiceCount == 0 ? 0 : Mathf.Clamp(choiceIndex, 0, choiceCount - 1);
        return indexes;
    }

    private static int BestChoiceIndex(VnTurn turn)
    {
        if (turn?.choices == null || turn.choices.Count == 0) return 0;
        int bestIndex = 0;
        int bestQuality = int.MinValue;
        for (int i = 0; i < turn.choices.Count; i++)
        {
            int quality = turn.choices[i]?.quality ?? int.MinValue;
            if (quality > bestQuality)
            {
                bestQuality = quality;
                bestIndex = i;
            }
        }
        return bestIndex;
    }

    private static int WorstChoiceIndex(VnTurn turn)
    {
        if (turn?.choices == null || turn.choices.Count == 0) return 0;
        int worstIndex = 0;
        int worstQuality = int.MaxValue;
        for (int i = 0; i < turn.choices.Count; i++)
        {
            int quality = turn.choices[i]?.quality ?? int.MaxValue;
            if (quality < worstQuality)
            {
                worstQuality = quality;
                worstIndex = i;
            }
        }
        return worstIndex;
    }

    private static RouteSimulationResult SimulateRoute(string caseId, VnCaseScript script, string routeName, int[] selectedChoiceIndexes, string expectedEndingSuffix)
    {
        var selections = new List<SessionSelection>();
        var selectedPath = new List<string>();
        if (script?.turns != null)
        {
            for (int i = 0; i < script.turns.Count; i++)
            {
                VnTurn turn = script.turns[i];
                int choiceCount = turn?.choices?.Count ?? 0;
                if (choiceCount == 0) continue;
                int selectedIndex = selectedChoiceIndexes != null && i < selectedChoiceIndexes.Length ? selectedChoiceIndexes[i] : 0;
                selectedIndex = Mathf.Clamp(selectedIndex, 0, choiceCount - 1);
                VnChoice choice = turn.choices[selectedIndex];
                string token = choice?.interventionType ?? "";
                selectedPath.Add("T" + (i + 1).ToString(CultureInfo.InvariantCulture) + "." + (selectedIndex + 1).ToString(CultureInfo.InvariantCulture) + ":" + token);
                selections.Add(new SessionSelection
                {
                    turn = i + 1,
                    choice = choice?.label ?? "",
                    theoryId = choice?.theoryId ?? "",
                    routeQuality = choice?.quality ?? 0,
                    quality = choice?.quality ?? 0,
                    feedback = choice?.feedback ?? "",
                    interventionType = token,
                    familyReaction = choice?.familyReaction ?? "",
                    reactionSpeakerId = choice?.reactionSpeakerId ?? "",
                    reactionExpressionId = choice?.reactionExpressionId ?? ""
                });
            }
        }

        string[] routeTokens = selections
            .Where(s => !string.IsNullOrEmpty(s.interventionType))
            .SelectMany(s => SplitRouteTokens(s.interventionType))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(token => token, StringComparer.Ordinal)
            .ToArray();
        string endingId = ResolveEndingIdForSelections(caseId, selections);
        bool passed = string.IsNullOrEmpty(expectedEndingSuffix) || endingId.EndsWith(expectedEndingSuffix, StringComparison.OrdinalIgnoreCase);
        return new RouteSimulationResult
        {
            caseId = caseId,
            routeName = routeName,
            applicable = true,
            selectedPath = selectedPath.ToArray(),
            routeTokens = routeTokens,
            highCount = selections.Count(s => GetRouteQuality(s) >= 80),
            lowCount = selections.Count(s => GetRouteQuality(s) < 50),
            finalHigh = selections.Count > 0 && GetRouteQuality(selections.Last()) >= 80,
            expectedEndingSuffix = expectedEndingSuffix,
            actualEndingId = endingId,
            passed = passed
        };
    }

    private static string ResolveEndingIdForSelections(string caseId, IReadOnlyList<SessionSelection> selections)
    {
        if (selections == null || selections.Count == 0) return "no_choices";
        int high = selections.Count(s => GetRouteQuality(s) >= 80);
        int low = selections.Count(s => GetRouteQuality(s) < 50);
        bool finalHigh = GetRouteQuality(selections[selections.Count - 1]) >= 80;
        string joinedFlags = " " + string.Join(" ", selections.Select(s => s.interventionType ?? "").ToArray()) + " ";
        string[] selectedTokens = SplitRouteTokens(joinedFlags).ToArray();
        if (IsSafetyCriticalCase(caseId) && !HasRequiredSafetyTokens(caseId, selectedTokens))
        {
            return caseId + "_D_safety_unresolved";
        }

        string caseSpecific = ResolveCaseSpecificEndingId(caseId, joinedFlags, high, low, finalHigh);
        if (!string.IsNullOrEmpty(caseSpecific))
        {
            return caseSpecific;
        }

        if (low >= 2) return caseId + "_D_closed_or_harmful";
        if (low == 1 && high >= 3 && finalHigh) return caseId + "_B_repaired";
        if (high >= 4 && finalHigh) return caseId + "_A_integrated";
        if (ContainsAny(joinedFlags, "forced_disclosure", "child_made_decider", "authority_push", "guilt_flood", "premature_interpretation", "hero_burden", "placating_deepened")) return caseId + "_C_key_risk_unrepaired";
        return caseId + "_B_partial";
    }

    private static void AppendRouteSimulationResult(StringBuilder builder, RouteSimulationResult result, string indent)
    {
        builder.Append(indent).Append("{ ");
        builder.Append("\"caseId\": ").Append(JsonString(result.caseId)).Append(", ");
        builder.Append("\"routeName\": ").Append(JsonString(result.routeName)).Append(", ");
        builder.Append("\"applicable\": ").Append(result.applicable.ToString().ToLowerInvariant()).Append(", ");
        builder.Append("\"selectedPath\": ").Append(JsonStringArray(result.selectedPath ?? Array.Empty<string>())).Append(", ");
        builder.Append("\"routeTokens\": ").Append(JsonStringArray(result.routeTokens ?? Array.Empty<string>())).Append(", ");
        builder.Append("\"highCount\": ").Append(result.highCount).Append(", ");
        builder.Append("\"lowCount\": ").Append(result.lowCount).Append(", ");
        builder.Append("\"finalHigh\": ").Append(result.finalHigh.ToString().ToLowerInvariant()).Append(", ");
        builder.Append("\"expectedEndingSuffix\": ").Append(JsonString(result.expectedEndingSuffix)).Append(", ");
        builder.Append("\"actualEndingId\": ").Append(JsonString(result.actualEndingId)).Append(", ");
        builder.Append("\"passed\": ").Append(result.passed.ToString().ToLowerInvariant());
        builder.Append(" }");
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

        ShowEthics();
        yield return CaptureVisualAuditFrame("01a_ethics_notice", auditFolder, entries);

        ShowCampaignBriefing();
        yield return CaptureVisualAuditFrame("01b_campaign_briefing", auditFolder, entries);

        caseBrowserPage = 0;
        ShowCaseBrowser();
        yield return CaptureVisualAuditFrame("02_case_browser_page_1", auditFolder, entries);

        caseBrowserPage = 5;
        ShowCaseBrowser();
        yield return CaptureVisualAuditFrame("03_case_browser_page_6", auditFolder, entries);

        BeginCaseIntake(cases.First(c => c.id == "FT-001"));
        yield return CaptureVisualAuditFrame("04_ft001_intake", auditFolder, entries);

        BeginVnCase(cases.First(c => c.id == "FT-001"));
        yield return CaptureVisualAuditFrame("05_ft001_intro_mother", auditFolder, entries);

        currentVnIntroLineIndex = 1;
        ShowVnIntroLine();
        yield return CaptureVisualAuditFrame("05b_ft001_intro_child", auditFolder, entries);

        currentVnIntroLineIndex = 3;
        ShowVnIntroLine();
        yield return CaptureVisualAuditFrame("05c_ft001_intro_teacher", auditFolder, entries);

        currentVnLineIndex = 1;
        ShowVnSessionTurn();
        yield return CaptureVisualAuditFrame("06_ft001_dialogue_line_2", auditFolder, entries);

        ShowVnChoiceDeck(currentVnScript.turns[currentTurn]);
        yield return CaptureVisualAuditFrame("07_ft001_choice_deck", auditFolder, entries);

        ApplyVnChoice(currentVnScript.turns[currentTurn].choices[0]);
        yield return CaptureVisualAuditFrame("08_ft001_reaction", auditFolder, entries);

        currentTurn++;
        currentVnLineIndex = 0;
        ShowVnSessionTurn();
        yield return CaptureVisualAuditFrame("08b_ft001_choice_affects_next_scene", auditFolder, entries);

        BeginVnCase(cases.First(c => c.id == "FT-001"));
        ApplyVnChoice(currentVnScript.turns[currentTurn].choices[1]);
        currentTurn++;
        currentVnLineIndex = 0;
        ShowVnSessionTurn();
        yield return CaptureVisualAuditFrame("08c_ft001_bad_choice_affects_next_scene", auditFolder, entries);

        BeginVnCase(cases.First(c => c.id == "FT-012"));
        yield return CaptureVisualAuditFrame("09_ft012_corecase_dialogue", auditFolder, entries);

        ShowVnChoiceDeck(currentVnScript.turns[currentTurn]);
        yield return CaptureVisualAuditFrame("10_ft012_corecase_choices", auditFolder, entries);

        FamilyCase genericCase = cases.FirstOrDefault(c => !HasVnScript(c)) ?? cases.First();
        currentCase = genericCase;
        selectedTheory = theories.First(t => t.id == currentCase.recommendedTheoryId);
        currentTurn = 0;
        currentSelections.Clear();
        trustScore = 50;
        safetyScore = 50;
        insightScore = 50;
        ShowSessionTurn();
        yield return CaptureVisualAuditFrame("10b_generic_session", auditFolder, entries);

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
        scaler.referenceResolution = new Vector2(1200, 675);
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
                "어머니: 괜찮아요. 제가 더 배워야죠. 다시 해보면 됩니다.",
                "아버지: 매번 괜찮다고만 하니까 똑같은 일이 반복되는 거예요.",
                "어머니: 제가 말하면 더 복잡해져요. 그냥 조용히 있는 게 낫습니다.",
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
                "산후 보호자: 제가 못 버티는 걸 인정하면 엄마 자격이 없는 것 같아요.",
                "배우자: 도와주려고 하면 방식이 틀렸다고 해서 손을 못 대겠어요.",
                "산후 보호자: 말하지 않아도 알아서 해주면 좋겠어요.",
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
        AddVnCharacter("ft001_child", "이주형", "등교 거부 뒤에 분리 불안을 숨기고 있는 남자 초등학생", "VN/Characters/FT001/ft001_child", "center");
        AddVnCharacter("ft001_grandmother", "오선진", "돕고 싶지만 비판적 언어로 개입하는 외조모", "VN/Characters/FT001/ft001_grandmother", "right");
        AddVnCharacter("ft001_teacher", "서건창", "학교 절차와 아이 걱정 사이에 있는 남자 담임교사", "VN/Characters/FT001/ft001_teacher", "right");
        AddVnCharacter("ft002_grandmother", "김선기", "손자를 잃을까 봐 통제로 걱정을 표현하는 친조모", "VN/Characters/FT002/ft002_grandmother", "left");
        AddVnCharacter("ft002_grandson", "박준현", "야간 귀가 지연 뒤에 아버지에 대한 단절감을 숨긴 중2 남학생", "VN/Characters/FT002/ft002_grandson", "center");
        AddVnCharacter("ft002_grandfather", "박석민", "두 사람 사이를 말리기만 하며 갈등을 피해 온 친조부", "VN/Characters/FT002/ft002_grandfather", "right");
        AddVnCharacter("ft003_mother", "어머니", "아이의 지침을 가장 가까이서 감당하는 맞벌이 보호자", "VN/Characters/FT003/ft003_mother", "left");
        AddVnCharacter("ft003_father", "아버지", "치료 일정표와 성과 압박으로 불안을 붙잡는 보호자", "VN/Characters/FT003/ft003_father", "right");
        AddVnCharacter("ft003_child", "자녀", "치료 일정 논쟁 사이에서 결정을 들고 서는 발달지연 아동", "VN/Characters/FT003/ft003_child", "center");
        AddVnCharacter("ft003_coordinator", "기관 담당자", "치료실 권고와 가족 지속 가능성 사이를 조율하는 외부 전문가", "VN/Characters/FT003/ft003_coordinator", "right");
        AddVnCharacter("ft004_caregiver", "어머니", "이민 배경 보호자. 괜찮다는 웃음 뒤에 수치심과 도움 욕구를 접어 둔 어머니", "VN/Characters/FT004/ft004_caregiver", "left");
        AddVnCharacter("ft004_spouse", "아버지", "서류 탈락을 생계 불안과 비난으로 표현하는 아버지", "VN/Characters/FT004/ft004_spouse", "right");
        AddVnCharacter("ft004_child", "자녀", "부모의 웃음과 한숨 사이에서 긴장을 감지하는 아이", "VN/Characters/FT004/ft004_child", "center");
        AddVnCharacter("ft004_institution", "보육기관 담당자", "통번역 지원과 보육 신청 절차를 설명하는 기관 인물", "VN/Characters/FT004/ft004_institution", "right");
        AddVnCharacter("ft005_mother", "어머니", "새아버지와 자녀 사이에서 계속 통역자가 되는 보호자", "VN/Characters/FT005/ft005_mother", "left");
        AddVnCharacter("ft005_stepfather", "새아버지", "가족 안의 자리를 빨리 확인받고 싶어 하는 새 부모", "VN/Characters/FT005/ft005_stepfather", "right");
        AddVnCharacter("ft005_teen", "청소년 자녀", "친아버지 충성심과 새 가족 압박 사이에서 거리를 두는 자녀", "VN/Characters/FT005/ft005_teen", "center");
        AddVnCharacter("ft006_mother", "어머니", "입원 자녀 돌봄과 둘째의 외로움 사이에서 죄책감을 느끼는 보호자", "VN/Characters/FT006/ft006_mother", "left");
        AddVnCharacter("ft006_father", "아버지", "첫째 치료 우선이라는 말로 가족 불안을 견디는 보호자", "VN/Characters/FT006/ft006_father", "right");
        AddVnCharacter("ft006_sibling", "둘째", "괜찮다는 말로 외로움과 죄책감을 숨기는 형제자매", "VN/Characters/FT006/ft006_sibling", "center");
        AddVnCharacter("ft007_father", "아버지", "분노와 도덕 판단으로 실패 불안을 막는 원가족 부모", "VN/Characters/FT007/ft007_father", "right");
        AddVnCharacter("ft007_adult_child", "성인자녀", "경제 의존 뒤에 수치심과 철수를 숨긴 성인자녀", "VN/Characters/FT007/ft007_adult_child", "center");
        AddVnCharacter("ft007_mother", "어머니", "비밀 지원으로 갈등을 잠시 낮추지만 삼각관계를 유지하는 보호자", "VN/Characters/FT007/ft007_mother", "left");
        AddVnCharacter("ft008_teen", "청소년", "학교폭력 이후 침묵과 전학 요구 사이에 갇힌 학생", "VN/Characters/FT008/ft008_teen", "center");
        AddVnCharacter("ft008_mother", "어머니", "다시 꺼내면 더 다칠까 봐 침묵을 택한 보호자", "VN/Characters/FT008/ft008_mother", "left");
        AddVnCharacter("ft008_father", "아버지", "버티기와 도망 담론 사이에서 불안을 표현하는 보호자", "VN/Characters/FT008/ft008_father", "right");
        AddVnCharacter("ft008_teacher", "학교 담당자", "학교 절차와 피해 학생 안전 사이를 설명하는 외부 인물", "VN/Characters/FT008/ft008_teacher", "right");
        AddVnCharacter("ft009_mother", "산후 보호자", "산후 우울과 수면 부족 속에서 도움 요청을 실패로 느끼는 어머니", "VN/Characters/FT009/ft009_mother", "left");
        AddVnCharacter("ft009_spouse", "배우자", "돕고 싶지만 무엇을 해야 할지 모르는 배우자", "VN/Characters/FT009/ft009_spouse", "right");
        AddVnCharacter("ft009_support", "지원 인물", "오늘 밤 실제 돌봄 공백을 메울 수 있는 가족/지인", "VN/Characters/FT009/ft009_support", "center");
        AddVnCharacter("ft010_teen", "누나", "집을 지탱하지만 자기 시간이 사라진 부모화 청소년", "VN/Characters/FT010/ft010_teen", "center");
        AddVnCharacter("ft010_guardian", "보호자", "건강 문제와 죄책감 속에서 돌봄을 나누기 어려운 보호자", "VN/Characters/FT010/ft010_guardian", "left");
        AddVnCharacter("ft010_sibling", "동생", "누나에게 의존하지만 작은 역할을 배울 수 있는 초등학생 동생", "VN/Characters/FT010/ft010_sibling", "right");
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
            scriptKind = "full_case_specific_v1",
            chapter = 1,
            backgroundId = "VN/Backgrounds/counseling_room_day",
            characters = new[] { "ft001_mother", "ft001_child", "ft001_teacher", "ft001_grandmother", "supervisor_system" },
            turns = new List<VnTurn>
            {
                new VnTurn(
                    "초기 합류와 문제 정의",
                    new[]
                    {
                        Ft001Line("t01_l01_mother_neutral", "ft001_mother", "neutral", "left", "아침마다 같은 장면이 반복돼요. 깨우고, 달래고, 결국 제가 소리를 지르고 나면 둘 다 지쳐버립니다.", "처음부터 한 사람을 원인으로 정하지 말고, 반복되는 아침 장면을 함께 보세요."),
                        Ft001Line("t01_l02_child_anxious", "ft001_child", "anxious", "center", "엄마가 일하러 나가면 집이 너무 조용해요. 학교에 가면 괜찮은 척해야 해서 더 힘들어요.", "증상 뒤의 가족 불안 신호가 드러나고 있습니다."),
                        Ft001Line("t01_l03_mother_worried", "ft001_mother", "worried", "left", "저도 일부러 화내는 건 아니에요. 학교에서 또 전화 올까 봐 급해지고, 그러면 주형이가 더 안 움직여요.", "보호자의 다그침도 불안을 낮추려는 반응입니다."),
                        Ft001Line("t01_l04_child_quiet", "ft001_child", "quiet", "center", "엄마가 급해지면 제가 말하면 안 될 것 같아요. 그냥 가만히 있으면 엄마가 조금 더 집에 있어요.", "침묵과 멈춤이 관계 안에서 기능을 갖기 시작합니다."),
                        Ft001Line("t01_l05_teacher_concerned", "ft001_teacher", "concerned", "right", "교실에 오면 조용히 앉아 있습니다. 그런데 지각과 결석이 반복되니 학교도 계속 연락할 수밖에 없습니다.", "학교도 가족 체계 밖의 압력으로 작동합니다."),
                        Ft001Line("t01_l06_supervisor_explaining", "supervisor_system", "explaining", "supervisor", "첫 회기에서는 원인을 단정하지 마세요. 이 가족이 아침마다 어떤 순서로 서로를 밀고 당기는지 먼저 보세요.", "초기 합류와 순환 관찰을 함께 잡아야 합니다.")
                    },
                    new[]
                    {
                        Ft001Choice("t01_reaction_a_mother_softened", "\"오늘 상담에서 각자 꼭 달라졌으면 하는 것과, 걱정되는 것을 한 문장씩 들어보고 싶습니다.\"", "system", "joining", 90, "초기 합류와 순환 관찰이 모두 살아납니다.", "박성빈은 숨을 고르며 고개를 끄덕입니다. 이주형도 상담자가 자신을 문제로만 보지 않는다고 느끼며 작은 목소리로 말을 잇습니다.", "ft001_mother", "softened"),
                        Ft001Choice("t01_reaction_b_child_withdrawn", "\"먼저 주형이가 등교를 거부하는 행동을 멈추는 데 집중해야겠습니다.\"", "cbft", "ip_fixing", 35, "IP를 고정해 가족 전체 패턴을 놓칩니다.", "이주형은 시선을 내리고, 박성빈은 다시 자신이 실패한 보호자처럼 느낍니다.", "ft001_child", "withdrawn"),
                        Ft001Choice("t01_reaction_c_teacher_procedural", "\"학교와 가정에서 각각 어떤 절차와 요구가 있었는지부터 정확히 확인하겠습니다.\"", "procedure", "paperwork", 45, "정보 확인은 필요하지만 합류 전 절차화는 방어를 키웁니다.", "서건창은 대답하지만 가족의 정서는 더 닫힙니다.", "ft001_teacher", "procedural")
                    }),
                new VnTurn(
                    "가족역동 개념화",
                    new[]
                    {
                        Ft001Line("t02_l01_mother_defensive", "ft001_mother", "defensive", "left", "학교에서 전화가 오면 제가 더 다그치게 돼요. 그러면 주형이는 더 굳어버립니다.", "비난이 아니라 순환을 보이게 만들어야 합니다."),
                        Ft001Line("t02_l02_child_quiet", "ft001_child", "quiet", "center", "엄마가 화내면 저는 아무 말도 안 하고 싶어요. 그러면 엄마가 더 화내요.", "아이의 침묵도 상호작용의 일부입니다."),
                        Ft001Line("t02_l03_mother_exhausted", "ft001_mother", "exhausted", "left", "말을 안 하면 저는 더 무섭습니다. 오늘도 못 가면 직장에 또 늦고, 학교에서는 제가 방치하는 것처럼 보일까 봐요.", "박성빈의 행동은 통제 욕구만이 아니라 생계와 평가 불안에 연결됩니다."),
                        Ft001Line("t02_l04_child_hesitant", "ft001_child", "hesitant", "center", "제가 말하면 엄마가 더 힘들까 봐요. 근데 아무 말 안 해도 엄마가 힘들어해서 어떻게 해야 할지 모르겠어요.", "아이도 가족 정서 조절에 참여하고 있습니다."),
                        Ft001Line("t02_l05_teacher_procedural", "ft001_teacher", "procedural", "right", "학교 입장에서는 결석이 누적되면 절차가 필요합니다. 다만 연락할수록 아침 갈등이 커진다면 방식은 조정해볼 수 있습니다.", "기관의 절차도 순환을 강화하거나 완화할 수 있습니다."),
                        Ft001Line("t02_l06_supervisor_explaining", "supervisor_system", "explaining", "supervisor", "지금 핵심은 누가 시작했는지가 아니라, 서로의 반응이 다음 반응을 어떻게 부르는지입니다.", "가족체계 기본 렌즈를 명확히 유지하세요.")
                    },
                    new[]
                    {
                        Ft001Choice("t02_reaction_a_mother_softened", "\"아침 장면을 순서대로 같이 그려볼까요? 주형이가 멈추면 어머니는 무엇을 하고, 그다음 학교 연락은 어떻게 이어지나요?\"", "system", "circular_mapping", 94, "가족체계 개념화가 회기 안에서 보이는 선택입니다.", "박성빈은 '제가 화낼수록 더 굳는 거였네요'라고 말합니다. 이주형은 '제가 멈추면 엄마도 더 급해져요'라고 덧붙이며 패턴을 함께 보기 시작합니다.", "ft001_mother", "softened"),
                        Ft001Choice("t02_reaction_b_mother_defensive", "\"성빈 씨가 조금 더 단호하게 기준을 세우면 아침 혼란이 줄어들 수도 있습니다.\"", "structural", "parent_directive", 52, "구조화 의도는 있으나 현재는 보호자 책임만 강조됩니다.", "박성빈의 어깨가 굳고, 이주형은 상담자가 엄마 편을 든다고 느낍니다.", "ft001_mother", "defensive"),
                        Ft001Choice("t02_reaction_c_child_withdrawn", "\"주형아, 엄마가 더 걱정하지 않도록 네가 조금만 더 움직여볼 수 있을까?\"", "strategic", "pressure", 25, "아이에게 책임을 전가해 회기가 닫힙니다.", "이주형은 대답하지 않고 의자 깊숙이 몸을 넣습니다.", "ft001_child", "withdrawn")
                    }),
                new VnTurn(
                    "감정과 구조 단서 확인",
                    new[]
                    {
                        Ft001Line("t03_l01_grandmother_critical", "ft001_grandmother", "critical", "right", "성빈이가 일을 줄이면 되잖아요. 애가 약해서 그렇지, 집이 안정되면 나아질 겁니다.", "오선진은 비판처럼 말하지만 불안과 걱정도 함께 표현하고 있습니다."),
                        Ft001Line("t03_l02_mother_exhausted", "ft001_mother", "exhausted", "left", "엄마가 그렇게 말하면 제가 가족을 망친 사람 같아요. 그래도 일을 안 할 수는 없잖아요.", "보호자 고립과 세대 간 비난 루프가 드러납니다."),
                        Ft001Line("t03_l03_grandmother_worried", "ft001_grandmother", "worried", "right", "나는 성빈이를 탓하려는 게 아니에요. 밤새 일하고 와서 애랑 싸우는 걸 보면 마음이 철렁합니다.", "비판 아래의 걱정을 분리해서 들을 필요가 있습니다."),
                        Ft001Line("t03_l04_mother_tearful", "ft001_mother", "tearful", "left", "그 걱정이 저한테는 '너는 엄마 노릇을 못 한다'는 말처럼 들려요. 그래서 엄마 앞에서는 더 작아져요.", "세대 간 상호작용이 보호자의 자기비난을 키웁니다."),
                        Ft001Line("t03_l05_child_scared", "ft001_child", "scared", "center", "할머니가 오면 엄마가 더 조용해져요. 그럼 저도 말하면 안 될 것 같아요.", "아이의 증상은 어른들 사이 긴장도 감지합니다."),
                        Ft001Line("t03_l06_supervisor_questioning", "supervisor_system", "questioning", "supervisor", "비난을 바로 고치려 하기보다, 걱정이 어떤 말투로 나오고 그 말투가 누구를 침묵시키는지 확인하세요.", "정서와 구조 단서를 동시에 다룹니다.")
                    },
                    new[]
                    {
                        Ft001Choice("t03_reaction_a_grandmother_softened", "\"선진 씨의 말에는 걱정이 있고, 성빈 씨에게는 그 걱정이 심판처럼 들리는 순간이 있는 것 같습니다.\"", "satir", "emotion_reflection", 84, "정서적 안전감을 높이면서 순환 관찰을 유지합니다.", "오선진의 목소리가 낮아지고, 박성빈은 처음으로 '엄마가 도와주면 좋겠지만 심판받는 느낌은 힘들다'고 말합니다.", "ft001_grandmother", "softened"),
                        Ft001Choice("t03_reaction_b_grandmother_defensive", "\"선진 씨, 그런 비판적인 말투는 지금 바로 고치셔야 합니다.\"", "cbft", "correction", 40, "맞는 방향처럼 보이지만 합류 전 직접 지적은 방어를 키웁니다.", "오선진은 입을 다물고, 박성빈은 더 난처해합니다.", "ft001_grandmother", "defensive"),
                        Ft001Choice("t03_reaction_c_child_hesitant", "\"최근 아침 중에 아주 조금이라도 덜 힘들었던 날이 있었나요? 그때는 무엇이 달랐나요?\"", "solution", "exception", 78, "다음 과제 설계에 좋은 단서입니다.", "이주형이 '엄마가 먼저 깨워주지 않은 날은 덜 무서웠다'고 작은 예외를 꺼냅니다.", "ft001_child", "hesitant")
                    }),
                new VnTurn(
                    "핵심 개입 선택",
                    new[]
                    {
                        Ft001Line("t04_l01_supervisor_questioning", "supervisor_system", "questioning", "supervisor", "이제 개입은 멋진 기법보다 가족이 자기 패턴을 볼 수 있게 돕는 질문이어야 합니다.", "순환질문을 사용할 타이밍입니다."),
                        Ft001Line("t04_l02_mother_worried", "ft001_mother", "worried", "left", "제가 화내지 않으면 학교에서 더 뭐라고 할까 봐 무서워요. 그래서 부드럽게 말하려다가도 결국 재촉하게 됩니다.", "박성빈의 행동도 불안 조절 방식입니다."),
                        Ft001Line("t04_l03_child_quiet", "ft001_child", "quiet", "center", "제가 안 가면 엄마가 집에 조금 더 있어요. 그때는 엄마가 나를 두고 바로 가버리지 않는 것 같아요.", "등교 거부의 기능이 드러납니다."),
                        Ft001Line("t04_l04_mother_listening", "ft001_mother", "listening", "left", "주형아, 네가 학교 가기 싫은 게 나를 붙잡으려는 거였다는 말이야? 저는 그냥 제가 실패한 줄만 알았어요.", "보호자가 행동의 기능을 들으며 자기비난에서 조금 이동합니다."),
                        Ft001Line("t04_l05_child_hesitant", "ft001_child", "hesitant", "center", "엄마를 힘들게 하려던 건 아니에요. 근데 아침에 엄마가 나가려고 하면 배가 아프고 머리가 멍해져요.", "증상 언어가 관계 언어로 번역되고 있습니다."),
                        Ft001Line("t04_l06_supervisor_explaining", "supervisor_system", "explaining", "supervisor", "이 장면에서는 '왜 안 가니'보다 '네가 멈출 때 엄마와 학교가 어떻게 움직이는지'가 더 치료적인 질문입니다.", "개입은 가족이 순환을 직접 보게 해야 합니다.")
                    },
                    new[]
                    {
                        Ft001Choice("t04_reaction_a_supervisor_approving", "\"주형이가 몸이 멈춘다고 말하는 순간, 성빈 씨는 무엇을 하게 되고 학교 연락은 어떻게 달라지나요?\"", "system", "circular_question", 97, "이론과 사례 단서가 가장 잘 맞는 핵심 개입입니다.", "가족은 등교 거부가 누군가의 잘못이 아니라 아침 불안을 묶어두는 방식이었음을 보기 시작합니다. 담임도 연락 방식이 가족의 압박을 키웠을 수 있음을 인정합니다.", "supervisor_system", "approving"),
                        Ft001Choice("t04_reaction_b_mother_anxious", "\"내일부터는 지각을 허용하지 않는 행동계약을 정하고, 가족 모두 그 기준을 지키는 방식으로 가보겠습니다.\"", "cbft", "premature_contract", 50, "행동계약은 가능하지만 현재는 기능 이해보다 통제 강화가 앞섭니다.", "박성빈은 실행 가능성을 걱정하고, 이주형은 다시 몸을 움츠립니다.", "ft001_mother", "anxious"),
                        Ft001Choice("t04_reaction_c_child_withdrawn", "\"지금 양상은 분리불안으로 보입니다. 우선 주형이 개인 치료를 먼저 잡는 것이 좋겠습니다.\"", "procedure", "diagnostic_closure", 30, "교육적으로도 실제 회기적으로도 너무 성급한 판정입니다.", "가족은 답을 들은 듯하지만 서로의 관계 패턴은 그대로 남습니다.", "ft001_child", "withdrawn")
                    }),
                new VnTurn(
                    "다음 주 과제와 복기",
                    new[]
                    {
                        Ft001Line("t05_l01_teacher_concerned", "ft001_teacher", "concerned", "right", "학교에서도 아침 연락 방식을 조정할 수 있다면 해보겠습니다. 가족이 덜 몰리게 하는 게 중요해 보입니다.", "외부 체계도 작은 피드백 루프 조정에 참여할 수 있습니다."),
                        Ft001Line("t05_l02_mother_softened", "ft001_mother", "softened", "left", "제가 혼자 해결해야 한다고 생각해서 더 몰아붙였던 것 같아요. 내일부터는 깨우기 전에 먼저 주형이 상태를 물어보고 싶어요.", "보호자의 자기비난이 패턴 이해로 이동합니다."),
                        Ft001Line("t05_l03_child_relieved", "ft001_child", "relieved", "center", "엄마가 먼저 물어보면 저도 바로 안 된다고만 하지는 않을 것 같아요. 가방은 전날 같이 챙겨볼 수 있어요.", "작은 행동 실험이 가족 안에서 구체화됩니다."),
                        Ft001Line("t05_l04_grandmother_softened", "ft001_grandmother", "softened", "right", "나는 아침에 전화해서 잔소리하기보다, 성빈이가 퇴근하고 잠깐 쉴 시간을 만들어주는 게 낫겠네요.", "외조모의 개입도 비난에서 지원으로 바뀔 수 있습니다."),
                        Ft001Line("t05_l05_teacher_softened", "ft001_teacher", "softened", "right", "학교에서는 첫 연락을 바로 경고처럼 하지 않고, 등교 가능 시간을 확인하는 방식으로 바꿔보겠습니다.", "학교 체계도 순환 완화에 참여합니다."),
                        Ft001Line("t05_l06_supervisor_reflective", "supervisor_system", "reflective", "supervisor", "마지막 선택은 가족이 다음 주 실제로 해볼 수 있는 작고 관찰 가능한 루틴이어야 합니다.", "과제는 작아야 유지됩니다.")
                    },
                    new[]
                    {
                        Ft001Choice("t05_reaction_a_mother_softened", "\"다음 아침에는 성빈 씨, 주형이, 학교가 각각 한 가지씩만 다르게 해보고 무엇이 달라지는지 보겠습니다.\"", "system", "feedback_task", 92, "회기 안 통찰을 다음 주 피드백 루프 실험으로 연결합니다.", "가족은 완벽한 해결보다 다음 아침에 해볼 수 있는 한 가지 변화를 합의합니다. 박성빈은 재촉 전에 질문하기, 이주형은 전날 가방 챙기기, 학교는 첫 연락 문구 바꾸기를 맡습니다.", "ft001_mother", "softened"),
                        Ft001Choice("t05_reaction_b_child_scared", "\"주형이가 다음 주에는 지각하지 않겠다고 지금 여기서 약속하는 게 먼저인 것 같습니다.\"", "strategic", "compliance_promise", 32, "가족 전체 실험이 아니라 아이 한 명의 다짐으로 회기가 좁혀집니다.", "이주형은 고개를 작게 끄덕이지만 표정은 더 굳고, 박성빈은 그 약속이 또 자기 몫이 될까 봐 조심스러워합니다.", "ft001_child", "scared"),
                        Ft001Choice("t05_reaction_c_teacher_procedural", "\"오늘 나온 가족치료 이론명을 각자 정리해오면 다음 시간에 확인하겠습니다.\"", "procedure", "academic_homework", 28, "수련생 학습과 가족 회기 과제를 혼동한 선택입니다.", "가족은 무엇을 해야 할지 알지 못한 채 상담실을 나섭니다.", "ft001_teacher", "procedural")
                    })
            }
        };
        vnScripts[ft001.caseId] = ft001;

        var ft002 = new VnCaseScript
        {
            caseId = "FT-002",
            scriptKind = "full_case_specific_v1",
            chapter = 1,
            backgroundId = "VN/Backgrounds/counseling_room_day",
            characters = new[] { "ft002_grandmother", "ft002_grandson", "ft002_grandfather", "supervisor_bowen" },
            turns = new List<VnTurn>
            {
                new VnTurn(
                    "초기 합류와 문제 정의",
                    new[]
                    {
                        new VnDialogueLine("ft002_grandmother", "anxious", "left", "애가 밤 열한 시가 넘어도 안 들어와요. 그러니 휴대폰이라도 봐야 마음이 놓이죠. 안 그러면 무슨 일이 났나 싶어서요.", "처음부터 한 사람을 탓하지 말고, 걱정이 어떤 모양으로 표현되는지 보세요."),
                        new VnDialogueLine("ft002_grandson", "withdrawn", "center", "집에 들어가면 어차피 또 검사예요. 그럴 거면 늦게 들어가는 게 나아요. 어차피 제 말은 안 믿으시잖아요.", "통제와 도피가 서로를 부르는 신호가 벌써 보입니다."),
                        new VnDialogueLine("ft002_grandfather", "concerned", "right", "저는 둘이 부딪치면 그냥 그만하라고 말리기만 합니다. 누구 편을 들 수도 없고, 시끄러운 게 싫어서요.", "조부가 갈등에서 어떤 자리를 잡고 있는지 기억해 두세요."),
                        new VnDialogueLine("ft002_grandmother", "worried", "left", "제가 이 아이까지 놓치면 안 되잖아요. 그래서 자꾸 확인하게 돼요. 저도 잔소리하고 싶어서 하는 게 아니에요.", "통제 아래 '잃을지도 모른다'는 불안이 깔려 있습니다."),
                        new VnDialogueLine("ft002_grandson", "defensive", "center", "그 얘기는 하지 마세요. 아빠 얘기 나오면 저 그냥 방에 들어갈 거예요.", "특정 주제에서 단절이 작동합니다. 아직 건드리지 마세요."),
                        new VnDialogueLine("supervisor_bowen", "explaining", "supervisor", "첫 회기에는 누가 문제인지 정하지 마세요. 걱정과 통제가 누구를 거쳐 도는지, 그리고 무엇이 금기인지부터 조용히 보세요.", "합류와 다세대 단서 관찰을 함께 잡습니다.")
                    },
                    new[]
                    {
                        new VnChoice("\"오늘 세 분이 각각 가장 걱정되는 것과, 이 시간에 조금이라도 달라졌으면 하는 걸 한 문장씩 듣고 싶습니다.\"", "bowen", "joining", 90, "합류를 유지하면서 각자의 입장을 분리해 듣습니다.", "김선기는 잠시 숨을 고르고, 박준현도 상담자가 자기를 바로 문제아로 보지 않는다고 느껴 조금 자세를 폅니다.", "ft002_grandmother", "softened"),
                        new VnChoice("\"우선 준현이 귀가 시간을 몇 시로 할지부터 분명히 정하는 게 좋겠습니다.\"", "cbft", "ip_fixing", 42, "규칙은 필요하지만 지금은 손자만 문제로 좁혀집니다.", "박준현은 다시 시선을 내리고, 김선기는 거봐라 하는 표정이 되며 갈등이 굳습니다.", "ft002_grandson", "withdrawn"),
                        new VnChoice("\"할머니, 그렇게 매일 휴대폰을 검사하시면 안 됩니다. 그것부터 멈추셔야 해요.\"", "structural", "premature_correction", 33, "방향은 맞지만 합류 전 직접 지적은 방어를 키웁니다.", "김선기는 입을 닫고, 박석민은 더 난처해하며 중재할 말을 찾습니다.", "ft002_grandmother", "defensive")
                    }),
                new VnTurn(
                    "통제와 도피의 순환 보기",
                    new[]
                    {
                        new VnDialogueLine("ft002_grandmother", "defensive", "left", "검사하면 할수록 더 늦게 들어와요. 그걸 알면서도 안 하면 더 불안해서 못 견디겠어요.", "통제가 도피를 키운다는 걸 본인도 어렴풋이 압니다."),
                        new VnDialogueLine("ft002_grandson", "quiet", "center", "검사당하면 그날은 아예 말을 안 해요. 그러면 할머니가 더 캐물어서, 더 입을 닫게 돼요.", "손자의 침묵도 순환의 한 부분입니다."),
                        new VnDialogueLine("ft002_grandfather", "concerned", "right", "그럴 때 저는 그냥 둘 다 진정하라고만 해요. 그러고 나면 아무것도 안 변하고 다음 날 또 똑같습니다.", "중재가 평화를 만드는 듯하지만 변화는 멈춰 있습니다."),
                        new VnDialogueLine("ft002_grandmother", "worried", "left", "솔직히… 이 아이도 제 아빠처럼 어느 날 말없이 사라질까 봐 무서워요. 그래서 더 붙잡게 돼요.", "현재의 통제가 과거의 상실과 연결됩니다."),
                        new VnDialogueLine("ft002_grandson", "defensive", "center", "왜 자꾸 저를 아빠랑 비교해요. 저는 아빠 아니에요.", "세대를 가로지르는 불안이 손자에게 옮겨붙고 있습니다."),
                        new VnDialogueLine("supervisor_bowen", "explaining", "supervisor", "지금 핵심은 누가 먼저냐가 아니라, 확인-침묵-중재가 어떤 순서로 돌고 그 고리가 어디서 더 조여지는지입니다.", "반복되는 고리를 가족이 함께 보게 하세요.")
                    },
                    new[]
                    {
                        new VnChoice("\"그 밤 장면을 순서대로 같이 따라가 볼까요? 할머니가 확인하면 준현이는 무엇을 하게 되고, 그때 할아버지는 어디에 계시나요?\"", "bowen", "circular_mapping", 94, "통제-도피-중재의 고리를 회기 안에서 함께 그립니다.", "김선기는 '내가 캐물을수록 더 입을 닫았던 거네요'라고 말하고, 박준현도 '검사 안 한 날은 그냥 말했어요'라고 덧붙입니다.", "ft002_grandmother", "softened"),
                        new VnChoice("\"할아버지가 좀 더 적극적으로 준현이를 단속해 주시면 균형이 잡힐 것 같습니다.\"", "structural", "shift_burden", 50, "위계를 손보려는 의도지만 조부를 새 통제자로 끌어들여 고리를 키울 수 있습니다.", "박석민은 마지못해 고개를 끄덕이고, 박준현은 이제 두 명한테 감시당한다고 느낍니다.", "ft002_grandson", "withdrawn"),
                        new VnChoice("\"늦게 들어올 때마다 벌칙을 정해서 준현이가 책임을 지게 합시다.\"", "cbft", "control_escalation", 28, "통제를 더 세게 해 도피를 강화하는 선택입니다.", "박준현은 대꾸 없이 의자에 깊이 기대고, 김선기는 잠깐 통쾌해하다 곧 더 불안해합니다.", "ft002_grandson", "defensive")
                    }),
                new VnTurn(
                    "아버지의 빈자리와 세대 간 단서",
                    new[]
                    {
                        new VnDialogueLine("ft002_grandmother", "critical", "left", "제 아들이 애만 두고 떠난 뒤로, 저는 이 손자라도 똑바로 키워야 한다는 생각뿐이에요.", "통제의 뿌리에 아들 상실과 책임감이 있습니다."),
                        new VnDialogueLine("ft002_grandson", "scared", "center", "그러니까 저를 아빠 대신으로 보는 거잖아요. 저까지 잘못되면 안 된다고. 그 눈빛이 제일 싫어요.", "손자는 자신이 '아버지의 대체물'로 보이는 걸 견디기 힘들어합니다."),
                        new VnDialogueLine("ft002_grandfather", "quiet", "right", "이 집에서 아들 얘기는 오랫동안 꺼내면 안 되는 말이었습니다. 저도 그냥 덮어 두는 게 편했고요.", "가족 안의 단절이 침묵으로 유지돼 왔습니다."),
                        new VnDialogueLine("ft002_grandmother", "tearful", "left", "또 잃을까 봐 무서워서 더 붙잡았는데… 붙잡을수록 애가 더 멀어지는 것 같아요.", "상실 불안이 통제를 키우고 통제가 거리를 키웁니다."),
                        new VnDialogueLine("ft002_grandson", "hesitant", "center", "사실 저도 아빠가 왜 안 오는지 아무도 말을 안 해줘서… 그냥 저 혼자 답답한 거예요.", "단절된 주제에 손자의 미해결 감정이 묶여 있습니다."),
                        new VnDialogueLine("supervisor_bowen", "questioning", "supervisor", "통제를 바로 고치려 하기보다, 그 통제 아래 무엇을 잃을까 두려운지, 그리고 누구 이야기가 금기로 묶여 있는지 확인하세요.", "정서적 단서와 다세대 단서를 함께 봅니다.")
                    },
                    new[]
                    {
                        new VnChoice("\"할머니의 단속 속에는 또 잃을지 모른다는 두려움이 있고, 준현이에게는 아빠의 빈자리가 아직 설명되지 않은 채 남아 있는 것 같습니다.\"", "bowen", "loss_reflection", 86, "통제 아래의 상실과 단절된 주제를 안전하게 이름 붙입니다.", "김선기의 어깨가 내려가고, 박준현은 처음으로 '아빠가 밉기도 하고 보고 싶기도 하다'고 작게 말합니다.", "ft002_grandmother", "softened"),
                        new VnChoice("\"최근에 두 분이 평소보다 덜 부딪친 밤이 있었나요? 그날은 무엇이 달랐을까요?\"", "solution", "exception", 78, "다음 과제 설계에 쓸 자원을 찾는 좋은 우회입니다.", "박준현이 '할머니가 묻기 전에 제가 먼저 어디 갔다 왔다고 한 날은 안 싸웠어요'라고 예외를 꺼냅니다.", "ft002_grandson", "hesitant"),
                        new VnChoice("\"아버지가 왜 떠났는지 지금 여기서 자세히 이야기해 봅시다. 거기에 답이 있을 거예요.\"", "psychodynamic", "premature_depth", 38, "안전감이 충분하지 않은 상태에서 단절된 주제를 너무 빨리 파헤칩니다.", "박준현은 말없이 일어나려다 멈추고, 박석민은 그 얘기는 그만하자며 막아섭니다.", "ft002_grandson", "scared")
                    }),
                new VnTurn(
                    "핵심 개입: 자기 입장 세우기",
                    new[]
                    {
                        new VnDialogueLine("supervisor_bowen", "questioning", "supervisor", "이제 준현이가 비난도 도피도 아닌 '자기 입장'을 말하도록, 그리고 할머니가 걱정을 통제가 아닌 자기 말로 옮기도록 도와야 합니다.", "I-position을 끌어낼 타이밍입니다."),
                        new VnDialogueLine("ft002_grandmother", "worried", "left", "제가 안 챙기면 이 집에서 누가 챙기겠어요. 그래서 자꾸 확인하게 되는 거예요.", "통제가 책임감의 언어로 굳어 있습니다."),
                        new VnDialogueLine("ft002_grandson", "hesitant", "center", "늦는 건 집이 싫어서가 아니라… 들어오자마자 추궁당하면 숨 쉴 데가 없어서예요. 어디든 잠깐 더 있고 싶어요.", "도피 행동이 관계 신호로 번역되기 시작합니다."),
                        new VnDialogueLine("ft002_grandmother", "listening", "left", "그래서 늦게 들어왔던 거야? 난 네가 우릴 무시해서 일부러 그러는 줄로만 알았는데.", "조모가 손자의 행동을 처음으로 다르게 듣습니다."),
                        new VnDialogueLine("ft002_grandson", "hesitant", "center", "할머니가 무서운 게 아니라, 저를 볼 때 아빠처럼 될까 봐 불안해하는 그 눈빛이 힘들어요.", "손자가 비난 대신 자기 경험을 말로 꺼냅니다."),
                        new VnDialogueLine("supervisor_bowen", "explaining", "supervisor", "여기서는 '너 때문에'가 아니라 '나는 ~할 때 ~하다'로 각자 자기 입장을 말하게 하는 질문이 가장 치료적입니다.", "분화는 자기 입장에서 시작됩니다.")
                    },
                    new[]
                    {
                        new VnChoice("\"준현이는 '나는 들어올 때 ~하면 ~하다'로, 할머니는 '나는 ~가 걱정돼서 ~한다'로 서로에게 한 문장씩 말해볼까요?\"", "bowen", "i_position", 97, "이론과 사례 단서가 가장 잘 맞는 핵심 개입입니다.", "박준현은 '나는 들어오자마자 캐물으면 숨고 싶어져요'라고, 김선기는 '나는 너를 또 잃을까 봐 확인하게 돼'라고 처음으로 서로에게 직접 말합니다.", "supervisor_bowen", "approving"),
                        new VnChoice("\"오늘 안에 귀가 시간 합의서를 쓰고 세 분이 서명하는 방식으로 정리합시다.\"", "cbft", "premature_contract", 50, "합의서는 가능하지만 지금은 자기 입장 이해보다 통제 정리가 앞섭니다.", "김선기는 종이를 반기지만, 박준현은 또 자기만 묶이는 문서라고 느껴 굳습니다.", "ft002_grandson", "withdrawn"),
                        new VnChoice("\"지금 양상은 준현이의 반항으로 보입니다. 우선 준현이만 따로 개인 상담을 잡는 게 좋겠습니다.\"", "procedure", "diagnostic_closure", 30, "가족 고리를 손자 개인 문제로 닫아 버리는 성급한 판정입니다.", "가족은 답을 들은 듯하지만 조모-손자 사이 거리는 그대로 남습니다.", "ft002_grandson", "scared")
                    }),
                new VnTurn(
                    "작은 실험과 다음 회기",
                    new[]
                    {
                        new VnDialogueLine("ft002_grandfather", "concerned", "right", "저도 매번 말리기만 할 게 아니라, 한 가지 정해진 역할을 맡아 보겠습니다. 그게 나을 것 같아요.", "조부가 회피적 중재에서 한 걸음 나옵니다."),
                        new VnDialogueLine("ft002_grandmother", "softened", "left", "들어오면 휴대폰부터 보는 대신, 딱 한 가지만 물어보기로 해볼게요. '오늘 어땠어' 정도로요.", "통제 행동을 작고 관찰 가능한 변화로 바꿉니다."),
                        new VnDialogueLine("ft002_grandson", "relieved", "center", "들어오자마자 추궁만 안 하면, 저도 어디 갔다 왔는지 먼저 말할 수 있을 것 같아요.", "손자가 작은 호혜적 행동을 제안합니다."),
                        new VnDialogueLine("ft002_grandfather", "softened", "right", "아들 얘기도… 더는 무조건 덮어 두지만 말고, 준현이가 묻고 싶을 때 같이 이야기해 봅시다.", "단절된 주제를 다음 단계로 조심스럽게 엽니다."),
                        new VnDialogueLine("ft002_grandmother", "softened", "left", "잡아두려고 할수록 준현이가 더 피하는 것 같다는 건 알겠어요. 휴대폰부터 보는 건 줄여보고 싶어요.", "통찰이 행동 변화 동기로 이어집니다."),
                        new VnDialogueLine("supervisor_bowen", "reflective", "supervisor", "마지막 선택은 세 사람이 다음 주에 실제로 해볼 수 있는, 작고 확인 가능한 한 가지여야 합니다.", "과제는 작아야 유지됩니다.")
                    },
                    new[]
                    {
                        new VnChoice("\"다음 한 주는 세 분이 각각 한 가지만 다르게 해보고 무엇이 달라지는지 보겠습니다. 할머니는 한 가지만 묻기, 준현이는 귀가하면 한마디 먼저, 할아버지는 말리는 대신 정한 역할 맡기로요.\"", "bowen", "feedback_task", 92, "회기 안 통찰을 다음 주 다세대 실험으로 연결합니다.", "세 사람은 완벽한 해결 대신 각자 해볼 한 가지를 정합니다. 김선기는 한 가지만 묻기, 박준현은 귀가 후 한마디, 박석민은 정해진 역할을 맡기로 합의합니다.", "ft002_grandmother", "softened"),
                        new VnChoice("\"준현이가 다음 주부터는 일찍 들어오겠다고 지금 여기서 약속하는 게 먼저인 것 같습니다.\"", "strategic", "compliance_promise", 32, "가족 전체 실험이 아니라 손자 한 명의 다짐으로 회기가 좁혀집니다.", "박준현은 고개를 작게 끄덕이지만 표정은 굳고, 김선기는 그 약속이 또 자기 감시 거리가 될까 망설입니다.", "ft002_grandson", "scared"),
                        new VnChoice("\"오늘은 여기까지 보고, 다음 주에는 준현이가 약속을 지켰는지만 확인하겠습니다.\"", "procedure", "symptom_check_closure", 28, "회기를 귀가 준수 확인으로 좁혀 다세대 단서와 관계 실험을 놓칩니다.", "박석민은 정리된 것 같다고 말하지만, 김선기와 박준현 사이에는 무엇을 다르게 해볼지 남지 않습니다.", "ft002_grandfather", "concerned")
                    })
            }
        };
        AssignConventionCgPaths(ft002);
        vnScripts[ft002.caseId] = ft002;

        RegisterFocusedVnScripts();

        foreach (var caseData in cases.Where(c => !vnScripts.ContainsKey(c.id)))
        {
            vnScripts[caseData.id] = CreateTrainingVnScript(caseData);
        }
    }

    private void RegisterFocusedVnScripts()
    {
        vnScripts["FT-003"] = BuildFocusedVnScript("FT-003", "supervisor_structural", new[] { "ft003_mother", "ft003_father", "ft003_child", "ft003_coordinator" }, new[]
        {
            T("치료 일정과 부모 하위체계",
                new[]
                {
                    L("ft003_mother", "worried", "left", "현관 앞 파란 치료가방을 제가 늘 챙깁니다. 아이가 끈만 잡고 있으면, 남편은 일정표를 보고 저는 아이 얼굴만 보게 됩니다.", "치료 일정 논쟁을 아이 개인 의지로 좁히지 말고 부모가 함께 기준을 정하는 장면으로 보세요."),
                    L("ft003_father", "defensive", "right", "빠지면 목표가 밀릴까 봐 무섭습니다. 그래서 일정표를 붙잡는데, 그럴수록 아이가 더 작아지는 건 잘 못 봤습니다."),
                    L("supervisor_structural", "explaining", "supervisor", "이정후입니다. 구조적 개입은 누가 가방을 들고 누가 결정을 들고 있는지 보이게 하는 것에서 시작합니다.")
                },
                new[]
                {
                    C("아이에게 묻기 전에 부모님 두 분이 아이가 듣지 않는 자리에서 치료를 지키는 기준과 쉬는 기준을 먼저 정해보겠습니다.", "structural", "parental_alignment", 94, "아이를 결정자 자리에서 내려오게 하고 부모가 함께 기준을 잡도록 돕습니다.", "어머니와 아버지는 처음으로 아이 앞이 아니라 둘 사이에서 기준을 말해야 한다는 점을 받아들입니다.", "ft003_mother", "softened"),
                    C("아이의 몸이 제일 중요하니, 오늘은 아이가 직접 치료를 갈지 말지 말하게 해보겠습니다.", "satir", "child_made_decider", 34, "아이의 목소리를 듣는 것과 결정을 맡기는 것은 다릅니다.", "자녀는 가기 싫다고 하면 엄마가 힘들고, 간다고 하면 자신이 힘들다며 더 얼어붙습니다.", "ft003_child", "scared"),
                    C("기관 권장 기준을 먼저 놓고, 부모님이 그 기준을 어떻게 지킬지 정해보겠습니다.", "procedure", "professional_takeover", 54, "외부 기준은 자료가 될 수 있지만 부모 결정을 대신하면 구조가 약해집니다.", "기관 담당자는 기준은 줄 수 있지만 가족 생활을 대신 판단할 수는 없다고 선을 긋습니다.", "ft003_coordinator", "procedural")
                }),
            T("일정표가 명령이 되는 순간",
                new[]
                {
                    L("ft003_child", "quiet", "center", "아빠가 일정표를 보면 저는 가야 하는 거예요. 엄마가 제 얼굴을 보면 엄마가 울 것 같아요.", "아이가 두 부모의 다른 불안을 동시에 들고 있습니다."),
                    L("ft003_coordinator", "neutral", "right", "치료실에서는 출석만 보이지만, 아이가 들어오기 전부터 지쳐 있는 날이 있습니다."),
                    L("supervisor_structural", "questioning", "supervisor", "일정표를 버리라는 뜻이 아닙니다. 누가 일정표를 들고 누가 몸으로 감당하는지 실연해야 합니다.")
                },
                new[]
                {
                    C("일정표를 가운데 놓고, 부모가 각각 자신이 실제로 감당하는 부분을 말하게 하겠습니다.", "structural", "implementation_burden_seen", 92, "성과 압박과 실행 부담을 부모 하위체계 안으로 돌려놓습니다.", "아버지는 자신이 일정표만 보며 실제 이동 부담을 덜 봤다는 점을 인정합니다.", "ft003_father", "listening"),
                    C("아버지가 출석 원칙을 더 분명히 말하고, 아이가 흔들릴 때도 같은 메시지를 유지해야 합니다.", "cbft", "schedule_as_command", 38, "원칙이 아이의 몸 신호를 지워 버립니다.", "자녀는 힘들어도 가야 하는 거냐며 말수가 줄고, 어머니는 더 방어적으로 변합니다.", "ft003_child", "withdrawn"),
                    C("어머니가 아이 컨디션을 보며 실행을 조정하고, 아버지는 원칙만 확인하는 방식으로 역할을 나누겠습니다.", "procedure", "mother_overfunctioning", 46, "역할 분담처럼 보이지만 어머니의 실행 부담을 고정합니다.", "어머니는 결국 자신이 보고 달래고 조정해야 하는 거냐며 피로감을 드러냅니다.", "ft003_mother", "exhausted")
                }),
            T("보호와 배제의 경계",
                new[]
                {
                    L("ft003_mother", "listening", "left", "아이 상태만 보느라 남편 말은 자꾸 끊게 돼요. 그러다 보니 남편은 점점 입을 안 열어요."),
                    L("ft003_father", "worried", "right", "치료를 말하면 나쁜 사람이 되고, 가만히 있으면 아무것도 안 하는 사람이 됩니다."),
                    L("supervisor_structural", "explaining", "supervisor", "보호를 줄이는 것이 아니라, 보호가 한쪽 부모에게만 맡겨지지 않도록 다시 나눠야 합니다.")
                },
                new[]
                {
                    C("아이의 지침을 보호하면서도 두 부모가 함께 정할 수 있는 휴식 기준과 치료 기준을 찾겠습니다.", "structural", "rest_standard_defined", 95, "보호와 부모 협력을 동시에 살리는 구조적 선택입니다.", "부모는 아이를 사이에 두지 않고 둘이 먼저 기준을 말하는 짧은 실연을 시작합니다.", "ft003_mother", "softened"),
                    C("당분간 어머니가 아이 컨디션을 기준으로 치료 참석을 결정하고, 아버지는 그 결정을 존중해보겠습니다.", "solution", "father_excluded", 42, "아이 보호는 되지만 아버지가 부모 팀 밖으로 밀립니다.", "아버지는 존중하겠다고 말하지만 자신은 또 밖에 서 있는 느낌이라고 말합니다.", "ft003_father", "defensive"),
                    C("아버지가 먼저 아이의 피로를 충분히 인정해야 아이와 어머니가 안심할 수 있습니다.", "satir", "father_blamed", 50, "인정은 필요하지만 한쪽 부모를 심판대에 세우면 부모 하위체계가 더 갈라집니다.", "아버지는 자신이 또 나쁜 사람으로 지정된 것 같다고 말하며 뒤로 물러납니다.", "ft003_father", "withdrawn")
                }),
            T("기관 권고를 자료로 둘 것인가",
                new[]
                {
                    L("ft003_coordinator", "procedural", "right", "권장 일정표는 있습니다. 하지만 이 표는 가족 생활 전체를 대신 판단하지는 못합니다."),
                    L("ft003_child", "hesitant", "center", "선생님들이 정하면 저는 그냥 해야 하는 것 같아요. 조용한데, 제 말은 더 작아져요."),
                    L("supervisor_structural", "questioning", "supervisor", "전문가를 배제할 필요는 없습니다. 다만 외부 기준이 부모 하위체계를 대신하면 구조는 약해집니다.")
                },
                new[]
                {
                    C("기관 권고는 자료로 두고, 최종 문장은 부모님 두 분이 아이 상태와 가족 생활을 보고 정하겠습니다.", "structural", "professional_used_as_resource", 96, "전문가 기준을 부모 결정의 자료로 돌려놓습니다.", "기관 담당자는 부모 기준이 있으면 치료 강도도 조정할 수 있다며 협력합니다.", "ft003_coordinator", "softened"),
                    C("이번 달은 기관 권장 일정을 그대로 따르며 안정성을 보겠습니다.", "procedure", "professional_authority_outsourced", 35, "덜 싸우는 대신 부모 결정권과 아이 목소리가 작아집니다.", "자녀는 선생님들이 정하면 그냥 해야 하는 거냐고 묻고, 부모는 다시 서로를 봅니다.", "ft003_child", "scared"),
                    C("어머니가 기관 담당자에게 아이 상태를 자세히 설명하고, 그 조정안을 가족이 따르겠습니다.", "structural", "mother_overfunctioning", 44, "기관과 어머니가 직접 연결되고 아버지가 밖에 서는 구조가 됩니다.", "어머니는 설명할 수는 있지만 또 자신이 다 조정하는 사람이 되는 것 같다고 말합니다.", "ft003_mother", "exhausted")
                }),
            T("다음 주 구조 실험",
                new[]
                {
                    L("ft003_mother", "softened", "left", "치료 준비를 저 혼자 다 챙기려 했던 것 같아요. 다음 치료 전에는 남편이랑 먼저 얘기해보고 싶어요."),
                    L("ft003_father", "softened", "right", "일정표를 지우자는 게 아니라, 제가 실제로 맡을 행동을 정해야 한다는 말로 들었습니다."),
                    L("supervisor_structural", "reflective", "supervisor", "마지막 과제는 부모가 먼저 정하고 아이에게는 선택지가 아니라 예고와 안전감을 주는 구조여야 합니다.")
                },
                new[]
                {
                    C("다음 치료 전날 부모가 먼저 10분 회의하고, 아이에게는 가는 기준과 쉬는 기준을 부모 공동 문장으로 알려주겠습니다.", "structural", "home_practice_parent_team", 94, "부모 하위체계 회복을 다음 주 행동으로 연결합니다.", "부모는 아이 앞에서 협상하지 않고 먼저 짧게 회의해보기로 합의합니다.", "ft003_mother", "softened"),
                    C("아이가 치료를 거부하면 그날은 쉬되, 다음 날 보충 일정을 반드시 잡겠습니다.", "cbft", "child_decision_hidden", 45, "아이 결정을 존중하는 듯하지만 부모 기준이 여전히 흐립니다.", "자녀는 또 자신이 거절하면 일정이 꼬이는 사람처럼 느끼며 고개를 숙입니다.", "ft003_child", "withdrawn"),
                    C("기관 담당자가 다음 주까지 적정 출석 기준을 다시 보내주면 가족은 그대로 따르겠습니다.", "procedure", "outsourced_closure", 32, "회기는 정리되지만 부모 팀은 생기지 않습니다.", "기관 담당자는 자료는 보내겠지만 가족 기준이 필요하다고 다시 말합니다.", "ft003_coordinator", "procedural")
                })
        });

        vnScripts["FT-004"] = BuildFocusedVnScript("FT-004", "supervisor_satir", new[] { "ft004_caregiver", "ft004_spouse", "ft004_child", "ft004_institution" }, new[]
        {
            T("괜찮다는 말 아래",
                new[] { L("ft004_caregiver", "neutral", "left", "보육 신청 안내문을 접어서 가방에 넣었습니다. 빨간 표시가 보이면 제가 또 틀린 사람 같아서요.", "괜찮다는 말 아래의 수치심과 도움 욕구를 보세요."), L("ft004_spouse", "defensive", "right", "저는 그 웃음을 보고 괜찮은 줄 알았습니다. 그런데 또 서류가 빠지면 생활이 흔들릴까 봐 화가 납니다."), L("supervisor_satir", "explaining", "supervisor", "김연주입니다. 회유를 거짓말로 보지 말고 가족을 조용하게 만드는 생존 방식으로 보세요.") },
                new[] { C("괜찮다는 말 아래에서 가장 큰 마음이 수치심인지 두려움인지 외로움인지 들어보겠습니다.", "satir", "iceberg_named", 94, "빙산 아래 감정을 안전하게 이름 붙입니다.", "보호자는 종이 하나도 못 읽는 사람처럼 느껴져 부끄러웠다고 처음 말합니다.", "ft004_caregiver", "softened"), C("배우자분이 앞으로 기관 전화를 대신 맡으면 부담이 줄어들 수 있습니다.", "procedure", "institution_as_only_solution", 46, "대신해주는 도움은 가능하지만 보호자의 목소리가 사라집니다.", "보호자는 편할 것 같지만 또 모르는 사람으로 남을 것 같다고 말합니다.", "ft004_caregiver", "worried"), C("보호자분이 전화 표현을 더 연습하면 다음 신청에서는 덜 막힐 수 있습니다.", "cbft", "placating_accepted", 38, "수치심을 개인 노력 문제로 되돌립니다.", "보호자는 네, 제가 더 연습하겠다며 다시 미안하다고 말합니다.", "ft004_caregiver", "withdrawn") }),
            T("체크리스트 뒤의 같은 미소",
                new[] { L("ft004_caregiver", "neutral", "left", "체크리스트를 만들면 할 수 있습니다. 그런데 그렇게 말하면 집이 조용해져서 자꾸 제가 더 하겠다고 말하게 됩니다."), L("ft004_child", "quiet", "center", "엄마가 미안하다고 하면 아빠 목소리가 작아져요. 그런데 엄마는 설거지할 때 한숨을 쉬어요."), L("supervisor_satir", "questioning", "supervisor", "절차는 필요하지만, 체크리스트가 보호자를 혼자 책임지는 사람으로 만들면 회유가 강화됩니다.") },
                new[] { C("신청을 다시 하는 것과 동시에 누가 옆에서 같이 확인할지도 정해야겠습니다.", "satir", "checklist_connected_to_help", 92, "절차와 도움 요청을 연결해 보호자 혼자 책임지는 구조를 줄입니다.", "배우자는 목록만 만들면 된다고 생각했는데 같이 확인할 사람이 필요했다는 점을 인정합니다.", "ft004_spouse", "listening"), C("이번에는 보호자분이 실수하지 않도록 더 자세한 체크리스트를 만들겠습니다.", "procedure", "caregiver_alone_responsible", 36, "정돈되어 보이지만 보호자에게 모든 책임을 다시 돌립니다.", "자녀는 엄마가 또 혼자 하는 거냐고 묻고, 보호자는 다시 웃으며 괜찮다고 말합니다.", "ft004_child", "worried"), C("배우자분이 화내지 않겠다고 약속하면 보호자분도 덜 긴장할 수 있습니다.", "strategic", "emotion_bypassed", 48, "화를 금지하면 잠시 조용하지만 두려움과 부탁은 말해지지 않습니다.", "배우자는 화내지 않겠다고 할 수는 있지만 왜 화나는지도 말하지 않으면 또 커진다고 말합니다.", "ft004_spouse", "defensive") }),
            T("빨간 표시가 증거가 될 때",
                new[] { L("ft004_spouse", "defensive", "right", "빨간 표시를 보면 또 밀릴까 봐 무섭습니다. 그런데 화가 먼저 나와서 이 사람이 잘못한 증거처럼 들고 있게 됩니다."), L("ft004_caregiver", "tearful", "left", "그 말을 들으면 제가 가족을 망치는 사람처럼 느껴집니다. 그래서 먼저 죄송하다고 합니다."), L("supervisor_satir", "explaining", "supervisor", "비난 아래 배우자의 두려움, 회유 아래 보호자의 수치심을 둘 다 들어야 합니다.") },
                new[] { C("배우자분은 무엇이 무서운지, 보호자분은 무엇이 부끄러운지 한 문장씩 말해보겠습니다.", "satir", "congruent_pair_statement", 96, "두 사람의 빙산을 동시에 언어화합니다.", "배우자는 생활이 계속 불안정해질까 봐 무섭다고 말하고, 보호자는 도움을 달라고 하면 더 부끄러워질까 봐 두렵다고 말합니다.", "ft004_spouse", "softened"), C("배우자분은 억울하더라도 지금은 보호자분에게 사과부터 하셔야 합니다.", "satir", "blame_reversed", 42, "한쪽을 가해자로만 세우면 다른 쪽 불안이 사라집니다.", "배우자는 사과하라는 말이 맞으면서도 자신의 불안은 또 말할 곳이 없다고 느낍니다.", "ft004_spouse", "withdrawn"), C("보호자분이 더 명확히 준비하지 못한 부분을 인정해야 다음 신청 준비로 갈 수 있습니다.", "procedure", "placating_deepened", 28, "보호자가 다시 모든 책임을 가져가며 회유가 강화됩니다.", "보호자는 네, 제가 더 확인했어야 했다며 작게 웃고 고개를 숙입니다.", "ft004_caregiver", "withdrawn") }),
            T("스피커폰과 서류봉투",
                new[] { L("ft004_institution", "procedural", "right", "통번역 예약이랑 보완 서류는 제가 정리해드릴 수 있어요. 다만 전화가 시작되면 어디서 막히는지는 아버지도 옆에서 같이 봐주셔야 해요."), L("ft004_caregiver", "worried", "left", "서류봉투 안에 뭐가 있는지는 알겠는데, 스피커폰으로 켜지면 목소리가 너무 빨리 지나갑니다."), L("supervisor_satir", "questioning", "supervisor", "기관 도움과 가족 안의 목소리 연습을 둘 다 살려야 합니다.") },
                new[] { C("통번역 예약은 기관과 잡고, 집에서는 배우자와 함께 전화 첫 문장을 연습하겠습니다.", "satir", "voice_practiced_with_support", 94, "제도 지원과 일치적 의사소통을 함께 연결합니다.", "보호자는 혼자 해내라는 말이 아니라 같이 시작한다는 말이면 시도할 수 있다고 말합니다.", "ft004_caregiver", "softened"), C("기관 담당자가 모든 신청 전화를 대신 처리하도록 연결하겠습니다.", "procedure", "voice_removed", 40, "절차는 정리되지만 보호자의 목소리는 더 작아집니다.", "보호자는 고맙지만 다음에도 혼자 전화해야 할 때가 오면 다시 막힐 것 같다고 말합니다.", "ft004_caregiver", "worried"), C("가족 갈등을 줄이기 위해 신청 이야기는 기관에서만 다루겠습니다.", "strategic", "family_bypass", 34, "가족 안의 수치심과 도움 요청은 그대로 남습니다.", "배우자는 집에서는 조용해지겠지만 다음 빨간 표시가 오면 다시 터질 것 같다고 말합니다.", "ft004_spouse", "defensive") }),
            T("다음 신청을 함께 여는 법",
                new[] { L("ft004_caregiver", "softened", "left", "제가 못 하는 사람이 아니라, 같이 확인해야 하는 일이었다고 말하면 조금 숨이 쉽니다."), L("ft004_spouse", "softened", "right", "괜찮다는 말을 그대로 믿기 전에, 무섭거나 부끄러운 게 있는지 물어보겠습니다."), L("supervisor_satir", "reflective", "supervisor", "마무리는 더 완벽한 체크리스트가 아니라, 보호자가 자기 목소리로 도움을 요청할 수 있는 작은 장면이어야 합니다.") },
                new[] { C("다음 신청 전 보호자는 첫 도움 요청 문장을 말하고, 배우자는 서류봉투를 같이 확인하고, 기관은 통번역 예약을 확인하겠습니다.", "satir", "congruent_help_plan", 95, "빙산 탐색을 실제 도움 요청과 절차 지원으로 연결합니다.", "가족은 완벽한 해결보다 다음 신청을 함께 열 수 있는 작은 역할을 나눕니다.", "ft004_caregiver", "softened"), C("다음에는 보호자가 체크리스트를 매일 확인하고 배우자에게 완료 여부만 보고하겠습니다.", "cbft", "compliance_trap", 36, "깔끔하지만 보호자를 다시 혼자 책임지는 사람으로 만듭니다.", "보호자는 할 수 있다고 말하지만 웃음이 다시 굳고, 자녀는 엄마가 또 혼자 한다고 말합니다.", "ft004_child", "worried"), C("기관 담당자가 신청 전날 가족에게 준비 상황을 전화로 확인하도록 하겠습니다.", "procedure", "institution_final_fix", 48, "도움은 되지만 가족 안의 목소리와 요청 연습이 빠집니다.", "기관 담당자는 확인 전화는 가능하지만 가족이 함께 확인하는 문장도 필요하다고 말합니다.", "ft004_institution", "procedural") })
        });

        vnScripts["FT-005"] = BuildFocusedVnScript("FT-005", "supervisor_structural", new[] { "ft005_mother", "ft005_stepfather", "ft005_teen" }, new[]
        {
            T("빈 식탁 의자",
                new[] { L("ft005_mother", "worried", "left", "아이가 방으로 들어가면 저는 그 의자 뒤에 서서 남편에게는 시간이 필요하다고, 아이에게는 그래도 대답은 해야 한다고 말합니다.", "어머니의 중재 과부하와 빈 의자의 의미를 보세요."), L("ft005_stepfather", "defensive", "right", "그 빈 의자를 보면 제가 이 집에서 아무 자리가 없는 사람처럼 느껴집니다."), L("supervisor_structural", "explaining", "supervisor", "권한보다 먼저 빈 의자를 어떻게 견딜지 정해야 합니다.") },
                new[] { C("새아버지는 짧은 관심 질문 하나만 하고, 자녀는 대답 길이를 정하고, 어머니는 대신 설명하지 않는 30초 장면을 만들어보겠습니다.", "structural", "staged_contact", 95, "작은 직접 접촉과 어머니의 통역 중단을 동시에 실연합니다.", "청소년은 길게 대답하라고 하지 않으면 한마디 정도는 할 수 있을지도 모른다고 말합니다.", "ft005_teen", "hesitant"), C("한집에 사는 기본 인사는 필요하니, 인사와 짧은 대답 규칙부터 정하겠습니다.", "cbft", "respect_rule_imposed", 38, "규칙이 너무 빨리 오면 빈 의자는 더 멀어집니다.", "청소년은 결국 자신이 예의 없는 애라는 말 아니냐며 더 말하기 싫다고 합니다.", "ft005_teen", "defensive"), C("자녀가 부담을 느끼니 당분간 새아버지는 식사 자리에서 질문하지 않는 것으로 하겠습니다.", "solution", "stepfather_excluded", 42, "부담은 줄지만 관계 행동도 사라집니다.", "새아버지는 그럼 자신은 또 아무것도 하지 않는 사람이 되는 거냐고 묻습니다.", "ft005_stepfather", "withdrawn") }),
            T("인사가 권한 시험이 될 때",
                new[] { L("ft005_stepfather", "defensive", "right", "다녀왔습니다 한마디를 바란 겁니다. 그런데 그게 안 되면 제가 아무 권한도 없는 사람처럼 느껴집니다."), L("ft005_teen", "quiet", "center", "인사를 하면 그다음엔 아빠처럼 대하라는 말이 올 것 같아서 싫어요."), L("supervisor_structural", "questioning", "supervisor", "인사는 예의 이전에 권한과 충성심의 시험이 되고 있습니다.") },
                new[] { C("오늘은 훈육 권한이 아니라, 새아버지가 관계 행동 하나를 안전하게 시작하는 장면으로 되돌리겠습니다.", "structural", "authority_speed_acknowledged", 93, "권한 과속을 인정하고 관계 속도를 낮춥니다.", "새아버지는 아버지 자리를 빼앗으려던 건 아니라며 질문 하나부터 다시 해보겠다고 말합니다.", "ft005_stepfather", "softened"), C("자녀가 새아버지를 가족 어른으로 인정하려면 인사와 기본 대답은 반드시 하기로 정하겠습니다.", "structural", "authority_push", 36, "인정 요구가 앞서면 충성심 갈등이 더 커집니다.", "청소년은 빨리 인정하라고 하면 더 싫어진다며 몸을 돌립니다.", "ft005_teen", "withdrawn"), C("어머니가 자녀에게 새아버지의 자리를 분명히 설명하고 규칙을 지키도록 도와야겠습니다.", "procedure", "mother_bridge_reinforced", 44, "어머니를 다시 중재자 자리로 밀어 넣습니다.", "어머니는 또 자신이 둘 사이를 설명해야 하냐며 지친 표정을 보입니다.", "ft005_mother", "exhausted") }),
            T("친아버지를 지우지 않는 자리",
                new[] { L("ft005_teen", "hesitant", "center", "새아버지랑 잘 지내면 아빠를 버리는 것 같아요. 좋은 사람이라는 말은 아니지만 그래도 제 아빠잖아요."), L("ft005_mother", "worried", "left", "그 말을 들으면 제가 나쁜 엄마가 된 것 같아서 그 얘기를 피했습니다."), L("supervisor_structural", "explaining", "supervisor", "자녀의 충성심은 보호되어야 하지만 새아버지가 완전히 밖에 서 있으면 가족은 둘로 갈라집니다.") },
                new[] { C("친아버지를 지우지 않아도 새아버지와의 관계를 천천히 만들 수 있다는 기준을 세우겠습니다.", "structural", "loyalty_named", 94, "두 충성심이 동시에 존재할 수 있게 합니다.", "청소년은 새아버지가 아빠 자리를 달라고 하지 않는다면 엄마 남편으로 시작하는 건 생각해볼 수 있다고 말합니다.", "ft005_teen", "softened"), C("자녀가 안전해질 때까지 새아버지는 한발 물러서고 어머니와 자녀 관계를 먼저 안정시키겠습니다.", "satir", "stepfather_excluded", 45, "자녀 보호는 되지만 새아버지가 가족 밖에 고정됩니다.", "새아버지는 존중하겠지만 자신이 가족 밖에 서 있는 느낌은 더 커진다고 말합니다.", "ft005_stepfather", "withdrawn"), C("어머니가 자녀에게 새아버지도 가족이라는 점을 더 분명히 말해야 합니다.", "procedure", "premature_parent_claim", 34, "가족 인정 요구가 충성심 갈등을 압박합니다.", "청소년은 엄마가 그 말을 할수록 친아버지를 지우라는 뜻처럼 들린다고 말합니다.", "ft005_teen", "defensive") }),
            T("어머니가 의자 뒤에 서 있을 때",
                new[] { L("ft005_mother", "exhausted", "left", "제가 빠지면 바로 싸울 것 같아서 무섭습니다. 하지만 계속 서 있는 것도 너무 힘듭니다."), L("ft005_stepfather", "worried", "right", "아내를 통해서만 아이를 만나는 것 같습니다. 직접 말하면 싸움이 될까 봐 또 아내를 봅니다."), L("supervisor_structural", "questioning", "supervisor", "중재를 없애자는 것이 아니라 30초만 옆으로 물러나는 실험이 필요합니다.") },
                new[] { C("어머니가 통역하지 않는 30초 대화를 실험하겠습니다. 새아버지는 한 문장만 말하고 자녀는 답하거나 답하지 않을 수 있습니다.", "structural", "direct_30sec_contact", 96, "어머니의 다리 역할을 잠시 줄이고 직접 접촉을 만듭니다.", "어머니는 끼어들고 싶지만 참아보겠다고 하고, 청소년은 짧게 대답합니다.", "ft005_mother", "listening"), C("아직 직접 대화는 위험하니 어머니가 계속 중간에서 정리하되 말투를 부드럽게 바꿔보겠습니다.", "satir", "mediation_burden", 40, "갈등은 줄어도 구조는 그대로입니다.", "어머니는 말투를 바꾸는 것도 결국 자기 몫이라며 지친다고 말합니다.", "ft005_mother", "exhausted"), C("새아버지가 느끼는 서운함을 어머니가 자녀에게 대신 전달해보겠습니다.", "psychodynamic", "triangulation_reinforced", 32, "어머니를 삼각관계 중심에 더 고정합니다.", "청소년은 왜 엄마가 또 대신 말하냐며 새아버지를 보지 않습니다.", "ft005_teen", "withdrawn") }),
            T("작은 관계 행동으로 마무리하기",
                new[] { L("ft005_stepfather", "softened", "right", "당장 아빠처럼 굴려고 하진 않을게요. 밥 먹을 때 그냥 '오늘 어땠어?' 한마디만 해볼게요."), L("ft005_teen", "softened", "center", "길게 말하라고 안 하면 한마디 정도는 할 수 있어요. 대신 엄마가 대신 설명하지 않았으면 좋겠어요."), L("supervisor_structural", "reflective", "supervisor", "다음 주 과제는 존중 규칙이 아니라 작은 직접 접촉과 어머니의 자리 이동입니다.") },
                new[] { C("식사 때 새아버지는 관심 질문 하나, 자녀는 대답 길이 선택, 어머니는 30초 관찰자로 머무는 실험을 하겠습니다.", "structural", "home_practice_boundary", 95, "새 가족 구조를 작은 행동으로 재배열합니다.", "세 사람은 완전한 가족 선언 대신 30초 장면부터 해보기로 합의합니다.", "ft005_mother", "softened"), C("다음 주부터 인사 규칙을 지키고 어기면 가족회의에서 확인하겠습니다.", "cbft", "respect_rule_final", 38, "예의 규칙이 관계 속도보다 앞섭니다.", "청소년은 또 규칙으로 자신을 고치려 한다고 느끼며 입을 닫습니다.", "ft005_teen", "withdrawn"), C("새아버지는 한동안 질문하지 않고 어머니가 상황을 전달하도록 하겠습니다.", "procedure", "avoidant_closure", 30, "불편함은 줄지만 관계 경험이 생기지 않습니다.", "새아버지는 더 멀어진 것 같다고 말하고, 어머니는 다시 중간에 남습니다.", "ft005_stepfather", "withdrawn") })
        });

        RegisterFocusedVnScriptsPartTwo();
    }

    private void RegisterFocusedVnScriptsPartTwo()
    {
        vnScripts["FT-006"] = BuildFocusedVnScript("FT-006", "supervisor_satir", new[] { "ft006_mother", "ft006_father", "ft006_sibling" }, new[]
        {
            T("괜찮은 둘째",
                new[] { L("ft006_sibling", "neutral", "center", "저는 괜찮아요. 병원에 있는 언니가 더 힘들잖아요. 제가 힘들다고 하면 엄마가 더 힘들어질 것 같아요.", "괜찮다는 말이 성숙함인지 정서 억압인지 함께 봐야 합니다."), L("ft006_mother", "worried", "left", "그 말이 고맙지만 가끔 너무 어른 같아서 무섭습니다. 그래도 병원 일정이 밀리면 제가 또 첫째 쪽으로 가게 됩니다."), L("supervisor_satir", "explaining", "supervisor", "김연주입니다. 아픈 아이를 탓하지 않으면서도 둘째의 외로움이 사라지지 않게 해야 합니다.") },
                new[] { C("둘째가 괜찮다고 말할 때, 고마움과 외로움이 동시에 있을 수 있는지 물어보겠습니다.", "satir", "two_feelings_named", 94, "두 감정 공존을 허용해 둘째가 부모를 돌보는 자리에서 내려올 수 있습니다.", "둘째는 언니가 걱정되지만 자신도 가끔 누가 물어봐 줬으면 좋겠다고 말합니다.", "ft006_sibling", "softened"), C("첫째 치료가 우선이니 둘째가 조금만 더 이해해줄 수 있도록 설명하겠습니다.", "procedure", "illness_totalizing|cheerful_mask_reinforced", 30, "질병 중심 구조를 더 강화하고 둘째의 목소리를 지웁니다.", "둘째는 고개를 끄덕이지만 표정은 더 굳고, 어머니는 미안하다는 말만 반복합니다.", "ft006_sibling", "withdrawn"), C("부모님이 둘째에게 미안하다고 충분히 말하면 외로움이 줄어들 수 있습니다.", "satir", "guilt_flood", 45, "사과가 과하면 둘째가 부모를 위로하는 자리로 올라갑니다.", "둘째는 엄마 울지 말라며 오히려 어머니를 달래기 시작합니다.", "ft006_sibling", "worried") }),
            T("가족조각으로 보는 자리",
                new[] { L("ft006_father", "defensive", "right", "지금은 첫째 치료가 우선이라고 말하면 제가 차가운 사람처럼 보입니다. 하지만 안 그러면 무너질 것 같습니다."), L("ft006_sibling", "quiet", "center", "병원 얘기가 시작되면 저는 뒤에 서 있는 느낌이에요. 그래도 앞으로 나오면 나쁜 동생 같아요."), L("supervisor_satir", "questioning", "supervisor", "가족조각은 누가 중심인지 비난하려는 도구가 아니라, 사라진 자리를 보이게 하는 도구입니다.") },
                new[] { C("의자 몇 개를 놓고 첫째 병원 자리, 부모 자리, 둘째 자리가 어떻게 보이는지 함께 보겠습니다.", "satir", "family_sculpture", 96, "보이지 않던 둘째의 자리를 가족이 눈으로 확인합니다.", "부모는 둘째가 뒤에 서 있었다는 말을 듣고 죄책감보다 위치를 먼저 보려 합니다.", "ft006_mother", "listening"), C("첫째 이야기를 잠시 멈추고 둘째가 원하는 걸 먼저 말하게 하겠습니다.", "solution", "sibling_centered_only", 50, "둘째를 보려는 의도는 좋지만 병든 자녀를 밀어내는 방식으로 들릴 수 있습니다.", "둘째는 자신 때문에 언니 이야기가 밀리는 것 같아 더 조심스러워합니다.", "ft006_sibling", "hesitant"), C("부모님의 죄책감을 줄이기 위해 현재 최선을 다했다는 점부터 확인하겠습니다.", "satir", "parent_guilt_centered", 42, "부모 안정은 필요하지만 둘째가 다시 부모를 돌보게 됩니다.", "어머니는 미안하다고 울고, 둘째는 괜찮다며 어머니를 달랩니다.", "ft006_mother", "tearful") }),
            T("두 감정을 동시에 말하기",
                new[] { L("ft006_sibling", "hesitant", "center", "언니가 아픈 건 싫고 걱정돼요. 그런데 엄마 아빠가 병원에 가면 저도 집에서 혼자인 게 싫어요."), L("ft006_mother", "tearful", "left", "그 말을 들으니 제가 너무 미안해서 또 사과만 하고 싶어집니다."), L("supervisor_satir", "explaining", "supervisor", "사과는 필요하지만, 사과가 둘째의 감정을 다시 부모 위로로 바꾸지 않게 조절하세요.") },
                new[] { C("부모님은 바로 사과하기보다, 둘째의 걱정과 외로움을 각각 한 문장씩 그대로 반영해보겠습니다.", "satir", "emotion_reflection_balanced", 95, "부모 죄책감 범람을 막고 둘째 감정을 둘 다 인정합니다.", "둘째는 처음으로 걱정과 서운함을 같이 말해도 되는 것 같다고 합니다.", "ft006_sibling", "softened"), C("어머니가 둘째에게 지금까지 미안했던 일을 충분히 말하고 안아주겠습니다.", "satir", "guilt_flood", 44, "따뜻하지만 부모 죄책감이 중심이 되어 둘째가 다시 어른 역할을 합니다.", "둘째는 울지 말라며 어머니를 달래고 자신의 이야기를 멈춥니다.", "ft006_sibling", "worried"), C("둘째가 힘들 때마다 조부모에게 연락하도록 안전망을 정하겠습니다.", "procedure", "outsourced_emotion", 48, "지원망은 필요하지만 부모와 둘째의 정서 대화가 우회됩니다.", "둘째는 연락할 사람은 생겼지만 엄마 아빠가 자신의 마음을 아는지는 모르겠다고 말합니다.", "ft006_sibling", "quiet") }),
            T("10분 의식",
                new[] { L("ft006_father", "listening", "right", "저는 해결책을 크게 생각했습니다. 그런데 매일 10분이라도 둘째 얘기를 묻는 게 더 현실적일 수도 있겠습니다."), L("ft006_sibling", "softened", "center", "언니 이야기를 안 하는 시간이 아니라, 제 이야기도 있는 시간이면 좋겠어요."), L("supervisor_satir", "questioning", "supervisor", "작은 의식은 질병을 지우지 않고도 둘째의 자리를 회복하게 합니다.") },
                new[] { C("매일 병원 이야기와 별도로 둘째의 하루를 묻는 10분 의식을 정하겠습니다.", "satir", "ten_minute_ritual", 96, "둘째 자리를 일상 구조 안에 작게 복원합니다.", "부모와 둘째는 병원 이야기를 없애지 않고도 둘째 시간이 생길 수 있다는 데 합의합니다.", "ft006_sibling", "relieved"), C("첫째 치료가 안정될 때까지 둘째와의 시간은 주말에 몰아서 보상하겠습니다.", "procedure", "delayed_sibling_need", 36, "지금 필요한 자리를 미래 보상으로 미룹니다.", "둘째는 또 기다리라는 말처럼 들린다며 조용해집니다.", "ft006_sibling", "withdrawn"), C("둘째가 힘든 날에는 부모에게 바로 말하겠다고 약속하게 하겠습니다.", "cbft", "sibling_responsible_for_signal", 42, "신호를 말하는 책임이 다시 둘째에게만 갑니다.", "둘째는 말하겠다고 하지만, 바쁠 때 말하면 방해가 될 것 같다고 덧붙입니다.", "ft006_sibling", "hesitant") }),
            T("가족이 지킬 작은 자리",
                new[] { L("ft006_mother", "softened", "left", "미안하다는 말만 하지 않고, 오늘 하루를 묻는 시간을 실제로 만들겠습니다."), L("ft006_father", "softened", "right", "첫째 병원 가는 걸 줄이자는 말은 아니네요. 둘째랑 얘기하는 시간을 달력에 아예 넣어보자는 거죠."), L("supervisor_satir", "reflective", "supervisor", "마지막 과제는 죄책감이 아니라 유지 가능한 가족 의식이어야 합니다.") },
                new[] { C("다음 주까지 부모는 매일 10분 둘째 시간, 둘째는 그 시간에 좋았던 일과 싫었던 일을 하나씩 말해보겠습니다.", "satir", "home_ritual", 94, "빙산 탐색을 지속 가능한 가족 의식으로 연결합니다.", "가족은 첫째를 중심에서 밀어내지 않고 둘째 자리도 놓치지 않는 작은 약속을 정합니다.", "ft006_mother", "softened"), C("부모가 둘째에게 미안했던 점을 편지로 써서 전달하겠습니다.", "satir", "apology_without_structure", 45, "정서는 담기지만 일상 구조가 바뀌지 않습니다.", "둘째는 편지는 고맙지만 내일도 혼자일까 봐 걱정된다고 말합니다.", "ft006_sibling", "worried"), C("조부모가 둘째 돌봄을 더 맡도록 일정을 조정하겠습니다.", "procedure", "support_without_parent_connection", 48, "지원은 필요하지만 부모-둘째 정서 연결이 빠졌습니다.", "둘째는 돌봐줄 사람은 생기지만 부모가 자신을 보는지는 아직 모르겠다고 말합니다.", "ft006_sibling", "quiet") })
        });

        vnScripts["FT-007"] = BuildFocusedVnScript("FT-007", "supervisor_psychodynamic", new[] { "ft007_father", "ft007_adult_child", "ft007_mother" }, new[]
        {
            T("닫힌 방문과 구직 서류", new[] { L("ft007_father", "critical", "right", "방문이 닫혀 있으면 저는 제가 실패한 아버지처럼 느껴집니다. 그래서 게으르다는 말이 먼저 나갑니다.", "도덕 판단 아래에는 수치심과 서로에게 떠넘겨진 두려움이 있습니다."), L("ft007_adult_child", "withdrawn", "center", "아버지는 제가 뭘 해도 실패자라고 생각하잖아요. 그래서 구직 서류도 숨기게 됩니다."), L("supervisor_psychodynamic", "explaining", "supervisor", "송성문입니다. 맞는 해석도 너무 이르면 공격이 됩니다. 먼저 방어 순서를 보세요.") },
                new[] { C("게으름이라는 말이 나오기 전, 아버지 안에서 어떤 실패감이 올라오는지부터 천천히 묻겠습니다.", "psychodynamic", "shame_named", 94, "공격 뒤의 수치심을 해석하기 전에 안전하게 접촉합니다.", "아버지는 화가 먼저 났지만 사실은 자녀가 무너질까 봐 겁났다고 말합니다.", "ft007_father", "softened"), C("성인자녀가 부모 뒤로 물러나는 이유를 지금 바로 지적하고 직면시키겠습니다.", "psychodynamic", "premature_interpretation|premature_depth", 32, "생활 언어로 바꾸어도 타이밍이 너무 빠르면 공격처럼 들립니다.", "성인자녀는 또 분석당한다고 느끼며 방문을 닫는 것처럼 침묵합니다.", "ft007_adult_child", "withdrawn"), C("경제 규칙을 먼저 쓰고 용돈과 집안일 기준을 명확히 하겠습니다.", "cbft", "contract_before_shame|father_control_shift", 52, "계약은 필요하지만 수치심-공격 고리를 보기 전에는 처벌처럼 들립니다.", "아버지는 반기지만 성인자녀는 또 실패 평가표가 생겼다고 느낍니다.", "ft007_adult_child", "defensive") }),
            T("어머니의 비밀 봉투", new[] { L("ft007_mother", "worried", "left", "둘이 싸우는 게 싫어서 제가 몰래 용돈을 줬습니다. 그 순간은 조용해지지만 더 숨기는 일이 됩니다."), L("ft007_father", "defensive", "right", "그러니까 제가 더 화가 나는 겁니다. 저는 밖에 있고 둘이만 아는 일이 생깁니다."), L("supervisor_psychodynamic", "questioning", "supervisor", "비밀 지원은 사랑이면서 삼각관계를 유지하는 장치입니다.") },
                new[] { C("어머니의 지원이 갈등을 낮추는 동시에 아버지와 자녀 사이의 직접 대화를 막는다는 점을 조심스럽게 확인하겠습니다.", "psychodynamic", "triangle_named", 92, "비난 없이 삼각관계를 보이게 합니다.", "어머니는 자신이 도우려 했지만 둘 사이 대화를 더 피하게 했다는 점을 인정합니다.", "ft007_mother", "listening"), C("어머니는 앞으로 몰래 돕지 않겠다고 약속하고, 아버지에게 모든 결정을 맡기겠습니다.", "structural", "father_control_shift", 40, "비밀은 줄지만 권력이 한쪽으로 이동합니다.", "성인자녀는 이제 아버지 허락 없이는 아무것도 못 하는 것 같다고 말합니다.", "ft007_adult_child", "scared"), C("성인자녀가 어머니 뒤에 숨는 것처럼 보인다고 바로 말하겠습니다.", "psychodynamic", "premature_depth", 34, "말은 쉬워졌지만 타이밍이 빠르고 수치심을 자극합니다.", "성인자녀는 자신을 또 미성숙한 사람으로 보는 거냐며 굳어집니다.", "ft007_adult_child", "defensive") }),
            T("분노 뒤의 두려움",
                new[] { L("ft007_father", "worried", "right", "사실은 저도 퇴직 후에 쓸모없어질까 봐 무섭습니다. 그래서 아이를 보면 제 불안이 더 커집니다."), L("ft007_adult_child", "hesitant", "center", "그 말을 들으니 조금 다르게 들리지만, 저는 아직 아버지 앞에서 실패 얘기를 꺼내기가 무섭습니다."), L("supervisor_psychodynamic", "explaining", "supervisor", "투사를 이름 붙일 때는 두 사람의 취약성이 충분히 올라온 뒤여야 합니다.") },
                new[] { C("아버지의 두려움과 자녀의 수치심이 서로를 자극하는 순서를 함께 그려보겠습니다.", "psychodynamic", "defense_sequence_seen", 96, "방어 순서를 가족이 함께 볼 수 있게 합니다.", "아버지와 성인자녀는 게으름과 철수라는 표면 아래 서로 다른 두려움이 있었다고 말합니다.", "ft007_father", "softened"), C("아버지가 화를 내는 건 결국 자녀가 아버지처럼 실패할까 봐 겁난다는 뜻이라고 바로 말하겠습니다.", "psychodynamic", "interpretation_attack", 36, "개념은 맞아도 너무 직접적이면 공격이 됩니다.", "아버지는 자신을 문제로 몰아간다며 목소리를 높이고, 성인자녀는 다시 물러납니다.", "ft007_father", "critical"), C("자녀가 먼저 구직 계획을 공개하면 아버지 분노가 줄어들 것입니다.", "cbft", "child_must_prove", 40, "자녀가 증명해야 하는 구조가 수치심을 강화합니다.", "성인자녀는 또 성과를 보여줘야만 말할 자격이 생기는 것 같다고 합니다.", "ft007_adult_child", "withdrawn") }),
            T("돈과 존중의 계약",
                new[] { L("ft007_mother", "listening", "left", "계약이 필요하긴 한데, 처벌문처럼 쓰이면 또 숨기게 될 것 같습니다."), L("ft007_adult_child", "hesitant", "center", "돈 얘기를 하면 제가 민폐인 것 같아서 피했는데, 기준이 없으니 더 불안하기도 했습니다."), L("supervisor_psychodynamic", "questioning", "supervisor", "계약은 방어를 본 뒤에 와야 합니다. 그래야 처벌이 아니라 예측 가능성이 됩니다.") },
                new[] { C("돈과 집안일 기준을 쓰되, 먼저 각자 모욕으로 들리는 표현과 필요한 존중 문장을 함께 정하겠습니다.", "psychodynamic", "respect_contract", 94, "정신역동 이해를 현실 계약으로 연결합니다.", "가족은 용돈 액수보다 먼저 서로를 모욕하지 않는 문장을 정해야 한다는 점에 동의합니다.", "ft007_mother", "softened"), C("경제 의존을 끊기 위해 지원 중단 날짜부터 정하겠습니다.", "strategic", "cutoff_contract", 38, "분화가 아니라 정서적 단절을 강화할 수 있습니다.", "성인자녀는 쫓겨나는 것처럼 느끼고, 아버지는 잠깐 통제감을 느낍니다.", "ft007_adult_child", "scared"), C("어머니가 중간에서 금액과 일정을 조율하면 갈등이 줄어들 것입니다.", "procedure", "mother_triangle_locked", 34, "어머니를 다시 비밀 조정자 자리에 둡니다.", "어머니는 또 자신이 몰래 조절해야 할 것 같아 부담스럽다고 말합니다.", "ft007_mother", "exhausted") }),
            T("다시 열리는 방문",
                new[] { L("ft007_father", "softened", "right", "제가 겁나니까 자꾸 '게으르다'고 몰아붙였던 것 같아요. 그래도 집에서 지킬 기준은 필요합니다."), L("ft007_adult_child", "softened", "center", "저도 숨기만 하면 더 실패자처럼 느껴집니다. 기준이 공격처럼 오지 않으면 말할 수 있을 것 같습니다."), L("supervisor_psychodynamic", "reflective", "supervisor", "마지막은 해석을 행동으로 번역하는 단계입니다. 수치심을 낮추는 계약이어야 합니다.") },
                new[] { C("다음 주까지 구직 시간, 생활비 기준, 모욕 금지 문장을 한 장에 같이 적고 어머니는 비밀 지원을 멈추겠습니다.", "psychodynamic", "money_contract_written", 95, "방어 이해와 현실 구조를 함께 담은 마무리입니다.", "가족은 완전한 화해보다 숨기지 않고 말할 수 있는 첫 기준을 정합니다.", "ft007_adult_child", "softened"), C("아버지는 비난하지 않고, 자녀는 매일 구직 결과를 보고하는 방식으로 정리하겠습니다.", "cbft", "performance_report", 45, "행동 구조는 있으나 성과 평가 루프가 살아납니다.", "성인자녀는 매일 보고가 또 실패 검사처럼 느껴질까 봐 걱정합니다.", "ft007_adult_child", "worried"), C("어머니가 두 사람에게 따로 감정을 확인해 다음 회기 때 알려주겠습니다.", "procedure", "triangulated_closure", 30, "직접 대화를 다시 어머니에게 위임합니다.", "어머니는 부담을 떠안고, 아버지와 자녀는 서로를 보지 않습니다.", "ft007_mother", "worried") })
        });

        RegisterFocusedVnScriptsPartThree();
    }

    private void RegisterFocusedVnScriptsPartThree()
    {
        vnScripts["FT-008"] = BuildFocusedVnScript("FT-008", "supervisor_narrative", new[] { "ft008_teen", "ft008_mother", "ft008_father", "ft008_teacher" }, new[]
        {
            T("침묵이 지키는 것",
                new[] { L("ft008_teen", "quiet", "center", "학교 이름만 들어도 속이 안 좋아요. 그런데 집에서도 말하면 분위기가 이상해져서 그냥 말하지 않게 됩니다.", "침묵을 저항이나 회피로만 보지 말고 무엇을 지키려 했는지 보세요."), L("ft008_mother", "worried", "left", "다시 꺼내면 아이가 더 힘들까 봐 말하지 않았습니다. 그런데 안 물어보는 것도 아이를 혼자 두는 것 같아요."), L("ft008_teacher", "procedural", "right", "절차와 별개로 지금 등하교나 온라인 접촉에서 안전하게 분리되어 있는지도 확인해야 합니다."), L("supervisor_narrative", "explaining", "supervisor", "박병호입니다. 문제를 학생 정체성에서 떼어내되, 현재 안전 확인을 이야기치료의 반대편에 놓지 마세요.") },
                new[] { C("사건 내용을 묻기 전에, 지금 안전한 이동 동선과 온라인 접촉 차단이 되어 있는지 확인하고 침묵이 무엇을 지키고 무엇을 빼앗는지 보겠습니다.", "narrative", "safety_check_before_story|silence_externalized", 96, "현재 안전을 확인한 뒤 침묵을 가족의 선택으로 존중하면서 비용을 보이게 합니다.", "청소년은 사건을 다 말하지 않아도 지금 안전부터 확인해 주니 조금 숨이 쉬어진다고 말합니다.", "ft008_teen", "softened"), C("피해 사실을 정확히 알아야 하니 오늘 사건 내용을 자세히 말해보게 하겠습니다.", "procedure", "forced_disclosure", 24, "강제 폭로는 안전감과 주도권을 해칩니다.", "청소년은 그 얘기를 하려면 여기 있기 싫다며 시선을 피합니다.", "ft008_teen", "scared"), C("부모님이 더 단단해지도록 전학을 도망으로 보지 말자고 설득하겠습니다.", "strategic", "parent_reassurance_first", 42, "부모 불안을 먼저 달래면 청소년의 경험이 뒤로 밀립니다.", "청소년은 또 어른들이 전학을 어떻게 볼지만 이야기한다고 느낍니다.", "ft008_teen", "withdrawn") }),
            T("문제 이름을 누가 정하는가",
                new[] { L("ft008_father", "defensive", "right", "전학하면 도망친 것처럼 남을까 봐 걱정됩니다. 버티면 이긴다는 말도 틀린 말은 아닌 것 같고요."), L("ft008_teen", "hesitant", "center", "저는 이미 도망친 사람처럼 느껴져요. 그런데 버티라고 하면 그 말이 저를 더 작게 만듭니다."), L("supervisor_narrative", "questioning", "supervisor", "버티기 담론이 누구에게 힘을 주고 누구를 고립시키는지 묻는 것이 이야기치료의 입구입니다.") },
                new[] { C("학생이 직접 이 문제를 뭐라고 부르고 싶은지 묻고, 그 이름이 학생을 어떻게 다루는지 보겠습니다.", "narrative", "problem_name_teen_authored", 96, "문제 이름을 청소년이 다시 쓰게 합니다.", "청소년은 학교폭력보다 '다시 작아지게 하는 목소리'라는 이름이 더 맞는 것 같다고 말합니다.", "ft008_teen", "softened"), C("버티면 이긴다는 가족 이야기를 더 강하게 만들어 학교에 다시 적응하도록 돕겠습니다.", "cbft", "endurance_story", 34, "버티기 담론이 피해자의 고립을 강화합니다.", "청소년은 이기는 게 아니라 버려지는 느낌이라고 말합니다.", "ft008_teen", "withdrawn"), C("학교 절차를 먼저 정리하고 전학 가능 여부를 확인하겠습니다.", "procedure", "procedure_closure", 50, "절차는 필요하지만 문제 이름과 주도권이 빠집니다.", "학교 담당자는 절차는 설명할 수 있지만 학생이 원하는 안전의 언어가 필요하다고 말합니다.", "ft008_teacher", "procedural") }),
            T("다르게 버틴 순간 찾기",
                new[] { L("ft008_mother", "listening", "left", "아이가 완전히 무너진 줄만 알았는데, 아직 버틴 순간이 아니라 자기 편을 찾은 순간도 있었을까요?"), L("ft008_teen", "quiet", "center", "상담실 선생님한테는 한 번 말했어요. 그때는 제가 이상한 애가 아닌 것 같았습니다."), L("supervisor_narrative", "explaining", "supervisor", "그 순간은 미담이 아니라, 문제 이야기가 전부가 아니라는 증거입니다.") },
                new[] { C("그때 무엇이 학생을 조금 덜 작아지게 했는지, 그 장면을 두껍게 이야기해보겠습니다.", "narrative", "unique_outcome_thickened", 94, "대안 이야기를 구체화합니다.", "청소년은 자신을 믿어준 어른이 있을 때 문제의 목소리가 약해졌다고 말합니다.", "ft008_teen", "relieved"), C("그 정도로 말할 수 있었다면 앞으로 가족에게도 더 자세히 말해보자고 격려하겠습니다.", "satir", "disclosure_pressure", 40, "작은 말하기를 더 큰 공개 의무로 바꿉니다.", "청소년은 말한 게 실수였던 것 같다고 움츠립니다.", "ft008_teen", "scared"), C("부모가 학교에 강하게 항의하면 학생이 덜 무력할 수 있습니다.", "strategic", "parent_action_over_teen_voice", 48, "부모 행동이 학생 목소리를 덮을 수 있습니다.", "청소년은 자신이 원하지 않는 방식으로 일이 커질까 봐 걱정합니다.", "ft008_teen", "worried") }),
            T("학교 절차를 도구로 쓰기",
                new[] { L("ft008_teacher", "procedural", "right", "분리 조치와 상담 연계는 가능합니다. 다만 학생 동의 없이 모든 내용을 공유할 수는 없습니다."), L("ft008_father", "worried", "right", "절차를 밟으면 아이가 또 사건 중심으로 불릴까 봐 걱정됩니다."), L("supervisor_narrative", "questioning", "supervisor", "절차는 이야기를 대신 쓰는 권력이 아니라 안전을 돕는 도구여야 합니다.") },
                new[] { C("학생이 동의한 범위 안에서 학교 절차를 안전 도구로 쓰고, 가족은 학생이 정한 문제 이름을 유지하겠습니다.", "narrative", "outsider_witness_consent", 95, "학교 절차와 청소년 주도권을 함께 살립니다.", "학교 담당자는 동의 범위를 확인하고, 부모는 학생이 정한 이름을 따라 말하기로 합니다.", "ft008_teacher", "softened"), C("학교가 알아서 가해 학생 조치와 전학 절차를 정리하도록 맡기겠습니다.", "procedure", "school_takes_story", 38, "학교가 이야기의 주도권을 가져갑니다.", "청소년은 또 자신 없이 어른들이 정하는 느낌이라고 말합니다.", "ft008_teen", "withdrawn"), C("가족이 사건 이야기를 더 하지 않기로 하고 절차만 조용히 진행하겠습니다.", "strategic", "silence_kept", 35, "침묵의 비용을 다루지 못합니다.", "어머니는 조용해질 것 같지만 아이가 덜 혼자인지는 모르겠다고 말합니다.", "ft008_mother", "worried") }),
            T("다음 이야기의 증인",
                new[] { L("ft008_teen", "softened", "center", "그 일 얘기만 하는 사람 말고, 그냥 저를 저로 봐주는 사람이 있으면 전학이든 절차든 조금 덜 무서울 것 같아요."), L("ft008_mother", "softened", "left", "말하지 않는 게 배려라고만 생각했는데, 이제는 아이가 정한 이름으로 물어봐야겠습니다."), L("supervisor_narrative", "reflective", "supervisor", "마지막 과제는 폭로가 아니라 동의된 증인과 대안 이야기의 유지입니다.") },
                new[] { C("학생이 동의한 한 명의 증인을 정하고, 가족은 '다시 작아지게 하는 목소리'가 약해진 순간을 기록하겠습니다.", "narrative", "alternative_story_task", 94, "대안 이야기와 안전한 증인을 다음 주 과제로 연결합니다.", "가족은 사건 전체가 아니라 학생이 정한 이름과 약해진 순간을 함께 기록하기로 합니다.", "ft008_teen", "relieved"), C("다음 주까지 사건을 가족에게 한 번 자세히 설명하는 것을 목표로 하겠습니다.", "procedure", "disclosure_homework", 26, "폭로를 과제로 만들어 통제감을 빼앗습니다.", "청소년은 다시 상담이 무서워졌다고 말합니다.", "ft008_teen", "scared"), C("전학 여부를 빨리 결정해 불확실성을 줄이겠습니다.", "solution", "decision_before_story", 44, "결정은 필요하지만 이야기 회복보다 앞서면 도피/버티기 프레임이 남습니다.", "아버지는 편해지지만 청소년은 자신의 이야기가 아직 정리되지 않았다고 말합니다.", "ft008_teen", "worried") })
        });

        vnScripts["FT-009"] = BuildFocusedVnScript("FT-009", "supervisor_cbft", new[] { "ft009_mother", "ft009_spouse", "ft009_support" }, new[]
        {
            T("오늘 밤의 안전",
                new[] { L("ft009_mother", "exhausted", "left", "제가 못 버티는 걸 인정하면 엄마 자격이 없는 것 같습니다. 그런데 어젯밤에는 아이가 울 때 제 몸이 움직이지 않았습니다.", "산후 사례는 행동계약 전에 안전 확인이 먼저입니다."), L("ft009_spouse", "worried", "right", "도와주려고 하면 방식이 틀렸다고 해서 손을 못 대겠습니다. 그래도 오늘 밤은 이렇게 두면 안 될 것 같습니다."), L("supervisor_cbft", "explaining", "supervisor", "정세영입니다. 자동사고와 행동계약을 다루되, 영아 안전과 보호자 안전을 먼저 확인하세요.") },
                new[] { C("지금은 먼저 보호자와 아기의 오늘 밤 안전을 구체적으로 확인하겠습니다. 혼자 위험해지는 순간, 아기와 단둘이 있는 시간, 연락할 사람, 배우자 교대 시간을 차례로 보겠습니다.", "cbft", "safety_screen_started|crisis_contact_named|baby_safety_check", 98, "고위험 산후 사례에서 안전 확인이 최우선입니다.", "보호자와 배우자는 오늘 밤 혼자 두지 않을 시간표와 연락할 사람을 구체적으로 말하기 시작합니다.", "ft009_spouse", "softened"), C("배우자가 더 공감적으로 말하면 보호자가 덜 외로울 수 있으니 감정 반영부터 하겠습니다.", "satir", "empathy_before_safety", 42, "정서 반영은 필요하지만 안전 확인을 대체할 수 없습니다.", "보호자는 고맙지만 오늘 밤 아이가 계속 울면 자신이 어떻게 할지 모르겠다고 말합니다.", "ft009_mother", "worried"), C("가사 분담표를 만들어 배우자가 무엇을 할지 바로 정하겠습니다.", "structural", "chore_plan_without_risk", 48, "행동계획은 필요하지만 산후 위험 신호를 먼저 지나칩니다.", "배우자는 할 일을 적지만 보호자의 안전과 수면 공백은 아직 확인되지 않았습니다.", "ft009_spouse", "procedural") }),
            T("생각과 수면 부족이 맞물리는 밤",
                new[] { L("ft009_mother", "worried", "left", "도움을 요청하면 실패한 엄마라는 생각이 계속 납니다. 그래서 말하지 않고 버티다가 더 무너집니다."), L("ft009_spouse", "listening", "right", "말하지 않아도 알아야 한다고 들으면 저는 더 얼어붙습니다. 정확히 말해주면 할 수 있는데, 그 말이 또 부담이 되는 것 같습니다."), L("supervisor_cbft", "questioning", "supervisor", "자동사고-행동-수면 부족 고리를 실제 밤 장면으로 좁히세요.") },
                new[] { C("'도움 요청은 실패'라는 생각이 밤에 어떤 행동을 막는지 보고, 요청 문장을 하나만 연습하겠습니다.", "cbft", "automatic_thought_linked", 94, "신념과 행동을 연결해 작은 대체 행동으로 옮깁니다.", "산후 보호자는 실패가 아니라 교대 요청이라는 문장이라면 말해볼 수 있다고 합니다.", "ft009_mother", "softened"), C("배우자가 더 알아서 해야 하니 산후 보호자가 말하지 않아도 할 일을 정하겠습니다.", "strategic", "mindreading_contract", 38, "말하지 않아도 알아야 한다는 고리를 강화합니다.", "배우자는 또 틀릴까 봐 불안해지고, 산후 보호자는 말하지 않아도 알아주길 바라는 마음이 남습니다.", "ft009_spouse", "defensive"), C("산후 보호자가 매일 사고기록지를 작성해서 비합리적 생각을 점검하겠습니다.", "cbft", "thought_homework_without_sleep", 52, "기록지는 가능하지만 수면과 돌봄 교대가 빠지면 실행이 어렵습니다.", "산후 보호자는 잠도 못 자는데 기록까지 해야 하냐며 더 지친 표정을 보입니다.", "ft009_mother", "exhausted") }),
            T("첫 울음 행동계약",
                new[] { L("ft009_spouse", "worried", "right", "아이가 처음 울 때 제가 바로 들어가도 되는지 모르겠습니다. 잘못 안으면 또 혼날까 봐 멈춥니다."), L("ft009_mother", "exhausted", "left", "제가 다 해야 한다는 생각이 들면서도, 남편이 하면 불안해서 계속 확인하게 됩니다."), L("supervisor_cbft", "explaining", "supervisor", "행동계약은 누가 나쁜 부모인지가 아니라 첫 울음 때 누가 무엇을 하는지까지 내려가야 합니다.") },
                new[] { C("첫 울음이 나면 배우자는 20분 안기, 보호자는 물 마시고 눕기, 실패하면 지원 인물에게 연락하는 규칙을 정하겠습니다.", "cbft", "first_cry_contract", 96, "구체적이고 관찰 가능한 돌봄 행동계약입니다.", "배우자는 자신이 할 수 있는 행동을 알고, 보호자는 20분이라도 몸을 내려놓는 데 동의합니다.", "ft009_spouse", "softened"), C("보호자가 불안하지 않도록 배우자가 먼저 허락을 받고 아기를 돌보겠습니다.", "structural", "permission_loop", 42, "허락 루프가 보호자를 계속 감독자 자리에 둡니다.", "보호자는 쉬지 못하고 계속 확인해야 할 것 같다고 말합니다.", "ft009_mother", "worried"), C("지원 인물이 매일 밤 와서 배우자 대신 아기를 맡는 방식으로 가겠습니다.", "procedure", "support_replaces_spouse", 48, "지원은 필요하지만 부부의 행동계약이 생기지 않습니다.", "배우자는 자신이 또 빠지는 것 같다고 말하고, 보호자는 장기적으로 불안해합니다.", "ft009_spouse", "withdrawn") }),
            T("지원망을 실제로 부르기",
                new[] { L("ft009_support", "neutral", "center", "오늘 밤 9시부터 11시까지는 전화 받을 수 있습니다. 필요하면 30분이라도 가겠습니다."), L("ft009_mother", "hesitant", "left", "도움을 부르면 민폐 같지만, 혼자 있다가 무너지는 게 더 무섭기도 합니다."), L("supervisor_cbft", "questioning", "supervisor", "지원망은 막연한 위로가 아니라 시간, 연락처, 실패 시 재시도 규칙이어야 합니다.") },
                new[] { C("지원 인물의 가능 시간을 적고, 배우자가 먼저 연락 문자를 보내며, 보호자는 위기 신호를 말로 정하겠습니다.", "cbft", "support_network_contacted", 95, "지원망을 실제 행동 단위로 연결합니다.", "지원 인물은 받을 수 있는 시간을 말하고, 배우자는 지금 문자를 보내겠다고 합니다.", "ft009_support", "softened"), C("보호자가 정말 힘들 때만 직접 연락하도록 번호를 알려주겠습니다.", "solution", "mother_must_initiate_crisis", 36, "위기 순간의 요청 책임을 보호자에게만 둡니다.", "보호자는 정말 힘들 때는 오히려 전화할 힘이 없을 것 같다고 말합니다.", "ft009_mother", "withdrawn"), C("배우자가 모든 밤 돌봄을 책임지겠다고 약속하고 지원망은 예비로 두겠습니다.", "cbft", "spouse_overpromise", 52, "좋아 보이지만 실패 시 복구 규칙이 약합니다.", "배우자는 약속하지만, 실패하면 둘 다 무너질 수 있다는 불안이 남습니다.", "ft009_spouse", "worried") }),
            T("오늘 밤 계획",
                new[] { L("ft009_mother", "softened", "left", "도와달라고 하는 게 제가 못난 게 아니라 오늘 밤을 넘기려고 하는 거라면, 한마디는 해볼 수 있을 것 같아요."), L("ft009_spouse", "softened", "right", "첫 울음 때 제가 20분 맡고, 안 되면 바로 연락한다는 식이면 제가 할 일이 보입니다."), L("supervisor_cbft", "reflective", "supervisor", "마지막은 좋은 말이 아니라 오늘 밤 실행표와 재시도 규칙입니다.") },
                new[] { C("오늘 밤 9시부터 첫 울음 20분 교대, 보호자 수면 90분, 실패 시 지원 인물 연락까지 적겠습니다.", "cbft", "safety_plan_written", 98, "안전, 행동계약, 지원망을 한 장면으로 묶습니다.", "가족은 오늘 밤 누가 무엇을 하는지와 실패 시 누구에게 연락할지 구체적으로 합의합니다.", "ft009_mother", "softened"), C("서로 고생했다는 말을 매일 한 번씩 하고, 내일 다시 기분을 점검하겠습니다.", "satir", "warmth_without_plan", 40, "정서 확인은 좋지만 오늘 밤 위험과 행동 공백이 남습니다.", "보호자는 따뜻하지만 오늘 밤 아이가 울 때는 여전히 막막하다고 말합니다.", "ft009_mother", "worried"), C("배우자가 앞으로 집안일을 더 많이 하겠다고 약속하고 마무리하겠습니다.", "structural", "chores_only_closure", 34, "산후우울과 영아 안전을 집안일 문제로 축소합니다.", "배우자는 의지는 보이지만 보호자의 수면과 위기 연락은 정해지지 않습니다.", "ft009_spouse", "procedural") })
        });

        vnScripts["FT-010"] = BuildFocusedVnScript("FT-010", "supervisor_solution", new[] { "ft010_teen", "ft010_guardian", "ft010_sibling" }, new[]
        {
            T("8점짜리 하루",
                new[] { L("ft010_teen", "exhausted", "center", "제가 안 하면 동생 밥도 못 먹을 때가 있어요. 동아리도 그만뒀고, 요즘은 제가 얼마나 힘든지도 잘 모르겠어요.", "강점 인정이 더 많은 돌봄 요구로 이어지지 않게 조심하세요."), L("ft010_guardian", "worried", "left", "미안하다는 말밖에 못 하니까 더 미안합니다. 그래도 이 집이 돌아가는 건 아이 덕분입니다."), L("supervisor_solution", "explaining", "supervisor", "송지후입니다. 해결중심은 희생을 칭찬하는 것이 아니라, 이미 작동한 예외에서 부담을 1점 낮추는 것입니다.") },
                new[] { C("지금 부담을 0에서 10으로 묻고, 8을 7로 낮추는 아주 작은 차이를 찾겠습니다.", "solution", "scale_one_point", 96, "0~10 점검을 돌봄 부담 줄이기로 연결합니다.", "누나는 8에서 7이 되려면 동생 준비물 하나만 덜 챙겨도 된다고 말합니다.", "ft010_teen", "softened"), C("누나가 이미 가족을 잘 지탱하고 있다는 강점을 충분히 인정하겠습니다.", "solution", "hero_burden", 42, "강점 인정이 더 많은 돌봄을 정당화할 수 있습니다.", "누나는 칭찬을 듣지만 그러면 계속 자신이 해야 하는 것 같다고 말합니다.", "ft010_teen", "worried"), C("보호자의 죄책감을 먼저 다뤄야 누나 부담도 줄어들 것입니다.", "satir", "guilt_centered", 40, "보호자 죄책감이 중심이 되면 청소년이 다시 위로자가 됩니다.", "보호자가 울자 누나는 괜찮다며 다시 보호자를 달랩니다.", "ft010_teen", "withdrawn") }),
            T("30분 예외",
                new[] { L("ft010_teen", "hesitant", "center", "지난주 한 번은 동생이 친구 집에 가서 30분 시간이 비었어요. 그때 아무것도 안 했는데도 숨이 쉬어졌습니다."), L("ft010_sibling", "neutral", "right", "저도 가방에 물통 넣는 건 할 수 있어요. 근데 누나가 늘 먼저 해줘요."), L("supervisor_solution", "questioning", "supervisor", "예외는 큰 성공이 아니라 부담이 1점 낮아진 순간입니다.") },
                new[] { C("그 30분이 어떻게 가능했는지 찾아서, 동생이 맡을 준비물 하나와 보호자가 맡을 10분을 정하겠습니다.", "solution", "exception_scaled", 95, "예외를 가족 역할 재분배로 확장합니다.", "동생은 물통을 맡겠다고 하고, 보호자는 10분이라도 알람을 맞춰 일어나겠다고 말합니다.", "ft010_sibling", "softened"), C("누나가 그 30분을 더 잘 활용하도록 휴식 계획표를 만들겠습니다.", "cbft", "teen_optimizes_rest", 45, "휴식까지 수행 과제가 됩니다.", "누나는 쉬는 것도 계획해야 하냐며 웃지만 표정은 지칩니다.", "ft010_teen", "exhausted"), C("동생을 외부 돌봄에 더 많이 맡기면 누나 부담이 빠르게 줄어들 것입니다.", "procedure", "resource_takeover", 52, "외부지원이 가족 선택권과 작은 협력을 덮을 수 있습니다.", "보호자는 도움이 필요하지만 집 안에서 바꿀 수 있는 것부터 알고 싶다고 말합니다.", "ft010_guardian", "worried") }),
            T("강점과 위험을 같이 보기",
                new[] { L("ft010_guardian", "tearful", "left", "아이가 기특하다고만 말하면 안 되는 거겠죠. 그런데 제가 할 수 있는 게 작아서 더 미안합니다."), L("ft010_teen", "quiet", "center", "기특하다는 말은 좋은데, 그 말을 들으면 계속 해야 할 것 같아요."), L("supervisor_solution", "explaining", "supervisor", "해결중심에서도 청소년이 부모 역할을 떠안는 위험은 명확히 보아야 합니다. 강점은 부담을 나누기 위한 단서입니다.") },
                new[] { C("누나가 잘해온 것을 인정하되, 그 강점이 앞으로 덜 해도 되는 방향으로 쓰이게 하겠습니다.", "solution", "strength_reduces_burden", 96, "강점을 희생 유지가 아니라 부담 감소로 재정의합니다.", "누나는 잘했다는 말보다 덜 해도 된다는 말이 더 낯설지만 좋다고 합니다.", "ft010_teen", "relieved"), C("누나가 책임감이 강하니 동생 돌봄을 더 체계적으로 할 수 있게 목록을 만들겠습니다.", "cbft", "parentification_organized", 28, "부모화를 정리해서 더 강화하는 선택입니다.", "누나는 목록을 보며 자신이 진짜 부모가 된 것 같다고 말합니다.", "ft010_teen", "withdrawn"), C("보호자가 누나에게 미안하다고 충분히 표현하면 관계가 나아질 것입니다.", "satir", "apology_only", 42, "사과만으로는 돌봄 구조가 줄지 않습니다.", "누나는 괜찮다고 말하지만 내일 아침도 자신이 해야 할 일이 그대로라고 말합니다.", "ft010_teen", "worried") }),
            T("각자 하나씩",
                new[] { L("ft010_sibling", "softened", "right", "물통이랑 알림장은 제가 할 수 있어요. 누나가 검사 안 해도 되게 해볼게요."), L("ft010_guardian", "listening", "left", "저는 10분이라도 동생 숙제 시작을 봐줄 수 있습니다. 실패하면 다시 미안하다고만 하지 않겠습니다."), L("supervisor_solution", "questioning", "supervisor", "각자 하나씩 맡되, 실패 복구 규칙이 있어야 청소년이 다시 다 떠안지 않습니다.") },
                new[] { C("동생 준비물 하나, 보호자 10분, 누나 자기시간 30분을 정하고 실패하면 다시 나누는 규칙을 쓰겠습니다.", "solution", "shared_micro_tasks", 97, "작은 과제와 실패 복구를 동시에 잡습니다.", "가족은 누나가 빠진 시간을 비워두는 것이 아니라 각자 하나씩 맡는 실험에 합의합니다.", "ft010_teen", "softened"), C("동생이 할 수 있는 일을 늘려 누나가 감독만 하도록 하겠습니다.", "structural", "teen_as_manager", 42, "감독자 역할이 남아 부모화가 유지됩니다.", "누나는 직접 하는 것보다 감독하는 게 더 피곤할 수도 있다고 말합니다.", "ft010_teen", "exhausted"), C("지원 인물이 주 1회 와서 누나 시간을 보장하게 하겠습니다.", "procedure", "external_support_only", 54, "지원은 좋지만 가족 내부 재분배와 실패 규칙이 약합니다.", "누나는 그날은 쉬겠지만 나머지 날은 똑같을까 봐 걱정합니다.", "ft010_teen", "worried") }),
            T("8에서 7로 낮추기",
                new[] { L("ft010_teen", "softened", "center", "동아리를 바로 다시 가는 건 어렵지만, 일주일에 한 번 30분은 제 시간이면 좋겠습니다."), L("ft010_guardian", "softened", "left", "제가 10분을 실패해도 미안하다고만 하지 않고 다시 나누자고 말해보겠습니다."), L("supervisor_solution", "reflective", "supervisor", "마지막 과제는 영웅 만들기가 아니라 부담 1점을 실제로 낮추는 실험입니다.") },
                new[] { C("다음 주는 누나 부담 8을 7로 낮추는 실험으로, 동생 물통, 보호자 10분, 누나 30분 자기시간을 확인하겠습니다.", "solution", "one_point_relief_plan", 96, "해결중심 척도를 부모화 완화 실험으로 마무리합니다.", "가족은 완벽한 해결 대신 누나의 부담을 1점 낮추는 구체적 행동을 나눕니다.", "ft010_teen", "relieved"), C("누나가 힘들 때 바로 말하고 가족이 그때마다 도와주기로 하겠습니다.", "satir", "teen_must_signal", 44, "도움 요청 책임이 다시 청소년에게만 갑니다.", "누나는 정말 힘들 때는 말할 힘도 없을 것 같다고 말합니다.", "ft010_teen", "worried"), C("지원 인물에게 동생 돌봄을 맡기는 날을 늘려 빠르게 부담을 줄이겠습니다.", "procedure", "resource_takeover_closure", 48, "외부지원만으로 가족 안 역할 변화가 약합니다.", "보호자는 도움은 고맙지만 가족 안에서 자신이 맡을 작은 역할도 필요하다고 말합니다.", "ft010_guardian", "worried") })
        });
    }

    private VnCaseScript BuildFocusedVnScript(string caseId, string supervisorId, string[] characterIds, IEnumerable<VnTurn> turns)
    {
        FamilyCase caseData = cases.First(c => c.id == caseId);
        var script = new VnCaseScript
        {
            caseId = caseId,
            scriptKind = "focused_case_specific_v1",
            chapter = caseData.chapter,
            backgroundId = "VN/Backgrounds/counseling_room_day",
            characters = characterIds.Concat(new[] { supervisorId }).Distinct().ToArray(),
            turns = turns.ToList()
        };
        AssignConventionCgPaths(script);
        return script;
    }

    private static string BuildConventionCgPath(string caseId, string slug)
    {
        string compactId = string.IsNullOrEmpty(caseId) ? "ft000" : caseId.Replace("-", "").ToLowerInvariant();
        return "VN/EventCG/" + compactId.ToUpperInvariant() + "/" + compactId + "_" + slug;
    }

    private static void AssignConventionCgPaths(VnCaseScript script)
    {
        if (script == null || script.turns == null || string.IsNullOrEmpty(script.caseId)) return;
        string compactId = script.caseId.Replace("-", "").ToLowerInvariant();
        for (int turnIndex = 0; turnIndex < script.turns.Count; turnIndex++)
        {
            VnTurn turn = script.turns[turnIndex];
            string turnSlug = "t" + (turnIndex + 1).ToString("00", CultureInfo.InvariantCulture);
            if (turn.setupLines != null)
            {
                for (int lineIndex = 0; lineIndex < turn.setupLines.Count; lineIndex++)
                {
                    VnDialogueLine line = turn.setupLines[lineIndex];
                    if (line == null || !string.IsNullOrEmpty(line.cgResourcePath)) continue;
                    string speakerSlug = SlugForCg(line.speakerId);
                    line.cgResourcePath = "VN/EventCG/" + compactId.ToUpperInvariant() + "/" + compactId + "_" + turnSlug + "_l" + (lineIndex + 1).ToString("00", CultureInfo.InvariantCulture) + "_" + speakerSlug;
                }
            }
            if (turn.choices != null)
            {
                for (int choiceIndex = 0; choiceIndex < turn.choices.Count; choiceIndex++)
                {
                    VnChoice choice = turn.choices[choiceIndex];
                    if (choice == null || !string.IsNullOrEmpty(choice.reactionCgResourcePath)) continue;
                    string choiceSlug = choiceIndex == 0 ? "a" : choiceIndex == 1 ? "b" : "c";
                    string speakerSlug = SlugForCg(choice.reactionSpeakerId);
                    choice.reactionCgResourcePath = "VN/EventCG/" + compactId.ToUpperInvariant() + "/" + compactId + "_" + turnSlug + "_reaction_" + choiceSlug + "_" + speakerSlug;
                }
            }
        }
    }

    private static string SlugForCg(string value)
    {
        if (string.IsNullOrEmpty(value)) return "scene";
        string slug = value.ToLowerInvariant();
        int underscore = slug.LastIndexOf('_');
        if (underscore >= 0 && underscore < slug.Length - 1) slug = slug.Substring(underscore + 1);
        return slug.Replace("supervisor", "supervisor");
    }

    private static VnTurn T(string title, IEnumerable<VnDialogueLine> lines, IEnumerable<VnChoice> choices)
    {
        return new VnTurn(title, lines, choices);
    }

    private static VnDialogueLine L(string speakerId, string expressionId, string position, string text, string supervisorNote = "")
    {
        return new VnDialogueLine(speakerId, expressionId, position, text, supervisorNote);
    }

    private static VnChoice C(string label, string theoryId, string interventionType, int quality, string feedback, string familyReaction, string reactionSpeakerId, string reactionExpressionId)
    {
        return new VnChoice(label, theoryId, interventionType, quality, feedback, familyReaction, reactionSpeakerId, reactionExpressionId);
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
            scriptKind = "generic_fallback",
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

    private static VnDialogueLine Ft001Line(string cgSlug, string speakerId, string expressionId, string position, string text, string supervisorNote = "")
    {
        return new VnDialogueLine(speakerId, expressionId, position, text, supervisorNote, Ft001CgPath(cgSlug));
    }

    private static VnChoice Ft001Choice(string reactionCgSlug, string label, string theoryId, string interventionType, int quality, string feedback, string familyReaction, string reactionSpeakerId, string reactionExpressionId)
    {
        return new VnChoice(label, theoryId, interventionType, quality, feedback, familyReaction, reactionSpeakerId, reactionExpressionId, Ft001CgPath(reactionCgSlug));
    }

    private static string Ft001CgPath(string slug)
    {
        if (string.IsNullOrEmpty(slug)) return "";

        string lineByLinePath = Ft001LineByLineCgPath(slug);
        if (!string.IsNullOrEmpty(lineByLinePath)) return lineByLinePath;

        string mapped = Ft001CommercialCgName(slug);
        return string.IsNullOrEmpty(mapped)
            ? "VN/EventCG/FT001/ft001_cg_" + slug
            : "VN/EventCG/FT001_CommercialBranching/" + mapped;
    }

    private static string Ft001LineByLineCgPath(string slug)
    {
        string fileName = "ft001_cg_" + slug;
        string[] folders =
        {
            "VN/EventCG/FT001_LineByLineLocked_Regen_20260611",
            "VN/EventCG/FT001_LineByLineLocked"
        };

        foreach (string folder in folders)
        {
            string path = folder + "/" + fileName;
            if (Resources.Load<Texture2D>(path) != null) return path;
        }

        return "";
    }

    private static string Ft001CommercialCgName(string slug)
    {
        switch (slug)
        {
            case "intro_01_mother_neutral":
            case "t01_l01_mother_neutral":
            case "t01_l03_mother_worried":
                return "ft001_cb_001_intro_mother_pressure";

            case "intro_02_child_neutral":
            case "t01_l02_child_anxious":
            case "t01_l04_child_quiet":
                return "ft001_cb_002_intro_child_anxiety";

            case "intro_03_grandmother_neutral":
                return "ft001_cb_003_intro_grandmother_worry";

            case "intro_04_teacher_neutral":
            case "t01_l05_teacher_concerned":
                return "ft001_cb_004_intro_teacher_procedure";

            case "t01_reaction_a_mother_softened":
            case "t02_l00_branch_mother_open":
                return "ft001_cb_005_t1_good_joining_reaction";

            case "t01_reaction_b_child_withdrawn":
            case "t02_l00_branch_child_closed":
                return "ft001_cb_006_t1_bad_ip_fixed_reaction";

            case "t01_reaction_c_teacher_procedural":
            case "t02_l00_branch_teacher_cautious":
                return "ft001_cb_007_t1_procedure_reaction";

            case "t02_l01_mother_defensive":
            case "t02_l02_child_quiet":
            case "t02_l03_mother_exhausted":
            case "t02_l04_child_hesitant":
            case "t02_reaction_a_mother_softened":
                return "ft001_cb_008_t2_systemic_morning_map";

            case "t02_reaction_b_mother_defensive":
                return "ft001_cb_009_t2_rupture_repair_apology";

            case "t02_l05_teacher_procedural":
            case "t02_reaction_c_child_withdrawn":
                return "ft001_cb_010_t2_institutional_loop";

            case "t02_choice_idle":
                return "ft001_cb_011_t2_choice_idle_three_routes";

            case "t03_l01_grandmother_critical":
            case "t03_l02_mother_exhausted":
            case "t03_l03_grandmother_worried":
            case "t03_l04_mother_tearful":
            case "t03_l05_child_scared":
            case "t03_reaction_a_grandmother_softened":
            case "t04_l00_branch_grandmother_softened":
                return "ft001_cb_012_t3_open_grandmother_worry";

            case "t03_l00_branch_mother_cautious":
                return "ft001_cb_013_t3_fragile_rejoining";

            case "t03_l00_branch_mother_defensive":
            case "t03_reaction_b_grandmother_defensive":
            case "t04_l00_branch_grandmother_stubborn":
                return "ft001_cb_014_t3_rupture_therapist_responsibility";

            case "t04_l00_branch_mother_anxious":
                return "ft001_cb_015_t3_compliance_without_feeling";

            case "t03_l00_branch_child_links_pattern":
            case "t03_reaction_c_child_hesitant":
            case "t04_l00_branch_child_exception":
                return "ft001_cb_016_t3_exception_child_speaks";

            case "t04_l02_mother_worried":
            case "t04_l03_child_quiet":
            case "t04_l04_mother_listening":
            case "t04_l05_child_hesitant":
            case "t04_reaction_a_supervisor_approving":
            case "t05_l00_branch_teacher_adjusts":
                return "ft001_cb_017_t4_systemic_circular_question";

            case "t04_reaction_b_mother_anxious":
            case "t05_l00_branch_mother_anxious":
                return "ft001_cb_018_t4_fragile_failure_rule";

            case "t04_reaction_c_child_withdrawn":
                return "ft001_cb_019_t4_rupture_repair_not_solution";

            case "t05_l01_teacher_concerned":
            case "t05_l05_teacher_softened":
                return "ft001_cb_020_t4_institutional_pressure_reduced";

            case "t05_l00_branch_child_scared":
                return "ft001_cb_021_t4_bad_diagnostic_closure";

            case "t05_l02_mother_softened":
            case "t05_l03_child_relieved":
            case "t05_l04_grandmother_softened":
            case "t05_reaction_a_mother_softened":
                return "ft001_cb_022_ending_system_plan";

            case "t05_reaction_b_child_scared":
                return "ft001_cb_024_ending_compliance";

            case "t05_reaction_c_teacher_procedural":
                return "ft001_cb_025_ending_rupture";

            case "intro_05_supervisor_explaining":
            case "t01_l06_supervisor_explaining":
            case "t02_l06_supervisor_explaining":
                return "ft001_cb_026_supervisor_hyesung_opening";

            case "t03_l06_supervisor_questioning":
            case "t04_l01_supervisor_questioning":
            case "t04_l06_supervisor_explaining":
                return "ft001_cb_027_supervisor_hyesung_rupture_warning";

            case "t05_l06_supervisor_reflective":
                return "ft001_cb_028_supervisor_hyesung_final_review";

            case "t01_choice_idle":
            case "t03_choice_idle":
            case "t04_choice_idle":
            case "t05_choice_idle":
                return "ft001_cb_029_choice_idle_tense";

            default:
                return "";
        }
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

        CreateAbsolutePanel(root.transform, "Title Vignette", new Color32(0, 0, 0, 116), Vector2.zero, Vector2.one, 0, 0, 0, 0);
        CreateAbsolutePanel(root.transform, "Left Mood Shade", new Color32(11, 15, 20, 145), new Vector2(0f, 0f), new Vector2(0.52f, 1f), 0, 0, 0, 0);
        CreateAbsolutePanel(root.transform, "Bottom Title Fade", new Color32(13, 16, 20, 206), new Vector2(0f, 0f), new Vector2(1f, 0.25f), 0, 0, 0, 0);

        var titleBlock = CreateAbsolutePanel(root.transform, "Title Block", new Color32(0, 0, 0, 0), new Vector2(0.06f, 0.59f), new Vector2(0.74f, 0.9f), 0, 0, 0, 0);
        var titleLayout = titleBlock.AddComponent<VerticalLayoutGroup>();
        titleLayout.spacing = 7;
        titleLayout.childControlWidth = true;
        titleLayout.childControlHeight = true;
        titleLayout.childForceExpandWidth = true;
        titleLayout.childForceExpandHeight = false;
        var title = CreateText(titleBlock.transform, "가족치료 시뮬레이션", 66, FontStyle.Bold, Color.white);
        title.alignment = TextAnchor.UpperLeft;
        title.gameObject.AddComponent<Shadow>().effectColor = new Color32(0, 0, 0, 210);
        var subtitle = CreateText(titleBlock.transform, "첫 회기: 등교를 멈춘 아침", 31, FontStyle.Bold, new Color32(255, 241, 212, 255));
        subtitle.gameObject.AddComponent<Shadow>().effectColor = new Color32(0, 0, 0, 180);
        var tagline = CreateText(titleBlock.transform, "문제를 가진 사람을 찾는 대신, 가족이 반복하는 장면을 읽습니다.", 22, FontStyle.Normal, new Color32(226, 232, 236, 255));
        tagline.gameObject.AddComponent<Shadow>().effectColor = new Color32(0, 0, 0, 180);

        var episodePanel = CreateAbsolutePanel(root.transform, "Episode Slate", new Color32(17, 21, 28, 170), new Vector2(0.06f, 0.33f), new Vector2(0.47f, 0.50f), 0, 0, 0, 0);
        var episodeLayout = episodePanel.AddComponent<VerticalLayoutGroup>();
        episodeLayout.padding = new RectOffset(24, 24, 18, 18);
        episodeLayout.spacing = 5;
        episodeLayout.childControlWidth = true;
        episodeLayout.childControlHeight = true;
        episodeLayout.childForceExpandWidth = true;
        episodeLayout.childForceExpandHeight = false;
        FamilyCase featured = currentCase ?? cases.First(c => c.id == "FT-001");
        CreateText(episodePanel.transform, "오늘의 수련 파일", 18, FontStyle.Bold, new Color32(255, 241, 212, 255));
        CreateText(episodePanel.transform, featured.id + " · " + featured.presentingProblem, 20, FontStyle.Bold, new Color32(226, 232, 236, 255));
        CreateText(episodePanel.transform, "담당: " + GetSupervisorForTheory(featured.recommendedTheoryId).name, 15, FontStyle.Normal, new Color32(198, 214, 220, 255));

        var commandPanel = CreateAbsoluteSkinnedPanel(root.transform, "Title Command Menu", "VN/UI/case_file_panel", new Color32(18, 21, 27, 218), new Vector2(0.67f, 0.12f), new Vector2(0.92f, 0.74f), 0, 0, 0, 0);
        var commandLayout = commandPanel.AddComponent<VerticalLayoutGroup>();
        commandLayout.padding = new RectOffset(20, 20, 18, 18);
        commandLayout.spacing = 8;
        commandLayout.childControlWidth = true;
        commandLayout.childControlHeight = true;
        commandLayout.childForceExpandWidth = true;
        commandLayout.childForceExpandHeight = false;
        var menuTitle = CreateText(commandPanel.transform, "상담실 입장", 22, FontStyle.Bold, new Color32(255, 241, 212, 255));
        SetLayout(menuTitle.gameObject, 1, 0, -1, 30);
        CreateSkinnedButton(commandPanel.transform, "새 게임", "VN/UI/choice_card_intervention", Good, StartCampaignRoute, 50f, 18);
        CreateSkinnedButton(commandPanel.transform, "이어하기", "VN/UI/choice_card_question", Warm, ContinueFromLastCase, 50f, 18);
        CreateSkinnedButton(commandPanel.transform, "사례 선택", "VN/UI/choice_card_question", Accent, ShowCaseBrowser, 50f, 18);
        CreateSkinnedButton(commandPanel.transform, "저장 / 불러오기", "VN/UI/choice_card_question", MutedInk, ShowSaveLoad, 50f, 18);
        CreateSkinnedButton(commandPanel.transform, "학습 기록", "VN/UI/choice_card_question", MutedInk, ShowDashboard, 50f, 18);
        CreateSkinnedButton(commandPanel.transform, aiSupervisorEnabled ? "AI 슈퍼바이저 켜짐" : "AI 슈퍼바이저 꺼짐", "VN/UI/choice_card_question", aiSupervisorEnabled ? Good : Accent, () =>
        {
            aiSupervisorEnabled = !aiSupervisorEnabled;
            WriteSaveSlot(1);
            ShowMainMenu();
        }, 50f, 18);

        var footer = CreateAbsolutePanel(root.transform, "Title Footer", new Color32(0, 0, 0, 0), new Vector2(0.06f, 0.055f), new Vector2(0.93f, 0.11f), 0, 0, 0, 0);
        var footerText = CreateText(footer.transform, "자동 저장 슬롯 1 · 저장된 회기 " + logs.Count + "건", 17, FontStyle.Bold, new Color32(226, 232, 236, 235));
        footerText.alignment = TextAnchor.MiddleRight;
        Stretch(footerText.gameObject, 0, 0, 0, 0);
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
        var root = CreateVnRoot("case-browser", "VN/Backgrounds/counseling_room_day");
        int maxPage = Mathf.Max(0, Mathf.CeilToInt(cases.Count / (float)CasesPerBrowserPage) - 1);
        caseBrowserPage = Mathf.Clamp(caseBrowserPage, 0, maxPage);

        CreateAbsolutePanel(root.transform, "Lobby Shade", new Color32(7, 10, 14, 118), Vector2.zero, Vector2.one, 0, 0, 0, 0);
        CreateVnHud(root.transform, "센터 로비", "오늘의 사례 보드", "파일 " + (caseBrowserPage + 1) + "/" + (maxPage + 1));

        var titleBlock = CreateAbsolutePanel(root.transform, "Lobby Title", new Color32(0, 0, 0, 0), new Vector2(0.06f, 0.80f), new Vector2(0.72f, 0.91f), 0, 0, 0, 0);
        var titleLayout = titleBlock.AddComponent<VerticalLayoutGroup>();
        titleLayout.spacing = 4;
        titleLayout.childControlWidth = true;
        titleLayout.childControlHeight = true;
        titleLayout.childForceExpandWidth = true;
        titleLayout.childForceExpandHeight = false;
        var title = CreateText(titleBlock.transform, "오늘의 접수 파일", 37, FontStyle.Bold, Color.white);
        SetLayout(title.gameObject, 1, 0, -1, 60);
        title.gameObject.AddComponent<Shadow>().effectColor = new Color32(0, 0, 0, 190);
        var subtitle = CreateText(titleBlock.transform, "가족을 만나기 전에 파일을 고르고, 어떤 렌즈로 들을지 정합니다.", 18, FontStyle.Normal, new Color32(226, 232, 236, 245));
        SetLayout(subtitle.gameObject, 1, 0, -1, 30);
        subtitle.gameObject.AddComponent<Shadow>().effectColor = new Color32(0, 0, 0, 170);

        var board = CreateAbsoluteSkinnedPanel(root.transform, "Reception Board", "VN/UI/case_file_panel", new Color32(238, 232, 218, 244), new Vector2(0.055f, 0.12f), new Vector2(0.64f, 0.80f), 0, 0, 0, 0);
        var boardLayout = board.AddComponent<VerticalLayoutGroup>();
        boardLayout.padding = new RectOffset(24, 24, 20, 20);
        boardLayout.spacing = 8;
        boardLayout.childControlWidth = true;
        boardLayout.childControlHeight = true;
        boardLayout.childForceExpandWidth = true;
        boardLayout.childForceExpandHeight = false;
        var boardTitle = CreateText(board.transform, "사례 보드 " + (caseBrowserPage + 1) + "/" + (maxPage + 1), 22, FontStyle.Bold, Ink);
        SetLayout(boardTitle.gameObject, 1, 0, -1, 34);

        var pageCases = cases.Skip(caseBrowserPage * CasesPerBrowserPage).Take(CasesPerBrowserPage).ToList();
        foreach (var caseData in pageCases)
        {
            FamilyCase captured = caseData;
            string theoryName = theories.First(t => t.id == caseData.recommendedTheoryId).name;
            string status = HasVnScript(caseData) ? "VN" : "훈련";
            string label = caseData.id + " · " + caseData.familyType + " · " + status + "\n" + caseData.presentingProblem + "  |  " + theoryName;
            CreateSkinnedButton(board.transform, label, "VN/UI/choice_card_question", caseData.isHandcrafted ? Good : Accent, () => BeginCaseIntake(captured), 68f, 17);
        }

        var nav = CreateHorizontal(board, "Case Browser Nav");
        nav.spacing = 8;
        CreateSkinnedButton(nav.transform, "이전", "VN/UI/choice_card_question", caseBrowserPage > 0 ? Accent : MutedInk, () =>
        {
            if (caseBrowserPage > 0) caseBrowserPage--;
            ShowCaseBrowser();
        }, 46f, 17);
        CreateSkinnedButton(nav.transform, "다음", "VN/UI/choice_card_question", caseBrowserPage < maxPage ? Accent : MutedInk, () =>
        {
            if (caseBrowserPage < maxPage) caseBrowserPage++;
            ShowCaseBrowser();
        }, 46f, 17);
        CreateSkinnedButton(nav.transform, "메인", "VN/UI/choice_card_question", MutedInk, ShowMainMenu, 46f, 17);

        FamilyCase featured = pageCases.Count > 0 ? pageCases[0] : cases.First();
        TherapyTheory featuredTheory = theories.First(t => t.id == featured.recommendedTheoryId);
        SupervisorProfile featuredSupervisor = GetSupervisorForTheory(featured.recommendedTheoryId);
        var detail = CreateAbsoluteSkinnedPanel(root.transform, "Selected File Preview", "VN/UI/case_file_panel", new Color32(18, 22, 28, 222), new Vector2(0.67f, 0.16f), new Vector2(0.93f, 0.76f), 0, 0, 0, 0);
        var detailLayout = detail.AddComponent<VerticalLayoutGroup>();
        detailLayout.padding = new RectOffset(20, 20, 18, 18);
        detailLayout.spacing = 8;
        detailLayout.childControlWidth = true;
        detailLayout.childControlHeight = true;
        detailLayout.childForceExpandWidth = true;
        detailLayout.childForceExpandHeight = false;
        var previewTitle = CreateText(detail.transform, "접수 미리보기", 18, FontStyle.Bold, Accent);
        SetLayout(previewTitle.gameObject, 1, 0, -1, 38);
        var previewCase = CreateText(detail.transform, featured.id + " · " + featured.familyType, 20, FontStyle.Bold, Ink);
        SetLayout(previewCase.gameObject, 1, 0, -1, 78);
        var previewProblem = CreateText(detail.transform, featured.presentingProblem, 16, FontStyle.Bold, Warm);
        SetLayout(previewProblem.gameObject, 1, 0, -1, 36);
        var previewMeta = CreateText(detail.transform, "추천 렌즈: " + featuredTheory.name + "\n담당: " + featuredSupervisor.name + "\n위험도 " + featured.riskLevel + " · 기록 " + logs.Count + "건", 15, FontStyle.Normal, MutedInk);
        SetLayout(previewMeta.gameObject, 1, 0, -1, 94);
        CreateSpacer(detail.transform, 6);
        CreateSkinnedButton(detail.transform, "파일 열기", "VN/UI/choice_card_intervention", Accent, () => BeginCaseIntake(featured), 44f, 15);
        if (HasVnScript(featured))
        {
            CreateSkinnedButton(detail.transform, "첫 회기 바로 시작", "VN/UI/choice_card_intervention", Good, () => BeginVnCase(featured), 44f, 15);
        }
        CreateSkinnedButton(detail.transform, "저장 / 불러오기", "VN/UI/choice_card_question", Warm, ShowSaveLoad, 42f, 14);
        CreateSkinnedButton(detail.transform, "기록실", "VN/UI/choice_card_question", MutedInk, ShowDashboard, 42f, 14);
    }

    private void ShowEthics()
    {
        ClearCanvas();
        var root = CreateVnRoot("ethics", "VN/Backgrounds/counseling_room_day");
        CreateAbsolutePanel(root.transform, "Ethics Shade", new Color32(8, 11, 15, 142), Vector2.zero, Vector2.one, 0, 0, 0, 0);
        CreateVnHud(root.transform, "센터 안내", "윤리·합성 데이터 고지", "Education Only");
        var card = CreateAbsoluteSkinnedPanel(root.transform, "Ethics Card", "VN/UI/session_result_sheet", new Color32(238, 232, 218, 248), new Vector2(0.18f, 0.10f), new Vector2(0.82f, 0.82f), 0, 0, 0, 0);
        var cardLayout = card.AddComponent<VerticalLayoutGroup>();
        cardLayout.padding = new RectOffset(30, 30, 26, 26);
        cardLayout.spacing = 10;
        cardLayout.childControlWidth = true;
        cardLayout.childControlHeight = true;
        cardLayout.childForceExpandWidth = true;
        cardLayout.childForceExpandHeight = false;
        var ethicsTitle = CreateText(card.transform, "중요 고지", 28, FontStyle.Bold, Ink);
        SetLayout(ethicsTitle.gameObject, 1, 0, -1, 42);
        var ethicsBody = CreateText(card.transform,
            "이 게임의 가족명, 사례, 대화, 위험도, 상담 기록은 모두 창작된 합성 데이터입니다. 실제 상담 원문, 개인정보, 실제 가족 사례, 영화/드라마 원작 사례를 포함하지 않습니다.\n\n" +
            "게임의 점수와 슈퍼비전 해설은 가족치료 개념 학습을 돕기 위한 교육용 피드백입니다. 실제 상담, 의료, 법률, 복지 판단을 대체하지 않습니다.\n\n" +
            "선택형 AI 슈퍼바이저는 추후 확장 지점이며, 활성화하더라도 공식 점수 판정에는 영향을 주지 않는 참고 코멘트로만 사용합니다.", 22, FontStyle.Normal, Ink);
        SetLayout(ethicsBody.gameObject, 1, 0, -1, 310);
        CreateSpacer(card.transform, 10);
        CreateSkinnedButton(card.transform, "확인", "VN/UI/choice_card_intervention", Accent, ShowMainMenu, 54f, 19);
    }

    private void ShowSaveLoad()
    {
        ClearCanvas();
        var root = CreateVnRoot("save-load", "VN/Backgrounds/counseling_room_day");
        CreateAbsolutePanel(root.transform, "Save Shade", new Color32(8, 11, 15, 138), Vector2.zero, Vector2.one, 0, 0, 0, 0);
        CreateVnHud(root.transform, "기록 보관함", "저장 / 불러오기", "Local Save");

        var left = CreateAbsoluteSkinnedPanel(root.transform, "Save File Drawer", "VN/UI/session_result_sheet", new Color32(238, 232, 218, 246), new Vector2(0.055f, 0.12f), new Vector2(0.66f, 0.82f), 0, 0, 0, 0);
        var leftLayout = left.AddComponent<VerticalLayoutGroup>();
        leftLayout.padding = new RectOffset(28, 28, 24, 24);
        leftLayout.spacing = 8;
        leftLayout.childControlWidth = true;
        leftLayout.childControlHeight = true;
        leftLayout.childForceExpandWidth = true;
        leftLayout.childForceExpandHeight = false;
        var saveTitle = CreateText(left.transform, "수련 파일 보관함", 26, FontStyle.Bold, Ink);
        SetLayout(saveTitle.gameObject, 1, 0, -1, 48);
        var saveDesc = CreateText(left.transform, "슬롯 1은 회기 종료 때 자동 저장됩니다. 슬롯 2~3은 사용자가 직접 백업할 수 있습니다.", 17, FontStyle.Normal, MutedInk);
        SetLayout(saveDesc.gameObject, 1, 0, -1, 50);
        CreateSpacer(left.transform, 4);
        for (int slot = 1; slot <= 3; slot++)
        {
            int captured = slot;
            GameSaveData preview = ReadSaveSlot(slot);
            var slotPanel = CreateCard(left.transform, "Save Slot " + slot, preview == null ? new Color32(220, 213, 196, 222) : new Color32(247, 239, 219, 236));
            SetLayout(slotPanel, 1, 0, -1, 122);
            var slotLayout = slotPanel.GetComponent<VerticalLayoutGroup>();
            slotLayout.padding = new RectOffset(16, 16, 12, 12);
            slotLayout.spacing = 5;
            string status = preview == null
                ? "빈 슬롯"
                : preview.savedAt + " · 로그 " + (preview.logs == null ? 0 : preview.logs.Count) + "건 · 마지막 " + preview.lastCaseId;
            var slotTitle = CreateText(slotPanel.transform, "파일 " + slot + " · " + status, 17, FontStyle.Bold, preview == null ? MutedInk : Ink);
            SetLayout(slotTitle.gameObject, 1, 0, -1, 30);
            var row = CreateHorizontal(slotPanel, "Save Slot Actions " + slot);
            row.spacing = 8;
            row.childForceExpandWidth = true;
            CreateSkinnedButton(row.transform, "저장", "VN/UI/choice_card_intervention", Good, () =>
            {
                WriteSaveSlot(captured);
                ShowSaveLoad();
            }, 48f, 17);
            CreateSkinnedButton(row.transform, "불러오기", "VN/UI/choice_card_question", preview == null ? MutedInk : Accent, () =>
            {
                if (LoadSaveSlot(captured, true)) ShowMainMenu();
                else ShowSaveLoad();
            }, 48f, 17);
        }

        var right = CreateAbsoluteSkinnedPanel(root.transform, "Save Current Status", "VN/UI/supervisor_note_panel", new Color32(18, 22, 28, 226), new Vector2(0.69f, 0.18f), new Vector2(0.93f, 0.72f), 0, 0, 0, 0);
        var rightLayout = right.AddComponent<VerticalLayoutGroup>();
        rightLayout.padding = new RectOffset(20, 20, 18, 18);
        rightLayout.spacing = 8;
        rightLayout.childControlWidth = true;
        rightLayout.childControlHeight = true;
        rightLayout.childForceExpandWidth = true;
        rightLayout.childForceExpandHeight = false;
        var currentTitle = CreateText(right.transform, "현재 진행", 23, FontStyle.Bold, new Color32(255, 241, 212, 255));
        SetLayout(currentTitle.gameObject, 1, 0, -1, 38);
        var currentStats = CreateText(right.transform, "플레이 로그 " + logs.Count + "건\n완료 사례 " + logs.Select(l => l.caseId).Distinct().Count() + "개\nAI 슈퍼바이저 " + (aiSupervisorEnabled ? "ON" : "OFF"), 17, FontStyle.Bold, Color.white);
        SetLayout(currentStats.gameObject, 1, 0, -1, 70);
        var currentCaseText = CreateText(right.transform, "마지막 현재 사례\n" + (currentCase == null ? "없음" : currentCase.id + " · " + currentCase.familyType), 16, FontStyle.Normal, new Color32(226, 228, 232, 255));
        SetLayout(currentCaseText.gameObject, 1, 0, -1, 68);
        CreateSpacer(right.transform, 8);
        CreateSkinnedButton(right.transform, "센터 로비", "VN/UI/choice_card_question", Accent, ShowCaseBrowser, 50f, 18);
        CreateSkinnedButton(right.transform, "메인 메뉴", "VN/UI/choice_card_question", MutedInk, ShowMainMenu, 48f, 17);
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
        ShowCampaignBriefing();
    }

    private void ShowCampaignBriefing()
    {
        ClearCanvas();
        var caseData = cases.First(c => c.id == "FT-001");
        var root = CreateVnRoot("campaign-briefing", Ft001CgPath("t01_choice_idle"));
        CreateAbsolutePanel(root.transform, "Briefing Shade", new Color32(8, 11, 15, 128), Vector2.zero, Vector2.one, 0, 0, 0, 0);

        var panel = CreateAbsoluteSkinnedPanel(root.transform, "First Session Briefing", "VN/UI/case_file_panel", new Color32(238, 232, 218, 246), new Vector2(0.07f, 0.12f), new Vector2(0.58f, 0.84f), 0, 0, 0, 0);
        var layout = panel.AddComponent<VerticalLayoutGroup>();
        layout.padding = new RectOffset(26, 26, 22, 22);
        layout.spacing = 8;
        layout.childControlWidth = true;
        layout.childControlHeight = true;
        layout.childForceExpandWidth = true;
        layout.childForceExpandHeight = false;
        var campaignTitle = CreateText(panel.transform, "첫 회기 브리핑", 29, FontStyle.Bold, Ink);
        SetLayout(campaignTitle.gameObject, 1, 0, -1, 52);
        var campaignCase = CreateText(panel.transform, caseData.id + " · " + caseData.familyType, 18, FontStyle.Bold, Warm);
        SetLayout(campaignCase.gameObject, 1, 0, -1, 30);
        var campaignBody = CreateText(panel.transform,
            "당신은 가족치료 수련생입니다. 지금부터 가족의 말을 듣고, 한 사람을 문제로 고정하지 않은 채 반복되는 장면을 찾아야 합니다.",
            18, FontStyle.Normal, Ink);
        SetLayout(campaignBody.gameObject, 1, 0, -1, 66);
        var campaignStepsTitle = CreateText(panel.transform, "진행 방식", 19, FontStyle.Bold, Ink);
        SetLayout(campaignStepsTitle.gameObject, 1, 0, -1, 32);
        var campaignSteps = CreateText(panel.transform,
            "1. 가족의 대화를 읽습니다.\n" +
            "2. 슈퍼바이저 노트로 관찰 초점을 확인합니다.\n" +
            "3. 상담자 개입을 선택합니다.\n" +
            "4. 가족 반응과 점수 변화를 확인합니다.",
            17, FontStyle.Normal, MutedInk);
        SetLayout(campaignSteps.gameObject, 1, 0, -1, 126);
        var campaignGoal = CreateText(panel.transform, "첫 목표: 잘못한 사람보다 서로의 반응이 다음 반응을 어떻게 부르는지 보기", 16, FontStyle.Bold, Accent);
        SetLayout(campaignGoal.gameObject, 1, 0, -1, 48);
        CreateSkinnedButton(panel.transform, "첫 회기 시작", "VN/UI/choice_card_intervention", Good, () => BeginVnCase(caseData), 48f, 17);
        CreateSkinnedButton(panel.transform, "사례 파일 먼저 보기", "VN/UI/choice_card_question", Accent, () => BeginCaseIntake(caseData), 48f, 17);
        CreateSkinnedButton(panel.transform, "메인으로", "VN/UI/choice_card_question", MutedInk, ShowMainMenu, 46f, 16);
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
        var root = CreateVnRoot("case-intake", currentCase.id == "FT-001" ? Ft001CgPath("t01_choice_idle") : "VN/Backgrounds/counseling_room_day");
        CreateAbsolutePanel(root.transform, "Case File Shade", new Color32(8, 11, 15, 128), Vector2.zero, Vector2.one, 0, 0, 0, 0);
        CreateVnHud(root.transform, currentCase.id, "사례 파일", "회기 준비");

        var file = CreateAbsoluteSkinnedPanel(root.transform, "Case File", "VN/UI/case_file_panel", new Color32(238, 232, 218, 246), new Vector2(0.055f, 0.09f), new Vector2(0.60f, 0.82f), 0, 0, 0, 0);
        var fileLayout = file.AddComponent<VerticalLayoutGroup>();
        fileLayout.padding = new RectOffset(24, 24, 20, 20);
        fileLayout.spacing = 6;
        fileLayout.childControlWidth = true;
        fileLayout.childControlHeight = true;
        fileLayout.childForceExpandWidth = true;
        fileLayout.childForceExpandHeight = false;
        var caseTitle = CreateText(file.transform, currentCase.id + " · " + currentCase.familyType, 24, FontStyle.Bold, Ink);
        SetLayout(caseTitle.gameObject, 1, 0, -1, 38);
        var problem = CreateText(file.transform, currentCase.presentingProblem, 17, FontStyle.Bold, Warm);
        SetLayout(problem.gameObject, 1, 0, -1, 28);
        var context = CreateText(file.transform, currentCase.context, 15, FontStyle.Normal, Ink);
        SetLayout(context.gameObject, 1, 0, -1, 72);
        var mapTitle = CreateText(file.transform, "가족 구성 / 관계도", 17, FontStyle.Bold, Ink);
        SetLayout(mapTitle.gameObject, 1, 0, -1, 26);
        var map = CreateText(file.transform, currentCase.familyMap, 14, FontStyle.Normal, MutedInk);
        SetLayout(map.gameObject, 1, 0, -1, 52);
        var cueTitle = CreateText(file.transform, "회기에서 들어야 할 단서", 17, FontStyle.Bold, Ink);
        SetLayout(cueTitle.gameObject, 1, 0, -1, 26);
        var cues = CreateText(file.transform, "위험도 " + currentCase.riskLevel + " · " + string.Join(", ", currentCase.dynamicsTags), 14, FontStyle.Normal, MutedInk);
        SetLayout(cues.gameObject, 1, 0, -1, 30);
        var objective = CreateText(file.transform, currentCase.learningObjective, 14, FontStyle.Normal, MutedInk);
        SetLayout(objective.gameObject, 1, 0, -1, 46);
        var utterTitle = CreateText(file.transform, "초기 발화", 17, FontStyle.Bold, Ink);
        SetLayout(utterTitle.gameObject, 1, 0, -1, 26);
        foreach (string line in currentCase.familyDialogue.Take(2))
        {
            var utterance = CreateText(file.transform, line, 14, FontStyle.Normal, MutedInk);
            SetLayout(utterance.gameObject, 1, 0, -1, 38);
        }

        var lens = CreateAbsoluteSkinnedPanel(root.transform, "Theory Lens", "VN/UI/supervisor_note_panel", new Color32(18, 22, 28, 226), new Vector2(0.62f, 0.17f), new Vector2(0.93f, 0.84f), 0, 0, 0, 0);
        var lensLayout = lens.AddComponent<VerticalLayoutGroup>();
        lensLayout.padding = new RectOffset(20, 20, 18, 18);
        lensLayout.spacing = 8;
        lensLayout.childControlWidth = true;
        lensLayout.childControlHeight = true;
        lensLayout.childForceExpandWidth = true;
        lensLayout.childForceExpandHeight = false;
        SupervisorProfile supervisor = GetSupervisorForTheory(selectedTheory.id);
        var briefingTitle = CreateText(lens.transform, "슈퍼바이저 브리핑", 22, FontStyle.Bold, new Color32(255, 241, 212, 255));
        SetLayout(briefingTitle.gameObject, 1, 0, -1, 48);
        var supervisorName = CreateText(lens.transform, supervisor.name + " · " + selectedTheory.name, 17, FontStyle.Bold, Color.white);
        SetLayout(supervisorName.gameObject, 1, 0, -1, 34);
        var briefing = CreateText(lens.transform, supervisor.openingLine, 15, FontStyle.Normal, new Color32(226, 232, 236, 255));
        SetLayout(briefing.gameObject, 1, 0, -1, 64);
        var tagTitle = CreateText(lens.transform, "가설 태그", 18, FontStyle.Bold, new Color32(255, 241, 212, 255));
        SetLayout(tagTitle.gameObject, 1, 0, -1, 36);
        CreateCaseTagRow(lens.transform, "IP 지정", currentCase.dynamicsTags.Any(t => t.Contains("IP")) ? Good : Accent);
        CreateCaseTagRow(lens.transform, "순환 패턴", currentCase.dynamicsTags.Any(t => t.Contains("순환")) ? Good : Accent);
        CreateCaseTagRow(lens.transform, "경계 혼란", currentCase.dynamicsTags.Any(t => t.Contains("경계")) ? Good : Accent);
        CreateCaseTagRow(lens.transform, "삼각관계", currentCase.dynamicsTags.Any(t => t.Contains("삼각")) ? Good : Accent);
        var goalTitle = CreateText(lens.transform, "회기 목표", 18, FontStyle.Bold, new Color32(255, 241, 212, 255));
        SetLayout(goalTitle.gameObject, 1, 0, -1, 36);
        var goal = CreateText(lens.transform, "누가 문제인지 정하기보다, 아침 장면의 반복 순서를 가족이 함께 보게 한다.", 15, FontStyle.Bold, new Color32(226, 232, 236, 255));
        SetLayout(goal.gameObject, 1, 0, -1, 64);
        CreateSkinnedButton(lens.transform, HasVnScript(currentCase) ? "회기 입장" : "훈련 회기 입장", "VN/UI/choice_card_intervention", Warm, StartCurrentCaseSession, 54f, 18);
        CreateSkinnedButton(lens.transform, "로비로", "VN/UI/choice_card_question", MutedInk, ShowCaseBrowser, 42f, 14);
    }

    private void CreateCaseTagRow(Transform parent, string label, Color color)
    {
        var row = CreateSkinnedInlinePanel(parent, "Case Tag " + label, "VN/UI/choice_card_question", new Color32(29, 35, 42, 224), 1, 0, -1, 32);
        AddButtonAccentStrip(row.transform, color);
        var text = CreateText(row.transform, label, 14, FontStyle.Bold, new Color32(235, 238, 240, 255));
        text.alignment = TextAnchor.MiddleLeft;
        Stretch(text.gameObject, 18, 0, 10, 0);
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
        currentVnIntroLineIndex = 0;
        sessionScore = 0;
        trustScore = 50;
        safetyScore = 50;
        insightScore = 50;
        currentSelections.Clear();
        currentVnIntroLines.Clear();

        if (currentVnScript == null || currentVnScript.turns == null || currentVnScript.turns.Count == 0)
        {
            ShowSessionTurn();
            return;
        }

        currentVnIntroLines.AddRange(BuildVnIntroLines());
        if (currentVnIntroLines.Count > 0)
        {
            ShowVnIntroLine();
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

    private string GetVnSceneBackground(string speakerId, string expressionId, string explicitCgResourcePath = "")
    {
        if (!string.IsNullOrEmpty(explicitCgResourcePath) && LoadVnTexture(explicitCgResourcePath) != null)
        {
            return explicitCgResourcePath;
        }

        if (currentCase == null || currentCase.id != "FT-001")
        {
            return currentVnScript != null ? currentVnScript.backgroundId : "VN/Backgrounds/counseling_room_day";
        }

        string expression = expressionId ?? "";
        if (expression == "softened" || expression == "relieved" || expression == "listening" || expression == "approving")
        {
            return "VN/EventCG/ft001_group_session_softening";
        }
        if (expression == "defensive" || expression == "scared" || expression == "withdrawn" || expression == "critical" || expression == "stubborn" || expression == "anxious")
        {
            return "VN/EventCG/ft001_group_session_tension";
        }

        switch (speakerId)
        {
            case "ft001_mother":
                return "VN/EventCG/ft001_group_session_mother_speaking";
            case "ft001_child":
                return "VN/EventCG/ft001_group_session_child_speaking";
            case "ft001_teacher":
                return "VN/EventCG/ft001_group_session_teacher_speaking";
            case "ft001_grandmother":
                return "VN/EventCG/ft001_group_session_grandmother_speaking";
            default:
                return "VN/EventCG/ft001_group_session_neutral";
        }
    }

    private string GetVnChoiceDeckBackground(VnTurn turn, string activeSpeakerId)
    {
        if (currentCase != null && currentCase.id == "FT-001" && turn != null)
        {
            int turnIndex = currentVnScript != null && currentVnScript.turns != null ? currentVnScript.turns.IndexOf(turn) + 1 : currentTurn + 1;
            string explicitPath = Ft001CgPath("t" + Mathf.Max(1, turnIndex).ToString("00", CultureInfo.InvariantCulture) + "_choice_idle");
            if (LoadVnTexture(explicitPath) != null)
            {
                return explicitPath;
            }
        }
        else if (currentCase != null && turn != null)
        {
            int turnIndex = currentVnScript != null && currentVnScript.turns != null ? currentVnScript.turns.IndexOf(turn) + 1 : currentTurn + 1;
            string explicitPath = BuildConventionCgPath(currentCase.id, "t" + Mathf.Max(1, turnIndex).ToString("00", CultureInfo.InvariantCulture) + "_choice_idle");
            if (LoadVnTexture(explicitPath) != null)
            {
                return explicitPath;
            }
        }

        return GetVnSceneBackground(activeSpeakerId, "neutral");
    }

    private IEnumerable<VnDialogueLine> BuildVnIntroLines()
    {
        if (currentVnScript == null) yield break;
        if (currentCase != null && currentCase.id == "FT-001")
        {
            yield return Ft001Line("intro_01_mother_neutral", "ft001_mother", "neutral", "left", "주형이 엄마 박성빈입니다. 밤에는 일하고, 아침에는 주형이를 학교에 보내려고 애쓰는데 매번 같은 자리에서 무너집니다.", "상담자는 가족을 평가하기보다 각자의 자리와 부담을 먼저 듣습니다.");
            yield return Ft001Line("intro_02_child_neutral", "ft001_child", "neutral", "center", "해솔초등학교 4학년 이주형입니다. 학교에 가야 하는 건 아는데, 아침이 되면 배가 아프고 엄마가 나가버릴 것 같아서 무서워요.", "이주형은 남자 초등학생입니다. 등교 거부를 의지 문제로 단정하지 마세요.");
            yield return Ft001Line("intro_03_grandmother_neutral", "ft001_grandmother", "neutral", "right", "오선진입니다. 주형이 할머니입니다. 도와주고 싶은데 제 말이 자꾸 잔소리처럼 들리는 것 같습니다.", "외조모의 비판적 말투 아래에 있는 걱정을 분리해서 들어야 합니다.");
            yield return Ft001Line("intro_04_teacher_neutral", "ft001_teacher", "neutral", "right", "서건창입니다. 주형이 담임입니다. 학교 절차도 챙겨야 하지만, 아이가 왜 멈추는지도 함께 알고 싶습니다.", "서건창은 남자 담임교사입니다. 학교도 가족 순환에 영향을 주는 외부 체계입니다.");
            yield return Ft001Line("intro_05_supervisor_explaining", "supervisor_system", "explaining", "supervisor", "김혜성입니다. 오늘 목표는 누가 문제인지 찾는 것이 아니라, 아침 장면이 어떤 순서로 반복되는지 보는 것입니다.", "가족체계 기본 렌즈로 첫 회기를 시작합니다.");
            yield break;
        }
        if (currentCase != null && currentCase.id == "FT-002")
        {
            yield return new VnDialogueLine("ft002_grandmother", "neutral", "left", "준현이 할머니 김선기입니다. 아들이 떠난 뒤로 이 손자만큼은 똑바로 키우려는데, 요즘은 밤마다 마음을 졸입니다.", "조모의 통제 아래에 있는 상실과 걱정을 함께 들어야 합니다.");
            yield return new VnDialogueLine("ft002_grandson", "neutral", "center", "한울중학교 2학년 박준현입니다. 늦게 들어가는 건 맞는데, 집에 들어가면 바로 검사부터라서 그냥 밖이 편해요.", "박준현은 남자 중학생입니다. 야간 귀가 지연을 단순한 반항으로 단정하지 마세요.");
            yield return new VnDialogueLine("ft002_grandfather", "neutral", "right", "준현이 할아버지 박석민입니다. 둘이 부딪치면 저는 늘 말리기만 했는데, 그게 도움이 됐는지는 모르겠습니다.", "조부의 회피적 중재가 갈등 구도에서 어떤 자리인지 보세요.");
            yield return new VnDialogueLine("supervisor_bowen", "explaining", "supervisor", "안우진입니다. 오늘은 누구를 고칠지가 아니라, 이 가족의 걱정과 통제가 누구를 거쳐 도는지부터 조용히 따라가 보겠습니다.", "Bowen 다세대 렌즈로 첫 회기를 시작합니다.");
            yield break;
        }

        foreach (string id in currentVnScript.characters.Where(id => !id.StartsWith("supervisor_", StringComparison.OrdinalIgnoreCase)).Take(3))
        {
            VnCharacterProfile profile = GetVnCharacter(id);
            if (profile == null) continue;
            yield return new VnDialogueLine(id, profile.defaultExpression, profile.defaultPosition, profile.displayName + "입니다. 오늘은 제 입장에서 무슨 일이 있었는지 이야기해보려고 왔습니다.", "등장인물의 자리를 먼저 확인한 뒤 회기 대화로 들어갑니다.");
        }
    }

    private void ShowVnIntroLine()
    {
        if (currentVnIntroLineIndex >= currentVnIntroLines.Count)
        {
            currentVnLineIndex = 0;
            ShowVnSessionTurn();
            return;
        }

        VnDialogueLine line = currentVnIntroLines[currentVnIntroLineIndex];
        var root = CreateVnRoot("vn-introduction", GetVnSceneBackground(line.speakerId, line.expressionId, line.cgResourcePath));
        CreateVnHud(root.transform, currentCase.id, "등장인물 소개", "소개");
        CreateVnStage(root.transform, line.speakerId, line.expressionId);
        if (!string.IsNullOrEmpty(line.supervisorNote))
        {
            CreateVnSupervisorNote(root.transform, line.supervisorNote);
        }
        CreateVnDialogueBox(root.transform, line.speakerId, line.text, currentVnIntroLineIndex + 1, currentVnIntroLines.Count, () =>
        {
            currentVnIntroLineIndex++;
            ShowVnIntroLine();
        }, "다음 소개");
    }

    private List<VnDialogueLine> GetCurrentVnSetupLines(VnTurn turn)
    {
        var lines = new List<VnDialogueLine>();
        if (currentCase != null && currentCase.id == "FT-001")
        {
            lines.AddRange(BuildFt001ChoiceCarryoverLines());
        }
        else
        {
            lines.AddRange(BuildFocusedChoiceCarryoverLines());
        }
        if (turn != null && turn.setupLines != null)
        {
            lines.AddRange(turn.setupLines);
        }
        return lines;
    }

    private IEnumerable<VnDialogueLine> BuildFocusedChoiceCarryoverLines()
    {
        if (currentTurn <= 0 || currentSelections.Count == 0) yield break;

        SessionSelection previous = currentSelections.LastOrDefault(selection => selection.turn == currentTurn);
        if (previous == null || string.IsNullOrEmpty(previous.familyReaction)) yield break;

        string speakerId = string.IsNullOrEmpty(previous.reactionSpeakerId) ? FirstNonSupervisorCharacterId() : previous.reactionSpeakerId;
        string expressionId = string.IsNullOrEmpty(previous.reactionExpressionId) ? "neutral" : previous.reactionExpressionId;
        string note = previous.quality >= 80
            ? "이전 선택이 다음 장면의 방어를 낮추고, 가족이 같은 주제를 더 구체적으로 말하게 합니다."
            : previous.quality >= 55
                ? "이전 선택이 일부 도움이 되었지만, 아직 회복해야 할 긴장과 놓친 단서가 남아 있습니다."
                : "이전 선택의 여파가 다음 장면에 남아 있습니다. 관계가 닫힌 상태에서 다시 합류해야 합니다.";
        yield return new VnDialogueLine("session_reaction", expressionId, "center", "방금 개입 이후, 상담실 분위기가 이렇게 바뀝니다. " + previous.familyReaction, note);
    }

    private string FirstNonSupervisorCharacterId()
    {
        if (currentVnScript == null || currentVnScript.characters == null) return "generic_guardian";
        return currentVnScript.characters.FirstOrDefault(id => !IsSupervisorCharacter(id)) ?? "generic_guardian";
    }

    private IEnumerable<VnDialogueLine> BuildFt001ChoiceCarryoverLines()
    {
        if (currentTurn <= 0 || currentSelections.Count == 0) yield break;

        SessionSelection previous = currentSelections.LastOrDefault(selection => selection.turn == currentTurn);
        if (previous == null) yield break;

        bool strong = previous.quality >= 80;
        bool strained = previous.quality < 55;
        string prefix = "방금 선택의 여파: ";

        if (currentTurn == 1)
        {
            if (strong)
            {
                yield return Ft001Line("t02_l00_branch_mother_open", "ft001_mother", "softened", "left", "아까 각자 걱정을 먼저 물어봐 주셔서 조금 덜 몰리는 느낌이었어요. 그래서 아침 장면을 더 차분히 떠올려볼 수 있을 것 같습니다.", prefix + "초기 합류가 살아나 다음 장면의 방어가 낮아졌습니다.");
            }
            else if (strained)
            {
                yield return Ft001Line("t02_l00_branch_child_closed", "ft001_child", "withdrawn", "center", "아까는 제가 먼저 고쳐져야 하는 사람처럼 들렸어요. 그래서 지금은 뭘 말해도 또 혼날 것 같아요.", prefix + "IP 고정 선택 때문에 아이의 방어가 올라갔습니다. 다음 개입에서 관계 고리를 회복해야 합니다.");
            }
            else
            {
                yield return Ft001Line("t02_l00_branch_teacher_cautious", "ft001_teacher", "procedural", "right", "절차를 확인하는 건 필요하지만, 가족이 아직 긴장한 것 같습니다. 학교 이야기가 또 압박처럼 들릴까 조심스럽습니다.", prefix + "정보 확인은 되었지만 정서적 합류가 충분하지 않습니다.");
            }
        }
        else if (currentTurn == 2)
        {
            if (strong)
            {
                yield return Ft001Line("t03_l00_branch_child_links_pattern", "ft001_child", "hesitant", "center", "방금 그림으로 보니까 제가 멈추면 엄마가 더 급해지고, 엄마가 급해지면 저는 더 못 움직이는 것 같았어요.", prefix + "순환을 함께 본 선택이 다음 장면의 자기관찰을 늘렸습니다.");
            }
            else if (strained)
            {
                yield return Ft001Line("t03_l00_branch_mother_defensive", "ft001_mother", "defensive", "left", "제가 더 단호해야 한다는 말로 들리니까 또 제 책임인 것 같아요. 그러면 주형이 말을 들을 여유가 없어집니다.", prefix + "책임을 보호자에게 몰아 가족역동 논의가 좁아졌습니다.");
            }
            else
            {
                yield return Ft001Line("t03_l00_branch_mother_cautious", "ft001_mother", "worried", "left", "방금 이야기가 도움이 되긴 했는데, 아직 제가 무엇을 다르게 해야 하는지는 잘 모르겠습니다.", prefix + "부분적으로 열렸지만 다음 장면에서 더 구체적인 정서 단서가 필요합니다.");
            }
        }
        else if (currentTurn == 3)
        {
            if (strong)
            {
                yield return Ft001Line("t04_l00_branch_grandmother_softened", "ft001_grandmother", "softened", "right", "내 말이 걱정이라는 걸 알아주니 조금 덜 억울하네요. 성빈이를 밀어붙이기보다 내가 어떤 식으로 도울 수 있을지 듣고 싶습니다.", prefix + "정서 반영이 외조모의 방어를 낮춰 핵심 개입으로 넘어갈 공간이 생겼습니다.");
            }
            else if (strained)
            {
                yield return Ft001Line("t04_l00_branch_grandmother_stubborn", "ft001_grandmother", "stubborn", "right", "제 말투만 문제라고 하시면 저는 더 할 말이 없습니다. 저는 정말 걱정돼서 그런 건데요.", prefix + "직접 지적이 방어를 키웠습니다. 핵심 개입 전에 합류 회복이 필요합니다.");
            }
            else
            {
                yield return Ft001Line("t04_l00_branch_child_exception", "ft001_child", "hesitant", "center", "덜 힘들었던 날을 생각해보니, 엄마가 바로 재촉하지 않았던 아침은 조금 나았어요.", prefix + "예외 탐색이 다음 핵심 개입의 단서로 이어집니다.");
            }
        }
        else if (currentTurn == 4)
        {
            if (strong)
            {
                yield return Ft001Line("t05_l00_branch_teacher_adjusts", "ft001_teacher", "softened", "right", "방금 질문을 듣고 보니 학교 연락도 가족의 압박을 키울 수 있겠네요. 다음 주에는 연락 방식부터 바꿔보겠습니다.", prefix + "핵심 순환질문이 외부 체계의 협력까지 끌어냈습니다.");
            }
            else if (strained)
            {
                yield return Ft001Line("t05_l00_branch_child_scared", "ft001_child", "scared", "center", "또 제가 치료받아야 하는 사람처럼 된 것 같아요. 그러면 엄마랑 학교가 왜 더 무서워지는지는 말하기 어렵습니다.", prefix + "성급한 진단이 가족 순환을 다시 아이 개인 문제로 좁혔습니다.");
            }
            else
            {
                yield return Ft001Line("t05_l00_branch_mother_anxious", "ft001_mother", "anxious", "left", "행동계약을 하면 당장은 정리될 것 같지만, 실패하면 또 제가 못 지킨 사람이 될까 봐 걱정됩니다.", prefix + "구조화는 되었지만 가족이 감당 가능한 과제로 낮춰야 합니다.");
            }
        }
    }

    private void ShowVnSessionTurn()
    {
        if (currentVnScript == null || currentTurn >= currentVnScript.turns.Count)
        {
            SaveSessionLog();
            ShowVnEnding();
            return;
        }

        VnTurn turn = currentVnScript.turns[currentTurn];
        List<VnDialogueLine> setupLines = GetCurrentVnSetupLines(turn);
        if (setupLines.Count == 0)
        {
            ShowVnChoiceDeck(turn);
            return;
        }

        currentVnLineIndex = Mathf.Clamp(currentVnLineIndex, 0, setupLines.Count - 1);
        VnDialogueLine line = setupLines[currentVnLineIndex];
        var root = CreateVnRoot("vn-session", GetVnSceneBackground(line.speakerId, line.expressionId, line.cgResourcePath));
        CreateVnHud(root.transform, currentCase.id, turn.title, "회기 진행");
        CreateVnStage(root.transform, line.speakerId, line.expressionId);
        if (!string.IsNullOrEmpty(line.supervisorNote))
        {
            CreateVnSupervisorNote(root.transform, line.supervisorNote);
        }
        CreateVnDialogueBox(root.transform, line.speakerId, line.text, currentVnLineIndex + 1, setupLines.Count, () =>
        {
            if (currentVnLineIndex < setupLines.Count - 1)
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
        List<VnDialogueLine> setupLines = GetCurrentVnSetupLines(turn);
        string activeSpeaker = setupLines.Count > 0
            ? setupLines.LastOrDefault(line => !IsSupervisorCharacter(line.speakerId))?.speakerId ?? "ft001_child"
            : "ft001_child";
        var root = CreateVnRoot("vn-choices", GetVnChoiceDeckBackground(turn, activeSpeaker));
        CreateVnHud(root.transform, currentCase.id, turn.title, "개입 선택");
        CreateVnStage(root.transform, activeSpeaker, "neutral");

        var panel = CreateAbsoluteSkinnedPanel(root.transform, "Intervention Deck", "VN/UI/case_file_panel", new Color32(238, 232, 218, 246), new Vector2(0.065f, 0.045f), new Vector2(0.935f, 0.445f), 0, 0, 0, 0);
        var layout = panel.AddComponent<VerticalLayoutGroup>();
        layout.padding = new RectOffset(22, 22, 14, 14);
        layout.spacing = 7;
        layout.childControlWidth = true;
        layout.childControlHeight = true;
        layout.childForceExpandWidth = true;
        layout.childForceExpandHeight = false;
        var title = CreateText(panel.transform, "상담자 개입 선택", 21, FontStyle.Bold, Ink);
        SetLayout(title.gameObject, 1, 0, -1, 32);
        var subtitle = CreateText(panel.transform, "지금 가족에게 실제로 건넬 말을 고르세요. 이론명과 평가는 선택 후 확인합니다.", 14, FontStyle.Normal, MutedInk);
        SetLayout(subtitle.gameObject, 1, 0, -1, 26);
        foreach (VnChoice choice in turn.choices)
        {
            VnChoice captured = choice;
            CreateInterventionChoiceRow(panel.transform, choice, () => ApplyVnChoice(captured));
        }
    }

    private void CreateInterventionChoiceRow(Transform parent, VnChoice choice, Action action)
    {
        var row = new GameObject("Intervention Choice Row");
        row.transform.SetParent(parent, false);
        var image = row.AddComponent<Image>();
        image.color = new Color32(28, 34, 40, 232);
        var outline = row.AddComponent<Outline>();
        outline.effectColor = new Color32(80, 153, 171, 120);
        outline.effectDistance = new Vector2(1, -1);
        var button = row.AddComponent<Button>();
        button.targetGraphic = image;
        button.transition = Selectable.Transition.ColorTint;
        button.colors = new ColorBlock
        {
            normalColor = new Color32(28, 34, 40, 232),
            highlightedColor = new Color32(44, 60, 68, 248),
            pressedColor = new Color32(63, 86, 96, 255),
            selectedColor = new Color32(52, 73, 82, 255),
            disabledColor = new Color32(24, 26, 30, 150),
            colorMultiplier = 1f,
            fadeDuration = 0.08f
        };
        button.onClick.AddListener(() => action());
        SetLayout(row, 1, 0, -1, 62);
        AddButtonAccentStrip(row.transform, Accent);

        var layout = row.AddComponent<VerticalLayoutGroup>();
        layout.padding = new RectOffset(19, 16, 7, 7);
        layout.spacing = 2;
        layout.childControlWidth = true;
        layout.childControlHeight = true;
        layout.childForceExpandWidth = true;
        layout.childForceExpandHeight = false;

        var speech = CreateText(row.transform, choice.label, 16, FontStyle.Bold, new Color32(247, 249, 250, 255));
        speech.alignment = TextAnchor.MiddleLeft;
        SetLayout(speech.gameObject, 1, 0, -1, 48);
        speech.resizeTextMinSize = 13;
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
            routeQuality = choice.quality,
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
        var root = CreateVnRoot("vn-reaction", GetVnSceneBackground(choice.reactionSpeakerId, choice.reactionExpressionId, choice.reactionCgResourcePath));
        CreateVnHud(root.transform, currentCase.id, "가족 반응", completed ? "회기 종료 전 복기" : "다음 장면으로");
        CreateVnStage(root.transform, choice.reactionSpeakerId, choice.reactionExpressionId);
        CreateVnSupervisorNote(root.transform, choice.feedback);
        CreateReactionImpactStrip(root.transform);
        CreateVnDialogueBox(root.transform, choice.reactionSpeakerId, choice.familyReaction, currentTurn + 1, SessionTurnCount, () =>
        {
            currentTurn++;
            currentVnLineIndex = 0;
            if (currentTurn >= SessionTurnCount)
            {
                SaveSessionLog();
                ShowVnEnding();
            }
            else
            {
                ShowVnSessionTurn();
            }
        }, null, "회기 반응");
    }

    private void CreateReactionImpactStrip(Transform parent)
    {
        SessionSelection latest = currentSelections.LastOrDefault();
        if (latest == null) return;

        var strip = CreateAbsoluteSkinnedPanel(parent, "Reaction Impact", "VN/UI/supervisor_note_panel", new Color32(14, 18, 23, 206), new Vector2(0.055f, 0.79f), new Vector2(0.445f, 0.86f), 0, 0, 0, 0);
        var layout = strip.AddComponent<HorizontalLayoutGroup>();
        layout.padding = new RectOffset(12, 12, 6, 6);
        layout.spacing = 8;
        layout.childControlWidth = true;
        layout.childControlHeight = true;
        layout.childForceExpandWidth = true;
        layout.childForceExpandHeight = true;
        CreateImpactText(strip.transform, "신뢰", latest.trustDelta);
        CreateImpactText(strip.transform, "안전", latest.safetyDelta);
        CreateImpactText(strip.transform, "통찰", latest.insightDelta);
    }

    private void CreateImpactText(Transform parent, string label, int delta)
    {
        string sign = delta > 0 ? "+" : "";
        Color color = delta >= 0 ? new Color32(151, 219, 184, 255) : new Color32(231, 142, 135, 255);
        var text = CreateText(parent, label + " " + sign + delta, 13, FontStyle.Bold, color);
        text.alignment = TextAnchor.MiddleCenter;
        SetLayout(text.gameObject, 0, 1, 86, -1);
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
        var root = CreateVnRoot("session", "VN/Backgrounds/counseling_room_day");
        CreateAbsolutePanel(root.transform, "Generic Session Shade", new Color32(8, 11, 15, 138), Vector2.zero, Vector2.one, 0, 0, 0, 0);
        CreateVnHud(root.transform, currentCase.id, GetSceneTitle(), "회기 진행");

        var scene = CreateAbsoluteSkinnedPanel(root.transform, "Generic Session Scene", "VN/UI/supervisor_note_panel", new Color32(18, 22, 28, 226), new Vector2(0.055f, 0.43f), new Vector2(0.61f, 0.78f), 0, 0, 0, 0);
        var sceneLayout = scene.AddComponent<VerticalLayoutGroup>();
        sceneLayout.padding = new RectOffset(24, 24, 20, 20);
        sceneLayout.spacing = 8;
        sceneLayout.childControlWidth = true;
        sceneLayout.childControlHeight = true;
        sceneLayout.childForceExpandWidth = true;
        sceneLayout.childForceExpandHeight = false;
        CreateText(scene.transform, currentCase.familyType + " · " + selectedTheory.name, 23, FontStyle.Bold, new Color32(255, 241, 212, 255));
        CreateText(scene.transform, GetSceneNarration(), 18, FontStyle.Normal, new Color32(235, 237, 241, 255));
        CreateSpacer(scene.transform, 4);
        CreateText(scene.transform, currentCase.familyDialogue[Mathf.Min(currentTurn, currentCase.familyDialogue.Length - 1)], 23, FontStyle.Bold, Color.white);

        var note = CreateAbsoluteSkinnedPanel(root.transform, "Generic Supervisor Note", "VN/UI/supervisor_note_panel", new Color32(18, 22, 28, 218), new Vector2(0.64f, 0.50f), new Vector2(0.93f, 0.78f), 0, 0, 0, 0);
        var noteLayout = note.AddComponent<VerticalLayoutGroup>();
        noteLayout.padding = new RectOffset(18, 18, 16, 16);
        noteLayout.spacing = 6;
        noteLayout.childControlWidth = true;
        noteLayout.childControlHeight = true;
        noteLayout.childForceExpandWidth = true;
        noteLayout.childForceExpandHeight = false;
        CreateText(note.transform, "슈퍼바이저 노트", 21, FontStyle.Bold, new Color32(255, 241, 212, 255));
        CreateText(note.transform, currentCase.supervisorCue, 17, FontStyle.Normal, new Color32(226, 228, 232, 255));
        CreateText(note.transform, "신뢰 " + trustScore + " · 안전 " + safetyScore + " · 통찰 " + insightScore, 17, FontStyle.Bold, Accent);

        var choices = CreateAbsoluteSkinnedPanel(root.transform, "Generic Choices", "VN/UI/dialogue_box", new Color32(238, 232, 218, 246), new Vector2(0.055f, 0.04f), new Vector2(0.93f, 0.39f), 0, 0, 0, 0);
        var choiceLayout = choices.AddComponent<VerticalLayoutGroup>();
        choiceLayout.padding = new RectOffset(22, 22, 16, 16);
        choiceLayout.spacing = 7;
        choiceLayout.childControlWidth = true;
        choiceLayout.childControlHeight = true;
        choiceLayout.childForceExpandWidth = true;
        choiceLayout.childForceExpandHeight = false;
        var genericChoiceTitle = CreateText(choices.transform, "상담자 선택", 21, FontStyle.Bold, Ink);
        SetLayout(genericChoiceTitle.gameObject, 1, 0, -1, 36);
        foreach (var choice in BuildChoicesForCurrentTurn())
        {
            SessionChoice captured = choice;
            CreateSkinnedButton(choices.transform, choice.label, "VN/UI/choice_card_intervention", choice.quality >= 80 ? Good : choice.quality >= 55 ? Warn : Bad, () => ApplyChoice(captured), 46f, 16);
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
            routeFlags = BuildRouteFlagSummary(),
            endingId = ResolveCaseEndingId(),
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

    private string BuildRouteFlagSummary()
    {
        var flags = currentSelections
            .Where(s => !string.IsNullOrEmpty(s.interventionType))
            .Select(s => "T" + s.turn + ":" + s.interventionType)
            .ToArray();
        return flags.Length == 0 ? "아직 선택 기록 없음" : string.Join(" | ", flags);
    }

    private string ResolveCaseEndingId()
    {
        if (currentSelections.Count == 0) return "no_choices";

        int high = currentSelections.Count(s => GetRouteQuality(s) >= 80);
        int low = currentSelections.Count(s => GetRouteQuality(s) < 50);
        bool finalHigh = GetRouteQuality(currentSelections.Last()) >= 80;
        string joinedFlags = " " + string.Join(" ", currentSelections.Select(s => s.interventionType ?? "").ToArray()) + " ";
        string caseId = currentCase != null ? currentCase.id : "unknown_case";

        string[] selectedTokens = SplitRouteTokens(joinedFlags).ToArray();
        if (currentCase != null && IsSafetyCriticalCase(currentCase.id) && !HasRequiredSafetyTokens(currentCase.id, selectedTokens))
        {
            return currentCase.id + "_D_safety_unresolved";
        }

        string caseSpecific = ResolveCaseSpecificEndingId(caseId, joinedFlags, high, low, finalHigh);
        if (!string.IsNullOrEmpty(caseSpecific))
        {
            return caseSpecific;
        }

        if (low >= 2)
        {
            return currentCase.id + "_D_closed_or_harmful";
        }
        if (low == 1 && high >= 3 && finalHigh)
        {
            return currentCase.id + "_B_repaired";
        }
        if (high >= 4 && finalHigh)
        {
            return currentCase.id + "_A_integrated";
        }
        if (ContainsAny(joinedFlags, "forced_disclosure", "child_made_decider", "authority_push", "guilt_flood", "premature_interpretation", "hero_burden", "placating_deepened"))
        {
            return currentCase.id + "_C_key_risk_unrepaired";
        }
        return currentCase.id + "_B_partial";
    }

    private static int GetRouteQuality(SessionSelection selection)
    {
        if (selection == null) return 0;
        return selection.routeQuality > 0 ? selection.routeQuality : selection.quality;
    }

    private static string ResolveCaseSpecificEndingId(string caseId, string joinedFlags, int high, int low, bool finalHigh)
    {
        switch (caseId)
        {
            case "FT-002":
                return ResolveEndingByRouteTokens(caseId, joinedFlags, high, low, finalHigh,
                    new[] { "i_position", "feedback_task" },
                    new[] { "ip_fixing", "premature_correction", "control_escalation", "diagnostic_closure", "premature_depth", "compliance_promise", "shift_burden", "premature_contract", "academic_homework", "symptom_check_closure" },
                    new[] { "joining", "exception", "loss_reflection", "circular_mapping", "i_position", "feedback_task" });
            case "FT-003":
                return ResolveEndingByRouteTokens(caseId, joinedFlags, high, low, finalHigh,
                    new[] { "professional_used_as_resource", "home_practice_parent_team" },
                    new[] { "child_made_decider", "professional_authority_outsourced", "professional_takeover", "mother_overfunctioning", "schedule_as_command", "father_excluded", "father_blamed", "child_decision_hidden", "outsourced_closure" },
                    new[] { "parental_alignment", "implementation_burden_seen", "rest_standard_defined", "professional_used_as_resource", "home_practice_parent_team" });
            case "FT-004":
                return ResolveEndingByRouteTokens(caseId, joinedFlags, high, low, finalHigh,
                    new[] { "voice_practiced_with_support", "congruent_help_plan" },
                    new[] { "placating_accepted", "caregiver_alone_responsible", "emotion_bypassed", "placating_deepened", "compliance_trap", "blame_reversed", "institution_final_fix", "institution_as_only_solution", "voice_removed", "family_bypass" },
                    new[] { "iceberg_named", "checklist_connected_to_help", "congruent_pair_statement", "voice_practiced_with_support", "congruent_help_plan" });
            case "FT-005":
                return ResolveEndingByRouteTokens(caseId, joinedFlags, high, low, finalHigh,
                    new[] { "staged_contact", "direct_30sec_contact", "home_practice_boundary" },
                    new[] { "respect_rule_imposed", "respect_rule_final", "stepfather_excluded", "authority_push", "mother_bridge_reinforced", "premature_parent_claim", "mediation_burden", "triangulation_reinforced", "avoidant_closure" },
                    new[] { "staged_contact", "authority_speed_acknowledged", "loyalty_named", "direct_30sec_contact", "home_practice_boundary" });
            case "FT-006":
                return ResolveEndingByRouteTokens(caseId, joinedFlags, high, low, finalHigh,
                    new[] { "ten_minute_ritual", "home_ritual" },
                    new[] { "guilt_flood", "illness_totalizing", "sibling_centered_only", "outsourced_emotion", "parent_guilt_centered", "delayed_sibling_need", "sibling_responsible_for_signal", "apology_without_structure", "support_without_parent_connection" },
                    new[] { "two_feelings_named", "family_sculpture", "emotion_reflection_balanced", "ten_minute_ritual", "home_ritual" });
            case "FT-007":
                return ResolveEndingByRouteTokens(caseId, joinedFlags, high, low, finalHigh,
                    new[] { "respect_contract", "money_contract_written" },
                    new[] { "premature_interpretation", "interpretation_attack", "father_control_shift", "mother_triangle_locked", "child_must_prove", "cutoff_contract", "performance_report", "triangulated_closure", "contract_before_shame" },
                    new[] { "shame_named", "triangle_named", "defense_sequence_seen", "respect_contract", "money_contract_written" });
            case "FT-008":
                return ResolveEndingByRouteTokens(caseId, joinedFlags, high, low, finalHigh,
                    new[] { "outsider_witness_consent", "alternative_story_task" },
                    new[] { "forced_disclosure", "disclosure_homework", "endurance_story", "procedure_closure", "disclosure_pressure", "parent_action_over_teen_voice", "school_takes_story", "silence_kept", "decision_before_story", "procedure_totalized", "teen_as_case_file", "school_voice_over_takes" },
                    new[] { "safety_check_before_story", "silence_externalized", "problem_name_teen_authored", "unique_outcome_thickened", "outsider_witness_consent", "alternative_story_task" });
            case "FT-009":
                return ResolveEndingByRouteTokens(caseId, joinedFlags, high, low, finalHigh,
                    new[] { "first_cry_contract", "support_network_contacted", "safety_plan_written" },
                    new[] { "empathy_before_safety", "chore_plan_without_risk", "mindreading_contract", "permission_loop", "support_replaces_spouse", "spouse_overpromise", "chores_only_closure", "mother_must_initiate_crisis", "warmth_without_plan", "safety_deferred", "self_harm_signal_minimized", "baby_risk_signal_minimized", "reassurance_only", "thought_homework_without_sleep" },
                    new[] { "safety_screen_started", "crisis_contact_named", "baby_safety_check", "automatic_thought_linked", "first_cry_contract", "support_network_contacted", "safety_plan_written" });
            case "FT-010":
                return ResolveEndingByRouteTokens(caseId, joinedFlags, high, low, finalHigh,
                    new[] { "shared_micro_tasks", "one_point_relief_plan" },
                    new[] { "hero_burden", "parentification_organized", "guilt_centered", "resource_takeover", "teen_optimizes_rest", "apology_only", "external_support_only", "teen_as_manager", "teen_must_signal", "resource_takeover_closure" },
                    new[] { "scale_one_point", "exception_scaled", "strength_reduces_burden", "shared_micro_tasks", "one_point_relief_plan" });
            default:
                return null;
        }
    }

    private static bool IsSafetyCriticalCase(string caseId)
    {
        return caseId == "FT-008" || caseId == "FT-009";
    }

    private static bool HasRequiredSafetyTokens(string caseId, string[] selectedTokens)
    {
        if (selectedTokens == null) return false;
        switch (caseId)
        {
            case "FT-008":
                return ContainsAll(selectedTokens, "safety_check_before_story");
            case "FT-009":
                return ContainsAll(selectedTokens, "safety_screen_started", "crisis_contact_named", "baby_safety_check", "first_cry_contract", "support_network_contacted", "safety_plan_written");
            default:
                return true;
        }
    }

    private static bool ContainsAll(IEnumerable<string> sourceTokens, params string[] requiredTokens)
    {
        if (sourceTokens == null || requiredTokens == null || requiredTokens.Length == 0) return true;
        var source = new HashSet<string>(sourceTokens.Where(t => !string.IsNullOrWhiteSpace(t)), StringComparer.OrdinalIgnoreCase);
        return requiredTokens.All(token => source.Contains(token));
    }

    private static string ResolveEndingByRouteTokens(string caseId, string joinedFlags, int high, int low, bool finalHigh, string[] integrationTokens, string[] riskTokens, string[] repairTokens)
    {
        string[] selectedTokens = SplitRouteTokens(joinedFlags).ToArray();
        bool hasIntegration = ContainsAny(selectedTokens, integrationTokens);
        int riskCount = CountMatchingTokens(selectedTokens, riskTokens);
        bool hasRepair = ContainsAny(selectedTokens, repairTokens);

        if (low >= 2)
        {
            return caseId + "_D_closed_or_harmful";
        }
        if (riskCount >= 2)
        {
            return caseId + "_C_key_risk_unrepaired";
        }
        if (riskCount == 1 && hasRepair && finalHigh && low <= 1)
        {
            return caseId + "_B_repaired";
        }
        if (riskCount == 1)
        {
            return caseId + "_C_key_risk_unrepaired";
        }
        if (low == 1 && high >= 3 && finalHigh)
        {
            return caseId + "_B_repaired";
        }
        if (hasIntegration && high >= 4 && finalHigh && low == 0)
        {
            return caseId + "_A_integrated";
        }
        if (hasIntegration || high >= 2)
        {
            return caseId + "_B_partial";
        }
        return caseId + "_C_key_risk_unrepaired";
    }

    private static bool ContainsAny(string source, params string[] tokens)
    {
        if (string.IsNullOrEmpty(source)) return false;
        return ContainsAny(SplitRouteTokens(source).ToArray(), tokens);
    }

    private static bool ContainsAny(IEnumerable<string> sourceTokens, params string[] tokens)
    {
        if (sourceTokens == null || tokens == null) return false;
        var set = new HashSet<string>(sourceTokens.Where(token => !string.IsNullOrEmpty(token)), StringComparer.OrdinalIgnoreCase);
        return tokens.Any(token => !string.IsNullOrEmpty(token) && set.Contains(token));
    }

    private static int CountMatchingTokens(IEnumerable<string> sourceTokens, IEnumerable<string> tokens)
    {
        if (sourceTokens == null || tokens == null) return 0;
        var source = new HashSet<string>(sourceTokens.Where(token => !string.IsNullOrEmpty(token)), StringComparer.OrdinalIgnoreCase);
        return tokens.Where(token => !string.IsNullOrEmpty(token)).Distinct(StringComparer.OrdinalIgnoreCase).Count(source.Contains);
    }

    private SupervisorProfile GetSupervisorForTheory(string theoryId)
    {
        return supervisors.FirstOrDefault(s => s.theoryId == theoryId) ?? supervisors.First();
    }

    private static bool HasEndingScenePresenter(string caseId)
    {
        switch (caseId)
        {
            case "FT-002":
            case "FT-003":
            case "FT-004":
            case "FT-005":
            case "FT-006":
            case "FT-007":
            case "FT-008":
            case "FT-009":
            case "FT-010":
                return true;
            default:
                return false;
        }
    }

    private void ShowVnEnding()
    {
        PlayerChoiceLog last = logs.LastOrDefault();
        if (last == null)
        {
            ShowSupervision();
            return;
        }

        VnEndingPresentation ending = BuildEndingPresentation(last);
        var root = CreateVnRoot("vn-ending", GetVnEndingBackground(last.caseId, last.endingId));
        CreateAbsolutePanel(root.transform, "Ending Shade", new Color32(4, 8, 12, 138), Vector2.zero, Vector2.one, 0, 0, 0, 0);
        CreateVnHud(root.transform, last.caseId, "회기 엔딩", ending.gradeLabel + " · " + ending.routeLabel);

        var panel = CreateAbsoluteSkinnedPanel(root.transform, "Ending Panel", "VN/UI/session_result_sheet", new Color32(238, 232, 218, 246), new Vector2(0.085f, 0.105f), new Vector2(0.675f, 0.825f), 0, 0, 0, 0);
        var layout = panel.AddComponent<VerticalLayoutGroup>();
        layout.padding = new RectOffset(28, 28, 24, 24);
        layout.spacing = 10;
        layout.childControlWidth = true;
        layout.childControlHeight = true;
        layout.childForceExpandWidth = true;
        layout.childForceExpandHeight = false;

        var eyebrow = CreateText(panel.transform, last.caseId + " · " + last.familyType, 16, FontStyle.Bold, MutedInk);
        SetLayout(eyebrow.gameObject, 1, 0, -1, 28);
        var title = CreateText(panel.transform, ending.title, 30, FontStyle.Bold, Ink);
        title.resizeTextMinSize = 21;
        SetLayout(title.gameObject, 1, 0, -1, 54);
        var grade = CreateText(panel.transform, ending.gradeLabel + " · " + last.endingId, 17, FontStyle.Bold, ending.gradeColor);
        grade.resizeTextMinSize = 13;
        SetLayout(grade.gameObject, 1, 0, -1, 34);
        var body = CreateText(panel.transform, ending.body, 18, FontStyle.Normal, Ink);
        body.lineSpacing = 1.05f;
        body.resizeTextMinSize = 14;
        SetLayout(body.gameObject, 1, 0, -1, 132);

        var routeTitle = CreateText(panel.transform, "선택 경로", 18, FontStyle.Bold, Ink);
        SetLayout(routeTitle.gameObject, 1, 0, -1, 30);
        var routeFlags = CreateText(panel.transform, last.routeFlags, 13, FontStyle.Normal, MutedInk);
        routeFlags.resizeTextMinSize = 10;
        SetLayout(routeFlags.gameObject, 1, 0, -1, 58);

        var next = CreateText(panel.transform, ending.nextFocus, 17, FontStyle.Bold, ending.gradeColor);
        next.resizeTextMinSize = 13;
        SetLayout(next.gameObject, 1, 0, -1, 58);

        var supervisor = CreateAbsoluteSkinnedPanel(root.transform, "Ending Supervisor", "VN/UI/supervisor_note_panel", new Color32(18, 22, 28, 224), new Vector2(0.70f, 0.20f), new Vector2(0.93f, 0.52f), 0, 0, 0, 0);
        var supervisorLayout = supervisor.AddComponent<VerticalLayoutGroup>();
        supervisorLayout.padding = new RectOffset(18, 18, 16, 16);
        supervisorLayout.spacing = 8;
        supervisorLayout.childControlWidth = true;
        supervisorLayout.childControlHeight = true;
        supervisorLayout.childForceExpandWidth = true;
        supervisorLayout.childForceExpandHeight = false;
        var supervisorTitle = CreateText(supervisor.transform, "슈퍼바이저 메모", 18, FontStyle.Bold, new Color32(255, 241, 212, 255));
        SetLayout(supervisorTitle.gameObject, 1, 0, -1, 32);
        var supervisorText = CreateText(supervisor.transform, ending.supervisorNote, 15, FontStyle.Normal, new Color32(235, 238, 240, 255));
        supervisorText.lineSpacing = 1.02f;
        SetLayout(supervisorText.gameObject, 1, 0, -1, 126);
        CreateSkinnedButton(supervisor.transform, "슈퍼비전으로", "VN/UI/choice_card_intervention", Accent, ShowSupervision, 48f, 17);
    }

    private string GetVnEndingBackground(string caseId, string endingId)
    {
        if (caseId == "FT-001")
        {
            string ft001Ending = GetFt001EndingBackground(endingId);
            if (LoadVnTexture(ft001Ending) != null) return ft001Ending;
        }

        string compactId = string.IsNullOrEmpty(caseId) ? "ft000" : caseId.Replace("-", "").ToLowerInvariant();
        string endingKey = CompactEndingKey(endingId);
        string[] candidates =
        {
            BuildConventionCgPath(caseId, "ending_" + endingKey),
            "VN/EventCG/Endings/" + compactId + "_" + endingKey,
            "VN/EventCG/" + compactId.ToUpperInvariant() + "/ending_" + endingKey,
            "VN/EventCG/" + compactId + "_ending_" + endingKey
        };
        foreach (string candidate in candidates)
        {
            if (LoadVnTexture(candidate) != null) return candidate;
        }
        return "VN/Backgrounds/counseling_room_day";
    }

    private static string GetFt001EndingBackground(string endingId)
    {
        string id = endingId ?? "";
        if (id.IndexOf("_A_", StringComparison.OrdinalIgnoreCase) >= 0) return Ft001CgPath("t05_reaction_a_mother_softened");
        if (id.IndexOf("_B_repaired", StringComparison.OrdinalIgnoreCase) >= 0) return Ft001CgPath("t05_l03_child_relieved");
        if (id.IndexOf("_B_partial", StringComparison.OrdinalIgnoreCase) >= 0) return Ft001CgPath("t05_l02_mother_softened");
        if (id.IndexOf("_C_", StringComparison.OrdinalIgnoreCase) >= 0) return Ft001CgPath("t05_reaction_b_child_scared");
        if (id.IndexOf("_D_", StringComparison.OrdinalIgnoreCase) >= 0) return Ft001CgPath("t05_reaction_c_teacher_procedural");
        return Ft001CgPath("t05_choice_idle");
    }

    private static string CompactEndingKey(string endingId)
    {
        if (string.IsNullOrEmpty(endingId)) return "unknown";
        string[] parts = endingId.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length <= 1) return endingId.ToLowerInvariant();
        return string.Join("_", parts.Skip(1).ToArray()).ToLowerInvariant();
    }

    private VnEndingPresentation BuildEndingPresentation(PlayerChoiceLog log)
    {
        string endingId = log.endingId ?? "";
        string grade = endingId.Contains("_A_") ? "A" : endingId.Contains("_B_") ? "B" : endingId.Contains("_C_") ? "C" : endingId.Contains("_D_") ? "D" : "?";
        bool repaired = endingId.IndexOf("repaired", StringComparison.OrdinalIgnoreCase) >= 0;
        bool integrated = grade == "A";
        bool harmful = grade == "D";
        bool risk = grade == "C";

        string routeLabel = integrated ? "통합 경로" : repaired ? "회복 경로" : harmful ? "닫힌 경로" : risk ? "핵심 위험 남음" : "부분 진전";
        Color gradeColor = integrated ? Good : repaired ? Accent : harmful ? Bad : risk ? Warm : MutedInk;

        return new VnEndingPresentation
        {
            gradeLabel = grade + " 엔딩",
            routeLabel = routeLabel,
            gradeColor = gradeColor,
            title = BuildEndingTitle(log.caseId, grade, repaired),
            body = BuildEndingBody(log.caseId, grade, repaired, log),
            supervisorNote = BuildEndingSupervisorNote(log.caseId, grade, repaired),
            nextFocus = BuildEndingNextFocus(log.caseId, grade, repaired)
        };
    }

    private static string BuildEndingTitle(string caseId, string grade, bool repaired)
    {
        string state = grade == "A" ? "가족이 새 경로를 잡았다" : repaired ? "흔들렸지만 회복했다" : grade == "D" ? "회기가 닫혔다" : grade == "C" ? "핵심 위험이 남았다" : "부분적인 변화가 남았다";
        switch (caseId)
        {
            case "FT-002": return "걱정과 통제가 분리되는 밤 · " + state;
            case "FT-003": return "파란 치료가방을 부모가 다시 든다 · " + state;
            case "FT-004": return "괜찮다는 웃음 아래 목소리가 생긴다 · " + state;
            case "FT-005": return "빈 식탁 의자에 작은 접촉이 생긴다 · " + state;
            case "FT-006": return "괜찮은 둘째에게도 자리가 생긴다 · " + state;
            case "FT-007": return "비난과 철수 사이에 계약이 놓인다 · " + state;
            case "FT-008": return "문제의 이름을 다시 쓰기 시작한다 · " + state;
            case "FT-009": return "오늘 밤 안전계획이 먼저 선다 · " + state;
            case "FT-010": return "8점의 부담을 7점으로 낮춘다 · " + state;
            default: return "회기 엔딩 · " + state;
        }
    }

    private static string BuildEndingBody(string caseId, string grade, bool repaired, PlayerChoiceLog log)
    {
        string outcome = grade == "A"
            ? "선택들은 가족이 증상을 한 사람의 문제로 닫지 않고, 관계 안의 반복과 다음 행동을 함께 보도록 이끌었습니다."
            : repaired
                ? "중간에 위험한 선택이 있었지만, 이후 개입이 가족의 방어를 낮추고 다시 작업 가능한 장면을 만들었습니다."
                : grade == "D"
                    ? "위험하거나 닫힌 선택이 누적되어 가족이 상담실 안에서 더 작아졌습니다. 다음 회기 전 안전과 합류부터 다시 확인해야 합니다."
                    : grade == "C"
                        ? "핵심 위험 신호가 충분히 수리되지 않았습니다. 가족은 답을 들은 듯하지만 실제 상호작용은 아직 같은 고리 안에 남아 있습니다."
                        : "일부 단서는 잡았지만, 다음 주 행동으로 이어질 만큼 충분히 구조화되지는 않았습니다.";

        string caseLine;
        switch (caseId)
        {
            case "FT-002": caseLine = "Bowen 관점에서는 통제를 멈추라고 지시하기보다, 상실 불안과 자기 입장을 분리해 말하게 하는지가 핵심입니다."; break;
            case "FT-003": caseLine = "구조적 관점에서는 아이를 치료 참석의 결정자 자리에 세우지 않고, 부모가 함께 기준을 잡도록 도와야 합니다."; break;
            case "FT-004": caseLine = "Satir 관점에서는 회유와 비난 아래의 수치심, 두려움, 도움 요청을 일치적으로 말하게 해야 합니다."; break;
            case "FT-005": caseLine = "구조적 재혼가족 사례에서는 새아버지의 권한보다 작은 관계 행동과 어머니의 통역자 자리 이동이 먼저입니다."; break;
            case "FT-006": caseLine = "Satir 질병 가족 사례에서는 부모 죄책감이 둘째를 다시 위로자 자리에 올리지 않게 조절해야 합니다."; break;
            case "FT-007": caseLine = "정신역동 관점에서는 해석을 공격처럼 던지지 않고, 수치심과 방어 순서를 현실 계약으로 연결해야 합니다."; break;
            case "FT-008": caseLine = "이야기치료에서는 폭로가 아니라 청소년이 문제의 이름과 증인 범위를 정하도록 돕는 것이 핵심입니다."; break;
            case "FT-009": caseLine = "CBFT 산후 사례에서는 따뜻한 말보다 오늘 밤 안전, 교대, 지원망, 실패 시 연락 규칙이 먼저입니다."; break;
            case "FT-010": caseLine = "해결중심 부모화 사례에서는 강점을 더 많은 돌봄으로 칭찬하지 않고 부담을 1점 낮추는 데 써야 합니다."; break;
            default: caseLine = "다음 회기에서는 선택 경로와 가족 반응을 함께 복기해야 합니다."; break;
        }
        return outcome + "\n\n" + caseLine;
    }

    private static string BuildEndingSupervisorNote(string caseId, string grade, bool repaired)
    {
        if (grade == "A") return "좋은 엔딩은 높은 점수보다 가족이 다음 주에 실제로 다르게 해볼 장면을 얻었는지로 판단합니다.";
        if (repaired) return "회복 경로에서는 실수 자체보다 그 뒤에 상담자가 무엇을 회복했는지를 보세요. 회복 개입을 명시하면 로그 분석에 강해집니다.";
        if (grade == "D") return "닫힌 엔딩에서는 개입을 더 밀지 말고 안전, 합류, 관계 손상을 먼저 복구해야 합니다.";
        return "핵심 위험이 남은 엔딩입니다. 다음 설계에서는 위험 선택을 중간에 발견했을 때 복구 선택지를 더 빨리 열어야 합니다.";
    }

    private static string BuildEndingNextFocus(string caseId, string grade, bool repaired)
    {
        if (grade == "A") return "다음 초점: 이 가족이 정한 한 가지 실험이 실제 생활에서 유지되는지 관찰합니다.";
        if (repaired) return "다음 초점: 회기 중 복구된 장면을 가족이 기억할 수 있는 문장으로 고정합니다.";
        if (grade == "D") return "다음 초점: 진단, 절차, 통제보다 먼저 다시 합류하고 안전을 확인합니다.";
        return "다음 초점: 아직 다루지 못한 위험 단서를 한 장면에서 다시 확인합니다.";
    }

    private static string FormatEndingIdForDisplay(string endingId)
    {
        if (string.IsNullOrWhiteSpace(endingId)) return "아직 엔딩 없음";
        if (endingId.IndexOf("_A_", StringComparison.OrdinalIgnoreCase) >= 0) return "A 엔딩 · 통합 경로";
        if (endingId.IndexOf("_B_repaired", StringComparison.OrdinalIgnoreCase) >= 0) return "B 엔딩 · 회복 경로";
        if (endingId.IndexOf("_B_partial", StringComparison.OrdinalIgnoreCase) >= 0) return "B 엔딩 · 부분 진전";
        if (endingId.IndexOf("_C_", StringComparison.OrdinalIgnoreCase) >= 0) return "C 엔딩 · 핵심 위험 남음";
        if (endingId.IndexOf("_D_safety", StringComparison.OrdinalIgnoreCase) >= 0) return "D 엔딩 · 안전 미해결";
        if (endingId.IndexOf("_D_", StringComparison.OrdinalIgnoreCase) >= 0) return "D 엔딩 · 닫힌 경로";
        return endingId.Replace("_", " ");
    }

    private static string FormatRouteFlagsForDisplay(string routeFlags)
    {
        if (string.IsNullOrWhiteSpace(routeFlags) || routeFlags == "none") return "아직 선택 기록 없음";
        return routeFlags
            .Replace("Route flags:", "")
            .Replace("_", " ")
            .Replace("|", "→")
            .Trim();
    }

    private void ShowSupervision()
    {
        var last = logs.Last();
        TherapyTheory recommended = theories.First(t => t.id == currentCase.recommendedTheoryId);
        SupervisorProfile supervisor = GetSupervisorForTheory(recommended.id);
        ClearCanvas();
        var root = CreateVnRoot("supervision", "VN/Backgrounds/counseling_room_day");
        CreateAbsolutePanel(root.transform, "Supervision Shade", new Color32(8, 11, 15, 132), Vector2.zero, Vector2.one, 0, 0, 0, 0);
        if (currentCase == null || currentCase.id != "FT-001")
        {
            CreateAbsoluteVnPortrait(root.transform, "supervisor_" + recommended.id, last.score >= 80 ? "approving" : "reflective", new Vector2(0.62f, 0.15f), new Vector2(0.90f, 0.82f), 225);
        }
        CreateVnHud(root.transform, currentCase.id, "슈퍼비전 회고", "회기 종료");

        var report = CreateAbsoluteSkinnedPanel(root.transform, "Training Report", "VN/UI/session_result_sheet", new Color32(238, 232, 218, 246), new Vector2(0.055f, 0.055f), new Vector2(0.66f, 0.88f), 0, 0, 0, 0);
        var reportLayout = report.AddComponent<VerticalLayoutGroup>();
        reportLayout.padding = new RectOffset(28, 28, 22, 22);
        reportLayout.spacing = 8;
        reportLayout.childControlWidth = true;
        reportLayout.childControlHeight = true;
        reportLayout.childForceExpandWidth = true;
        reportLayout.childForceExpandHeight = false;
        var title = CreateText(report.transform, "슈퍼비전 회고", 24, FontStyle.Bold, Ink);
        SetLayout(title.gameObject, 1, 0, -1, 42);
        var summary = CreateText(report.transform, currentCase.id + " · " + currentCase.familyType + " · 가족 반응 복기", 16, FontStyle.Bold, MutedInk);
        SetLayout(summary.gameObject, 1, 0, -1, 30);
        var scoreLine = CreateText(report.transform, "보조 지표: 점수 " + last.score + "/100 · 신뢰 " + trustScore + " · 안전 " + safetyScore + " · 통찰 " + insightScore, 15, FontStyle.Bold, last.score >= 80 ? Good : last.score >= 60 ? Warm : Bad);
        SetLayout(scoreLine.gameObject, 1, 0, -1, 30);
        var routeLine = CreateText(report.transform, "엔딩: " + FormatEndingIdForDisplay(last.endingId) + " · 선택 흐름: " + FormatRouteFlagsForDisplay(last.routeFlags), 13, FontStyle.Bold, MutedInk);
        routeLine.resizeTextMinSize = 11;
        SetLayout(routeLine.gameObject, 1, 0, -1, 42);
        var supervisorLine = CreateText(report.transform, supervisor.name + ": 이번 회기는 점수가 아니라 가족 반응을 먼저 복기하세요.", 17, FontStyle.Bold, Ink);
        SetLayout(supervisorLine.gameObject, 1, 0, -1, 34);
        foreach (var selection in currentSelections.OrderByDescending(s => s.quality).Take(3))
        {
            CreateSupervisionReflectionItem(report.transform, selection);
        }
        CreateSpacer(report.transform, 4);
        var questionTitle = CreateText(report.transform, "다음 회기 질문", 19, FontStyle.Bold, Ink);
        SetLayout(questionTitle.gameObject, 1, 0, -1, 34);
        var question = CreateText(report.transform, currentCase.reflectionQuestion, 16, FontStyle.Normal, MutedInk);
        SetLayout(question.gameObject, 1, 0, -1, 44);
        var missed = CreateText(report.transform, "놓친 개념: " + last.missedConcepts, 16, FontStyle.Bold, Warm);
        SetLayout(missed.gameObject, 1, 0, -1, 30);
        if (aiSupervisorEnabled)
        {
            CreateText(report.transform, "AI 슈퍼바이저 참고: 현재 빌드는 기본 슈퍼비전 코멘트를 표시합니다.", 16, FontStyle.Normal, Bad);
        }

        var actions = CreateAbsoluteSkinnedPanel(root.transform, "Supervision Actions", "VN/UI/supervisor_note_panel", new Color32(18, 22, 28, 225), new Vector2(0.67f, 0.16f), new Vector2(0.93f, 0.48f), 0, 0, 0, 0);
        var actionLayout = actions.AddComponent<VerticalLayoutGroup>();
        actionLayout.padding = new RectOffset(18, 18, 16, 16);
        actionLayout.spacing = 8;
        actionLayout.childControlWidth = true;
        actionLayout.childControlHeight = true;
        actionLayout.childForceExpandWidth = true;
        actionLayout.childForceExpandHeight = false;
        var actionTitle = CreateText(actions.transform, "다음 행동", 18, FontStyle.Bold, new Color32(255, 241, 212, 255));
        SetLayout(actionTitle.gameObject, 1, 0, -1, 38);
        CreateSkinnedButton(actions.transform, "다음 사례 접수", "VN/UI/choice_card_question", Accent, ShowCaseIntake, 50f, 18);
        CreateSkinnedButton(actions.transform, "기록실 보기", "VN/UI/choice_card_intervention", Warm, ShowDashboard, 50f, 18);
        CreateSkinnedButton(actions.transform, "로그 Export", "VN/UI/choice_card_intervention", Good, () =>
        {
            ExportAll();
            ShowDashboard();
        }, 50f, 18);
        CreateSkinnedButton(actions.transform, "메인 메뉴", "VN/UI/choice_card_question", MutedInk, ShowMainMenu, 48f, 17);
    }

    private void CreateSupervisionReflectionItem(Transform parent, SessionSelection selection)
    {
        var item = CreateSkinnedInlinePanel(parent, "Reflection Item", "VN/UI/supervisor_note_panel", new Color32(255, 252, 243, 235), 1, 0, -1, 86);
        var layout = item.AddComponent<VerticalLayoutGroup>();
        layout.padding = new RectOffset(14, 14, 8, 8);
        layout.spacing = 3;
        layout.childControlWidth = true;
        layout.childControlHeight = true;
        layout.childForceExpandWidth = true;
        layout.childForceExpandHeight = false;

        string deltas = "신뢰 " + FormatDelta(selection.trustDelta) + " · 안전 " + FormatDelta(selection.safetyDelta) + " · 통찰 " + FormatDelta(selection.insightDelta);
        var line = CreateText(item.transform, "T" + selection.turn + "  " + selection.choice, 15, FontStyle.Bold, Ink);
        SetLayout(line.gameObject, 1, 0, -1, 34);
        var reaction = CreateText(item.transform, CompactForAudit(selection.familyReaction) + "  /  " + deltas, 13, FontStyle.Normal, MutedInk);
        SetLayout(reaction.gameObject, 1, 0, -1, 28);
    }

    private static string FormatDelta(int value)
    {
        return (value > 0 ? "+" : "") + value;
    }

    private void ShowDashboard()
    {
        ClearCanvas();
        var root = CreateVnRoot("dashboard", "VN/Backgrounds/counseling_room_day");
        CreateAbsolutePanel(root.transform, "Records Shade", new Color32(8, 11, 15, 140), Vector2.zero, Vector2.one, 0, 0, 0, 0);
        CreateVnHud(root.transform, "기록실", "수련 로그와 데이터 분석", "Export");

        var left = CreateAbsoluteSkinnedPanel(root.transform, "Training Archive", "VN/UI/session_result_sheet", new Color32(238, 232, 218, 246), new Vector2(0.055f, 0.11f), new Vector2(0.59f, 0.80f), 0, 0, 0, 0);
        var leftLayout = left.AddComponent<VerticalLayoutGroup>();
        leftLayout.padding = new RectOffset(22, 22, 18, 18);
        leftLayout.spacing = 5;
        leftLayout.childControlWidth = true;
        leftLayout.childControlHeight = true;
        leftLayout.childForceExpandWidth = true;
        leftLayout.childForceExpandHeight = false;
        CreateText(left.transform, "수련 기록실", 25, FontStyle.Bold, Ink);
        if (logs.Count == 0)
        {
            CreateText(left.transform, "아직 플레이 로그가 없습니다. 사례를 1건 이상 진행하면 선택 패턴과 이론 적용 결과가 여기에 쌓입니다.", 17, FontStyle.Normal, Ink);
        }
        else
        {
            float avg = (float)logs.Average(l => l.score);
            float match = logs.Count(l => l.matchedRecommendedTheory) * 100f / logs.Count;
            CreateText(left.transform, "세션 " + logs.Count + "건 · 평균 " + avg.ToString("0.0", CultureInfo.InvariantCulture) + "점 · 추천 렌즈 일치 " + match.ToString("0.0", CultureInfo.InvariantCulture) + "%", 17, FontStyle.Bold, Warm);
            foreach (var group in logs.GroupBy(l => l.recommendedTheory).OrderByDescending(g => g.Count()).Take(4))
            {
                CreateBar(left.transform, group.Key, group.Count(), logs.Count, Good);
            }
            CreateText(left.transform, "최근 세션", 18, FontStyle.Bold, Ink);
            foreach (var log in logs.Skip(Math.Max(0, logs.Count - 3)).Reverse())
            {
                CreateText(left.transform, log.caseId + " · " + log.score + "점 · " + log.selectedTheory, 15, FontStyle.Normal, MutedInk);
            }
        }

        var right = CreateAbsoluteSkinnedPanel(root.transform, "Data Archive", "VN/UI/supervisor_note_panel", new Color32(18, 22, 28, 226), new Vector2(0.62f, 0.14f), new Vector2(0.93f, 0.80f), 0, 0, 0, 0);
        var rightLayout = right.AddComponent<VerticalLayoutGroup>();
        rightLayout.padding = new RectOffset(16, 16, 14, 14);
        rightLayout.spacing = 4;
        rightLayout.childControlWidth = true;
        rightLayout.childControlHeight = true;
        rightLayout.childForceExpandWidth = true;
        rightLayout.childForceExpandHeight = false;
        var dataTitle = CreateText(right.transform, "데이터 분석실", 21, FontStyle.Bold, new Color32(255, 241, 212, 255));
        SetLayout(dataTitle.gameObject, 1, 0, -1, 28);
        CreateText(right.transform, "총 사례 " + cases.Count + "개 · 캠페인 6장 · 이론 " + theories.Count + "개", 14, FontStyle.Bold, Color.white);
        foreach (var group in cases.GroupBy(c => theories.First(t => t.id == c.recommendedTheoryId).name).OrderBy(g => g.Key).Take(6))
        {
            CreateBar(right.transform, group.Key, group.Count(), cases.Count, Accent);
        }
        CreateSkinnedButton(right.transform, "CSV / JSON / HTML Export", "VN/UI/choice_card_intervention", Good, () =>
        {
            ExportAll();
            ShowDashboard();
        }, 42f, 14);
        var exportText = CreateText(right.transform, "Export folder: " + Path.GetFileName(exportFolder), 14, FontStyle.Normal, new Color32(228, 230, 234, 255));
        SetLayout(exportText.gameObject, 1, 0, -1, 40);
        CreateSkinnedButton(right.transform, "메인 메뉴", "VN/UI/choice_card_question", MutedInk, ShowMainMenu, 40f, 14);
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
        b.AppendLine("session_id,case_id,chapter,family_type,selected_theory,recommended_theory,matched,score,trust,safety,insight,risk_level,missed_concepts,selected_interventions,vn_mode,vn_choice_path,route_flags,ending_id,vn_reaction_summary,turn_metric_deltas,created_at");
        foreach (var log in logs)
        {
            b.AppendLine(string.Join(",", new[]
            {
                Csv(log.sessionId), Csv(log.caseId), log.chapter.ToString(CultureInfo.InvariantCulture), Csv(log.familyType), Csv(log.selectedTheory), Csv(log.recommendedTheory),
                log.matchedRecommendedTheory ? "true" : "false", log.score.ToString(CultureInfo.InvariantCulture), log.trust.ToString(CultureInfo.InvariantCulture),
                log.safety.ToString(CultureInfo.InvariantCulture), log.insight.ToString(CultureInfo.InvariantCulture), log.riskLevel.ToString(CultureInfo.InvariantCulture),
                Csv(log.missedConcepts), Csv(log.selectedInterventions), log.vnMode ? "true" : "false", Csv(log.vnChoicePath), Csv(log.routeFlags), Csv(log.endingId), Csv(log.vnReactionSummary), Csv(log.turnMetricDeltas), Csv(log.createdAt)
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
        if (string.IsNullOrEmpty(backgroundPath))
        {
            backgroundPath = "VN/Backgrounds/counseling_room_day";
        }
        suppressVnCharacterSprites = backgroundPath.StartsWith("VN/EventCG/", StringComparison.OrdinalIgnoreCase);
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
            raw.uvRect = GetCoverUvRect(backgroundTexture.width / Mathf.Max(1f, backgroundTexture.height), 16f / 9f);
        }
        else
        {
            var fallback = CreateAbsolutePanel(root.transform, "Missing Background", new Color32(31, 37, 45, 255), Vector2.zero, Vector2.one, 0, 0, 0, 0);
            var label = CreateText(fallback.transform, "배경 이미지를 불러오지 못했습니다\n" + backgroundPath, 24, FontStyle.Bold, new Color32(214, 218, 224, 255));
            label.alignment = TextAnchor.MiddleCenter;
            Stretch(label.gameObject, 0, 0, 0, 0);
        }

        var shade = CreateAbsolutePanel(root.transform, "VN Shade", new Color32(0, 0, 0, 72), Vector2.zero, Vector2.one, 0, 0, 0, 0);
        shade.transform.SetAsLastSibling();
        return root;
    }

    private static Rect GetCoverUvRect(float textureAspect, float targetAspect)
    {
        if (textureAspect <= 0f || targetAspect <= 0f) return new Rect(0f, 0f, 1f, 1f);
        if (textureAspect > targetAspect)
        {
            float width = targetAspect / textureAspect;
            return new Rect((1f - width) * 0.5f, 0f, width, 1f);
        }

        float height = textureAspect / targetAspect;
        return new Rect(0f, (1f - height) * 0.5f, 1f, height);
    }

    private void CreateVnHud(Transform parent, string caseId, string title, string progress)
    {
        var hud = CreateAbsoluteSkinnedPanel(parent, "VN HUD", "VN/UI/metrics_hud", new Color32(11, 14, 18, 190), new Vector2(0.045f, 0.922f), new Vector2(0.955f, 0.972f), 0, 0, 0, 0);
        var layout = hud.AddComponent<HorizontalLayoutGroup>();
        layout.padding = new RectOffset(18, 18, 5, 5);
        layout.spacing = 12;
        layout.childControlWidth = true;
        layout.childControlHeight = true;
        layout.childForceExpandWidth = false;
        layout.childForceExpandHeight = true;
        var left = CreateText(hud.transform, caseId + " · " + title, 17, FontStyle.Bold, Color.white);
        left.alignment = TextAnchor.MiddleLeft;
        SetLayout(left.gameObject, 1, 1, -1, -1);
        var right = CreateText(hud.transform, progress, 14, FontStyle.Bold, new Color32(238, 232, 218, 235));
        right.alignment = TextAnchor.MiddleRight;
        SetLayout(right.gameObject, 0, 1, 260, -1);
    }

    private void CreateVnStage(Transform parent, string activeSpeakerId, string expressionId)
    {
        if (suppressVnCharacterSprites) return;

        var ids = GetVisibleVnCharacterIds(activeSpeakerId);
        float[] centers = ids.Count == 1 ? new[] { 0.5f } : ids.Count == 2 ? new[] { 0.34f, 0.66f } : new[] { 0.24f, 0.5f, 0.76f };
        for (int i = 0; i < ids.Count; i++)
        {
            string id = ids[i];
            VnCharacterProfile profile = GetVnCharacter(id);
            if (profile == null) continue;
            bool active = id == activeSpeakerId;
            string expression = active ? expressionId : profile.defaultExpression;
            float width = (active ? 0.255f : 0.225f) * GetCharacterPortraitWidthScale(id);
            float center = centers[Mathf.Min(i, centers.Length - 1)];
            var holder = CreateAbsolutePanel(parent, "Character " + id, new Color32(0, 0, 0, 0), new Vector2(center - width / 2f, 0.18f), new Vector2(center + width / 2f, 0.89f), 0, 0, 0, 0);
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
                raw.color = active ? Color.white : new Color32(218, 222, 226, 232);
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
                CreateCharacterIdentityBadge(holder.transform, profile, active);
            }
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

        var relation = CreateText(layoutRoot.transform, GetShortRelationLabel(profile), active ? 25 : 21, FontStyle.Bold, Color.white);
        relation.alignment = TextAnchor.MiddleCenter;
        SetLayout(relation.gameObject, 1, 0, -1, active ? 52 : 60);
        var name = CreateText(layoutRoot.transform, profile.displayName, active ? 31 : 26, FontStyle.Bold, Color.white);
        name.alignment = TextAnchor.MiddleCenter;
        SetLayout(name.gameObject, 1, 0, -1, active ? 60 : 46);
        var role = CreateText(layoutRoot.transform, CompactCharacterRole(profile), active ? 17 : 15, FontStyle.Normal, new Color32(226, 232, 236, 255));
        role.alignment = TextAnchor.MiddleCenter;
        SetLayout(role.gameObject, 1, 0, -1, active ? 44 : 48);
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
        if (!string.IsNullOrEmpty(activeSpeakerId) && IsSupervisorCharacter(activeSpeakerId))
        {
            if (currentVnScript == null || currentVnScript.characters == null || currentVnScript.characters.Contains(activeSpeakerId))
            {
                ids.Add(activeSpeakerId);
                return ids;
            }
        }
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
        if (!string.IsNullOrEmpty(activeSpeakerId) && !IsSupervisorCharacter(activeSpeakerId) && !ids.Contains(activeSpeakerId)) ids.Add(activeSpeakerId);
        if (ids.Count == 0 && currentVnScript != null && currentVnScript.characters != null) ids.AddRange(currentVnScript.characters.Take(3));
        return ids.Take(3).ToList();
    }

    private static bool IsSupervisorCharacter(string characterId)
    {
        return !string.IsNullOrEmpty(characterId) && characterId.StartsWith("supervisor_", StringComparison.OrdinalIgnoreCase);
    }

    private void CreateVnSupervisorNote(Transform parent, string note)
    {
        var panel = CreateAbsoluteSkinnedPanel(parent, "Supervisor Note", "VN/UI/supervisor_note_panel", new Color32(18, 22, 28, 190), new Vector2(0.642f, 0.265f), new Vector2(0.952f, 0.385f), 0, 0, 0, 0);
        var layout = panel.AddComponent<VerticalLayoutGroup>();
        layout.padding = new RectOffset(13, 13, 8, 8);
        layout.spacing = 2;
        var title = CreateText(panel.transform, "슈퍼바이저", 13, FontStyle.Bold, new Color32(255, 241, 212, 245));
        SetLayout(title.gameObject, 1, 0, -1, 20);
        var body = CreateText(panel.transform, note, 12, FontStyle.Normal, new Color32(230, 232, 236, 245));
        body.lineSpacing = 0.92f;
        SetLayout(body.gameObject, 1, 0, -1, 58);
    }

    private void CreateVnDialogueBox(Transform parent, string speakerId, string content, int index, int total, Action next, string continueLabel = null, string speakerNameOverride = null)
    {
        VnCharacterProfile speaker = GetVnCharacter(speakerId);
        string speakerName = !string.IsNullOrEmpty(speakerNameOverride) ? speakerNameOverride : speaker != null ? FormatCharacterNameWithRelation(speaker) : speakerId == "session_reaction" ? "회기 반응" : "상담실";
        var box = CreateAbsoluteSkinnedPanel(parent, "Dialogue Box", "VN/UI/dialogue_box", new Color32(238, 232, 218, 248), new Vector2(0.045f, 0.04f), new Vector2(0.955f, 0.245f), 0, 0, 0, 0);
        var layout = box.AddComponent<VerticalLayoutGroup>();
        layout.padding = new RectOffset(22, 22, 10, 10);
        layout.spacing = 4;
        layout.childControlWidth = true;
        layout.childControlHeight = true;
        layout.childForceExpandWidth = true;
        layout.childForceExpandHeight = false;
        var nameplate = CreateSkinnedInlinePanel(box.transform, "Speaker Nameplate", "VN/UI/speaker_nameplate", new Color32(218, 231, 231, 255), 1, 0, -1, 28);
        var nameText = CreateText(nameplate.transform, speakerName, 16, FontStyle.Bold, Accent);
        nameText.alignment = TextAnchor.MiddleLeft;
        Stretch(nameText.gameObject, 16, 0, 16, 0);
        var body = CreateText(box.transform, content, 21, FontStyle.Bold, Ink);
        body.lineSpacing = 0.95f;
        body.resizeTextMinSize = 15;
        SetLayout(body.gameObject, 1, 0, -1, 64);
        string label = continueLabel ?? (index >= total ? "개입 선택 / 계속" : "다음");
        var footer = new GameObject("Dialogue Footer");
        footer.transform.SetParent(box.transform, false);
        var footerLayout = footer.AddComponent<HorizontalLayoutGroup>();
        footerLayout.spacing = 8;
        footerLayout.childControlWidth = true;
        footerLayout.childControlHeight = true;
        footerLayout.childForceExpandWidth = false;
        footerLayout.childForceExpandHeight = true;
        SetLayout(footer, 1, 0, -1, 34);
        var spacer = new GameObject("Dialogue Button Spacer");
        spacer.transform.SetParent(footer.transform, false);
        SetLayout(spacer, 1, 1, -1, -1);
        var nextButton = CreateSkinnedButton(footer.transform, label, "VN/UI/choice_card_question", Accent, next, 34f, 16);
        SetLayout(nextButton, 0, 1, index >= total ? 190 : 120, -1);
    }

    private void CreateAbsoluteVnPortrait(Transform parent, string characterId, string expressionId, Vector2 anchorMin, Vector2 anchorMax, byte alpha)
    {
        VnCharacterProfile profile = GetVnCharacter(characterId);
        if (profile == null) return;
        Texture2D texture = LoadVnTexture(profile.baseAssetPath + "_" + expressionId) ?? LoadVnTexture(profile.baseAssetPath + "_" + profile.defaultExpression);
        if (texture == null) return;
        ApplyCharacterPortraitWidthScale(characterId, ref anchorMin, ref anchorMax);
        var holder = CreateAbsolutePanel(parent, "Briefing Portrait " + characterId, new Color32(0, 0, 0, 0), anchorMin, anchorMax, 0, 0, 0, 0);
        var image = holder.GetComponent<Image>();
        image.raycastTarget = false;
        var portrait = new GameObject("Character Image");
        portrait.transform.SetParent(holder.transform, false);
        var rect = portrait.AddComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.pivot = new Vector2(0.5f, 0f);
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
        rect.sizeDelta = Vector2.zero;
        var raw = portrait.AddComponent<RawImage>();
        raw.texture = texture;
        raw.color = new Color32(255, 255, 255, alpha);
        raw.raycastTarget = false;
        var fitter = portrait.AddComponent<AspectRatioFitter>();
        fitter.aspectMode = AspectRatioFitter.AspectMode.FitInParent;
        fitter.aspectRatio = texture.width / Mathf.Max(1f, texture.height);
    }

    private void CreateAbsoluteEventCg(Transform parent, string name, string resourcePath, Vector2 anchorMin, Vector2 anchorMax, byte alpha)
    {
        Texture2D texture = LoadVnTexture(resourcePath);
        if (texture == null) return;

        var holder = CreateAbsolutePanel(parent, name, new Color32(0, 0, 0, 0), anchorMin, anchorMax, 0, 0, 0, 0);
        holder.GetComponent<Image>().raycastTarget = false;

        var imageObject = new GameObject("Event CG Image");
        imageObject.transform.SetParent(holder.transform, false);
        var rect = imageObject.AddComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        var raw = imageObject.AddComponent<RawImage>();
        raw.texture = texture;
        raw.color = new Color32(255, 255, 255, alpha);
        raw.raycastTarget = false;
        float targetAspect = (anchorMax.x - anchorMin.x) * 16f / Mathf.Max(0.001f, (anchorMax.y - anchorMin.y) * 9f);
        raw.uvRect = GetCoverUvRect(texture.width / Mathf.Max(1f, texture.height), targetAspect);
    }

    private static float GetCharacterPortraitWidthScale(string characterId)
    {
        if (characterId == "ft001_mother") return 0.84f;
        if (characterId == "ft001_child") return 0.92f;
        return 1f;
    }

    private static void ApplyCharacterPortraitWidthScale(string characterId, ref Vector2 anchorMin, ref Vector2 anchorMax)
    {
        float scale = GetCharacterPortraitWidthScale(characterId);
        if (Mathf.Approximately(scale, 1f)) return;
        float centerX = (anchorMin.x + anchorMax.x) * 0.5f;
        float halfWidth = (anchorMax.x - anchorMin.x) * scale * 0.5f;
        anchorMin.x = centerX - halfWidth;
        anchorMax.x = centerX + halfWidth;
    }

    private static string FormatCharacterNameWithRelation(VnCharacterProfile profile)
    {
        if (profile == null) return "상담실";
        string relation = GetShortRelationLabel(profile);
        if (relation == profile.displayName) return profile.displayName;
        return string.IsNullOrEmpty(relation) ? profile.displayName : profile.displayName + " · " + relation;
    }

    private static string FormatCharacterNameForRoster(VnCharacterProfile profile)
    {
        if (profile == null) return "";
        string relation = GetShortRelationLabel(profile);
        if (relation == profile.displayName) return profile.displayName;
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
        if (id == "ft002_grandmother") return "친조모";
        if (id == "ft002_grandson") return "손자";
        if (id == "ft002_grandfather") return "친조부";
        if (id == "ft003_mother") return "어머니";
        if (id == "ft003_father") return "아버지";
        if (id == "ft003_child") return "자녀";
        if (id == "ft003_coordinator") return "기관 담당자";
        if (id == "ft004_caregiver") return "어머니";
        if (id == "ft004_spouse") return "아버지";
        if (id == "ft004_child") return "자녀";
        if (id == "ft004_institution" || id == "ft004_worker") return "보육기관 담당자";
        if (id == "ft005_mother") return "어머니";
        if (id == "ft005_stepfather") return "새아버지";
        if (id == "ft005_teen") return "청소년 자녀";
        if (id == "ft006_mother") return "어머니";
        if (id == "ft006_father") return "아버지";
        if (id == "ft006_sibling") return "둘째";
        if (id == "ft007_father") return "아버지";
        if (id == "ft007_adult_child") return "성인자녀";
        if (id == "ft007_mother") return "어머니";
        if (id == "ft008_teen") return "청소년";
        if (id == "ft008_mother") return "어머니";
        if (id == "ft008_father") return "아버지";
        if (id == "ft008_teacher") return "학교 담당자";
        if (id == "ft009_mother") return "산후 보호자";
        if (id == "ft009_spouse") return "배우자";
        if (id == "ft009_support") return "지원 인물";
        if (id == "ft010_teen") return "누나";
        if (id == "ft010_guardian") return "보호자";
        if (id == "ft010_sibling") return "동생";
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
            AddUiSkinImage(panel, texture, resourcePath);
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
            AddUiSkinImage(panel, texture, resourcePath);
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

        texture = Resources.Load<Texture2D>(resourcePath + "_candidate01");
        if (texture != null) return texture;

        texture = Resources.Load<Texture2D>(resourcePath + "_phase1_candidate01");
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
        if (resourcePath == "VN/Characters/FT001/ft001_child_neutral")
        {
            return Resources.Load<Texture2D>("VN/Characters/FT001/ft001_child_male_neutral_phase1");
        }
        if (resourcePath.StartsWith("VN/Characters/FT001/ft001_child_", StringComparison.Ordinal))
        {
            return Resources.Load<Texture2D>("VN/Characters/FT001/ft001_child_male_neutral_phase1");
        }
        if (resourcePath == "VN/Characters/FT001/ft001_teacher_neutral")
        {
            return Resources.Load<Texture2D>("VN/Characters/FT001/ft001_teacher_male_neutral_phase1");
        }
        if (resourcePath.StartsWith("VN/Characters/FT001/ft001_teacher_", StringComparison.Ordinal))
        {
            return Resources.Load<Texture2D>("VN/Characters/FT001/ft001_teacher_male_neutral_phase1");
        }
        if (resourcePath.StartsWith("VN/Characters/Supervisors/supervisor_narrative_", StringComparison.Ordinal))
        {
            string fallback = resourcePath.Replace("supervisor_narrative_", "supervisor_solution_");
            return Resources.Load<Texture2D>(fallback) ?? Resources.Load<Texture2D>(fallback + "_candidate01");
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
            AddUiSkinImage(card, texture, resourcePath);
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
        text.verticalOverflow = VerticalWrapMode.Truncate;
        text.resizeTextForBestFit = true;
        text.resizeTextMinSize = Mathf.Max(13, Mathf.RoundToInt(size * 0.78f));
        text.resizeTextMaxSize = size;
        text.lineSpacing = 1.0f;
        SetLayout(textObject, 1, 0, -1, -1);
        return text;
    }

    private void CreateButton(Transform parent, string label, Color color, Action action)
    {
        var buttonObject = new GameObject("Button");
        buttonObject.transform.SetParent(parent, false);
        var image = buttonObject.AddComponent<Image>();
        image.color = new Color32(22, 27, 34, 215);
        var outline = buttonObject.AddComponent<Outline>();
        outline.effectColor = new Color32(170, 181, 187, 82);
        outline.effectDistance = new Vector2(1, -1);
        var button = buttonObject.AddComponent<Button>();
        button.targetGraphic = image;
        button.onClick.AddListener(() => action());
        SetLayout(buttonObject, 1, 0, -1, 56);
        AddButtonAccentStrip(buttonObject.transform, color);
        var text = CreateText(buttonObject.transform, label, 19, FontStyle.Bold, Color.white);
        text.alignment = TextAnchor.MiddleCenter;
        var rect = text.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = new Vector2(12, 4);
        rect.offsetMax = new Vector2(-12, -4);
    }

    private GameObject CreateSkinnedButton(Transform parent, string label, string resourcePath, Color fallbackColor, Action action, float preferredHeight = 62f, int fontSize = 21)
    {
        var buttonObject = new GameObject("Button");
        buttonObject.transform.SetParent(parent, false);
        Graphic targetGraphic;
        Texture2D texture = LoadVnTexture(resourcePath);
        if (UseDecorativeUiSkins && texture != null)
        {
            targetGraphic = AddUiSkinImage(buttonObject, texture, resourcePath);
        }
        else
        {
            var image = buttonObject.AddComponent<Image>();
            image.color = new Color32(22, 27, 34, 218);
            targetGraphic = image;
            var outline = buttonObject.AddComponent<Outline>();
            outline.effectColor = new Color32(170, 181, 187, 72);
            outline.effectDistance = new Vector2(1, -1);
        }
        var button = buttonObject.AddComponent<Button>();
        button.targetGraphic = targetGraphic;
        button.onClick.AddListener(() => action());
        SetLayout(buttonObject, 1, 0, -1, preferredHeight);
        if (!UseDecorativeUiSkins || texture == null)
        {
            AddButtonAccentStrip(buttonObject.transform, fallbackColor);
        }
        Color textColor = UseDecorativeUiSkins && texture != null ? Ink : new Color32(235, 238, 240, 255);
        var text = CreateText(buttonObject.transform, label, Mathf.Max(12, fontSize - 2), FontStyle.Bold, textColor);
        text.alignment = TextAnchor.MiddleCenter;
        text.horizontalOverflow = HorizontalWrapMode.Wrap;
        text.verticalOverflow = VerticalWrapMode.Truncate;
        text.resizeTextMinSize = Mathf.Max(10, Mathf.RoundToInt(fontSize * 0.58f));
        text.gameObject.AddComponent<Shadow>().effectColor = UseDecorativeUiSkins && texture != null ? new Color32(255, 255, 255, 120) : new Color32(0, 0, 0, 180);
        var rect = text.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = new Vector2(14, 5);
        rect.offsetMax = new Vector2(-14, -5);
        return buttonObject;
    }

    private static void AddButtonAccentStrip(Transform parent, Color color)
    {
        var strip = new GameObject("Accent Strip");
        strip.transform.SetParent(parent, false);
        var rect = strip.AddComponent<RectTransform>();
        rect.anchorMin = new Vector2(0, 0);
        rect.anchorMax = new Vector2(0, 1);
        rect.pivot = new Vector2(0, 0.5f);
        rect.sizeDelta = new Vector2(6, 0);
        rect.anchoredPosition = Vector2.zero;
        var image = strip.AddComponent<Image>();
        image.color = new Color(color.r, color.g, color.b, 0.95f);
    }

    private static Image AddUiSkinImage(GameObject target, Texture2D texture, string resourcePath)
    {
        var image = target.AddComponent<Image>();
        image.sprite = CreateUiSkinSprite(texture, resourcePath);
        image.type = Image.Type.Sliced;
        image.color = Color.white;
        image.preserveAspect = false;
        return image;
    }

    private static Sprite CreateUiSkinSprite(Texture2D texture, string resourcePath)
    {
        float border = GetUiSkinBorder(texture, resourcePath);
        float horizontal = Mathf.Min(border, texture.width / 3f);
        float vertical = Mathf.Min(border, texture.height / 3f);
        return Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f),
            100f,
            0,
            SpriteMeshType.FullRect,
            new Vector4(horizontal, vertical, horizontal, vertical));
    }

    private static float GetUiSkinBorder(Texture2D texture, string resourcePath)
    {
        if (texture == null) return 0f;
        string path = resourcePath ?? "";
        float border = 36f;
        if (path.Contains("metrics_hud")) border = 18f;
        else if (path.Contains("speaker_nameplate")) border = 22f;
        else if (path.Contains("choice_card")) border = 30f;
        else if (path.Contains("dialogue_box")) border = 46f;
        else if (path.Contains("case_file_panel")) border = 58f;
        else if (path.Contains("session_result_sheet")) border = 64f;
        return Mathf.Min(border, Mathf.Min(texture.width, texture.height) / 3f);
    }

    private void CreateMetricRow(Transform parent, string label1, string value1, string label2, string value2, string label3, string value3)
    {
        var row = new GameObject("Metric Row");
        row.transform.SetParent(parent, false);
        SetLayout(row, 1, 0, -1, 96);
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
        var card = new GameObject("Metric");
        card.transform.SetParent(parent, false);
        var image = card.AddComponent<Image>();
        image.color = new Color32(245, 240, 227, 255);
        SetLayout(card, 1, 1, -1, -1);
        var labelText = CreateText(card.transform, label, 16, FontStyle.Normal, MutedInk);
        labelText.alignment = TextAnchor.MiddleLeft;
        AnchorMetricText(labelText, new Vector2(0f, 0.52f), new Vector2(1f, 0.92f));
        var valueText = CreateText(card.transform, value, 22, FontStyle.Bold, Ink);
        valueText.alignment = TextAnchor.MiddleLeft;
        AnchorMetricText(valueText, new Vector2(0f, 0.1f), new Vector2(1f, 0.54f));
    }

    private static void AnchorMetricText(Text text, Vector2 anchorMin, Vector2 anchorMax)
    {
        var rect = text.GetComponent<RectTransform>();
        rect.anchorMin = anchorMin;
        rect.anchorMax = anchorMax;
        rect.offsetMin = new Vector2(18, 0);
        rect.offsetMax = new Vector2(-18, 0);
    }

    private void CreateBar(Transform parent, string label, int count, int total, Color color)
    {
        var labelText = CreateText(parent, label + " · " + count, 14, FontStyle.Bold, parent.GetComponent<Image>() != null && parent.GetComponent<Image>().color == Paper ? Ink : Color.white);
        SetLayout(labelText.gameObject, 1, 0, -1, 19);
        var holder = new GameObject("Bar Holder");
        holder.transform.SetParent(parent, false);
        var holderImage = holder.AddComponent<Image>();
        holderImage.color = new Color32(70, 73, 82, 255);
        SetLayout(holder, 1, 0, -1, 12);
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
    private sealed class RouteSimulationResult
    {
        public string caseId;
        public string routeName;
        public bool applicable;
        public string[] selectedPath = Array.Empty<string>();
        public string[] routeTokens = Array.Empty<string>();
        public int highCount;
        public int lowCount;
        public bool finalHigh;
        public string expectedEndingSuffix;
        public string actualEndingId;
        public bool passed;
    }

    [Serializable]
    private sealed class SessionSelection
    {
        public int turn;
        public string choice;
        public string theoryId;
        public int routeQuality;
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
        public string routeFlags;
        public string endingId;
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
        public string cgResourcePath;

        public VnDialogueLine(string speakerId, string expressionId, string position, string text, string supervisorNote = "", string cgResourcePath = "")
        {
            this.speakerId = speakerId;
            this.expressionId = expressionId;
            this.position = position;
            this.text = text;
            this.supervisorNote = supervisorNote;
            this.cgResourcePath = cgResourcePath;
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
        public string reactionCgResourcePath;

        public VnChoice(string label, string theoryId, string interventionType, int quality, string feedback, string familyReaction, string reactionSpeakerId, string reactionExpressionId, string reactionCgResourcePath = "")
        {
            this.label = label;
            this.theoryId = theoryId;
            this.interventionType = interventionType;
            this.quality = quality;
            this.feedback = feedback;
            this.familyReaction = familyReaction;
            this.reactionSpeakerId = reactionSpeakerId;
            this.reactionExpressionId = reactionExpressionId;
            this.reactionCgResourcePath = reactionCgResourcePath;
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
        public string scriptKind;
        public int chapter;
        public string backgroundId;
        public string[] characters;
        public List<VnTurn> turns;

        public static VnCaseScript CreatePlaceholder(string caseId, int chapter)
        {
            return new VnCaseScript
            {
                caseId = caseId,
                scriptKind = "placeholder",
                chapter = chapter,
                backgroundId = "VN/Backgrounds/counseling_room_day",
                characters = new[] { "supervisor_system" },
                turns = new List<VnTurn>()
            };
        }
    }

    private sealed class VnEndingPresentation
    {
        public string gradeLabel;
        public string routeLabel;
        public Color gradeColor;
        public string title;
        public string body;
        public string supervisorNote;
        public string nextFocus;
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
