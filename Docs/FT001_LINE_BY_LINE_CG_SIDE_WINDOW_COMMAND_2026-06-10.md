# FT-001 대사별 1CG 생성 명령문

아래 내용을 이미지 생성 전용 창에 그대로 붙여넣고 실행한다.

## 작업 목표

FT-001만 만든다. 이번에는 요약 CG나 장면별 CG가 아니라, **실제 게임에 들어가는 대사 1개당 이미지 1장**을 만든다.

기준은 상용 비주얼노벨식 CG 연출이다. 사용자가 대사를 넘길 때마다 같은 상담실 안에서 인물의 표정, 시선, 손짓, 긴장도가 바뀌어야 한다. 카메라와 좌석 배치는 흔들리면 안 된다.

## 생성 수량

이번 FT-001 1차 완성 목표는 총 67장이다.

| 구분 | 수량 | 설명 |
| --- | ---: | --- |
| 인트로 대사 CG | 5 | 등장인물 소개 |
| 본 회기 대사 CG | 30 | 5턴 x 6대사 |
| 선택 반응 CG | 15 | 5턴 x 3선택지 반응 |
| 이전 선택 carryover CG | 12 | 2~5턴 진입 시 이전 선택에 따라 달라지는 첫 대사 |
| 선택지 대기 CG | 5 | 선택지 화면 배경 |

## 저장 위치

최종 게임 적용용:

```text
Assets/Resources/VN/EventCG/FT001_LineByLineLocked/
```

후보/소스/검토용:

```text
Docs/GeneratedSources/FT001_LineByLineLocked_20260610/
```

## 절대 규격

- 모든 이미지는 네이티브 `1600x900`, 16:9
- 정사각형 생성 후 늘리기 금지
- 16:9가 아닌 이미지를 억지로 크롭/리사이즈해서 제출 금지
- 텍스트, 자막, 말풍선, UI, 워터마크 금지
- 하단 25~30%는 게임 대사창이 올라가므로 시각적으로 단순하게 유지
- 모든 최종 파일명은 shotlist의 이름을 그대로 사용
- `.png`로 저장

## 인물 고정

| ID | 이름 | 성별/역할 | 고정 설정 |
| --- | --- | --- | --- |
| `ft001_mother` | 박성빈 | 여성, 어머니 | 야간 근무와 자녀 등교 거부 사이에서 소진됨 |
| `ft001_child` | 이주형 | 남자 초등학생 | 불안하고 위축됨, 청소년처럼 보이면 실패 |
| `ft001_grandmother` | 오선진 | 여성, 외조모 | 걱정이 많고 단단한 말투 |
| `ft001_teacher` | 서건창 | 남성, 담임교사 | 가족 구성원이 아니라 학교 측 인물 |
| `supervisor_system` | 김혜성 | 여성, 가족체계 슈퍼바이저 | 가족 맞은편에 앉은 여성 치료자/슈퍼바이저 |

김혜성은 반드시 여성이다. 서건창과 절대 닮으면 안 된다. 김혜성은 가족 4명과 같은 줄에 앉지 않는다.

## 참고 레퍼런스

가족 4명 상담실 구도:

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

아래 구버전 김혜성 자료는 남성/구버전 가능성이 있으므로 identity reference로 쓰지 않는다.

```text
Docs/GeneratedSources/FT001_ReferenceLocked_20260609/references/ft001_ref_supervisor_system_kim_hyesung_1600x900.png
Docs/GeneratedSources/FT001_ReferenceLocked_20260609/references/ft001_ref_supervisor_view_master_1600x900.png
Docs/GeneratedSources/FT001_ReferenceLocked_20260609/references/ft001_ref_supervisor_identity_1600x900.png
Docs/GeneratedSources/FT001_ReferenceLocked_20260609/references/ft001_ref_supervisor_expression_sheet_1600x900.png
```

## 구도 고정

### Family Master Shot

가족/담임 쪽 대사는 모두 같은 구도를 사용한다.

```text
카메라: 김혜성/플레이어가 앉은 자리에서 가족 4명을 바라보는 상담자 시점
왼쪽: 오선진
중앙 왼쪽: 박성빈
중앙 오른쪽/앞: 이주형
오른쪽: 서건창
전경: 낮은 상담 테이블
배경: 같은 상담실, 같은 창문, 같은 문, 같은 책장, 같은 의자
```

