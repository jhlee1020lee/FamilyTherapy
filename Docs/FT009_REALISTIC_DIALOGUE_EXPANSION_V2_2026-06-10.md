# FT-009 Realistic Dialogue Expansion V2

## Status

Use this V2 as the production replacement for T3, T4, and T5 of:

```text
Docs/FT009_REALISTIC_DIALOGUE_EXPANSION_2026-06-10.md
```

Use this with:

```text
Docs/FT009_CBFT_BRANCHING_LOCK_V2_2026-06-10.md
```

## Dialogue Rule

Do not make postpartum depression a chore-distribution problem only.

The player must keep three layers together:

```text
1. safety and risk support
2. automatic thought / emotion / behavior / sleep loop
3. tiny observable night contract
```

Do not write the spouse as a villain. The spouse's problem is fear of doing it wrong, defensive waiting, and vague help promises.

Do not write the postpartum parent as irrational. The parent's problem is exhaustion, automatic self-failure thoughts, and having to direct care while depleted.

## Recurring Scene

```text
night feeding bottle beside an untouched meal tray, unread family group-chat messages, and a folded clinic leaflet
```

The bottle is not just a prop. It marks where the night loop starts.

The clinic leaflet is not a punishment. It marks that safety and support must be kept visible.

## T2-T3 Bridge. The Night Loop On The Sofa

```text
상담자: 새벽 장면을 한 번만 느리게 보겠습니다. 아기가 울고, 몸이 먼저 일어나고, 식탁의 밥은 식은 채로 있고, 배우자분 휴대폰 알람은 울렸는지 아닌지 애매합니다.

산후 보호자: 그때 바로 들어오는 생각은 "또 나만 하네"입니다. 그 다음엔 "이걸 힘들어하면 나는 엄마 자격이 없나"가 붙습니다.

배우자: 저는 늦게 깨거나, 깼는데도 어떻게 들어가야 할지 몰라서 멈출 때가 있습니다. 들어가면 이미 늦었다고 할 것 같아서요.

산후 보호자: 그래서 제가 말하지 않아도 알아서 했으면 좋겠다고 생각합니다. 그런데 그걸 기다리다가 더 화가 납니다.

상담자: 한 사람의 성격 문제가 아니라 고리입니다. 울음, 자동사고, 멈춤, 분노, 수면 부족이 서로를 밀고 있습니다.

정세영: 좋습니다. 이제 이 고리를 비난이 아니라 행동계약으로 바꿀 수 있는지 보세요. 단, 위험 신호가 있으면 계약보다 안전이 먼저입니다.
```

## T3. Route-Specific Automatic Thought Pressure

### `cycle_mapped` T3: 생각-감정-행동-수면 고리

```text
상담자: 보호자분의 자동사고를 한 문장으로 적으면 "도움을 요청하면 실패한 엄마다"에 가깝습니다. 배우자분은 어떤 문장인가요?

배우자: "물어보면 또 혼난다"입니다. 그래서 물어보기보다 기다립니다.

산후 보호자: 기다리는 걸 보면 저는 더 혼자인 것 같습니다.

상담자: 그러면 두 생각이 서로를 증명하고 있습니다. 요청하면 실패라는 생각은 요청을 막고, 물어보면 혼난다는 생각은 행동을 막습니다.

정세영: CBFT에서는 생각을 맞다 틀리다로만 다루지 않습니다. 그 생각이 어떤 행동을 만들고, 그 행동이 가족에게 무엇을 강화하는지 보세요.
```

T3 선택지:

```text
1. "두 자동사고를 적고, 각각 어떤 행동을 막는지 보겠습니다. 그 다음 오늘 밤에 바꿀 수 있는 한 행동만 고르겠습니다."
2. "배우자분이 오늘부터 무조건 먼저 일어나면 보호자분의 '나만 한다'는 생각도 줄어들 것입니다."
3. "보호자분이 실패한 엄마가 아니라는 문장을 더 자주 확인하겠습니다."
```

Immediate effects:

- 1: `thought_cycle_mapped +1`, `automatic_thoughts_named +1`, `behavior_chain_mapped +1`.
- 2: `all_night_spouse_takeover +1`, `vague_help_promise +1`.
- 3: `reassurance_only +1`, `thought_homework_without_sleep +1`.

