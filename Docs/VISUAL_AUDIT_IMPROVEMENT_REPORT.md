# 비주얼 감사 및 개선 보고서

- 작성일: 2026-06-08
- 대상 빌드: `Builds/Windows/FamilyTherapyPracticum.exe`
- 코드 기준: `Assets/Scripts/FamilyTherapyPracticumGame.cs`
- 결론: 기능은 작동하지만, 현재 화면 품질은 상용 비주얼노벨이 아니라 이미지 스킨을 얹은 교육용 프로토타입에 가깝다.

## 1. 실제 실행 검토 범위

이번 검토는 `batchmode` smoke가 아니라 실제 그래픽 실행본을 여러 해상도로 띄워 캡처했다. 캡처 자동화를 위해 `-familyTherapyVisualAudit` 실행 모드를 추가했고, 각 해상도에서 13개 화면을 순서대로 렌더링했다.

실행 해상도:

| 해상도 | 화면 수 | 감사 결과 |
|---|---:|---|
| 1920x1080 | 13장 | 완료 |
| 1366x768 | 13장 | 완료 |
| 1280x720 | 13장 | 완료 |
| 1024x768 | 13장 | 완료 |

검토한 화면:

1. 메인 메뉴
2. 케이스 브라우저 1페이지
3. 케이스 브라우저 6페이지
4. FT-001 사례 접수
5. FT-001 대화 장면
6. FT-001 슈퍼바이저 라인
7. FT-001 선택지 화면
8. FT-001 가족 반응
9. FT-012 CoreCases 대화 장면
10. FT-012 CoreCases 선택지 화면
11. 저장/불러오기
12. 대시보드
13. 슈퍼비전 리포트

감사 로그:

```text
Logs/visual_audit_build.log
Logs/visual_audit_1920x1080.log
Logs/visual_audit_1366x768.log
Logs/visual_audit_1280x720.log
Logs/visual_audit_1024x768.log
```

캡처 폴더:

```text
C:\Users\이종호\AppData\LocalLow\Family Therapy Practicum\Family Therapy Practicum\FamilyTherapyPracticumExports\visual_audit_1920x1080
C:\Users\이종호\AppData\LocalLow\Family Therapy Practicum\Family Therapy Practicum\FamilyTherapyPracticumExports\visual_audit_1366x768
C:\Users\이종호\AppData\LocalLow\Family Therapy Practicum\Family Therapy Practicum\FamilyTherapyPracticumExports\visual_audit_1280x720
C:\Users\이종호\AppData\LocalLow\Family Therapy Practicum\Family Therapy Practicum\FamilyTherapyPracticumExports\visual_audit_1024x768
```

대표 캡처:

```text
visual_audit_1920x1080/01_main_menu.png
visual_audit_1920x1080/05_ft001_dialogue.png
visual_audit_1024x768/04_ft001_intake.png
visual_audit_1024x768/07_ft001_choice_deck.png
visual_audit_1024x768/12_dashboard.png
visual_audit_1280x720/13_supervision_report.png
```

## 2. 정량 결과

`textOverflowCount`는 텍스트의 선호 높이/너비가 실제 RectTransform보다 큰 경우를 잡은 값이다. `tinyTextCount`는 CanvasScaler 적용 후 글자가 너무 작아지는 경우를 잡은 값이다.

| 해상도 | 총 텍스트 overflow | 작은 글씨 판정 | 최악 화면 | 최악 화면 overflow |
|---|---:|---:|---|---:|
| 1920x1080 | 47 | 0 | `12_dashboard` | 14 |
| 1366x768 | 45 | 59 | `12_dashboard` | 13 |
| 1280x720 | 45 | 64 | `12_dashboard` | 13 |
| 1024x768 | 42 | 205 | `12_dashboard` | 12 |

중요한 해석:

- 1920x1080에서도 overflow가 47건이다. 작은 해상도만 문제가 아니다.
- 1024x768에서는 작은 글씨 판정이 205건이다. 전체 UI가 읽을 수는 있어도 상용 품질의 판독성은 아니다.
- 가장 많이 무너지는 화면은 대시보드, 사례 접수, 저장/불러오기, 슈퍼비전 리포트다.
- VN 대화/선택 화면은 자동 수치상 overflow가 적게 잡히지만, 실제 캡처에서는 이미지 스킨과 캐릭터 합성이 심하게 깨진다.

## 3. 자산 통계

현재 `Assets/Resources/VN` PNG는 762장이다.

| 분류 | 개수 | 주요 해상도 | 비율 | 포맷 |
|---|---:|---|---:|---|
| Characters | 532 | 1024x1536 | 0.6667 | RGB |
| Characters | 40 | 1672x941 | 1.7768 | RGB |
| Backgrounds | 40 | 1672x941 | 1.7768 | RGB |
| EventCG | 50 | 1672x941 | 1.7768 | RGB |
| UI | 100 | 180x180, 190x190, 420x280, 520x300, 560x150, 1600x320 등 | 혼재 | RGBA |

자산 쪽 핵심 문제:

- 캐릭터 PNG가 투명 스탠딩 스프라이트가 아니다. 대부분 알파 없는 RGB라 배경이 포함된 사각 이미지로 렌더링된다.
- FT-001 계열은 와이드 캐릭터와 세로 캐릭터가 섞여 있다. 같은 캐릭터 슬롯에 들어가면 크기와 비율이 통일되지 않는다.
- 배경과 EventCG는 16:9에 가깝게 통일되어 있어 상대적으로 안정적이다.
- UI 이미지는 비율이 다양하지만 코드에서는 `RawImage`로 패널 크기에 맞춰 늘린다. 그래서 장식선, 테두리, 질감이 텍스트 영역을 침범하거나 비율이 깨진다.

## 4. 직접 확인한 주요 문제

### P0-1. 캐릭터가 VN 스프라이트가 아니라 사각 이미지 카드처럼 보임

1920x1080 FT-001 대화 화면에서도 캐릭터 뒤에 흰색/베이지색 사각 배경이 크게 보인다. 상담실 배경 위에 캐릭터가 서 있는 것이 아니라, 배경 위에 인물 이미지 박스를 얹은 느낌이다.

원인:

- 캐릭터 PNG가 알파 없는 RGB다.
- `CreateVnStage()`에서 캐릭터를 `RawImage`로 holder 전체에 stretch한다.
- 비활성 캐릭터는 알파를 낮춰서 더 큰 반투명 사각판처럼 보인다.

개선:

- 캐릭터는 반드시 투명 배경 PNG로 다시 받아야 한다.
- 기준 규격을 `1024x1536`, 전신 또는 반신, transparent background, 발 위치/pivot 통일로 고정한다.
- Unity 쪽에서는 `RawImage` stretch를 중단하고 `AspectRatioFitter` 또는 sprite용 rect 계산을 적용한다.
- 비활성 화자는 사각 투명도 처리 대신 밝기/채도/scale만 낮춘다.

### P0-2. 대화창/선택지 UI 스킨이 텍스트를 가림

대화창과 선택지 카드 안에 장식선이 들어가 있는데, 이 선들이 실제 텍스트와 같은 영역을 지나간다. 1920x1080에서도 대화문을 장식선이 관통하고, 1024x768 선택지 화면에서는 선택지 문장이 거의 읽기 어렵다.

원인:

- UI 이미지를 9-slice sprite가 아니라 `RawImage`로 늘린다.
- UI 이미지 자체가 텍스트 안전 영역을 고려하지 않고 장식선이 중앙에 있다.
- 버튼 높이가 62px로 고정이라 긴 선택지가 줄바꿈되면 텍스트와 장식이 겹친다.

개선:

- `dialogue_box`, `choice_card_*`, `supervisor_note_panel`, `case_file_panel`은 텍스트 영역이 비어 있는 이미지로 재제작한다.
- 장식은 가장자리/상단 라벨 영역에만 배치한다.
- UI 스킨은 `Sprite` + 9-slice로 가져가고, 패널 비율에 따라 늘어도 중앙 텍스트 영역이 깨지지 않게 한다.
- 선택지 버튼은 고정 높이가 아니라 텍스트 preferred height 기반으로 높이를 계산한다.

### P0-3. 1920x1080 기준에서도 메인 화면이 상용 게임처럼 보이지 않음

메인 화면은 배경 이미지는 좋지만, UI가 다음 이유로 개발 중 화면처럼 보인다.

- 하단에 `C:/Users/...` 로컬 저장 경로가 그대로 노출된다.
- 진행률 패널 안에서 숫자와 바가 겹친다.
- 버튼 스킨의 장식선과 텍스트가 충돌한다.
- 오른쪽 Dossier 패널은 내용보다 장식 이미지가 크게 보여 정보 계층이 약하다.
- `START CAMPAIGN`, `CASE FILES`, `CONTINUE` 등은 기능 버튼처럼 보이고, 첫 실행 서사가 없다.

개선:

- 메인 화면에서 로컬 경로 노출 제거.
- 저장 상태는 `Slot 1 저장됨`, `마지막 사례 FT-001` 정도로 축약.
- 진행률 패널은 숫자와 바를 분리한 전용 HUD 이미지로 재작업.
- Dossier 패널은 실제 사건 파일 카드처럼 제목, 주호소, 위험도, 추천 렌즈, 시작 버튼을 명확히 분리.
- 첫 실행 시 윤리 고지/오프닝/센터 로비/첫 사례 알림을 거쳐야 한다.

### P0-4. 선택지 화면이 캐릭터와 충돌함

FT-001 선택지 화면에서 오른쪽 선택 패널이 인물 위에 올라간다. 캐릭터 3명이 동시에 보이는 구조에서는 선택 패널과 인물이 서로 공간을 빼앗는다.

원인:

- `ShowVnChoiceDeck()`의 선택 패널이 `x 0.58~0.96`, `y 0.18~0.84`로 고정되어 있다.
- 캐릭터 stage는 `y 0.14~0.88`을 사용한다.
- 캐릭터 안전 영역과 UI 안전 영역이 분리되어 있지 않다.

개선:

- 선택지는 하단 drawer 또는 전용 선택 화면으로 분리한다.
- 선택지 표시 중에는 캐릭터를 좌측 1~2명으로 축소하거나, 캐릭터 stage를 뒤로 흐리게 처리한다.
- safe area를 HUD/노트/대화창/선택지 영역 기준으로 계산한다.

### P0-5. 선택지 색이 정답을 미리 노출함

현재 선택지는 품질 점수에 따라 초록/노랑/빨강으로 표시된다. 사용자가 누르기 전부터 어떤 선택이 좋은지 알 수 있어 상담 시뮬레이션의 판단 경험이 사라진다.

개선:

- 선택 전 카드는 모두 중립 색상으로 표시.
- 선택 후 가족 반응, 지표 변화, 슈퍼바이저 피드백에서 품질을 드러낸다.
- 선택지는 “정답 찾기”가 아니라 “치료적 판단”처럼 보이게 해야 한다.

### P1-1. 화면 대부분이 1920x1080 고정 설계임

케이스 브라우저는 list 1180 + detail 620 + padding/spacing 구조라 1920 폭에 거의 딱 맞는다. 1366 이하에서는 압축되거나 글자가 작아진다.

개선:

- 1600px 이하에서는 2열을 1열+스크롤로 전환.
- 1024x768 같은 4:3 화면에서는 메인 메뉴, 브라우저, 접수, 대시보드 모두 compact layout 사용.
- `CanvasScaler.screenMatchMode`와 `matchWidthOrHeight`를 명시하고, 화면별 breakpoint를 둔다.

