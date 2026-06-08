# VN Asset Remediation Plan 2026-06-08

## Summary

현재 `Assets/Resources/VN`에는 런타임 PNG 762개가 존재하지만, 상당수가 실제 생성 결과물이 아니라 기존 이미지 기반의 색상, 표정, 오버레이 변형입니다. 사용자가 지적한 것처럼 표정 차이가 거의 없는 파일들이 포함되어 있으므로, 기존 대량 산출물은 최종 납품물이 아니라 placeholder로 간주합니다.

앞으로의 작업은 기존 파일을 삭제하지 않고 격리한 뒤, `image_gen` 기반으로 단계별 재생성합니다. 단순 색상 변경, 입/눈 픽셀 오버레이, 배경 필터, 기존 이미지 합성만으로 만든 파일은 최종 수량에 포함하지 않습니다.

## Key Decisions

- 기존 대량 산출물은 `Assets/_PlaceholderAudit/VN_2026-06-08/`로 이동한다.
- `.png`와 해당 `.png.meta`는 항상 함께 이동한다.
- 최종 런타임 후보만 `Assets/Resources/VN/...`에 다시 배치한다.
- Unity 코드, 빌드 설정, smoke-test 문서, 기존 프롬프트 문서는 수정하지 않는다.
- 파일 ID는 한국어 표시명이 아니라 기존 규칙처럼 role/theory 기반 영문 ID를 유지한다.
- approved style reference는 `Assets/ConceptArt/StyleTest_2026-06-08` 기준으로 유지한다.
- 실패한 생성분은 placeholder로 채우지 않고 실패로 기록한다.

## Implementation Changes

### 1. Quarantine and Audit

- `Assets/Resources/VN`의 현재 PNG 762개를 최종물이 아닌 placeholder로 분류한다.
- 이동 대상:
  - `Assets/Resources/VN/Characters`
  - `Assets/Resources/VN/Backgrounds`
  - `Assets/Resources/VN/UI`
  - `Assets/Resources/VN/EventCG`
- 이동 위치:
  - `Assets/_PlaceholderAudit/VN_2026-06-08/Characters`
  - `Assets/_PlaceholderAudit/VN_2026-06-08/Backgrounds`
  - `Assets/_PlaceholderAudit/VN_2026-06-08/UI`
  - `Assets/_PlaceholderAudit/VN_2026-06-08/EventCG`
- 이동 전후에 파일 수, `.meta` 누락 여부, PNG decode 가능 여부를 기록한다.
- 기존 산출물은 삭제하지 않는다. 비교와 원인 분석용으로 보존한다.

### 2. Generation Rules

- 모든 캐릭터 표정/포즈/이벤트 CG는 `image_gen`으로 새로 생성한다.
- 한 캐릭터의 표정 세트는 다음 기준을 통과해야 한다:
  - 눈썹, 눈, 입, 턱/볼 긴장, 시선 중 최소 3개 요소가 감정별로 다르다.
  - neutral, worried, angry, sad, relieved, reflective 등이 육안으로 즉시 구분된다.
  - 같은 얼굴에 작은 선만 덧그은 수준이면 reject 처리한다.
- 캐릭터별 일관성은 유지하되, 가족 구성원 간 얼굴형, 나이, 체형, 복장 실루엣은 분명히 다르게 만든다.
- 배경은 단순 색 보정 변형을 금지한다. 장소, 카메라 각도, 시간대, 상담실 구성 요소가 실제로 달라야 한다.
- EventCG는 캐릭터 스프라이트를 배경 위에 올린 합성물이 아니라 장면 단위 일러스트로 생성한다.
- UI는 필요 시 직접 제작할 수 있으나, 단순한 임시 도형 묶음은 commercial-quality 수량에 포함하지 않는다.

### 3. Phase Plan

- Phase 0: style lock
  - approved style reference를 기준으로 캐릭터 1명, 배경 1장, EventCG 1장을 재생성한다.
  - 사용자가 이 3개 샘플을 확인하기 전에는 대량 생성하지 않는다.
