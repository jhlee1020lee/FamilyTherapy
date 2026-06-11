# FT002-FT010 Subagent Review Patch Report

Last updated: 2026-06-10

## Scope

Six read-only review agents checked FT002-FT010 from separate perspectives:

- family therapy theory fidelity
- realistic counseling dialogue
- game branching and consequence strength
- CG/manifest pipeline
- Korean naming/style consistency
- Unity build/runtime stability

This report records the code/document changes applied after that review.

## Applied Fixes

### Runtime Reaction Display

Problem:

```text
familyReaction text was written as third-person scene summary but displayed as if spoken by one character.
```

Fix:

- VN reaction screens now show the speaker name as `회기 반응`.
- Focused-case carryover lines now use `session_reaction`, so summary text no longer appears as a direct family member quote.

File:

```text
Assets/Scripts/FamilyTherapyPracticumGame.cs
```

### FT002 Bowen Choice Cleanup

Problem:

```text
FT002 T5 still contained an academic-homework bad choice that belonged to trainee learning, not family session work.
```

Fix:

- Replaced it with a clinically realistic but poor closure choice:
  - symptom-compliance check only
  - misses multigenerational clues and relational experiment
- Added `symptom_check_closure` to the FT002 risk token list.
- Corrected visible Korean particle errors:
  - `김선기은` -> `김선기는`
  - `박석민는` -> `박석민은`

### FT007 Psychodynamic Wording

Problem:

```text
Some family-facing choices exposed theory terms such as "방어 해석" too directly.
```

Fix:

- Rewrote the most exposed family-facing bad choices into everyday Korean while preserving the bad-intervention token.
- Kept the supervisor/theory logic intact.

### FT008 Safety Before Narrative Work

Problem:

```text
The narrative therapy scene did not visibly check current safety before externalization.
```

Fix:

- Added a school representative line about current commute/online separation safety.
- Rewrote the first high-quality FT008 choice to check current safety before discussing what silence protects or costs.
- Added `safety_check_before_story` to FT008 repair/integration tokens.

Side effect:

```text
FT008 required CG slots increased from 41 to 42 because the new safety-check dialogue line now needs its own EventCG.
```

### FT009 Postpartum Safety Specificity

Problem:

```text
The first safety choice was good but too generic for postpartum risk.
```

Fix:

- The high-quality FT009 opening choice now explicitly checks:
  - moments when the caregiver becomes unsafe alone
  - time alone with the baby
  - contact person
  - spouse shift/rotation
- Added `crisis_contact_named` and `baby_safety_check` to safety/recovery token handling.
- Added missing risk tokens such as `chore_plan_without_risk`, `safety_deferred`, `self_harm_signal_minimized`, and `thought_homework_without_sleep`.

### Route Token Matching

Problem:

```text
Ending resolver used substring matching. This could create false positives and made multi-token strings fragile.
```

Fix:

- `ContainsAny` now compares exact split route tokens.
- `SplitRouteTokens` now splits on `|`, `,`, `;`, and whitespace.

### Runtime Guard

Problem:

```text
CreateVnRoot called StartsWith on backgroundPath without a null/empty guard.
```

Fix:

- Empty background paths now fall back to `VN/Backgrounds/counseling_room_day`.

### Character Registry

Problem:

```text
FT003-FT010 lacked a central temporary display-name/relationship policy.
```

Fix:

- Added FT003-FT010 temporary display names and relationship/gender constraints to:

```text
Docs/CHARACTER_NAME_REGISTRY.md
```

Notable locked terminology:

- FT009: prefer `산후 보호자`
- FT010: use `누나`, not slash-gender 병기.
- FT004: use `보육기관 담당자`

## Verification

Unity build:

```text
Logs/ft002_ft010_subagent_regression_build_retry.log
Build Finished, Result: Success.
Family Therapy Practicum build result: Succeeded
```

Post-build VN audit:

```text
Logs/ft002_ft010_subagent_regression_postbuild_vn_audit.log
FAMILY_THERAPY_PRACTICUM_VN_DATA_AUDIT
FAMILY_THERAPY_PRACTICUM_CG_SLOT_MANIFEST
```

Post-build smoke:

```text
Logs/ft002_ft010_subagent_regression_postbuild_smoke.log
FAMILY_THERAPY_PRACTICUM_SMOKE completed=true
```

Previous audit snapshot, superseded by the Round 2 and Round 3 summaries below:

| Case | Script | Lines | Choices | Route Tokens | CG Available | CG Missing |
| --- | --- | ---: | ---: | ---: | ---: | ---: |
| FT002 | full case-specific | 30 | 15 | 9/9 | 20/56 | 36 |
| FT003 | focused case-specific | 15 | 15 | 7/7 | 0/41 | 41 |
| FT004 | focused case-specific | 15 | 15 | 7/7 | 0/41 | 41 |
| FT005 | focused case-specific | 15 | 15 | 7/7 | 0/41 | 41 |
| FT006 | focused case-specific | 15 | 15 | 11/11 | 0/41 | 41 |
| FT007 | focused case-specific | 15 | 15 | 11/11 | 0/41 | 41 |
| FT008 | focused case-specific | 16 | 15 | 11/11 | 0/42 | 42 |
| FT009 | focused case-specific | 15 | 15 | 7/7 | 0/41 | 41 |
| FT010 | focused case-specific | 15 | 15 | 11/11 | 0/41 | 41 |

## Still Not Complete

The active objective is not complete.

Remaining blockers:

1. CG production is still mostly missing.
   - At this earlier snapshot, FT002 had 20/56.
   - Current Round 3 baseline is FT002 21/56 and total 385 required / 21 available / 364 missing.
   - FT003-FT010 have no accepted EventCG yet.
2. FT003-FT010 still use compressed 5-turn focused scripts rather than the full route-specific V2/V3 document structure.
3. Endings still use the common six-key ending system rather than every document's full route-specific A-Repaired/C-Repaired/D-Repaired tables.
4. Several family reactions are still third-person summaries, though they are no longer displayed as direct character quotes.
5. FT006, FT010, FT003, and FT008 remain priority candidates for deeper dialogue naturalization.

## Next Recommended Work

Immediate next batch:

## Round 3 Six-Agent Re-Review Patch

Date: 2026-06-10

This round addressed the highest-risk runtime issues raised by the six-agent review.

### Runtime Fixes Applied

1. Added `routeQuality` to session selections.
   - UI/trust scoring can still use theory bonus-adjusted `quality`.
   - Ending resolution now uses raw `routeQuality` so a poor choice cannot become safe simply because the selected theory matched the recommended lens.

2. Added `routeSimulationAudit` to the VN data audit export.
   - The audit now simulates `all_good`, `first_bad_then_good`, `middle_bad_then_good`, `all_worst`, and safety-missing routes.
   - Safety-missing simulation applies to FT008 and FT009 only.
   - Output is embedded in `family_therapy_practicum_vn_data_audit.json`.

3. Tightened common ending logic.
   - `A_integrated` now requires no low-quality choices.
   - Multiple risk tokens route to `C_key_risk_unrepaired`.
   - A single risk token can become `B_repaired` only when later repair/final-high evidence exists.

4. Tightened FT009 safety gate.
   - FT009 now requires `first_cry_contract` in addition to safety screen, crisis contact, baby safety check, support network contact, and written safety plan.

5. Expanded risky-token coverage.
   - FT004 now treats `placating_accepted`, `caregiver_alone_responsible`, and `emotion_bypassed` as risk tokens.
   - FT009 now treats `mindreading_contract`, `permission_loop`, `support_replaces_spouse`, and `spouse_overpromise` as risk tokens.

### Current Known Gaps