### P1-2. 텍스트 overflow 정책이 약함

`CreateText()`가 `verticalOverflow = Overflow`를 사용한다. 긴 문장은 부모 밖으로 흘러나갈 수 있다. 자동 감사에서 대시보드와 접수 화면 overflow가 특히 많았다.

개선:

- TextMeshPro로 전환한다.
- 본문은 max height + ScrollRect 또는 ellipsis를 쓴다.
- 긴 Windows 경로, 사례 설명, 선택 해설은 한 화면에 전부 넣지 않는다.

### P1-3. 결과/대시보드가 게임 화면이 아니라 관리 도구처럼 보임

슈퍼비전 리포트와 대시보드는 기능적으로는 유용하지만, 상용 VN의 보상/완료감이 약하다. 현재는 점수표, CSV export, 분석 바가 전면에 나와서 수업용 대시보드에 가깝다.

개선:

- 회기 종료 직후에는 슈퍼바이저 컷신을 먼저 보여준다.
- 이후 선택 경로 요약, 놓친 역동, 다음 수련 목표, 자동 저장 완료를 순서대로 보여준다.
- 대시보드/CSV export는 게임 내 기록실 또는 수련 파일 메뉴로 격리한다.

### P1-4. 첫 10분 플로우가 상용 VN의 도입부가 아님

`START CAMPAIGN`이 곧바로 FT-001 회기로 들어간다. 사례 파일 읽기, 이론 렌즈 선택, 슈퍼바이저 등장, 첫 출근 같은 도입이 없다.

권장 플로우:

```text
첫 실행 윤리 고지
-> 타이틀
-> 수련생 첫 출근/센터 로비
-> FT-001 접수 알림
-> 사례 파일 확인
-> 이론 렌즈 선택
-> 슈퍼바이저 한 줄 브리핑
-> VN 회기 시작
-> 가족 반응
-> 슈퍼비전 컷신
-> 저장/다음 사례
```

### P2-1. 저장/불러오기가 상용 VN 기준으로 부족함

현재 저장 슬롯은 작동하지만, 상용 VN처럼 보이려면 다음이 필요하다.

- 저장 썸네일
- 사례 ID/챕터/회기 턴/마지막 대사
- 덮어쓰기 확인 모달
- 삭제 버튼
- 세션 중간 저장
- `CONTINUE` 버튼의 비활성/활성 상태 구분

### P2-2. VN 기본 편의 기능이 없음

상용 VN로 보이려면 다음이 기본적으로 있어야 한다.

- 대사 backlog
- Auto
- Skip
- 대사창 숨기기
- 텍스트 속도
- BGM/SFX 볼륨
- 옵션 화면
- 이전 선택 확인
- 버튼 hover/click SFX

## 5. 서브에이전트 검토 요약

### 레이아웃 코드 리뷰어

핵심 지적:

- `CanvasScaler`가 1920x1080 기준이고 반응형 분기가 없다.
- `CreateText()`의 세로 overflow가 켜져 있어 텍스트가 부모 밖으로 흐를 수 있다.
- 버튼 높이가 56/62px로 고정되어 긴 선택지와 저장 슬롯에서 잘릴 수 있다.
- 케이스 브라우저 2열 구조가 1920폭에 거의 맞춰져 있어 1600 이하에서 위험하다.
- VN 배경/캐릭터/스킨이 모두 `RawImage` stretch라 비율이 깨진다.

### 이미지 자산 QA 리뷰어

핵심 지적:

- 캐릭터 PNG 572장 중 532장이 1024x1536 RGB, 40장이 1672x941 RGB다.
- 캐릭터는 알파 없는 사각 이미지라 스탠딩 스프라이트로 쓸 수 없다.
- Backgrounds/EventCG는 1672x941로 통일되어 상대적으로 안정적이다.
- UI는 RGBA지만 크기/비율이 혼재되어 있고, 코드가 RawImage로 늘려서 왜곡된다.

