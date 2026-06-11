# FT-007 Realistic Dialogue Expansion V2

## Status

Use this V2 as the production replacement for T3, T4, and T5 of:

```text
Docs/FT007_REALISTIC_DIALOGUE_EXPANSION_2026-06-10.md
```

Use this with:

```text
Docs/FT007_PSYCHODYNAMIC_BRANCHING_LOCK_V2_2026-06-10.md
```

## Dialogue Rule

Do not make the father a cartoon villain, the mother a simple enabler, or the adult child a passive victim.

The scene works when everyone is defending against shame:

```text
father: fear becomes contempt or moral verdict
adult child: shame becomes withdrawal
mother: anxiety becomes secret rescue
therapist: interpretation must wait until the family's own words can hold it
```

## Recurring Scene

```text
closed bedroom door beside job rejection papers and mother's hidden cash envelope
```

The envelope is love and evasion. The closed door is defense and stuckness. The rejection papers are evidence only if the family turns them into a trial.

## T2-T3 Bridge. Defense Loop On The Table

```text
상담자: 오늘은 돈 액수부터 정하기 전에, 이 세 물건을 놓고 시작하겠습니다. 닫힌 방문, 구직 서류, 그리고 어머니의 봉투입니다.

어머니가 식탁 끝에 봉투를 내려놓는다. 봉투는 접혀 있고, 아버지는 그쪽을 보지 않으려 한다.

성인자녀는 방문 쪽 의자에 앉는다. 손에는 구직 서류가 있지만 펼치지 않는다.

아버지는 낡은 작업화 사진이 들어 있는 휴대폰을 테이블에 뒤집어 놓는다.

송성문: 이것은 누가 잘못했는지 재판하는 장면이 아닙니다. 비난, 철수, 비밀 지원이 어떤 수치심을 막아주는지 보는 장면입니다.
```

## T3. Route-Specific Defense Pressure

### `shame_named` T3: 실패라는 말이 몸에 닿을 때

```text
아버지: 저는 "실패자"라고 말하려던 건 아닙니다. 그런데 면접 떨어졌다는 말을 들으면 속에서 그 말이 올라옵니다.

성인자녀: 말 안 해도 보여요. 아버지 얼굴 보면 "또 떨어졌냐"가 먼저 보여요.

어머니: 그때 제가 돈을 주면 잠깐 조용해집니다. 아이가 방에 들어가기 전에 붙잡는 것 같아서요.

상담자: 아버지는 두려움을 분노로 말하고, 자녀분은 수치심을 방으로 피하고, 어머니는 불안을 봉투로 낮춥니다.

송성문: 해석을 시작할 수 있지만 아주 천천히 해야 합니다. 방어를 공격하지 말고 방어가 무엇을 지키는지 보세요.
```

T3 선택지:

```text
1. "아버지의 분노, 자녀의 방 철수, 어머니의 봉투가 모두 수치심을 피하려는 방어라는 점을 천천히 놓고 보겠습니다."
2. "감정은 이해하지만, 성인자녀가 독립할 날짜와 생활 규칙을 먼저 정해야 합니다."
3. "어머니의 비밀 지원이 갈등을 키웠으니, 그 부분부터 분명히 다루겠습니다."
```

Immediate effects:

- 1: `defense_sequence_seen +1`, `adult_child_shame_named +1`, `father_fear_named +1`.
- 2: `contract_as_punishment +1`, `moral_verdict_reinforced +1`.
- 3: `mother_triangle_locked +1`, `father_excluded_from_money +1`.

### `moral_attack` T3: 구직 서류가 증거가 될 때

