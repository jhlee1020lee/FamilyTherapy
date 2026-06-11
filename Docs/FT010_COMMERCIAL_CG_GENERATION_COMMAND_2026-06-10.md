# FT-010 Commercial CG Generation Command

> Superseded for runtime import as of 2026-06-10 Round 3.
>
> Do not use this document as the active Unity import command. It targets an older `FT010_CommercialBranching` workflow and a smaller CG count. The current runtime manifest requires saving to `Assets/Resources/VN/EventCG/FT010/` through `family_therapy_practicum_cg_slot_manifest.json`.
>
> Active command: `Docs/FT002_FT010_V3_MANIFEST_IMAGE_WINDOW_COMMAND_2026-06-10.md`

## Purpose

Generate FT-010 commercial-quality visual novel CGs after reference images are approved.

This document is for the image-generation window. Do not edit code from that window.

## Source Documents

Use these files as the scene authority:

```text
Docs/FT010_SOLUTION_PARENTIFICATION_MAJOR_BRANCHING_SCENARIO_2026-06-10.md
Docs/FT010_SOLUTION_BRANCHING_LOCK_V2_2026-06-10.md
Docs/FT010_REALISTIC_DIALOGUE_EXPANSION_V2_2026-06-10.md
```

Authority order:

```text
1. Use the original major branching scenario for common intro, family premise, initial pacing, and base setting.
2. Use the V2 branching lock for route structure, state flags, endings, and CG coverage requirements.
3. Use the V2 dialogue expansion for T3-T5 scene wording, emotional beats, and supervisor tone.
```

## Reference Pack Required Before Final CGs

Do not generate final route CGs until the following references are created and approved:

```text
Docs/GeneratedSources/FT010_ReferencePack_20260610/ref/ft010_cast_identity_sheet_1600x900.png
Docs/GeneratedSources/FT010_ReferencePack_20260610/ref/ft010_home_kitchen_calendar_master_1600x900.png
Docs/GeneratedSources/FT010_ReferencePack_20260610/ref/ft010_after_school_living_room_master_1600x900.png
Docs/GeneratedSources/FT010_ReferencePack_20260610/ref/ft010_prop_sheet_backpack_checklist_homework_calendar_support_1600x900.png
Docs/GeneratedSources/FT010_ReferencePack_20260610/ref/ft010_supervisor_song_jihu_identity_1600x900.png
Docs/GeneratedSources/FT010_ReferencePack_20260610/ft010_reference_manifest.json
```

Reference pack contents:

- adolescent caregiver identity: high-school-age Korean adolescent, competent and tired, not adult-coded;
- ill guardian identity: chronically ill Korean adult guardian, guilty but not helpless caricature;
- younger sibling identity: Korean child, dependent but capable of small tasks;
- neighbor/teacher support person identity: practical support role, not a savior;
- supervisor 송지후 identity: solution-focused supervisor, warm, concrete, exception/scale oriented;
- home geometry: kitchen/living room with fridge calendar, table, homework area;
- prop sheet: backpack, medication/checklist sheet, unfinished homework, blank fridge calendar, support card, 30-minute self-time card, blank 1-point experiment card.

## Output Folders

Final accepted CGs:

```text
Assets/Resources/VN/EventCG/FT010_CommercialBranching/
```

Source candidates, rejected images, and contact sheets:

```text
Docs/GeneratedSources/FT010_CommercialBranching_20260610/
```

Manifest:

```text
Docs/GeneratedSources/FT010_CommercialBranching_20260610/ft010_cg_manifest.json
```

The manifest should record file name, linked CG ID, prompt, reference files, accepted/rejected status, rejection reason, and identity/room/prop notes.

Required manifest fields for each accepted or rejected candidate:

```text
cg_id
file_name
linked_scene
linked_choice_or_ending
route
visible_characters
camera_angle
focal_emotion
required_reference_files
prompt
negative_prompt
bottom_clean_area_confirmed
identity_consistency_confirmed
room_geometry_confirmed
accepted_status
rejection_reason
unity_resource_path_if_accepted
```

## Global Image Rules

Every final CG must follow:

```text
1600x900
16:9
no stretching
no baked text
no readable checklist text
no readable calendar text
no readable support-card text
no UI frame
no speech bubble
no watermark
bottom 25-30% visually clean for dialogue box
consistent character identity
consistent room geometry
consistent clothing
seated or naturally positioned characters, no standing sprite look
```

## Style Lock

Use this style:

```text
polished commercial Korean visual novel CG, realistic painterly 2D, restrained emotional acting, warm lived-in home lighting, intimate family counseling-drama mood, cinematic but readable composition, consistent character identity, no UI text, no caption, no watermark
```

Avoid:

```text
photorealistic uncanny AI portrait, poverty spectacle, savior imagery, melodramatic crying close-up, chibi, exaggerated anime expression, random extra people, readable text, calendar text, checklist text, support-card text, distorted hands, changing clothes, changing room layout, square image stretched to 16:9
```

## Character And Room Lock

Characters:

- `adolescent_caregiver`: Korean high-school adolescent, over-responsible, alert, tired, still visually a teenager.
- `ill_guardian`: Korean adult guardian, physically limited, guilty, but still capable of small actions.
- `younger_sibling`: Korean child, dependent but able to do one small self-care task.
- `support_person`: neighbor/teacher support role, practical and respectful, not taking over the family.
- `supervisor_song_jihu`: solution-focused supervisor, warm and precise.

Room:

- family kitchen/living room;
- fridge calendar, dining table, homework area;
- backpack, medication checklist, homework, support card, 30-minute card;
- bottom 25-30% must remain clean.

## Per-CG Prompt Template

Use this template for every CG:

```text
[CG_ID] [short file name]
polished commercial Korean visual novel CG, realistic painterly 2D, 1600x900, 16:9.
Scene: [specific scene].
Characters: [who is visible, posture, emotional state].
Props: [required props, no readable text].
Composition: [camera angle, focal figure, bottom 25-30% clean].
Continuity: match FT010 approved cast identity, room geometry, clothing, lighting.
Negative: no readable text, no UI, no speech bubble, no watermark, no extra people, no distorted hands, no standing sprite look, no square crop, no stretched image.
```

## 28-CG Shotlist

Use each item as a concrete prompt brief. Preserve 1600x900, no readable text, and bottom 25-30% clean in every CG.

1. `ft010_sf_001_intro_anchor.png`
   - Linked scene: common intro, recurring anchor.
   - Visible characters: none or only cropped hands at table edge.
   - Camera: medium-wide table still life from slightly above.
   - Focal emotion: quiet overload before anyone speaks.
   - Required props: school backpack beside medication checklist, unfinished homework, blank fridge calendar in background.
   - Composition: table/props in upper two-thirds, lower dialogue area simple tabletop shadow, no readable marks.

2. `ft010_sf_002_adolescent_after_school.png`
   - Linked scene: presenting pressure.
   - Visible characters: adolescent caregiver entering kitchen/living room, younger sibling in soft background.
   - Camera: doorway medium-wide, adolescent three-quarter view, backpack still on one shoulder.
   - Focal emotion: competent exhaustion, not adult glamour.
   - Required props: backpack, homework stack, pill case on counter.
   - Composition: adolescent upper center-left, lower clean area left open.

3. `ft010_sf_003_guardian_guilt.png`
   - Linked scene: guardian guilt.
   - Visible characters: ill guardian seated with water cup; adolescent visible but not comforting yet.
   - Camera: seated eye-level two-shot.
   - Focal emotion: guardian shame and limitation without helpless caricature.
   - Required props: pill case, water cup, blank checklist.
   - Composition: guardian focal in upper center, lower area clean.

4. `ft010_sf_004_sibling_small_task.png`
   - Linked scene: sibling capacity.
   - Visible characters: younger sibling attempting one self-care item; adolescent watching from a respectful distance.
   - Camera: low table-height medium shot.
   - Focal emotion: unsure capability.
   - Required props: small pouch, blank checklist, pencil without readable writing.
   - Composition: sibling upper center, lower area simple floor/table.