### 게임 플로우/상용 VN UX 리뷰어

핵심 지적:

- 기능은 작동하지만 첫 10분 사용자 여정은 상용 VN이 아니라 교육 툴에 가깝다.
- `START CAMPAIGN`이 사례 파일/렌즈 선택/브리핑 없이 바로 회기로 들어간다.
- 선택지 색이 정답을 미리 노출한다.
- 결과 화면은 게임 보상보다 관리 리포트에 가깝다.
- VN 편의 기능과 저장 UX가 부족하다.

## 6. 권장 개선 순서

### 1단계: 바로 눈에 보이는 붕괴 제거

목표: “네모 박스와 해상도 난리”를 먼저 줄인다.

작업:

1. 캐릭터 PNG를 투명 스프라이트로 다시 받기.
2. 캐릭터 `RawImage`에 비율 유지 적용.
3. `dialogue_box`, `choice_card_*`, `supervisor_note_panel`에서 텍스트를 가리는 장식 제거.
4. 하단 로컬 경로 노출 제거.
5. 선택지 색상 정답 노출 제거.
6. 대화창/선택지/노트의 텍스트 영역을 단색 또는 약한 질감으로 정리.

완료 기준:

- 1920x1080 VN 대화 화면에서 캐릭터 사각 배경이 보이지 않는다.
- 대화문을 가로지르는 장식선이 없다.
- 선택지 텍스트가 모든 카드에서 읽힌다.
- 메인 화면에 `C:/Users/...` 경로가 보이지 않는다.

### 2단계: 반응형 레이아웃

목표: 1280x720과 1024x768에서 UI가 압축되지 않게 한다.

작업:

1. `CanvasScaler` 설정 명시.
2. 화면 폭/비율별 layout mode 추가.
3. 케이스 브라우저, 접수, 저장, 대시보드, 결과 화면에 ScrollRect 도입.
4. 고정 폭 2열 화면을 compact 1열 화면으로 전환.
5. metric row의 최소 높이 보장.

완료 기준:

- 1280x720 기준 `textOverflowCount=0`.
- 1024x768 기준 주요 화면에서 `tinyTextCount`를 현재 205건에서 20건 이하로 줄인다.
- 대시보드와 슈퍼비전 리포트는 스크롤로 끝까지 읽을 수 있다.

### 3단계: 첫 10분 캠페인 플로우 재설계

목표: 사용자가 처음 눌렀을 때 상용 VN처럼 들어가게 한다.

작업:

1. 첫 실행 윤리 고지 연결.
2. `START CAMPAIGN`을 바로 회기 시작이 아니라 사례 파일로 연결.
3. 이론 렌즈 선택 후 슈퍼바이저 브리핑.
4. FT-001 오프닝 대사 추가.
5. 회기 종료를 슈퍼비전 컷신으로 마무리.

완료 기준:

- 첫 플레이 10분이 `메뉴 -> 사례 파일 -> 렌즈 선택 -> 슈퍼바이저 -> 회기 -> 결과`로 자연스럽게 흐른다.
- 사용자가 왜 이 가족을 만나고, 어떤 판단을 하는지 화면상 이해된다.

### 4단계: 상용 VN 기능 보강

목표: 클릭형 교육 앱에서 VN 엔진으로 보이게 한다.

작업:

1. Backlog.
2. Auto/Skip.
3. 대화창 숨기기.
4. 옵션 화면.
5. 저장 썸네일.
6. 버튼음/BGM/SFX.
7. 선택 후 지표 변화 애니메이션.

완료 기준:

- VN 기본 조작이 가능하다.
- 저장/불러오기가 상용 VN 슬롯처럼 보인다.
- 결과 화면이 단순 점수표가 아니라 회기 종료 연출로 보인다.

