# FT-006 V3 Patch And Gate Target

Last updated: 2026-06-10

## Status

FT-006 V2 passed clinical and commercial VN gates, but failed the game branching gate due to one production-document inconsistency.

The V3 patch is applied directly to:

```text
Docs/FT006_SATIR_ILLNESS_SIBLING_BRANCHING_LOCK_V2_2026-06-10.md
Docs/FT006_REALISTIC_DIALOGUE_EXPANSION_V2_2026-06-10.md
```

These files remain the active production authority for FT-006 after the V3 patch.

## Gate Failure Addressed

### High A Path Effect Mismatch

Problem:

```text
The branching lock required iceberg_layers_named for Ending A.
The dialogue file did not include iceberg_layers_named in T3A-1, T4A-1, or Ending A conditions.
```

This could cause:

```text
T3A-1 -> T4A-1 -> T5-1
```

to miss Ending A if implemented from the dialogue file effects.

Patch:

```text
T3A-1 dialogue effects now add iceberg_layers_named +1.
T4A-1 dialogue effects now add family_sculpture_repositioned +1 and iceberg_layers_named +1.
Ending A dialogue conditions now require iceberg_layers_named >= 1, matching the branching lock.
```

### CG Coverage Expanded

Non-blocking VN note:

```text
A-Repaired, C-Repaired, D-Repaired, and generic Low were implied but not individually CG-locked.
```

Patch:

```text
Expanded FT-006 CG lock from 20 to 24 images.
Added explicit CG rows for A-Repaired, C-Repaired, D-Repaired, and Low.
```

## Re-Gate Requirement

Run a focused game branching/consequence re-check:

```text
1. Verify T3A-1 -> T4A-1 -> T5-1 now opens Ending A.
2. Verify the dialogue and branching lock agree on iceberg_layers_named.
3. Verify no new resolver contradiction was introduced.
```

Clinical and VN gates have already passed:

```text
Clinical/Satir fidelity: PASS, 9.0/10.
Commercial VN scene quality: PASS, 8.6/10.
```