The active objective is still not complete.

1. FT002 still needs the V3 route-locked T3-T5 rewrite in runtime.
2. FT003-FT010 still need deeper route-specific expansion beyond the current focused-case scripts.
3. CG production is still the largest asset blocker: current baseline is 385 required, 21 available, 364 missing.
4. FT002 V3 branch-specific runtime implementation remains pending even though the common A/B/C/D route gate now has automated coverage.

### Round 3 Verification

Fresh verification after this patch and the follow-up six-agent review fixes:

```text
Logs/ft002_ft010_round3_build_post_agents.log
Build Finished, Result: Success.
Family Therapy Practicum build result: Succeeded

Logs/ft002_ft010_round3_vn_audit_post_agents.log
FAMILY_THERAPY_PRACTICUM_VN_DATA_AUDIT path=...
FAMILY_THERAPY_PRACTICUM_CG_SLOT_MANIFEST path=...

Logs/ft002_ft010_round3_smoke_post_agents.log
FAMILY_THERAPY_PRACTICUM_SMOKE completed=true
```

Latest `routeSimulationAudit` result:

```text
routeCount: 45
applicableRouteCount: 38
passedApplicableRouteCount: 38
allRoutesPassed: true
```

`-familyTherapyVnDataAudit` now exits nonzero when applicable route simulation fails.

Latest VN audit summary:

| Case | Route Tokens | CG Available | CG Missing |
| --- | ---: | ---: | ---: |
| FT002 | 15/15 | 21/56 | 35 |
| FT003 | 13/13 | 0/41 | 41 |
| FT004 | 15/15 | 0/41 | 41 |
| FT005 | 14/14 | 0/41 | 41 |
| FT006 | 12/12 | 0/41 | 41 |
| FT007 | 15/15 | 0/41 | 41 |
| FT008 | 15/15 | 0/42 | 42 |
| FT009 | 17/17 | 0/41 | 41 |
| FT010 | 11/11 | 0/41 | 41 |

Current CG baseline remains:

```text
385 required / 21 available / 364 missing
```

```text
FT002 T03 batch03
```

Required files:

```text
Assets/Resources/VN/EventCG/FT002/ft002_t03_choice_idle.png
Assets/Resources/VN/EventCG/FT002/ft002_t03_l01_grandmother.png
Assets/Resources/VN/EventCG/FT002/ft002_t03_l02_grandson.png
Assets/Resources/VN/EventCG/FT002/ft002_t03_l03_grandfather.png
Assets/Resources/VN/EventCG/FT002/ft002_t03_l04_grandmother.png
Assets/Resources/VN/EventCG/FT002/ft002_t03_l05_grandson.png
Assets/Resources/VN/EventCG/FT002/ft002_t03_l06_bowen.png
Assets/Resources/VN/EventCG/FT002/ft002_t03_reaction_a_grandmother.png
Assets/Resources/VN/EventCG/FT002/ft002_t03_reaction_b_grandson.png
Assets/Resources/VN/EventCG/FT002/ft002_t03_reaction_c_grandson.png
```

After FT002 T03:

1. FT002 T04 batch
2. FT002 T05 batch
3. FT002 endings batch
4. FT009 CG and route-specific safety resolver pass
5. FT008 CG and narrative safety/consent resolver pass

## Round 2 Six-Agent Re-Review Patch

Time: 2026-06-10 evening.

This round used six parallel review perspectives:

1. family-therapy theory fidelity;
2. branching/token/ending logic;
3. character naming and relationship labels;
4. Korean dialogue realism;
5. EventCG/runtime presentation stability;
6. verification-gate and document consistency.

### Applied Runtime Fixes

