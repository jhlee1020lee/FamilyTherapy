# FT-001 Dialogue-By-Dialogue CG Image Window Command

이 문서를 이미지 생성 전용 창에 그대로 입력한다.

## 이번 작업 범위

FT-001만 만든다. 다른 사례 FT-002~FT-010은 이번 작업에서 건드리지 않는다.

이번 방식은 요약 CG가 아니라 **대사/반응별 1장씩**이다. 실제 게임에서 컷 전환이 자연스럽게 보이도록, 한 줄의 대사 또는 선택 후 반응마다 대응하는 16:9 CG를 만든다.

## 필요한 이미지 수

최종 목표는 총 50장이다.

```text
본대사 CG: 30장
선택 후 반응 CG: 15장
선택지 대기 CG: 5장
총 50장
```

선택지 대기 CG 5장은 대사는 아니지만 실제 게임에서 선택지가 뜰 때 배경으로 쓰인다. 이것이 없으면 선택지 화면에서 갑자기 이미지가 끊기므로 같이 만든다.

## 반드시 따를 원본 Shotlist

아래 문서를 유일한 shotlist로 사용한다.

```text
Docs/FT001_LINE_BY_LINE_LOCKED_CG_COMMAND_2026-06-10.md
```

해당 문서의 `Shotlist`에 있는 파일명 50개를 그대로 만든다. 파일명은 절대 바꾸지 않는다.

## 출력 폴더

최종 게임 적용용 폴더:

```text
Assets/Resources/VN/EventCG/FT001_LineByLineLocked/
```

후보, 원본, contact sheet, 실패작 백업 폴더:

```text
Docs/GeneratedSources/FT001_LineByLineLocked_20260610/
```

## 절대 조건

- 모든 이미지는 `1600x900` PNG.
- 정사각형 생성 후 늘리기 금지.
- 16:9가 아닌 이미지를 강제 리사이즈해서 제출 금지.
- 이미지 안에 텍스트, 자막, UI, 말풍선, 워터마크 금지.
- 하단 25-30%는 대사창이 올라가도 핵심 얼굴이 가려지지 않게 비워둔다.
- 가족 4명 컷은 모두 같은 카메라, 같은 방, 같은 의자 배치, 같은 인물 위치를 유지한다.
- 김혜성 컷은 별도의 김혜성 단독/중심 고정 구도를 유지한다.

## 인물 고정

| ID | 이름 | 고정 |
| --- | --- | --- |
| `ft001_mother` | 박성빈 | 한국인 성인 여성, 어머니, 야간 근무로 지쳐 있음 |
| `ft001_child` | 이주형 | 한국인 남자 초등학생, 불안하고 위축됨, 청소년 아님 |
| `ft001_grandmother` | 오선진 | 한국인 노년 여성, 외조모, 걱정이 많고 단단한 말투 |
| `ft001_teacher` | 서건창 | 한국인 성인 남성 담임교사, 가족 구성원 아님 |
| `supervisor_system` | 김혜성 | 한국인 성인 여성 슈퍼바이저, 남성 아님 |

중요: 김혜성은 여성이다. 김혜성은 서건창과 다른 인물이다. 김혜성은 가족 4명과 같은 줄에 앉지 않는다.

## 구도 고정

### 가족 컷

가족 컷은 김혜성/플레이어가 앉은 자리에서 가족 4명을 바라보는 구도다.

```text
왼쪽: 오선진
중앙 왼쪽: 박성빈
중앙 오른쪽/앞: 이주형
오른쪽: 서건창
전경: 낮은 상담 테이블
배경: 같은 상담실, 같은 창문, 같은 문, 같은 책장, 같은 의자
```

대사별로 바꿀 수 있는 것은 표정, 시선, 작은 손짓뿐이다. 카메라 위치, 인물 크기, 방 구조, 테이블 높이, 크롭은 바꾸지 않는다.

가족 컷 공통 프롬프트:

