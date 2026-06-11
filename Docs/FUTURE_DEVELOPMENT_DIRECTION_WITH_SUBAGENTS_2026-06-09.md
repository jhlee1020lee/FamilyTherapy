# Family Therapy Practicum Future Development Direction

- 작성일: 2026-06-09
- 목적: `MULTI_AGENT_DESIGN_REVIEW_2026-06-09.md`와 4개 sub agent 리뷰를 통합해, 이후 개발 방향을 실행 가능한 작업 지시서로 고정한다.
- 결론: 현재 Unity 빌드는 완성본 품질이 아니다. 목표는 `Eliza`식 임상 비주얼노벨 목업을 실제 게임 UI/플레이 루프로 이식하고, FT-001 수직 슬라이스를 먼저 상용 VN 수준으로 끌어올리는 것이다.

## 1. 사용한 근거

### 기준 문서

- `Docs/MULTI_AGENT_DESIGN_REVIEW_2026-06-09.md`
- `Docs/ELIZA_UI_STYLE_BIBLE_2026-06-09.md`
- `Docs/ELIZA_UI_MOCKUP_REVIEW_2026-06-09.md`
- `Docs/LARGE_SCALE_STEAM_GAME_BENCHMARK_AND_REDESIGN_2026-06-09.md`

### 기준 이미지

- `Docs/GeneratedSources/Eliza_UI_Mockups_2026-06-09/04_case_file_supervisor_briefing.png`
- `Docs/GeneratedSources/Eliza_UI_Mockups_2026-06-09/06_session_dialogue.png`
- `Docs/GeneratedSources/Eliza_UI_Mockups_2026-06-09/07_intervention_choice.png`
- `Docs/GeneratedSources/Eliza_UI_Mockups_2026-06-09/09_supervision_report.png`
- `Docs/GeneratedSources/Eliza_UI_Mockups_2026-06-09/Eliza_UI_Mockups_ContactSheet_2026-06-09.jpg`

### 현재 코드 기준점

- `Assets/Scripts/FamilyTherapyPracticumGame.cs`
- 주요 수정 대상:
  - `ShowCaseBrowser`
  - `BeginCaseIntake`
  - `ShowVnSessionTurn`
  - `ShowVnChoiceDeck`
  - `ShowVnReaction`
  - `ShowSupervision`
  - `ShowDashboard`
  - `CreateVnHud`
  - `CreateVnDialogueBox`
  - `CreateText`

## 2. Sub Agent 통합 결론

### A. 상업 VN 아트디렉션

아트 방향은 새 콘셉트를 계속 추가하는 것이 아니라, 이미 만든 `Eliza` 목업의 완성도를 실제 Unity 화면에 이식하는 것이다. 현재 핵심 리스크는 아이디어 부족이 아니라 실행 화면이 상업 VN처럼 보이지 않는 점이다.

우선순위는 `07_intervention_choice` 선택지 화면, `06_session_dialogue` 상담 장면, `04_case_file_supervisor_briefing` 사례 파일, `09_supervision_report` 슈퍼비전 리포트 순서다.

핵심 기준:

- 캐릭터가 배경 위 PNG처럼 붙어 보이면 실패다.
- 상담 장면 위에 선택 UI가 자연스럽게 얹혀야 한다.
- 사례 파일은 정보 패널이 아니라 수련자 브리핑 장면이어야 한다.
- 슈퍼비전은 점수표가 아니라 회고 장면이어야 한다.

### B. 한국어 UX / 가독성

현재 감사 결과는 `textOverflowCount=0`, `offscreenRectCount=0`으로 깨지지는 않지만, tiny text 비율이 너무 높다. 다음 목표는 "안 깨짐"이 아니라 "읽고 판단할 수 있음"이다.

고정할 기준:

- 1024x768 기준 본문 최소 16px급.
- 보조 라벨 최소 13-14px급.
- 버튼 핵심 라벨 최소 15-16px급.
- 전체 tiny text 비율 25% 이하.
- 핵심 화면 tiny text 비율 20% 이하.
- 본문 overflow 0건, offscreen rect 0건 유지.

우선 교정 화면:

1. `07_ft001_choice_deck`
2. `04_ft001_intake`
3. `13_supervision_report`
4. `12_dashboard`

### C. 가족치료 게임 디자인