- Added `FT-002` to VN audit `focusedCaseIds`.
- Changed CG manifest availability to use the same `LoadVnTexture` fallback path as runtime.
- Added case-specific safety gating:
  - FT-008 requires `safety_check_before_story`.
  - FT-009 requires `safety_screen_started`, `crisis_contact_named`, `baby_safety_check`, `support_network_contacted`, and `safety_plan_written`.
  - FT-006 and FT-010 no longer get falsely forced into `D_safety_unresolved` just because their risk level is high.
- Changed route ending order so risk plus repair resolves to `B_repaired`, not `A_integrated`; two or more low-quality choices now force `D_closed_or_harmful`.
- Expanded required route-token audit coverage for FT-002, FT-003, FT-004, FT-005, and FT-007.
- Added missing resolver buckets for live tokens such as `father_excluded`, `father_blamed`, `child_decision_hidden`, `direct_30sec_contact`, `authority_push`, `mother_bridge_reinforced`, `triangle_named`, and `checklist_connected_to_help`.
- Added supervisor fallback rendering: if a supervisor reaction CG is missing, the active supervisor now appears as the fallback stage subject instead of disappearing behind a family-only stage.
- Added explicit FT-002~FT-010 relationship-label mapping so runtime does not produce labels such as `산후 보호자 · 어머니` or `누나 · 청소년`.

### Applied Dialogue/Naming Fixes

- Replaced the generic intro fallback that made characters speak their metadata role aloud.
- Lowered several theory-summary lines into more natural Korean for FT-002, FT-003, FT-004, FT-005, FT-006, FT-007, FT-008, FT-009, and FT-010.
- Reworded the FT-006 `가족조각` choice into family-facing session language.
- Removed the remaining runtime psychodynamic-jargon family-facing choice phrasing.
- Changed visible FT-004 raw dialogue prefixes from `보호자/배우자` to `어머니/아버지`.
- Changed visible FT-009 raw dialogue prefixes from `보호자` to `산후 보호자`.
- Updated `Docs/CHARACTER_NAME_REGISTRY.md`:
  - FT-004 external ID is now `ft004_institution`.
  - FT-004 child row is present.
- Replaced active FT-010 doc slash-gender mentions with `누나`.
- Marked the old `FT002_REALISTIC_DIALOGUE_EXPANSION_2026-06-10.md` as superseded by V3/runtime.

### Verification

Build:

```text
Logs/ft002_ft010_round2_build.log
Build Finished, Result: Success.
Family Therapy Practicum build result: Succeeded
```

VN data audit:

```text
Logs/ft002_ft010_round2_vn_audit.log
FAMILY_THERAPY_PRACTICUM_VN_DATA_AUDIT
```

Smoke:

```text
Logs/ft002_ft010_round2_smoke.log
FAMILY_THERAPY_PRACTICUM_SMOKE completed=true
```

Round 2 audit summary, superseded by the Round 3 summary above:

| Case | Required CG | Available | Missing | Required route tokens covered |
| --- | ---: | ---: | ---: | ---: |
| FT-002 | 56 | 21 | 35 | 15/15 |
| FT-003 | 41 | 0 | 41 | 13/13 |
| FT-004 | 41 | 0 | 41 | 11/11 |
| FT-005 | 41 | 0 | 41 | 14/14 |
| FT-006 | 41 | 0 | 41 | 11/11 |
| FT-007 | 41 | 0 | 41 | 15/15 |
| FT-008 | 42 | 0 | 42 | 15/15 |
| FT-009 | 41 | 0 | 41 | 13/13 |
| FT-010 | 41 | 0 | 41 | 11/11 |

### Still Not Complete

The active objective is still not complete.

Remaining blockers:

1. FT-003~FT-010 still have no EventCG assets.
2. FT-002 still misses 35 required EventCG slots.
3. FT-002~FT-010 still use the shared focused-case ending key set rather than full per-case route-specific endings.
4. FT-003~FT-010 remain compressed focused scripts, not fully expanded route-specific commercial VN scripts.
5. FT-002 V3 document-level route lock is still richer than the runtime route implementation.
