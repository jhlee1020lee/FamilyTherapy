# FT-001 Commercial CG Generation Command After References

## 목적

레퍼런스 이미지가 승인된 뒤, FT-001의 큰 분기/현실 상담형 대사에 맞는 상용급 여유 버전 CG를 생성한다.

이 명령서는 이미지 생성 전용 창에 전달한다. 코드 수정은 하지 않는다.

## 반드시 먼저 확인할 레퍼런스

아래 레퍼런스가 완성되고 사용자가 승인한 뒤에만 본편 CG를 생성한다.

- `ft001_ref_cast_identity_sheet_1600x900.png`
- `ft001_ref_counseling_room_empty_1600x900.png`
- `ft001_ref_group_seating_master_1600x900.png`
- `Docs/GeneratedSources/FT001_KH_FemaleRef_20260610/ref/kh_f_identity_1600x900.png`
- `Docs/GeneratedSources/FT001_KH_FemaleRef_20260610/ref/kh_f_speaking_1600x900.png`
- `Docs/GeneratedSources/FT001_KH_FemaleRef_20260610/ref/kh_f_opposite_seating_1600x900.png`
- `Docs/GeneratedSources/FT001_KH_FemaleRef_20260610/kh_f_reference_contact_sheet_20260610.png`

주의:

- 김혜성은 여성 슈퍼바이저/치료자다.
- `kh_f_identity_1600x900.png`와 `kh_f_speaking_1600x900.png`를 김혜성 얼굴/복장/역할의 최우선 기준으로 사용한다.
- `kh_f_opposite_seating_1600x900.png`는 김혜성이 가족 맞은편 슈퍼바이저 좌석에 앉는 구도 참고용으로만 사용한다.
- 기존 `ft001_ref_supervisor_system_kim_hyesung_1600x900.png`, `ft001_ref_supervisor_view_master_1600x900.png`, `ft001_ref_supervisor_identity_1600x900.png`, `ft001_ref_supervisor_expression_sheet_1600x900.png`는 김혜성 identity reference로 사용하지 않는다.

레퍼런스가 아직 미완성이면 본편 CG를 만들지 말고 레퍼런스 완성/수정만 진행한다.

## 출력 위치와 파일 규칙

최종 출력 폴더:

```text
Assets/Resources/VN/EventCG/FT001_CommercialBranching/
```

소스/검토용 백업 폴더:

```text
Docs/GeneratedSources/FT001_CommercialBranching_20260610/
```

모든 최종 CG 규칙:

- 해상도 정확히 `1600x900`
- 텍스트, UI, 워터마크 금지
- 네 명의 FT-001 가족/기관 인물이 모든 가족 상담 CG에 앉아서 보여야 함
  - 박성빈: 어머니
  - 이주형: 남자 초등학생 자녀
  - 오선진: 외조모
  - 서건창: 남자 담임교사
- 김혜성 슈퍼바이저 CG는 김혜성이 상담자/슈퍼바이저로 명확히 등장해야 함
- 김혜성은 가족 구성원처럼 같은 줄에 앉히지 말 것
- 김혜성 발화 CG는 김혜성을 단독 또는 김혜성 중심 구도로 보여줄 것
- 가족 4명은 김혜성의 맞은편에 앉아 있는 설정이므로, 김혜성 발화 CG에서 가족 4명을 억지로 함께 보여주지 말 것
- 김혜성 컷은 상담자/슈퍼바이저가 가족을 바라보며 말하는 건너편 좌석 구도여야 함
- 상담실 구조, 의자 위치, 의상, 얼굴, 나이, 성별이 레퍼런스와 일치해야 함
- 하단 25-30%는 대사창이 올라갈 수 있게 시각적으로 깨끗하게 둘 것
- 모든 인물은 상담 중 앉아 있어야 하며, 서 있는 전신샷 금지

## 스타일 고정

사용할 스타일:

```text
polished commercial Korean visual novel CG, realistic painterly 2D, restrained emotional acting, warm counseling-room lighting, consistent character identity, cinematic but readable composition, no UI text, no caption, no watermark
```

금지:

```text
photorealistic uncanny AI portrait, standing character sprites, chibi, anime exaggeration, extreme fisheye, random extra people, cropped-out family members, text on image, UI frame, speech bubble, distorted hands, changing clothes, changing room layout
```

## 기준 대사 문서

CG는 아래 문서의 장면 흐름에 맞춘다.

```text
Docs/FT001_REALISTIC_DIALOGUE_EXPANSION_2026-06-10.md
Docs/FT001_MAJOR_BRANCHING_SCENARIO_2026-06-10.md
```

## 생성 수량

상용급 여유 버전 기준 총 **30장**을 생성한다.

구성:

- 공통 도입/초기 장면: 4장
- T1 선택 직후 반응: 3장
- T2 루트 장면: 4장
- T3 루트 장면: 5장
- T4 핵심 개입 장면: 5장
- T5 엔딩: 4장
- 슈퍼바이저/회고/선택 대기 보강: 4장
- 여분 QA 후보: 1장

## 30장 Shotlist

### A. 공통 도입/초기 장면 4장

