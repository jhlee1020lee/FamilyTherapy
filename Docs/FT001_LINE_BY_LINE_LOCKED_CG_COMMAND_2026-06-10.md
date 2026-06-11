# FT-001 Line-By-Line Locked CG Generation Command

이 문서는 이미지 생성 전용 창에 그대로 전달한다.

목표는 FT-001만 다시 만든다. 이번에는 기존 30장 요약 CG 방식이 아니라, **대사/반응별로 한 장씩** 만든다.

## 핵심 목표

FT-001 가족치료 수련 비주얼노벨의 상담 장면을 상용 비주얼노벨처럼 보이게 만들기 위해, 모든 주요 대사와 선택 반응에 대응하는 CG를 생성한다.

이번 작업의 최우선 기준은 **구도 고정**이다.

기존 문제는 이미지마다 카메라 위치, 인물 간 거리, 테이블 높이, 우측 인물 크롭이 조금씩 바뀌어서 게임에서 컷이 전환될 때 흔들려 보인다는 점이었다. 이번에는 그 문제를 없애야 한다.

## 생성 수량

최소 생성 수량:

```text
대사 CG 30장
선택 후 반응 CG 15장
선택지 대기 CG 5장
총 50장
```

선택지 대기 CG는 대사는 아니지만 실제 게임 화면에서 선택지 뒤에 깔릴 이미지라 같이 만든다.

## 출력 위치

최종 게임 적용용 폴더:

```text
Assets/Resources/VN/EventCG/FT001_LineByLineLocked/
```

소스/후보/검토용 백업 폴더:

```text
Docs/GeneratedSources/FT001_LineByLineLocked_20260610/
```

## 파일 규칙

- 모든 최종 파일은 `1600x900`
- 모든 최종 파일은 `.png`
- 텍스트, 자막, UI, 말풍선, 워터마크 금지
- 파일명은 아래 shotlist의 이름을 그대로 사용
- 정사각형 생성 후 늘리기 금지
- 16:9가 아닌 이미지를 강제로 리사이즈해서 제출 금지
- 하단 25-30%는 대사창이 올라갈 수 있게 비교적 단순하게 유지

## 스타일 고정

```text
polished commercial Korean visual novel CG, realistic painterly 2D, restrained emotional acting, warm counseling-room lighting, consistent character identity, cinematic but readable composition, natural seated counseling posture, no UI text, no caption, no watermark
```

금지:

```text
photorealistic uncanny AI portrait, standing character sprites, chibi, anime exaggeration, extreme fisheye, random extra people, changed clothes, changed room layout, cropped-out family members in family shots, text on image, UI frame, speech bubble, distorted hands
```

## 인물 고정

| ID | 이름 | 역할 | 고정 사항 |
| --- | --- | --- | --- |
| `ft001_mother` | 박성빈 | 어머니 | 한국인 성인 여성, 야간 근무로 지쳐 있음, 죄책감과 압박감 |
| `ft001_child` | 이주형 | 자녀 | 한국인 남자 초등학생, 불안하고 위축됨, 청소년 아님 |
| `ft001_grandmother` | 오선진 | 외조모 | 한국인 노년 여성, 걱정이 많고 말투가 단단함 |
| `ft001_teacher` | 서건창 | 담임 | 한국인 성인 남성 교사, 가족 구성원 아님 |
| `supervisor_system` | 김혜성 | 가족체계 기본 슈퍼바이저 | 한국인 성인 여성 치료자/슈퍼바이저, 남성 아님 |

중요:

- 김혜성은 여성이다.
- 김혜성은 서건창과 완전히 다른 인물이다.
- 김혜성은 가족 4명과 같은 줄에 앉지 않는다.
- 이주형은 남자 초등학생이다.
- 서건창은 남자 담임교사다.

## 레퍼런스

가족 4명과 상담실 구도 참고:

```text
Docs/GeneratedSources/FT001_ReferenceLocked_20260609/references/ft001_ref_group_seating_master_1600x900.png
Docs/GeneratedSources/FT001_ReferenceLocked_20260609/references/ft001_ref_counseling_room_empty_1600x900.png
Docs/GeneratedSources/FT001_ReferenceLocked_20260609/references/ft001_ref_cast_identity_sheet_1600x900.png
```

김혜성 여성 슈퍼바이저 참고:

```text
Docs/GeneratedSources/FT001_KH_FemaleRef_20260610/ref/kh_f_identity_1600x900.png
Docs/GeneratedSources/FT001_KH_FemaleRef_20260610/ref/kh_f_speaking_1600x900.png
Docs/GeneratedSources/FT001_KH_FemaleRef_20260610/ref/kh_f_opposite_seating_1600x900.png
Docs/GeneratedSources/FT001_KH_FemaleRef_20260610/kh_f_reference_contact_sheet_20260610.png
```

주의:

```text
Docs/GeneratedSources/FT001_ReferenceLocked_20260609/references/ft001_ref_supervisor_system_kim_hyesung_1600x900.png
Docs/GeneratedSources/FT001_ReferenceLocked_20260609/references/ft001_ref_supervisor_view_master_1600x900.png
Docs/GeneratedSources/FT001_ReferenceLocked_20260609/references/ft001_ref_supervisor_identity_1600x900.png
Docs/GeneratedSources/FT001_ReferenceLocked_20260609/references/ft001_ref_supervisor_expression_sheet_1600x900.png
```

위 오래된 김혜성 파일들은 남성/구버전 가능성이 있으므로 김혜성 identity reference로 사용하지 않는다.

## 구도 고정 규칙

### Family Master Shot

가족/기관 인물 대사와 선택 반응은 모두 같은 마스터 구도를 유지한다.

```text
카메라: 김혜성/플레이어가 앉은 자리에서 가족 4명을 바라보는 상담자 시점
왼쪽: 오선진
중앙 왼쪽: 박성빈
중앙 오른쪽/앞: 이주형
오른쪽: 서건창
전경: 낮은 상담 테이블
배경: 같은 상담실, 같은 창문, 같은 문, 같은 책장, 같은 의자
```

절대 바꾸지 말 것:

- 카메라 높이
- 렌즈/화각
- 인물 좌표
- 인물 크기
- 의자 위치
- 테이블 높이
- 문/창문/책장 위치
- 방의 깊이감
- 하단 여백

대사별로 바꿀 수 있는 것:

- 화자의 표정
- 화자의 손짓
- 다른 인물들의 미세한 반응
- 긴장/완화 정도
- 시선 방향

즉, **같은 장면에서 배우들이 표정과 손짓만 바꾸는 느낌**이어야 한다. 컷마다 카메라가 움직이면 실패다.

### Hyesung Master Shot

김혜성 대사/슈퍼바이저 반응은 별도의 고정 구도를 사용한다.

```text
카메라: 가족 쪽에서 김혜성을 바라보는 반대 시점
중심 인물: 김혜성
김혜성은 여성 치료자/슈퍼바이저
김혜성은 가족 4명 맞은편 좌석에 앉아 있음
```

김혜성 컷에서는 가족 4명을 억지로 전부 넣지 않아도 된다. 김혜성이 말할 때는 김혜성 단독 또는 김혜성 중심 구도가 맞다.

## 생성 방식

가능하면 텍스트 프롬프트만으로 매번 새로 생성하지 말고, 반드시 레퍼런스 이미지 기반으로 생성한다.

권장 순서:

1. `Family Master Shot`을 먼저 1장 확정한다.
2. 그 마스터 구도를 기준으로 아래 가족 컷들을 생성한다.
3. `Hyesung Master Shot`을 먼저 1장 확정한다.
4. 그 마스터 구도를 기준으로 김혜성 컷들을 생성한다.
5. 10장 단위로 contact sheet를 만들어 구도 흔들림을 확인한다.
6. 구도가 흔들린 이미지는 최종 폴더에 넣지 말고 재생성한다.

## 공통 프롬프트 뼈대

가족 컷에는 아래 문장을 항상 포함한다.

```text
Use the exact same locked camera composition as the approved Family Master Shot. Same room, same lens, same crop, same foreground table height, same chair positions, same character positions and scale. Do not zoom, pan, reframe, reseat, recrop, or change the furniture. Only change facial expressions, gaze, and small hand gestures according to the line.
```

김혜성 컷에는 아래 문장을 항상 포함한다.

```text
Use the exact same locked camera composition as the approved Hyesung Master Shot. Kim Hyesung is a Korean adult female family-systems supervisor sitting across from the family, shown alone or centered. Same room, same lens, same crop, same chair position, same clothing, same face. Do not turn her into a male therapist, teacher, or family member. Only change facial expression and small hand gestures according to the line.
```

