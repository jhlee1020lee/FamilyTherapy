# FT-001 Cross-Window Sync Status

Last updated: 2026-06-10

This is the current coordination document for this Codex window and the separate image-generation window. If another document conflicts with this one, follow this document first.

## Current Goal

FT-001 is being rebuilt as a commercial-quality family therapy visual novel scene.

The current target is not a demo-only placeholder. The immediate production unit is one high-quality FT-001 route with expanded dialogue, meaningful branch differences, and a 30-image commercial CG pass that can later become the standard for the rest of the game.

## Source Of Truth Files

Use these files as the current base:

```text
Docs/CHARACTER_NAME_REGISTRY.md
Docs/FT001_MAJOR_BRANCHING_SCENARIO_2026-06-10.md
Docs/FT001_REALISTIC_DIALOGUE_EXPANSION_2026-06-10.md
Docs/FT001_COMMERCIAL_CG_GENERATION_COMMAND_AFTER_REFERENCES_2026-06-10.md
Docs/FT001_IMAGE_PIPELINE_COORDINATION_BRIEF_2026-06-10.md
```

Older image-plan files can still contain useful notes, but they are not the final authority when they conflict with the latest decisions here.

## Locked Character Decisions

| ID | Name | Current role | Locked notes |
| --- | --- | --- | --- |
| `ft001_mother` | 박성빈 | 어머니 | adult Korean mother, tired, pressured, trying to hold the morning routine together |
| `ft001_child` | 이주형 | 자녀 | elementary-school-age Korean boy, male, withdrawn/anxious, not a teenager |
| `ft001_grandmother` | 오선진 | 외조모 | older Korean woman, worried and firm |
| `ft001_teacher` | 서건창 | 담임 | adult Korean male teacher, school/procedural role, not family |
| `supervisor_system` | 김혜성 | 가족체계 기본 슈퍼바이저 | female supervisor/therapist, not male, not family, not teacher |

Important corrections:

- 김혜성 is female.
- 이주형 is the male child.
- 서건창 is the male teacher.
- 김혜성 must remain visually distinct from 서건창.
- 김혜성 must not be seated in the same row as the family/session participants.

## Latest 김혜성 Reference Override

Use this folder as the current Kim Hyesung visual reference set:

```text
Docs/GeneratedSources/FT001_KH_FemaleRef_20260610/
```

Key files:

```text
Docs/GeneratedSources/FT001_KH_FemaleRef_20260610/ref/kh_f_identity_1600x900.png
Docs/GeneratedSources/FT001_KH_FemaleRef_20260610/ref/kh_f_speaking_1600x900.png
Docs/GeneratedSources/FT001_KH_FemaleRef_20260610/ref/kh_f_opposite_seating_1600x900.png
Docs/GeneratedSources/FT001_KH_FemaleRef_20260610/kh_f_reference_contact_sheet_20260610.png
Docs/GeneratedSources/FT001_KH_FemaleRef_20260610/kh_f_reference_manifest.json
```

Use `kh_f_identity_1600x900.png` and `kh_f_speaking_1600x900.png` as the face, clothing, and role authority.

Use `kh_f_opposite_seating_1600x900.png` only as a seating/composition reference. It shows 김혜성 on the supervisor side across from the four session-side figures.

For 김혜성, the older files below are superseded and must not be used as identity references:

```text
Docs/GeneratedSources/FT001_ReferenceLocked_20260609/references/ft001_ref_supervisor_system_kim_hyesung_1600x900.png
Docs/GeneratedSources/FT001_ReferenceLocked_20260609/references/ft001_ref_supervisor_view_master_1600x900.png
Docs/GeneratedSources/FT001_ReferenceLocked_20260609/references/ft001_ref_supervisor_identity_1600x900.png
Docs/GeneratedSources/FT001_ReferenceLocked_20260609/references/ft001_ref_supervisor_expression_sheet_1600x900.png
```

## Core Blocking Issue

The image-generation window and this Codex window must not work from different assumptions.

The biggest current risk is generating CGs where 김혜성 appears as a male therapist, a teacher, or a fifth family/session participant. That is now wrong.

## Scene Geometry Lock

