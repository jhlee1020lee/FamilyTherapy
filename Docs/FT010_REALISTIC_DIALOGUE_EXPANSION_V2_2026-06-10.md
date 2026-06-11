# FT-010 Realistic Dialogue Expansion V2

## Status

Use this V2 as the production replacement for T3, T4, and T5 of:

```text
Docs/FT010_REALISTIC_DIALOGUE_EXPANSION_2026-06-10.md
```

Use this with:

```text
Docs/FT010_SOLUTION_BRANCHING_LOCK_V2_2026-06-10.md
```

## Dialogue Rule

Do not make this a generic boundary-setting scene.

The high route must include:

```text
strengths acknowledged without romanticizing sacrifice
+ parentification risk named
+ exception identified and thickened
+ scaling from 8 to 7
+ one-point next step
+ adolescent self-time
+ small tasks from sibling/guardian/support person
+ recovery rule if the experiment fails
```

The adolescent must not be praised into more caregiving.

The ill guardian must not be reduced to guilt.

External support must not override family choice.

## Recurring Scene

```text
school backpack beside sibling medication checklist, unfinished homework, and a fridge calendar
```

The backpack marks the adolescent's life outside caregiving.

The checklist marks care work.

The fridge calendar marks whether one small exception can be repeated.

## T2-T3 Bridge. The 30-Minute Exception

```text
상담자: 지난주에 30분 늦게 와도 집이 무너지지 않았던 날을 자세히 보겠습니다. 그때 무엇이 평소와 달랐나요?

누나: 이웃 선생님이 동생을 데려다줬고, 동생이 준비물을 혼자 챙겼습니다. 저는 집에 와서 바로 밥부터 하지 않고 숙제를 조금 했어요.

보호자: 저는 그날 약 먹고 10분 정도는 옆에 앉아 있었습니다. 큰일은 아니라고 생각했는데, 아이가 덜 날카로웠던 것 같습니다.

동생: 제가 준비물 하나는 챙겼어요. 누나이 다 해주면 편하지만, 제가 한 날에는 덜 혼났어요.

상담자: 이미 30분의 예외 안에 여러 사람이 조금씩 움직인 흔적이 있습니다.

송지후: 해결중심에서는 예외를 칭찬으로 끝내지 않습니다. 어떻게 가능했는지 두껍게 묻고, 1점 낮추는 실험으로 바꿉니다.
```

## T3. Route-Specific Scaling Pressure

### `one_point_relief` T3: 8에서 7로

```text
상담자: 지금 부담을 0에서 10으로 놓으면 몇 점인가요?

누나: 8점이요. 이웃 선생님이 데려다준 날은 6점까지 내려간 것 같아요.

상담자: 완전히 0으로 만드는 게 아니라, 다음 주에 8을 7로 낮춘다면 무엇이 달라져야 할까요?

누나: 제가 집에 오자마자 바로 동생부터 보지 않고, 제 숙제를 30분 할 수 있으면 7점일 것 같아요.

보호자: 저는 약 먹은 뒤 10분이라도 옆에 앉아 있을 수 있습니다.

송지후: 숫자가 작아졌습니다. 이제 1점 변화의 구성 요소를 구체화하세요.
```

T3 선택지:

```text
1. "지난주 30분 예외를 자세히 보고, 다음 주 목표를 부담 8에서 7로 낮추는 것으로 잡겠습니다."
2. "30분이 생기면 청소년이 밀린 숙제와 집안일을 더 효율적으로 정리하도록 하겠습니다."
3. "이웃 선생님이 지원 가능한 시간을 먼저 정리하고 가족은 그 안에서 맞추겠습니다."
```

Immediate effects:

- 1: `strengths_acknowledged_without_romanticizing +1`, `parentification_risk_named +1`, `exception_identified +1`, `exception_thickened +1`, `scale_number_named +1`, `one_point_goal_set +1`.
- 2: `hero_praise_reinforced +1`, `sacrifice_moralized +1`.
- 3: `external_support_overrides_choice +1`, `family_choice_preserved -1`.

### `hero_burden` T3: 칭찬이 부담으로 들리는 순간

