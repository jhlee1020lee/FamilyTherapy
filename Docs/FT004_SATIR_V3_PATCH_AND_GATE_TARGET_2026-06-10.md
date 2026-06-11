# FT-004 Satir V3 Patch And Gate Target

Date: 2026-06-10

## Status

This document records the V3 patch applied after FT-004 V2 failed two gate checks.

Active production files remain:

```text
Docs/FT004_SATIR_BRANCHING_LOCK_V2_2026-06-10.md
Docs/FT004_REALISTIC_DIALOGUE_EXPANSION_V2_2026-06-10.md
```

Those files now include the V3 fixes. This document is the gate coordination record for those fixes.

## Failed Gate Issues

The failed reviewers identified four concrete blockers:

```text
1. Pure congruent high route did not satisfy Ending A because checklist_connected_to_help was missing.
2. Ending priority used human-readable phrases that were not deterministic enough for implementation.
3. T5 final confirmation did not clearly affect endings.
4. Speakerphone and document envelope were not repeated strongly enough as visual anchors.
```

## Applied V3 Fixes

### 1. Congruent Route Lock

T4A-1 now sets:

```text
congruent_request_practiced +2
spouse_reflection_practiced +1
checklist_connected_to_help +1
```

This means pure `congruent_voice` high play can satisfy Ending A without requiring a detour through another route.

### 2. Final Confirmation Flags

The final confirmation turn now uses explicit flags:

```text
final_confirm_help
final_confirm_compliance_trap
final_confirm_bypass_trap
```

The high confirmation sets:

```text
final_confirm_help = true
checklist_connected_to_help +1
congruent_request_practiced +1
institution_as_resource +1
```

The two traps set:

```text
final_confirm_compliance_trap = true
placating_accepted +1
caregiver_alone_responsible +1
unresolved_smile +1

final_confirm_bypass_trap = true
institution_as_only_solution +1
superreasonable_cover +1
```

### 3. Deterministic Ending Resolver

The ending resolver must be implemented exactly as:

```text
if final_confirm_compliance_trap == true:
    if blame_intensified >= 2:
        choose C
    elif institution_as_only_solution >= 2 or superreasonable_cover >= 2:
        choose D
    else:
        choose B
elif final_confirm_bypass_trap == true:
    if superreasonable_cover >= 2 and emotion_bypassed >= 2:
        choose E
    else:
        choose D
elif congruent_request_practiced >= 2
     and spouse_reflection_practiced >= 1
     and checklist_connected_to_help >= 1
     and blame_intensified < 2:
    choose A
elif repair_attempted >= 1
     and checklist_connected_to_help >= 2
     and emotion_bypassed < 2:
    choose A-Repaired
elif spouse_fear_named >= 2
     and iceberg_named >= 1
     and blame_intensified < 2:
    choose C-Repaired
elif institution_as_resource >= 2
     and congruent_request_practiced >= 1
     and institution_as_only_solution < 3:
    choose D-Repaired
elif superreasonable_cover >= 2
     and emotion_bypassed >= 2
     and congruent_request_practiced < 1:
    choose E
elif placating_accepted >= 2
     or caregiver_alone_responsible >= 2:
    choose B
elif blame_intensified >= 2
     or (caregiver_alone_responsible >= 2 and route_primary == blame_loop):
    choose C
elif institution_as_only_solution >= 2
     or emotion_bypassed >= 2:
    choose D
else:
    choose Low
```

### 4. Repeated Visual Anchors

The active docs now repeat:

```text
speakerphone
document envelope
folded notice
red missing-document mark
checklist beside request sentence
```

The CG lock now includes:

```text
FT004_CG_02B speakerphone on table
FT004_CG_02C document envelope with mixed papers
```

## Gate Target

FT-004 should pass only if reviewers agree:

- A high route is implementable.
- Final confirmation can change the ending.
- Ending priority is deterministic.
- Speakerphone and document envelope are sufficiently present as visual anchors.