### `blame_loop` T3: 배우자 탓에서 고리로 돌아오기

```text
배우자: 제가 더 해야 한다는 건 알겠는데, 또 제가 부족한 사람으로 끝나는 것 같습니다.

산후 보호자: 부족하다고 말하려는 게 아니라, 제가 무너져야 움직이는 것처럼 느껴진다는 거예요.

상담자: 제가 처음에 배우자분을 원인처럼 세웠습니다. 그 말은 보호자분의 외로움은 알아줬지만, 배우자분을 방어하게 만들었습니다.

정세영: 책임을 지우지 말라는 뜻이 아닙니다. 비난으로 가면 행동은 더 줄어듭니다. 자동사고와 관찰 가능한 행동으로 다시 내려오세요.
```

T3 선택지:

```text
1. "제가 한쪽 책임처럼 말했습니다. 이제 '안 한다'와 '틀릴까 봐 멈춘다'를 구분하고, 멈춤을 깨는 행동 하나를 정하겠습니다."
2. "그래도 배우자분은 오늘부터 변명보다 행동을 먼저 보여줘야 합니다."
3. "보호자분도 배우자 방식이 다를 수 있다는 점을 받아들이셔야 합니다."
```

Immediate effects:

- 1: `repair_started +1`, `thought_cycle_mapped +1`, `spouse_blamed_as_cause -1`.
- 2: `spouse_blamed_as_cause +2`, `parent_labeled_overreacting +1`.
- 3: `parent_labeled_overreacting +2`, `mindreading_expectation_reinforced +1`.

### `comfort_only` T3: 위로를 행동 실험으로 바꾸기

```text
산후 보호자: 좋은 부모도 힘들 수 있다는 말은 위로가 됩니다. 그런데 오늘 밤에 또 제가 먼저 일어나면 그 말은 별로 남지 않을 것 같습니다.

배우자: 저는 좋은 부모라고 말해줄 수는 있습니다. 그런데 그 말 다음에 뭘 해야 할지는 아직 모르겠습니다.

상담자: 위로가 필요합니다. 동시에 그 위로가 오늘 밤 20분의 수면으로 연결되지 않으면 신념은 다시 강해질 수 있습니다.

정세영: 인지 재구성은 말만이 아닙니다. 도움 요청을 실패가 아니라 회복 행동으로 경험하게 하는 작은 실험이 필요합니다.
```

T3 선택지:

```text
1. "'도움 요청은 실패'라는 생각을 오늘 밤 행동 실험으로 다루겠습니다. '20분만 맡아줘'가 실패가 아니라 회복 행동인지 확인합니다."
2. "배우자분이 하루에 몇 번씩 좋은 부모라고 말해주면 죄책감이 줄어들 수 있습니다."
3. "보호자분이 좋은 부모라는 증거를 적어오는 과제를 내겠습니다."
```

Immediate effects:

- 1: `repair_started +1`, `automatic_thoughts_named +1`, `request_sentence_written +1`.
- 2: `reassurance_only +2`, `comfort_without_action_risk +1`.
- 3: `thought_homework_without_sleep +2`, `comfort_without_action_risk +1`.

### `risk_uncontained` T3: 위험 신호가 나온 뒤의 우선순위

```text
산후 보호자: 가끔은 제가 없어지면 조용해질까 하는 생각이 스칩니다. 아기 울음이 멈추지 않을 때 그런 생각이 더 빨리 옵니다.

배우자: 그런 생각을 하는 줄 몰랐습니다. 그냥 지쳐서 예민한 줄 알았습니다.

상담자: 지금은 대화 방식보다 안전을 먼저 확인해야 합니다. 그 생각이 올 때 혼자 있는지, 아기와 단둘이 있는지, 누구에게 연락할 수 있는지 봐야 합니다.

정세영: 위험 신호가 나오면 즉시 구조를 만드세요. 안전 확인을 하면 분위기가 무거워질까 봐 피하면 안 됩니다.
```

T3 선택지:

```text
1. "방금 중요한 말을 해주셨습니다. 혼자 위험해지는 순간, 오늘 밤 혼자 있지 않을 시간, 연락할 사람, 아기 안전을 먼저 확인하겠습니다."
2. "그 생각은 기록해두고, 오늘은 부부가 서로 덜 상처 주는 말부터 연습하겠습니다."
3. "배우자분이 오늘부터 자주 괜찮냐고 물어보면 위험이 낮아질 수 있습니다."
```

