# FT-006 V3 Gate Review

Last updated: 2026-06-10

## Decision

```text
PASS
```

FT-006 V3 patched is judged FT-001-level or better.

Active production files:

```text
Docs/FT006_SATIR_ILLNESS_SIBLING_BRANCHING_LOCK_V2_2026-06-10.md
Docs/FT006_REALISTIC_DIALOGUE_EXPANSION_V2_2026-06-10.md
Docs/FT006_V3_PATCH_AND_GATE_TARGET_2026-06-10.md
```

## Gate Results

| Gate | Result | Score | Reviewer Notes |
| --- | --- | --- | --- |
| Clinical/Satir experiential fidelity | PASS | 9.0/10 | Iceberg layers, family sculpture, congruent communication, parent guilt regulation, and non-blaming illness frame all passed. |
| Game branching/consequence integrity | PASS | focused re-check PASS | V3 fixed the `iceberg_layers_named` effect mismatch; documented regression paths now match resolver behavior. |
| Commercial VN dialogue and CG-scene quality | PASS | 8.6/10 | Visual anchors, object beats, low-choice reactions, ending visuals, and 1600x900 CG lock passed. |

## Issues Fixed Before Pass

### High A Path Effect Mismatch

Earlier V2 problem:

```text
The branching lock required iceberg_layers_named for Ending A, but the dialogue effects did not add it.
```

V3 fix:

```text
T3A-1 adds iceberg_layers_named +1.
T4A-1 adds family_sculpture_repositioned +1 and iceberg_layers_named +1.
Ending A dialogue conditions include iceberg_layers_named >= 1.
```

### Resolver Regression Lock

Final checked examples:

```text
T3A-1 -> T4A-1 -> T5-1 = A
T3B-1 -> T4B-2 -> T5-1 = B
T3C-1 -> T4C-2 -> T5-1 = C
T3D-1 -> T4D-2 -> T5-1 = D
T3B-2 -> T4B-1 -> T5-1 = B
```

### CG Planning

FT-006 CG lock now contains 24 images, including explicit rows for:

```text
A-Repaired
C-Repaired
D-Repaired
Low
```

## Implementation Watch

When this is converted into Unity dialogue, preserve the difference between:

```text
apology as regulated repair
apology as guilt-flooding trap
```

Condensing those reactions too aggressively would flatten the central Satir teaching point.

## Next Step

Proceed to FT-007 only after this gate record is reflected in the production index and cross-window status.
