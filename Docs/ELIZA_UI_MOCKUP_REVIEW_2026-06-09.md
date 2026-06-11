# Eliza UI Mockup Review

- 작성일: 2026-06-09
- 생성 방식: Codex built-in image generation
- 저장 위치: `Docs/GeneratedSources/Eliza_UI_Mockups_2026-06-09`

## 생성된 목업

| 파일 | 용도 | 판정 |
| --- | --- | --- |
| `01_title_main_menu.png` | 타이틀 / 메인 메뉴 | 사용 가능. 어두운 로비와 절제된 메뉴 방향이 맞음. |
| `02_ethics_notice.png` | 윤리 고지 | 사용 가능. 상담/동의서 톤이 적절함. |
| `03_case_lobby.png` | 사례 선택 | 우선 기준 후보. 현재 사례 선택 화면을 이 구조로 바꾸는 것이 좋음. |
| `04_case_file_supervisor_briefing.png` | 사례 파일 / 슈퍼바이저 브리핑 | 우선 기준 후보. 좌측 intake form + 우측 supervisor panel 구조가 가장 적합함. |
| `05_character_intro.png` | 등장인물 소개 | 사용 가능. 프로필 패널 방향이 좋음. |
| `06_session_dialogue.png` | 회기 대화 | 사용 가능. 다만 실제 캐릭터 이미지와 충돌하지 않게 하단 대화창 높이는 Unity에서 조정 필요. |
| `07_intervention_choice.png` | 개입 선택 | 우선 기준 후보. 퀴즈 카드가 아니라 상담자 발화 리스트처럼 보임. |
| `08_save_records.png` | 저장 / 기록실 | 사용 가능. 기존 대시보드보다 낫지만 기록실은 더 단순화 가능. |
| `09_supervision_report.png` | 슈퍼비전 리포트 | 우선 기준 후보. 보고서 + 우측 평가 패널 구조가 적합함. |

## Unity 적용 우선순위

1. `04_case_file_supervisor_briefing.png` 기준으로 사례 파일 화면을 재배치한다.
2. `07_intervention_choice.png` 기준으로 선택지 화면을 얇은 리스트형으로 바꾼다.
3. `06_session_dialogue.png` 기준으로 회기 대화창을 단순한 하단 패널로 바꾼다.
4. `09_supervision_report.png` 기준으로 슈퍼비전 리포트를 보고서형으로 정리한다.
5. `03_case_lobby.png` 기준으로 사례 선택 화면을 리스트형으로 정리한다.

## 적용 원칙

- 목업 이미지를 그대로 배경으로 쓰지 않는다.
- 목업에서 레이아웃, 여백, 색상, 패널 우선순위만 추출한다.
- Unity 텍스트는 런타임 텍스트로 유지한다.
- 버튼 이미지는 두꺼운 장식 스킨이 아니라 얇은 list row 형태로 다시 만든다.
- 작은 해상도 기준 검증은 1024x768부터 한다.

## 현재 결정

- 이전 `Coffee Talk` UI 스킨 방향은 중단한다.
- `Eliza`식 clinical VN UI가 공식 UI 아트 기준이다.
- 기존 `Assets/Resources/VN/UI/*_candidate01.png` 계열은 바로 적용하지 않는다. 화면을 망친 두꺼운 장식 스킨이므로 폐기 후보로 둔다.

