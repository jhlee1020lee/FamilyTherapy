# FT-008 V3 Patch And Gate Target

Last updated: 2026-06-10

## Status

FT-008 V2 was blocked by formal clinical and game-branching reviews.

The underlying narrative therapy content passed substantially, but two gate blockers remained:

1. repaired route endings were shadowed by `A-Repaired`;
2. outsider-witness consent was awarded as a flag but not explicitly earned in dialogue.

This V3 patch keeps the V2 scenario/dialogue files as the active production files and records the exact fixes applied to them.

Active patched files:

```text
Docs/FT008_NARRATIVE_BRANCHING_LOCK_V2_2026-06-10.md
Docs/FT008_REALISTIC_DIALOGUE_EXPANSION_V2_2026-06-10.md
```

## Patch 1. Repaired Ending Resolver

Problem:

```text
A-Repaired resolved before C-Repaired and D-Repaired.
```

Clinical/game consequence:

```text
T3C-1 -> T4C-1 -> T5-1 incorrectly resolved to A-Repaired.
T3D-1 -> T4D-1 -> T5-1 incorrectly resolved to A-Repaired.
Late repairs could also erase heavy B/C damage.
```

V3 fix:

- `C-Repaired` now resolves before `A-Repaired`.
- `D-Repaired` now resolves before `A-Repaired`.
- `A-Repaired` is limited to `route_primary == silence_protection`.
- `C-Repaired` is limited to `route_primary == endurance_story`.
- `D-Repaired` is limited to `route_primary == procedure_closure`.
- repaired endings now check route-specific heavy-damage blockers:
  - `parent_reassurance_overwrites < 2`;
  - `endurance_story_reinforced < 2`;
  - `school_voice_over_takes < 2`;
  - `forced_disclosure_attempted < 2`;
  - existing silence, transfer, procedure, and case-file blockers.

Added resolver regression examples:

```text
T3C-1 -> T4C-1 -> T5-1 = C-Repaired
T3D-1 -> T4D-1 -> T5-1 = D-Repaired
T3C-2 -> T4C-1 -> T5-1 = C
T3B-3 -> T4B-1 -> T5-1 = B
```

## Patch 2. Outsider-Witness Consent

Problem:

```text
T5 high awarded outsider_witness_consent_obtained, but dialogue only implied consent through school support/public-scope language.
```

V3 fix:

T5 high now explicitly asks the teen what family and school may remember, witness, and reflect.

The teen permits only:

```text
friend-reply moment
family checking whether silence protects or isolates
```

The teen refuses:

```text
incident-detail retelling
```

This makes `outsider_witness_consent_obtained +1` clinically earned rather than automatically granted.

## Patch 3. Image Prompt Safety

The commercial VN review passed, with two non-blocking image prompt clarifications.

V3 applied both:

- `FT008_CG_05` now requires no readable phone characters or legible chat UI text.
- `FT008_CG_15` now asks for a blank sticky note / clean note area, with any text added later in-engine.

## Focused Re-Check Target

FT-008 V3 should now pass if reviewers agree that:

1. repaired route endings are no longer shadowed;
2. late repairs cannot erase heavy prior damage;
3. outsider-witness consent is explicitly dialogic and teen-limited;
4. no new contradiction was introduced between branching lock and dialogue.