```text
누나: 제가 잘한다고 하면 기분은 좋은데, 그러면 제가 계속 해야 한다는 말처럼 들립니다.

보호자: 저는 고맙다는 말을 자주 했습니다. 그런데 그 말이 부탁처럼 들렸을 수 있겠네요.

동생: 누나이 잘한다고 하면 저는 계속 부탁해도 되는 줄 알았어요.

상담자: 책임감을 인정하는 말이 책임을 고정하는 말이 될 수 있습니다.

송지후: 강점을 지우지 말고, 강점이 혼자 짊어지는 이유가 되지 않게 바꾸세요.
```

T3 선택지:

```text
1. "해낸 것은 강점입니다. 동시에 그 강점이 혼자 짊어지는 이유가 되면 안 됩니다. 덜 힘들었던 예외를 찾아보겠습니다."
2. "청소년이 가족을 지켜온 책임감을 계속 살리되, 스트레스를 덜 받는 방법을 찾겠습니다."
3. "가족이 청소년에게 고마움을 더 자주 말해 부담을 알아주겠습니다."
```

Immediate effects:

- 1: `repair_started +1`, `strengths_acknowledged_without_romanticizing +1`, `parentification_risk_named +1`.
- 2: `hero_praise_reinforced +2`, `adolescent_role_locked +2`.
- 3: `sacrifice_moralized +2`, `adolescent_role_locked +1`.

### `guilt_centered` T3: 죄책감이 행동을 대신할 때

```text
보호자: 제가 미안하다고 하면 아이가 또 괜찮다고 합니다. 그 말을 들으면 더 미안해서 저는 더 아무것도 못 합니다.

누나: 보호자가 미안하다고 하면 제가 더 괜찮다고 해야 할 것 같아요. 힘들다는 말을 못 하겠습니다.

동생: 보호자가 울면 누나이 조용해져요.

상담자: 죄책감이 방을 차지하면 청소년은 다시 보호자를 달래는 자리로 돌아갑니다.

송지후: 죄책감을 없애려 하지 말고, 10분 행동으로 바꾸세요.
```

T3 선택지:

```text
1. "죄책감은 말로 오래 두지 않고, 보호자가 약 먹은 뒤 10분 숙제 옆에 있기라는 작은 행동으로 바꾸겠습니다."
2. "보호자가 충분히 미안함을 말하고, 청소년이 그 마음을 들은 뒤 계획으로 넘어가겠습니다."
3. "청소년이 보호자를 달래지 않도록, 보호자는 미안하다는 말을 줄이겠습니다."
```

Immediate effects:

- 1: `repair_started +1`, `guardian_small_task_named +1`, `adolescent_comforts_guardian -1`.
- 2: `guardian_guilt_centered +2`, `adolescent_comforts_guardian +2`.
- 3: `guilt_replaces_action +1`, `adolescent_role_locked +1`.

### `resource_takeover` T3: 지원이 선택권을 덮을 때

```text
이웃 교사/지원 인물: 주 1회 하교 동행은 확인할 수 있습니다. 지역센터 연결도 가능합니다.

누나: 도움이 필요하긴 한데, 갑자기 누가 다 정하면 제가 못 하는 사람처럼 보입니다.

보호자: 저희가 선택할 수 있는 범위가 있으면 좋겠습니다.

상담자: 지원이 필요하지만, 가족이 이미 경험한 30분 예외와 연결되어야 지속됩니다.

송지후: 외부 자원은 해결을 대신하지 않습니다. 가족이 선택한 작은 도움이어야 합니다.
```

T3 선택지:

```text
1. "지원은 연결하되, 지난주 30분 예외를 유지하는 작은 도움 하나만 가족이 고르겠습니다."
2. "청소년 부담이 크므로 가능한 지원을 많이 넣어 역할을 빠르게 줄이겠습니다."
3. "가족이 준비될 때까지 외부 지원은 보류하고 내부 대화만 먼저 하겠습니다."
```

Immediate effects:

- 1: `repair_started +1`, `chosen_support_contacted +1`, `family_choice_preserved +1`.
- 2: `external_support_overrides_choice +2`, `family_exception_ignored +2`.
- 3: `support_avoided +2`, `adolescent_role_locked +1`.

## T4. Playable One-Point Experiment

### `one_point_relief` T4: 이미 가능했던 30분을 반복하기

T4 선택지:

```text
1. "지난주 가능했던 30분을 다음 주 한 번 더 만들겠습니다. 동생은 준비물 하나, 보호자는 약 먹은 뒤 10분, 지원 인물은 주 1회 하교 동행 가능 여부 확인, 청소년은 그 30분에 자기 일을 하나만 합니다."
2. "30분을 확보하면 청소년이 밀린 숙제와 집안일을 더 효율적으로 정리하도록 하겠습니다."
3. "지원 인물이 가능한 도움을 먼저 정리하고 가족은 그 목록에서 선택하겠습니다."
```

