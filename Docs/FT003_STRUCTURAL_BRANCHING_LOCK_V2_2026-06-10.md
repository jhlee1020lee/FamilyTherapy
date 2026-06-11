# FT-003 Structural Branching Lock V2

## Status

This V2 supersedes the branching portions of:

```text
Docs/FT003_STRUCTURAL_MAJOR_BRANCHING_SCENARIO_2026-06-10.md
Docs/FT003_REALISTIC_DIALOGUE_EXPANSION_2026-06-10.md
```

The original common intro and T1 remain usable. V2 is the production authority for T3, T4, T5, state, ending lock, and CG planning.

## Core Structural Question

FT-003 is not about whether therapy is good or bad.

The central question is:

```text
When the therapy bag is by the door, who decides, who carries, who explains, and where is the child standing?
```

## Recurring Visual Anchor

```text
blue therapy bag by the front door
```

Supporting objects:

- laminated weekly therapy schedule on the refrigerator;
- car keys in 아버지's hand;
- water bottle and noise-canceling headphones in 어머니's tote;
- child holding the bag strap without lifting it;
- 기관 담당자's clipboard or tablet;
- three chairs in session, rearranged for enactment.

The blue therapy bag changes meaning by route:

| Route | Bag Meaning |
| --- | --- |
| `parental_alignment` | a shared object parents decide about before asking the child |
| `schedule_escalation` | proof the child must go, regardless of fatigue |
| `mother_child_coalition` | object 어머니 quietly removes to protect the child |
| `professional_takeover` | item checked against the institution's schedule rather than family capacity |

## State Model

```text
route_primary:
  parental_alignment
  schedule_escalation
  mother_child_coalition
  professional_takeover

flags:
  enactment_opened
  parent_meeting_rehearsed
  child_removed_from_decision
  child_made_decider
  schedule_as_command
  rest_standard_defined
  father_excluded
  mother_overfunctioning
  professional_authority_outsourced
  professional_used_as_resource
  implementation_burden_seen
  same_message_practiced
  repaired_at_t4
  unresolved_structure

scores:
  hierarchy_score
  boundary_score
  enactment_score
  child_burden_score
```

## Turn Lock

| Turn | Function | Must Not Collapse |
| --- | --- | --- |
| T1 | route entry | defines whether problem is structure, schedule, protection, or expert authority |
| T2 | first enactment or first rupture | shows where the child stands when adults disagree |
| T3 | route-specific pressure scene | bag/schedule/chair arrangement changes by route |
| T4 | playable structural intervention | three choices per route, must include family re-enactment response |
| T5 | next-week structure lock | ending condition uses flags, not generic route count |

## T3 Choice Consequences

### `parental_alignment`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3A-1 parents decide before child | `parent_meeting_rehearsed +1`, `child_removed_from_decision +1`, `hierarchy_score +2` | opens high A |
| T3A-2 ask child to choose | `child_made_decider +2`, `boundary_score -1` | child burden ending risk |
| T3A-3 therapist sets schedule | `professional_authority_outsourced +1` | route drifts to D |

### `schedule_escalation`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3B-1 repair and see burden | `implementation_burden_seen +1`, `enactment_score +1` | repair possible |
| T3B-2 father enforces attendance | `schedule_as_command +2`, `child_burden_score +2` | locks B unless repaired |
| T3B-3 mother executes schedule | `mother_overfunctioning +2`, `father_excluded +1` | B/C mixed low |

### `mother_child_coalition`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3C-1 protect child and bring father back | `rest_standard_defined +1`, `father_excluded -1`, `boundary_score +1` | opens C repaired |
| T3C-2 mother decides rest | `father_excluded +2`, `mother_overfunctioning +1` | locks protective coalition |
| T3C-3 father must prove care first | `father_excluded +1`, `schedule_as_command +1` | escalates couple split |

### `professional_takeover`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3D-1 expert recommendation becomes resource | `professional_used_as_resource +2`, `hierarchy_score +1` | opens D repaired |
| T3D-2 institution decides | `professional_authority_outsourced +2`, `child_burden_score +1` | locks outsourced authority |
| T3D-3 mother negotiates with institution alone | `mother_overfunctioning +1`, `father_excluded +1` | weak unresolved |

## T4 Playable Choice Lock

### T4A `parental_alignment`: Parent Team Or Child Judge

```text
A1. "아이에게 묻기 전에 부모님 두 분만 치료를 지키는 기준과 쉬는 기준을 한 문장씩 말하고, 아이에게 전달할 한 문장으로 합쳐보겠습니다."
A2. "아이의 몸이 제일 중요하니, 오늘은 아이가 직접 치료를 갈지 말지 말해보게 하겠습니다."
A3. "기관 담당자가 권장하는 기준을 먼저 놓고, 부모님은 그 기준에 맞춰 역할을 나누겠습니다."
```

Effects:

- A1: `parent_meeting_rehearsed +2`, `same_message_practiced +1`, high A candidate.
- A2: `child_made_decider +2`, child burden low candidate.
- A3: `professional_authority_outsourced +1`, D drift.

### T4B `schedule_escalation`: Repair Schedule Pressure Or Double Down

```text
B1. "제가 일정을 먼저 세우며 아이와 어머니가 감당하는 실행 부담을 놓쳤습니다. 지금은 일정표보다, 두 분이 같은 팀으로 그 일정표를 볼 수 있는지 실연해보겠습니다."
B2. "아버지가 치료 출석을 관리하고, 어머니는 준비와 이동을 맡으면 역할이 분명해질 수 있습니다."
B3. "빠지면 회복이 어렵기 때문에, 다음 주는 아이가 힘들어도 결석하지 않는 것을 최우선으로 하겠습니다."
```

