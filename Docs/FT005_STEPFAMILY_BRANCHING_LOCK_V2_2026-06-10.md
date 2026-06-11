# FT-005 Stepfamily Structural Branching Lock V2

## Status

This V2 supersedes the branching portions of:

```text
Docs/FT005_STRUCTURAL_STEPFAMILY_MAJOR_BRANCHING_SCENARIO_2026-06-10.md
Docs/FT005_REALISTIC_DIALOGUE_EXPANSION_2026-06-10.md
```

The original common intro, T1, and T2 remain usable. V2 is the production authority for T3, T4, T5, state, ending lock, and CG planning.

## Core Structural Question

FT-005 is not about forcing respect or asking the adolescent to erase loyalty to the biological father.

The central question is:

```text
When the dinner chair is empty, who tries to fill it, who reads it as rejection, and who is allowed to return to it at their own pace?
```

## Recurring Visual Anchor

```text
empty dinner chair between mother and stepfather
```

Supporting objects:

- adolescent's closed bedroom door;
- mother's hand on the back of the empty chair;
- stepfather's untouched rice bowl or cup;
- adolescent's phone with an unread message from biological father;
- hallway light between dining table and bedroom door;
- three chairs in the session room used for enactment.

The empty chair changes meaning by route:

| Route | Chair Meaning |
| --- | --- |
| `boundary_staging` | a place the adolescent can return to without immediate loyalty demand |
| `authority_push` | proof of disrespect that must be corrected |
| `child_loyalty_only` | protected distance that also leaves stepfather outside |
| `mother_mediation` | a gap mother keeps filling with explanations |

## State Model

```text
route_primary:
  boundary_staging
  authority_push
  child_loyalty_only
  mother_mediation

flags:
  staged_contact_practiced
  stepfather_question_limited
  adolescent_answer_choice_preserved
  mother_translation_reduced
  biological_father_loyalty_named
  stepfather_authority_pushed
  respect_rule_imposed
  child_position_protected_only
  stepfather_excluded
  mother_bridge_reinforced
  mother_observer_role_practiced
  direct_30sec_contact_practiced
  authority_speed_acknowledged
  premature_parent_claim
  no_contact_avoidance
  repaired_at_t4
  final_confirm_staged_boundary
  final_confirm_authority_trap
  final_confirm_mediation_trap

scores:
  boundary_score
  attachment_pace_score
  loyalty_safety_score
  authority_risk_score
  mediation_burden_score
```

## Turn Lock

| Turn | Function | Must Not Collapse |
| --- | --- | --- |
| T1 | route entry | defines conflict as staged boundary, authority, loyalty only, or mother mediation |
| T2 | first enactment | dinner chair / bedroom door scene exposes structure |
| T3 | route-specific pressure scene | loyalty, authority, or mediation pressure changes the empty chair |
| T4 | playable structural intervention | three choices per route, with visible family re-response |
| T5 | final confirmation and ending | ending resolver uses flags and final confirmation traps |

## T3 Choice Consequences

### `boundary_staging`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3A-1 stage small direct contact | `staged_contact_practiced +1`, `mother_translation_reduced +1`, `biological_father_loyalty_named +1` | opens high A |
| T3A-2 require basic greeting | `respect_rule_imposed +1`, `authority_risk_score +1` | drifts to B |
| T3A-3 pause stepfather questions | `no_contact_avoidance +1`, `stepfather_excluded +1` | drifts to C |

### `authority_push`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3B-1 acknowledge authority speed | `authority_speed_acknowledged +1`, `attachment_pace_score +1` | repair possible only if T4B-1 is chosen |
| T3B-2 set greeting/deference rule | `respect_rule_imposed +2`, `stepfather_authority_pushed +2` | locks B unless repaired |
| T3B-3 mother enforces rule | `mother_bridge_reinforced +1`, `respect_rule_imposed +1` | B/D mixed low |

### `child_loyalty_only`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3C-1 name loyalty and keep stepfather in room | `biological_father_loyalty_named +1`, `stepfather_excluded -1` | opens C repaired/A |
| T3C-2 protect child by stepfather backing off | `child_position_protected_only +2`, `stepfather_excluded +2` | locks C |
| T3C-3 mother tells child to accept stepfather | `premature_parent_claim +1`, `authority_risk_score +1` | B/C rupture |

### `mother_mediation`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3D-1 mother observes 30 seconds | `mother_observer_role_practiced +1`, `direct_30sec_contact_practiced +1` | opens D repaired/A |
| T3D-2 mother keeps translating softly | `mother_bridge_reinforced +2`, `mediation_burden_score +2` | locks D |
| T3D-3 mother speaks stepfather's hurt for him | `mother_bridge_reinforced +1`, `stepfather_authority_pushed +1` | D/B low |

## T4 Playable Choice Lock

### T4A `boundary_staging`: Small Contact Or Premature Family Claim