Choice 1 reaction:

```text
상담자: 목표는 완벽한 분담이 아니라 이미 가능했던 30분을 한 번 더 만드는 것입니다.

누나: 30분이면 동아리까지는 아니어도 제 숙제는 할 수 있어요.

보호자: 저는 약 먹은 뒤 10분은 동생 숙제 옆에 있을 수 있습니다. 미안하다는 말만 하는 것보다는 낫습니다.

동생: 준비물 하나는 제가 할게요. 까먹으면 다시 같이 정하면 좋겠어요.

이웃 교사/지원 인물: 주 1회 하교 동행은 확인하겠습니다. 가족이 원하는 방식으로 작게 시작하겠습니다.

송지후: 좋은 실험입니다. 작고, 측정 가능하고, 가족이 선택한 도움입니다.
```

Effect: `strengths_acknowledged_without_romanticizing +1`, `parentification_risk_named +1`, `adolescent_self_time_named +1`, `sibling_small_task_named +1`, `guardian_small_task_named +1`, `chosen_support_contacted +1`, `family_choice_preserved +1`.

Choice 2 reaction:

```text
상담자: 30분이 생기면 청소년이 밀린 일들을 더 효율적으로 정리하도록 하겠습니다.

누나: 그러면 30분도 결국 집을 위해 쓰는 시간이네요.

보호자: 아이 시간이 생긴 것 같았는데, 다시 일 목록이 된 것 같습니다.

송지후: 자기 시간이 다시 돌봄 효율화로 바뀌었습니다. 부담이 내려가지 않습니다.
```

Effect: `hero_praise_reinforced +1`, `sacrifice_moralized +1`, `adolescent_role_locked +1`.

Choice 3 reaction:

```text
이웃 교사/지원 인물: 가능한 도움 목록을 정리해오겠습니다.

누나: 목록이 먼저 정해지면 제가 그 안에 맞춰야 할 것 같습니다.

보호자: 도움은 필요한데, 저희가 고르는 느낌이 적습니다.

송지후: 지원 목록이 예외보다 앞섰습니다. 선택권이 약해졌습니다.
```

Effect: `external_support_overrides_choice +2`, `family_exception_ignored +1`.

### `hero_burden` T4: 강점이 희생을 고정하지 않게 하기

T4 선택지:

```text
1. "제가 책임감을 칭찬하다가 계속 혼자 하라는 말처럼 만들었습니다. 해낸 것은 인정하되, 그 힘이 덜 필요했던 30분을 다시 찾겠습니다."
2. "청소년이 가족을 잘 지켜온 만큼, 다음 주도 중심 역할은 유지하되 스트레스를 덜 받는 방법을 찾겠습니다."
3. "가족이 청소년에게 고마움을 더 자주 말해 부담을 알아주겠습니다."
```

Choice 1 reaction:

```text
상담자: 책임감은 강점입니다. 하지만 그 강점이 계속 청소년 혼자 해야 하는 이유가 되면 안 됩니다.

누나: 그 말은 좀 다릅니다. 제가 잘했다는 말보다, 계속 제가 안 해도 된다는 쪽이 필요했습니다.

보호자: 고맙다는 말 뒤에 부탁이 붙지 않게 해야겠네요.

송지후: 좋은 수리입니다. 강점 인정과 부모화 위험이 같이 보였습니다.
```

Effect: `repaired_at_t4 = true`, `strengths_acknowledged_without_romanticizing +1`, `parentification_risk_named +1`, `exception_identified +1`.

Choice 2 reaction:

```text
누나: 결국 다음 주도 제가 중심이라는 말이네요.

동생: 누나이 계속 해주면 편하긴 해요.

보호자: 아이가 대견하다는 마음이 또 부담을 만든 것 같습니다.

송지후: 영웅 역할이 유지됐습니다. 강점이 희생을 고정했습니다.
```

Effect: `hero_praise_reinforced +2`, `adolescent_role_locked +2`.

Choice 3 reaction:

```text
보호자: 고맙다고 더 자주 말하겠습니다.

누나: 고맙다는 말은 좋은데, 할 일이 줄지는 않아요.

동생: 고맙다고 하면 계속 해줘도 되는 건가요?

송지후: 인정이 행동 변화를 대신했습니다. 희생이 미덕화됩니다.
```

