# FT-009 Commercial CG Generation Command

> Superseded for runtime import as of 2026-06-10 Round 3.
>
> Do not use this document as the active Unity import command. It targets an older `FT009_CommercialBranching` workflow and a smaller CG count. The current runtime manifest requires saving to `Assets/Resources/VN/EventCG/FT009/` through `family_therapy_practicum_cg_slot_manifest.json`.
>
> Active command: `Docs/FT002_FT010_V3_MANIFEST_IMAGE_WINDOW_COMMAND_2026-06-10.md`

## Purpose

Generate FT-009 commercial-quality visual novel CGs after reference images are approved.

This document is for the image-generation window. Do not edit code from that window.

## Source Documents

Use these files as the scene authority:

```text
Docs/FT009_CBFT_BRANCHING_LOCK_V2_2026-06-10.md
Docs/FT009_REALISTIC_DIALOGUE_EXPANSION_V2_2026-06-10.md
```

## Reference Pack Required Before Final CGs

Do not generate final route CGs until the following references are created and approved:

```text
Docs/GeneratedSources/FT009_ReferencePack_20260610/ref/ft009_cast_identity_sheet_1600x900.png
Docs/GeneratedSources/FT009_ReferencePack_20260610/ref/ft009_night_living_room_master_1600x900.png
Docs/GeneratedSources/FT009_ReferencePack_20260610/ref/ft009_nursery_corner_master_1600x900.png
Docs/GeneratedSources/FT009_ReferencePack_20260610/ref/ft009_prop_sheet_bottle_meal_phone_leaflet_contract_1600x900.png
Docs/GeneratedSources/FT009_ReferencePack_20260610/ref/ft009_supervisor_jung_seyoung_identity_1600x900.png
Docs/GeneratedSources/FT009_ReferencePack_20260610/ft009_reference_manifest.json
```

Reference pack contents:

- postpartum parent identity: exhausted but not caricatured, seated, realistic Korean adult;
- spouse identity: tired, anxious, not villain-coded, realistic Korean adult;
- infant presence: implied through bassinet/bottle/blanket, never distressed close-up;
- supervisor 정세영 identity: warm, practical, professional CBFT supervisor;
- night living-room geometry: sofa, dim nursery lamp, meal tray, bottle, phone, clinic leaflet;
- prop sheet: bottle, untouched meal tray, unread group-chat screen with no readable text, clinic leaflet with no readable text, blank three-line contract card, support contact card with no readable numbers.

## Output Folders

Final accepted CGs:

```text
Assets/Resources/VN/EventCG/FT009_CommercialBranching/
```

Source candidates, rejected images, and contact sheets:

```text
Docs/GeneratedSources/FT009_CommercialBranching_20260610/
```

Manifest:

```text
Docs/GeneratedSources/FT009_CommercialBranching_20260610/ft009_cg_manifest.json
```

The manifest should record:

- file name;
- linked CG ID;
- source prompt;
- reference files used;
- accepted/rejected status;
- reason for rejection if rejected;
- notes on identity, room, and prop continuity.

## Global Image Rules

Every final CG must follow:

```text
1600x900
16:9
no stretching
no baked text
no readable phone text
no readable leaflet text
no readable contract text
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
polished commercial Korean visual novel CG, realistic painterly 2D, restrained emotional acting, warm dim night lighting, intimate domestic counseling-drama mood, cinematic but readable composition, consistent character identity, no UI text, no caption, no watermark
```

Avoid:

```text
photorealistic uncanny AI portrait, horror mood, glamorized distress, melodramatic crying close-up, chibi, exaggerated anime expression, random extra people, readable text, medical chart text, phone chat text, distorted hands, changing clothes, changing room layout, square image stretched to 16:9
```

## Character And Room Lock

Characters:

- `postpartum_parent`: Korean adult postpartum parent, exhausted, sleep-deprived, guarded, not irrational or villainized.
- `spouse`: Korean adult spouse, anxious and frozen, wants to help but fears doing it wrong, not lazy/villain-coded.
- `infant`: represented safely through bassinet, blanket, bottle, or off-frame care object; no distress exploitation.
- `support_network`: represented through unread group-chat phone or implied support card; no extra family members unless a specific CG asks for them.
- `supervisor_jung_seyoung`: CBFT supervisor, warm but practical, centered in supervisor/debrief CGs.

Room:

- nighttime living room / nursery corner;
- dim lamp, sofa, small table;
- feeding bottle, untouched meal tray, unread group-chat phone, clinic leaflet, blank contract/support card;
- bottom 25-30% must remain clean.

## Per-CG Prompt Template

Use this template for every CG:

```text
[CG_ID] [short file name]
polished commercial Korean visual novel CG, realistic painterly 2D, 1600x900, 16:9.
Scene: [specific scene].
Characters: [who is visible, their posture, emotional state].
Props: [required props, no readable text].
Composition: [camera angle, who is focal, bottom 25-30% clean].
Continuity: match FT009 approved cast identity, room geometry, clothing, lighting.
Negative: no readable text, no UI, no speech bubble, no watermark, no extra people, no distorted hands, no standing sprite look, no square crop, no stretched image.
```

## 24-CG Shotlist

1. `ft009_cb_001_intro_anchor.png`
   - Night feeding bottle beside untouched meal tray, unread group-chat phone, folded clinic leaflet.
   - No readable text anywhere.

2. `ft009_cb_002_parent_night_exhaustion.png`
   - Postpartum parent under dim nursery lamp, physically exhausted but dignified.

3. `ft009_cb_003_spouse_frozen_alarm.png`
   - Spouse awake but frozen beside phone alarm, wanting to help but unsure.

4. `ft009_cb_004_clinic_leaflet_support_card.png`
   - Clinic leaflet and support card beside empty water cup, no readable text.

5. `ft009_cb_005_behavior_chain_blank_cards.png`
   - Blank thought/behavior/sleep cards on table, no readable text.

6. `ft009_cb_006_cycle_mapped_shared_loop.png`
   - Bottle, alarm, diaper stack arranged as shared night loop.

7. `ft009_cb_007_blame_loop_bottle_evidence.png`
   - Bottle visually between spouses like evidence, tension but not melodrama.

8. `ft009_cb_008_comfort_only_untouched_meal.png`
   - Warm reassurance while meal tray remains untouched and night setup unchanged.

9. `ft009_cb_009_risk_uncontained_folded_leaflet.png`
   - Clinic leaflet still folded while parent looks away, safety support not yet opened.

10. `ft009_cb_010_t4a_three_line_plan.png`
   - Support card and blank three-line plan beside bottle; spouse ready to act.

11. `ft009_cb_011_t4b_low_defensive_spouse.png`
   - Spouse defensive after being framed as the cause; parent looks lonelier.

12. `ft009_cb_012_t4c_low_reassurance_no_action.png`
   - Reassurance offered while diaper stack and bottle remain untouched.

13. `ft009_cb_013_t4d_low_leaflet_unopened.png`
   - Risk language softened; clinic leaflet/support card still unused.

14. `ft009_cb_014_t4d_repaired_safety_first.png`
   - Support card opened, phone ready for contact, baby safety implied, no readable numbers.

15. `ft009_cb_015_t5_high_blank_contract.png`
   - Blank three-line night contract card, bottle, support card, phone; clean bottom.

16. `ft009_cb_016_ending_a_tonight_plan.png`
   - Spouse preparing diaper/water while parent rests nearby; small realistic hope.

17. `ft009_cb_017_ending_a_repaired_shared_cycle.png`
   - Blame diagram replaced by shared cycle card; no readable text.

18. `ft009_cb_018_ending_b_better_blame.png`
   - One spouse overburdened while the other withdraws; no villain posing.

19. `ft009_cb_019_ending_c_comfort_without_action.png`
   - Warm reassurance without changed night setup.

20. `ft009_cb_020_ending_c_repaired_request.png`
   - Parent practices 20-minute request; bottle set aside; spouse attentive.

21. `ft009_cb_021_ending_d_risk_uncontained.png`
   - Clinic leaflet and support card still unused; parent isolated.

22. `ft009_cb_022_ending_d_repaired_safety_contract.png`
   - Safety card first, blank night contract second; support phone visible, no readable text.

23. `ft009_cb_023_low_same_night_loop.png`
   - Same bottle, meal tray, unread messages; room unchanged.

24. `ft009_cb_024_supervisor_jung_seyoung_debrief.png`
   - 정세영-centered supervisor debrief, warm but practical, no family in same row unless composition demands it.

## Acceptance Checklist

Reject any image if:

- it is not exactly 1600x900;
- it has readable text, UI, captions, speech bubbles, or watermark;
- the phone/group chat/leaflet/contract contains legible characters;
- bottom dialogue area is visually busy;
- the parent looks villainized, irrational, glamorized, or horror-coded;
- the spouse looks lazy/villain-coded rather than anxious/frozen;
- the infant is shown in a distress-exploitative way;
- room geometry or clothing changes without reason;
- it looks like standing sprite art instead of a full-scene CG;
- it conflicts with the approved reference pack.

## Batch Rule

Generate in small batches:

```text
1. Reference pack first.
2. Intro/route anchors, CG 01-09.
3. T4/T5 intervention CGs, CG 10-15.
4. Endings and supervisor, CG 16-24.
```

Do not generate all 24 final CGs blindly in one batch.