```text
아버지: 이력서 몇 장 넣었다고 노력했다고 할 수 있습니까. 결과가 없으면 현실을 봐야죠.

성인자녀가 서류를 접는다. 종이 모서리가 구겨진다.

성인자녀: 그럼 이건 증거네요. 제가 또 안 된 사람이라는 증거.

어머니: 규칙을 말해야 하는 건 알지만, 지금은 아이가 더 작아지는 것 같습니다.

상담자: 책임을 다루는 순간이 실패 재판처럼 바뀌었습니다. 그러면 행동 계약도 처벌로 들릴 수 있습니다.

송성문: 도덕 판단은 변화 요구처럼 보이지만, 자녀의 수치심을 잠그고 아버지의 두려움을 가립니다.
```

T3 선택지:

```text
1. "제가 책임을 말하려다 자녀분을 실패자로 세운 것 같습니다. 책임은 다루되, 이 말이 어떤 수치심을 잠그는지 먼저 보겠습니다."
2. "성인자녀가 다음 주까지 독립 계획과 구직 시간을 제출해야 합니다."
3. "아버지가 집안 규칙을 분명히 세우고, 자녀는 그 규칙을 따르겠다고 약속해야 합니다."
```

Immediate effects:

- 1: `repair_started +1`, `adult_child_shame_named +1`, `moral_attack_risk_score -1`.
- 2: `moral_verdict_reinforced +2`, `failure_label_reinforced +2`.
- 3: `contract_as_punishment +2`, `failure_label_reinforced +1`.

### `rescue_triangle` T3: 봉투가 조용하게 만드는 것

```text
어머니: 제가 돈을 주면 그날은 싸움이 없습니다. 아이가 밥도 먹고, 남편도 모르면 넘어갑니다.

아버지: 모르면 넘어간다는 말이 제일 화가 납니다. 이 집에서 저는 늘 마지막에 알게 됩니다.

성인자녀: 엄마한테 받으면 고맙고, 동시에 더 부끄럽습니다. 아버지가 알면 끝장이고요.

상담자: 봉투는 사랑이지만, 비밀일 때는 세 사람 모두를 숨게 합니다.

송성문: 어머니를 비난하면 안 됩니다. 이 봉투가 가족의 어떤 불안을 대신 처리하는지 보게 해야 합니다.
```

T3 선택지:

```text
1. "어머니의 도움을 비난하지 않겠습니다. 다만 비밀 봉투가 세 사람의 수치심과 분노를 어떻게 키우는지 보겠습니다."
2. "당장 싸움을 줄이려면 어머니가 조용히 도와주는 방식이 현실적입니다."
3. "어머니가 몰래 돈을 준 것이 문제의 핵심이므로, 그 책임부터 분명히 해야 합니다."
```

Immediate effects:

- 1: `mother_secret_support_named +1`, `defense_sequence_seen +1`, `repair_started +1`.
- 2: `secret_cash_reinforced +2`, `mother_triangle_locked +2`.
- 3: `father_excluded_from_money +1`, `mother_triangle_locked +1`, `alliance_rupture +1`.

### `premature_interpretation` T3: 맞는 해석이 공격이 될 때

```text
아버지: 투사라는 말은 듣기 싫습니다. 저는 제 자식을 걱정하는 겁니다. 왜 그걸 병처럼 말합니까.

성인자녀: 아버지가 화내면 저는 또 가만히 있겠습니다. 괜히 말하면 더 커지니까요.

어머니: 맞는 말인지 아닌지보다, 지금 남편이 공격받는다고 느끼는 것 같습니다.

상담자: 제가 가족의 언어보다 이론 언어를 먼저 썼습니다. 그러면 해석이 도움보다 공격으로 들립니다.

송성문: 해석은 타이밍입니다. 지금은 투사보다 "무엇이 가장 겁나는가"가 먼저입니다.
```

T3 선택지:

```text
1. "투사라는 말을 잠시 내려놓고, 아버지께서 자녀를 볼 때 가장 무서운 장면을 자신의 말로 말해보겠습니다."
2. "아버지의 실패 공포가 자녀에게 투사되는 장면을 더 분명히 해석하겠습니다."
3. "자녀분도 아버지의 말이 걱정에서 나온다는 점을 인정해보겠습니다."
```

Immediate effects:

