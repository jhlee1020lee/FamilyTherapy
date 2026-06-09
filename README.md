# Family Therapy Practicum

`Family Therapy Practicum`은 가족치료를 공부하는 학생이 합성 가족 사례를 비주얼노벨형 상담 회기 안에서 접수하고, 이론 렌즈를 선택하고, 개입을 고르고, 슈퍼비전 리포트로 복기하는 Unity 기반 교육용 게임 프로젝트입니다.

이 저장소는 서울대학교 2026년 봄학기 `아동가족트렌드와 빅데이터 분석` 기말 프로젝트 작업물을 백업하기 위한 저장소입니다. 발표 자료만 만드는 대신, 가족치료 수업에서 배운 이론을 실제 선택형 상담 시뮬레이션으로 번역하는 것을 목표로 합니다.

## 프로젝트 한 줄 요약

플레이어는 가족치료 수련생이 되어 `FT-001 · 한부모 초등 자녀 가족` 사례를 맡고, 가족 구성원의 대화를 관찰한 뒤, 가족체계/보웬/전략적/구조적/Satir/정신역동/인지행동/해결중심/이야기치료 렌즈에 따라 개입을 선택합니다. 선택 결과는 가족 반응, 점수, 슈퍼바이저 피드백, 플레이 로그로 남습니다.

## 핵심 목표

- 가족치료 이론을 실제 회기 장면의 선택지와 피드백 규칙으로 번역합니다.
- 플레이어가 한 사람을 문제로 고정하지 않고, 반복되는 상호작용과 가족 체계를 보도록 설계합니다.
- 상담 개입 선택을 점수, 가족 반응, 슈퍼바이저 피드백, 로그 데이터와 연결합니다.
- CSV/JSON/HTML export를 통해 플레이 경험이 학습 데이터로 남도록 만듭니다.
- 교육용 프로젝트이지만, 화면 흐름과 연출은 상용 비주얼노벨/상담 시뮬레이션을 목표로 확장 중입니다.
- 발표용 데모가 아니라, 장기적으로 완성형 게임 프로젝트로 확장 가능한 구조를 지향합니다.

## 현재 구현 범위

현재 빌드는 `FT-001 · 한부모 초등 자녀 가족`을 중심으로 실제 플레이 가능한 비주얼노벨 회기 루트를 포함합니다.

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
- FT-001 가족 캐릭터 이미지 일부 교체 및 적용
- VN UI 이미지 에셋 적용 실험

## 게임 플레이 구조

전체 플레이 흐름은 아래 구조를 기준으로 설계되어 있습니다.

```text
메인 메뉴
  -> 사례 파일 확인
  -> 사례 접수/초기 정보 확인
  -> 이론 렌즈 선택
  -> VN 회기 장면
  -> 상담자 개입 선택
  -> 가족 반응 확인
  -> 슈퍼바이저 피드백
  -> 다음 회기 또는 리포트
  -> 로그/데이터 export
```

현재는 FT-001을 중심으로 위 흐름이 연결되어 있으며, 후속 사례는 같은 데이터 구조에 맞춰 추가할 수 있도록 설계했습니다.

## 가족치료 이론 렌즈

게임은 아래 이론 렌즈를 중심으로 사례를 해석하고 피드백을 제공합니다.

| 이론 렌즈 | 게임 내 역할 |
| --- | --- |
| 가족체계 기본 | 개인 증상보다 상호작용 순환과 가족 전체의 균형을 보도록 유도 |
| Bowen 다세대 가족치료 | 분화, 삼각관계, 정서적 반응성, 세대 간 패턴을 해석 |
| 전략적 가족치료 | 문제 유지 순환과 증상 기능을 파악하고 역설/과제 중심 개입을 설계 |
| 구조적 가족치료 | 하위체계, 경계, 위계, 연합 구조를 파악 |
| Satir/경험적 가족치료 | 감정, 자기존중감, 의사소통 방식, 일치성을 강조 |
| 정신역동 가족치료 | 반복되는 관계 경험, 방어, 무의식적 갈등을 해석 |
| 인지행동 가족치료 | 자동사고, 행동 패턴, 강화 구조, 실천 과제를 다룸 |
| 해결중심 가족치료 | 예외, 자원, 목표, 작은 변화를 탐색 |
| 이야기치료 | 문제의 외재화, 지배적 이야기, 대안적 정체성을 다룸 |

각 이론은 `Assets/Scripts/FamilyTherapyPracticumGame.cs` 내부의 이론 데이터, 추천 개입, 슈퍼바이저 프로필, 선택지 피드백과 연결되어 있습니다.