## Shotlist

### Turn 1: 초기 합류와 문제 정의

1. `ft001_cg_t01_l01_mother_neutral.png`
   - Family Master Shot.
   - 화자: 박성빈.
   - 대사: "아침마다 같은 장면이 반복돼요. 깨우고, 달래고, 결국 제가 소리를 지르고 나면 둘 다 지쳐버립니다."
   - 표정/연기: 지치고 압박받은 어머니. 말하면서도 죄책감이 보임.

2. `ft001_cg_t01_l02_child_anxious.png`
   - Family Master Shot.
   - 화자: 이주형.
   - 대사: "엄마가 일하러 나가면 집이 너무 조용해요. 학교에 가면 괜찮은 척해야 해서 더 힘들어요."
   - 표정/연기: 불안하고 작게 움츠린 남자 초등학생.

3. `ft001_cg_t01_l03_mother_worried.png`
   - Family Master Shot.
   - 화자: 박성빈.
   - 대사: "저도 일부러 화내는 건 아니에요. 학교에서 또 전화 올까 봐 급해지고, 그러면 주형이가 더 안 움직여요."
   - 표정/연기: 걱정과 변명이 섞인 어머니.

4. `ft001_cg_t01_l04_child_quiet.png`
   - Family Master Shot.
   - 화자: 이주형.
   - 대사: "엄마가 급해지면 제가 말하면 안 될 것 같아요. 그냥 가만히 있으면 엄마가 조금 더 집에 있어요."
   - 표정/연기: 고개를 살짝 내리고 조용히 말함.

5. `ft001_cg_t01_l05_teacher_concerned.png`
   - Family Master Shot.
   - 화자: 서건창.
   - 대사: "교실에 오면 조용히 앉아 있습니다. 그런데 지각과 결석이 반복되니 학교도 계속 연락할 수밖에 없습니다."
   - 표정/연기: 절차적이지만 걱정하는 담임교사.

6. `ft001_cg_t01_l06_supervisor_explaining.png`
   - Hyesung Master Shot.
   - 화자: 김혜성.
   - 대사: "첫 회기에서는 원인을 단정하지 마세요. 이 가족이 아침마다 어떤 순서로 서로를 밀고 당기는지 먼저 보세요."
   - 표정/연기: 차분하게 교육하는 여성 슈퍼바이저.

7. `ft001_cg_t01_choice_idle.png`
   - Family Master Shot.
   - 선택지 대기용.
   - 네 명 모두 조심스럽고 긴장된 중립 상태.

8. `ft001_cg_t01_reaction_a_mother_softened.png`
   - Family Master Shot.
   - 좋은 선택 반응.
   - 반응: 박성빈은 숨을 고르며 고개를 끄덕이고, 이주형도 작은 목소리로 말을 잇기 시작함.

9. `ft001_cg_t01_reaction_b_child_withdrawn.png`
   - Family Master Shot.
   - 나쁜 선택 반응.
   - 반응: 이주형은 시선을 내리고, 박성빈은 실패한 보호자처럼 느끼며 굳음.

10. `ft001_cg_t01_reaction_c_teacher_procedural.png`
   - Family Master Shot.
   - 절차 중심 선택 반응.
   - 반응: 서건창은 대답하지만 가족 정서는 닫힘.

### Turn 2: 가족역동 개념화

11. `ft001_cg_t02_l01_mother_defensive.png`
   - Family Master Shot.
   - 화자: 박성빈.
   - 대사: "학교에서 전화가 오면 제가 더 다그치게 돼요. 그러면 주형이는 더 굳어버립니다."
   - 표정/연기: 방어적이지만 스스로도 힘들어함.

12. `ft001_cg_t02_l02_child_quiet.png`
   - Family Master Shot.
   - 화자: 이주형.
   - 대사: "엄마가 화내면 저는 아무 말도 안 하고 싶어요. 그러면 엄마가 더 화내요."
   - 표정/연기: 조용하고 위축됨.

13. `ft001_cg_t02_l03_mother_exhausted.png`
   - Family Master Shot.
   - 화자: 박성빈.
   - 대사: "말을 안 하면 저는 더 무섭습니다. 오늘도 못 가면 직장에 또 늦고, 학교에서는 제가 방치하는 것처럼 보일까 봐요."
   - 표정/연기: 피로, 생계 압박, 평가 불안.