- 1: `repair_started +1`, `interpretation_timing_score +1`, `father_fear_named +1`.
- 2: `premature_interpretation_attack +2`, `father_humiliated_by_analysis +2`.
- 3: `adult_child_shame_named -1`, `failure_label_reinforced +1`, `alliance_rupture +1`.

## T4. Playable Interpretation Timing

### `shame_named` T4: 방어를 공격하지 않고 해석하기

T4 선택지:

```text
1. "아버지의 분노, 자녀의 방 철수, 어머니의 비밀 봉투가 모두 수치심을 피하려는 방어라는 점을 천천히 놓고 보겠습니다."
2. "감정은 이해했으니 이제 독립 날짜와 생활비 중단 기준을 먼저 정하겠습니다."
3. "어머니의 비밀 지원이 문제의 핵심이므로, 어머니가 먼저 그 사실을 인정하고 사과해야 합니다."
```

Choice 1 reaction:

```text
상담자: 지금 세 분이 모두 무언가를 막고 있습니다. 아버지는 두려움을 막기 위해 화를 내고, 자녀분은 수치심을 막기 위해 방으로 가고, 어머니는 싸움을 막기 위해 봉투를 씁니다.

아버지: 제가 겁난다는 말은 익숙하지 않습니다. 화가 난다고 하는 게 훨씬 쉽습니다.

성인자녀: 방에 들어가면 덜 부끄러울 줄 알았는데, 사실 더 아무것도 못 하게 됩니다.

어머니: 봉투가 아이를 살리는 줄 알았는데, 셋 다 숨게 만든 것도 맞습니다.

송성문: 좋은 타이밍의 해석입니다. 이제 돈을 처벌이 아니라 구조로 다룰 수 있습니다.
```

Effect: `defense_sequence_seen +2`, `father_projection_softened +1`, `withdrawal_named_as_defense +1`, `mother_secret_support_named +1`.

Choice 2 reaction:

```text
아버지: 그래야 합니다. 날짜가 있어야 사람이 움직입니다.

성인자녀: 결국 또 저를 평가하는 거네요. 그 날짜 못 지키면 저는 끝인 거고요.

어머니: 계약을 만들기도 전에 아이가 재판받는 얼굴이 됐습니다.

상담자: 독립 날짜는 필요할 수 있지만, 지금은 수치심을 더 잠그는 처벌 계약으로 들립니다.

송성문: 감정 확인이 계약으로 너무 빨리 넘어갔습니다. B 경로로 기울었습니다.
```

Effect: `contract_as_punishment +2`, `moral_verdict_reinforced +1`, B drift.

Choice 3 reaction:

```text
어머니: 제가 잘못한 건 맞습니다. 그런데 사과부터 하라고 하면 제가 또 문제의 중심이 된 것 같습니다.

아버지: 그래서 결국 아내가 문제였다는 겁니까?

성인자녀: 엄마 때문에 싸우는 것 같으면 제가 돈 받은 걸 더 숨기고 싶어집니다.

상담자: 비밀 지원을 봐야 하지만, 어머니를 피고석에 세우면 삼각관계가 더 단단해집니다.

송성문: 봉투의 기능을 보려다 사람을 문제로 만들었습니다. C/D 혼합 위험입니다.
```

Effect: `mother_triangle_locked +1`, `alliance_rupture +1`, C/D drift.

### `moral_attack` T4: 책임과 실패자 낙인 분리하기

T4 선택지:

```text
1. "제가 책임을 말하려다 자녀분을 실패자로 세웠습니다. 책임은 다루되, 실패자 언어가 수치심을 어떻게 잠그는지 먼저 보겠습니다."
2. "성인자녀가 다음 주까지 독립 계획과 구직 시간을 제출하고, 부모가 이행 여부를 평가하겠습니다."
3. "아버지가 집안 규칙을 분명히 세우고, 자녀는 그 규칙을 따르겠다고 약속해야 합니다."
```

