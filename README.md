# Family Therapy Practicum

`Family Therapy Practicum`은 가족치료를 공부하는 학생이 합성 가족 사례를 비주얼노벨형 상담 회기 안에서 접수하고, 이론 렌즈를 선택하고, 개입을 고르고, 슈퍼비전 리포트로 복기하는 Unity 기반 교육용 게임 프로젝트입니다.

이 프로젝트는 서울대학교 2026년 봄학기 `아동가족트렌드와 빅데이터 분석` 기말 프로젝트 용도로 제작되었습니다. 핵심 아이디어는 가족치료 수업에서 배운 이론을 단순 발표 자료가 아니라, 플레이어가 직접 선택하고 결과를 확인하는 상담 시뮬레이션 게임으로 구현하는 것입니다.

## 핵심 목표

- 가족치료 이론을 실제 회기 장면의 선택지와 피드백 규칙으로 번역합니다.
- 플레이어가 가족 사례를 한 사람의 문제로 고정하지 않고, 반복되는 관계 패턴과 가족 체계를 보도록 설계합니다.
- 상담 개입 선택을 점수, 가족 반응, 슈퍼바이저 피드백, 로그 데이터로 연결합니다.
- CSV/JSON/HTML export를 통해 게임 플레이가 학습 데이터로 남도록 만듭니다.
- 교육용 게임이지만, 가능한 한 상용 비주얼노벨/상담 시뮬레이션에 가까운 구성과 화면 흐름을 지향합니다.

## 현재 구현 범위

현재 빌드는 `FT-001 · 한부모 초등 자녀 가족`을 중심으로 실제 플레이 가능한 VN 회기 루트를 포함합니다.

- 메인 메뉴
- 사례 파일/사례 접수 화면
- 이론 렌즈 선택
- FT-001 비주얼노벨 회기
- 상담자 개입 선택지
- 가족 반응 장면
- 슈퍼비전 리포트
- 학습 로그 대시보드
- CSV/JSON/HTML export
- 저장/불러오기 UI
- 1600x900 visual audit용 자동 캡처 루틴
- UI smoke test 루틴

## 가족치료 이론 렌즈

게임은 아래 이론 렌즈를 중심으로 사례를 해석하고 피드백을 제공합니다.

- 가족체계 기본
- Bowen 다세대 가족치료
- 전략적 가족치료
- 구조적 가족치료
- Satir/경험적 가족치료
- 정신역동 가족치료
- 인지행동 가족치료
- 해결중심 가족치료
- 이야기치료

각 이론은 `FamilyTherapyPracticumGame.cs` 내부의 이론 데이터, 추천 개입, 슈퍼바이저 프로필, 선택지 피드백과 연결되어 있습니다.

## 주요 등장인물

FT-001의 핵심 가족 구성원과 주변 인물은 다음과 같습니다.

- 어머니: 박성빈
- 자녀: 이주형
- 외조모: 오선진
- 담임: 서건창

슈퍼바이저 이름은 사용자가 직접 지정한 이름을 유지합니다.

- 가족체계 기본: 김혜성
- Bowen: 안우진
- 전략적: 김윤하
- 구조적: 이정후
- Satir: 김연주
- 정신역동: 송성문
- 인지행동: 정세영
- 해결중심: 송지후
- 이야기치료: 박병호

## 프로젝트 구조

```text
Assets/
  Editor/
    FamilyTherapyPracticumBuilder.cs
      Unity 메뉴 기반 scene 생성 및 Windows 빌드 스크립트

  Scripts/
    FamilyTherapyPracticumGame.cs
      게임 본체. 데이터, UI, VN 흐름, 점수, 로그 export, visual audit 포함

  Scenes/
    FamilyTherapyPracticum.unity
      실행 대상 메인 씬

  Resources/
    VN/
      Backgrounds/
      Characters/
      EventCG/
      UI/
      런타임에서 Resources.Load로 불러오는 VN 이미지 리소스

  ConceptArt/
    초기 콘셉트 및 리뷰용 이미지

Docs/
  기획서, 구현 기록, 이미지 생성 프롬프트, 감사 로그, 발표/QA 자료

Packages/
  Unity package manifest

ProjectSettings/
  Unity 프로젝트 설정
```

## 중요한 문서

- `Docs/VISUAL_NOVEL_REMAKE_PLAN.md`
  - 기존 학습 앱을 비주얼노벨형 상담 게임으로 바꾸기 위한 리메이크 계획

- `Docs/TEN_DOLLAR_COMMERCIAL_GAME_DESIGN_DOC.md`
  - 상용 VN/상담 시뮬레이션 수준을 목표로 한 큰 프로젝트 설계 문서

- `Docs/CHARACTER_NAME_REGISTRY.md`
  - 캐릭터 및 슈퍼바이저 이름 정리

- `Docs/PROMPT_FOR_IMAGE_GENERATION_WINDOW.md`
  - 이미지 생성 전담 창에 전달하기 위한 프롬프트 모음

- `Docs/PROMPT_FOR_THIS_WINDOW_NON_IMAGE_WORK.md`
  - 코드/시스템 작업 전담 창의 작업 지시 문서

- `Docs/UI_ASSET_PRODUCTION_PLAN.md`
  - UI 이미지 에셋 제작 계획과 적용 기록

- `Docs/ASSET_REMEDIATION_PLAN_2026-06-08.md`
  - placeholder 이미지와 최종 이미지의 구분 및 정리 계획

- `Docs/FT001_COMPLETION_TARGET_2026-06-08.md`
  - FT-001 완성 기준

- `Docs/FT001_COMPLETION_PROGRESS_2026-06-08.md`
  - FT-001 이미지/구현 진행 기록