1. `ft001_cb_001_intro_mother_pressure.png`
   - 박성빈이 아침 등교 장면을 말하며 지친 표정.
   - 가족 모두 앉아 있음. 박성빈이 중심 화자.

2. `ft001_cb_002_intro_child_anxiety.png`
   - 이주형이 엄마가 나갈 때의 불안을 말함.
   - 아이는 작게 움츠렸지만 얼굴이 보여야 함.

3. `ft001_cb_003_intro_grandmother_worry.png`
   - 오선진이 걱정과 잔소리 사이의 곤란함을 말함.
   - 외조모는 비판적이지만 악인처럼 보이면 안 됨.

4. `ft001_cb_004_intro_teacher_procedure.png`
   - 서건창이 학교 절차와 아이 걱정을 설명함.
   - 담임교사로 읽혀야 하며 가족 구성원처럼 보이면 안 됨.

### B. T1 선택 직후 반응 3장

5. `ft001_cb_005_t1_good_joining_reaction.png`
   - 좋은 선택 후 가족이 조금 숨을 고르는 장면.
   - 박성빈은 긴장이 풀리고, 이주형은 조심스럽게 말할 준비.

6. `ft001_cb_006_t1_bad_ip_fixed_reaction.png`
   - 나쁜 선택 후 이주형이 자신이 문제로 지목됐다고 느끼는 장면.
   - 아이는 닫히고, 박성빈은 더 무거운 표정.

7. `ft001_cb_007_t1_procedure_reaction.png`
   - 절차 중심 선택 후 서건창은 응하지만 가족 정서가 닫히는 장면.
   - 학교/서류의 압박이 시각적으로 느껴지되 실제 종이 텍스트는 넣지 말 것.

### C. T2 루트 장면 4장

8. `ft001_cb_008_t2_systemic_morning_map.png`
   - 아침 장면을 함께 순서대로 그려보는 루트.
   - 박성빈과 이주형이 서로를 보며 조금 열림.

9. `ft001_cb_009_t2_rupture_repair_apology.png`
   - 상담자가 초점을 좁힌 것을 인정한 뒤 가족이 조심스럽게 다시 열리는 장면.
   - 아이의 방어가 남아 있어야 함.

10. `ft001_cb_010_t2_institutional_loop.png`
   - 학교 연락이 가족 압박으로 들어오는 것을 다루는 장면.
   - 서건창이 절차를 설명하고 박성빈은 압박을 느낌.

11. `ft001_cb_011_t2_choice_idle_three_routes.png`
   - T2 선택지 대기용 중립 CG.
   - 네 명 모두 보이고, 표정은 조심스럽고 중립적.

### D. T3 루트 장면 5장

12. `ft001_cb_012_t3_open_grandmother_worry.png`
   - 외조모의 걱정과 어머니의 상처가 함께 드러나는 장면.
   - 오선진은 걱정스럽게 말하고, 박성빈은 상처받은 표정.

13. `ft001_cb_013_t3_fragile_rejoining.png`
   - 불안정하지만 다시 말이 이어지는 회복 루트.
   - 가족은 협조하지만 아직 몸이 경직됨.

14. `ft001_cb_014_t3_rupture_therapist_responsibility.png`
   - 상담자 책임 인정 장면.
   - 가족은 닫혀 있고 김혜성 또는 상담자 시점의 긴장감이 있음.

15. `ft001_cb_015_t3_compliance_without_feeling.png`
   - 절차는 정리되지만 정서가 빠져 있는 장면.
   - 서건창은 절차적으로 차분하고, 이주형은 작아짐.

16. `ft001_cb_016_t3_exception_child_speaks.png`
   - 이주형이 덜 힘들었던 아침 예외를 조심스럽게 말하는 장면.
   - 아이가 처음으로 조금 주도적으로 말함.

### E. T4 핵심 개입 장면 5장

17. `ft001_cb_017_t4_systemic_circular_question.png`
   - 핵심 순환질문 장면.
   - 상담 질문 뒤 가족이 서로의 반응을 보기 시작함.

18. `ft001_cb_018_t4_fragile_failure_rule.png`
   - 실패해도 다시 모이는 규칙을 만드는 장면.
   - 완벽한 해결보다 조심스러운 합의 분위기.

19. `ft001_cb_019_t4_rupture_repair_not_solution.png`
   - 해결보다 회복을 선택하는 장면.
   - 이주형은 여전히 조심스럽고, 상담실 분위기는 무겁지만 완전히 끊기지는 않음.

20. `ft001_cb_020_t4_institutional_pressure_reduced.png`
   - 학교 절차를 압박 낮추기로 바꾸는 장면.
   - 서건창이 가족을 향해 협력적으로 말함.

21. `ft001_cb_021_t4_bad_diagnostic_closure.png`
   - 성급한 진단/개인치료 제안으로 아이가 닫히는 장면.
   - 가족 전체의 거리가 다시 벌어짐.

### F. T5 엔딩 4장

22. `ft001_cb_022_ending_system_plan.png`
   - Ending A: 함께 보는 아침.
   - 네 사람 모두 작지만 구체적인 실험에 합의한 따뜻한 장면.

