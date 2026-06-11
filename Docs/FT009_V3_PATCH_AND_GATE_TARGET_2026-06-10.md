# FT-009 V3 Patch And Gate Target

Last updated: 2026-06-10

## Status

FT-009 V2 was blocked by formal review in three narrow areas:

1. support-network use was defined but not operationalized;
2. repaired blame routes could be swallowed by full A or over-repaired;
3. commercial VN image-generation readiness was below FT-001-level production packaging.

This V3 patch keeps the V2 scenario/dialogue filenames as active production files and records the exact fixes applied to them.

Active patched files:

```text
Docs/FT009_CBFT_BRANCHING_LOCK_V2_2026-06-10.md
Docs/FT009_REALISTIC_DIALOGUE_EXPANSION_V2_2026-06-10.md
Docs/FT009_COMMERCIAL_CG_GENERATION_COMMAND_2026-06-10.md
```

## Patch 1. Support Network Becomes A Live Behavior

Problem:

```text
support_network_contacted was in the state model, but no choice activated it and endings did not require it.
```

V3 fix:

- T4A-1 now contacts an actual standby supporter through the family group chat.
- T4D-1 now contacts an actual standby supporter before returning to behavior contract work.
- T5-1 now requires contacting the crisis/support contact and the standby family supporter.
- A, A-Repaired, C-Repaired, and D-Repaired now require `support_network_contacted >= 1`.
- Dialogue now shows the spouse sending the group-chat message instead of leaving support as a prop.

## Patch 2. Resolver Shadowing And Over-Repair

Problem:

```text
Full A resolved before A-Repaired.
Bad blame/overreaction paths could over-repair into full A.
T3B-1 -> T4B-2 -> T5-1 did not reliably resolve to B.
```

V3 fix:

- A-Repaired, C-Repaired, and D-Repaired now resolve before full A.
- Full A is limited to `route_primary == cycle_mapped`.
- Full A now blocks:
  - `spouse_blamed_as_cause >= 2`;
  - `parent_labeled_overreacting >= 2`;
  - `mindreading_expectation_reinforced >= 2`;
  - reassurance-only and safety-delay traps.
- T4B-2 now applies `spouse_blamed_as_cause +3`, so renewed blame after a repair attempt still resolves to B.
- Added regression examples:
  - `T3B-3 -> T4B-1 -> T5-1 = B`;
  - `T3B-1 -> T4B-2 -> T5-1 = B`.

## Patch 3. D-Repaired Requires A Final Plan

Problem:

```text
D-Repaired promised "safety card first, night contract second" but did not require final confirmation or contract flags.
```

V3 fix:

D-Repaired now requires:

```text
final_confirm_three_line_plan == true
night_contract_written >= 1
failure_retry_rule_written >= 1
```

## Patch 4. Supervisor Voice

Problem:

```text
Some 정세영 lines used game-mechanic labels such as "C 경로" and "최종 D 트랩".
```

V3 fix:

Those lines were rewritten as clinical supervisor feedback:

- "위로가 행동을 대신하고 있습니다."
- "오늘 밤 바뀌는 행동이 없습니다."
- "위험 신호가 나온 장면에서는 대화 기술보다 보호 계획이 먼저입니다."

## Patch 5. Commercial CG Production Package

Problem:

```text
The CG lock listed images but did not include FT-001-level production packaging.
```

V3 fix:

Created:

```text
Docs/FT009_COMMERCIAL_CG_GENERATION_COMMAND_2026-06-10.md
```

The command file includes:

- required reference pack;
- character identity locks;
- room and prop reference requirements;
- supervisor 정세영 reference authority;
- output paths;
- manifest/archive rules;
- global 1600x900/no text/no UI/bottom-clean rules;
- per-CG prompt template;
- negatives;
- 24-CG shotlist;
- acceptance checklist;
- small-batch generation rule.

## Focused Re-Check Target

FT-009 V3 should pass if reviewers agree that:

1. support-network use is a live required behavior;
2. repaired endings are not shadowed by full A;
3. bad blame/overreaction paths cannot over-repair;
4. supervisor spoken lines no longer expose game route labels;
5. image-generation readiness now matches the FT-001 production-command standard.
