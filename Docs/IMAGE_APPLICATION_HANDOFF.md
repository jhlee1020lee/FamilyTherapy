# 이미지 적용 협업 Handoff

- 작성일: 2026-06-08
- 목표: 이미지 생성 창은 파일을 만들고, 이 창은 Unity 적용과 검증을 담당한다.
- 원칙: 파일명은 이름이 아니라 역할/이론 ID 기준으로 유지한다.

## 현재 자동 적용 구조

Unity는 아래 경로의 png를 `Resources.Load<Texture2D>()`로 자동 로드한다.

```text
Assets/Resources/VN/Backgrounds
Assets/Resources/VN/Characters/FT001
Assets/Resources/VN/Characters/Chapter01/FT001
Assets/Resources/VN/Characters/Chapter01/FT002
...
Assets/Resources/VN/Characters/Chapter01/FT024
Assets/Resources/VN/Characters/CoreCases/FT011
Assets/Resources/VN/Characters/CoreCases/FT012
...
Assets/Resources/VN/Characters/CoreCases/FT024
Assets/Resources/VN/Characters/Supervisors
Assets/Resources/VN/UI
Assets/Resources/VN/EventCG
```

파일을 올바른 이름으로 넣으면 별도 코드 수정 없이 다음 화면에 적용된다.

- 배경: `VN/Backgrounds/{background_id}`
- 캐릭터: `{baseAssetPath}_{expression}`
- Chapter01 케이스 캐릭터: `Assets/Resources/VN/Characters/Chapter01/{FT번호}/{ft번호_역할}_{expression}.png`
- UI: `VN/UI/{ui_asset_id}`

Chapter01 케이스 폴더는 런타임에서 자동 스캔한다. 예를 들어 아래 파일이 있으면 `ft002_mother` 캐릭터가 자동 등록되고, `neutral`, `defensive`, `softened` 표정이 회기 중 로드된다.

```text
Assets/Resources/VN/Characters/Chapter01/FT002/ft002_mother_neutral.png
Assets/Resources/VN/Characters/Chapter01/FT002/ft002_mother_defensive.png
Assets/Resources/VN/Characters/Chapter01/FT002/ft002_mother_softened.png
```

## 지금 적용된 화면

- 메인 타이틀
  - `counseling_room_day`
  - `case_file_panel`
  - `choice_card_question`
  - `choice_card_intervention`
  - `metrics_hud`
- VN 회기
  - `dialogue_box`
  - `speaker_nameplate`
  - `choice_card_intervention`
  - `choice_card_question`
  - `supervisor_note_panel`
  - `metrics_hud`
- 캐릭터
  - FT-001 가족 표정 세트
  - 슈퍼바이저 기본/설명/질문/경고/승인/성찰 세트
  - Chapter01 `FT-001`~`FT-010` 케이스별 캐릭터 세트 자동 등록
  - CoreCases `FT-011`~`FT-024` 케이스별 캐릭터 세트 자동 등록

## 현재 검증된 수치

- `Assets/Resources/VN` PNG: 762장
- 런타임 캐릭터 프로필: 108개
- 레거시 VN 캐릭터/UI 필수 자산: 44/44
- 레거시 VN 캐릭터/UI 누락 필수 자산: 없음
- FT002~FT010 EventCG 슬롯 감사: 385장 필요 / 21장 사용 가능 / 364장 누락
- VN 플레이 가능 사례: 60/60
- 최신 검증 로그:
  - `Logs/corecases_asset_apply_build.log`
  - `Logs/corecases_asset_apply_ui_smoke_rerun.log`
  - `Logs/corecases_asset_apply_smoke_rerun.log`

## 이미지 창 작업 순서

1. `Assets/Resources/VN/UI`의 8개 UI 이미지를 먼저 polish한다.
   - `dialogue_box.png`
   - `speaker_nameplate.png`
   - `choice_card_question.png`
   - `choice_card_intervention.png`
   - `supervisor_note_panel.png`
   - `case_file_panel.png`
   - `metrics_hud.png`
   - `session_result_sheet.png`
2. FT-001 표정 세트를 같은 스타일로 보강한다.
3. 슈퍼바이저 9명 6표정 세트를 통일한다.
4. 표정 차이가 약한 캐릭터 세트를 같은 파일명으로 다시 생성해서 덮어쓴다.
5. 이후 핵심 24개 이후의 훈련 사례 전체로 확장한다.

## 다른 이미지 생성 창에 바로 줄 작업 지시

다른 창은 코드를 만지지 않고 png만 추가한다. 파일은 반드시 아래 규칙을 지킨다.

```text
프로젝트 루트:
C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity

캐릭터 저장:
Assets\Resources\VN\Characters\Chapter01\FT012\ft012_mother_neutral.png
Assets\Resources\VN\Characters\Chapter01\FT012\ft012_mother_anxious.png
Assets\Resources\VN\Characters\Chapter01\FT012\ft012_child_neutral.png
Assets\Resources\VN\Characters\Chapter01\FT012\ft012_child_withdrawn.png

배경 저장:
Assets\Resources\VN\Backgrounds\{background_id}.png

UI 저장:
Assets\Resources\VN\UI\{ui_asset_id}.png
```

표정 suffix는 현재 자동 인식된다.

```text
neutral, anxious, defensive, exhausted, softened, worried, tearful, listening,
withdrawn, scared, quiet, relieved, hesitant, critical, stubborn, concerned,
procedural, explaining, questioning, warning, approving, reflective, supportive
```

## 적용 후 검증 명령

```powershell
& 'C:\Program Files\Unity\Hub\Editor\6000.4.5f1\Editor\Unity.exe' -batchmode -quit -projectPath 'C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity' -executeMethod FamilyTherapyPracticumBuilder.BuildWindows -logFile 'C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity\Logs\image_apply_build.log'

& 'C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity\Builds\Windows\FamilyTherapyPracticum.exe' -batchmode -nographics -familyTherapyUiSmokeTest -logFile 'C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity\Logs\image_apply_ui_smoke.log'
```

성공 기준:

- `family_therapy_practicum_ui_smoke_result.json`
  - `completed=true`
  - `hudCount >= 1`
  - `dialogueCount >= 1`
  - `characterImageCount >= 2`
- 일반 smoke JSON
  - `missingVnAssets=[]`
  - `vnPlayableCaseCount=60`
  - `commercialCoreVnScriptCount=24`
- FT002~FT010 VN data audit
  - `routeSimulationAudit.allRoutesPassed=true`
  - 완성 판정 시 `availableCgSlotCount == requiredCgSlotCount`
  - 완성 판정 시 `missingCgSlotCount == 0`
