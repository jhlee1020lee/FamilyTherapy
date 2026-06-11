# FT001 Line-By-Line Event CG Side Window Command 2026-06-10

이 문서는 이미지 생성 전용 창에 그대로 붙여넣는 작업 명령이다.

## Copy-Paste Command

```text
너는 Family Therapy Practicum의 이미지 생성 전용 작업 창이다.

이번 작업은 FT-001만 한다. 다른 사례는 만들지 마라.

목표는 FT-001을 기존 스프라이트 조합 방식이 아니라, 대사 한 줄마다 한 장의 1600x900 풀씬 Event CG로 연출하는 것이다. 지금은 FT-001 회기 대사와 선택 후 반응만 만든다.

1차 총량:
- 회기 대사 CG: 25장
- 선택 후 반응 CG: 15장
- 합계: 40장

후순위:
- 선택지 대기 화면 CG 5장
- 이 5장은 40장 완료 후 별도 지시가 있을 때 만든다.

중요:
- 한 번에 40장을 무리해서 만들지 말고, 8~10장 단위로 배치 작업한다.
- 각 배치 완료 후 contact sheet를 만든다.
- 이미지마다 파일명을 정확히 붙인다.
- 정사각형 이미지를 만들지 마라.
- 1600x900, 16:9, no text, no watermark, no UI baked in.
- 하단 25~30%는 대화창이 올라갈 공간이므로 얼굴, 손, 핵심 감정 정보를 넣지 않는다.
- 모든 이미지는 같은 상담실, 같은 시간대, 같은 렌즈감, 같은 인물 정체성을 유지한다.

작업 폴더:

소스 보관:
C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity\Docs\GeneratedSources\FT001_LineByLineEventCG_20260610\source

컨택트시트:
C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity\Docs\GeneratedSources\FT001_LineByLineEventCG_20260610\contact_sheets

최종 런타임 폴더는 나중에 이 창이 직접 넣지 않는다. 우선 source 폴더에 정확한 파일명으로 저장한다. Codex 메인 창이 검수 후 Unity 코드 매핑과 런타임 반영을 한다.

현재 Unity 코드는 FT001 일부 대사를 30장짜리 commercial branching CG에 묶어 쓰고 있다. 하지만 이번 작업은 그 매핑을 따르지 말고, 아래 line-by-line 파일명 기준으로 새 원본을 만든다. 이후 메인 창에서 1대사=1CG 매핑으로 바꿀 예정이다.

## Visual Style Lock

상용 한국 비주얼노벨 느낌의 사실적인 2D/세미리얼 일러스트.
실사 사진처럼 보이기보다, 정돈된 상업 VN 이벤트 CG처럼 보이게 한다.
색감은 상담실의 따뜻한 목재, 차분한 청록/베이지 포인트, 부드러운 자연광을 유지한다.
인물은 과장된 애니 표정보다 실제 상담 장면처럼 절제된 감정 변화가 보여야 한다.

절대 금지:
- 텍스트, 자막, UI, 말풍선, 워터마크
- 캐릭터 얼굴이 매번 바뀌는 것
- 같은 인물이 장면마다 나이/성별/머리 모양이 바뀌는 것
- 상담자가 가족과 같은 줄에 앉는 것
- 김혜성을 남성으로 그리는 것
- 이주형을 여자아이 또는 청소년으로 그리는 것
- 서건창을 가족 구성원처럼 보이게 하는 것
- 생성 이미지 비율을 나중에 늘려 맞추는 것

## Character Lock

박성빈:
- ft001_mother
- 어머니
- 성인 한국 여성
- 야간 근무와 자녀 등교 거부 사이에서 소진되어 있음
- 지쳐 있지만 아이를 포기한 사람이 아님

이주형:
- ft001_child
- 자녀
- 남자 초등학생
- 작고 위축된 인상
- 등교 거부 뒤에 분리 불안과 가족 긴장을 숨김

오선진:
- ft001_grandmother
- 외조모
- 60~70대 한국 여성
- 비판적으로 말하지만 실제로는 걱정과 책임감이 강함

서건창:
- ft001_teacher
- 남자 담임교사
- 가족 구성원이 아님
- 학교 절차와 아이 걱정 사이에서 조심스럽게 말함

김혜성:
- supervisor_system
- 가족체계 기본 슈퍼바이저
- 여성
- 상담자/슈퍼바이저 위치
- 가족과 같은 줄에 앉지 않음
- 김혜성이 말하는 장면은 김혜성 단독 또는 김혜성 중심 구도

## Composition Lock

Family-side scene:
- 왼쪽: 오선진
- 중앙 왼쪽: 박성빈
- 중앙 또는 약간 앞: 이주형
- 오른쪽: 서건창
- 모두 앉아 있어야 한다.
- 가족-side CG에는 가능하면 네 명이 모두 등장해야 한다.
- 말하는 인물 쪽으로 시선, 조명, 초점, 손동작을 약간 더 준다.

Supervisor-side scene:
- 김혜성이 말하는 장면만 사용한다.
- 김혜성은 가족 건너편 상담자 자리에서 앉아 있다.
- 김혜성을 혼자 보여주거나 김혜성 중심으로 보여준다.
- 네 명이 억지로 같은 프레임에 들어오지 않아도 된다.

## Batch Rule

먼저 Batch 01의 10장만 생성한다.
Batch 01 완료 후 contact sheet를 만들고 멈춘다.
내가 확인하면 Batch 02로 넘어간다.

Batch 01:
1. ft001_line_t01_l01_mother_neutral.png
2. ft001_line_t01_l02_child_anxious.png
3. ft001_line_t01_l03_mother_worried.png
4. ft001_line_t01_l04_child_quiet.png
5. ft001_line_t01_l05_teacher_concerned.png
6. ft001_line_t01_l06_supervisor_explaining.png
7. ft001_line_t01_reaction_a_mother_softened.png
8. ft001_line_t01_reaction_b_child_withdrawn.png
9. ft001_line_t01_reaction_c_teacher_procedural.png
10. ft001_line_t02_l01_mother_defensive.png

Batch 01 scene notes:

1. ft001_line_t01_l01_mother_neutral.png
박성빈이 처음 상담실에서 지친 얼굴로 아침마다 같은 장면이 반복된다고 말한다. 네 명 모두 앉아 있고, 박성빈에게 초점. 이주형은 작게 웅크리고, 오선진은 걱정스럽게 보고, 서건창은 조심스럽게 듣는다.

2. ft001_line_t01_l02_child_anxious.png
이주형이 엄마가 일하러 나가면 집이 너무 조용하고 학교에서 괜찮은 척해야 해서 힘들다고 말한다. 초점은 남자 초등학생 이주형. 어른들은 끼어들지 않고 듣는다.

3. ft001_line_t01_l03_mother_worried.png
박성빈이 일부러 화내는 게 아니라 학교 전화가 올까 급해진다고 말한다. 박성빈의 걱정과 죄책감이 보이게 한다. 이주형은 더 조용해지고, 서건창은 미안한 듯 듣는다.

4. ft001_line_t01_l04_child_quiet.png
이주형이 엄마가 급해지면 말하면 안 될 것 같고 가만히 있으면 엄마가 조금 더 집에 있는다고 말한다. 아이의 조용한 기능적 행동이 드러나는 장면. 과장된 울음 금지.

5. ft001_line_t01_l05_teacher_concerned.png
서건창이 교실에서는 조용하지만 지각과 결석이 반복되어 학교도 연락할 수밖에 없다고 말한다. 가족이 아닌 학교 측 인물임이 보이게 정장/교사 분위기. 가족들은 압박을 느끼지만 듣고 있다.

6. ft001_line_t01_l06_supervisor_explaining.png
김혜성이 첫 회기에서 원인을 단정하지 말고 반복되는 아침 장면의 순서를 보라고 설명한다. 김혜성 단독 또는 김혜성 중심 구도. 여성 슈퍼바이저, 차분하고 전문적. 가족 네 명을 같은 줄에 넣지 마라.

7. ft001_line_t01_reaction_a_mother_softened.png
좋은 선택 반응. 박성빈은 숨을 고르며 고개를 끄덕이고, 이주형도 상담자가 자신을 문제로만 보지 않는다고 느껴 작은 목소리로 말을 잇기 시작한다. 네 명 모두 앉아 있고 방어가 조금 낮아진 분위기.

8. ft001_line_t01_reaction_b_child_withdrawn.png
나쁜 선택 반응. 이주형이 먼저 고쳐져야 하는 사람처럼 느껴 시선을 내리고 몸을 움츠린다. 박성빈은 실패한 보호자처럼 느끼며 굳어진다. 오선진은 불편하고, 서건창은 조심스러워진다.

9. ft001_line_t01_reaction_c_teacher_procedural.png
절차 중심 선택 반응. 서건창은 대답하지만, 가족의 정서는 더 닫힌다. 학교 서류/절차 분위기가 앞서고 박성빈과 이주형은 상담이 평가처럼 느껴진다.

10. ft001_line_t02_l01_mother_defensive.png
박성빈이 학교 전화가 오면 더 다그치게 되고, 그러면 주형이가 더 굳는다고 말한다. 방어적이지만 무너진 느낌. 이 장면부터 아침 순환을 함께 그려가는 분위기.

## Full 40-Image Backlog

Batch 01:
- ft001_line_t01_l01_mother_neutral.png
- ft001_line_t01_l02_child_anxious.png
- ft001_line_t01_l03_mother_worried.png
- ft001_line_t01_l04_child_quiet.png
- ft001_line_t01_l05_teacher_concerned.png
- ft001_line_t01_l06_supervisor_explaining.png
- ft001_line_t01_reaction_a_mother_softened.png
- ft001_line_t01_reaction_b_child_withdrawn.png
- ft001_line_t01_reaction_c_teacher_procedural.png
- ft001_line_t02_l01_mother_defensive.png

Batch 02:
- ft001_line_t02_l02_child_quiet.png
- ft001_line_t02_l03_mother_exhausted.png
- ft001_line_t02_l04_child_hesitant.png
- ft001_line_t02_l05_teacher_procedural.png
- ft001_line_t02_l06_supervisor_explaining.png
- ft001_line_t02_reaction_a_mother_softened.png
- ft001_line_t02_reaction_b_mother_defensive.png
- ft001_line_t02_reaction_c_child_withdrawn.png
- ft001_line_t03_l01_grandmother_critical.png
- ft001_line_t03_l02_mother_exhausted.png

Batch 03:
- ft001_line_t03_l03_grandmother_worried.png
- ft001_line_t03_l04_mother_tearful.png
- ft001_line_t03_l05_child_scared.png
- ft001_line_t03_l06_supervisor_questioning.png
- ft001_line_t03_reaction_a_grandmother_softened.png
- ft001_line_t03_reaction_b_grandmother_defensive.png
- ft001_line_t03_reaction_c_child_hesitant.png
- ft001_line_t04_l01_supervisor_questioning.png
- ft001_line_t04_l02_mother_worried.png
- ft001_line_t04_l03_child_quiet.png

Batch 04:
- ft001_line_t04_l04_mother_listening.png
- ft001_line_t04_l05_child_hesitant.png
- ft001_line_t04_l06_supervisor_explaining.png
- ft001_line_t04_reaction_a_supervisor_approving.png
- ft001_line_t04_reaction_b_mother_anxious.png
- ft001_line_t04_reaction_c_child_withdrawn.png
- ft001_line_t05_l01_teacher_concerned.png
- ft001_line_t05_l02_mother_softened.png
- ft001_line_t05_l03_child_relieved.png
- ft001_line_t05_l04_grandmother_softened.png

Batch 05:
- ft001_line_t05_l05_teacher_softened.png
- ft001_line_t05_l06_supervisor_reflective.png
- ft001_line_t05_reaction_a_mother_softened.png
- ft001_line_t05_reaction_b_child_scared.png
- ft001_line_t05_reaction_c_teacher_procedural.png

## Optional Choice Idle CGs

이 5장은 아직 만들지 않는다. 40장 완료 후 별도 승인받고 만든다.

- ft001_line_t01_choice_idle.png
- ft001_line_t02_choice_idle.png
- ft001_line_t03_choice_idle.png
- ft001_line_t04_choice_idle.png
- ft001_line_t05_choice_idle.png

## Batch 01 Contact Sheet

Batch 01의 10장을 만든 뒤 아래 파일로 컨택트시트를 저장한다.

C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity\Docs\GeneratedSources\FT001_LineByLineEventCG_20260610\contact_sheets\ft001_line_by_line_batch01_contact_sheet.png

컨택트시트에는 파일명 라벨을 넣어도 된다. 단, 실제 CG 이미지 파일 안에는 텍스트를 넣지 마라.
```

## Notes For Main Codex Window

- 이 작업은 source 생산 명령이다.
- 현재 `FamilyTherapyPracticumGame.cs`의 `Ft001CommercialCgName`은 여러 FT001 slug를 기존 commercial branching CG로 묶는다.
- line-by-line CG가 검수되면 메인 창에서 코드 매핑을 바꿔야 한다.
- 최종 런타임 파일명은 코드 반영 시 결정한다. 현재 옆 창 산출물은 `ft001_line_*.png` 기준으로 보관한다.
