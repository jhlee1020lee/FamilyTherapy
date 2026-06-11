# FT-010 Solution-Focused Branching Lock V2

## Status

This V2 supersedes the branching portions of:

```text
Docs/FT010_SOLUTION_PARENTIFICATION_MAJOR_BRANCHING_SCENARIO_2026-06-10.md
Docs/FT010_REALISTIC_DIALOGUE_EXPANSION_2026-06-10.md
```

The original common intro remains usable. V2 is the production authority for T3, T4, T5, state, ending lock, and CG planning.

## Core Solution-Focused Question

FT-010 is not about praising the adolescent caregiver into more work, making the ill guardian confess guilt, or replacing family choice with outside services.

The central question is:

```text
When the family has survived by making the adolescent indispensable, can they find one already-working exception and turn it into a chosen 1-point relief experiment?
```

## Recurring Visual Anchor

```text
school backpack beside sibling medication checklist, unfinished homework, and a fridge calendar
```

Supporting objects:

- high-school backpack with club pin partly hidden;
- younger sibling's medication/checklist sheet with no readable text;
- unfinished homework open beside dinner bowl;
- fridge calendar with blank colored blocks, no readable dates;
- neighbor/teacher contact card with no readable text;
- guardian's pill case and water cup;
- younger sibling's self-packed school pouch;
- 0-10 scaling card with blank scale marks, no numbers required in image;
- 30-minute self-time card;
- supervisor notebook with "exception -> scale -> 1-point next step -> recovery rule".

The anchor changes meaning by route:

| Route | Anchor Meaning |
| --- | --- |
| `one_point_relief` | backpack and checklist can both exist without the adolescent carrying all of it |
| `hero_burden` | backpack disappears under praise and duty |
| `guilt_centered` | guardian's pill case and tears pull the adolescent back into comforter role |
| `resource_takeover` | outside support card covers the family's own exception and choice |

## State Model

```text
route_primary:
  one_point_relief
  hero_burden
  guilt_centered
  resource_takeover

flags:
  strengths_acknowledged_without_romanticizing
  parentification_risk_named
  exception_identified
  exception_thickened
  scale_number_named
  one_point_goal_set
  adolescent_self_time_named
  sibling_small_task_named
  guardian_small_task_named
  chosen_support_contacted
  family_choice_preserved
  recovery_rule_written
  repair_started
  repaired_at_t4

  hero_praise_reinforced
  sacrifice_moralized
  adolescent_role_locked
  guardian_guilt_centered
  adolescent_comforts_guardian
  guilt_replaces_action
  external_support_overrides_choice
  family_exception_ignored
  too_large_change_plan
  sibling_dependence_reinforced
  support_avoided

  final_confirm_one_point_experiment
  final_confirm_hero_maintenance_trap
  final_confirm_guilt_apology_trap
  final_confirm_resource_takeover_trap
  final_confirm_delay_support_trap

scores:
  sf_exception_score
  scaling_score
  parentification_relief_score
  family_choice_score
  romanticizing_risk_score
```

## Turn Lock

| Turn | Function | Must Not Collapse |
| --- | --- | --- |
| T1 | route entry | defines the issue as 1-point relief, heroic caregiver, guardian guilt, or external rescue |
| T2 | route-specific exception search | shows already-working moments or how the route blocks them |
| T3 | scaling and coping pressure | route changes whether scale becomes relief or burden |
| T4 | playable 1-point experiment design | three choices per route with visible family re-response |
| T5 | next-week experiment confirmation | resolver uses flags and final traps |

## T3 Choice Consequences

### `one_point_relief`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3A-1 thicken the 30-minute exception and scale from 8 to 7 | `strengths_acknowledged_without_romanticizing +1`, `parentification_risk_named +1`, `exception_identified +1`, `exception_thickened +1`, `scale_number_named +1`, `one_point_goal_set +1` | opens A |
| T3A-2 make the adolescent use the 30 minutes productively | `hero_praise_reinforced +1`, `sacrifice_moralized +1` | drifts B |
| T3A-3 schedule external support before family chooses scope | `external_support_overrides_choice +1`, `family_choice_preserved -1` | drifts D |

### `hero_burden`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3B-1 repair praise into exception and cost | `repair_started +1`, `strengths_acknowledged_without_romanticizing +1`, `parentification_risk_named +1` | repair possible |
| T3B-2 use adolescent's responsibility as main resource | `hero_praise_reinforced +2`, `adolescent_role_locked +2` | locks B |
| T3B-3 ask family to thank adolescent more often | `sacrifice_moralized +2`, `adolescent_role_locked +1` | B low |

