# Asset Review Apply Decisions 2026-06-09

## Scope

검수 대상은 옆 창에서 생성/교체한 FT-001 가족 캐릭터 이미지와, 이 창에서 만든 UI 스킨 이미지다.

- 캐릭터: `Assets/Resources/VN/Characters/FT001/*.png`
- UI: `Assets/Resources/VN/UI/*.png`
- 검수용 접촉 시트:
  - `Docs/ASSET_REVIEW_FT001_RUNTIME_CONTACT_SHEET_2026-06-09.png`
  - `Docs/ASSET_REVIEW_UI_RUNTIME_CONTACT_SHEET_2026-06-09.png`
- 이미지 치수/디코딩 검증:
  - `Docs/asset_review_runtime_image_dimensions_2026-06-09.json`

## Applied

아래 파일들은 현재 런타임 리소스로 적용한다. 파일명은 `FamilyTherapyPracticumGame.cs`의 `RequiredVnAssetPaths` 및 VN 대사 스크립트의 expression id와 맞는다.

### 2026-06-09 Runtime Correction

사용자 확인 후 `이주형`과 `서건창`은 남성 캐릭터여야 함을 재확인했다. 기존 `ft001_child_*`, `ft001_teacher_*` 후보는 접촉 시트와 런타임 화면에서 여성처럼 보여 최종 적용 판정을 취소한다.

긴급 교정으로 아래 남성 neutral 스프라이트를 추가하고, 런타임 기본 에셋 경로를 이 파일들로 우선 연결한다.

- `ft001_child_male_neutral_phase1.png`
- `ft001_teacher_male_neutral_phase1.png`

최종 작업에서는 `ft001_child_<expression>_phase1.png` 8장 전체와 `ft001_teacher_<expression>_phase1.png` 4장 전체를 남자 아이/남자 담임교사로 다시 생성해 교체해야 한다.

### FT-001 Mother

- `ft001_mother_neutral_phase0.png`
- `ft001_mother_worried_phase1.png`
- `ft001_mother_anxious_phase1.png`
- `ft001_mother_exhausted_phase1.png`
- `ft001_mother_defensive_phase1.png`
- `ft001_mother_tearful_phase1.png`
- `ft001_mother_listening_phase1.png`
- `ft001_mother_softened_phase1.png`

판정: 적용. 전신 구도, 의상, 배경 톤이 대체로 유지되고, 소진/방어/경청/완화 흐름을 구분할 수 있다.

### FT-001 Child

- `ft001_child_neutral_phase1.png`
- `ft001_child_anxious_phase1.png`
- `ft001_child_hesitant_phase1.png`
- `ft001_child_listening_phase1.png`
- `ft001_child_quiet_phase1.png`
- `ft001_child_relieved_phase1.png`
- `ft001_child_scared_phase1.png`
- `ft001_child_withdrawn_phase1.png`

판정: 최종 적용 취소. 표정 기능은 있었지만 성별이 잘못 읽힌다. 현재 런타임은 `ft001_child_male_neutral_phase1.png`를 임시 기본 컷으로 사용한다.

### FT-001 Grandmother

- `ft001_grandmother_neutral_phase1.png`
- `ft001_grandmother_worried_phase1.png`
- `ft001_grandmother_critical_phase1.png`
- `ft001_grandmother_defensive_phase1.png`
- `ft001_grandmother_stubborn_phase1.png`
- `ft001_grandmother_softened_phase1.png`

판정: 적용. 외조모 역할과 정서 변화가 읽히며, 비판/방어/완화 반응을 현재 회기 흐름에 사용할 수 있다.

### FT-001 Teacher

- `ft001_teacher_neutral_phase1.png`
- `ft001_teacher_concerned_phase1.png`
- `ft001_teacher_procedural_phase1.png`
- `ft001_teacher_softened_phase1.png`

판정: 최종 적용 취소. 교사 역할은 읽히지만 성별이 잘못 읽힌다. 현재 런타임은 `ft001_teacher_male_neutral_phase1.png`를 임시 기본 컷으로 사용한다.

### Runtime UI Skins

- `dialogue_box.png`
- `speaker_nameplate.png`
- `choice_card_question.png`
- `choice_card_intervention.png`
- `supervisor_note_panel.png`
- `case_file_panel.png`
- `metrics_hud.png`
- `session_result_sheet.png`