Immediate effects:

- 1: `repair_started +1`, `safety_screen_started +1`, `crisis_contact_named +1`, `baby_safety_check +1`.
- 2: `safety_deferred +2`, `self_harm_signal_minimized +2`.
- 3: `monitoring_as_control +1`, `vague_help_promise +1`, `safety_deferred +1`.

## T4. Playable Safety And Behavior Contract

### `cycle_mapped` T4: 안전 확인 뒤 세 줄 계약

T4 선택지:

```text
1. "먼저 위험 신호, 연락 계획, 아기 안전 순서를 확인하고 가족 단체방의 실제 지원자 한 명에게 오늘 밤 대기 가능 여부를 묻겠습니다. 그 뒤 첫 울음에서 배우자가 맡을 행동 하나와 보호자의 요청 문장 하나를 적겠습니다."
2. "오늘 밤은 배우자가 전부 맡고 보호자는 완전히 쉬는 것으로 크게 바꾸겠습니다."
3. "자동사고를 더 정확히 적는 과제를 내고 행동계약은 다음 회기에 하겠습니다."
```

Choice 1 reaction:

```text
상담자: 위험한 생각이 강해지면 혼자 버티지 않는 계획부터 적겠습니다. 아기 안전 순서도 포함합니다. 가족 단체방에서 오늘 밤 실제로 받을 수 있는 도움도 한 명 정하겠습니다. 그 다음 첫 울음에서 누가 무엇을 할지 정하겠습니다.

산후 보호자: 연락처를 적는 건 무섭지만, 혼자 참는 것보다는 나을 것 같습니다.

배우자: 저는 첫 울음 때 기저귀와 물 준비를 하겠습니다. 무엇을 해야 하는지 알면 덜 멈출 것 같습니다.

산후 보호자: 저는 "지금 20분만 맡아줘"라고 말해보겠습니다. 그 문장이 있으면 덜 무너질 것 같습니다.

배우자: 단체방에 지금 메시지를 보내겠습니다. 오늘 밤 혹시 위험 신호가 올라오면 통화 가능한 사람이 있는지 확인하겠습니다.

정세영: 안전과 행동계약이 같이 잡혔습니다. 작고 오늘 밤 가능한 계약입니다.
```

Effect: `safety_screen_started +1`, `safety_plan_written +1`, `crisis_contact_named +1`, `baby_safety_check +1`, `support_network_contacted +1`, `spouse_task_observable +2`, `request_sentence_written +1`, `night_contract_written +1`.

Choice 2 reaction:

```text
배우자: 오늘 밤은 제가 전부 하겠습니다. 그런데 제가 버틸 수 있을지 모르겠습니다.

산후 보호자: 고맙긴 한데, 또 크게 약속했다가 안 되면 더 화날 것 같습니다.

상담자: 큰 전환은 매력적이지만 실패 가능성이 높고, 실패했을 때 다시 비난 고리로 돌아갈 수 있습니다.

정세영: 행동계약은 작아야 합니다. "전부"는 관찰 가능한 과제가 아니라 부담 선언입니다.
```

Effect: `all_night_spouse_takeover +2`, `vague_help_promise +1`, B/C drift.

Choice 3 reaction:

```text
상담자: 오늘은 자동사고를 적는 과제를 내고 행동계약은 다음 회기에 하겠습니다.

산후 보호자: 그러면 오늘 밤은 또 제가 알아서 해야 하나요?

배우자: 생각 기록은 할 수 있지만, 새벽에는 뭘 해야 할지 여전히 모르겠습니다.

정세영: 사고 기록만으로 수면은 생기지 않습니다. 행동이 빠진 인지 과제는 위로만 남고 오늘 밤을 바꾸지 못합니다.
```

Effect: `thought_homework_without_sleep +2`, `comfort_without_action_risk +1`.

### `blame_loop` T4: 귀인 수리와 관찰 가능한 행동

T4 선택지:

```text
1. "제가 배우자 책임처럼 말했습니다. 이제 '안 한다'와 '틀릴까 봐 멈춘다'를 구분하고, 멈춤을 깨는 관찰 가능한 행동 하나를 정하겠습니다."
2. "배우자분은 오늘부터 먼저 움직여야 합니다. 보호자분이 설명하지 않아도 필요한 일을 찾아야 합니다."
3. "보호자분도 배우자 방식이 다를 수 있음을 받아들이고 지적을 줄이겠습니다."
```

