# FT-003 Structural Major Branching Scenario

## Goal

FT-003 focuses on a dual-earner family raising a child with developmental delay. The presenting issue is repeated fights over therapy schedules, but the clinical issue is the disorganized boundary between the couple subsystem, the parent-child subsystem, and treatment professionals.

The player should learn that more therapy hours are not automatically better if the family structure cannot carry them.

## Cast

Names are not locked yet. Use role labels until final names are assigned.

| Character | Role | Surface statement | Hidden pressure |
| --- | --- | --- | --- |
| 아버지 | father | "계획대로 해야 좋아집니다." | 치료 성과를 놓치면 부모 역할을 실패한다고 느낌 |
| 어머니 | mother | "아이가 지쳤습니다." | 쉬자는 말이 포기처럼 들릴까 봐 방어함 |
| 자녀 | child with developmental delay | "오늘은 아무 데도 안 가고 싶어요." | 치료 일정 속에서 선택권과 휴식이 사라짐 |
| 치료사/기관 담당자 | outside helper | "출석이 불규칙하면 치료 목표 조정이 어렵습니다." | 가족의 부부갈등을 일정 문제로만 봄 |
| 이정후 | structural supervisor | 경계, 위계, 실연을 짚음 | 단호하지만 가족을 탓하지 않음 |

## Route Model

| Route ID | Name | Entry condition | Session tone |
| --- | --- | --- | --- |
| `parental_alignment` | 부모 하위체계 정렬 루트 | 부모가 자녀 앞에서 싸우는 구조를 다룸 | 부모가 같은 팀으로 말하기 시작 |
| `schedule_escalation` | 일정 강화 루트 | 치료 시간/출석률만 강화 | 아이가 더 지치고 부부는 서로를 탓함 |
| `mother_child_coalition` | 어머니-자녀 밀착 루트 | 어머니와 아이만 보호하고 아버지를 배제 | 아버지가 성과 언어로 더 강하게 들어옴 |
| `professional_takeover` | 전문가 위임 루트 | 기관/치료사가 가족 결정을 대신하게 함 | 가족 주도권이 약해지고 부부 위계가 더 흔들림 |

## Five-Turn Flow

```text
Common intro
-> T1: 일정 문제인가, 가족 구조 문제인가
-> T2: 치료 일정 장면 실연
-> T3: 부부 하위체계와 부모-자녀 경계
-> T4: 구조적 재배열 개입
-> T5: 다음 주 치료 일정 회의 규칙
```

## Common Intro

```text
아버지: 치료를 빠지면 아이가 뒤처질까 봐 불안합니다. 어렵게 잡은 일정인데, 그날그날 아이 컨디션에 따라 빼면 결국 아무것도 남지 않을 것 같습니다.

어머니: 아이가 차에서 잠들 정도로 지쳐요. 그런데 제가 쉬자고 하면, 제가 아이 치료를 포기한 사람처럼 됩니다.

자녀: 오늘은 집에 있고 싶어요. 차 타고 또 가면 머리가 아파요.

치료사/기관 담당자: 치료 목표를 잡으려면 일정이 어느 정도 유지되어야 합니다. 다만 부모님이 매번 다른 메시지를 주시면 아이도 더 힘들어합니다.

이정후: 일정표만 보지 마세요. 누가 결정하고, 누가 아이를 대신 말하고, 부부가 언제 부모 팀을 잃는지가 핵심입니다.
```

## T1. Initial Definition

| Choice | Player line | Route impact | Immediate response |
| --- | --- | --- | --- |
| A | "치료 일정을 정하기 전에, 부모님 두 분이 아이 앞에서 어떻게 의견을 나누는지부터 보겠습니다." | `parental_alignment +2` | 부부가 불편하지만 구조 초점을 받아들임 |
| B | "치료 효과를 위해 결석하지 않는 원칙을 먼저 세우겠습니다." | `schedule_escalation +2` | 아버지는 안도, 어머니와 아이는 굳음 |
| C | "오늘은 아이가 지쳤다는 신호를 보호하는 데 초점을 두겠습니다." | `mother_child_coalition +1` | 어머니는 안도하지만 아버지가 밀려남 |
| D | "기관 담당자가 권장하는 일정을 우선 기준으로 삼겠습니다." | `professional_takeover +2` | 가족 결정권이 외부 전문가에게 이동 |

## T2. Enactment

### `parental_alignment`

```text
상담자: 지난주 치료 가기 전 10분을 여기서 짧게 해보겠습니다. 두 분은 실제처럼 아이 앞에서 이야기해 주세요.
아버지: 오늘도 빠지면 안 된다고 말했을 겁니다.
어머니: 저는 아이 얼굴을 보고 오늘은 쉬어야 한다고 했을 거예요.
자녀: 둘이 말할 때 저는 그냥 가방을 들고 있었어요. 제가 말하면 더 싸울 것 같아서요.
이정후: 실연이 잘 열렸습니다. 아이가 증상이 아니라 부모 하위체계의 갈등을 들고 서 있는 장면입니다.
```

Good T2 choice:

```text
"아이에게 묻기 전에, 부모님 두 분이 아이가 듣지 않는 자리에서 치료 원칙과 휴식 원칙을 먼저 한 문장씩 정해보겠습니다."
```

### `schedule_escalation`

