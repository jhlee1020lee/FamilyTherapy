# FT-001 Seated All-Cast Image Generation Command

> Superseded on 2026-06-09 by `FT001_REFERENCE_LOCKED_CG_GENERATION_PLAN_2026-06-09.md`.
> The visual direction remains valid, but final production should use the reference-locked `1600x900` CG pipeline instead of independent 7-plate/26-sprite generation.

## Purpose

Generate only the FT-001 assets for `Family Therapy Practicum`.

The current standing full-body character sprites look awkward in counseling scenes because the family appears to be standing during therapy. Replace the FT-001 presentation with seated counseling-room assets.

Most important rule:

**All FT-001 main characters must be visually present in every gameplay scene.**

This is not a one-speaker-at-a-time VN. The scene should feel like a family therapy session where everyone is in the room together, seated, listening, reacting, and participating. The active speaker can be emphasized later in Unity through lighting, opacity, focus, or expression, but the other family members should remain visible.

## Characters

Use these names and roles exactly.

| ID | Name | Role | Gender/Presentation |
| --- | --- | --- | --- |
| `ft001_mother` | 박성빈 | mother | adult woman |
| `ft001_child` | 이주형 | child | elementary-school-age boy |
| `ft001_grandmother` | 오선진 | maternal grandmother | older woman |
| `ft001_teacher` | 서건창 | homeroom teacher | adult man |

Do not change gender, age category, or role.

## Core Visual Direction

Style:

- polished commercial Korean visual novel
- serious counseling simulation
- soft painterly 2D illustration
- realistic but not photographic
- emotionally restrained
- consistent face identity across all expressions
- same art style as the current accepted image style
- warm Korean counseling center mood

Do not create:

- standing full-body sprites
- floating full-body cutouts
- characters standing in the counseling room
- dramatic anime action poses
- exaggerated comic expressions
- sitcom-like family portrait posing
- Western office/corporate therapy style
- random character redesigns between expressions

## Scene Composition Rule

For every full scene or event CG:

- 박성빈, 이주형, 오선진, 서건창 must all appear.
- They should be seated in a counseling room.
- The arrangement should make sense for family therapy:
  - mother and child close enough to read as a family unit
  - grandmother slightly adjacent, involved but creating tension
  - teacher slightly to the side as an external system/school representative
  - therapist/player viewpoint implied from camera position, not shown
- Everyone should be visible above the dialogue box area.
- Leave the bottom 25-30% of the image visually clean enough for VN dialogue UI.
- Do not crop faces or hide a character behind UI space.

Recommended layout:

```text
left: grandmother
center-left: mother
center-right/front: child
right: teacher
camera: therapist/player view from across the room
```

The child may sit slightly forward or lower because he is younger, but he must not disappear behind furniture.

## Asset Strategy

Create two types of assets.

### A. Group Scene Plates

These are full 1920x1080 scene images with all four characters seated together in the counseling room.

Use these for the main FT-001 gameplay scenes.

Required group plates:

1. `ft001_group_session_neutral.png`
   - all four seated, tense but calm first-session atmosphere
   - no one speaking strongly

2. `ft001_group_session_mother_speaking.png`
   - 박성빈 is visually emphasized
   - others remain visible and listening

3. `ft001_group_session_child_speaking.png`
   - 이주형 is visually emphasized
   - others remain visible and listening

4. `ft001_group_session_grandmother_speaking.png`
   - 오선진 is visually emphasized
   - others remain visible and reacting

5. `ft001_group_session_teacher_speaking.png`
   - 서건창 is visually emphasized
   - others remain visible and listening

6. `ft001_group_session_tension.png`
   - family tension is higher
   - mother looks exhausted/defensive
   - child looks withdrawn/anxious
   - grandmother looks worried but critical
   - teacher looks procedurally concerned

7. `ft001_group_session_softening.png`
   - later-session softening
   - mother and child show slight emotional safety
   - grandmother less critical
   - teacher more cooperative

### B. Seated Transparent Character Sprites

These are transparent PNGs for Unity composition.

Each sprite should be seated half-body or seated knee-up, not standing. Do not include a large visible chair unless it is necessary to make the seated posture readable.

Canvas:

- transparent background PNG
- minimum height 1400 px
- consistent scale across the four characters
- character should fit naturally into a seated counseling-room composition
- no background
- no text
- no watermark

Required expressions:

#### 박성빈 / mother

1. `ft001_mother_seated_neutral_phase2.png`
2. `ft001_mother_seated_anxious_phase2.png`
3. `ft001_mother_seated_defensive_phase2.png`
4. `ft001_mother_seated_exhausted_phase2.png`
5. `ft001_mother_seated_listening_phase2.png`
6. `ft001_mother_seated_softened_phase2.png`
7. `ft001_mother_seated_tearful_phase2.png`
8. `ft001_mother_seated_worried_phase2.png`

#### 이주형 / child

1. `ft001_child_seated_neutral_phase2.png`
2. `ft001_child_seated_anxious_phase2.png`
3. `ft001_child_seated_hesitant_phase2.png`
4. `ft001_child_seated_listening_phase2.png`
5. `ft001_child_seated_quiet_phase2.png`
6. `ft001_child_seated_relieved_phase2.png`
7. `ft001_child_seated_scared_phase2.png`
8. `ft001_child_seated_withdrawn_phase2.png`

#### 오선진 / grandmother

1. `ft001_grandmother_seated_neutral_phase2.png`
2. `ft001_grandmother_seated_critical_phase2.png`
3. `ft001_grandmother_seated_defensive_phase2.png`
4. `ft001_grandmother_seated_softened_phase2.png`
5. `ft001_grandmother_seated_stubborn_phase2.png`
6. `ft001_grandmother_seated_worried_phase2.png`

#### 서건창 / teacher

1. `ft001_teacher_seated_neutral_phase2.png`
2. `ft001_teacher_seated_concerned_phase2.png`
3. `ft001_teacher_seated_procedural_phase2.png`
4. `ft001_teacher_seated_softened_phase2.png`

## Count

Minimum required for this FT-001 seated replacement pass:

- 7 group scene plates
- 26 seated transparent sprites
- total: 33 images

Do not generate other cases.
Do not generate other supervisors.
Do not generate UI.
Do not generate random extra characters.

## Prompt Template For Group Scene Plates

Use this structure and adapt the active speaker/emotional state.

```text
Create a 1920x1080 polished commercial Korean visual novel scene for a serious family therapy counseling simulation.

Scene: Korean counseling center therapy room, warm wood, muted teal accents, comfortable chairs arranged for family therapy, realistic calm lighting.

All four FT-001 characters must appear in the scene, seated and visible:
- 박성빈, adult Korean mother, exhausted but trying to stay composed
- 이주형, elementary-school-age Korean boy, anxious and quiet
- 오선진, older Korean maternal grandmother, worried but sometimes critical
- 서건창, adult Korean male homeroom teacher, procedural and concerned

Composition: therapist/player viewpoint from across the room. All four characters are seated. No one is standing. Leave the bottom 25-30% visually clean for a VN dialogue box. No text, no UI, no watermark.

Active focus: [ACTIVE_CHARACTER_AND_EMOTION]

Style: serious 2D visual novel illustration, soft painterly rendering, emotionally restrained, consistent character identities, not photorealistic, not chibi, not exaggerated anime.
```

## Prompt Template For Seated Transparent Sprites

Use this structure for each character/expression.

```text
Create a transparent-background PNG character sprite for a serious Korean family therapy visual novel.

Character: [NAME_AND_ROLE]
Expression/emotion: [EXPRESSION]

Pose: seated half-body or seated knee-up pose, natural counseling-room posture, hands and shoulders readable, not standing, not full-body standing, not floating. The character should look like they are sitting in a therapy session and listening or speaking.

Style: polished commercial Korean visual novel, soft painterly 2D, emotionally restrained, realistic proportions, consistent identity with the other FT-001 assets.

Technical: transparent background, no chair-dominant background, no text, no watermark, minimum character height 1400 px.
```

## Quality Check

Reject and regenerate if:

- any full scene does not include all four characters
- a character is standing
- the child is hidden, tiny, or blocked
- the teacher looks like a family member rather than a school representative
- grandmother looks like the mother
- mother looks too young or too old
- expression differences are too subtle to read
- bottom dialogue area is visually crowded
- image contains text or UI
- transparent sprite has a background
- face identity changes across expressions