Effects:

- B1: `implementation_burden_seen +2`, `enactment_opened +1`, `repaired_at_t4 = true`, repair toward A.
- B2: `mother_overfunctioning +2`, `schedule_as_command +1`, B/C low.
- B3: `schedule_as_command +2`, B low.

### T4C `mother_child_coalition`: Protection With Boundary Or Exclusion

```text
C1. "아이의 지침을 보호하겠습니다. 동시에 아버지를 밖으로 밀어내지 않기 위해, 쉬는 기준도 부모님 두 분이 함께 정하는 장면을 연습하겠습니다."
C2. "당분간 어머니가 아이 컨디션을 기준으로 치료 참석을 결정하고, 아버지는 그 결정을 존중해보겠습니다."
C3. "아버지가 치료보다 아이의 피로를 먼저 인정해야, 아이와 어머니가 안심할 수 있을 것 같습니다."
```

Effects:

- C1: `rest_standard_defined +2`, `father_excluded -1`, `repaired_at_t4 = true`, repaired C/high A candidate.
- C2: `father_excluded +2`, `mother_overfunctioning +2`, C low.
- C3: `father_excluded +1`, `schedule_as_command +1`, couple split scene.

### T4D `professional_takeover`: Resource Or Substitute Hierarchy

```text
D1. "기관 권고는 자료로 두겠습니다. 최종 문장은 부모님 두 분이 아이 상태와 가족 생활을 보고 정하고, 기관에는 그 기준을 공유하겠습니다."
D2. "가족 안에서 계속 다투니, 이번 달은 기관 권장 일정을 그대로 따르며 안정성을 보겠습니다."
D3. "어머니가 기관 담당자와 아이 컨디션을 자세히 상의하고, 그 조정안을 가족이 따르겠습니다."
```

Effects:

- D1: `professional_used_as_resource +2`, `parent_meeting_rehearsed +1`, `repaired_at_t4 = true`, D repaired/high candidate.
- D2: `professional_authority_outsourced +2`, D low.
- D3: `mother_overfunctioning +1`, `father_excluded +1`, weak unresolved.

## T5 Ending Lock

| Ending | Required Conditions | Blocking Flags | Final Aftereffect |
| --- | --- | --- | --- |
| A. Parents Reclaim The Frame | `parent_meeting_rehearsed >= 2`, `same_message_practiced >= 1`, `child_removed_from_decision >= 1` | `child_made_decider >= 2` | parents speak first, child no longer judges adult disagreement |
| A-Repaired. Schedule Held By Parent Team | `implementation_burden_seen >= 2`, `enactment_opened >= 1` | `schedule_as_command >= 2` unless repaired at T4 | schedule remains but is carried by parental subsystem |
| B. Schedule Wins, Child Disappears | `schedule_as_command >= 2` | no repair flag | attendance may rise, child shuts down and mother carries execution |
| C. Protected But Split | `father_excluded >= 2` or `mother_overfunctioning >= 2` | no C1 repair | child is protected from schedule but placed in mother-father split |
| C-Repaired. Rest Has Two Parents | `rest_standard_defined >= 2`, `father_excluded < 2` | none | rest standard becomes a parent-team decision |
| D. Expert As Resource | `professional_used_as_resource >= 2`, `parent_meeting_rehearsed >= 1` | `professional_authority_outsourced >= 2` | institution advises but parents hold hierarchy |
| D-Low. Outsourced Authority | `professional_authority_outsourced >= 2` | no resource repair | family gets a schedule but loses ownership |
| Low. The Bag Waits By The Door | `child_made_decider >= 2` or `unresolved_structure >= 1` | no repair | blue therapy bag remains by the door while adults wait for the child to solve the structure |

## Route-Specific CG Lock

Minimum commercial-comfortable FT-003 image set:

| ID | Scene |
| --- | --- |
| FT003_CG_01 | blue therapy bag by front door, child holding strap |
| FT003_CG_02 | refrigerator therapy schedule with parents standing on opposite sides |
| FT003_CG_03 | car keys in father's hand, mother holding child's headphones |
| FT003_CG_04 | session room, child seated between parents before enactment |
| FT003_CG_05 | parental alignment enactment, child moved out of decision seat |
| FT003_CG_06 | schedule escalation, father pointing at schedule while child shrinks |
| FT003_CG_07 | mother-child coalition, mother pulls bag away, father outside frame |
| FT003_CG_08 | professional takeover, 기관 담당자 clipboard/tablet becomes center |
| FT003_CG_09 | T4A parent team message rehearsal |
| FT003_CG_10 | T4B repair, father notices mother carrying bag and burden |
| FT003_CG_11 | T4B low, child silent with bag already on shoulder |
| FT003_CG_12 | T4C repaired, rest standard spoken by both parents |
| FT003_CG_13 | T4C low, mother-child close pair with father separated |
| FT003_CG_14 | T4D repaired, professional note on side table, parents face child |
| FT003_CG_15 | T4D low, clipboard/tablet centered between everyone |
| FT003_CG_16 | Ending A, parents deliver one shared sentence |
| FT003_CG_17 | Ending B, schedule wins but child disappears behind bag |
| FT003_CG_18 | Ending C/D low, bag still waiting by door |

All CGs must be 1600x900, no baked text, no UI, bottom 25-30% clean.

## Gate Requirement

FT-003 cannot proceed to FT-004 until V2 passes:

- structural family therapy fidelity review;
- game branching/consequence review;
- commercial VN dialogue review.