Effect: `sacrifice_moralized +2`, `adolescent_role_locked +1`.

### `guilt_centered` T4: 죄책감을 10분 행동으로 바꾸기

T4 선택지:

```text
1. "죄책감은 말로 오래 두지 않고, 보호자가 약 먹은 뒤 10분 숙제 옆에 있기라는 작은 행동으로 바꾸겠습니다."
2. "보호자가 충분히 미안함을 말하고, 청소년이 그 마음을 들은 뒤 계획으로 넘어가겠습니다."
3. "청소년이 보호자를 달래지 않도록, 보호자는 미안하다는 말을 줄이겠습니다."
```

Choice 1 reaction:

```text
상담자: 미안하다는 말은 중요한 마음입니다. 다만 오늘은 그 마음을 10분 행동으로 바꿔보겠습니다.

보호자: 약 먹은 뒤 10분이면 해볼 수 있습니다. 동생 숙제 옆에 앉아 있는 것부터 하겠습니다.

누나: 10분이라도 보호자가 맡으면 제가 바로 밥부터 하지 않아도 될 것 같습니다.

동생: 보호자가 옆에 있으면 저도 해볼게요.

송지후: 죄책감이 행동 가능성으로 바뀌었습니다.
```

Effect: `repaired_at_t4 = true`, `guardian_small_task_named +2`, `guilt_replaces_action -1`, `adolescent_comforts_guardian -1`.

Choice 2 reaction:

```text
보호자: 정말 미안합니다. 내가 아프지만 않았어도 네가 이렇게까지 하지는 않았을 텐데요.

누나: 아니에요. 제가 하면 돼요. 그런 말 하지 마세요.

동생: 또 누나이 보호자를 달래고 있어요.

송지후: 죄책감이 다시 방을 차지했습니다. 청소년이 위로자 자리로 돌아갔습니다.
```

Effect: `guardian_guilt_centered +2`, `adolescent_comforts_guardian +2`.

Choice 3 reaction:

```text
상담자: 보호자는 미안하다는 말을 줄이고, 청소년은 보호자를 달래지 않는 연습을 하겠습니다.

보호자: 말을 줄이면 마음은 그대로 남을 것 같습니다.

누나: 제가 괜찮다고 말하지 않으면 더 나쁜 사람 같을 것 같습니다.

송지후: 말 금지만으로 행동 가능성이 생기지는 않습니다.
```

Effect: `guilt_replaces_action +1`, `adolescent_role_locked +1`.

### `resource_takeover` T4: 선택권을 보존하는 지원

T4 선택지:

```text
1. "외부 지원은 연결하되, 가족이 이미 경험한 30분 예외를 유지하는 작은 도움 하나만 고르겠습니다. 지원 인물은 주 1회 하교 동행 가능 여부만 확인합니다."
2. "청소년 부담이 크므로 가능한 지원을 많이 넣어 역할을 빠르게 줄이겠습니다."
3. "갑작스러운 지원은 부담스러우니 외부 지원은 보류하고 가족 내부 대화만 먼저 하겠습니다."
```

Choice 1 reaction:

```text
상담자: 지원은 필요합니다. 하지만 가족이 고른 작은 도움이어야 합니다. 지난주 30분 예외를 유지하는 도움부터 고르겠습니다.

누나: 주 1회 하교 동행 정도면 괜찮을 것 같습니다. 갑자기 다 바뀌는 건 싫어요.

보호자: 저희가 고르는 느낌이 있으면 덜 부담스럽습니다.

이웃 교사/지원 인물: 그 범위에서 가능 여부를 확인하겠습니다.

송지후: 지원이 가족 선택권을 덮지 않고 예외를 보강했습니다.
```

Effect: `repaired_at_t4 = true`, `chosen_support_contacted +2`, `family_choice_preserved +2`, `exception_thickened +1`.

Choice 2 reaction:

```text
이웃 교사/지원 인물: 여러 지원을 동시에 연결할 수는 있습니다.

누나: 갑자기 많이 들어오면 제가 실패한 사람처럼 보일 것 같습니다.

보호자: 필요하지만 너무 빨라서 따라가기 어렵습니다.

송지후: 지원이 가족의 작은 성공을 보강하지 못하고, 가족이 끌려가는 느낌을 만들었습니다.
```

Effect: `external_support_overrides_choice +2`, `family_exception_ignored +2`.

