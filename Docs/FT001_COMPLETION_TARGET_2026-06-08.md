# FT001 Completion Target 2026-06-08

## Summary

FT-001 will be completed before any other family case, supervisor set, background expansion, or bulk asset generation continues. Existing quarantined FT001 placeholder filenames are used only as a naming and coverage reference, not as final art.

## Final Sprite Target

Target count: 26 FT001 character sprites.

### Mother

- `ft001_mother_neutral`
- `ft001_mother_worried`
- `ft001_mother_anxious`
- `ft001_mother_exhausted`
- `ft001_mother_defensive`
- `ft001_mother_tearful`
- `ft001_mother_listening`
- `ft001_mother_softened`

### Child

- `ft001_child_neutral`
- `ft001_child_anxious`
- `ft001_child_hesitant`
- `ft001_child_listening`
- `ft001_child_quiet`
- `ft001_child_relieved`
- `ft001_child_scared`
- `ft001_child_withdrawn`

### Grandmother

- `ft001_grandmother_neutral`
- `ft001_grandmother_worried`
- `ft001_grandmother_critical`
- `ft001_grandmother_defensive`
- `ft001_grandmother_stubborn`
- `ft001_grandmother_softened`

### Teacher

- `ft001_teacher_neutral`
- `ft001_teacher_concerned`
- `ft001_teacher_procedural`
- `ft001_teacher_softened`

## Current Accepted Candidates

- `ft001_mother_neutral_phase0.png`
- `ft001_mother_worried_phase1.png`
- `ft001_mother_angry_contained_phase1.png`
- `ft001_child_neutral_phase1.png`
- `ft001_grandmother_neutral_phase1.png`

`ft001_mother_angry_contained_phase1.png` is accepted as a useful anger/defensiveness candidate, but it still needs review against the final `defensive` target before final naming cleanup.

## Completion Rules

- Generate new images with `image_gen`; do not count local pixel edits, color changes, or placeholder variants.
- Use transparent PNG character sprites under `Assets/Resources/VN/Characters/FT001`.
- Each PNG must have a corresponding `.png.meta`.
- Every expression must be visibly distinct at contact-sheet scale.
- Character identity must remain consistent within a role, while mother, child, grandmother, and teacher must remain clearly distinct from each other.
- Stop at reviewable batches and update a review sheet/log after each batch.