Family/session side:

```text
left: 오선진
center-left: 박성빈
center-right/front: 이주형
right: 서건창
camera: therapist/player viewpoint from across the room
```

김혜성 side:

```text
김혜성 sits across from the four family/session participants.
When 김혜성 speaks, the CG should show 김혜성 alone or 김혜성-centered.
The four seated figures do not need to appear in the same frame.
```

Do not force the four family/session participants into 김혜성 speaking CGs. The shot only needs to imply that 김혜성 is speaking from the opposite counselor/supervisor seat.

## Image Format Lock

Final in-game CGs:

```text
1600x900
16:9
no stretching
no text
no watermark
no UI baked into the image
bottom 25-30% visually clean for dialogue UI
```

The Unity game is currently intended to run at 1600x900 windowed. Full-scene CGs should be produced directly at 1600x900, not generated square and then stretched.

## Current Image Generation Plan

The active image-generation command file is:

```text
Docs/FT001_COMMERCIAL_CG_GENERATION_COMMAND_AFTER_REFERENCES_2026-06-10.md
```

Current target count:

```text
30 final CGs for FT-001
```

Planned final folder:

```text
Assets/Resources/VN/EventCG/FT001_CommercialBranching/
```

Planned source/archive folder:

```text
Docs/GeneratedSources/FT001_CommercialBranching_20260610/
```

Do not use the older minimum 13-image idea as the target. The current target is the commercial comfortable version: 30 images.

## Division Of Labor

Image-generation window:

1. Finish the visual references first.
2. Keep 김혜성 female and supervisor-only.
3. Use the 30-CG command file after references are stable.
4. Generate in small batches, not all 30 blindly.
5. Save selected final CGs under `Assets/Resources/VN/EventCG/FT001_CommercialBranching/`.
6. Save source candidates and rejected candidates under `Docs/GeneratedSources/FT001_CommercialBranching_20260610/`.
7. Tell this Codex window the final filenames when a batch is accepted.

This Codex window:

1. Maintains source-of-truth docs.
2. Checks for conflicting old assumptions.
3. Improves UI and runtime integration.
4. Maps final accepted CG filenames into `Assets/Scripts/FamilyTherapyPracticumGame.cs`.
5. Tests the game after assets are connected.
6. Prepares screenshots/attachment materials when needed.

## Unity Runtime State

Main script:

```text
Assets/Scripts/FamilyTherapyPracticumGame.cs
```

Current CG helper:

```text
VN/EventCG/FT001/ft001_cg_...
```

This means the new folder below is not automatically wired yet:

```text
VN/EventCG/FT001_CommercialBranching/...
```

After the image-generation window finishes accepted filenames, this Codex window must update the runtime mapping. Until then, copying images into `FT001_CommercialBranching` alone will not guarantee that the game uses them.

## Do Not Use These Older Assumptions

The following older assumptions are superseded:

- 김혜성 as adult Korean male therapist.
- 김혜성 speaking shots with the family visible whenever possible.
- 김혜성 in foreground with family necessarily visible over shoulder.
- one-line-one-CG 55 image pass as the current production target.
- minimum 13-image pass as the current production target.
- square character shots stretched into 16:9 game backgrounds.

## Immediate Next Steps

Image-generation window:

1. Confirm the latest reference set.
2. Confirm 김혜성 female supervisor reference.
3. Generate or select a 김혜성 solo/centered speaking reference.
4. Start the 30-CG plan in small batches from the command file.

This Codex window:

1. Keep this sync document updated whenever the user makes a decision.
2. Patch old docs only when they actively confuse the next step.
3. Improve UI without assuming final CGs are already available.
4. When accepted CG filenames arrive, wire them into Unity and test at 1600x900.

## Quick Reminder For Every Window

- 김혜성: female supervisor, solo/centered when speaking.
- 박성빈, 이주형, 오선진, 서건창: four seated family/session-side figures.
- Family-session CG: show all four seated figures when the scene is on their side.
- 김혜성 CG: do not force the four figures into the same frame.
- Final CG: 1600x900, no stretching, no baked UI, clean lower dialogue area.
- Current target: 30 commercial CGs for FT-001.