5. `ft010_sf_005_support_card_calendar.png`
   - Linked scene: possible external support.
   - Visible characters: support person at doorway or table edge, family partially visible.
   - Camera: medium-wide from family side toward blank fridge calendar.
   - Focal emotion: practical support offered without taking over.
   - Required props: blank support card near calendar, no readable text.
   - Composition: support card never covers whole calendar; lower area clean.

6. `ft010_sf_006_t3a_one_point_exception.png`
   - Linked scene/choice: T3A-1, 30-minute exception and 8-to-7 scaling.
   - Visible characters: adolescent setting backpack down while guardian and sibling notice.
   - Camera: medium-wide family table shot.
   - Focal emotion: a small already-working exception becoming visible.
   - Required props: backpack set aside, blank scale card, homework untouched for 30 minutes.
   - Composition: small open space around backpack; lower dialogue area uncluttered.

7. `ft010_sf_007_t3b_hero_burden.png`
   - Linked scene/choice: hero_burden route seed.
   - Visible characters: adolescent with praise directed at them, hands still full of care objects.
   - Camera: compressed medium shot from table side.
   - Focal emotion: praise landing as another obligation.
   - Required props: checklist, sibling item, homework.
   - Composition: objects cluster around adolescent, lower area clean.

8. `ft010_sf_008_t3c_guilt_centered.png`
   - Linked scene/choice: guilt_centered route seed.
   - Visible characters: guardian emotional in foreground, adolescent leaning toward them but not embracing heroically.
   - Camera: eye-level two-shot, guardian foreground left.
   - Focal emotion: guilt pulling attention away from action.
   - Required props: pill case, homework still untouched.
   - Composition: avoid warm-sacrifice framing; lower area clean.

9. `ft010_sf_009_t3d_resource_takeover.png`
   - Linked scene/choice: resource_takeover route seed.
   - Visible characters: support person presenting options while family looks smaller at table.
   - Camera: medium-wide from above table.
   - Focal emotion: help starting to outrun family choice.
   - Required props: several blank support cards partially covering blank calendar printout.
   - Composition: no readable marks; lower area clean.

10. `ft010_sf_010_t4a_one_point_plan.png`
   - Linked scene/choice: T4A-1 high path.
   - Visible characters: adolescent, guardian, sibling, support person all visible around table.
   - Camera: balanced medium-wide.
   - Focal emotion: shared, small, chosen next step.
   - Required props: blank 30-minute self-time card, one sibling task object, guardian water cup, one support card.
   - Composition: each person has one small item, lower area clean.

11. `ft010_sf_011_t4b_repaired_strength_cost.png`
   - Linked scene/choice: T4B-1 repaired path.
   - Visible characters: adolescent centered, guardian and sibling listening; therapist viewpoint implied.
   - Camera: close medium shot on adolescent and backpack.
   - Focal emotion: strength acknowledged without making sacrifice sacred.
   - Required props: praise card turned face-down, backpack loosened, blank exception card visible.
   - Composition: adolescent not carrying all objects; lower area clean.

12. `ft010_sf_012_t4b_low_praised_work.png`
   - Linked scene/choice: T4B-2 or T4B-3 low path.
   - Visible characters: family praising adolescent while adolescent still holds care objects.
   - Camera: slightly compressed medium shot.
   - Focal emotion: warm praise becoming role lock.
   - Required props: checklist, sibling pouch, homework all near adolescent.
   - Composition: no celebratory hero lighting; lower area clean.

13. `ft010_sf_013_t4c_repaired_ten_minute_action.png`
   - Linked scene/choice: T4C-1 repaired path.
   - Visible characters: guardian seated beside homework area for a small concrete action; adolescent steps back.
   - Camera: medium two-shot with guardian foreground and adolescent relaxed in background.
   - Focal emotion: guilt converted into a doable 10-minute action.
   - Required props: homework, water cup, blank timer/card with no readable text.
   - Composition: adolescent not comforting guardian; lower area clean.