- Phase 1: FT001 core family
  - `FT001` 핵심 가족 스프라이트를 우선 생성한다.
  - 각 인물별 표정 차이가 명확한지 검수한다.
  - 실패 파일은 같은 이름으로 덮어쓰지 않고 reject 목록에 기록한다.
- Phase 2: supervisors
  - supervisors 60개 목표를 재생성한다.
  - supervisor별 이론/역할 차이가 복장, 나이대, 태도, 색감으로 구분되어야 한다.
  - 설명, 승인, 질문 표정은 실제 감정 변화가 보일 때만 통과시킨다.
- Phase 3: chapter and core case characters
  - `Chapter01`, `CoreCases` 캐릭터를 케이스 단위로 나누어 생성한다.
  - 한 번에 대량 처리하지 않고 케이스별 검수 후 다음 케이스로 넘어간다.
- Phase 4: backgrounds
  - 상담실, 가정, 학교, 복지기관 등 장면 기능별로 40개 목표를 생성한다.
  - 같은 배경의 필터/크롭 변형은 별도 배경으로 계산하지 않는다.
- Phase 5: EventCG
  - 치료 장면, 갈등 장면, 상담 개입 장면 중심으로 50개 목표를 생성한다.
  - 각 CG는 장면 설명, 등장인물, 감정 초점, 사용 위치를 기록한다.
- Phase 6: UI assets
  - 실제 게임 UI에 필요한 버튼, 패널, 선택지, 상태 표시, 상담 노트 계열 에셋을 정리한다.
  - 임시 느낌의 단색 박스나 장식 반복은 최종 UI 수량에 포함하지 않는다.

### 4. Reporting and Review

- 각 Phase 종료 시 다음 항목을 보고한다:
  - 생성 성공 수
  - 실패 수
  - reject 사유
  - 스타일 일관성 평가
  - 표정/장면 다양성 평가
  - 다음 Phase 진행 전 사용자 확인 필요 여부
- "완료"라는 표현은 해당 Phase의 검수 기준을 통과한 파일에만 사용한다.
- 목표 수량을 채우지 못했으면 "미완료"로 기록한다.

## Test Plan

- PNG 검증:
  - 모든 최종 PNG가 열리고 decode 되는지 확인한다.
  - `.png.meta`가 누락되지 않았는지 확인한다.
- 수량 검증:
  - 최종 후보만 `Assets/Resources/VN`에서 카운트한다.
  - quarantine 파일은 최종 수량에서 제외한다.
- 품질 검증:
  - 캐릭터별 contact sheet를 만들어 표정 차이를 육안 확인한다.
  - 배경은 장소, 구도, 시간대 차이를 확인한다.
  - EventCG는 단순 합성 여부를 확인한다.
- Unity 검증:
  - 에셋 경로가 기존 런타임 규칙과 맞는지 확인한다.
  - 코드 수정 없이 Unity가 새 에셋을 import할 수 있는지 확인한다.
  - smoke-test 문서는 수정하지 않는다.

## Acceptance Criteria

- 기존 placeholder 산출물은 최종물로 계산하지 않는다.
- 핵심 캐릭터 표정은 썸네일 크기에서도 구분 가능해야 한다.
- 가족 구성원, supervisor, 케이스 캐릭터가 서로 복붙처럼 보이지 않아야 한다.
- 배경과 EventCG는 실제 장면 설계가 보여야 한다.
- 최종 보고서에는 성공, 실패, 보류가 분리되어 있어야 한다.
- 사용자가 검수하기 전 다음 대량 Phase로 넘어가지 않는다.

## Assumptions

- 현재 런타임 PNG 762개는 보존하되 placeholder로 취급한다.
- 사용자가 선택한 방향은 "격리 후 재생성"이다.
- 최종 목표 수량은 기존 프롬프트의 약 750개를 기준으로 하되, 품질 미달 파일은 수량에 넣지 않는다.
- `image_gen` 장애가 발생하면 해당 항목은 실패로 기록하고 임시 변형으로 대체하지 않는다.
- 실제 파일 생성, 이동, 검수 기록은 이 문서의 기준에 따라 수행한다.