14. `ft001_cg_t02_l04_child_hesitant.png`
   - Family Master Shot.
   - 화자: 이주형.
   - 대사: "제가 말하면 엄마가 더 힘들까 봐요. 근데 아무 말 안 해도 엄마가 힘들어해서 어떻게 해야 할지 모르겠어요."
   - 표정/연기: 망설이며 조심스럽게 말함.

15. `ft001_cg_t02_l05_teacher_procedural.png`
   - Family Master Shot.
   - 화자: 서건창.
   - 대사: "학교 입장에서는 결석이 누적되면 절차가 필요합니다. 다만 연락할수록 아침 갈등이 커진다면 방식은 조정해볼 수 있습니다."
   - 표정/연기: 절차적이지만 협력 가능성을 열어둠.

16. `ft001_cg_t02_l06_supervisor_explaining.png`
   - Hyesung Master Shot.
   - 화자: 김혜성.
   - 대사: "지금 핵심은 누가 시작했는지가 아니라, 서로의 반응이 다음 반응을 어떻게 부르는지입니다."
   - 표정/연기: 체계 관점을 설명하는 차분한 여성 슈퍼바이저.

17. `ft001_cg_t02_choice_idle.png`
   - Family Master Shot.
   - 선택지 대기용.
   - 긴장 중립. 가족이 서로를 조심스럽게 의식함.

18. `ft001_cg_t02_reaction_a_mother_softened.png`
   - Family Master Shot.
   - 좋은 선택 반응.
   - 반응: 박성빈과 이주형이 아침 장면의 순환을 함께 보기 시작함.

19. `ft001_cg_t02_reaction_b_mother_defensive.png`
   - Family Master Shot.
   - 나쁜 선택 반응.
   - 반응: 박성빈의 어깨가 굳고, 이주형은 상담자가 엄마 편을 든다고 느낌.

20. `ft001_cg_t02_reaction_c_child_withdrawn.png`
   - Family Master Shot.
   - 나쁜 선택 반응.
   - 반응: 이주형이 대답하지 않고 의자 깊숙이 몸을 넣음.

### Turn 3: 감정과 구조 단서 확인

21. `ft001_cg_t03_l01_grandmother_critical.png`
   - Family Master Shot.
   - 화자: 오선진.
   - 대사: "성빈이가 일을 줄이면 되잖아요. 애가 약해서 그렇지, 집이 안정되면 나아질 겁니다."
   - 표정/연기: 비판적으로 들리지만 걱정이 깔린 외조모.

22. `ft001_cg_t03_l02_mother_exhausted.png`
   - Family Master Shot.
   - 화자: 박성빈.
   - 대사: "엄마가 그렇게 말하면 제가 가족을 망친 사람 같아요. 그래도 일을 안 할 수는 없잖아요."
   - 표정/연기: 상처받고 지친 어머니.

23. `ft001_cg_t03_l03_grandmother_worried.png`
   - Family Master Shot.
   - 화자: 오선진.
   - 대사: "나는 성빈이를 탓하려는 게 아니에요. 밤새 일하고 와서 애랑 싸우는 걸 보면 마음이 철렁합니다."
   - 표정/연기: 걱정이 앞으로 드러나는 외조모.

24. `ft001_cg_t03_l04_mother_tearful.png`
   - Family Master Shot.
   - 화자: 박성빈.
   - 대사: "그 걱정이 저한테는 '너는 엄마 노릇을 못 한다'는 말처럼 들려요. 그래서 엄마 앞에서는 더 작아져요."
   - 표정/연기: 눈물이 고이고 작아지는 어머니.

25. `ft001_cg_t03_l05_child_scared.png`
   - Family Master Shot.
   - 화자: 이주형.
   - 대사: "할머니가 오면 엄마가 더 조용해져요. 그럼 저도 말하면 안 될 것 같아요."
   - 표정/연기: 어른들 긴장을 감지하고 겁먹은 아이.

26. `ft001_cg_t03_l06_supervisor_questioning.png`
   - Hyesung Master Shot.
   - 화자: 김혜성.
   - 대사: "비난을 바로 고치려 하기보다, 걱정이 어떤 말투로 나오고 그 말투가 누구를 침묵시키는지 확인하세요."
   - 표정/연기: 조용히 질문을 던지는 여성 슈퍼바이저.