### `guilt_centered`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3C-1 turn guilt into one 10-minute guardian task | `repair_started +1`, `guardian_small_task_named +1`, `adolescent_comforts_guardian -1` | repair possible |
| T3C-2 let guardian apologize fully before planning | `guardian_guilt_centered +2`, `adolescent_comforts_guardian +2` | locks C |
| T3C-3 tell adolescent not to reassure guardian | `guilt_replaces_action +1`, `adolescent_role_locked +1` | C/B low |

### `resource_takeover`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3D-1 choose one support that preserves the existing 30-minute exception | `repair_started +1`, `chosen_support_contacted +1`, `family_choice_preserved +1` | repair possible |
| T3D-2 maximize outside support quickly | `external_support_overrides_choice +2`, `family_exception_ignored +2` | locks D |
| T3D-3 delay support until family is ready | `support_avoided +2`, `adolescent_role_locked +1` | D/B low |

## T4 Playable Choice Lock

### T4A `one_point_relief`: One-Point Relief Experiment

```text
A1. "지난주 가능했던 30분을 다음 주 한 번 더 만들겠습니다. 동생은 준비물 하나, 보호자는 약 먹은 뒤 10분, 지원 인물은 주 1회 하교 동행 가능 여부 확인, 청소년은 그 30분에 자기 일을 하나만 합니다."
A2. "30분을 확보하면 청소년이 밀린 숙제와 집안일을 더 효율적으로 정리하도록 하겠습니다."
A3. "지원 인물이 가능한 도움을 먼저 정리하고 가족은 그 목록에서 선택하겠습니다."
```

Effects:

- A1: `strengths_acknowledged_without_romanticizing +1`, `parentification_risk_named +1`, `adolescent_self_time_named +1`, `sibling_small_task_named +1`, `guardian_small_task_named +1`, `chosen_support_contacted +1`, `family_choice_preserved +1`, high A candidate.
- A2: `hero_praise_reinforced +1`, `sacrifice_moralized +1`, `adolescent_role_locked +1`, B drift.
- A3: `external_support_overrides_choice +2`, `family_exception_ignored +1`, D drift.

### T4B `hero_burden`: Strength Without Making Sacrifice Sacred

```text
B1. "제가 책임감을 칭찬하다가 계속 혼자 하라는 말처럼 만들었습니다. 해낸 것은 인정하되, 그 힘이 덜 필요했던 30분을 다시 찾겠습니다."
B2. "청소년이 가족을 잘 지켜온 만큼, 다음 주도 중심 역할은 유지하되 스트레스를 덜 받는 방법을 찾겠습니다."
B3. "가족이 청소년에게 고마움을 더 자주 말해 부담을 알아주겠습니다."
```

Effects:

- B1: `repaired_at_t4 = true`, `strengths_acknowledged_without_romanticizing +1`, `parentification_risk_named +1`, `exception_identified +1`, repair toward A-Repaired.
- B2: `hero_praise_reinforced +2`, `adolescent_role_locked +2`, B.
- B3: `sacrifice_moralized +2`, `adolescent_role_locked +1`, B low.

### T4C `guilt_centered`: Guilt Into A Small Action

```text
C1. "죄책감은 말로 오래 두지 않고, 보호자가 약 먹은 뒤 10분 숙제 옆에 있기라는 작은 행동으로 바꾸겠습니다."
C2. "보호자가 충분히 미안함을 말하고, 청소년이 그 마음을 들은 뒤 계획으로 넘어가겠습니다."
C3. "청소년이 보호자를 달래지 않도록, 보호자는 미안하다는 말을 줄이겠습니다."
```

Effects:

- C1: `repaired_at_t4 = true`, `guardian_small_task_named +2`, `guilt_replaces_action -1`, `adolescent_comforts_guardian -1`, repair toward C-Repaired/A.
- C2: `guardian_guilt_centered +2`, `adolescent_comforts_guardian +2`, C.
- C3: `guilt_replaces_action +1`, `adolescent_role_locked +1`, C/B low.

### T4D `resource_takeover`: Support That Preserves Choice

```text
D1. "외부 지원은 연결하되, 가족이 이미 경험한 30분 예외를 유지하는 작은 도움 하나만 고르겠습니다. 지원 인물은 주 1회 하교 동행 가능 여부만 확인합니다."
D2. "청소년 부담이 크므로 가능한 지원을 많이 넣어 역할을 빠르게 줄이겠습니다."
D3. "갑작스러운 지원은 부담스러우니 외부 지원은 보류하고 가족 내부 대화만 먼저 하겠습니다."
```

Effects:

- D1: `repaired_at_t4 = true`, `chosen_support_contacted +2`, `family_choice_preserved +2`, `exception_thickened +1`, repair toward D-Repaired/A.
- D2: `external_support_overrides_choice +2`, `family_exception_ignored +2`, D.
- D3: `support_avoided +2`, `adolescent_role_locked +1`, D/B low.