Family Master Shot에서는 4명이 모두 보여야 한다. 대사 화자만 강조하되 나머지 인물도 같은 자리에 남아 있어야 한다.

변경 금지:

- 카메라 높이
- 렌즈/화각
- 인물 좌표
- 인물 크기
- 의자 위치
- 테이블 높이
- 문/창문/책장 위치
- 방의 깊이감
- 하단 여백

변경 가능:

- 화자의 표정
- 화자의 손짓
- 다른 인물들의 미세한 반응
- 긴장/완화 정도
- 시선 방향

### Hyesung Master Shot

김혜성 대사와 슈퍼바이저 반응은 별도 고정 구도를 사용한다.

```text
카메라: 가족 쪽에서 김혜성을 바라보는 반대 시점
중심 인물: 김혜성
김혜성은 한국인 성인 여성 가족체계 슈퍼바이저
김혜성은 가족 4명 맞은편 좌석에 앉아 있음
```

김혜성이 말할 때는 김혜성 단독 또는 김혜성 중심 구도가 맞다. 가족 4명을 억지로 같은 화면에 넣지 않는다.

## 공통 스타일 프롬프트

모든 이미지에 공통 적용:

```text
polished commercial Korean visual novel CG, realistic painterly 2D, restrained emotional acting, warm counseling-room lighting, consistent character identity, cinematic but readable composition, natural seated counseling posture, no UI text, no caption, no watermark, native 1600x900 16:9 image
```

금지:

```text
standing character sprites, square portrait, stretched image, chibi, exaggerated anime face, photorealistic uncanny AI portrait, extreme fisheye, random extra people, changed clothes, changed room layout, cropped-out family members in family shots, text on image, UI frame, speech bubble, distorted hands
```

Family Master Shot 계열에는 항상 추가:

```text
Use the exact same locked camera composition as the approved Family Master Shot. Same room, same lens, same crop, same foreground table height, same chair positions, same character positions and scale. The four session-side figures must remain visible: grandmother left, mother center-left, young boy center-right/front, male teacher right. Do not zoom, pan, reframe, reseat, recrop, or change the furniture. Only change facial expressions, gaze, and small hand gestures according to the line.
```

Hyesung Master Shot 계열에는 항상 추가:

```text
Use the exact same locked camera composition as the approved Hyesung Master Shot. Kim Hyesung is a Korean adult female family-systems supervisor sitting across from the family, shown alone or centered. Same room, same lens, same crop, same chair position, same clothing, same face. Do not turn her into a male therapist, teacher, or family member. Only change facial expression and small hand gestures according to the line.
```

## 작업 순서

1. 먼저 `Family Master Shot` 기준 이미지 1장을 확정한다.
2. 같은 구도로 가족/담임 쪽 CG를 10장 단위로 만든다.
3. `Hyesung Master Shot` 기준 이미지 1장을 확정한다.
4. 같은 구도로 김혜성 CG를 만든다.
5. 10장마다 contact sheet를 만들어 구도 흔들림을 확인한다.
6. 구도가 흔들린 이미지는 최종 폴더에 넣지 말고 재생성한다.
7. 최종 폴더에는 통과 이미지와 manifest만 둔다.

## Shotlist

### Intro: 등장인물 소개

1. `ft001_cg_intro_01_mother_neutral.png`
   - Family Master Shot.
   - 화자: 박성빈.
   - 대사: "주형이 엄마 박성빈입니다. 밤에는 일하고, 아침에는 주형이를 학교에 보내려고 애쓰는데 매번 같은 자리에서 무너집니다."
   - 연기: 지친 소개, 죄책감과 압박감.

2. `ft001_cg_intro_02_child_neutral.png`
   - Family Master Shot.
   - 화자: 이주형.
   - 대사: "해솔초등학교 4학년 이주형입니다. 학교에 가야 하는 건 아는데, 아침이 되면 배가 아프고 엄마가 나가버릴 것 같아서 무서워요."
   - 연기: 작고 불안한 남자 초등학생.

3. `ft001_cg_intro_03_grandmother_neutral.png`
   - Family Master Shot.
   - 화자: 오선진.
   - 대사: "오선진입니다. 주형이 할머니입니다. 도와주고 싶은데 제 말이 자꾸 잔소리처럼 들리는 것 같습니다."
   - 연기: 걱정하지만 단단한 노년 여성.

