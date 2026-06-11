# FT-001 Reference-Locked 1600x900 CG Generation Plan

## Summary

The earlier FT-001 seated all-cast pass produced a good visual direction, but independent generation caused identity and room drift. This plan replaces that workflow with a reference-locked pipeline:

1. create and approve fixed character/background references;
2. generate every FT-001 gameplay CG from those references;
3. lock all final full-scene CGs to `1600x900`;
4. use transparent seated sprites only as secondary UI/fallback assets.

The current generated group plates and seated mother sprites are candidates only. They are not final approval evidence for the new pass.

## Output Rules

- Final full-scene CG size: exactly `1600x900`.
- Final gameplay CG folder: `Assets/Resources/VN/EventCG/FT001/`.
- Source/archive folder: `Docs/GeneratedSources/FT001_ReferenceLocked_20260609/`.
- All gameplay CGs must show the four FT-001 family/session participants seated in the counseling room:
  - `ft001_mother` / 박성빈 / adult Korean mother
  - `ft001_child` / 이주형 / elementary-school-age Korean boy
  - `ft001_grandmother` / 오선진 / older Korean maternal grandmother
  - `ft001_teacher` / 서건창 / adult Korean male homeroom teacher
- `supervisor_system` / 김혜성 / family systems supervisor is also a required reference character.
- Family-member dialogue CGs should keep the current four-person counseling-room framing: 박성빈, 이주형, 오선진, 서건창 seated together, viewed from the therapist/player side.
- 김혜성 dialogue CGs must show 김혜성 on screen as the speaking professional. He should be positioned as counselor/supervisor looking toward the seated family of four, not as a fifth family participant in the same row.
- In 김혜성 CGs, keep the family of four visible when composition allows, either across from him, over his shoulder, or in the background, so the shot still reads as the same therapy session.
- No gameplay CG may show standing characters, missing characters, random extra characters, text, UI, or watermarks.
- Keep the lower 25-30% visually clean for the VN dialogue box.

## Reference Pack First

Generate and review these before bulk generation:

- `ft001_ref_cast_identity_sheet_1600x900.png`
  - four-character identity sheet, front-facing seated bust/knee-up studies, clear age/role/gender separation.
- `ft001_ref_counseling_room_empty_1600x900.png`
  - empty Korean counseling-room background, warm wood, muted teal chairs, therapy-session layout.
- `ft001_ref_group_seating_master_1600x900.png`
  - master all-cast seating layout: grandmother left, mother center-left, child center-right/front, teacher right, therapist/player viewpoint from across the room.
- `ft001_ref_supervisor_system_kim_hyesung_1600x900.png`
  - 김혜성 identity reference: Korean family systems supervisor/counselor, professional but warm, visually distinct from 서건창, designed to face and guide the seated family of four.
- `ft001_ref_supervisor_view_master_1600x900.png`
  - 김혜성 shot-composition reference: 김혜성 in foreground or side profile addressing the seated family of four across the room; family remains visible, but 김혜성 is clearly the active speaker.
- Optional close-up references if identity drift remains visible:
  - `ft001_ref_mother_identity_1600x900.png`
  - `ft001_ref_child_identity_1600x900.png`
  - `ft001_ref_grandmother_identity_1600x900.png`
  - `ft001_ref_teacher_identity_1600x900.png`
  - `ft001_ref_supervisor_identity_1600x900.png`

Reference QA must pass before generating the full shotlist.

## Reference Pack Completion Status

As of 2026-06-10, the approved reference folder contains 18 normalized `1600x900` references and one contact sheet:

- `ft001_ref_cast_identity_sheet_1600x900.png`
- `ft001_ref_counseling_room_empty_1600x900.png`
- `ft001_ref_group_seating_master_1600x900.png`
- `ft001_ref_supervisor_system_kim_hyesung_1600x900.png`
- `ft001_ref_supervisor_view_master_1600x900.png`
- `ft001_ref_mother_identity_1600x900.png`
- `ft001_ref_child_identity_1600x900.png`
- `ft001_ref_grandmother_identity_1600x900.png`
- `ft001_ref_teacher_identity_1600x900.png`
- `ft001_ref_supervisor_identity_1600x900.png`
- `ft001_ref_mother_expression_sheet_1600x900.png`
- `ft001_ref_child_expression_sheet_1600x900.png`
- `ft001_ref_grandmother_expression_sheet_1600x900.png`
- `ft001_ref_teacher_expression_sheet_1600x900.png`
- `ft001_ref_supervisor_expression_sheet_1600x900.png`
- `ft001_ref_mother_speaking_master_1600x900.png`
- `ft001_ref_child_speaking_master_1600x900.png`
- `ft001_ref_softening_master_1600x900.png`

Contact sheet:

- `Docs/GeneratedSources/FT001_ReferenceLocked_20260609/ft001_reference_pack_contact_sheet_20260610.png`

The earlier difficult/tension master prompt was intentionally not kept as a separate reference because it triggered generation failures. Use restrained active-speaker and expression references instead, and phrase difficult moments clinically without intensifying child distress language.

## Gameplay Shotlist

Generate one 1600x900 all-cast CG per visible FT-001 dialogue/reaction state.

### Intro