## T5 Final Confirmation Turn

```text
1. "다음 주 과제는 부담을 1점 낮추는 실험입니다. 성공 기준은 완벽한 돌봄이 아니라 청소년이 자기 시간 30분을 확보했는지입니다. 실패하면 누가 잘못했는지 따지지 않고, 어느 연결이 끊겼는지 다시 찾습니다."
2. "청소년이 지금까지 해온 강점을 유지하되, 스트레스를 덜 받는 방식으로 돌봄을 계속해보겠습니다."
3. "보호자의 미안함을 가족이 충분히 나누고, 행동 계획은 그 마음이 정리된 뒤 세우겠습니다."
4. "외부 지원을 우선 많이 연결하고, 가족은 지원을 받는 데 익숙해지는 것을 목표로 하겠습니다."
5. "가족이 준비될 때까지 외부 지원은 보류하고 내부 대화만 먼저 이어가겠습니다."
```

Effects:

- 1: `final_confirm_one_point_experiment = true`, `one_point_goal_set +1`, `adolescent_self_time_named +1`, `recovery_rule_written +1`, `family_choice_preserved +1`.
- 2: `final_confirm_hero_maintenance_trap = true`, `hero_praise_reinforced +2`, `adolescent_role_locked +1`.
- 3: `final_confirm_guilt_apology_trap = true`, `guardian_guilt_centered +2`, `adolescent_comforts_guardian +1`.
- 4: `final_confirm_resource_takeover_trap = true`, `external_support_overrides_choice +2`, `family_exception_ignored +1`.
- 5: `final_confirm_delay_support_trap = true`, `support_avoided +2`, `adolescent_role_locked +1`.

## T5 Ending Resolver

Use this resolver exactly:

```text
if final_confirm_hero_maintenance_trap == true:
    choose B
elif final_confirm_guilt_apology_trap == true:
    choose C
elif final_confirm_resource_takeover_trap == true:
    choose D
elif final_confirm_delay_support_trap == true:
    choose D
elif hero_praise_reinforced >= 2
     or sacrifice_moralized >= 2
     or adolescent_role_locked >= 2:
    choose B
elif guardian_guilt_centered >= 2
     or adolescent_comforts_guardian >= 2
     or guilt_replaces_action >= 2:
    choose C
elif external_support_overrides_choice >= 2
     or family_exception_ignored >= 2
     or support_avoided >= 2:
    choose D
elif route_primary == hero_burden
     and repaired_at_t4 == true
     and final_confirm_one_point_experiment == true
     and repair_started >= 1
     and strengths_acknowledged_without_romanticizing >= 1
     and parentification_risk_named >= 1
     and exception_identified >= 1
     and adolescent_self_time_named >= 1
     and recovery_rule_written >= 1
     and hero_praise_reinforced < 2
     and adolescent_role_locked < 2:
    choose A-Repaired
elif route_primary == guilt_centered
     and repaired_at_t4 == true
     and final_confirm_one_point_experiment == true
     and repair_started >= 1
     and guardian_small_task_named >= 2
     and adolescent_self_time_named >= 1
     and recovery_rule_written >= 1
     and guardian_guilt_centered < 2
     and adolescent_comforts_guardian < 2:
    choose C-Repaired
elif route_primary == resource_takeover
     and repaired_at_t4 == true
     and final_confirm_one_point_experiment == true
     and repair_started >= 1
     and chosen_support_contacted >= 2
     and family_choice_preserved >= 2
     and exception_thickened >= 1
     and recovery_rule_written >= 1
     and external_support_overrides_choice < 2
     and family_exception_ignored < 2:
    choose D-Repaired
elif route_primary == one_point_relief
     and final_confirm_one_point_experiment == true
     and strengths_acknowledged_without_romanticizing >= 1
     and parentification_risk_named >= 1
     and exception_identified >= 1
     and exception_thickened >= 1
     and scale_number_named >= 1
     and one_point_goal_set >= 1
     and adolescent_self_time_named >= 1
     and sibling_small_task_named >= 1
     and guardian_small_task_named >= 1
     and chosen_support_contacted >= 1
     and family_choice_preserved >= 1
     and recovery_rule_written >= 1
     and hero_praise_reinforced < 2
     and guardian_guilt_centered < 2
     and external_support_overrides_choice < 2:
    choose A
else:
    choose Low
```

Resolver regression examples:

```text
T3A-1 -> T4A-1 -> T5-1 = A
Reason: exception, scale, 1-point goal, adolescent self-time, family tasks, chosen support, and recovery rule are all present.

T3B-1 -> T4B-1 -> T5-1 = A-Repaired
Reason: praise is repaired into parentification-risk naming and exception search.

T3B-2 -> T4B-1 -> T5-1 = B
Reason: a late repair cannot erase strongly reinforced heroic burden.

T3C-1 -> T4C-1 -> T5-1 = C-Repaired
Reason: guardian guilt becomes a small action but still resolves as guilt-repaired.

T3C-2 -> T4C-1 -> T5-1 = C
Reason: a late small action cannot erase centered guilt and adolescent comforting.

T3D-1 -> T4D-1 -> T5-1 = D-Repaired
Reason: external support is repaired into family-chosen support preserving the exception.

T3D-2 -> T4D-1 -> T5-1 = D
Reason: a late D1 repair cannot erase external support overriding family choice.

T3A-1 -> T4A-1 -> T5-2 = B
Reason: final hero-maintenance trap overrides a good one-point plan.
```

## Endings

| Ending | Required Conditions | Blocking Flags | Final Aftereffect |
| --- | --- | --- | --- |
| A. One Point Less | exception + scale + 1-point goal + adolescent self-time + family small tasks + chosen support + recovery rule | hero/guilt/resource traps | family keeps strengths while adolescent burden drops one point |
| A-Repaired. Strength Without Sacred Sacrifice | hero_burden repaired into parentification risk and exception | reinforced hero role | adolescent's competence is honored without making sacrifice permanent |
| B. Praised Into More Work | heroic praise or role lock dominates | no B1 repair | adolescent receives praise and more work |
| C. Guilt Replaces Action | guardian guilt or adolescent comforting dominates | no C1 repair | adolescent returns to comforting the guardian |
| C-Repaired. Guilt Becomes Ten Minutes | guilt_centered repaired into guardian's small task | centered apology, no action | guardian has one small action that lowers burden |
| D. Help Without Ownership | external support overrides family choice or support is avoided | no D1 repair | support is either imposed or absent |
| D-Repaired. Chosen Help Preserves The Exception | resource_takeover repaired into chosen support that keeps the 30-minute exception | support takeover | outside help supports family choice rather than replacing it |
| Low. Same Backpack | no strong experiment or repair | none | backpack, checklist, homework, and calendar remain unchanged |

## Route-Specific CG Lock

Minimum commercial-comfortable FT-010 image set:

Use the production command below before image generation:

```text
Docs/FT010_COMMERCIAL_CG_GENERATION_COMMAND_2026-06-10.md
```

| ID | Scene |
| --- | --- |
| FT010_CG_01 | school backpack beside medication checklist, unfinished homework, and fridge calendar, no readable text |
| FT010_CG_02 | adolescent caregiver returning from school, backpack still on, preparing sibling care |
| FT010_CG_03 | ill guardian with pill case and water cup, guilty but not helpless caricature |
| FT010_CG_04 | younger sibling with self-packed pouch, dependent but capable |
| FT010_CG_05 | neighbor/teacher support card beside fridge calendar, no readable text |
| FT010_CG_06 | one_point_relief route, 30-minute exception visible through backpack set aside |
| FT010_CG_07 | hero_burden route, praise makes backpack sink under checklist |
| FT010_CG_08 | guilt_centered route, guardian emotion pulls adolescent back toward comforter role |
| FT010_CG_09 | resource_takeover route, support card covers family calendar |
| FT010_CG_10 | T4A high, 30-minute self-time card with blank text area |
| FT010_CG_11 | T4B low, adolescent praised while still holding all care objects |
| FT010_CG_12 | T4C low, guardian apology centered while tasks remain unchanged |
| FT010_CG_13 | T4D low, outside support list dominates the table |
| FT010_CG_14 | T4D repaired, family chooses one small support while keeping calendar visible |
| FT010_CG_15 | T5 high, blank 1-point experiment card and recovery rule card |
| FT010_CG_16 | Ending A, adolescent has 30 minutes with homework/club item while family handles small tasks |
| FT010_CG_17 | Ending A-Repaired, praise card replaced by exception card |
| FT010_CG_18 | Ending B, adolescent praised into more work |
| FT010_CG_19 | Ending C, guardian guilt pulls adolescent into comforter posture |
| FT010_CG_20 | Ending C-Repaired, guardian does 10-minute homework support |
| FT010_CG_21 | Ending D, support arrives as imposed list |
| FT010_CG_22 | Ending D-Repaired, chosen support preserves family ownership |
| FT010_CG_23 | Low ending, same backpack/checklist/homework/calendar |
| FT010_CG_24 | supervisor 송지후-centered debrief, exception/scale/1-point change tone |

All CGs must be 1600x900, no baked text, no UI, bottom 25-30% clean.

## Gate Requirement

FT-010 cannot be marked complete until V2 passes:

- solution-focused fidelity / parentification safety review;
- game branching/consequence review;
- commercial VN dialogue and CG readiness review.
