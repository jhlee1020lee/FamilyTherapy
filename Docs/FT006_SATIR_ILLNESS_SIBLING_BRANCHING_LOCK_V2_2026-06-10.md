# FT-006 Satir Illness Sibling Branching Lock V2

## Status

This V2 supersedes the branching portions of:

```text
Docs/FT006_SATIR_ILLNESS_SIBLING_MAJOR_BRANCHING_SCENARIO_2026-06-10.md
Docs/FT006_REALISTIC_DIALOGUE_EXPANSION_2026-06-10.md
```

The original common intro remains usable. V2 is the production authority for T3, T4, T5, state, ending lock, and CG planning.

## Core Satir Question

FT-006 is not about making the parents feel guilty or making the well sibling compete with the sick child.

The central question is:

```text
When illness fills the family room, can the well sibling's loneliness be seen without treating it as betrayal?
```

## Recurring Visual Anchor

```text
hospital tote bag beside a closed school backpack
```

Supporting objects:

- refrigerator treatment calendar with hospital appointments;
- phone alarm labeled with medication or hospital time;
- untouched school notebook or drawing folder in the well sibling's lap;
- kitchen timer set to 10 minutes;
- mother's tissue held but not used to stop the child;
- father's folded treatment schedule;
- support relative's house-key ring on the table.

The anchor changes meaning by route:

| Route | Anchor Meaning |
| --- | --- |
| `sibling_visible` | hospital reality and sibling life can sit in the same room |
| `illness_totalizing` | hospital bag covers the school backpack |
| `parent_guilt_flood` | tissue and apology pull attention away from the sibling |
| `cheerful_mask` | the closed backpack becomes proof that the sibling can manage alone |

## State Model

```text
route_primary:
  sibling_visible
  illness_totalizing
  parent_guilt_flood
  cheerful_mask

flags:
  sibling_feeling_named
  iceberg_layers_named
  two_feelings_coexist
  iceberg_below_okay_named
  family_sculpture_seen
  family_sculpture_repositioned
  parent_reflects_without_apology
  illness_reality_kept
  sibling_time_scheduled
  support_relative_supports_not_replaces
  repair_started
  repaired_at_t4

  illness_totalized
  treatment_explanation_overused
  sibling_self_erases
  parent_guilt_flooded
  apology_loop_reinforced
  sibling_parentifies
  cheerful_mask_reinforced
  maturity_praised_as_role
  support_relative_substitution
  sibling_need_postponed

  final_confirm_sibling_ritual
  final_confirm_treatment_briefing_trap
  final_confirm_apology_trap
  final_confirm_outsource_trap

scores:
  congruence_score
  iceberg_depth_score
  sibling_visibility_score
  parent_regulation_score
  illness_balance_score
```

## Turn Lock

| Turn | Function | Must Not Collapse |
| --- | --- | --- |
| T1 | route entry | defines "괜찮아요" as hidden feeling, treatment explanation, parent blame, or maturity |
| T2 | family sculpture | shows physical/emotional placement of hospital bag and school backpack |
| T3 | route-specific iceberg pressure | route pressure changes who gets protected |
| T4 | playable Satir intervention | three choices per route, with visible family re-response |
| T5 | final ritual confirmation | ending resolver uses flags and final confirmation traps |

## T3 Choice Consequences

## Family Sculpture Lock

Before T3, every route should preserve the same physical sculpture base:

```text
hospital direction: stage/right or table side with hospital tote and treatment calendar
home/school direction: stage/left or sofa side with closed school backpack
mother: between hospital bag and sibling, body angled toward hospital
father: beside treatment schedule, one hand on folded paper
sibling: beside closed backpack, slightly behind parents' line of sight
support relative: edge of room with key ring, available as resource but not replacement parent
```

Route-specific re-sculpting:

| Route | Re-sculpting Requirement |
| --- | --- |
| `sibling_visible` | parents turn their gaze toward sibling/backpack without moving hospital bag out of the room |
| `illness_totalizing` | treatment schedule spreads over the sibling's school object |
| `parent_guilt_flood` | mother moves toward sibling too quickly and sibling turns toward mother to comfort her |
| `cheerful_mask` | sibling stands straighter and smiles while backpack remains closed |

## Satir Iceberg Layer Lock

Every high or repaired path should make at least four layers playable:

```text
behavior: "괜찮아요", tight smile, closed backpack
coping stance: placating/protective good child
feelings: lonely, worried, angry for a second, scared
feelings-about-feelings: guilt about wanting attention, shame about resenting illness time
perceptions: "병원 이야기가 나오면 내 이야기는 나중이다"
expectations: "나는 이해해야 한다", "부모를 더 힘들게 하면 안 된다"
yearning: "내 하루도 물어봐 줬으면"
self-worth: "내가 힘들다고 말해도 나쁜 아이가 아니다"
```

Low routes can name fewer layers, but must show which layer gets blocked.

### `sibling_visible`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3A-1 name two feelings | `two_feelings_coexist +1`, `iceberg_below_okay_named +1`, `iceberg_layers_named +1`, `sibling_feeling_named +1` | opens high A |
| T3A-2 explain treatment schedule | `treatment_explanation_overused +1`, `illness_totalized +1` | drifts to B |
| T3A-3 ask parents to apologize first | `parent_guilt_flooded +1`, `apology_loop_reinforced +1` | drifts to C |

### `illness_totalizing`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3B-1 bracket illness and ask one feeling | `repair_started +1`, `illness_reality_kept +1`, `sibling_feeling_named +1` | repair possible |
| T3B-2 give more medical explanation | `treatment_explanation_overused +2`, `illness_totalized +2` | locks B unless repaired early |
| T3B-3 postpone sibling talk until stable treatment | `sibling_need_postponed +2`, `sibling_self_erases +1` | B/Low |

### `parent_guilt_flood`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3C-1 regulate guilt and return to sibling | `parent_regulation_score +1`, `sibling_feeling_named +1`, `repair_started +1` | repair possible |
| T3C-2 let parents apologize at length | `parent_guilt_flooded +2`, `apology_loop_reinforced +2` | locks C |
| T3C-3 ask sibling to reassure parents | `sibling_parentifies +2`, `parent_guilt_flooded +1` | C/D low |

### `cheerful_mask`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3D-1 name strength and loneliness together | `two_feelings_coexist +1`, `sibling_feeling_named +1`, `repair_started +1` | repair possible |
| T3D-2 praise maturity more | `cheerful_mask_reinforced +2`, `maturity_praised_as_role +2` | locks D |
| T3D-3 assign relative as emotional listener | `support_relative_substitution +2`, `sibling_need_postponed +1` | D/Low |

## T4 Playable Choice Lock

### T4A `sibling_visible`: Congruent Two-Feeling Statement

```text
A1. "둘째는 '언니가 걱정되지만 집에 혼자 있을 때 외롭다'를 말하고, 부모는 사과보다 먼저 들은 마음을 되돌려주겠습니다."
A2. "부모님이 둘째에게 얼마나 미안했는지 충분히 말하고 안아주겠습니다."
A3. "병원 일정표를 함께 보며 왜 가족 시간이 줄었는지 자세히 설명하겠습니다."
```

Effects:

- A1: `two_feelings_coexist +2`, `parent_reflects_without_apology +2`, `family_sculpture_repositioned +1`, `iceberg_layers_named +1`, `sibling_feeling_named +1`, high A candidate.
- A2: `parent_guilt_flooded +2`, `apology_loop_reinforced +1`, C drift.
- A3: `treatment_explanation_overused +2`, `illness_totalized +1`, B drift.

### T4B `illness_totalizing`: Treatment Reality Without Emotional Erasure

```text
B1. "치료 일정표는 접어두지 않되, 지금 3분은 둘째의 하루와 감정만 듣겠습니다."
B2. "둘째가 불안하지 않도록 치료 상황을 더 자세히 공유하겠습니다."
B3. "치료가 안정될 때까지 둘째 감정 대화는 짧게 확인만 하겠습니다."
```

Effects:

- B1: `repaired_at_t4 = true`, `illness_reality_kept +1`, `sibling_feeling_named +1`, `parent_reflects_without_apology +1`, repair toward A-Repaired.
- B2: `treatment_explanation_overused +2`, `illness_totalized +2`, B.
- B3: `sibling_need_postponed +2`, `sibling_self_erases +1`, B/Low.

### T4C `parent_guilt_flood`: Regulate Parent Guilt

```text
C1. "부모님 죄책감은 옆에 두고, 둘째가 어른을 달래지 않도록 부모가 한 문장만 반영하겠습니다."
C2. "부모님이 둘째에게 충분히 사과하고, 그동안 얼마나 미안했는지 말하겠습니다."
C3. "둘째가 부모님도 힘들었다는 점을 이해한다고 말해보겠습니다."
```