14. `ft010_sf_014_t4c_low_guilt_no_action.png`
   - Linked scene/choice: T4C-2 or T4C-3 low path.
   - Visible characters: guardian apologizing, adolescent drawn close into comfort role.
   - Camera: tight two-shot, emotional weight on adolescent's face.
   - Focal emotion: guilt replacing action.
   - Required props: unchanged homework/checklist in background.
   - Composition: avoid sentimental filial-hero framing; lower area clean.

15. `ft010_sf_015_t4d_low_support_list.png`
   - Linked scene/choice: T4D-2 low path.
   - Visible characters: support person and family at table, support materials dominating.
   - Camera: table-overhead medium-wide.
   - Focal emotion: family choice crowded out.
   - Required props: many blank support cards, blank calendar partly hidden.
   - Composition: no readable text; lower area clean.

16. `ft010_sf_016_t4d_repaired_chosen_support.png`
   - Linked scene/choice: T4D-1 repaired path.
   - Visible characters: family choosing one support card together; support person waits respectfully.
   - Camera: medium-wide from slightly behind adolescent.
   - Focal emotion: help preserving ownership.
   - Required props: one selected blank support card, calendar still visible, backpack set aside.
   - Composition: support person not centered as savior; lower area clean.

17. `ft010_sf_017_t5_high_experiment_card.png`
   - Linked scene/choice: T5-1 high confirmation.
   - Visible characters: family hands placing one card each on table; faces partly visible.
   - Camera: table-level close medium.
   - Focal emotion: one-point experiment and recovery rule.
   - Required props: blank 1-point experiment card, blank recovery rule card, one task item per family member.
   - Composition: cards blank/no readable writing; lower area clean.

18. `ft010_sf_018_ending_a_one_point_less.png`
   - Linked scene/ending: A.
   - Visible characters: adolescent with 30-minute self-time, guardian and sibling doing small tasks in background.
   - Camera: warm medium-wide.
   - Focal emotion: relief without fantasy resolution.
   - Required props: backpack set aside, homework or club item, sibling pouch, guardian water cup.
   - Composition: adolescent upper third, lower clean.

19. `ft010_sf_019_ending_a_repaired_strength.png`
   - Linked scene/ending: A-Repaired after hero_burden repair.
   - Visible characters: adolescent and guardian looking at exception card, sibling nearby.
   - Camera: medium shot from table side.
   - Focal emotion: strength respected and burden reduced.
   - Required props: praise card face-down, blank exception card, backpack lighter/open.
   - Composition: no heroic sacrifice pose; lower area clean.

20. `ft010_sf_020_ending_b_more_work.png`
   - Linked scene/ending: B.
   - Visible characters: adolescent praised while carrying care tasks again.
   - Camera: slightly distant medium-wide.
   - Focal emotion: good intentions increasing burden.
   - Required props: backpack, checklist, homework, sibling item all clustered.
   - Composition: family warmth should feel constricting, lower area clean.

21. `ft010_sf_021_ending_c_guilt_replaces_action.png`
   - Linked scene/ending: C.
   - Visible characters: guardian guilt centered, adolescent comforting posture.
   - Camera: eye-level two-shot.
   - Focal emotion: adolescent parentified by emotional caretaking.
   - Required props: unchanged homework and checklist visible.
   - Composition: do not romanticize comforting; lower area clean.

22. `ft010_sf_022_ending_c_repaired_ten_minutes.png`
   - Linked scene/ending: C-Repaired.
   - Visible characters: guardian doing a 10-minute support task while adolescent has personal space.
   - Camera: medium-wide with two action zones.
   - Focal emotion: apology converted into action.
   - Required props: homework area, blank timer/card, backpack off adolescent.
   - Composition: lower area clean and uncluttered.

23. `ft010_sf_023_ending_d_imposed_support.png`
   - Linked scene/ending: D.
   - Visible characters: support person central, family slightly pushed back.
   - Camera: table-overhead or medium-wide.
   - Focal emotion: imposed help replacing family choice.
   - Required props: many blank support cards, calendar obscured.
   - Composition: no savior glow; lower area clean.