판정: 적용. 텍스트 없는 상담 파일풍 UI로 현재 Unity 텍스트와 충돌하지 않는다. 최신 visual audit에서 전체 text overflow와 offscreen rect가 0으로 확인됐다.

## Not Applied / Superseded

### `ft001_mother_angry_contained_phase1.png`

판정: 미적용. `ft001_mother_defensive_phase1.png`가 같은 기능을 더 명확하게 대체하므로 런타임에서 제외한다.

이유:

- 현재 스크립트는 `defensive` expression id를 직접 사용한다.
- `angry_contained`는 최종 expression target에 포함되지 않는다.
- 분노 표현은 보호자 비난으로 과하게 읽힐 수 있어 가족치료 회기 톤에는 `defensive`가 더 적합하다.

## Applied But Needs Later Upgrade

아래 항목은 지금 빌드에는 적용하지만, 상용 출시급 최종 아트로는 후속 리터치 또는 재생성이 필요하다.

### Child Set

- 기존 child 세트는 성별 오류로 최종 재생성이 필요하다.
- `anxious`, `scared`, `quiet`는 감정 방향이 비슷해 작은 크기에서는 일부 겹쳐 보일 수 있다.
- `withdrawn`은 포즈가 강해서 좋지만, 같은 아이의 체형/얼굴 비율이 다른 컷보다 약간 작게 느껴진다.

개선 방향:

- 눈썹, 입 모양, 어깨 긴장도를 더 명확히 분리한다.
- 모든 child 컷의 키/머리 크기/신발 위치를 더 엄격히 맞춘다.

### Mother Set

- 전체 의상/정체성은 안정적이지만 `worried`, `listening`, `softened`의 차이는 접촉 시트 크기에서 다소 미묘하다.
- `tearful`과 `exhausted`는 손 위치와 얼굴 표정이 좋아 현재 적용 가능하지만, 더 큰 감정 차이를 줄 수 있다.

개선 방향:

- `defensive`는 팔/어깨 긴장, `listening`은 몸을 여는 자세, `softened`는 눈과 입의 완화가 더 분명하게 보이게 만든다.

### Grandmother Set

- `neutral`은 다른 외조모 expression 컷보다 cardigan 색감과 인상 톤이 조금 다르다.
- `critical`, `defensive`, `stubborn`은 의미가 구분되지만 모두 완고한 방향이라 작은 화면에서 겹칠 수 있다.

개선 방향:

- 외조모 기본 의상 색과 머리 실루엣을 전 세트에서 더 맞춘다.
- `worried`는 눈썹/손 모양, `softened`는 미소/어깨 이완으로 더 구분한다.

### Teacher Set

- 기존 teacher 세트는 성별 오류로 최종 재생성이 필요하다.
- `neutral`, `concerned`, `softened`는 좋은 기본 세트지만 표정 차이가 약하다.
- `procedural`은 소품 덕분에 명확하므로 유지 가치가 높다.

개선 방향:

- `concerned`는 눈썹과 입술 긴장, `softened`는 미소와 몸의 이완을 더 키운다.
- 교사 세트도 캐릭터 키와 얼굴 크기를 더 엄격히 통일한다.

### UI Set

- 현재 UI는 기능 검증용 1차 런타임 스킨으로는 통과한다.
- 상용 출시급 최종 UI로는 질감 밀도, 코너 디테일, hover/pressed 상태, 아이콘 세트, 9-slice 전용 margin 설계가 더 필요하다.

개선 방향:

- 각 버튼 상태별 normal/hover/pressed/disabled 이미지 추가.
- `speaker_nameplate`의 외곽 glow를 줄여 캐릭터/대화창보다 튀지 않게 조정.
- `metrics_hud`는 너무 얇기 때문에 실제 정보량 증가 시 별도 확장형 HUD 스킨이 필요하다.

## Verification Summary

- PNG decode/dimension check: 34/34 passed.
- Runtime character PNG count: 26.
- Runtime UI PNG count: 8.
- Unity visual audit: 전체 `textOverflowCount=0`, 전체 `offscreenRectCount=0`.
- Required VN target coverage: FT-001 character target 26/26 present.

## Decision

현재 빌드에는 FT-001 캐릭터 26장과 UI 스킨 8종을 모두 적용한다. 별로인 항목은 즉시 제외할 정도의 실패작은 아니며, “적용하되 상용 출시 전 리터치/재생성 필요”로 관리한다. 명확히 미적용할 항목은 `ft001_mother_angry_contained_phase1.png` 하나다.