Choice 1 reaction:

```text
상담자: 책임을 없애자는 것이 아닙니다. 실패자라는 말이 나오면 책임 이야기가 행동으로 가지 못하고 수치심으로 잠깁니다.

아버지: 제가 한심하다고 말하면 속이 시원한 줄 알았는데, 그 다음은 늘 방문이 닫혔습니다.

성인자녀: 실패자라는 말만 안 들으면, 구직 이야기를 조금은 할 수 있을 것 같습니다.

어머니: 이제야 규칙이 싸움이 아니라 대화가 될 수도 있겠다는 생각이 듭니다.

송성문: 수리 방향입니다. 책임을 방어가 아니라 계약으로 옮길 준비가 생겼습니다.
```

Effect: `repaired_at_t4 = true`, `adult_child_shame_named +1`, `father_fear_named +1`, `moral_attack_risk_score -1`.

Choice 2 reaction:

```text
아버지: 좋습니다. 제출하고 평가해야 합니다. 말만 해서는 안 됩니다.

성인자녀: 제출물 검사받는 것 같네요. 못 하면 또 한심한 거고요.

어머니: 아이가 계획을 말하기도 전에 떨어질 준비를 하는 것 같습니다.

상담자: 계획 제출이 가족 안에서는 실패 재판으로 들립니다.

송성문: 계약이 처벌로 변했습니다. B 경로가 강화됩니다.
```

Effect: `moral_verdict_reinforced +2`, `contract_as_punishment +2`, B.

Choice 3 reaction:

```text
아버지: 이 집에서 지켜야 할 규칙은 제가 정해야 합니다.

성인자녀: 그럼 저는 또 따르거나 나가거나 둘 중 하나네요.

어머니: 그렇게 말하면 아이가 더 방으로 들어갈 것 같습니다.

상담자: 아버지 권한이 선명해졌지만, 자녀의 수치심은 다뤄지지 않았습니다.

송성문: 규칙이 필요하지만 지금 방식은 실패 낙인을 강화합니다.
```

Effect: `failure_label_reinforced +2`, `withdrawal_locked +1`, B.

### `rescue_triangle` T4: 비밀 구조를 공개 장부로 옮기기

T4 선택지:

```text
1. "어머니의 도움을 사랑으로 인정하되, 비밀 봉투가 세 사람을 어떻게 숨게 만드는지 보겠습니다. 앞으로 지원은 세 사람이 보는 장부에 적겠습니다."
2. "당장 싸움을 줄이기 위해 어머니가 생활비 관리를 맡고, 아버지와 자녀는 돈 이야기를 직접 하지 않겠습니다."
3. "어머니가 몰래 돈을 준 것은 잘못이므로, 아버지에게 사과하고 앞으로 중단하겠다고 약속해야 합니다."
```

Choice 1 reaction:

```text
상담자: 어머니의 봉투는 사랑입니다. 동시에 비밀일 때는 아버지를 밖에 세우고, 자녀분의 수치심을 더 깊게 합니다.

어머니: 도와주는 마음까지 부정당하지 않으니 들을 수 있습니다. 숨기지 않는 장부라면 해볼 수 있습니다.

아버지: 제가 모르는 봉투가 아니라, 같이 보는 장부라면 화가 덜 날 것 같습니다.

성인자녀: 돈 받는 게 부끄럽지만, 숨기는 게 더 부끄러웠던 것 같습니다.

송성문: 좋은 수리입니다. 구조가 비밀에서 명시적 계약으로 이동했습니다.
```

Effect: `repaired_at_t4 = true`, `mother_secret_support_named +2`, `money_contract_score +1`, `mother_triangle_locked -1`.

Choice 2 reaction:

```text
어머니: 제가 맡으면 당장은 조용할 수 있습니다.

아버지: 또 제가 빠지는 겁니다. 돈 문제에서 저는 늘 마지막입니다.

성인자녀: 엄마한테만 말하면 편하지만, 아버지는 더 화낼 것 같습니다.

상담자: 갈등은 낮아졌지만 삼각관계가 유지됩니다.

송성문: 조용한 구조가 반복을 유지합니다. C 경로입니다.
```

