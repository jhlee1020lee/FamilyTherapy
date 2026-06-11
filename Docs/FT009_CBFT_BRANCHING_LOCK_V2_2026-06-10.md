# FT-009 CBFT Branching Lock V2

## Status

This V2 supersedes the branching portions of:

```text
Docs/FT009_CBFT_POSTPARTUM_MAJOR_BRANCHING_SCENARIO_2026-06-10.md
Docs/FT009_REALISTIC_DIALOGUE_EXPANSION_2026-06-10.md
```

The original common intro remains usable. V2 is the production authority for T3, T4, T5, state, ending lock, and CG planning.

## Clinical Safety Note

FT-009 is a training-game scene, not a medical protocol. However, because the case includes postpartum depression and possible self-harm thoughts, the route must never reward skipping safety assessment.

Use this rule in every implementation:

```text
If the postpartum parent mentions disappearing, dying, harming self, harming the baby, feeling unsafe alone, hallucinations, delusional certainty, or command-like thoughts, safety assessment and risk-tailored support must take priority over communication skills or chore planning.
```

## Core CBFT Question

FT-009 is not about deciding whether the spouse is lazy or whether the postpartum parent is too sensitive.

The central question is:

```text
When exhaustion creates automatic thoughts, can the family interrupt the thought-feeling-behavior-sleep loop with safety support and a tiny observable care contract tonight?
```

## Recurring Visual Anchor

```text
night feeding bottle beside an untouched meal tray, unread family group-chat messages, and a folded clinic leaflet
```

Supporting objects:

- dim nursery lamp at 2:07 a.m.;
- empty water cup beside the postpartum parent;
- spouse's phone alarm that is set but not trusted yet;
- diaper stack and burp cloth on the sofa arm;
- handwritten card with two automatic thoughts;
- clinic leaflet with unread screening/support section;
- family group-chat screen with unread support offers, no readable text;
- three-line night contract card;
- emergency/support contact card placed beside the bottle;
- supervisor notebook with "thought -> feeling -> behavior -> sleep" cycle.

The anchor changes meaning by route:

| Route | Anchor Meaning |
| --- | --- |
| `cycle_mapped` | bottle, meal tray, and alarm become observable parts of a shared night loop |
| `blame_loop` | bottle becomes proof in a trial about who failed |
| `comfort_only` | meal tray is acknowledged with sympathy but no one knows tonight's next action |
| `risk_uncontained` | clinic leaflet and support card remain folded while risk language is softened |

## State Model

```text
route_primary:
  cycle_mapped
  blame_loop
  comfort_only
  risk_uncontained

flags:
  safety_screen_started
  safety_plan_written
  crisis_contact_named
  baby_safety_check
  thought_cycle_mapped
  automatic_thoughts_named
  behavior_chain_mapped
  sleep_protection_named
  request_sentence_written
  spouse_task_observable
  night_contract_written
  failure_retry_rule_written
  support_network_contacted
  repair_started
  repaired_at_t4

  spouse_blamed_as_cause
  parent_labeled_overreacting
  mindreading_expectation_reinforced
  reassurance_only
  safety_deferred
  self_harm_signal_minimized
  baby_risk_signal_minimized
  vague_help_promise
  all_night_spouse_takeover
  monitoring_as_control
  thought_homework_without_sleep
  family_support_avoided

  final_confirm_three_line_plan
  final_confirm_total_takeover_trap
  final_confirm_thought_log_only_trap
  final_confirm_reassurance_trap
  final_confirm_safety_delay_trap

scores:
  cbft_cycle_score
  behavior_contract_score
  safety_priority_score
  blame_risk_score
  comfort_without_action_risk
```

## Turn Lock

| Turn | Function | Must Not Collapse |
| --- | --- | --- |
| T1 | route entry | defines problem as behavior cycle, spouse failure, maternal failure belief, or delayed safety |
| T2 | route-specific chain | shows night behavior, attribution, comfort-only, or safety rupture |
| T3 | automatic thought and reinforcement pressure | route pressure changes whether thoughts become behavior targets or accusations |
| T4 | playable safety + care contract turn | three choices per route with visible family re-response |
| T5 | tonight plan confirmation | ending resolver uses flags and final traps |