4. `ft001_cg_intro_04_teacher_neutral.png`
   - Family Master Shot.
   - 화자: 서건창.
   - 대사: "서건창입니다. 주형이 담임입니다. 학교 절차도 챙겨야 하지만, 아이가 왜 멈추는지도 함께 알고 싶습니다."
   - 연기: 절차적이지만 걱정하는 남성 담임.

5. `ft001_cg_intro_05_supervisor_explaining.png`
   - Hyesung Master Shot.
   - 화자: 김혜성.
   - 대사: "김혜성입니다. 오늘 목표는 누가 문제인지 찾는 것이 아니라, 아침 장면이 어떤 순서로 반복되는지 보는 것입니다."
   - 연기: 차분하게 회기 목표를 잡는 여성 슈퍼바이저.

### Turn 1: 초기 합류와 문제 정의

6. `ft001_cg_t01_l01_mother_neutral.png` - Family Master Shot. 박성빈: "아침마다 같은 장면이 반복돼요. 깨우고, 달래고, 결국 제가 소리를 지르고 나면 둘 다 지쳐버립니다."
7. `ft001_cg_t01_l02_child_anxious.png` - Family Master Shot. 이주형: "엄마가 일하러 나가면 집이 너무 조용해요. 학교에 가면 괜찮은 척해야 해서 더 힘들어요."
8. `ft001_cg_t01_l03_mother_worried.png` - Family Master Shot. 박성빈: "저도 일부러 화내는 건 아니에요. 학교에서 또 전화 올까 봐 급해지고, 그러면 주형이가 더 안 움직여요."
9. `ft001_cg_t01_l04_child_quiet.png` - Family Master Shot. 이주형: "엄마가 급해지면 제가 말하면 안 될 것 같아요. 그냥 가만히 있으면 엄마가 조금 더 집에 있어요."
10. `ft001_cg_t01_l05_teacher_concerned.png` - Family Master Shot. 서건창: "교실에 오면 조용히 앉아 있습니다. 그런데 지각과 결석이 반복되니 학교도 계속 연락할 수밖에 없습니다."
11. `ft001_cg_t01_l06_supervisor_explaining.png` - Hyesung Master Shot. 김혜성: "첫 회기에서는 원인을 단정하지 마세요. 이 가족이 아침마다 어떤 순서로 서로를 밀고 당기는지 먼저 보세요."
12. `ft001_cg_t01_choice_idle.png` - Family Master Shot. 선택지 대기. 네 명 모두 조심스럽고 긴장된 중립 상태.
13. `ft001_cg_t01_reaction_a_mother_softened.png` - Family Master Shot. 좋은 선택 반응. 박성빈이 숨을 고르며 고개를 끄덕이고 이주형도 조금 말문이 열림.
14. `ft001_cg_t01_reaction_b_child_withdrawn.png` - Family Master Shot. 나쁜 선택 반응. 이주형이 시선을 내리고 박성빈이 실패한 보호자처럼 위축됨.
15. `ft001_cg_t01_reaction_c_teacher_procedural.png` - Family Master Shot. 절차 선택 반응. 서건창은 답하지만 가족 정서는 닫힘.

### Turn 2 Carryover

16. `ft001_cg_t02_l00_branch_mother_open.png` - Family Master Shot. 박성빈: "아까 각자 걱정을 먼저 물어봐 주셔서 조금 덜 몰리는 느낌이었어요. 그래서 아침 장면을 더 차분히 떠올려볼 수 있을 것 같습니다."
17. `ft001_cg_t02_l00_branch_child_closed.png` - Family Master Shot. 이주형: "아까는 제가 먼저 고쳐져야 하는 사람처럼 들렸어요. 그래서 지금은 뭘 말해도 또 혼날 것 같아요."
18. `ft001_cg_t02_l00_branch_teacher_cautious.png` - Family Master Shot. 서건창: "절차를 확인하는 건 필요하지만, 가족이 아직 긴장한 것 같습니다. 학교 이야기가 또 압박처럼 들릴까 조심스럽습니다."

### Turn 2: 가족역동 개념화

