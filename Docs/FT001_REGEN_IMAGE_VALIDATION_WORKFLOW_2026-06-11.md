# FT001 Regen Image Validation Workflow

Date: 2026-06-11

Scope: validate regenerated FT001 line-by-line Event CG PNGs placed under:

`Assets/Resources/VN/EventCG/FT001_LineByLineLocked_Regen_20260611`

Tool:

`Tools/ValidateFt001RegenImages.py`

## What the Script Checks

The script is intentionally strict because the FT001 regeneration risk is duplicate images, weak cut-to-cut continuity, and unstable camera framing.

It checks:

1. Exactly 67 PNG files are present.
2. The 67 expected filenames are all present.
3. No extra PNG files are present.
4. Every readable expected PNG is exactly `1600x900`.
5. SHA256 hashes have no exact duplicates.
6. A simple 64-bit DCT perceptual hash flags near-duplicate image pairs.
7. A contact sheet is generated for manual camera/framing review.
8. A JSON report is generated for audit and handoff.

The perceptual hash check is a near-duplicate smoke test, not a full art-direction judgment. Camera stability, cast continuity, pose readability, and whether each cut visibly reflects the intended story beat still need contact-sheet review.

## Prerequisite

The script requires Python 3 and Pillow:

```powershell
python -m pip install Pillow
```

## Standard Run

Run from the Unity project root:

```powershell
cd C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity
python Tools\ValidateFt001RegenImages.py
```

Default outputs are timestamped under:

`Docs/GeneratedSources/FT001_RegenValidation_20260611`

The process exit code is:

- `0`: pass
- `1`: validation failed
- `2`: tool/runtime usage error, such as refusing to overwrite an explicit output path

## Stable CI or Handoff Output

Use explicit paths when another process needs stable filenames:

```powershell
python Tools\ValidateFt001RegenImages.py `
  --report Docs\GeneratedSources\FT001_RegenValidation_20260611\latest_report.json `
  --contact-sheet Docs\GeneratedSources\FT001_RegenValidation_20260611\latest_contact_sheet.png `
  --overwrite
```

## Similarity Threshold

Default pHash threshold:

```text
Hamming distance <= 4
```

Lower values catch only very close repeats. Higher values catch more suspicious pairs but can include legitimate cuts in the same room with the same cast.

For a stricter duplicate sweep:

```powershell
python Tools\ValidateFt001RegenImages.py --similarity-threshold 12
```

By default, pHash pairs are written to the JSON report but do not fail the run. This avoids rejecting good camera continuity just because all cuts share the same counseling room and cast. To make pHash hits fail the run:

```powershell
python Tools\ValidateFt001RegenImages.py --fail-on-similar
```

## Expected File Set

The expected set is 67 PNGs: 62 code-called intro/dialogue/reaction/branch slugs from `Ft001Line(...)` and `Ft001Choice(...)`, plus 5 `choice_idle` images.

```text
ft001_cg_t01_choice_idle.png
ft001_cg_t01_l01_mother_neutral.png
ft001_cg_t01_l02_child_anxious.png
ft001_cg_t01_l03_mother_worried.png
ft001_cg_t01_l04_child_quiet.png
ft001_cg_t01_l05_teacher_concerned.png
ft001_cg_t01_l06_supervisor_explaining.png
ft001_cg_t01_reaction_a_mother_softened.png
ft001_cg_t01_reaction_b_child_withdrawn.png
ft001_cg_t01_reaction_c_teacher_procedural.png
ft001_cg_t02_choice_idle.png
ft001_cg_t02_l01_mother_defensive.png
ft001_cg_t02_l02_child_quiet.png
ft001_cg_t02_l03_mother_exhausted.png
ft001_cg_t02_l04_child_hesitant.png
ft001_cg_t02_l05_teacher_procedural.png
ft001_cg_t02_l06_supervisor_explaining.png
ft001_cg_t02_reaction_a_mother_softened.png
ft001_cg_t02_reaction_b_mother_defensive.png
ft001_cg_t02_reaction_c_child_withdrawn.png
ft001_cg_t03_choice_idle.png
ft001_cg_t03_l01_grandmother_critical.png
ft001_cg_t03_l02_mother_exhausted.png
ft001_cg_t03_l03_grandmother_worried.png
ft001_cg_t03_l04_mother_tearful.png
ft001_cg_t03_l05_child_scared.png
ft001_cg_t03_l06_supervisor_questioning.png
ft001_cg_t03_reaction_a_grandmother_softened.png
ft001_cg_t03_reaction_b_grandmother_defensive.png
ft001_cg_t03_reaction_c_child_hesitant.png
ft001_cg_t04_choice_idle.png
ft001_cg_t04_l01_supervisor_questioning.png
ft001_cg_t04_l02_mother_worried.png
ft001_cg_t04_l03_child_quiet.png
ft001_cg_t04_l04_mother_listening.png
ft001_cg_t04_l05_child_hesitant.png
ft001_cg_t04_l06_supervisor_explaining.png
ft001_cg_t04_reaction_a_supervisor_approving.png
ft001_cg_t04_reaction_b_mother_anxious.png
ft001_cg_t04_reaction_c_child_withdrawn.png
ft001_cg_t05_choice_idle.png
ft001_cg_t05_l01_teacher_concerned.png
ft001_cg_t05_l02_mother_softened.png
ft001_cg_t05_l03_child_relieved.png
ft001_cg_t05_l04_grandmother_softened.png
ft001_cg_t05_l05_teacher_softened.png
ft001_cg_t05_l06_supervisor_reflective.png
ft001_cg_t05_reaction_a_mother_softened.png
ft001_cg_t05_reaction_b_child_scared.png
ft001_cg_t05_reaction_c_teacher_procedural.png
ft001_cg_intro_01_mother_neutral.png
ft001_cg_intro_02_child_neutral.png
ft001_cg_intro_03_grandmother_neutral.png
ft001_cg_intro_04_teacher_neutral.png
ft001_cg_intro_05_supervisor_explaining.png
ft001_cg_t02_l00_branch_mother_open.png
ft001_cg_t02_l00_branch_child_closed.png
ft001_cg_t02_l00_branch_teacher_cautious.png
ft001_cg_t03_l00_branch_child_links_pattern.png
ft001_cg_t03_l00_branch_mother_defensive.png
ft001_cg_t03_l00_branch_mother_cautious.png
ft001_cg_t04_l00_branch_grandmother_softened.png
ft001_cg_t04_l00_branch_grandmother_stubborn.png
ft001_cg_t04_l00_branch_child_exception.png
ft001_cg_t05_l00_branch_teacher_adjusts.png
ft001_cg_t05_l00_branch_child_scared.png
ft001_cg_t05_l00_branch_mother_anxious.png
```

## Manual QA Pass After Script

Open the generated contact sheet and review:

1. No two adjacent or semantically different cuts look like the same render.
2. The counseling room camera remains intentionally consistent, not randomly shifted.
3. Mother, child, teacher, grandmother, and supervisor identity stays stable across all cuts.
4. Each reaction cut has a visible expression, posture, or staging difference.
5. Choice idle cuts are visually distinct enough from line/reaction cuts to justify their slot.

Treat pHash hits as a review queue. A pHash hit is not automatically unusable, but every hit needs a human decision recorded in the JSON report handoff or downstream QA notes.