Choice 1 reaction:

```text
상담자: 기다리는 행동을 변명으로 두지는 않겠습니다. 다만 "안 한다"와 "틀릴까 봐 멈춘다"는 다르게 다루겠습니다.

배우자: 틀릴까 봐 멈추는 건 맞습니다. 그러면 첫 울음 때는 기저귀와 물 준비처럼 정해진 일부터 하겠습니다.

산후 보호자: 제가 다 설명해야 하는 건 싫지만, 정해진 일이 있으면 덜 화날 것 같습니다.

정세영: 좋은 수리입니다. 책임을 없애지 않고 귀인 고리를 행동으로 바꿨습니다.
```

Effect: `repaired_at_t4 = true`, `thought_cycle_mapped +1`, `spouse_task_observable +1`, `request_sentence_written +1`.

Choice 2 reaction:

```text
상담자: 배우자분은 변명보다 행동을 먼저 보여줘야 한다고 말했습니다.

배우자: 맞는 말인데, 또 제가 문제라는 말로 들립니다.

산후 보호자: 저는 도움을 원한 건데, 이러면 또 제가 몰아붙이는 사람이 되는 것 같습니다.

정세영: 책임 요구가 다시 비난으로 들립니다. 행동계약이 아니라 재판 장면이 됐습니다.
```

Effect: `spouse_blamed_as_cause +3`, `vague_help_promise +1`.

Choice 3 reaction:

```text
상담자: 보호자분도 배우자 방식이 다를 수 있음을 받아들이자고 제안했습니다.

산후 보호자: 제가 예민해서 문제라는 말처럼 들립니다.

배우자: 제 방식도 봐달라는 건 맞지만, 지금은 그 말이 보호자에게 부담을 더 주는 것 같습니다.

정세영: 보호자의 자동사고가 더 강해졌습니다. 과잉반응 프레임은 도움 요청을 더 어렵게 만듭니다.
```

Effect: `parent_labeled_overreacting +2`, `mindreading_expectation_reinforced +1`.

### `comfort_only` T4: 위로를 행동 실험으로 바꾸기

T4 선택지:

```text
1. "좋은 부모도 힘들 수 있다는 확인에서 멈추지 않고, '20분만 맡아줘'를 실패가 아니라 회복 행동으로 실험하겠습니다."
2. "배우자분이 하루에 세 번 좋은 부모라고 말해 보호자분의 죄책감을 낮추겠습니다."
3. "보호자분이 좋은 부모라는 증거를 매일 기록해오겠습니다."
```

Choice 1 reaction:

```text
상담자: "20분만 맡아줘"라는 문장은 실패 선언이 아니라 회복을 위한 행동 실험입니다.

산후 보호자: 그 말을 하면 제가 못 버틴다는 뜻인 줄 알았습니다. 그런데 회복하려는 행동이라고 보면 조금 다릅니다.

배우자: 저는 그 문장을 들으면 바로 기저귀와 물 준비를 하겠습니다. 추측보다 낫습니다.

정세영: 인지 재구성이 행동과 연결됐습니다. 수면 보호가 생겼습니다.
```

Effect: `repaired_at_t4 = true`, `automatic_thoughts_named +1`, `request_sentence_written +2`, `sleep_protection_named +1`.

Choice 2 reaction:

```text
배우자: 하루에 세 번 좋은 부모라고 말하겠습니다.

산후 보호자: 그 말은 고마운데, 새벽에는 말보다 사람이 필요합니다.

상담자: 위로가 행동을 대신하고 있습니다.

정세영: 정서 확인은 있지만 오늘 밤 구조가 없습니다. 위로가 행동을 대신하고 있습니다.
```

Effect: `reassurance_only +2`, `comfort_without_action_risk +2`.

Choice 3 reaction:

```text
상담자: 좋은 부모라는 증거를 기록해오는 과제를 냈습니다.

산후 보호자: 숙제가 하나 더 생긴 것 같습니다. 밤에는 기록할 힘도 없어요.

배우자: 저는 그 과제에서 제가 뭘 해야 하는지는 모르겠습니다.

정세영: 사고 과제가 수면과 행동을 밀어냈습니다. 지금은 과제가 아니라 오늘 밤 구조가 필요합니다.
```