게임은 가족치료 개념을 설명하는 앱이 아니라, 상담자가 회기 안에서 가설을 세우고 발화하고 가족 반응을 관찰하는 시뮬레이션이어야 한다.

기본 루프:

```text
사례 파일 읽기
-> IP / 순환 / 경계 / 삼각관계 / 위험 신호 태깅
-> 슈퍼바이저 브리핑
-> 회기 진입
-> 상담자 발화 선택
-> 가족 반응 관찰
-> 가설 수정
-> 슈퍼비전 리포트
-> 다음 회기 또는 재시도
```

선택지는 이론명이나 기술명이 아니라 실제 상담자 발화여야 한다.

나쁜 방향:

```text
순환질문을 사용한다.
IP 비난을 피한다.
구조적 가족치료 개입을 선택한다.
```

좋은 방향:

```text
"주형이가 배가 아프다고 말하는 순간, 어머니와 선생님은 각각 무엇을 하게 되나요?"
"지금은 누가 잘못했는지보다, 이 장면이 반복되는 순서를 같이 살펴보고 싶습니다."
"어머니가 더 세게 말하게 되는 건, 주형이가 걱정되기 때문일 수도 있겠네요."
```

모든 선택지는 그럴듯해야 한다. 명백한 함정 선택지는 게임성을 죽인다.

### D. Unity 엔지니어링

현재 `FamilyTherapyPracticumGame.cs`는 데이터, 화면 전환, UI 생성, 저장, visual audit가 한 파일에 몰려 있다. 빠른 프로토타입에는 유리했지만, 이제는 UI 품질을 올리기 어렵다.

다만 지금 당장 UI Toolkit으로 갈아타지는 않는다. 현재 런타임 uGUI 기반을 유지하면서, 화면 함수를 컴포넌트 단위로 분해하는 것이 리스크가 낮다.

분리할 단위:

- `GameFlowController`
- `TherapySessionModel`
- `VnScriptRepository`
- `UiTheme`
- `VnRoot`
- `VnHud`
- `CharacterStage`
- `DialogueBox`
- `SupervisorNote`
- `CaseFilePanel`
- `InterventionChoiceList`
- `InterventionChoiceRow`
- `SupervisionReportPanel`

## 3. 최종 개발 방향

### 방향 1. FT-001 수직 슬라이스를 먼저 완성한다

전체 사례를 동시에 고치지 않는다. 먼저 FT-001 한 사례만 대상으로 아래 흐름을 완성한다.

```text
사례 파일
-> 슈퍼바이저 브리핑
-> 상담 대화
-> 개입 선택
-> 가족 즉시 반응
-> 슈퍼비전 리포트
```

이 한 흐름이 상업 VN처럼 보이지 않으면 사례 수를 늘려도 완성도는 올라가지 않는다.

### 방향 2. `Eliza` 목업을 참고가 아니라 사양으로 쓴다

현재 기준 이미지는 단순 분위기 참고가 아니다. 실제 구현 기준은 다음 네 장이다.

- `04_case_file_supervisor_briefing.png`: 사례 파일 / 브리핑 화면
- `06_session_dialogue.png`: 기본 회기 대화 화면
- `07_intervention_choice.png`: 상담자 개입 선택 화면
- `09_supervision_report.png`: 회기 후 슈퍼비전 리포트

Unity 화면이 이 네 장과 같은 제품군으로 보이지 않으면 실패로 본다.

### 방향 3. 선택지 화면을 최우선으로 다시 만든다

`ShowVnChoiceDeck`이 가장 먼저 고쳐져야 한다. 이 화면이 게임성을 결정한다.

선택지 row 구조:

```text
[상담자 실제 발화 1-2줄]
[의도 / 초점 / 잠재 위험 1줄]
```

선택 전에는 점수, 정답 여부, 이론명을 숨긴다. 선택 후 슈퍼비전과 리포트에서 이론적 의미를 해석한다.

선택 후 즉시 보여줄 피드백:

- 가족 구성원 대사 변화
- 표정 변화
- 침묵 또는 시선 회피
- 방어 상승 또는 완화
- 신뢰 / 안전 / 통찰 변화

### 방향 4. 사례 파일을 플레이 준비 단계로 바꾼다

사례 파일은 긴 설명문이 아니라 플레이어가 가설을 세우는 화면이어야 한다.

도입할 태그:

- IP 지정
- 순환 패턴
- 경계 혼란
- 삼각관계
- 세대 전이
- 시도된 해결책
- 위험 신호
- 보호 요인
- 예외 상황
- 강점
- 비밀 / 윤리 쟁점

태깅은 정답 맞히기가 아니라 가설 설정이다. 하나의 단서에 여러 태그가 가능해야 한다.

### 방향 5. 슈퍼비전을 회고 장면으로 바꾼다

`ShowSupervision`은 현재 점수 중심 구조에서 벗어나야 한다.

새 구조:

```text
회기 요약
-> 결정적 장면 2-3개
-> 플레이어 발화
-> 가족 반응
-> 슈퍼바이저 해석
-> 다른 가능성
-> 다음 회기 질문
```

점수는 보조 정보다. 화면 첫인상은 채점표가 아니라 수련 기록이어야 한다.

## 4. 구현 순서

### Sprint A. UI 토대 재구축

목표: `Eliza` 목업을 Unity 컴포넌트로 옮길 토대를 만든다.

작업:

- `UiTheme` 생성: 색상, 폰트 크기, spacing, 패널 투명도, 버튼 상태를 한곳에서 관리.
- `CreateText` 정책 수정: 본문 best-fit 의존 축소, 최소 글자 크기 정책 적용.
- `CreateVnHud` 재작성: 얇은 상단 HUD, 작은 상태 라벨, 과도한 정보 제거.
- `DialogueBox` 컴포넌트화: speaker, page, content, continue button 분리.
- `InterventionChoiceRow` 컴포넌트 추가: 발화, 의도, hover/focus/selected 상태.

완료 기준:

- 1024x768에서 본문이 자동 축소로 뭉개지지 않는다.
- 새 버튼/패널이 기본 Unity 네모처럼 보이지 않는다.
- 선택지 row가 최소 48px 이상이며 hover/focus/selected 상태가 구분된다.

### Sprint B. FT-001 사례 파일 재구성

목표: `BeginCaseIntake`를 `04_case_file_supervisor_briefing` 기준으로 다시 만든다.

작업:

- 사례 요약을 2-4줄 블록으로 분리.
- 가족 구성, IP, 순환, 경계, 삼각관계 단서를 별도 영역으로 분리.
- 슈퍼바이저 브리핑을 화면 핵심 정보로 배치.
- `회기 입장`을 유일한 1차 CTA로 만든다.
- 태깅 UI의 최소 버전 추가.

완료 기준:

- 사용자가 5초 안에 사례 핵심과 다음 행동을 파악할 수 있다.
- `회기 입장`보다 강한 버튼이 없다.
- 1024x768에서 tiny text 비율 20% 이하.

### Sprint C. FT-001 상담 선택지 전면 재작성

목표: `ShowVnChoiceDeck`, `ShowVnReaction`, `ApplyVnChoice`를 실제 상담 장면처럼 만든다.

작업:

- 선택지 문구를 모두 실제 상담자 발화로 교체.
- 각 선택지에 내부 태그 부여:
  - 기술
  - 대상
  - 이론 렌즈
  - 효과
  - 위험
- 명백한 함정 선택지 제거.
- 선택 직후 가족 반응 대사와 표정 변화를 연결.
- 수치 변화는 반응 뒤 보조 정보로 표시.

완료 기준:

- 선택지 3개 이상이 모두 실제 초보 상담자가 할 법하다.
- 선택 후 1턴 안에 대사, 표정/침묵, 신뢰/안전/통찰 변화 중 최소 2개가 보인다.
- 선택지 화면을 처음 본 사람이 "상담자로서 개입을 고르는 중"이라고 이해할 수 있다.

### Sprint D. 회기 대화 장면 재연출

목표: `ShowVnSessionTurn`과 `CreateVnDialogueBox`를 `06_session_dialogue` 기준으로 정리한다.

작업:

- 캐릭터 위치, 스케일, 발 위치, 조명, 그림자 규칙 고정.
- 활성/비활성 캐릭터 처리 규칙 적용.
- 배경 블러와 패널 대비 조정.
- 대사창은 캐릭터를 과도하게 가리지 않게 배치.
- 페이지 진행 버튼은 명확하되 장면 몰입을 해치지 않게 축소.

완료 기준:

- 캐릭터가 배경 위 PNG처럼 보이지 않는다.
- 대사창이 주요 캐릭터 얼굴과 몸통을 과도하게 가리지 않는다.
- 1024x768, 1600x900, 1920x1080에서 UI 겹침이 없다.