## 7. 이미지 생성 창에 다시 요구할 자산 규칙

캐릭터:

```text
transparent background
full body or consistent half body
1024x1536
same camera distance per character
same floor/pivot position
expression must change clearly in eyes, mouth, brow, posture
no beige card background
no room background inside character PNG
```

UI:

```text
dialogue box with empty text-safe center
decorations only on border/nameplate area
choice card with no horizontal lines across text
9-slice friendly border
transparent margins
do not bake text into UI images
```

배경:

```text
1920x1080 or 2560x1440
no dark blur
actual readable counseling room/supervision room/center lobby
consistent lighting between backgrounds
```

표정:

```text
neutral, anxious, defensive, exhausted, softened, worried, tearful, listening,
withdrawn, scared, quiet, relieved, hesitant, critical, stubborn, concerned,
procedural, explaining, questioning, warning, approving, reflective, supportive
```

현재처럼 파일명만 다르고 얼굴이 같으면 게임에서는 표정 변화가 없는 것으로 보인다. 표정 차이는 눈썹, 눈, 입, 어깨, 시선 방향까지 바뀌어야 한다.

## 8. 다음 구현 티켓

### T-001 캐릭터 합성 방식 교체

- `CreateVnStage()`에서 캐릭터 `RawImage` stretch 제거.
- 비율 유지.
- 투명 스프라이트 기준 배치.
- 비활성 화자 처리를 사각 알파가 아니라 밝기/채도/scale로 변경.

### T-002 UI 스킨 적용 방식 교체

- `CreateAbsoluteSkinnedPanel()`과 `CreateSkinnedButton()`에서 RawImage 단순 stretch 제거.
- 9-slice Sprite 또는 단색 패널 + 얇은 border로 변경.
- 텍스트 safe area 보장.

### T-003 VN 선택지 화면 재배치

- 선택 패널이 캐릭터를 덮지 않게 하단 drawer 또는 별도 선택 레이어로 이동.
- 선택지 카드 높이를 텍스트 길이에 따라 계산.
- 선택지 색상 정답 노출 제거.

### T-004 메인 화면 정리

- 로컬 저장 경로 제거.
- 진행률 패널 재설계.
- Dossier 패널 정보 계층 정리.
- 버튼은 아이콘/라벨/상태가 충돌하지 않게 별도 UI 자산 사용.

### T-005 ScrollRect와 compact layout

- 케이스 브라우저.
- 사례 접수.
- 저장/불러오기.
- 대시보드.
- 슈퍼비전 리포트.

### T-006 첫 플레이 플로우 연결

- 윤리 고지.
- 센터 로비.
- 사례 파일.
- 렌즈 선택.
- 슈퍼바이저 브리핑.
- 회기 시작.

## 9. 현재 상태 판단

현재 빌드는 기능 기준으로는 많은 것이 연결되어 있다.

- 60개 사례 플레이 가능.
- 762장 PNG 런타임 인식.
- CoreCases FT-011~FT-024 캐릭터 자동 연결.
- 저장/export/smoke 작동.

하지만 상용 비주얼노벨 기준으로는 아직 핵심 품질 문제가 남아 있다.

- 캐릭터가 스프라이트가 아니라 사각 이미지처럼 보인다.
- UI 스킨이 텍스트를 침범한다.
- 1920x1080에서도 대화창과 메인 화면이 깨져 보인다.
- 작은 해상도에서는 글자가 지나치게 작아진다.
- 선택지와 결과 화면이 게임적 연출보다 교육용 관리 화면에 가깝다.

따라서 다음 작업은 이미지 수를 더 늘리는 것이 아니라, 이미 들어온 이미지를 상용 VN 문법에 맞게 다시 적용하는 것이다. 특히 투명 캐릭터, 9-slice UI, 선택지 중립화, 첫 10분 플로우 재설계가 우선이다.