```text
Use the exact same locked Family Master Shot composition. Same 1600x900 frame, same room, same lens, same crop, same foreground table height, same chair positions, same character positions and scale. Do not zoom, pan, reframe, reseat, recrop, or change the furniture. Only change facial expressions, gaze, and small hand gestures according to the specified line. Polished commercial Korean visual novel CG, realistic painterly 2D, restrained emotional acting, warm counseling-room lighting, no UI, no text, no subtitle, no watermark.
```

### 김혜성 컷

김혜성이 말할 때는 김혜성 단독 또는 김혜성 중심 구도다. 가족 4명을 같은 프레임에 억지로 넣지 않는다.

김혜성 컷 공통 프롬프트:

```text
Use the exact same locked Hyesung Master Shot composition. Kim Hyesung is a Korean adult female family-systems supervisor sitting across from the family, shown alone or centered. Same 1600x900 frame, same room, same lens, same crop, same chair position, same clothing, same face. Do not turn her into a male therapist, teacher, or family member. Only change facial expression and small hand gestures according to the specified line. Polished commercial Korean visual novel CG, realistic painterly 2D, restrained emotional acting, warm counseling-room lighting, no UI, no text, no subtitle, no watermark.
```

## 레퍼런스

가족 4명과 상담실:

```text
Docs/GeneratedSources/FT001_ReferenceLocked_20260609/references/ft001_ref_group_seating_master_1600x900.png
Docs/GeneratedSources/FT001_ReferenceLocked_20260609/references/ft001_ref_counseling_room_empty_1600x900.png
Docs/GeneratedSources/FT001_ReferenceLocked_20260609/references/ft001_ref_cast_identity_sheet_1600x900.png
```

김혜성 여성 슈퍼바이저:

```text
Docs/GeneratedSources/FT001_KH_FemaleRef_20260610/ref/kh_f_identity_1600x900.png
Docs/GeneratedSources/FT001_KH_FemaleRef_20260610/ref/kh_f_speaking_1600x900.png
Docs/GeneratedSources/FT001_KH_FemaleRef_20260610/ref/kh_f_opposite_seating_1600x900.png
Docs/GeneratedSources/FT001_KH_FemaleRef_20260610/kh_f_reference_contact_sheet_20260610.png
```

구버전 남성 김혜성 레퍼런스는 사용하지 않는다.

## 진행 순서

1. 먼저 `Family Master Shot` 1장을 확정한다.
2. 그 구도를 기준으로 Turn 1 가족 컷 9장을 만든다.
3. `Hyesung Master Shot` 1장을 확정한다.
4. 김혜성 컷은 이 구도에서만 변형한다.
5. 10장 단위로 contact sheet를 만든다.
6. 구도가 흔들린 이미지는 최종 폴더에 넣지 말고 다시 만든다.
7. 최종 폴더에는 검수 통과한 1600x900 PNG 50장만 넣는다.

## 첫 배치 지시

우선 Turn 1의 10장을 만든다.

```text
ft001_cg_t01_l01_mother_neutral.png
ft001_cg_t01_l02_child_anxious.png
ft001_cg_t01_l03_mother_worried.png
ft001_cg_t01_l04_child_quiet.png
ft001_cg_t01_l05_teacher_concerned.png
ft001_cg_t01_l06_supervisor_explaining.png
ft001_cg_t01_choice_idle.png
ft001_cg_t01_reaction_a_mother_softened.png
ft001_cg_t01_reaction_b_child_withdrawn.png
ft001_cg_t01_reaction_c_teacher_procedural.png
```

각 파일의 대사, 화자, 표정 지시는 반드시 아래 문서의 Turn 1 항목을 따른다.

```text
Docs/FT001_LINE_BY_LINE_LOCKED_CG_COMMAND_2026-06-10.md
```

## 완료 보고 형식

첫 배치가 끝나면 아래 형식으로 보고한다.

```text
완료 배치:
완료 폴더:
생성 파일 수:
누락 파일:
contact sheet 경로:
구도 흔들림 의심 파일:
김혜성 성별/정체성 확인:
1600x900 확인:
```