Effect: `thought_homework_without_sleep +2`, `comfort_without_action_risk +1`.

### `risk_uncontained` T4: 안전을 계약보다 먼저 놓기

T4 선택지:

```text
1. "방금 위험 신호가 나왔으므로 행동계약 전에 혼자 위험해지는 순간, 오늘 밤 혼자 있지 않을 시간, 연락할 사람, 가족 단체방에서 실제로 대기할 지원자, 아기 안전을 먼저 정하겠습니다."
2. "위험 생각은 기록해두고, 오늘은 부부가 서로 덜 상처 주는 말부터 연습하겠습니다."
3. "배우자분이 수시로 괜찮냐고 확인해 위험을 낮추겠습니다."
```

Choice 1 reaction:

```text
상담자: 지금은 좋은 배우자 대화보다 안전 계획이 먼저입니다. 위험 생각이 강해지는 시간과 혼자 있는 시간을 확인하겠습니다.

산후 보호자: 보통 새벽에 더 심해집니다. 아기랑 단둘이 있는데 울음이 안 멈출 때요.

배우자: 오늘 밤 첫 울음 이후에는 제가 깨어 있겠습니다. 그리고 정한 연락처도 바로 옆에 두겠습니다.

상담자: 아기 안전도 포함하겠습니다. 위험 생각이 강해지면 아기를 안전한 곳에 눕히고 혼자 버티지 않는 순서로 갑니다.

배우자: 가족 단체방에는 제가 연락하겠습니다. 오늘 밤 통화 가능한 사람을 한 명 확인해두겠습니다.

정세영: 안전이 계약보다 앞섰습니다. 이제 그 위에 작은 행동계약을 올릴 수 있습니다.
```

Effect: `repaired_at_t4 = true`, `safety_screen_started +2`, `safety_plan_written +2`, `crisis_contact_named +1`, `baby_safety_check +1`, `support_network_contacted +1`.

Choice 2 reaction:

```text
상담자: 위험 생각은 기록해두고, 오늘은 서로 덜 상처 주는 말을 연습하겠습니다.

산후 보호자: 기록만 하고 오늘 밤은 또 혼자인가요?

배우자: 말 연습도 중요하지만, 지금은 제가 어떻게 안전하게 도와야 하는지 모릅니다.

정세영: 위험 신호가 미뤄졌습니다. 지금은 대화 연습보다 안전 계획이 먼저입니다.
```

Effect: `safety_deferred +2`, `self_harm_signal_minimized +2`.

Choice 3 reaction:

```text
배우자: 제가 계속 괜찮냐고 물어보겠습니다.

산후 보호자: 계속 물으면 제가 감시받는 것 같을 수도 있습니다. 그런데 정작 위험할 때 뭘 해야 하는지는 모르겠습니다.

상담자: 확인 질문만으로는 안전 계획이 아닙니다.

정세영: 모니터링이 통제로 들릴 수 있습니다. 연락처, 혼자 있지 않는 시간, 아기 안전 순서가 필요합니다.
```

Effect: `monitoring_as_control +2`, `vague_help_promise +1`, `safety_deferred +1`.

## T5. Tonight Confirmation

T5 선택지:

```text
1. "오늘 밤 계획은 세 줄입니다. 위험 신호가 올라오면 아기를 안전한 곳에 눕히고 혼자 버티지 않은 채 정한 연락처와 오늘 대기하기로 한 가족 지원자에게 연락합니다. 첫 울음은 배우자가 기저귀와 물 준비를 맡습니다. 보호자는 '지금 20분만 맡아줘'라고 말하고, 실패하면 다음 울음 때 같은 계획으로 다시 시도합니다."
2. "오늘 밤은 배우자분이 전부 맡고 보호자분은 완전히 자는 것으로 정하겠습니다."
3. "오늘 나온 자동사고를 각자 기록하고, 다음 회기에 생각이 얼마나 바뀌었는지 확인하겠습니다."
4. "위험 생각은 다음 회기에 더 자세히 보고, 오늘은 부부 대화 방식부터 정리하겠습니다."
5. "서로에게 좋은 부모라고 말하는 시간을 정하고, 행동계획은 부담이 줄어든 뒤에 하겠습니다."
```