Effects:

- C1: `repaired_at_t4 = true`, `parent_regulation_score +2`, `parent_reflects_without_apology +1`, `sibling_parentifies -1`, repair toward C-Repaired.
- C2: `parent_guilt_flooded +2`, `apology_loop_reinforced +2`, C.
- C3: `sibling_parentifies +2`, `cheerful_mask_reinforced +1`, C/D low.

### T4D `cheerful_mask`: Strength Without Role Trap

```text
D1. "잘 버틴 점과 외로운 점을 동시에 놓겠습니다. 둘째는 강해서 괜찮은 아이가 아니라, 강하면서도 외로운 아이일 수 있습니다."
D2. "둘째가 가족을 위해 해준 일을 부모님이 더 많이 인정하고 칭찬하겠습니다."
D3. "조부모가 둘째의 정서 대화를 맡아 부모 부담을 줄이겠습니다."
```

Effects:

- D1: `repaired_at_t4 = true`, `two_feelings_coexist +2`, `cheerful_mask_reinforced -1`, `sibling_feeling_named +1`, repair toward D-Repaired/A.
- D2: `cheerful_mask_reinforced +2`, `maturity_praised_as_role +2`, D.
- D3: `support_relative_substitution +2`, `sibling_need_postponed +1`, Low/D.

## T5 Final Confirmation Turn

```text
1. "다음 주에는 병원 일정과 별개로 둘째에게만 묻는 10분을 정하겠습니다. 그 시간에는 첫째 치료 보고가 아니라 둘째의 하루, 감정, 필요한 도움만 듣고, 실패하면 누구 잘못인지 따지지 않고 다시 시간을 정합니다."
2. "부모님은 둘째가 불안하지 않게 첫째 치료 상황을 매일 자세히 설명하겠습니다."
3. "부모님은 둘째에게 미안함과 고마움을 충분히 표현하고, 둘째가 이해해준 점을 인정하겠습니다."
4. "부모님 부담이 크니 조부모가 둘째의 마음 대화를 맡고 부모는 치료에 집중하겠습니다."
```

Effects:

- 1: `final_confirm_sibling_ritual = true`, `sibling_time_scheduled +2`, `parent_reflects_without_apology +1`.
- 2: `final_confirm_treatment_briefing_trap = true`, `treatment_explanation_overused +1`, `illness_totalized +1`.
- 3: `final_confirm_apology_trap = true`, `apology_loop_reinforced +1`, `cheerful_mask_reinforced +1`.
- 4: `final_confirm_outsource_trap = true`, `support_relative_substitution +1`, `sibling_need_postponed +1`.

## T5 Ending Resolver

Use this resolver exactly:

```text
if final_confirm_treatment_briefing_trap == true:
    choose B
elif final_confirm_apology_trap == true:
    choose C
elif final_confirm_outsource_trap == true:
    choose Low-Outsourced
elif sibling_time_scheduled >= 2
     and two_feelings_coexist >= 2
     and parent_reflects_without_apology >= 1
     and iceberg_layers_named >= 1
     and illness_totalized < 2
     and parent_guilt_flooded < 2
     and cheerful_mask_reinforced < 2:
    choose A
elif repaired_at_t4 == true
     and sibling_feeling_named >= 2
     and treatment_explanation_overused < 2
     and apology_loop_reinforced < 2
     and maturity_praised_as_role < 2:
    choose A-Repaired
elif parent_regulation_score >= 2
     and sibling_parentifies < 2
     and apology_loop_reinforced < 2:
    choose C-Repaired
elif two_feelings_coexist >= 2
     and cheerful_mask_reinforced < 2
     and maturity_praised_as_role < 2:
    choose D-Repaired
elif illness_totalized >= 2
     or treatment_explanation_overused >= 2
     or sibling_need_postponed >= 2:
    choose B
elif parent_guilt_flooded >= 2
     or apology_loop_reinforced >= 2
     or sibling_parentifies >= 2:
    choose C
elif cheerful_mask_reinforced >= 2
     or maturity_praised_as_role >= 2:
    choose D
else:
    choose Low
```

Resolver regression examples:

```text
T3A-1 -> T4A-1 -> T5-1 = A
Reason: the high path names iceberg layers, practices two-feeling congruence, reflects before apology, and confirms the 10-minute sibling ritual.

T3B-1 -> T4B-2 -> T5-1 = B
Reason: the route began repair, but later medical explanation overload dominates.

T3C-1 -> T4C-2 -> T5-1 = C
Reason: guilt was initially regulated, but extended apology loop makes the sibling protect parents again.

T3D-1 -> T4D-2 -> T5-1 = D
Reason: strength/loneliness was named, but maturity praise at T4 locks the good-child trap.

T3B-2 -> T4B-1 -> T5-1 = B
Reason: one late 3-minute repair does not erase a heavy treatment-totalizing path.
```

## Endings

| Ending | Required Conditions | Blocking Flags | Final Aftereffect |
| --- | --- | --- | --- |
| A. The Well Child Has A Place | two feelings + parent reflection + 10-minute ritual | illness/guilt/maturity traps | sibling's school day enters family speech without competing with illness |
| A-Repaired. The Backpack Opens Late | repaired route with sibling feeling named | explanation/apology/maturity overload | the family recovers enough to schedule a small sibling-only ritual |
| B. Illness Owns The Family | treatment explanation or postponed sibling need dominates | none | hospital bag covers the school backpack again |
| C. Guilt Takes The Room | apology/guilt loop or parentification dominates | no C1 repair | sibling says "괜찮아요" to stop parent tears |
| C-Repaired. Guilt On The Side Chair | parent guilt regulated and sibling not parentified | apology loop | parents hold guilt without making the sibling soothe them |
| D. Good Child Trap | maturity praise dominates | no D1 repair | sibling is praised into silence |
| D-Repaired. Strong And Lonely | strength and loneliness coexist | maturity role lock | sibling can be competent without pretending to be fine |
| Low-Outsourced. The Key Ring Takes Over | relative substitution trap | none | care logistics improve, but parents still do not hear the sibling directly |
| Low. Okay Means Invisible | no strong repair | none | everyone remains kind, but "괜찮아요" closes the room |

## Route-Specific CG Lock

Minimum commercial-comfortable FT-006 image set:

| ID | Scene |
| --- | --- |
| FT006_CG_01 | hospital tote bag beside closed school backpack |
| FT006_CG_02 | refrigerator treatment calendar with sibling school note partly covered |
| FT006_CG_03 | phone hospital alarm interrupting sibling's sentence |
| FT006_CG_04 | sibling holding unopened school notebook/drawing folder |
| FT006_CG_05 | family sculpture: parents angled toward hospital bag, sibling beside backpack |
| FT006_CG_06 | sibling_visible route, parents looking at sibling's empty/side position |
| FT006_CG_07 | illness_totalizing route, father unfolding treatment schedule over table |
| FT006_CG_08 | parent_guilt_flood route, mother with tissue while sibling watches parent |
| FT006_CG_09 | cheerful_mask route, sibling praised while backpack stays closed |
| FT006_CG_10 | T4A high, two-feeling statement with parents listening, no apology interruption |
| FT006_CG_11 | T4B low, hospital schedule covers school notebook |
| FT006_CG_12 | T4C low, parent tears pull sibling forward to comfort |
| FT006_CG_13 | T4D low, family praise makes sibling smile tightly |
| FT006_CG_14 | T4D repaired, "strong and lonely" both visible |
| FT006_CG_15 | T5 high, kitchen timer set to 10 minutes |
| FT006_CG_16 | Ending A, school backpack open beside hospital bag |
| FT006_CG_17 | Ending B, hospital bag covers backpack |
| FT006_CG_18 | Ending C, tissue/apology loop with sibling saying okay |
| FT006_CG_19 | Ending D, good-child smile with closed backpack |
| FT006_CG_20 | Low-Outsourced, relative key ring in foreground while parents remain turned toward hospital |
| FT006_CG_21 | Ending A-Repaired, backpack opens late beside still-visible treatment schedule |
| FT006_CG_22 | Ending C-Repaired, tissue on side chair while parents listen first |
| FT006_CG_23 | Ending D-Repaired, sibling holds both school notebook and hospital concern without forced smile |
| FT006_CG_24 | Low ending, closed backpack and hospital bag remain side by side with no one looking at either |

All CGs must be 1600x900, no baked text, no UI, bottom 25-30% clean.

## Gate Requirement

FT-006 cannot proceed to FT-007 until V2 passes:

- Satir/experiential clinical fidelity review;
- game branching/consequence review;
- commercial VN dialogue review.