## FT-001 사례 개요

FT-001은 한부모 가정에서 초등학생 자녀의 등교 거부와 아침 갈등이 반복되는 사례입니다. 게임은 이 문제를 단순히 자녀의 의지 부족이나 어머니의 양육 문제로 고정하지 않고, 가족 구성원과 학교 체계가 만드는 반복 순환으로 다루도록 설계되었습니다.

핵심 관찰 지점은 다음과 같습니다.

- 아침 등교 준비 장면에서 갈등이 반복됩니다.
- 어머니는 생계 노동과 양육 책임을 동시에 감당하며 쉽게 소진됩니다.
- 자녀는 혼자 남겨지는 시간과 가족 갈등 사이에서 불안을 표현합니다.
- 외조모는 걱정과 비난이 섞인 방식으로 가족에 개입합니다.
- 담임은 학교 결석과 적응 문제를 제도적 언어로 전달합니다.
- 플레이어는 가족의 순환을 끊는 개입을 선택해야 합니다.

## 주요 등장인물

FT-001의 핵심 가족 구성원과 주변 인물은 다음과 같습니다.

| 역할 | 이름 | 설명 |
| --- | --- | --- |
| 어머니 | 박성빈 | 생계와 양육을 동시에 감당하는 한부모. 반복되는 아침 갈등 속에서 지쳐 있음 |
| 자녀 | 이주형 | 등교 거부와 분리 불안을 보이는 초등학생. 말로 표현하지 못한 불안이 행동으로 나타남 |
| 외조모 | 오선진 | 걱정이 많지만 비난처럼 들리는 방식으로 가족에 개입함 |
| 담임 | 서건창 | 학교 결석과 적응 문제를 전달하는 주변 체계 인물 |

슈퍼바이저 이름은 사용자가 직접 지정한 이름을 유지합니다.

| 이론 렌즈 | 슈퍼바이저 |
| --- | --- |
| 가족체계 기본 | 김혜성 |
| Bowen | 안우진 |
| 전략적 | 김윤하 |
| 구조적 | 이정후 |
| Satir | 김연주 |
| 정신역동 | 송성문 |
| 인지행동 | 정세영 |
| 해결중심 | 송지후 |
| 이야기치료 | 박병호 |

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
        상담실 등 VN 배경 리소스

      Characters/
        공통 캐릭터 리소스

      Characters/FT001/
        FT-001 가족 구성원 및 주변 인물 스프라이트

      EventCG/
        주요 장면용 이미지

      UI/
        VN 프레임, 대화창, 버튼, 패널 등 UI 이미지 에셋

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

| 문서 | 용도 |
| --- | --- |
| `Docs/VISUAL_NOVEL_REMAKE_PLAN.md` | 기존 학습 앱을 비주얼노벨형 상담 게임으로 바꾸기 위한 리메이크 계획 |
| `Docs/TEN_DOLLAR_COMMERCIAL_GAME_DESIGN_DOC.md` | 상용 VN/상담 시뮬레이션 수준을 목표로 한 장기 설계 문서 |
| `Docs/CHARACTER_NAME_REGISTRY.md` | 캐릭터 및 슈퍼바이저 이름 정리 |
| `Docs/PROMPT_FOR_IMAGE_GENERATION_WINDOW.md` | 이미지 생성 전담 창에 전달하기 위한 프롬프트 모음 |
| `Docs/PROMPT_FOR_THIS_WINDOW_NON_IMAGE_WORK.md` | 코드/시스템 작업 전담 창의 작업 지시 문서 |
| `Docs/UI_ASSET_PRODUCTION_PLAN.md` | UI 이미지 에셋 제작 계획과 적용 기록 |
| `Docs/ASSET_REMEDIATION_PLAN_2026-06-08.md` | placeholder 이미지와 최종 이미지의 구분 및 정리 계획 |
| `Docs/FT001_COMPLETION_TARGET_2026-06-08.md` | FT-001 완성 기준 |
| `Docs/FT001_COMPLETION_PROGRESS_2026-06-08.md` | FT-001 이미지/구현 진행 기록 |
| `Docs/ASSET_REVIEW_APPLY_DECISIONS_2026-06-09.md` | 런타임 이미지 검수와 적용 결정 기록 |

## 개발 환경

- Unity Editor: `6000.4.5f1`
- Target platform: Windows 64-bit
- 기본 해상도: `1600x900`
- 기본 실행 방식: Windowed
- 주요 언어: C#
- UI: Unity UGUI 기반 런타임 생성 UI
- 리소스 로딩: `Resources.Load`
- 주요 개발 OS: Windows

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

