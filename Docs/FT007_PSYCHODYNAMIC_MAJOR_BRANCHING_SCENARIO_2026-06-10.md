# FT-007 Psychodynamic Major Branching Scenario

## Goal

FT-007 focuses on an adult child returning to the family home after repeated employment failure. The presenting problem is economic dependence, but the psychodynamic family therapy focus is shame, projection, defensive contempt, and a secret rescue triangle.

The player should help the family hear "lazy" and "failure" as defenses against shame and disappointment, not as final truths.

## Cast

Names are not locked yet. Use role labels until final names are assigned.

| Character | Role | Surface statement | Hidden pressure |
| --- | --- | --- | --- |
| 아버지 | father | "게으른 겁니다." | 자신의 실패 공포와 기대를 자녀에게 투사 |
| 어머니 | mother | "둘이 싸우는 게 싫어서 조금 도와줬어요." | 비밀 지원으로 갈등을 낮추지만 삼각관계 유지 |
| 성인자녀 | adult child | "아버지는 제가 뭘 해도 실패자라고 보잖아요." | 수치심, 무력감, 인정받고 싶은 마음 |
| 송성문 | psychodynamic supervisor | 방어, 투사, 수치심, 반복을 짚음 | 느리고 해석적이며 성급한 폭로를 경계 |

## Route Model

| Route ID | Name | Entry condition | Session tone |
| --- | --- | --- | --- |
| `shame_named` | 수치심 명명 루트 | 게으름/실패 언어 아래 감정을 반영 | 가족이 비난 뒤의 두려움을 듣기 시작 |
| `moral_attack` | 도덕 판단 루트 | 성인자녀 책임/게으름을 직접 지적 | 자녀가 철수하고 아버지가 강화됨 |
| `rescue_triangle` | 비밀 구조 유지 루트 | 어머니 지원을 해결책으로 유지 | 아버지-자녀 갈등이 어머니를 통해 우회 |
| `premature_interpretation` | 해석 과속 루트 | 투사/무의식 해석을 너무 빨리 제시 | 가족이 분석당한다고 느끼며 방어 |

## Five-Turn Flow

```text
Common intro
-> T1: 의존을 도덕 문제로 볼 것인가, 수치심 신호로 볼 것인가
-> T2: 비난-철수-비밀지원 고리 보기
-> T3: 실패와 기대의 반복
-> T4: 방어를 낮추는 해석
-> T5: 돈과 존중의 명시적 계약
```

## Common Intro

```text
아버지: 저 나이까지 부모 집에 있으면 부끄러운 줄 알아야 합니다. 일자리를 못 구한 게 아니라, 버티는 힘이 없는 겁니다.

성인자녀: 아버지는 제가 뭘 해도 실패자라고 봅니다. 그래서 말을 안 하는 게 낫습니다.

어머니: 둘이 부딪히는 게 싫어서 제가 중간에서 조금 도와줬습니다. 몰래 용돈을 준 것도 사실입니다.

송성문: 경제 문제는 실제로 다뤄야 합니다. 다만 지금 이 가족은 돈을 말할 때 수치심과 실패 공포도 같이 말하고 있습니다.
```

## T1. Initial Definition

| Choice | Player line | Route impact | Immediate response |
| --- | --- | --- | --- |
| A | "돈 문제를 정리하되, 그 말이 나올 때 각자 어떤 수치심과 분노가 올라오는지도 같이 보겠습니다." | `shame_named +2` | 가족이 불편하지만 깊은 이야기가 열림 |
| B | "성인자녀가 책임감을 회복하도록 생활 규칙을 먼저 엄격히 정하겠습니다." | `moral_attack +2` | 아버지는 동의, 자녀는 철수 |
| C | "어머니가 당분간 경제적으로 완충해 주시면 갈등이 줄어들 수 있습니다." | `rescue_triangle +2` | 어머니의 비밀 동맹이 강화 |
| D | "아버지는 자신의 실패 공포를 자녀에게 투사하고 계신 것 같습니다." | `premature_interpretation +2` | 이론은 맞지만 아버지가 공격으로 느낌 |

## T2. Route Scenes

### `shame_named`

```text
성인자녀: 돈 얘기가 나오면 제가 집에 있는 사람이라는 게 몸으로 느껴집니다. 그래서 방에 들어가 버립니다.
아버지: 저는 그 모습을 보면 더 화가 납니다. 화가 나는 동시에 겁도 납니다. 저렇게 계속 살까 봐요.
어머니: 둘이 다칠까 봐 제가 돈을 주면 잠깐 조용해집니다. 그런데 그 뒤에 더 큰 싸움이 옵니다.
송성문: 비난, 철수, 비밀 지원이 서로를 지키는 방어처럼 작동합니다.
```

Good T2 choice:

```text
"아버지의 분노와 자녀의 철수가 만나는 순간을 천천히 보겠습니다. 그때 어머니가 돈으로 조용하게 만드는 방식도 같이 놓겠습니다."
```

### `moral_attack`

