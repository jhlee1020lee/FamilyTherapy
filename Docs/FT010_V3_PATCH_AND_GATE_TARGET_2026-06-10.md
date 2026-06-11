# FT-010 V3 Patch And Gate Target

Last updated: 2026-06-10

## Context

FT-010 V2 covered solution-focused parentification relief, but two formal gate reviewers blocked it:

```text
1. Mechanical blocker: some bad/low paths could over-repair into a repaired ending.
2. VN/CG handoff blocker: the commercial CG command was not yet detailed enough for FT-001-level image-window production.
```

The V3 patch keeps the V2 file names as the active scenario authorities and records the fixes here.

## Patched Files

```text
Docs/FT010_SOLUTION_BRANCHING_LOCK_V2_2026-06-10.md
Docs/FT010_REALISTIC_DIALOGUE_EXPANSION_V2_2026-06-10.md
Docs/FT010_COMMERCIAL_CG_GENERATION_COMMAND_2026-06-10.md
```

## Mechanical Fixes

Repaired endings now require `repair_started >= 1`:

```text
A-Repaired
C-Repaired
D-Repaired
```

This prevents a low/bad route such as `T3C-3 -> T4C-1 -> T5-1` from reaching `C-Repaired` without first choosing a true repair-starting intervention.

The dialogue expansion now matches the lock authority for `T3A-1`:

```text
strengths_acknowledged_without_romanticizing +1
parentification_risk_named +1
exception_identified +1
exception_thickened +1
scale_number_named +1
one_point_goal_set +1
```

The Low ending supervisor line no longer exposes English prop labels. It now uses natural Korean object description.

## CG Handoff Fixes

The FT-010 commercial CG command now includes:

- original major scenario as common intro/pacing/base-setting authority;
- explicit authority order among original scenario, V2 branching lock, and V2 dialogue expansion;
- required manifest fields for accepted and rejected candidates;
- rejection rule against romanticizing adolescent sacrifice, parentification, or guardian-comforting;
- expanded 28-CG shotlist;
- explicit T4B repaired and T4C repaired CGs;
- per-CG linked scene/choice, visible characters, camera, focal emotion, props, and composition notes;
- cross-window handoff rules for image-generation and Codex/Unity windows;
- accepted filename reporting and 1600x900 runtime test condition.

## Gate Target

FT-010 must pass three gates:

```text
1. Solution-focused fidelity and parentification safety.
2. Branching, state flags, resolver, and no over-repair.
3. Commercial VN/CG production readiness.
```

Passing threshold:

```text
FT-010 is FT-001-level or better if all three reviewers return PASS and no immediate blocker remains.
```