## T3 Choice Consequences

### `cycle_mapped`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3A-1 map thought-feeling-behavior-sleep loop | `thought_cycle_mapped +1`, `automatic_thoughts_named +1`, `behavior_chain_mapped +1` | opens A |
| T3A-2 make spouse wake first no matter what | `all_night_spouse_takeover +1`, `vague_help_promise +1` | drifts B/C |
| T3A-3 focus on positive self-talk before tasks | `reassurance_only +1`, `thought_homework_without_sleep +1` | drifts C |

### `blame_loop`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3B-1 repair blame into mutual automatic thoughts | `repair_started +1`, `thought_cycle_mapped +1`, `spouse_blamed_as_cause -1` | repair possible |
| T3B-2 require spouse accountability first | `spouse_blamed_as_cause +2`, `parent_labeled_overreacting +1` | locks B |
| T3B-3 teach postpartum parent to lower expectations | `parent_labeled_overreacting +2`, `mindreading_expectation_reinforced +1` | B/Low |

### `comfort_only`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3C-1 turn "asking means failure" into behavior experiment | `repair_started +1`, `automatic_thoughts_named +1`, `request_sentence_written +1` | repair possible |
| T3C-2 offer repeated reassurance from spouse | `reassurance_only +2`, `comfort_without_action_risk +1` | locks C |
| T3C-3 assign positive self-statement homework | `thought_homework_without_sleep +2`, `comfort_without_action_risk +1` | C low |

### `risk_uncontained`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3D-1 stop and assess risk, contacts, baby safety, and tonight supervision | `repair_started +1`, `safety_screen_started +1`, `crisis_contact_named +1`, `baby_safety_check +1` | repair possible |
| T3D-2 return to couple communication after acknowledging risk | `safety_deferred +2`, `self_harm_signal_minimized +2` | locks D |
| T3D-3 spouse promises to check more often | `monitoring_as_control +1`, `vague_help_promise +1`, `safety_deferred +1` | D/B low |

## T4 Playable Choice Lock

### T4A `cycle_mapped`: Safety First, Then Tiny Night Contract

```text
A1. "먼저 위험 신호, 연락 계획, 아기 안전 순서를 확인하고 가족 단체방의 실제 지원자 한 명에게 오늘 밤 대기 가능 여부를 묻겠습니다. 그 뒤 첫 울음에서 배우자가 맡을 행동 하나와 보호자의 요청 문장 하나를 적겠습니다."
A2. "오늘 밤은 배우자가 전부 맡고 보호자는 완전히 쉬는 것으로 크게 바꾸겠습니다."
A3. "자동사고를 더 정확히 적는 과제를 내고 행동계약은 다음 회기에 하겠습니다."
```

Effects:

- A1: `safety_screen_started +1`, `safety_plan_written +1`, `crisis_contact_named +1`, `baby_safety_check +1`, `support_network_contacted +1`, `spouse_task_observable +2`, `request_sentence_written +1`, `night_contract_written +1`, high A candidate.
- A2: `all_night_spouse_takeover +2`, `vague_help_promise +1`, B/C drift.
- A3: `thought_homework_without_sleep +2`, `comfort_without_action_risk +1`, C drift.

### T4B `blame_loop`: Attribution Repair Without Excusing Inaction

```text
B1. "제가 배우자 책임처럼 말했습니다. 이제 '안 한다'와 '틀릴까 봐 멈춘다'를 구분하고, 멈춤을 깨는 관찰 가능한 행동 하나를 정하겠습니다."
B2. "배우자분은 오늘부터 먼저 움직여야 합니다. 보호자분이 설명하지 않아도 필요한 일을 찾아야 합니다."
B3. "보호자분도 배우자 방식이 다를 수 있음을 받아들이고 지적을 줄이겠습니다."
```

Effects:

- B1: `repaired_at_t4 = true`, `thought_cycle_mapped +1`, `spouse_task_observable +1`, `request_sentence_written +1`, repair toward A-Repaired.
- B2: `spouse_blamed_as_cause +3`, `vague_help_promise +1`, B.
- B3: `parent_labeled_overreacting +2`, `mindreading_expectation_reinforced +1`, B low.