```text
A1. "새아버지는 지시가 아닌 짧은 관심 질문 하나만 하고, 자녀는 대답 길이를 정하고, 어머니는 대신 설명하지 않는 장면을 30초만 실연하겠습니다."
A2. "관계 속도는 존중하되, 한집에 사는 기본 인사는 지켜야 하니 인사 규칙을 먼저 정하겠습니다."
A3. "자녀가 부담을 느끼니 당분간 새아버지는 식사 자리에서 질문하지 않는 것으로 하겠습니다."
```

Effects:

- A1: `direct_30sec_contact_practiced +2`, `stepfather_question_limited +1`, `adolescent_answer_choice_preserved +1`, `mother_translation_reduced +1`, `biological_father_loyalty_named +1`, high A candidate.
- A2: `respect_rule_imposed +2`, B drift.
- A3: `no_contact_avoidance +2`, `stepfather_excluded +1`, C low drift.

### T4B `authority_push`: Repair Authority Speed Or Double Down

```text
B1. "제가 권한을 너무 빨리 세웠습니다. 오늘은 훈육 권한이 아니라, 새아버지가 관계 행동 하나를 안전하게 시작하는 장면으로 되돌리겠습니다."
B2. "자녀가 새아버지를 가족 어른으로 인정하려면 인사와 기본 대답은 반드시 하기로 정하겠습니다."
B3. "어머니가 자녀에게 새아버지의 자리를 분명히 설명하고, 규칙을 지키도록 도와야겠습니다."
```

Effects:

- B1: `repaired_at_t4 = true`, `staged_contact_practiced +1`, `direct_30sec_contact_practiced +1`, `mother_translation_reduced +1`, `authority_risk_score -1`, repair toward A.
- B2: `respect_rule_imposed +2`, `stepfather_authority_pushed +2`, B low.
- B3: `mother_bridge_reinforced +2`, `respect_rule_imposed +1`, D/B low.

### T4C `child_loyalty_only`: Loyalty Without Excluding Stepfather

```text
C1. "친아버지를 지우지 않아도 새아버지와의 관계를 천천히 만들 수 있다는 기준을 세우겠습니다. 새아버지는 아버지 자리를 요구하지 않고, 작은 관심 행동만 시작합니다."
C2. "자녀가 안전해질 때까지 새아버지는 한발 물러서고, 어머니와 자녀 관계를 먼저 안정시키겠습니다."
C3. "어머니가 자녀에게 새아버지도 가족이라는 점을 더 분명히 말해야 합니다."
```

Effects:

- C1: `biological_father_loyalty_named +2`, `staged_contact_practiced +1`, `stepfather_excluded -1`, repaired C/high A candidate.
- C2: `child_position_protected_only +2`, `stepfather_excluded +2`, C low.
- C3: `premature_parent_claim +2`, `authority_risk_score +1`, B/C rupture.

### T4D `mother_mediation`: Mother Steps Aside Or Permanent Bridge

```text
D1. "어머니가 통역하지 않는 30초 대화를 실험하겠습니다. 새아버지는 한 문장만 말하고, 자녀는 짧게 답하거나 답하지 않을 수 있고, 어머니는 관찰만 합니다."
D2. "아직 직접 대화는 위험하니 어머니가 계속 중간에서 정리하되, 말투를 부드럽게 바꿔보겠습니다."
D3. "새아버지가 느끼는 서운함을 어머니가 자녀에게 대신 전달해보겠습니다."
```

Effects:

- D1: `mother_observer_role_practiced +2`, `direct_30sec_contact_practiced +2`, `mother_translation_reduced +1`, repaired D/high A candidate.
- D2: `mother_bridge_reinforced +2`, `mediation_burden_score +2`, D low.
- D3: `mother_bridge_reinforced +2`, `stepfather_authority_pushed +1`, D/B low.

## T5 Final Confirmation Turn

```text
1. "다음 주에는 식사 자리에서 새아버지는 하루 한 번 짧은 관심 질문, 자녀는 대답 길이 선택, 어머니는 대신 번역하지 않고 끝난 뒤 자기 감정 말하기로 마무리하겠습니다."
2. "다음 주부터 자녀는 새아버지에게 인사와 기본 대답을 반드시 하기로 약속하겠습니다."
3. "갈등을 줄이기 위해 어머니가 두 사람 사이 대화를 계속 중재하되, 말투를 부드럽게 바꿔보겠습니다."
```

Effects:

- 1: `final_confirm_staged_boundary = true`, `staged_contact_practiced +1`, `mother_translation_reduced +1`.
- 2: `final_confirm_authority_trap = true`, `respect_rule_imposed +1`, `stepfather_authority_pushed +1`.
- 3: `final_confirm_mediation_trap = true`, `mother_bridge_reinforced +1`, `mediation_burden_score +1`.

## T5 Ending Resolver

Use this resolver exactly:

```text
if final_confirm_authority_trap == true:
    choose B
elif final_confirm_mediation_trap == true:
    choose D
elif staged_contact_practiced >= 2
     and direct_30sec_contact_practiced >= 1
     and mother_translation_reduced >= 1
     and biological_father_loyalty_named >= 1
     and respect_rule_imposed < 2:
    choose A
elif repaired_at_t4 == true
     and staged_contact_practiced >= 1
     and authority_risk_score < 2
     and respect_rule_imposed < 2
     and stepfather_authority_pushed < 2:
    choose A-Repaired
elif biological_father_loyalty_named >= 2
     and stepfather_excluded < 2
     and premature_parent_claim < 2:
    choose C-Repaired
elif mother_observer_role_practiced >= 2
     and direct_30sec_contact_practiced >= 2:
    choose D-Repaired
elif respect_rule_imposed >= 2
     or stepfather_authority_pushed >= 2:
    choose B
elif child_position_protected_only >= 2
     or stepfather_excluded >= 2:
    choose C
elif mother_bridge_reinforced >= 2
     or mediation_burden_score >= 2:
    choose D
else:
    choose Low
```

Resolver regression examples:

```text
T3B-1 -> T4B-2 -> T5-1 = B
Reason: authority speed was only acknowledged at T3, then rule imposition at T4 blocks A-Repaired.

T3B-2 -> T4B-1 -> T5-1 = B
Reason: T4B-1 starts repair, but it does not erase the earlier heavy rule imposition from T3B-2; authority-first damage remains the dominant ending lock.

T3D-1 -> T4D-2 -> T5-1 = D
Reason: early direct-contact attempt does not overcome later mother-bridge reinforcement.
```

## Endings

| Ending | Required Conditions | Blocking Flags | Final Aftereffect |
| --- | --- | --- | --- |
| A. Staged Stepfamily Boundary | staged contact + direct contact + reduced mother translation | `respect_rule_imposed >= 2` or final traps | empty dinner chair becomes available without demand |
| A-Repaired. Authority Slowed Into Contact | authority route repaired at T4 | `authority_risk_score >= 2` or `respect_rule_imposed >= 2` or `stepfather_authority_pushed >= 2` | stepfather gives up immediate parent claim and starts contact |
| B. Authority Before Attachment | authority rule or final authority trap | none | adolescent withdraws behind bedroom door, mother mediates more |
| C. Loyalty Protected, Stepfather Outside | child loyalty protected without stepfather inclusion | no C1 repair | child feels seen but couple split grows |
| C-Repaired. Two Loyalties Can Coexist | biological father loyalty named and stepfather not excluded | none | adolescent can keep biological father loyalty without erasing stepfather |
| D. Mother As Permanent Bridge | mediation bridge reinforced | no D1 repair | mother remains translator and burns out |
| D-Repaired. Mother Steps Aside For 30 Seconds | mother observes, direct contact practiced | none | mother stops filling every silence |
| Low. Empty Chair, Same Dinner | no strong repair | none | the chair is still empty and everyone waits for mother |

## Route-Specific CG Lock

Minimum commercial-comfortable FT-005 image set:

| ID | Scene |
| --- | --- |
| FT005_CG_01 | dinner table with empty chair between mother and stepfather |
| FT005_CG_02 | mother standing behind empty chair, hand on chair back |
| FT005_CG_03 | adolescent at bedroom door, half turned away |
| FT005_CG_04 | stepfather at table with untouched rice bowl/cup |
| FT005_CG_05 | adolescent phone with unread biological father message, kept private |
| FT005_CG_06 | boundary route, 30-second direct question enactment |
| FT005_CG_07 | authority route, stepfather leaning toward empty chair |
| FT005_CG_08 | loyalty route, mother-child closeness with stepfather outside frame |
| FT005_CG_09 | mediation route, mother physically between both |
| FT005_CG_10 | T4A high, mother observing rather than translating |
| FT005_CG_11 | T4B low, greeting rule imposed and bedroom door closes |
| FT005_CG_12 | T4C repaired, biological father loyalty acknowledged without excluding stepfather |
| FT005_CG_13 | T4D repaired, mother lets a silence stand |
| FT005_CG_14 | Ending A, chair remains open without demand |
| FT005_CG_15 | Low ending, same empty dinner chair |
| FT005_CG_16 | Ending B, bedroom door closes after greeting rule |
| FT005_CG_17 | Ending C, mother-child closeness with stepfather isolated |
| FT005_CG_18 | Ending C-Repaired, biological father phone/loyalty present while stepfather remains in frame |
| FT005_CG_19 | Ending D, mother physically centered and exhausted as permanent bridge |
| FT005_CG_20 | Ending D-Repaired, mother seated back while a short silence holds |

All CGs must be 1600x900, no baked text, no UI, bottom 25-30% clean.

## Gate Requirement

FT-005 cannot proceed to FT-006 until V2 passes:

- structural/stepfamily therapy fidelity review;
- game branching/consequence review;
- commercial VN dialogue review.