- `ft001_cg_intro_01_mother_neutral.png`
- `ft001_cg_intro_02_child_neutral.png`
- `ft001_cg_intro_03_grandmother_neutral.png`
- `ft001_cg_intro_04_teacher_neutral.png`
- `ft001_cg_intro_05_supervisor_explaining.png`

### Turn 01

- `ft001_cg_t01_l01_mother_neutral.png`
- `ft001_cg_t01_l02_child_anxious.png`
- `ft001_cg_t01_l03_mother_worried.png`
- `ft001_cg_t01_l04_child_quiet.png`
- `ft001_cg_t01_l05_teacher_concerned.png`
- `ft001_cg_t01_l06_supervisor_explaining.png`
- `ft001_cg_t01_choice_idle.png`
- `ft001_cg_t01_reaction_a_mother_softened.png`
- `ft001_cg_t01_reaction_b_child_withdrawn.png`
- `ft001_cg_t01_reaction_c_teacher_procedural.png`

### Turn 02

- `ft001_cg_t02_l01_mother_defensive.png`
- `ft001_cg_t02_l02_child_quiet.png`
- `ft001_cg_t02_l03_mother_exhausted.png`
- `ft001_cg_t02_l04_child_hesitant.png`
- `ft001_cg_t02_l05_teacher_procedural.png`
- `ft001_cg_t02_l06_supervisor_explaining.png`
- `ft001_cg_t02_choice_idle.png`
- `ft001_cg_t02_reaction_a_mother_softened.png`
- `ft001_cg_t02_reaction_b_mother_defensive.png`
- `ft001_cg_t02_reaction_c_child_withdrawn.png`

### Turn 03

- `ft001_cg_t03_l01_grandmother_critical.png`
- `ft001_cg_t03_l02_mother_exhausted.png`
- `ft001_cg_t03_l03_grandmother_worried.png`
- `ft001_cg_t03_l04_mother_tearful.png`
- `ft001_cg_t03_l05_child_scared.png`
- `ft001_cg_t03_l06_supervisor_questioning.png`
- `ft001_cg_t03_choice_idle.png`
- `ft001_cg_t03_reaction_a_grandmother_softened.png`
- `ft001_cg_t03_reaction_b_grandmother_defensive.png`
- `ft001_cg_t03_reaction_c_child_hesitant.png`

### Turn 04

- `ft001_cg_t04_l01_supervisor_questioning.png`
- `ft001_cg_t04_l02_mother_worried.png`
- `ft001_cg_t04_l03_child_quiet.png`
- `ft001_cg_t04_l04_mother_listening.png`
- `ft001_cg_t04_l05_child_hesitant.png`
- `ft001_cg_t04_l06_supervisor_explaining.png`
- `ft001_cg_t04_choice_idle.png`
- `ft001_cg_t04_reaction_a_supervisor_approving.png`
- `ft001_cg_t04_reaction_b_mother_anxious.png`
- `ft001_cg_t04_reaction_c_child_withdrawn.png`

### Turn 05

- `ft001_cg_t05_l01_teacher_concerned.png`
- `ft001_cg_t05_l02_mother_softened.png`
- `ft001_cg_t05_l03_child_relieved.png`
- `ft001_cg_t05_l04_grandmother_softened.png`
- `ft001_cg_t05_l05_teacher_softened.png`
- `ft001_cg_t05_l06_supervisor_reflective.png`
- `ft001_cg_t05_choice_idle.png`
- `ft001_cg_t05_reaction_a_mother_softened.png`
- `ft001_cg_t05_reaction_b_child_scared.png`
- `ft001_cg_t05_reaction_c_teacher_procedural.png`

## Prompting Contract

Every generated gameplay CG prompt must include:

- the three approved reference roles: cast identity, empty room, group seating;
- exact filename target;
- speaker and expression from the shotlist;
- the matching Korean dialogue or reaction summary;
- the fixed seating arrangement;
- “all four are seated and visible” as a hard constraint;
- for 김혜성/supervisor lines: “김혜성 is visible as the speaking counselor/supervisor facing the seated family of four; he is not seated as a fifth family member” as a hard constraint;
- “leave bottom 25-30% clean for dialogue UI” as a hard constraint;
- “same identities, same clothing, same room geometry as references” as a hard constraint.

## QA Gates

Reject/regenerate if:

- any of the four required characters is missing;
- anyone is standing or appears as a floating cutout;
- child is too small, hidden, or gender-ambiguous;
- teacher reads as a family member instead of school representative;
- 김혜성 reads as a family member or teacher instead of the counselor/supervisor;
- 김혜성 speaking shots omit 김혜성 or place him in the same participant row as the family;
- grandmother and mother look like the same person;
- faces, clothes, room layout, or camera drift from the approved references;
- active speaker does not match the dialogue/reaction state;
- bottom dialogue area is crowded;
- size is not exactly `1600x900`;
- image includes text, UI, watermark, or extra characters.

## Runtime Integration

Unity should prefer explicit CG resource paths on FT-001 dialogue lines and choice reactions. If a line-specific CG is missing, runtime should fall back to the older speaker/expression group plate mapping so the current build remains usable during incremental generation.

Transparent seated sprites remain useful as secondary assets for briefing, character cards, and fallback composition, but they are not the primary FT-001 gameplay presentation.
