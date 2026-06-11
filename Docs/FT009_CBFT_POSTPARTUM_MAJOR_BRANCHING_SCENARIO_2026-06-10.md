# FT-009 CBFT Postpartum Major Branching Scenario

## Goal

FT-009 focuses on an isolated postpartum family. The presenting problem is postpartum depression, sleep deprivation, and spouse conflict around help. The cognitive-behavioral family therapy focus is attribution, reinforcement, concrete communication, safety screening, and small behavioral contracts.

The player should not treat the issue as only mood or only chores. The episode should connect thoughts, emotions, behaviors, and the couple's reinforcing cycle.

## Cast

Names are not locked yet. Use role labels until final names are assigned.

| Character | Role | Surface statement | Hidden pressure |
| --- | --- | --- | --- |
| 산후 보호자 | postpartum parent | "제가 못 버티면 엄마 자격이 없는 것 같아요." | 도움 요청을 실패로 해석, 수면 부족, 우울 |
| 배우자 | spouse | "도와주려고 하면 방식이 틀렸다고 해서 손을 못 대겠어요." | 무엇을 해야 할지 모르는 무력감과 방어 |
| 아기 | infant | 화면상 직접 대사 없음 | 수면/돌봄 루틴의 중심 |
| 친족/지원망 | distant family support | 전화나 언급으로만 등장 가능 | 도움을 요청하기 어렵거나 멀리 있음 |
| 정세영 | CBFT supervisor | 귀인, 상호강화, 행동계약, 안전 확인 | 실용적이고 구체적 |

## Route Model

| Route ID | Name | Entry condition | Session tone |
| --- | --- | --- | --- |
| `behavior_plan` | 행동계약 루트 | 신념과 행동을 분리하고 구체 과제를 만듦 | 부부가 오늘 밤 할 일을 명확히 정함 |
| `blame_loop` | 귀인 갈등 루트 | 배우자 무능/보호자 예민함으로 해석 | 서로의 부정적 귀인이 강화 |
| `maternal_failure_story` | 실패 신념 루트 | 보호자의 "엄마 자격" 신념을 그대로 강화 | 도움 요청이 더 어려워짐 |
| `safety_missed` | 안전 확인 누락 루트 | 우울/위험 신호를 감정 대화로만 처리 | 실제 위험 평가가 빠짐 |

## Five-Turn Flow

```text
Common intro
-> T1: 우울, 귀인, 행동 중 어디서 시작할 것인가
-> T2: 밤 돌봄 장면 행동분석
-> T3: 자동사고와 상호강화
-> T4: 안전 확인과 행동계약
-> T5: 오늘 밤 실행 계획과 실패 시 규칙
```

## Common Intro

```text
산후 보호자: 잠을 거의 못 자요. 그런데 제가 힘들다고 말하면 엄마 자격이 없는 것 같아서, 그냥 버텨야 한다고 생각합니다.

배우자: 도와주려고 하면 방식이 틀렸다고 해서 손을 못 대겠습니다. 그러다 보니 제가 뭘 해도 도움이 안 되는 사람처럼 느껴집니다.

산후 보호자: 말하지 않아도 알아서 해주면 좋겠어요. 제가 하나하나 지시해야 하면 결국 제가 다 하는 것 같습니다.

배우자: 저는 정확히 말해주면 할 수 있습니다. 그런데 물어보면 또 모른다고 혼날까 봐 멈추게 됩니다.

정세영: 감정은 충분히 들어야 하지만 오늘 밤 실행할 행동도 필요합니다. 생각, 감정, 행동, 수면을 한 장면 안에서 보세요.
```

## T1. Initial Definition

| Choice | Player line | Route impact | Immediate response |
| --- | --- | --- | --- |
| A | "우선 오늘 밤 돌봄 장면을 구체적으로 나누고, 그때 떠오르는 생각과 행동을 같이 보겠습니다." | `behavior_plan +2` | 부부가 구체 장면으로 이동 |
| B | "배우자분이 더 적극적으로 도와야 할 것 같습니다." | `blame_loop +2` | 배우자가 방어하고 보호자는 잠깐 인정받지만 반복 강화 |
| C | "좋은 부모도 힘들 수 있다는 점을 먼저 충분히 확인하겠습니다." | `maternal_failure_story +1` | 정서 반영은 좋지만 행동계획 없으면 부족 |
| D | "우울감과 위험 신호를 확인하기 전에 부부 대화 방식부터 정리하겠습니다." | `safety_missed +2` | 위험 평가가 뒤로 밀림 |

## T2. Behavioral Chain

### `behavior_plan`

```text
정세영: 장면을 작게 쪼개세요. 새벽 2시에 아기가 울면 누가 일어나고, 누가 무엇을 생각하고, 다음 행동은 무엇입니까?
산후 보호자: 제가 먼저 일어나요. 그때 '또 나만 하네'라는 생각이 바로 듭니다.
배우자: 저는 깬 줄 몰랐다가 나중에 눈치채요. 그때는 이미 화가 나 있어서 들어가기가 무섭습니다.
정세영: 자동사고와 행동 회피가 서로를 강화하고 있습니다.
```

Good T2 choice:

```text
"새벽 돌봄을 '누가 더 힘든가'가 아니라 행동 순서로 나눠보겠습니다. 울음, 기상, 수유/기저귀, 다시 재우기 중 어느 부분을 누가 맡을지 정하겠습니다."
```