27. `ft001_cg_t03_choice_idle.png`
   - Family Master Shot.
   - 선택지 대기용.
   - 오선진과 박성빈 사이의 긴장이 보이되 과장하지 않음.

28. `ft001_cg_t03_reaction_a_grandmother_softened.png`
   - Family Master Shot.
   - 좋은 선택 반응.
   - 반응: 오선진의 목소리가 낮아지고, 박성빈이 도움과 심판의 차이를 말할 수 있게 됨.

29. `ft001_cg_t03_reaction_b_grandmother_defensive.png`
   - Family Master Shot.
   - 나쁜 선택 반응.
   - 반응: 오선진은 입을 다물고, 박성빈은 더 난처해함.

30. `ft001_cg_t03_reaction_c_child_hesitant.png`
   - Family Master Shot.
   - 예외 질문 반응.
   - 반응: 이주형이 아주 작은 예외를 조심스럽게 말함.

### Turn 4: 핵심 개입 선택

31. `ft001_cg_t04_l01_supervisor_questioning.png`
   - Hyesung Master Shot.
   - 화자: 김혜성.
   - 대사: "이제 개입은 멋진 기법보다 가족이 자기 패턴을 볼 수 있게 돕는 질문이어야 합니다."
   - 표정/연기: 수련생에게 핵심을 짚어주는 여성 슈퍼바이저.

32. `ft001_cg_t04_l02_mother_worried.png`
   - Family Master Shot.
   - 화자: 박성빈.
   - 대사: "제가 화내지 않으면 학교에서 더 뭐라고 할까 봐 무서워요. 그래서 부드럽게 말하려다가도 결국 재촉하게 됩니다."
   - 표정/연기: 불안과 걱정.

33. `ft001_cg_t04_l03_child_quiet.png`
   - Family Master Shot.
   - 화자: 이주형.
   - 대사: "제가 안 가면 엄마가 집에 조금 더 있어요. 그때는 엄마가 나를 두고 바로 가버리지 않는 것 같아요."
   - 표정/연기: 조용히 중요한 말을 꺼냄.

34. `ft001_cg_t04_l04_mother_listening.png`
   - Family Master Shot.
   - 화자: 박성빈.
   - 대사: "주형아, 네가 학교 가기 싫은 게 나를 붙잡으려는 거였다는 말이야? 저는 그냥 제가 실패한 줄만 알았어요."
   - 표정/연기: 아이의 말을 처음으로 다르게 듣는 어머니.

35. `ft001_cg_t04_l05_child_hesitant.png`
   - Family Master Shot.
   - 화자: 이주형.
   - 대사: "엄마를 힘들게 하려던 건 아니에요. 근데 아침에 엄마가 나가려고 하면 배가 아프고 머리가 멍해져요."
   - 표정/연기: 몸 증상을 조심스럽게 설명하는 아이.

36. `ft001_cg_t04_l06_supervisor_explaining.png`
   - Hyesung Master Shot.
   - 화자: 김혜성.
   - 대사: "이 장면에서는 '왜 안 가니'보다 '네가 멈출 때 엄마와 학교가 어떻게 움직이는지'가 더 치료적인 질문입니다."
   - 표정/연기: 교육적이고 차분한 설명.

37. `ft001_cg_t04_choice_idle.png`
   - Family Master Shot.
   - 선택지 대기용.
   - 가족이 패턴을 막 보기 시작한 긴장된 중립 상태.

38. `ft001_cg_t04_reaction_a_supervisor_approving.png`
   - Hyesung Master Shot.
   - 좋은 선택 반응.
   - 반응: 김혜성이 수련생의 순환질문을 승인하는 듯 차분하게 바라봄.

39. `ft001_cg_t04_reaction_b_mother_anxious.png`
   - Family Master Shot.
   - 나쁜 선택 반응.
   - 반응: 박성빈은 실행 가능성을 걱정하고, 이주형은 다시 몸을 움츠림.

40. `ft001_cg_t04_reaction_c_child_withdrawn.png`
   - Family Master Shot.
   - 나쁜 선택 반응.
   - 반응: 성급한 진단으로 이주형이 닫히고 가족의 거리가 벌어짐.