## 개발 환경

- Unity Editor: `6000.4.5f1`
- Target platform: Windows 64-bit
- 기본 해상도: `1600x900`
- 기본 실행 방식: Windowed
- 주요 언어: C#
- UI: Unity UGUI 기반 런타임 생성 UI

## 실행 방법

Unity Hub에서 이 폴더를 프로젝트로 열고 아래 씬을 실행합니다.

```text
Assets/Scenes/FamilyTherapyPracticum.unity
```

씬이 없거나 재생성이 필요하면 Unity 상단 메뉴에서 실행합니다.

```text
Family Therapy Practicum > Create Main Scene
```

## Windows 빌드 방법

Unity 메뉴에서 빌드할 수 있습니다.

```text
Family Therapy Practicum > Build Windows
```

배치 모드 빌드 예시는 다음과 같습니다.

```powershell
& 'C:\Program Files\Unity\Hub\Editor\6000.4.5f1\Editor\Unity.exe' `
  -batchmode -quit `
  -projectPath 'C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity' `
  -executeMethod FamilyTherapyPracticumBuilder.BuildWindows `
  -logFile 'Logs\build_windows.log'
```

빌드 산출물은 기본적으로 아래에 생성됩니다.

```text
Builds/Windows/FamilyTherapyPracticum.exe
```

`Builds/`는 Git에 올리지 않습니다. 저장소를 받은 사람은 Unity에서 다시 빌드해야 합니다.

## 자동 검증 루틴

게임 스크립트에는 배치 실행용 검증 루틴이 들어 있습니다.

### UI smoke test

```powershell
& '.\Builds\Windows\FamilyTherapyPracticum.exe' `
  -batchmode -nographics `
  -familyTherapyUiSmokeTest `
  -logFile '.\Logs\ui_smoke.log'
```

성공하면 LocalLow export 폴더에 `family_therapy_practicum_ui_smoke_result.json`이 생성됩니다.

### Visual audit

```powershell
& '.\Builds\Windows\FamilyTherapyPracticum.exe' `
  -screen-fullscreen 0 `
  -screen-width 1600 `
  -screen-height 900 `
  -familyTherapyVisualAudit `
  -logFile '.\Logs\visual_audit.log'
```

주요 화면 캡처와 `visual_audit_result.json`이 아래 경로에 저장됩니다.

```text
%USERPROFILE%\AppData\LocalLow\Family Therapy Practicum\Family Therapy Practicum\FamilyTherapyPracticumExports\visual_audit_1600x900
```

## Export 데이터

게임은 플레이 로그와 사례 데이터를 아래 형식으로 export할 수 있습니다.

- `player_choice_log.csv`
- `player_choice_log.json`
- `case_dataset.csv`
- `case_dataset.json`
- `dashboard.html`

export 데이터는 사용자의 LocalLow 폴더에 저장됩니다. 이 데이터는 개인별 실행 결과물이므로 Git에 포함하지 않습니다.

## 이미지 에셋 상태

이 프로젝트는 이미지 에셋을 단계적으로 제작하는 중입니다.

- FT-001 가족 캐릭터 일부와 상담실 배경이 프로젝트에 포함되어 있습니다.
- 과거에 대량 생성된 placeholder성 이미지들은 최종물로 계산하지 않습니다.
- placeholder 감사를 위해 격리된 대량 파일은 `Assets/_PlaceholderAudit/`에 있었으나, GitHub 백업에서는 제외합니다.
- UI 에셋은 `Docs/UI_ASSET_PRODUCTION_PLAN.md` 기준으로 별도 제작/검수 대상입니다.

## GitHub 백업 정책

이 저장소에는 실제 프로젝트 소스와 문서, Unity 설정, 현재 사용 가능한 경량 리소스만 올립니다.

Git에 포함하는 주요 항목:

- `Assets/Editor`
- `Assets/Scripts`
- `Assets/Scenes`
- `Assets/Resources`
- `Assets/ConceptArt`
- `Docs`
- `Packages`
- `ProjectSettings`
- `.gitignore`
- `README.md`

Git에서 제외하는 주요 항목:

- `Library/`
- `Logs/`
- `Builds/`
- `Temp/`
- `Obj/`
- `UserSettings/`
- `Assets/_PlaceholderAudit/`
- Unity가 자동 생성하는 `.csproj`, `.sln`, user 파일

## 교육적 주의사항

이 프로젝트는 학습용 시뮬레이션입니다.

- 실제 상담, 진단, 치료를 대체하지 않습니다.
- 사례는 합성 사례이며 실제 개인정보를 사용하지 않습니다.
- 점수는 교육적 피드백을 위한 게임 규칙입니다.
- AI 슈퍼바이저 관련 문구는 현재 빌드에서 API 호출 없이 placeholder 코멘트로만 표시됩니다.

## 현재 한계와 다음 작업

현재 버전은 기말 프로젝트용 prototype과 production direction 사이에 있습니다. 게임 흐름과 데이터 구조는 작동하지만, 상용화 수준을 목표로 하려면 다음 작업이 남아 있습니다.

- FT-001 전체 표정 세트 완성
- 슈퍼바이저 캐릭터 이미지 확장
- FT-002 이후 핵심 사례의 VN 대본/이미지 제작
- UI 이미지 에셋 최종화
- 선택지 분기와 가족 반응 다양화
- 결과 리포트와 대시보드 시각 품질 개선
- 실제 발표용 시연 루트 안정화

## 라이선스/사용 범위

현재 저장소는 수업 프로젝트 백업 용도입니다. 이미지 생성물과 코드의 외부 재사용 범위는 추후 별도 정리가 필요합니다.