### `blame_loop`

```text
배우자: 제가 안 한 사람처럼 들립니다. 저도 출근해야 하고, 뭘 해야 할지 몰라서 멈춘 겁니다.
산후 보호자: 결국 제가 설명해야 한다는 거잖아요. 저는 설명할 힘도 없습니다.
정세영: 귀인이 굳어지고 있습니다. 한쪽은 '무능하다', 다른 쪽은 '예민하다'로 서로를 읽게 됩니다.
```

Repair T2 choice:

```text
"제가 한쪽 책임처럼 말했습니다. 지금은 누가 더 잘못했는지가 아니라, 서로를 어떻게 해석해서 행동이 멈추는지 보겠습니다."
```

### `maternal_failure_story`

```text
산후 보호자: 좋은 부모도 힘들 수 있다는 말은 위로가 됩니다. 그런데 오늘 밤에도 결국 제가 혼자 일어나면 똑같을 것 같아요.
배우자: 저도 위로는 하고 싶은데, 실제로 뭘 해야 할지 모르겠습니다.
정세영: 정서 확인은 필요하지만 행동 단위로 좁히지 않으면 변화가 유지되지 않습니다.
```

Repair T2 choice:

```text
"위로에서 멈추지 않고, 그 신념이 오늘 밤 도움 요청을 어떻게 막는지 보겠습니다."
```

### `safety_missed`

```text
산후 보호자: 가끔은 그냥 사라지고 싶다는 생각도 합니다. 그런데 그런 말 하면 다들 놀랄까 봐 안 했습니다.
배우자: 그런 생각을 하는 줄은 몰랐습니다.
정세영: 안전 확인은 선택이 아닙니다. 행동계약 전에 위험 신호와 지원 체계를 확인해야 합니다.
```

Repair T2 choice:

```text
"방금 중요한 말을 해주셨습니다. 지금은 대화 기술보다 안전을 먼저 확인하겠습니다. 혼자 있을 때 위험해지는 순간, 연락할 사람, 오늘 밤 보호 계획을 정하겠습니다."
```

## T3. Thoughts And Reinforcement

Core scene:

```text
산후 보호자: 제가 도움을 요청하면 실패한 엄마 같고, 요청하지 않으면 혼자 하다가 배우자를 미워하게 됩니다.
배우자: 저는 뭘 해도 틀릴 거라고 생각해서 기다립니다. 그러면 더 안 돕는 사람이 됩니다.
정세영: '요청하면 실패'와 '물어보면 혼남'이라는 생각이 서로를 강화하고 있습니다.
```

Key choices:

```text
A. "두 분의 자동사고를 각각 한 문장으로 적고, 그 생각이 행동을 어떻게 막는지 보겠습니다."
B. "배우자분이 더 눈치껏 움직이면 보호자분 생각도 줄어들 것입니다."
C. "우울감은 시간이 지나면 나아질 수 있으니 우선 부부 갈등만 줄이겠습니다."
```

## T4. Core CBFT Intervention

Target intervention:

```text
"안전 확인을 한 뒤, 오늘 밤 행동계약을 아주 작게 만들겠습니다. 새벽 첫 울음은 배우자가 기저귀와 물 준비를 맡고, 보호자는 요청 문장을 하나만 사용합니다. '지금 20분만 맡아줘'처럼 관찰 가능한 행동으로 말합니다."
```

Expected response:

```text
산후 보호자: '20분만 맡아줘'라고 말하면 제가 실패한 엄마가 되는 게 아니라, 잠깐 회복하는 사람이 되는 거네요.
배우자: 저는 정확히 무엇을 해야 하는지 알면 움직일 수 있습니다. 틀릴까 봐 멈추는 것보다 낫습니다.
정세영: 좋은 행동계약은 작고, 오늘 밤 실행 가능하고, 실패했을 때 다시 시도하는 규칙이 있어야 합니다.
```

## T5. Closure

Best closure:

```text
"오늘 밤 계획은 세 줄입니다. 첫 울음: 배우자가 기저귀와 물 준비. 보호자 요청 문장: '20분만 맡아줘.' 위험 신호가 올라오면 혼자 버티지 않고 정한 연락처에 연락. 내일은 성공/실패가 아니라 실제로 몇 분 쉬었는지만 기록합니다."
```

## Endings

| Ending | Condition | Result |
| --- | --- | --- |
| A. Tonight Has A Plan | behavior route maintained with safety check | 안전 확인과 구체 행동계약이 생김 |
| B. Better Blame | blame route dominant | 한쪽 책임으로 귀인이 굳어져 행동 변화 약함 |
| C. Comfort Without Action | failure-story route dominant | 위로는 있으나 오늘 밤 구조가 없음 |
| D. Risk Uncontained | safety missed route dominant | 우울/위험 신호가 충분히 다뤄지지 않아 즉시 보완 필요 |

## Dialogue Expansion Notes

- 산후 우울은 고위험 가능성이 있으므로 안전 확인을 반드시 포함한다.
- 배우자를 무능하거나 악의적인 사람으로만 쓰지 않는다. 모르는 것과 방어가 핵심이다.
- 정세영은 따뜻하지만 구체적이다. "오늘 밤 누가 무엇을 할지"로 좁힌다.
- 행동계약은 실패 가능성을 포함해야 한다.