Effect: `secret_cash_reinforced +2`, `mother_rescue_as_solution +2`, `father_excluded_from_money +1`, C.

Choice 3 reaction:

```text
아버지: 사과는 받아야겠습니다. 제가 모르게 돈이 오간 건 말이 안 됩니다.

어머니: 사과하겠습니다. 그런데 지금은 제가 전부 잘못한 사람처럼 느껴집니다.

성인자녀: 제가 돈 받은 것도 문제잖아요. 그럼 저도 더 숨고 싶습니다.

상담자: 사과가 필요할 수 있지만, 지금은 봉투의 기능보다 사람의 죄책감이 방을 차지합니다.

송성문: 구조 해석이 비난으로 바뀌었습니다. 동맹이 흔들립니다.
```

Effect: `alliance_rupture +2`, `mother_triangle_locked +1`, D/C low.

### `premature_interpretation` T4: 해석의 타이밍을 되돌리기

T4 선택지:

```text
1. "투사라는 말을 잠시 내려놓고, 아버지께서 자녀를 볼 때 가장 무서운 장면을 자신의 말로 말해보겠습니다."
2. "아버지의 실패 공포가 자녀에게 투사되는 장면을 더 분명히 해석하겠습니다."
3. "자녀분도 아버지의 말이 걱정에서 나온다는 점을 인정해보겠습니다."
```

Choice 1 reaction:

```text
상담자: 투사라는 말은 잠시 내려놓겠습니다. 아버지 말로, 자녀분을 볼 때 가장 무서운 장면은 무엇입니까.

아버지: 방문이 계속 닫혀 있는 겁니다. 어느 날 정말 아무것도 안 하게 될까 봐 겁납니다.

성인자녀: 그렇게 말하면 화내는 것보다 듣기 낫습니다. 그래도 무섭긴 합니다.

어머니: 지금은 분석받는 느낌보다 가족 이야기를 하는 느낌입니다.

송성문: 좋은 수리입니다. 가족의 언어가 생겼으니 해석은 나중에 붙일 수 있습니다.
```

Effect: `repaired_at_t4 = true`, `interpretation_timing_score +2`, `father_fear_named +1`, `father_humiliated_by_analysis -1`.

Choice 2 reaction:

```text
상담자: 아버지의 실패 공포가 자녀분에게 투사되면서, 자녀분은 아버지의 과거 실패 공포를 대신 떠안는 것 같습니다.

아버지: 또 분석이군요. 저는 그냥 제 아이가 걱정된다고 했습니다.

성인자녀: 맞는 말일 수도 있는데, 지금은 아버지가 더 화난 것 같습니다.

어머니: 말이 점점 어려워지고, 서로 멀어지는 것 같습니다.

송성문: 이론은 맞아도 타이밍이 틀렸습니다. D 경로입니다.
```

Effect: `premature_interpretation_attack +2`, `father_humiliated_by_analysis +2`, D.

Choice 3 reaction:

```text
상담자: 자녀분도 아버지 말이 걱정에서 나온다는 점을 인정해볼 수 있을까요.

성인자녀: 또 제가 이해해야 하는 거네요. 아버지가 한심하다고 해도 걱정이니까요?

아버지: 저는 한심하다는 말을 안 하려고 했습니다. 그런데 이렇게 되니 또 변명처럼 들립니다.

어머니: 둘 다 방어적으로 변했습니다.

송성문: 자녀의 수치심이 충분히 다뤄지기 전에 아버지를 이해하라고 요구했습니다. 동맹이 흔들립니다.
```

Effect: `alliance_rupture +1`, `failure_label_reinforced +1`, D/B low.

## T5. Final Money And Respect Contract

T5 선택지:

```text
1. "다음 주까지 돈 문제를 비밀로 처리하지 않는 규칙을 세우겠습니다. 월 지원 범위, 지원 기간, 다음 검토일, 구직 행동, 집안 역할을 세 사람이 한 장에 적고, 면접 탈락 후에는 평가하지 않고 먼저 '무엇이 두려운가'를 말하겠습니다. '게으르다', '실패자', '한심하다'는 표현은 중단합니다."
2. "성인자녀는 다음 주까지 독립 계획과 구직 계획을 제출하고, 부모님은 결과를 평가하겠습니다."
3. "갈등을 줄이기 위해 어머니가 생활비 관리를 맡고, 아버지와 자녀는 당분간 돈 이야기를 직접 하지 않겠습니다."
4. "아버지의 투사와 자녀의 수치심 방어를 가족이 인정하는 것으로 회기를 마무리하겠습니다."
```

Choice 1 reaction:

```text
아버지: 지원은 한 달 단위로 검토하고, 금액도 장부에 적겠습니다. 실패자라는 말은 쓰지 않겠습니다. 화가 날 때는 제가 무엇이 겁나는지 먼저 말해보겠습니다.

성인자녀: 저는 지원한 곳, 면접 여부, 집에서 맡을 일을 적겠습니다. 떨어졌을 때 바로 평가받지 않는다는 조건이 있으면 말할 수 있습니다.

어머니: 저는 몰래 돈을 주지 않겠습니다. 도와야 할 일이 있으면 이 장부에 적고, 다음 검토일에 셋이 말하겠습니다.

상담자: 이 계약은 처벌이 아니라 수치심과 비밀을 줄이기 위한 구조입니다.

송성문: 좋은 마무리입니다. 돈과 존중이 같은 종이에 올라왔습니다.
```

Effect: `final_confirm_explicit_contract = true`, `money_contract_written +2`, `respect_language_rule_written +1`, `defense_sequence_seen +1`.

Choice 2 reaction:

```text
아버지: 제출하고 평가해야 합니다. 그래야 달라집니다.

성인자녀: 저는 또 심사받는 느낌입니다. 실패하면 끝인 거죠.

어머니: 계획표가 필요하지만, 지금은 벌점표처럼 들립니다.

송성문: 독립 계획이 처벌 계약으로 잠겼습니다. 최종 B 트랩입니다.
```

Effect: `final_confirm_independence_trial_trap = true`, `contract_as_punishment +1`, `moral_verdict_reinforced +1`.

Choice 3 reaction:

```text
어머니: 제가 맡으면 당장은 덜 싸울 수 있습니다.

아버지: 그럼 저는 또 빠지는 겁니다.

성인자녀: 엄마한테만 말하면 편하지만, 더 숨어야 할 것 같습니다.

송성문: 어머니가 관리자이자 완충자가 되면서 비밀 구조가 유지됩니다. 최종 C 트랩입니다.
```

Effect: `final_confirm_mother_manager_trap = true`, `mother_rescue_as_solution +1`, `father_excluded_from_money +1`.

Choice 4 reaction:

```text
상담자: 오늘은 아버지의 투사와 자녀분의 수치심 방어를 이해한 것으로 정리하겠습니다.

아버지: 그래서 돈 문제는 어떻게 하자는 겁니까.

성인자녀: 말은 맞는 것 같은데, 집에 가면 똑같을 것 같습니다.

송성문: 해석은 남았지만 구조가 남지 않았습니다. 최종 D 트랩입니다.
```

Effect: `final_confirm_analysis_trap = true`, `premature_interpretation_attack +1`, `money_contract_written -1`.

## Ending Scenes

### Ending A: Shame Can Be Spoken

Conditions:

```text
money_contract_written >= 2
respect_language_rule_written >= 1
defense_sequence_seen >= 2
adult_child_shame_named >= 1
father_fear_named >= 1
moral_verdict_reinforced < 2
mother_triangle_locked < 2
premature_interpretation_attack < 2
```