### Turn 5: 다음 주 과제와 복기

41. `ft001_cg_t05_l01_teacher_concerned.png`
   - Family Master Shot.
   - 화자: 서건창.
   - 대사: "학교에서도 아침 연락 방식을 조정할 수 있다면 해보겠습니다. 가족이 덜 몰리게 하는 게 중요해 보입니다."
   - 표정/연기: 협력적으로 조정 가능성을 말하는 담임.

42. `ft001_cg_t05_l02_mother_softened.png`
   - Family Master Shot.
   - 화자: 박성빈.
   - 대사: "제가 혼자 해결해야 한다고 생각해서 더 몰아붙였던 것 같아요. 내일부터는 깨우기 전에 먼저 주형이 상태를 물어보고 싶어요."
   - 표정/연기: 긴장이 풀리고 작게 변화 의지가 생긴 어머니.

43. `ft001_cg_t05_l03_child_relieved.png`
   - Family Master Shot.
   - 화자: 이주형.
   - 대사: "엄마가 먼저 물어보면 저도 바로 안 된다고만 하지는 않을 것 같아요. 가방은 전날 같이 챙겨볼 수 있어요."
   - 표정/연기: 조금 안도한 아이.

44. `ft001_cg_t05_l04_grandmother_softened.png`
   - Family Master Shot.
   - 화자: 오선진.
   - 대사: "나는 아침에 전화해서 잔소리하기보다, 성빈이가 퇴근하고 잠깐 쉴 시간을 만들어주는 게 낫겠네요."
   - 표정/연기: 비판에서 지원으로 이동한 외조모.

45. `ft001_cg_t05_l05_teacher_softened.png`
   - Family Master Shot.
   - 화자: 서건창.
   - 대사: "학교에서는 첫 연락을 바로 경고처럼 하지 않고, 등교 가능 시간을 확인하는 방식으로 바꿔보겠습니다."
   - 표정/연기: 절차보다 협력으로 이동한 담임.

46. `ft001_cg_t05_l06_supervisor_reflective.png`
   - Hyesung Master Shot.
   - 화자: 김혜성.
   - 대사: "마지막 선택은 가족이 다음 주 실제로 해볼 수 있는 작고 관찰 가능한 루틴이어야 합니다."
   - 표정/연기: 수련생에게 회기를 정리해주는 여성 슈퍼바이저.

47. `ft001_cg_t05_choice_idle.png`
   - Family Master Shot.
   - 선택지 대기용.
   - 가족이 조금 열렸지만 아직 조심스러운 상태.

48. `ft001_cg_t05_reaction_a_mother_softened.png`
   - Family Master Shot.
   - 좋은 선택 반응.
   - 반응: 가족이 다음 아침에 해볼 구체적 변화를 합의함.

49. `ft001_cg_t05_reaction_b_child_scared.png`
   - Family Master Shot.
   - 나쁜 선택 반응.
   - 반응: 이주형은 고개를 끄덕이지만 표정은 더 굳고, 박성빈은 조심스러워짐.

50. `ft001_cg_t05_reaction_c_teacher_procedural.png`
   - Family Master Shot.
   - 나쁜 선택 반응.
   - 반응: 가족은 무엇을 해야 할지 알지 못하고 상담실을 나서는 느낌.

## QA 기준

최종 폴더에 넣기 전에 반드시 확인한다.

- 50장 모두 정확히 `1600x900`인가?
- 가족 컷에서 네 명이 모두 앉아 있는가?
- 가족 컷끼리 카메라/인물 위치/테이블 높이가 흔들리지 않는가?
- 김혜성 컷에서 김혜성이 여성 슈퍼바이저로 보이는가?
- 김혜성이 서건창처럼 보이거나 남성으로 보이지 않는가?
- 이주형이 초등학생 남자아이로 보이는가?
- 서건창이 가족 구성원이 아니라 담임교사로 보이는가?
- 하단 25-30%에 대사창을 올려도 핵심 얼굴이 가려지지 않는가?
- 이미지 안에 글자, 자막, UI, 워터마크가 없는가?

## 완료 보고 형식

작업이 끝나면 이 창에 아래 정보를 알려준다.

```text
완료 폴더:
총 파일 수:
누락 파일:
contact sheet 경로:
구도 흔들림이 있는 후보:
김혜성 컷 확인 결과:
```