```text
아버지: 원칙이 있어야 합니다. 빠지면 아이가 더 힘들어지는 건 사실입니다.
어머니: 그런데 그 원칙을 지키는 사람은 결국 저예요. 아이를 달래고, 차에 태우고, 울면 제가 안고 갑니다.
자녀: 아빠가 말하면 저는 더 가야 할 것 같고, 엄마가 울면 제가 나쁜 애 같아요.
이정후: 일정 강화가 가족 구조를 정리하지 못하면, 아이는 부모의 불안을 몸으로 떠안게 됩니다.
```

Repair T2 choice:

```text
"방금 제가 일정을 먼저 세우며 아이와 어머니가 감당하는 부담을 충분히 보지 못했습니다. 일정은 두 분이 같은 팀으로 정할 수 있을 때 다시 다루겠습니다."
```

### `mother_child_coalition`

```text
어머니: 아이가 지쳤다는 말을 들어주셔서 다행입니다.
아버지: 그러면 저는 늘 밀어붙이는 사람으로만 남는 겁니까? 저도 아이가 좋아지길 바라서 그러는 겁니다.
자녀: 엄마가 제 편이면 좋긴 한데, 그러면 아빠가 더 화내요.
이정후: 아이 보호는 중요하지만, 한쪽 부모를 밀어내면 부부 하위체계는 더 약해집니다.
```

Repair T2 choice:

```text
"아이의 지침을 보호하면서도, 아버지를 치료팀 밖으로 밀어내지는 않겠습니다. 두 분이 함께 정할 수 있는 기준을 찾아보겠습니다."
```

### `professional_takeover`

```text
치료사/기관 담당자: 권장 일정은 있지만, 가정에서 매번 싸움이 생기면 지속하기 어렵습니다.
아버지: 그래도 전문가가 정해주면 따르기가 쉽습니다.
어머니: 그러면 제 말은 또 감정적인 말이 되는 것 같아요.
이정후: 전문가 의견은 자료입니다. 가족의 위계를 대신 맡기면 치료실 밖에서 지속되지 않습니다.
```

Repair T2 choice:

```text
"기관 권고는 참고하되, 최종 일정은 부모님 두 분이 아이 상태와 가족 생활을 함께 보고 정하는 구조로 만들겠습니다."
```

## T3. Boundary and Hierarchy

Core scene:

```text
어머니: 저는 아이가 힘든 걸 제일 가까이 봅니다. 그래서 쉬자고 말하는 건데, 그 말이 매번 변명처럼 들립니다.
아버지: 저는 내가 덜 돌보니까 더 엄격하게라도 해야 한다고 생각했습니다. 안 그러면 제 역할이 없는 것 같았습니다.
자녀: 엄마랑 아빠가 서로 다르게 말하면, 저는 누구 말을 들어야 할지 모르겠어요.
이정후: 아버지는 일정표로 가족 안에 들어오고, 어머니는 아이 컨디션으로 가족을 지킵니다. 둘 다 부모 역할이지만 같은 팀으로 묶이지 않았습니다.
```

Key choices:

```text
A. "두 분이 아이 앞에서 결정을 시작하기 전에, 부모 회의 10분을 따로 갖는 구조를 만들어 보겠습니다."
B. "아이가 원하는 대로 당분간 치료를 줄이는 것이 좋겠습니다."
C. "아버지가 치료 출석을 더 엄격히 관리하고, 어머니는 실행을 맡는 방식으로 나누겠습니다."
```

## T4. Core Structural Intervention

Target intervention:

```text
"여기서 짧게 실연해 보겠습니다. 아이는 잠시 듣는 자리에서 빠지고, 부모님 두 분만 '치료를 지키는 기준'과 '쉬어야 하는 기준'을 각각 말한 뒤, 둘이 함께 아이에게 전달할 한 문장으로 합쳐보겠습니다."
```

Expected response:

```text
아버지: 저는 치료를 놓치면 불안합니다. 그래서 기준이 필요합니다.
어머니: 저는 아이가 완전히 지쳐버리면 치료도 의미가 없다고 느낍니다. 휴식 기준도 필요합니다.
자녀: 둘이 같이 말하면 덜 무서워요. 제가 골라야 하는 게 아닌 것 같아요.
```

## T5. Closure

Best closure:

```text
"다음 한 주는 치료 전날 밤 10분 부모 회의를 먼저 하고, 아이에게는 두 분이 합의한 한 문장만 전달해보겠습니다. 출석 기준과 휴식 기준을 둘 다 적고, 싸움이 시작되면 결정은 멈추고 부모 회의로 돌아갑니다."
```

## Endings

| Ending | Condition | Result |
| --- | --- | --- |
| A. Parents Reclaim The Frame | parental alignment maintained | 부모가 치료/휴식 기준을 함께 만들고 아이가 갈등 운반자 자리에서 내려옴 |
| B. Schedule Wins, Child Disappears | schedule route dominant | 출석률은 잠시 올라가지만 아이의 피로와 어머니 방어가 커짐 |
| C. Protective Coalition | mother-child route dominant | 아이는 보호받지만 아버지는 배제감을 느끼고 성과 압박이 강화 |
| D. Outsourced Authority | professional route dominant | 기관 권고가 가족 결정을 대신하며 가정 내 실행력은 약함 |

## Dialogue Expansion Notes

- 아버지는 냉정한 사람이 아니라 역할 불안을 일정표로 표현한다.
- 어머니는 치료를 포기하는 사람이 아니라 아이의 몸 신호를 혼자 들고 있다.
- 아이는 선택권이 없는 상태를 짧고 단순한 말로 표현해야 한다.
- 이정후는 "누가 위에 있어야 한다"보다 "누가 누구와 먼저 말해야 하는가"를 짚는다.