### Sprint E. 슈퍼비전 리포트 재작성

목표: `ShowSupervision`을 `09_supervision_report` 기준으로 다시 만든다.

작업:

- 점수 중심 레이아웃 제거.
- 핵심 회고 3개를 첫 화면에 배치.
- 각 회고 항목을 `내 발화 -> 가족 반응 -> 슈퍼바이저 해석` 묶음으로 구성.
- 다음 회기 목표를 명확히 표시.
- 세부 점수는 접힌 정보 또는 하단 보조 정보로 이동.

완료 기준:

- 첫 화면에서 총점보다 회고 내용이 먼저 보인다.
- 사용자가 다음 회기 목표를 1개 이상 확인할 수 있다.
- tiny text 비율 20% 이하.

## 5. 코드 작업 앵커

우선 수정할 함수 순서:

1. `CreateText`
2. `CreateVnHud`
3. `CreateVnDialogueBox`
4. `ShowVnChoiceDeck`
5. `ShowVnReaction`
6. `ApplyVnChoice`
7. `BeginCaseIntake`
8. `ShowVnSessionTurn`
9. `ShowSupervision`
10. `ShowDashboard`

분리 방향:

- 화면 함수는 화면 전환과 데이터 전달만 담당한다.
- 레이아웃은 View/Panel/Row 컴포넌트가 담당한다.
- 선택지 데이터는 코드 안 문자열 나열에서 `VnScriptRepository` 또는 ScriptableObject/JSON로 이동한다.
- 시각 기준은 `UiTheme`에 모은다.

## 6. 품질 게이트

자동 감사:

- 1024x768, 1366x768, 1600x900, 1920x1080에서 검사한다.
- 모든 해상도에서 `textOverflowCount=0`.
- 모든 해상도에서 `offscreenRectCount=0`.
- 전체 tiny text 비율 25% 이하.
- 핵심 화면 tiny text 비율 20% 이하.

핵심 화면:

- `04_ft001_intake`
- `06_session_dialogue`
- `07_ft001_choice_deck`
- `13_supervision_report`

수동 확인:

- 실제 Unity 캡처가 `Eliza` 목업 04/06/07/09와 같은 제품군으로 보인다.
- 선택지가 시험 문제가 아니라 상담자 발화로 보인다.
- 캐릭터가 배경 안에 존재하는 느낌이다.
- 대사창과 패널이 캐릭터 얼굴, 핵심 행동, 버튼을 가리지 않는다.
- 첫 플레이어 행동이 화면에서 즉시 보인다.

## 7. 지금부터 금지할 방향

- 새 UI 스타일을 임의로 추가하지 않는다.
- `Coffee Talk`류 카페 대화 UI를 1차 기준으로 삼지 않는다.
- 큰 네모 박스와 기본 버튼으로 기능만 배치하지 않는다.
- 점수, 대시보드, 분석표를 첫인상으로 내세우지 않는다.
- 선택지를 이론명/정답명으로 쓰지 않는다.
- 명백한 오답 선택지를 넣지 않는다.
- best-fit으로 글자를 계속 줄여서 감사만 통과시키지 않는다.
- 전체 사례를 동시에 고치지 않는다.

## 8. 다음 즉시 작업

가장 먼저 할 작업은 `ShowVnChoiceDeck` 재제작이다.

이유:

- 선택지 화면이 게임성을 결정한다.
- 현재 문제가 가장 직접적으로 드러나는 화면이다.
- UI, 텍스트, 가족치료 설계, 반응 시스템을 한 번에 검증할 수 있다.

작업 단위:

1. `InterventionChoiceRow` 생성.
2. FT-001 선택지 문구를 실제 상담자 발화로 교체.
3. 선택 후 가족 반응 대사/표정/침묵을 연결.
4. 1024x768 visual audit 실행.
5. 결과가 통과하면 `BeginCaseIntake`와 `ShowSupervision`으로 확장.

최종 판단 기준:

> 이 프로젝트는 "가족치료 내용을 설명하는 앱"이 아니라 "가족치료적 사고를 플레이하는 상용 비주얼노벨형 수련 게임"으로 만들어야 한다. 모든 UI, 선택지, 캐릭터 연출, 슈퍼비전은 이 기준에 맞지 않으면 다시 고친다.