19. `ft001_cg_t02_l01_mother_defensive.png` - Family Master Shot. 박성빈: "학교에서 전화가 오면 제가 더 다그치게 돼요. 그러면 주형이는 더 굳어버립니다."
20. `ft001_cg_t02_l02_child_quiet.png` - Family Master Shot. 이주형: "엄마가 화내면 저는 아무 말도 안 하고 싶어요. 그러면 엄마가 더 화내요."
21. `ft001_cg_t02_l03_mother_exhausted.png` - Family Master Shot. 박성빈: "말을 안 하면 저는 더 무섭습니다. 오늘도 못 가면 직장에 또 늦고, 학교에서는 제가 방치하는 것처럼 보일까 봐요."
22. `ft001_cg_t02_l04_child_hesitant.png` - Family Master Shot. 이주형: "제가 말하면 엄마가 더 힘들까 봐요. 근데 아무 말 안 해도 엄마가 힘들어해서 어떻게 해야 할지 모르겠어요."
23. `ft001_cg_t02_l05_teacher_procedural.png` - Family Master Shot. 서건창: "학교 입장에서는 결석이 누적되면 절차가 필요합니다. 다만 연락할수록 아침 갈등이 커진다면 방식은 조정해볼 수 있습니다."
24. `ft001_cg_t02_l06_supervisor_explaining.png` - Hyesung Master Shot. 김혜성: "지금 핵심은 누가 시작했는지가 아니라, 서로의 반응이 다음 반응을 어떻게 부르는지입니다."
25. `ft001_cg_t02_choice_idle.png` - Family Master Shot. 선택지 대기. 가족이 아침 순환을 떠올리며 긴장한 상태.
26. `ft001_cg_t02_reaction_a_mother_softened.png` - Family Master Shot. 좋은 선택 반응. 박성빈과 이주형이 패턴을 함께 보기 시작함.
27. `ft001_cg_t02_reaction_b_mother_defensive.png` - Family Master Shot. 중간/위험 선택 반응. 박성빈 어깨가 굳고 이주형이 엄마 편을 든다고 느낌.
28. `ft001_cg_t02_reaction_c_child_withdrawn.png` - Family Master Shot. 나쁜 선택 반응. 이주형이 의자 깊숙이 몸을 넣고 침묵.

### Turn 3 Carryover

29. `ft001_cg_t03_l00_branch_child_links_pattern.png` - Family Master Shot. 이주형: "방금 그림으로 보니까 제가 멈추면 엄마가 더 급해지고, 엄마가 급해지면 저는 더 못 움직이는 것 같았어요."
30. `ft001_cg_t03_l00_branch_mother_defensive.png` - Family Master Shot. 박성빈: "제가 더 단호해야 한다는 말로 들리니까 또 제 책임인 것 같아요. 그러면 주형이 말을 들을 여유가 없어집니다."
31. `ft001_cg_t03_l00_branch_mother_cautious.png` - Family Master Shot. 박성빈: "방금 이야기가 도움이 되긴 했는데, 아직 제가 무엇을 다르게 해야 하는지는 잘 모르겠습니다."

### Turn 3: 감정과 구조 단서 확인

32. `ft001_cg_t03_l01_grandmother_critical.png` - Family Master Shot. 오선진: "성빈이가 일을 줄이면 되잖아요. 애가 약해서 그렇지, 집이 안정되면 나아질 겁니다."
33. `ft001_cg_t03_l02_mother_exhausted.png` - Family Master Shot. 박성빈: "엄마가 그렇게 말하면 제가 가족을 망친 사람 같아요. 그래도 일을 안 할 수는 없잖아요."
34. `ft001_cg_t03_l03_grandmother_worried.png` - Family Master Shot. 오선진: "나는 성빈이를 탓하려는 게 아니에요. 밤새 일하고 와서 애랑 싸우는 걸 보면 마음이 철렁합니다."
35. `ft001_cg_t03_l04_mother_tearful.png` - Family Master Shot. 박성빈: "그 걱정이 저한테는 '너는 엄마 노릇을 못 한다'는 말처럼 들려요. 그래서 엄마 앞에서는 더 작아져요."
36. `ft001_cg_t03_l05_child_scared.png` - Family Master Shot. 이주형: "할머니가 오면 엄마가 더 조용해져요. 그럼 저도 말하면 안 될 것 같아요."
37. `ft001_cg_t03_l06_supervisor_questioning.png` - Hyesung Master Shot. 김혜성: "비난을 바로 고치려 하기보다, 걱정이 어떤 말투로 나오고 그 말투가 누구를 침묵시키는지 확인하세요."
38. `ft001_cg_t03_choice_idle.png` - Family Master Shot. 선택지 대기. 세대 간 긴장이 올라와 있지만 모두 자리를 지키고 있음.
39. `ft001_cg_t03_reaction_a_grandmother_softened.png` - Family Master Shot. 좋은 선택 반응. 오선진의 목소리가 낮아지고 박성빈이 자기 느낌을 말함.
40. `ft001_cg_t03_reaction_b_grandmother_defensive.png` - Family Master Shot. 나쁜 선택 반응. 오선진이 입을 다물고 박성빈이 난처해함.
41. `ft001_cg_t03_reaction_c_child_hesitant.png` - Family Master Shot. 예외 탐색 반응. 이주형이 작은 예외를 조심스럽게 꺼냄.