### T4C `comfort_only`: Comfort Becomes Behavior Experiment

```text
C1. "좋은 부모도 힘들 수 있다는 확인에서 멈추지 않고, '20분만 맡아줘'를 실패가 아니라 회복 행동으로 실험하겠습니다."
C2. "배우자분이 하루에 세 번 좋은 부모라고 말해 보호자분의 죄책감을 낮추겠습니다."
C3. "보호자분이 좋은 부모라는 증거를 매일 기록해오겠습니다."
```

Effects:

- C1: `repaired_at_t4 = true`, `automatic_thoughts_named +1`, `request_sentence_written +2`, `sleep_protection_named +1`, repair toward C-Repaired/A.
- C2: `reassurance_only +2`, `comfort_without_action_risk +2`, C.
- C3: `thought_homework_without_sleep +2`, `comfort_without_action_risk +1`, C low.

### T4D `risk_uncontained`: Risk-Tailored Support Before Contract

```text
D1. "방금 위험 신호가 나왔으므로 행동계약 전에 혼자 위험해지는 순간, 오늘 밤 혼자 있지 않을 시간, 연락할 사람, 가족 단체방에서 실제로 대기할 지원자, 아기 안전을 먼저 정하겠습니다."
D2. "위험 생각은 기록해두고, 오늘은 부부가 서로 덜 상처 주는 말부터 연습하겠습니다."
D3. "배우자분이 수시로 괜찮냐고 확인해 위험을 낮추겠습니다."
```

Effects:

- D1: `repaired_at_t4 = true`, `safety_screen_started +2`, `safety_plan_written +2`, `crisis_contact_named +1`, `baby_safety_check +1`, `support_network_contacted +1`, repair toward D-Repaired/A.
- D2: `safety_deferred +2`, `self_harm_signal_minimized +2`, D.
- D3: `monitoring_as_control +2`, `vague_help_promise +1`, `safety_deferred +1`, D/B low.

## T5 Final Confirmation Turn

```text
1. "오늘 밤 계획은 세 줄입니다. 위험 신호가 올라오면 아기를 안전한 곳에 눕히고 혼자 버티지 않은 채 정한 연락처와 오늘 대기하기로 한 가족 지원자에게 연락합니다. 첫 울음은 배우자가 기저귀와 물 준비를 맡습니다. 보호자는 '지금 20분만 맡아줘'라고 말하고, 실패하면 다음 울음 때 같은 계획으로 다시 시도합니다."
2. "오늘 밤은 배우자분이 전부 맡고 보호자분은 완전히 자는 것으로 정하겠습니다."
3. "오늘 나온 자동사고를 각자 기록하고, 다음 회기에 생각이 얼마나 바뀌었는지 확인하겠습니다."
4. "위험 생각은 다음 회기에 더 자세히 보고, 오늘은 부부 대화 방식부터 정리하겠습니다."
5. "서로에게 좋은 부모라고 말하는 시간을 정하고, 행동계획은 부담이 줄어든 뒤에 하겠습니다."
```

Effects:

- 1: `final_confirm_three_line_plan = true`, `safety_screen_started +1`, `safety_plan_written +1`, `crisis_contact_named +1`, `baby_safety_check +1`, `support_network_contacted +1`, `spouse_task_observable +1`, `request_sentence_written +1`, `failure_retry_rule_written +1`, `night_contract_written +1`.
- 2: `final_confirm_total_takeover_trap = true`, `all_night_spouse_takeover +2`, `vague_help_promise +1`.
- 3: `final_confirm_thought_log_only_trap = true`, `thought_homework_without_sleep +2`, `comfort_without_action_risk +1`.
- 4: `final_confirm_safety_delay_trap = true`, `safety_deferred +2`, `self_harm_signal_minimized +2`.
- 5: `final_confirm_reassurance_trap = true`, `reassurance_only +2`, `comfort_without_action_risk +1`.

## T5 Ending Resolver

Use this resolver exactly:

```text
if final_confirm_safety_delay_trap == true:
    choose D
elif final_confirm_total_takeover_trap == true:
    choose B
elif final_confirm_thought_log_only_trap == true:
    choose C
elif final_confirm_reassurance_trap == true:
    choose C
elif safety_deferred >= 2
     or self_harm_signal_minimized >= 2
     or baby_risk_signal_minimized >= 2:
    choose D
elif route_primary == blame_loop
     and repaired_at_t4 == true
     and final_confirm_three_line_plan == true
     and safety_screen_started >= 1
     and safety_plan_written >= 1
     and crisis_contact_named >= 1
     and baby_safety_check >= 1
     and support_network_contacted >= 1
     and thought_cycle_mapped >= 1
     and spouse_task_observable >= 1
     and request_sentence_written >= 1
     and spouse_blamed_as_cause < 2
     and parent_labeled_overreacting < 2
     and mindreading_expectation_reinforced < 2
     and safety_deferred < 2:
    choose A-Repaired
elif route_primary == comfort_only
     and repaired_at_t4 == true
     and final_confirm_three_line_plan == true
     and safety_screen_started >= 1
     and safety_plan_written >= 1
     and crisis_contact_named >= 1
     and baby_safety_check >= 1
     and support_network_contacted >= 1
     and request_sentence_written >= 2
     and sleep_protection_named >= 1
     and reassurance_only < 2
     and thought_homework_without_sleep < 2
     and safety_deferred < 2:
    choose C-Repaired
elif route_primary == risk_uncontained
     and repaired_at_t4 == true
     and final_confirm_three_line_plan == true
     and safety_screen_started >= 2
     and safety_plan_written >= 2
     and crisis_contact_named >= 1
     and baby_safety_check >= 1
     and support_network_contacted >= 1
     and night_contract_written >= 1
     and failure_retry_rule_written >= 1
     and safety_deferred < 2:
    choose D-Repaired
elif final_confirm_three_line_plan == true
     and safety_screen_started >= 1
     and safety_plan_written >= 1
     and crisis_contact_named >= 1
     and baby_safety_check >= 1
     and support_network_contacted >= 1
     and thought_cycle_mapped >= 1
     and spouse_task_observable >= 2
     and request_sentence_written >= 1
     and night_contract_written >= 1
     and failure_retry_rule_written >= 1
     and spouse_blamed_as_cause < 2
     and parent_labeled_overreacting < 2
     and mindreading_expectation_reinforced < 2
     and reassurance_only < 2
     and safety_deferred < 2
     and route_primary == cycle_mapped:
    choose A
elif spouse_blamed_as_cause >= 2
     or parent_labeled_overreacting >= 2
     or mindreading_expectation_reinforced >= 2
     or all_night_spouse_takeover >= 2:
    choose B
elif reassurance_only >= 2
     or thought_homework_without_sleep >= 2
     or comfort_without_action_risk >= 2:
    choose C
elif vague_help_promise >= 2
     or monitoring_as_control >= 2:
    choose Low
else:
    choose Low
```

Resolver regression examples:

```text
T3A-1 -> T4A-1 -> T5-1 = A
Reason: safety, cycle mapping, observable spouse task, request sentence, and retry rule are all present.

T3B-1 -> T4B-1 -> T5-1 = A-Repaired
Reason: blame is repaired into a mutual cycle and concrete behavior plan.

T3B-3 -> T4B-1 -> T5-1 = B
Reason: a late repair cannot erase a strong overreaction label.

T3C-1 -> T4C-1 -> T5-1 = C-Repaired
Reason: comfort becomes a behavior experiment, but the route still resolves as comfort-repaired rather than full A.

T3D-1 -> T4D-1 -> T5-1 = D-Repaired
Reason: risk is repaired through safety-first planning.

T3D-2 -> T4D-1 -> T5-1 = D
Reason: late safety repair cannot erase minimized self-harm signal.

T3B-2 -> T4B-1 -> T5-1 = B
Reason: late repair cannot erase strongly reinforced blame.

T3B-1 -> T4B-2 -> T5-1 = B
Reason: a repaired start is overridden by renewed spouse-blame accountability pressure.

T3A-1 -> T4A-1 -> T5-4 = D
Reason: final safety-delay trap overrides a good plan.
```