```text
성인자녀: 여기서도 제가 의지가 없는 사람이라는 말을 들어야 하는 거네요.
아버지: 틀린 말은 아니지 않습니까. 누군가는 현실을 말해야 합니다.
어머니: 또 시작될 것 같아서 무섭습니다.
송성문: 도덕 판단은 가족이 이미 반복해 온 방어를 치료실에서 재현합니다.
```

Repair T2 choice:

```text
"제가 책임을 말하려다 자녀분을 다시 실패자로 세운 것 같습니다. 책임은 다루되, 먼저 이 말이 가족 안에서 어떤 상처를 반복하는지 보겠습니다."
```

### `rescue_triangle`

```text
어머니: 제가 조금 도와주면 적어도 싸움은 줄어듭니다.
아버지: 그래서 제가 모르는 일이 생기고, 저는 더 화가 납니다.
성인자녀: 엄마가 도와주는 것도 미안하고, 아버지가 알면 또 끝장입니다.
송성문: 구조는 조용해 보이지만, 비밀이 수치심을 더 두껍게 만듭니다.
```

Repair T2 choice:

```text
"어머니의 도움을 비난하지 않겠습니다. 다만 비밀로 유지될 때 이 가족의 수치심과 분노가 어떻게 커지는지 보겠습니다."
```

### `premature_interpretation`

```text
아버지: 지금 저를 분석하시는 겁니까? 저는 그냥 현실을 말한 겁니다.
성인자녀: 아버지가 또 화내면 저는 가만히 있을게요.
송성문: 해석은 타이밍이 중요합니다. 가족이 감당할 언어가 생기기 전에 해석하면 공격으로 들립니다.
```

Repair T2 choice:

```text
"제가 너무 빨리 해석했습니다. 지금은 분석보다, 아버지께서 자녀를 볼 때 가장 두려운 장면이 무엇인지 듣겠습니다."
```

## T3. Shame And Repetition

Core scene:

```text
아버지: 저도 젊을 때 실패하면 아무도 기다려주지 않았습니다. 그래서 더 이를 악물고 버텼습니다.
성인자녀: 저는 그 말을 들을 때마다, 도움을 요청하면 약한 사람이 되는 것 같습니다.
어머니: 저는 남편이 무서운 게 아니라, 둘이 서로를 같은 말로 다치게 하는 게 무섭습니다.
송성문: 아버지는 자기 생존 방식을 자녀에게 요구하고, 자녀는 그 요구를 실패자 낙인으로 경험합니다.
```

Key choices:

```text
A. "아버지의 버티라는 말 속에는 걱정이 있지만, 자녀에게는 실패자라는 낙인으로 들리는 것 같습니다."
B. "자녀분은 이제 성인이니 부모의 도움을 받지 않는 것이 우선입니다."
C. "어머니가 중간에서 돈을 줄지 말지 명확히 결정하면 됩니다."
```

## T4. Core Psychodynamic Intervention

Target intervention:

```text
"지금은 서로가 서로에게 '실패'라는 말을 던지는 방식이 반복됩니다. 아버지는 두려움을 분노로, 자녀는 수치심을 철수로, 어머니는 불안을 비밀 지원으로 처리하는 것 같습니다. 이 중 한 가지를 오늘 다르게 해보겠습니다."
```

Expected response:

```text
아버지: 제가 겁난다는 말은 거의 해본 적이 없습니다. 화를 내는 게 더 쉬웠습니다.
성인자녀: 저는 방에 들어가면 덜 부끄러울 줄 알았는데, 더 아무것도 못 하게 됩니다.
어머니: 제가 몰래 돕는 것도 결국 둘을 더 멀게 했네요.
```

## T5. Closure

Best closure:

```text
"다음 주까지 돈 문제를 비밀로 처리하지 않는 규칙을 세우겠습니다. 생활비 지원 여부, 구직 행동, 집안 역할을 세 사람이 같이 적고, 비난이나 실패자 표현이 나오면 대화를 멈추고 다시 '나는 무엇이 두려운가'로 돌아오겠습니다."
```

## Endings

| Ending | Condition | Result |
| --- | --- | --- |
| A. Shame Can Be Spoken | shame route maintained | 가족이 돈과 수치심을 분리해 말하고 작은 명시적 계약을 만듦 |
| B. Failure Verdict | moral route dominant | 자녀가 더 철수하고 아버지의 도덕 판단이 강화 |
| C. Quiet Rescue | rescue route dominant | 어머니 비밀 지원이 계속되어 갈등은 숨지만 반복 |
| D. Analyzed, Not Met | interpretation route dominant | 맞는 해석이 공격처럼 들려 치료동맹 약화 |

## Dialogue Expansion Notes

- 아버지는 비난하지만, 그 아래에는 두려움과 자기 생존 방식이 있다.
- 성인자녀는 무책임함보다 수치심과 무력감으로 방에 들어간다.
- 어머니의 비밀 지원은 사랑이지만 동시에 삼각관계 유지 장치다.
- 송성문은 해석을 던지기보다 가족이 감당할 언어를 먼저 만든다.