Choice 3 reaction:

```text
상담자: 외부 지원은 보류하고 내부 대화부터 이어가겠습니다.

누나: 그러면 다음 주도 거의 똑같을 것 같습니다.

보호자: 도움을 미루면 아이가 또 중심이 됩니다.

송지후: 지원 회피가 청소년 역할을 유지합니다.
```

Effect: `support_avoided +2`, `adolescent_role_locked +1`.

## T5. Next-Week Experiment Confirmation

T5 선택지:

```text
1. "다음 주 과제는 부담을 1점 낮추는 실험입니다. 성공 기준은 완벽한 돌봄이 아니라 청소년이 자기 시간 30분을 확보했는지입니다. 실패하면 누가 잘못했는지 따지지 않고, 어느 연결이 끊겼는지 다시 찾습니다."
2. "청소년이 지금까지 해온 강점을 유지하되, 스트레스를 덜 받는 방식으로 돌봄을 계속해보겠습니다."
3. "보호자의 미안함을 가족이 충분히 나누고, 행동 계획은 그 마음이 정리된 뒤 세우겠습니다."
4. "외부 지원을 우선 많이 연결하고, 가족은 지원을 받는 데 익숙해지는 것을 목표로 하겠습니다."
5. "가족이 준비될 때까지 외부 지원은 보류하고 내부 대화만 먼저 이어가겠습니다."
```

Choice 1 reaction:

```text
누나: 30분이면 작아 보이지만, 제 시간이 생긴다는 게 중요할 것 같습니다.

보호자: 미안하다고만 하지 않고, 제가 할 수 있는 10분을 말해보겠습니다.

동생: 준비물 하나는 제가 해볼게요. 까먹으면 다시 정하면 되죠?

이웃 교사/지원 인물: 주 1회 하교 동행 가능 여부를 확인하고, 가족이 정한 범위 안에서 돕겠습니다.

상담자: 실패했을 때는 누가 잘못했는지 따지지 않고, 어느 연결이 끊겼는지 다시 찾습니다.

송지후: 좋은 마무리입니다. 1점 변화는 작지만, 부모화된 역할을 실제로 움직입니다.
```

Effect: `final_confirm_one_point_experiment = true`, `one_point_goal_set +1`, `adolescent_self_time_named +1`, `recovery_rule_written +1`, `family_choice_preserved +1`.

Choice 2 reaction:

```text
상담자: 청소년의 강점을 유지하되 스트레스를 덜 받는 방법을 찾겠습니다.

누나: 계속 제가 중심이라는 말로 들립니다.

보호자: 대견하다는 말이 또 부탁처럼 되었습니다.

송지후: 강점 유지가 역할 유지로 바뀌었습니다.
```

Effect: `final_confirm_hero_maintenance_trap = true`, `hero_praise_reinforced +2`, `adolescent_role_locked +1`.

Choice 3 reaction:

```text
보호자: 미안하다는 말을 더 해야 할 것 같습니다.

누나: 괜찮아요. 제가 하면 돼요.

동생: 또 누나이 괜찮다고 말해요.

송지후: 죄책감이 행동 계획을 대신했습니다.
```

Effect: `final_confirm_guilt_apology_trap = true`, `guardian_guilt_centered +2`, `adolescent_comforts_guardian +1`.

Choice 4 reaction:

```text
이웃 교사/지원 인물: 여러 지원을 먼저 연결해보겠습니다.

누나: 제 얘기보다 지원 목록이 먼저 정해진 느낌입니다.

보호자: 저희가 어느 정도까지 괜찮은지 말하기 전에 일이 커지는 것 같습니다.

송지후: 지원이 선택권을 앞질렀습니다.
```

Effect: `final_confirm_resource_takeover_trap = true`, `external_support_overrides_choice +2`, `family_exception_ignored +1`.

Choice 5 reaction:

```text
상담자: 가족이 준비될 때까지 외부 지원은 보류하겠습니다.

누나: 그러면 다음 주도 제가 해야 할 것 같습니다.

보호자: 준비될 때까지 기다리면 아이가 계속 버팁니다.

송지후: 지원 회피가 역할 고정을 만듭니다.
```

Effect: `final_confirm_delay_support_trap = true`, `support_avoided +2`, `adolescent_role_locked +1`.

## Ending Scenes

### Ending A: One Point Less

Conditions:

```text
route_primary == one_point_relief
final_confirm_one_point_experiment == true
strengths_acknowledged_without_romanticizing >= 1
parentification_risk_named >= 1
exception_identified >= 1
exception_thickened >= 1
scale_number_named >= 1
one_point_goal_set >= 1
adolescent_self_time_named >= 1
sibling_small_task_named >= 1
guardian_small_task_named >= 1
chosen_support_contacted >= 1
family_choice_preserved >= 1
recovery_rule_written >= 1
```

```text
누나: 30분이면 작아 보이지만, 제 시간이 생긴다는 게 중요할 것 같습니다.

보호자: 미안하다고만 하지 않고, 제가 할 수 있는 10분을 말해보겠습니다.

동생: 준비물은 제가 해볼게요. 누나이 조금 덜 화내면 좋겠어요.

송지후: 좋은 회기입니다. 가족의 강점을 유지하면서 부담을 1점 낮추는 실험이 생겼습니다.
```

### Ending A-Repaired: Strength Without Sacred Sacrifice

```text
누나: 잘했다는 말보다, 계속 제가 안 해도 된다는 말이 더 필요했습니다.

보호자: 고마움이 부탁처럼 들리지 않게 해야겠습니다.

송지후: 강점은 인정됐지만 희생으로 고정되지 않았습니다. 수리가 일어났습니다.
```

### Ending B: Praised Into More Work

```text
누나: 잘하고 있다는 말은 들었는데, 계속 제가 해야 하는 것 같습니다.

보호자: 고맙고 미안하지만, 달라질 행동은 아직 잘 모르겠습니다.

송지후: 강점은 보였지만 부담이 분산되지 않았습니다. 다음 회기에서는 예외와 1점 변화를 다시 찾아야 합니다.
```

### Ending C: Guilt Replaces Action

```text
보호자: 미안하다는 말만 계속 나옵니다. 그런데 제가 뭘 할 수 있는지는 아직 잘 모르겠습니다.

누나: 괜찮아요. 제가 할 수 있어요.

송지후: 죄책감이 행동을 대신했습니다. 청소년이 다시 보호자를 위로하는 자리로 돌아갔습니다.
```

### Ending C-Repaired: Guilt Becomes Ten Minutes

```text
보호자: 10분이라도 동생 숙제 옆에 있을 수 있다면, 미안하다는 말만 하지는 않아도 되겠습니다.

누나: 10분이 작아도, 제가 바로 뛰어들지 않아도 되는 시간이 생깁니다.

송지후: 죄책감이 작은 행동으로 바뀌었습니다.
```

### Ending D: Help Without Ownership

```text
이웃 교사/지원 인물: 지원은 연결할 수 있습니다. 다만 가족이 선택한 방식인지 계속 확인해야 합니다.

누나: 도움이 오는 건 좋은데, 제 얘기보다 지원 목록이 먼저 정해진 느낌입니다.

송지후: 외부 지원은 들어왔지만 가족의 선택권과 예외 기반 계획이 약합니다. 지속성을 위해 다시 작게 조정해야 합니다.
```

### Ending D-Repaired: Chosen Help Preserves The Exception

```text
누나: 주 1회 하교 동행 정도면 괜찮을 것 같습니다. 제가 못 해서가 아니라, 30분을 만들기 위한 도움이라고 생각할 수 있어요.

보호자: 도움을 받되 저희가 고른 범위 안에서 시작하니 덜 무섭습니다.

송지후: 외부 지원이 가족 선택권을 덮지 않고 예외를 보강했습니다.
```

### Low Ending: Same Backpack

```text
누나: 다음 주도 학교 끝나면 바로 와야 할 것 같습니다.

보호자: 미안합니다. 또 그 말밖에 못 하겠네요.

동생: 준비물은 누나이 해주겠죠?

송지후: 가방과 약 확인표, 밀린 숙제와 냉장고 달력이 같은 자리에 남았습니다. 예외가 실험으로 바뀌지 못했습니다.
```

## Implementation Notes

- Use `Docs/FT010_SOLUTION_BRANCHING_LOCK_V2_2026-06-10.md` as ending resolver authority.
- Do not infer endings from route count only.
- A requires exception, scale, one-point goal, adolescent self-time, small tasks, chosen support, and recovery rule.
- Hero praise, centered guilt, and resource takeover are final traps even if earlier work was strong.
- Solution-focused dialogue must remain concrete: exception, scale, 1-point change, chosen support, recovery rule.