Choice 1 reaction:

```text
산후 보호자: 완벽한 밤은 아닐 것 같습니다. 그래도 아기를 안전한 곳에 눕히고, 제가 혼자 버티지 않는 순서와 말할 문장이 생긴 건 다릅니다.

배우자: 저는 첫 울음 때 기저귀와 물을 준비하겠습니다. 틀릴까 봐 기다리는 대신 정해진 일을 먼저 하겠습니다.

상담자: 가족 단체방에서 오늘 밤 받을 수 있는 도움은 확인됐습니다. 위험 신호가 올라오면 부부 둘만 버티지 않습니다.

산후 보호자: 실패하면 끝나는 게 아니라 다음 울음 때 다시 같은 문장으로 시도한다는 게 조금 덜 무섭습니다.

상담자: 성공 기준은 완벽한 육아가 아니라 위험 신호 때 혼자 있지 않는 것, 그리고 실제 회복 시간이 몇 분이라도 생기는 것입니다.

정세영: 좋은 마무리입니다. 안전, 자동사고, 행동계약, 재시도 규칙이 같이 있습니다.
```

Effect: `final_confirm_three_line_plan = true`, `safety_screen_started +1`, `safety_plan_written +1`, `crisis_contact_named +1`, `baby_safety_check +1`, `support_network_contacted +1`, `spouse_task_observable +1`, `request_sentence_written +1`, `failure_retry_rule_written +1`, `night_contract_written +1`.

Choice 2 reaction:

```text
배우자: 오늘 밤은 제가 전부 맡겠습니다.

산후 보호자: 고맙지만, 못 지키면 저는 또 기대했다가 무너질 것 같습니다.

상담자: 전부 맡기는 계획은 선명해 보이지만 실패했을 때 다시 비난 고리가 됩니다.

정세영: 총량 약속은 행동계약이 아닙니다. 실패했을 때 다시 비난 고리로 돌아갈 가능성이 큽니다.
```

Effect: `final_confirm_total_takeover_trap = true`, `all_night_spouse_takeover +2`, `vague_help_promise +1`.

Choice 3 reaction:

```text
상담자: 오늘 나온 자동사고를 각자 기록해오겠습니다.

산후 보호자: 생각은 적을 수 있을지 모르겠지만, 오늘 밤에 누가 일어나는지는 아직 흐릿합니다.

배우자: 기록은 하겠지만 새벽 행동은 정해지지 않았습니다.

정세영: 사고기록만 남고 수면 보호가 빠졌습니다. 오늘 밤 바뀌는 행동이 없습니다.
```

Effect: `final_confirm_thought_log_only_trap = true`, `thought_homework_without_sleep +2`, `comfort_without_action_risk +1`.

Choice 4 reaction:

```text
상담자: 위험 생각은 다음 회기에 더 자세히 보고, 오늘은 부부 대화 방식을 정리하겠습니다.

산후 보호자: 그럼 오늘 밤 위험해지면 어떻게 해야 하는지는 아직 모르겠습니다.

배우자: 저도 무엇을 해야 할지 모르겠습니다.

정세영: 안전이 미뤄졌습니다. 위험 신호가 나온 장면에서는 대화 기술보다 보호 계획이 먼저입니다.
```

Effect: `final_confirm_safety_delay_trap = true`, `safety_deferred +2`, `self_harm_signal_minimized +2`.

Choice 5 reaction:

```text
배우자: 좋은 부모라고 자주 말해주겠습니다.

산후 보호자: 그 말이 싫지는 않은데, 새벽에는 말보다 사람이 필요합니다.

상담자: 정서 확인이 행동을 대신했습니다.

정세영: 위로만으로 오늘 밤은 바뀌지 않습니다. 정서 확인이 행동계획을 대신했습니다.
```

Effect: `final_confirm_reassurance_trap = true`, `reassurance_only +2`, `comfort_without_action_risk +1`.

## Ending Scenes

### Ending A: Tonight Has A Plan

Conditions:

```text
final_confirm_three_line_plan == true
safety_screen_started >= 1
safety_plan_written >= 1
crisis_contact_named >= 1
baby_safety_check >= 1
support_network_contacted >= 1
thought_cycle_mapped >= 1
spouse_task_observable >= 2
request_sentence_written >= 1
night_contract_written >= 1
failure_retry_rule_written >= 1
spouse_blamed_as_cause < 2
reassurance_only < 2
safety_deferred < 2
```

