# FT-002 Bowen Branching Lock V3

## Status

This V3 supersedes the branching portions of:

```text
Docs/FT002_BOWEN_MAJOR_BRANCHING_SCENARIO_V2_2026-06-10.md
Docs/FT002_REALISTIC_DIALOGUE_EXPANSION_V2_2026-06-10.md
```

V2 remains valid for common intro, T1, and T2 base dialogue. V3 is the production authority for T3, T4, T5, route consequences, ending locks, and CG planning.

## Revision Reason

The FT-002 V2 gate review failed on two non-clinical criteria:

- T4/T5 were written too much like automatic correct interventions, not playable choice turns.
- T3/T4 did not yet have enough commercial VN scene texture, silence, object reaction, and after-intervention family response.

Clinical fidelity passed, so V3 keeps the Bowen frame and expands interactivity and scene breath.

## Core Anchor

```text
11:37 p.m. phone screen
```

The phone must remain ambiguous:

- for 김선기, it is proof that she is still protecting someone;
- for 박준현, it is proof that he is already suspected;
- for 박석민, it is a light he pretends not to see;
- for the player, it is the object that changes meaning according to route state.

## State Model

```text
route_primary:
  differentiated_tracking
  fragile_rule_repair
  loss_touched_too_fast
  triangle_reassigned

flags:
  process_map_built
  checkin_language_rehearsed
  rule_as_punishment
  rule_repair_clause
  father_content_forced
  father_pressure_repaired
  grandfather_as_watchman
  grandfather_as_process_witness
  avoidance_preserved
  vague_reflection_homework

scores:
  differentiation_score
  anxiety_lowering_score
  triangle_risk_score
  rupture_risk_score
```

## Turn Lock

| Turn | Function | Must Change Later Scenes |
| --- | --- | --- |
| T1 | route entry | sets `route_primary` |
| T2 | first temptation | adds first flag and changes T3 opening line |
| T3 | pressure scene | route-specific choice, adds risk/repair flag |
| T4 | playable intervention | three choices per route, locks ending candidate |
| T5 | ending confirmation | route-specific aftereffect, not generic wrap-up |

## T3 Choice Consequences

### `differentiated_tracking`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3A-1 자기 문장 만들기 | `process_map_built +1`, `differentiation_score +2` | opens Ending A high version |
| T3A-2 전화 약속 먼저 | `rule_as_punishment +1`, route drifts to fragile rule | T4A starts with 준현 resistance |
| T3A-3 아버지 기억 더 말하기 | `father_content_forced +1`, `rupture_risk_score +2` | T4A becomes repair-only |

### `fragile_rule_repair`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3B-1 첫 확인 문장 실험 | `checkin_language_rehearsed +1`, `rule_repair_clause +1` | opens Ending B high version |
| T3B-2 예외 없는 규칙 | `rule_as_punishment +2`, `triangle_risk_score +1` | locks Low Ending unless repaired at T4 |
| T3B-3 감정만 확인하고 규칙 없음 | `avoidance_preserved +1` | Ending B unavailable |

### `loss_touched_too_fast`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3C-1 내용 전 멈춤 규칙 | `father_pressure_repaired +1`, `rupture_risk_score -1` | opens Ending C repaired version |
| T3C-2 방 문 보는 이유 캐기 | `father_content_forced +1`, `rupture_risk_score +2` | locks Door Opened Too Fast unless T4 repairs |
| T3C-3 선기의 상실을 충분히 말하기 | `father_content_forced +2` | 준현 withdrawal scene required at T4 |

### `triangle_reassigned`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3D-1 과정 증인 역할 | `grandfather_as_process_witness +1` | opens Ending D witness version |
| T3D-2 조부가 귀가 확인 | `grandfather_as_watchman +2`, `triangle_risk_score +2` | locks New Watchman low ending unless repaired |
| T3D-3 조부가 자리 피함 | `avoidance_preserved +1` | weak ending, no high D |

## T4 Playable Choice Lock

Each route must present exactly three player options at T4. The best option is not always the longest. Every option must lead to a visible family reaction before T5.

### T4A `differentiated_tracking`: Three Sentences Or One More Control

```text
A1. "오늘은 해결 약속보다 먼저, 11시 37분에 각자 몸에서 먼저 일어나는 일을 한 문장으로 말해보겠습니다."
A2. "이제 서로의 입장을 들었으니, 준현이가 그 시간에는 전화를 받겠다고 약속하는 것이 좋겠습니다."
A3. "아버지 이야기가 같이 떠오른다면, 선기 씨가 그날의 기억을 조금 더 말하고 준현이는 듣는 시간을 갖겠습니다."
```

Effects:

- A1: `process_map_built +1`, `differentiation_score +2`, Ending A candidate.
- A2: `rule_as_punishment +1`, Ending B weak candidate if T3B state exists, otherwise Low mixed ending.
- A3: `father_content_forced +1`, route shifts to C repaired or unrepaired depending on T3.

### T4B `fragile_rule_repair`: Repair Clause Or Punishment

```text
B1. "귀가 시간을 없애지 않겠습니다. 대신 넘겼을 때 보낼 첫 문장과, 집에 온 뒤 묻지 않을 질문을 같이 정하겠습니다."
B2. "이번 주는 정한 시간을 넘기면 이유를 묻지 않고 바로 결과를 정하겠습니다. 규칙은 예외가 없어야 합니다."
B3. "규칙 이야기가 부담을 키우니, 오늘은 서로 불안했다는 것만 확인하고 다음 회기에서 정하겠습니다."
```

