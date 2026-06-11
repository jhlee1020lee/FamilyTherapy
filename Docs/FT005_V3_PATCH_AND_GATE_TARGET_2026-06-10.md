# FT-005 V3 Patch And Gate Target

Last updated: 2026-06-10

## Status

FT-005 V2 received three gate reviews and failed narrowly.

The V3 patch is applied directly to:

```text
Docs/FT005_STEPFAMILY_BRANCHING_LOCK_V2_2026-06-10.md
Docs/FT005_REALISTIC_DIALOGUE_EXPANSION_V2_2026-06-10.md
```

These files remain the active production authority for FT-005 after the V3 patch.

## Gate Failures Addressed

### 1. Authority Repair Flag Bug

Problem:

```text
T3B-1 previously set repaired_at_t4 too early.
```

This could allow:

```text
T3B-1 -> T4B-2 -> T5-1 = A-Repaired
```

even though T4B-2 imposes greeting/deference rules and should end in B.

Patch:

```text
T3B-1 now sets authority_speed_acknowledged, not repaired_at_t4.
repaired_at_t4 is reserved for T4B-1 only.
A-Repaired now requires respect_rule_imposed < 2 and stepfather_authority_pushed < 2.
Resolver regression examples were added.
```

### 2. T4 Low Choice Scene Payoff

Problem:

```text
T4 Choice 2 and Choice 3 reactions were flag-only.
```

Patch:

```text
Added visible family/supervisor reactions for:
A2, A3
B2, B3
C2, C3
D2, D3
```

Each reaction now shows the emotional and structural cost of the low/mixed choice.

### 3. Ending CG Coverage

Problem:

```text
Only Ending A and Low had explicit ending CGs.
```

Patch:

```text
Expanded FT-005 CG lock from 15 to 20 images.
Added explicit ending visuals for B, C, C-Repaired, D, and D-Repaired.
```

## Re-Gate Requirement

FT-005 should be re-reviewed against:

```text
FT-001
FT-002 V3
FT-003 V2
FT-004 V3
```

Required pass gates:

```text
1. Clinical/structural stepfamily therapy fidelity
2. Game branching/consequence integrity
3. Commercial VN dialogue and CG-scene quality
```

Only after all three gates pass should production move to FT-006.
