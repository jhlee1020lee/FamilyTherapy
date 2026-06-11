# FT-009 V3 Gate Review

Last updated: 2026-06-10

## Verdict

```text
PASS
```

FT-009 now meets the FT-001-level-or-better bar for scenario, branching, dialogue, perinatal safety handling, and commercial VN image readiness.

## Active Production Files

```text
Docs/FT009_CBFT_BRANCHING_LOCK_V2_2026-06-10.md
Docs/FT009_REALISTIC_DIALOGUE_EXPANSION_V2_2026-06-10.md
Docs/FT009_COMMERCIAL_CG_GENERATION_COMMAND_2026-06-10.md
Docs/FT009_V3_PATCH_AND_GATE_TARGET_2026-06-10.md
```

The V2 filenames remain active because the V3 patch was applied directly to those production files and recorded separately.

## Review Results

### CBFT / Perinatal Safety

Initial V2 result:

```text
BLOCK
Overall CBFT/perinatal fidelity: 8.8/10
```

Blocker:

```text
support_network_contacted was defined but not operationalized.
```

V3 focused re-check:

```text
PASS
```

Reviewer conclusion:

```text
support_network_contacted is now activated in T4A-1, T4D-1, and T5-1.
A, A-Repaired, C-Repaired, and D-Repaired all require support_network_contacted >= 1.
Dialogue operationalizes the support network through the family group chat / real standby supporter.
```

### Game Branching / Resolver

Initial V2 result:

```text
BLOCK
Overall game branching/resolver: 7.4/10
```

Blockers:

- `A-Repaired` was shadowed by full A;
- bad blame/overreaction paths could over-repair into full A;
- `T3B-1 -> T4B-2 -> T5-1` did not reliably resolve to B.

V3 focused re-check:

```text
PASS
```

Verified outcomes:

```text
T3A-1 -> T4A-1 -> T5-1 = A
T3B-1 -> T4B-1 -> T5-1 = A-Repaired
T3B-3 -> T4B-1 -> T5-1 = B
T3B-1 -> T4B-2 -> T5-1 = B
T3C-1 -> T4C-1 -> T5-1 = C-Repaired
T3D-1 -> T4D-1 -> T5-1 = D-Repaired
T3D-2 -> T4D-1 -> T5-1 = D
T3A-1 -> T4A-1 -> T5-4 = D
T3B-2 -> T4B-1 -> T5-1 = B
```

### Commercial VN / CG Readiness

Initial V2 result:

```text
BLOCK
FT-001-level parity: 7.7/10
```

Blockers:

- `support_network_contacted` mismatch between resolver and dialogue Ending A condition;
- missing FT-001-level image production package;
- supervisor dialogue included route/trap labels;
- D-Repaired did not require final confirmation or night contract flags.

V3 focused re-check:

```text
PASS
```

Cleared items:

- Ending A dialogue conditions now include `support_network_contacted >= 1`;
- `Docs/FT009_COMMERCIAL_CG_GENERATION_COMMAND_2026-06-10.md` now provides reference pack, output paths, prompt template, negatives, manifest/archive rules, and acceptance checklist;
- supervisor spoken lines no longer expose game route labels;
- D-Repaired now requires `final_confirm_three_line_plan`, `night_contract_written`, and `failure_retry_rule_written`.

## Final FT-009 Gate Status

```text
FT-009 V3 passed.
Proceed to FT-010.
```