### Turn 4 Carryover

42. `ft001_cg_t04_l00_branch_grandmother_softened.png` - Family Master Shot. 오선진: "내 말이 걱정이라는 걸 알아주니 조금 덜 억울하네요. 성빈이를 밀어붙이기보다 내가 어떤 식으로 도울 수 있을지 듣고 싶습니다."
43. `ft001_cg_t04_l00_branch_grandmother_stubborn.png` - Family Master Shot. 오선진: "제 말투만 문제라고 하시면 저는 더 할 말이 없습니다. 저는 정말 걱정돼서 그런 건데요."
44. `ft001_cg_t04_l00_branch_child_exception.png` - Family Master Shot. 이주형: "덜 힘들었던 날을 생각해보니, 엄마가 바로 재촉하지 않았던 아침은 조금 나았어요."

### Turn 4: 핵심 개입 선택

45. `ft001_cg_t04_l01_supervisor_questioning.png` - Hyesung Master Shot. 김혜성: "이제 개입은 멋진 기법보다 가족이 자기 패턴을 볼 수 있게 돕는 질문이어야 합니다."
46. `ft001_cg_t04_l02_mother_worried.png` - Family Master Shot. 박성빈: "제가 화내지 않으면 학교에서 더 뭐라고 할까 봐 무서워요. 그래서 부드럽게 말하려다가도 결국 재촉하게 됩니다."
47. `ft001_cg_t04_l03_child_quiet.png` - Family Master Shot. 이주형: "제가 안 가면 엄마가 집에 조금 더 있어요. 그때는 엄마가 나를 두고 바로 가버리지 않는 것 같아요."
48. `ft001_cg_t04_l04_mother_listening.png` - Family Master Shot. 박성빈: "주형아, 네가 학교 가기 싫은 게 나를 붙잡으려는 거였다는 말이야? 저는 그냥 제가 실패한 줄만 알았어요."
49. `ft001_cg_t04_l05_child_hesitant.png` - Family Master Shot. 이주형: "엄마를 힘들게 하려던 건 아니에요. 근데 아침에 엄마가 나가려고 하면 배가 아프고 머리가 멍해져요."
50. `ft001_cg_t04_l06_supervisor_explaining.png` - Hyesung Master Shot. 김혜성: "이 장면에서는 '왜 안 가니'보다 '네가 멈출 때 엄마와 학교가 어떻게 움직이는지'가 더 치료적인 질문입니다."
51. `ft001_cg_t04_choice_idle.png` - Family Master Shot. 선택지 대기. 가족이 등교 거부의 기능을 조금 이해한 상태.
52. `ft001_cg_t04_reaction_a_supervisor_approving.png` - Hyesung Master Shot. 좋은 선택 반응. 김혜성이 수련생의 순환질문을 승인하듯 차분히 바라봄.
53. `ft001_cg_t04_reaction_b_mother_anxious.png` - Family Master Shot. 위험 선택 반응. 박성빈이 실행 가능성을 걱정하고 이주형이 움츠림.
54. `ft001_cg_t04_reaction_c_child_withdrawn.png` - Family Master Shot. 나쁜 선택 반응. 가족이 답은 들었지만 관계 패턴은 남아 있는 닫힌 상태.

### Turn 5 Carryover