23. `ft001_cb_023_ending_fragile_agreement.png`
   - Ending B: 조심스러운 다음 아침.
   - 긴장은 남아 있지만 다시 시도할 수 있는 분위기.

24. `ft001_cb_024_ending_compliance.png`
   - Ending C: 정리된 서류, 남은 아침.
   - 겉으로는 정리됐지만 아이와 어머니는 편하지 않음.

25. `ft001_cb_025_ending_rupture.png`
   - Ending D: 닫힌 상담실.
   - 가족이 답답하게 닫히고, 다음 회기 회복이 필요해 보임.

### G. 슈퍼바이저/회고/선택 대기 보강 4장

26. `ft001_cb_026_supervisor_hyesung_opening.png`
   - 김혜성 슈퍼바이저가 첫 회기 방향을 설명.
   - 김혜성을 단독 또는 김혜성 중심으로 보여줌.
   - 가족 네 명은 카메라 반대편에 앉아 있는 설정이며, 이 컷에 억지로 포함하지 않음.

27. `ft001_cb_027_supervisor_hyesung_rupture_warning.png`
   - 김혜성이 동맹 파열을 경고하는 장면.
   - 따뜻하지만 단호한 표정.
   - 김혜성 단독/중심 구도. 가족 4명 동시 노출 금지.

28. `ft001_cb_028_supervisor_hyesung_final_review.png`
   - 김혜성이 회기 회고를 하는 장면.
   - 결과 분석/수련 피드백 느낌. 텍스트 없는 이미지.
   - 김혜성 단독/중심 구도. 상담자 책상 또는 건너편 좌석의 공간감만 암시.

29. `ft001_cb_029_choice_idle_tense.png`
   - 긴장된 선택 대기 화면용.
   - 하단 여백 깨끗하게, 네 명 모두 조용히 기다림.

### H. 여분 QA 후보 1장

30. `ft001_cb_030_spare_best_route_softening.png`
   - 좋은 루트에서 가족이 부드러워지는 예비 CG.
   - 정체성/표정/구도를 가장 잘 살린 후보로 생성.

## 각 이미지 프롬프트 작성 형식

각 파일마다 아래 형식으로 프롬프트를 작성하고 생성한다.

```text
Target file: [filename]
References: approved FT-001 cast identity sheet, counseling room empty reference, group seating master reference, supervisor references if 김혜성 appears.
Scene: [shotlist scene summary]
Characters: For family-session shots, show 박성빈 mother, 이주형 elementary-school-age boy, 오선진 maternal grandmother, 서건창 male homeroom teacher, all seated and visible. For 김혜성 supervisor shots, show 김혜성 alone or as the dominant centered speaker from the opposite counselor/supervisor seat; do not force the family of four into the same frame.
Emotion: [active emotional state]
Composition: 1600x900, therapist/player-side view, warm Korean counseling room, bottom 25-30% clean for VN dialogue UI.
Style: polished commercial Korean visual novel CG, realistic painterly 2D, restrained emotional acting, consistent reference-locked identity.
Negative: no text, no UI, no watermark, no standing characters, no extra people, no missing family members, no changed clothes, no changed room geometry, no distorted faces or hands.
```

## 품질 검수 기준

각 이미지 생성 후 아래 항목을 통과하지 못하면 재생성한다.

- 네 명 가족/기관 인물이 모두 보이는가?
- 이주형이 남자 초등학생으로 보이는가?
- 서건창이 담임교사로 보이고 가족 구성원처럼 보이지 않는가?
- 오선진과 박성빈이 나이/역할이 확실히 구분되는가?
- 박성빈, 이주형, 오선진, 서건창의 얼굴과 옷이 레퍼런스와 일치하는가?
- 김혜성 장면에서 김혜성이 담임이나 가족 구성원처럼 보이지 않는가?
- 김혜성 발화 장면에서 김혜성이 단독/중심 구도로 보이고, 가족 4명이 같은 줄이나 배경 군중처럼 억지로 들어가지 않았는가?
- 모두 앉아 있는가?
- 하단 대사창 영역이 깨끗한가?
- 이미지 비율과 해상도가 1600x900인가?
- 텍스트, UI, 워터마크가 없는가?

## 작업 순서

1. 레퍼런스 승인 상태를 다시 확인한다.
2. 1-4번 공통 도입 이미지를 먼저 만든다.
3. 사용자가 스타일/정체성/구도 승인 후 5-11번을 만든다.
4. 다시 검수 후 12-21번을 만든다.
5. 다시 검수 후 22-30번을 만든다.
6. 최종 파일을 `Assets/Resources/VN/EventCG/FT001_CommercialBranching/`에 넣고, 원본/후보는 `Docs/GeneratedSources/FT001_CommercialBranching_20260610/`에 보관한다.

## 주의

사용자가 현재 레퍼런스 창에서 만들고 있는 이미지가 최종 기준이다. 이 명령서의 인물 묘사보다 승인된 레퍼런스의 얼굴, 의상, 좌석 배치를 우선한다.

한 번에 30장을 다 뽑지 말고, 반드시 묶음 단위로 검수한다.