visual audit는 주요 UI 화면에서 텍스트 오버플로, 화면 밖 Rect, 이미지 로딩 상태를 점검하기 위해 사용합니다.

## Export 데이터

게임은 플레이 로그와 사례 데이터를 아래 형식으로 export할 수 있습니다.

- `player_choice_log.csv`
- `player_choice_log.json`
- `case_dataset.csv`
- `case_dataset.json`
- `dashboard.html`

export 데이터는 사용자의 LocalLow 폴더에 저장됩니다. 이 데이터는 개인별 실행 결과물이므로 Git에 포함하지 않습니다.

## 이미지 에셋 상태

이 프로젝트는 이미지 에셋을 단계적으로 제작하는 중입니다. 현재 저장소에는 FT-001 가족 캐릭터 이미지 일부, 상담실 배경, VN UI 이미지 에셋, 검수용 contact sheet 문서가 포함되어 있습니다.

이미지 관련 현재 원칙은 다음과 같습니다.

- FT-001 가족 캐릭터는 정면형 VN 스프라이트를 기준으로 적용합니다.
- 같은 인물이라도 감정/상황별 표정 차이가 필요합니다.
- 상담실 배경은 반복 사용 가능한 핵심 배경으로 유지합니다.
- UI 이미지는 임시 네모 박스에서 벗어나 VN 게임처럼 보이도록 계속 교체합니다.
- 과거 대량 생성 placeholder 이미지는 최종 에셋으로 계산하지 않습니다.
- placeholder 감사/격리 폴더인 `Assets/_PlaceholderAudit/`은 Git에서 제외합니다.

## 저장소에 포함하는 것과 제외하는 것

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
- 런타임 export 결과물

## 교육적 주의사항

이 프로젝트는 학습용 시뮬레이션입니다.

- 실제 상담, 진단, 치료를 대체하지 않습니다.
- 사례는 합성 사례이며 실제 개인정보를 사용하지 않습니다.
- 점수는 교육적 피드백을 위한 게임 규칙입니다.
- AI 슈퍼바이저 관련 문구는 현재 빌드에서 API 호출 없이 placeholder 코멘트로만 표시됩니다.
- 실제 임상 판단이 필요한 상황에서는 전문가의 개입이 필요합니다.

## 현재 한계

현재 버전은 기말 프로젝트용 playable build와 production direction 사이에 있습니다. 게임 흐름과 데이터 구조는 작동하지만, 상용화 수준을 목표로 하려면 다음 작업이 남아 있습니다.

- 메인 화면을 대시보드가 아니라 게임 타이틀 화면처럼 재구성
- 첫 플레이어가 당황하지 않도록 프롤로그/브리핑 화면 추가
- FT-001 대사를 더 길고 자연스러운 대화로 확장
- FT-001 전체 표정 세트 완성
- 슈퍼바이저 캐릭터 이미지 확장
- FT-002 이후 핵심 사례의 VN 대본/이미지 제작
- UI 이미지 에셋 최종화
- 선택지 분기와 가족 반응 다양화
- 결과 리포트와 대시보드 시각 품질 개선
- 실제 발표용 시연 루트 안정화

## 다음 개발 우선순위

가장 가까운 우선순위는 다음 세 가지입니다.

1. 메인 메뉴를 상용 VN 게임의 타이틀 화면처럼 다시 구성합니다.
2. `캠페인 시작` 이후 바로 대사가 뜨지 않도록 첫 회기 브리핑/튜토리얼 화면을 넣습니다.
3. FT-001 회기 대사를 짧은 설명문이 아니라 실제 가족과 치료자가 주고받는 대화처럼 확장합니다.

이후에는 이미지 생성 전담 작업과 코드/UI 적용 작업을 분리해서 병렬로 진행하는 것이 효율적입니다.

## 백업 기록

이 저장소는 아래 원격 저장소로 백업합니다.

```text
https://github.com/jhlee1020lee/FamilyTherapy.git
```

현재 백업의 목적은 완성본 릴리스가 아니라, Unity 프로젝트 소스/문서/이미지 적용 상태를 안전하게 원격에 저장하는 것입니다. 빌드 산출물과 Unity 캐시 폴더는 저장소 크기와 재현성을 위해 제외합니다.

## 라이선스/사용 범위

현재 저장소는 수업 프로젝트 백업 용도입니다. 이미지 생성물, 코드, 문서의 외부 재사용 범위는 추후 별도 정리가 필요합니다.