## Endings

| Ending | Required Conditions | Blocking Flags | Final Aftereffect |
| --- | --- | --- | --- |
| A. Tonight Has A Plan | safety plan + support network contact + thought cycle + observable night contract + request sentence + retry rule | skipped safety, blame/reassurance traps | the couple leaves with a small, testable night plan and a real backup contact |
| A-Repaired. Blame Becomes A Cycle | blame_loop repaired into mutual automatic thoughts and observable task | blame labels, overreaction labels, safety delay | spouse is not excused, but the cycle replaces accusation |
| B. Better Blame | blame/overreaction/all-night takeover dominates | no B1 repair | one person is made responsible and the loop remains brittle |
| C. Comfort Without Action | reassurance or thought homework replaces night behavior | no C1 repair | parent feels seen, but tonight still has no structure |
| C-Repaired. Asking Is A Recovery Behavior | comfort_only repaired into 20-minute request experiment | reassurance-only, thought homework-only | help request is reframed as sleep protection, not failure |
| D. Risk Uncontained | safety deferred or self-harm signal minimized | no D1 repair | risk language remains without immediate support plan |
| D-Repaired. Safety Before Contract | risk_uncontained repaired through risk-tailored support and support-network contact | safety delay, monitoring-only | safety plan comes before chores and communication skills |
| Low. Same Night Loop | no strong plan or repair | none | bottle, meal tray, and unread messages stay in the same places |

## Route-Specific CG Lock

Minimum commercial-comfortable FT-009 image set:

Use the production command below before image generation:

```text
Docs/FT009_COMMERCIAL_CG_GENERATION_COMMAND_2026-06-10.md
```

| ID | Scene |
| --- | --- |
| FT009_CG_01 | night feeding bottle beside untouched meal tray and unread family group-chat, no readable text |
| FT009_CG_02 | postpartum parent seated under dim nursery lamp at 2:07 a.m. |
| FT009_CG_03 | spouse awake but frozen beside phone alarm |
| FT009_CG_04 | folded clinic leaflet beside empty water cup |
| FT009_CG_05 | behavior chain map with blank cards, no readable text |
| FT009_CG_06 | cycle_mapped route, bottle/alarm/diaper stack arranged as shared loop |
| FT009_CG_07 | blame_loop route, bottle held like evidence between spouses |
| FT009_CG_08 | comfort_only route, meal tray acknowledged but still untouched |
| FT009_CG_09 | risk_uncontained route, clinic leaflet still folded while parent looks away |
| FT009_CG_10 | T4A high, support card and three-line plan placed beside bottle |
| FT009_CG_11 | T4B low, spouse visually accused and defensive |
| FT009_CG_12 | T4C low, reassurance offered while diaper stack remains untouched |
| FT009_CG_13 | T4D low, danger language softened and leaflet left unopened |
| FT009_CG_14 | T4D repaired, support card opened and phone/contact ready, no readable numbers |
| FT009_CG_15 | T5 high, blank three-line night contract card |
| FT009_CG_16 | Ending A, spouse preparing diaper/water while parent rests nearby |
| FT009_CG_17 | Ending A-Repaired, blame diagram replaced by shared cycle card |
| FT009_CG_18 | Ending B, one spouse overburdened while the other withdraws |
| FT009_CG_19 | Ending C, warm reassurance without changed night setup |
| FT009_CG_20 | Ending C-Repaired, parent practices 20-minute request with bottle set aside |
| FT009_CG_21 | Ending D, clinic leaflet and support card still unused |
| FT009_CG_22 | Ending D-Repaired, safety card first, night contract second |
| FT009_CG_23 | Low ending, same bottle, tray, unread messages |
| FT009_CG_24 | supervisor 정세영-centered debrief, practical and warm |

All CGs must be 1600x900, no baked text, no UI, bottom 25-30% clean.

## Gate Requirement

FT-009 cannot proceed to FT-010 until V2 passes:

- CBFT/perinatal safety fidelity review;
- game branching/consequence review;
- commercial VN dialogue review.