55. `ft001_cg_t05_l00_branch_teacher_adjusts.png` - Family Master Shot. 서건창: "방금 질문을 듣고 보니 학교 연락도 가족의 압박을 키울 수 있겠네요. 다음 주에는 연락 방식부터 바꿔보겠습니다."
56. `ft001_cg_t05_l00_branch_child_scared.png` - Family Master Shot. 이주형: "또 제가 치료받아야 하는 사람처럼 된 것 같아요. 그러면 엄마랑 학교가 왜 더 무서워지는지는 말하기 어렵습니다."
57. `ft001_cg_t05_l00_branch_mother_anxious.png` - Family Master Shot. 박성빈: "행동계약을 하면 당장은 정리될 것 같지만, 실패하면 또 제가 못 지킨 사람이 될까 봐 걱정됩니다."

### Turn 5: 다음 주 과제와 복기

58. `ft001_cg_t05_l01_teacher_concerned.png` - Family Master Shot. 서건창: "학교에서도 아침 연락 방식을 조정할 수 있다면 해보겠습니다. 가족이 덜 몰리게 하는 게 중요해 보입니다."
59. `ft001_cg_t05_l02_mother_softened.png` - Family Master Shot. 박성빈: "제가 혼자 해결해야 한다고 생각해서 더 몰아붙였던 것 같아요. 내일부터는 깨우기 전에 먼저 주형이 상태를 물어보고 싶어요."
60. `ft001_cg_t05_l03_child_relieved.png` - Family Master Shot. 이주형: "엄마가 먼저 물어보면 저도 바로 안 된다고만 하지는 않을 것 같아요. 가방은 전날 같이 챙겨볼 수 있어요."
61. `ft001_cg_t05_l04_grandmother_softened.png` - Family Master Shot. 오선진: "나는 아침에 전화해서 잔소리하기보다, 성빈이가 퇴근하고 잠깐 쉴 시간을 만들어주는 게 낫겠네요."
62. `ft001_cg_t05_l05_teacher_softened.png` - Family Master Shot. 서건창: "학교에서는 첫 연락을 바로 경고처럼 하지 않고, 등교 가능 시간을 확인하는 방식으로 바꿔보겠습니다."
63. `ft001_cg_t05_l06_supervisor_reflective.png` - Hyesung Master Shot. 김혜성: "마지막 선택은 가족이 다음 주 실제로 해볼 수 있는 작고 관찰 가능한 루틴이어야 합니다."
64. `ft001_cg_t05_choice_idle.png` - Family Master Shot. 선택지 대기. 가족이 다음 주 실험을 정하기 직전.
65. `ft001_cg_t05_reaction_a_mother_softened.png` - Family Master Shot. 좋은 선택 반응. 가족이 작은 변화 한 가지씩을 합의함.
66. `ft001_cg_t05_reaction_b_child_scared.png` - Family Master Shot. 나쁜 선택 반응. 이주형이 고개만 끄덕이고 박성빈은 부담을 느낌.
67. `ft001_cg_t05_reaction_c_teacher_procedural.png` - Family Master Shot. 나쁜 선택 반응. 가족이 무엇을 해야 할지 모른 채 상담실을 나서는 느낌.

## 품질검수 기준

최종 폴더에 넣기 전 반드시 확인한다.

1. 1600x900인지 확인한다.
2. 모든 Family Master Shot에 오선진, 박성빈, 이주형, 서건창이 같은 좌표로 보이는지 확인한다.
3. 김혜성 컷에서 김혜성이 여성이고 서건창과 닮지 않았는지 확인한다.
4. 컷마다 테이블 높이와 인물 크기가 흔들리지 않는지 contact sheet로 확인한다.
5. 텍스트, 말풍선, UI가 이미지에 들어가지 않았는지 확인한다.
6. 10장 단위로 먼저 공유하고, 통과된 이미지만 최종 폴더에 넣는다.

## 완료 보고 형식

이미지 생성 창은 작업 후 아래 형식으로 이 창에 보고한다.

```text
FT001 line-by-line CG batch complete
Batch: 01/07
Generated: 10
Accepted: 8
Rejected/regenerate: 2
Final folder:
Assets/Resources/VN/EventCG/FT001_LineByLineLocked/
Contact sheet:
Docs/GeneratedSources/FT001_LineByLineLocked_20260610/contact_sheet_batch01.png
Notes:
- 김혜성 female reference used: yes/no
- Family Master Shot locked: yes/no
- Any crop/ratio issue: yes/no
```