24. `ft010_sf_024_ending_d_repaired_chosen_help.png`
   - Linked scene/ending: D-Repaired.
   - Visible characters: support person at edge, family keeps calendar and selected card.
   - Camera: balanced medium-wide.
   - Focal emotion: practical help chosen by family.
   - Required props: one blank support card, visible blank calendar, backpack set aside.
   - Composition: support person secondary, lower area clean.

25. `ft010_sf_025_low_same_backpack.png`
   - Linked scene/ending: Low.
   - Visible characters: adolescent alone or family blurred in background.
   - Camera: static table still life with adolescent edge silhouette.
   - Focal emotion: nothing changed.
   - Required props: same school bag, blank medication-check sheet, unfinished homework, and blank calendar remain unchanged.
   - Composition: lower dialogue area clean and quiet.

26. `ft010_sf_026_supervisor_song_jihu_exception.png`
   - Linked scene: supervisor debrief after high/repaired path.
   - Visible characters: supervisor 송지후 alone or centered.
   - Camera: seated counselor-side medium shot.
   - Focal emotion: warm precision, exception/scale/1-point change tone.
   - Required props: blank notebook/tablet with no readable text.
   - Composition: supervisor not in family row; lower area clean.

27. `ft010_sf_027_supervisor_song_jihu_warning.png`
   - Linked scene: supervisor debrief after low/harmful path.
   - Visible characters: supervisor 송지후 alone or centered.
   - Camera: slightly closer medium shot.
   - Focal emotion: calm warning about hero praise, guilt, or support takeover.
   - Required props: blank notes, soft office background.
   - Composition: no readable text; lower area clean.

28. `ft010_sf_028_route_contact_sheet.png`
   - Linked scene: production contact sheet/check image, not for final in-game use unless explicitly accepted.
   - Visible characters: small clean lineup of adolescent, guardian, sibling, support person, supervisor.
   - Camera: reference lineup, consistent clothing and lighting.
   - Focal emotion: identity continuity check.
   - Required props: none except neutral background.
   - Composition: no text labels baked into the image; leave lower area clean.

## Acceptance Checklist

Reject any image if:

- it is not exactly 1600x900;
- it has readable text, UI, captions, speech bubbles, or watermark;
- calendar/checklist/card contains legible characters;
- bottom dialogue area is visually busy;
- adolescent looks adult-coded or glamorized;
- guardian looks helpless caricatured;
- support person appears as a savior taking over;
- adolescent sacrifice, parentification, or comforting the guardian is framed as warm virtue or filial heroism;
- room geometry or clothing changes without reason;
- it looks like standing sprite art instead of a full-scene CG;
- it conflicts with the approved reference pack.

## Cross-Window Handoff Rule

This document is for the image-generation window, but the accepted filenames must be handed back to the Codex/Unity window.

Image-generation window responsibilities:

```text
1. Build and approve the FT010 reference pack before route CGs.
2. Generate candidates only in the batch order below.
3. Reject images that violate identity, 1600x900, no-stretch, no-readable-text, or bottom-clean-area rules.
4. Write/update ft010_cg_manifest.json after each accepted or rejected candidate.
5. Report accepted final filenames and rejected reasons to the Codex/Unity window.
```

Codex/Unity window responsibilities after accepted filenames are reported:

```text
1. Map accepted images into Assets/Resources/VN/EventCG/FT010_CommercialBranching/.
2. Connect CG IDs to FT-010 dialogue/branching scenes.
3. Verify runtime at the intended 1600x900 windowed resolution.
4. Confirm images are not stretched, cropped incorrectly, or covered by the dialogue UI.
5. Keep final resource names stable once Unity wiring begins.
```

Runtime test condition:

```text
Open the game at 1600x900 windowed mode and check at least one intro CG, one route CG, one T4 repaired CG, one bad ending CG, and one supervisor debrief CG.
The image must preserve 16:9 aspect ratio and leave the lower dialogue area readable.
```

## Batch Rule

Generate in small batches:

```text
1. Reference pack first.
2. Intro/route anchors, CG 01-09.
3. T4/T5 intervention CGs, CG 10-17.
4. Endings and supervisor, CG 18-28.
```

Do not generate all 28 final CGs blindly in one batch.
