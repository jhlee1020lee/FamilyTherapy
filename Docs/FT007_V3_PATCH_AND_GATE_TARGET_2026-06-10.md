# FT-007 V3 Patch And Gate Target

Last updated: 2026-06-10

## Status

FT-007 V2 passed clinical and commercial VN gates, but failed the game branching gate due to resolver consistency.

The V3 patch is applied directly to:

```text
Docs/FT007_PSYCHODYNAMIC_BRANCHING_LOCK_V2_2026-06-10.md
Docs/FT007_REALISTIC_DIALOGUE_EXPANSION_V2_2026-06-10.md
```

These files remain the active production authority for FT-007 after the V3 patch.

## Gate Failure Addressed

### Repaired Ending Shadowing

Problem:

```text
Generic A-Repaired was checked before C-Repaired and D-Repaired.
```

This could cause:

```text
T3C-1 -> T4C-1 -> T5-1
T3D-1 -> T4D-1 -> T5-1
```

to resolve as A-Repaired instead of the route-specific repaired endings.

Patch:

```text
C-Repaired and D-Repaired now resolve before generic A-Repaired.
A-Repaired now has stronger blockers for secret rescue, mother-manager risk, interpretation attack, and alliance rupture.
```

### Locked Bad Route Over-Repair

Problem:

```text
T3C-2 and T3D-2 heavy damage could be over-repaired by one later good T4 choice.
```

Patch:

```text
C-Repaired blocks on secret_cash_reinforced >= 2, mother_rescue_as_solution >= 2, and father_excluded_from_money >= 2.
D-Repaired blocks on premature_interpretation_attack >= 2.
```

### Regression Explanation

Problem:

```text
T3C-1 -> T4C-2 -> T5-1 = C had the right result but wrong explanation.
```

Patch:

```text
The explanation now names T4C-2 secret-cash reinforcement as the actual mechanism.
```

## Re-Gate Requirement

Run a focused game branching/consequence re-check:

```text
1. Verify C-Repaired and D-Repaired are no longer shadowed by A-Repaired.
2. Verify T3C-2 -> T4C-1 -> T5-1 remains C.
3. Verify T3D-2 -> T4D-1 -> T5-1 remains D.
4. Verify documented regression examples match resolver behavior.
```

Clinical and VN gates have already passed:

```text
Clinical/psychodynamic fidelity: PASS, 9.0/10.
Commercial VN scene quality: PASS, 8.7/10.
```