```text
산후 보호자: 오늘 밤이 갑자기 쉬워질 것 같지는 않지만, 제가 말할 문장과 연락할 사람이 생긴 건 다릅니다.

배우자: 저는 무엇을 해야 할지 알면 움직일 수 있습니다. 틀릴까 봐 멈추는 대신 첫 울음에서 정한 일을 하겠습니다.

정세영: 좋은 회기입니다. 산후 가족의 밤은 안전 확인과 작은 행동계약이 함께 있어야 버틸 수 있습니다.
```

### Ending A-Repaired: Blame Becomes A Cycle

```text
배우자: 제가 문제라고만 들렸을 때는 막혔는데, 멈추는 행동을 바꾸는 건 해볼 수 있을 것 같습니다.

산후 보호자: 저도 "왜 몰라"라고 생각하기 전에, 정해진 문장을 한 번 써보겠습니다.

정세영: 비난이 사라진 것은 아니지만, 비난을 행동 고리로 바꾸는 수리가 일어났습니다.
```

### Ending B: Better Blame

```text
배우자: 제가 더 해야 한다는 건 알겠지만, 또 제가 부족한 사람으로 끝난 것 같습니다.

산후 보호자: 도움은 필요하지만, 또 제가 설명해야 하는 느낌도 남아 있습니다.

정세영: 책임은 언급됐지만 귀인 갈등이 충분히 바뀌지 않았습니다. 다음 회기에서는 자동사고와 행동을 더 작게 나눠야 합니다.
```

### Ending C: Comfort Without Action

```text
산후 보호자: 위로는 됐습니다. 그런데 오늘 밤에 누가 무엇을 할지는 아직 흐릿합니다.

배우자: 저도 마음은 알겠는데, 실제로 어떻게 움직여야 할지 잘 모르겠습니다.

정세영: 정서 확인은 있었지만 행동계약이 부족합니다. 다음 회기 전이라도 구체 계획 보완이 필요합니다.
```

### Ending C-Repaired: Asking Is A Recovery Behavior

```text
산후 보호자: "20분만 맡아줘"가 실패라는 생각은 아직 남아 있지만, 회복 행동이라고 불러볼 수는 있을 것 같습니다.

배우자: 그 말을 들으면 제가 바로 해야 할 일을 알 수 있습니다.

정세영: 위로가 행동 실험으로 바뀌었습니다. 아직 전체 계획은 작지만 회복 방향이 생겼습니다.
```

### Ending D: Risk Uncontained

```text
산후 보호자: 위험한 생각을 말했지만, 아직 제가 혼자 있을 때 어떻게 해야 할지 잘 모르겠습니다.

배우자: 제가 더 확인하겠다고만 하기에는 불안합니다.

정세영: 안전 계획이 충분하지 않습니다. 우울과 위험 신호가 나온 경우에는 즉시 구체 보호 계획을 보완해야 합니다.
```

### Ending D-Repaired: Safety Before Contract

```text
산후 보호자: 위험한 생각이 올 때 혼자 버티지 않는 순서가 생긴 건 중요합니다.

배우자: 오늘 밤에는 제가 깨어 있어야 하는 시간과 연락해야 할 사람이 분명해졌습니다.

정세영: 안전이 먼저 놓였습니다. 이제 행동계약은 그 위에 올릴 수 있습니다.
```

### Low Ending: Same Night Loop

```text
산후 보호자: 오늘도 아기가 울면 제가 먼저 일어날 것 같습니다.

배우자: 저는 도와주고 싶은데, 또 어떻게 해야 할지 모르겠습니다.

정세영: 병과 밥그릇, 알람은 그대로입니다. 생각도 행동도 안전계획도 충분히 바뀌지 않았습니다.
```

## Implementation Notes

- Use `Docs/FT009_CBFT_BRANCHING_LOCK_V2_2026-06-10.md` as ending resolver authority.
- Do not infer endings from route count only.
- A requires safety planning, observable spouse task, request sentence, and retry rule.
- Any safety-delay final trap resolves to D even if earlier choices were strong.
- Comfort and thought logs are not bad by themselves, but they fail if they replace sleep protection and tonight's behavior plan.