```text
아버지: 제가 겁난다는 말을 먼저 해보겠습니다. 한심하다는 말은 쓰지 않겠습니다.

성인자녀: 방에 들어가기 전에 구직 이야기를 한 번은 말해보겠습니다. 실패자라는 말만 안 나오면요.

어머니: 봉투를 숨기지 않겠습니다. 도와야 할 일이 있으면 같이 보는 장부에 적겠습니다.

송성문: 좋은 회기입니다. 가족이 돈과 수치심을 분리해 말하기 시작했습니다.
```

### Ending A-Repaired: Contract After A Rupture

```text
성인자녀: 중간에 또 평가받는 것 같아서 닫혔는데, 마지막 계약은 조금 다르게 들렸습니다.

아버지: 완전히 믿기는 어렵지만, 실패자라는 말 대신 걱정을 말해보겠습니다.

송성문: 완벽한 회기는 아니지만 수리되었습니다. 계약이 처벌에서 구조로 이동했습니다.
```

### Ending B: Failure Verdict

```text
아버지: 규칙이 생기면 이제는 달라져야 합니다.

성인자녀: 또 평가받는 느낌입니다. 지키든 못 지키든 저는 실패자일 것 같습니다.

송성문: 책임은 다뤄졌지만 수치심은 더 깊어졌습니다. 다음 회기에서는 도덕 판단이 반복을 어떻게 유지하는지 다시 봐야 합니다.
```

### Ending C: Quiet Rescue

```text
어머니: 당장은 조용해질 수 있을 것 같습니다. 그런데 또 비밀이 생기는 건 무섭습니다.

아버지: 제가 모르는 지원이 계속되면 저는 더 화가 날 겁니다.

송성문: 비밀 지원은 갈등을 낮추지만 증상을 유지합니다.
```

### Ending C-Repaired: Envelope On The Table

```text
어머니: 봉투를 숨기지 않고 장부 위에 놓겠습니다. 돕는 마음은 있지만, 비밀로 하지는 않겠습니다.

아버지: 제가 늦게 알게 되는 일이 줄면, 화가 조금 덜 날 것 같습니다.

성인자녀: 돈 받는 게 부끄럽지만, 숨기는 것보다는 낫습니다.

송성문: 어머니의 사랑이 비밀 삼각관계에서 명시적 구조로 옮겨졌습니다.
```

### Ending D: Analyzed, Not Met

```text
아버지: 분석받은 느낌이 아직 불편합니다. 저는 제 이야기를 한 것 같지 않습니다.

성인자녀: 맞는 말일 수도 있지만, 우리 얘기보다 이론 얘기 같았습니다.

송성문: 해석이 너무 빨랐습니다. 다음 회기에서는 가족의 언어로 돌아가 안전한 경험을 먼저 만들어야 합니다.
```

### Ending D-Repaired: Fear Before Projection

```text
아버지: 투사라는 말보다, 방문이 계속 닫힐까 봐 겁난다는 말이 더 맞습니다.

성인자녀: 그렇게 말하면 제가 바로 방에 들어가고 싶지는 않습니다.

송성문: 해석을 가족의 언어로 낮췄습니다. 이론보다 경험이 먼저였습니다.
```

### Low Ending: Same Door, Same Envelope

```text
성인자녀: 그냥 방에 들어가겠습니다. 말하면 더 복잡해집니다.

어머니: 봉투를 다시 넣어둘게요. 오늘은 더 말하지 않는 게 나을 것 같습니다.

송성문: 아무도 악의적이지 않지만 반복은 그대로입니다. 방문과 봉투가 같은 자리에 남았습니다.
```

## Implementation Notes

- Use `Docs/FT007_PSYCHODYNAMIC_BRANCHING_LOCK_V2_2026-06-10.md` as ending resolver authority.
- Do not infer endings from route count only.
- The strongest path separates concrete money structure from shame language.
- Interpretation is only therapeutic after the family has enough shared language for fear, shame, and defense.