Effects:

- B1: `rule_repair_clause +2`, `checkin_language_rehearsed +1`, Ending B high candidate.
- B2: `rule_as_punishment +2`, `grandfather_as_watchman +1`, New Watchman/Control low candidate.
- B3: `avoidance_preserved +1`, no high ending; T5 shows same 11:37 loop likely repeats.

### T4C `loss_touched_too_fast`: Stop Rule Or Content Pressure

```text
C1. "오늘은 아버지 이야기를 더 열지 않겠습니다. 그 이름이 나오면 누가 문을 보고, 누가 붙잡고, 누가 덮는지만 말하는 규칙을 만들겠습니다."
C2. "준현이가 방 문을 보는 이유를 지금 조금 더 말해야, 이 주제를 계속 피하지 않을 수 있습니다."
C3. "선기 씨가 그날 이후 얼마나 무서웠는지 준현이가 들어야 서로를 오해하지 않을 것 같습니다."
```

Effects:

- C1: `father_pressure_repaired +2`, Ending C repaired candidate.
- C2: `father_content_forced +2`, Door Opened Too Fast low candidate.
- C3: `father_content_forced +2`, `triangle_risk_score +1`, 준현 withdrawal CG required.

### T4D `triangle_reassigned`: Witness Or Watchman

```text
D1. "석민 씨에게 귀가 확인을 맡기지 않겠습니다. 대신 질문이 심문으로 바뀌는 순간 '지금 시간 얘기입니까, 걱정 얘기입니까'라고 묻는 역할을 연습하겠습니다."
D2. "선기 씨가 너무 지쳐 있으니 이번 주는 석민 씨가 귀가 확인을 맡고, 선기 씨는 휴대폰을 내려놓는 연습을 하겠습니다."
D3. "두 사람이 격해질 때 석민 씨가 자리를 피하면 충돌은 줄어들 수 있습니다. 우선 싸움이 커지지 않게 하겠습니다."
```

Effects:

- D1: `grandfather_as_process_witness +2`, `triangle_risk_score -1`, Ending D witness candidate.
- D2: `grandfather_as_watchman +2`, New Watchman low candidate.
- D3: `avoidance_preserved +2`, Ending D weak unresolved candidate.

## T5 Ending Lock

| Ending | Required Conditions | Blocking Flags | Final Aftereffect |
| --- | --- | --- | --- |
| A. 11:37 Has Three Voices | `process_map_built >= 2`, `differentiation_score >= 3` | `father_content_forced >= 2` | the phone becomes a one-sentence check-in, not a search |
| B. Rule With A Repair Clause | `rule_repair_clause >= 2`, `checkin_language_rehearsed >= 1` | `rule_as_punishment >= 2` unless repaired | curfew remains, with no-search clause after return |
| C. Door Opened But Held | `father_pressure_repaired >= 2` | none if repaired | father topic is acknowledged only as a trigger, not content homework |
| C-Low. Door Opened Too Fast | `father_content_forced >= 2` | no repair flag | 준현 watches the door and stops answering |
| D. Process Witness | `grandfather_as_process_witness >= 2` | `grandfather_as_watchman >= 2` | 조부 slows questioning instead of taking over |
| D-Low. New Watchman | `grandfather_as_watchman >= 2` | no witness repair | 조부 becomes the new reporting line |
| Low. Same Light, Same Silence | `avoidance_preserved >= 2` or `vague_reflection_homework` | no repair flags | 11:37 phone light returns unchanged |

## Route-Specific CG Lock

Minimum commercial-comfortable FT-002 image set:

| ID | Scene |
| --- | --- |
| FT002_CG_01 | common intro, 김선기 holding phone at 11:37 |
| FT002_CG_02 | 준현 outside convenience store, unread call visible |
| FT002_CG_03 | 박석민 in living room, newspaper open but unread |
| FT002_CG_04 | three family members in session, phone placed between them |
| FT002_CG_05 | differentiated route, three separate reactions to same phone light |
| FT002_CG_06 | rule route, clock/phone becomes pressure object |
| FT002_CG_07 | loss route, 준현 looking toward door |
| FT002_CG_08 | triangle route, 박석민 caught between two seats |
| FT002_CG_09 | T4A, three I-position sentences spoken with visible distance softening |
| FT002_CG_10 | T4B, repair clause written on note/phone but not as UI text |
| FT002_CG_11 | T4C repaired, father topic held without opening content |
| FT002_CG_12 | T4C low, 준현 withdrawing toward door |
| FT002_CG_13 | T4D witness, 박석민 asking process question |
| FT002_CG_14 | T4D low, 박석민 as watchman/checker |
| FT002_CG_15 | Ending A, phone becomes check-in signal |
| FT002_CG_16 | Ending B, curfew with repair clause |
| FT002_CG_17 | Ending C, door opened but held |
| FT002_CG_18 | Low ending, same 11:37 phone light repeats |

All CGs must be 1600x900, no baked text, no UI, bottom 25-30% clean.

## Gate Requirement

FT-002 cannot proceed to Unity conversion or FT-003 production until V3 passes:

- clinical/Bowen review;
- game branching review;
- commercial VN dialogue review.
