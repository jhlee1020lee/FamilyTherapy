# FT-005 Structural Stepfamily Major Branching Scenario

## Goal

FT-005 is a structural family therapy episode about a remarried family eight months after remarriage. The presenting problem is a teenager "disrespecting" the stepfather, but the clinical focus is loyalty conflict, premature authority, and a mother trapped as mediator.

The player should learn that in stepfamilies, authority cannot be demanded faster than relationship and boundary formation can support.

## Cast

Names are not locked yet. Use role labels until final names are assigned.

| Character | Role | Surface statement | Hidden pressure |
| --- | --- | --- | --- |
| 어머니 | biological mother | "둘 다 조금만 양보하면 되는데 왜 이렇게 힘든지 모르겠어요." | spouse and child 사이에서 계속 중재하며 소진 |
| 새아버지 | stepfather | "가족이면 최소한 인사는 해야 합니다." | 가족으로 인정받지 못하는 수치심과 조급함 |
| 청소년 자녀 | adolescent child | "갑자기 가족이라고 하면 제가 네 해야 하나요?" | 친부에 대한 충성심과 엄마를 빼앗긴 느낌 |
| 친부 | off-screen figure | 화면에는 등장하지 않음 | 말해지지 않는 충성심 갈등의 기준점 |
| 이정후 | structural supervisor | 경계, 위계, 합류 속도를 짚음 | 단호하지만 새가족의 속도를 존중 |

## Route Model

| Route ID | Name | Entry condition | Session tone |
| --- | --- | --- | --- |
| `boundary_staging` | 단계적 경계 형성 루트 | 새아버지 권한보다 관계 형성 순서를 다룸 | 가족이 각자의 자리와 속도를 구분 |
| `authority_push` | 권한 밀어붙이기 루트 | 새아버지에게 즉시 부모 권한을 부여 | 청소년 반발과 어머니 중재 과부하 증가 |
| `child_loyalty_only` | 자녀 충성심만 보기 루트 | 청소년 감정만 보호하고 부부 하위체계를 놓침 | 새아버지가 배제되고 부부 갈등 심화 |
| `mother_mediation` | 어머니 중재 유지 루트 | 어머니에게 계속 조정 역할을 맡김 | 기존 삼각구조가 강화 |

## Five-Turn Flow

```text
Common intro
-> T1: 이 갈등을 무례함, 충성심, 구조 문제 중 무엇으로 볼 것인가
-> T2: 인사/식사 장면 실연
-> T3: 어머니의 중재 과부하와 새아버지의 조급함
-> T4: 단계적 역할 재구조화
-> T5: 다음 주 가족 규칙과 관계 과제
```

## Common Intro

```text
새아버지: 같이 산 지 8개월이면 가족이라고 생각했습니다. 그런데 아이는 제가 인사해도 못 들은 척하고, 제가 말하면 바로 방으로 들어갑니다.

청소년 자녀: 갑자기 가족이라고 하면 제가 네 하고 받아들여야 해요? 엄마가 결혼한 거지, 제가 새 아빠를 고른 건 아니잖아요.

어머니: 저는 두 사람 사이에서 계속 설명하고 달래고 사과합니다. 그런데 둘 다 저한테 자기 편을 들어달라는 것 같아서 지칩니다.

이정후: 지금은 누가 예의를 지켜야 하는지보다, 새 가족 구조에서 관계와 권한이 어떤 순서로 만들어져야 하는지를 봐야 합니다.
```

## T1. Initial Definition

| Choice | Player line | Route impact | Immediate response |
| --- | --- | --- | --- |
| A | "새아버지의 권한을 바로 정하기보다, 세 사람이 각자 지금 가족 안에서 어떤 자리에 있는지부터 보겠습니다." | `boundary_staging +2` | 청소년이 덜 방어하고 새아버지도 듣기 시작 |
| B | "같이 사는 어른인 만큼, 새아버지의 기본적인 지도 권한은 인정되어야 합니다." | `authority_push +2` | 새아버지는 안도, 자녀는 닫힘 |
| C | "먼저 자녀가 엄마를 빼앗겼다고 느끼는 마음을 충분히 보호해야겠습니다." | `child_loyalty_only +2` | 자녀는 안도하지만 새아버지가 배제감 느낌 |
| D | "어머니가 두 사람 사이에서 오해를 풀어주는 역할을 조금 더 분명히 해보겠습니다." | `mother_mediation +2` | 어머니가 더 지치고 삼각구조 유지 |

## T2. Enactment

### `boundary_staging`

```text
상담자: 어제 저녁 식사 장면을 짧게 해보겠습니다. 누가 먼저 말하고, 누가 누구를 보며, 어머니는 어디에 계셨나요?
새아버지: 저는 식탁에서 오늘 학교 어땠냐고 물었습니다.
청소년 자녀: 저는 그냥 "몰라요"라고 했고, 엄마가 옆에서 대답 좀 하라고 했습니다.
어머니: 제가 안 끼면 둘이 바로 싸울 것 같아서 중간에서 말하게 됩니다.
이정후: 지금 실연에서는 어머니가 통역자이자 완충재가 되어 있습니다. 이 자리를 줄이지 않으면 새 관계가 직접 만들어지지 않습니다.
```

Good T2 choice:

```text
"어머니가 대신 설명하지 않고, 새아버지는 지시가 아니라 짧은 관심 질문 하나만 하고, 자녀는 대답 길이를 스스로 정하는 장면으로 다시 해보겠습니다."
```

### `authority_push`

```text
청소년 자녀: 또 제가 예의 없는 애가 되는 거네요. 그럼 여기서도 혼나는 거랑 뭐가 달라요?
새아버지: 저는 혼내려는 게 아니라 기본을 말하는 겁니다. 가족이면 서로 지켜야 하는 게 있잖아요.
어머니: 둘 다 맞는 말 같아서 더 모르겠습니다.
이정후: 권한을 너무 빨리 세우면 충성심 갈등은 더 단단해집니다. 구조는 힘으로 세우는 것이 아니라 순서로 만듭니다.
```

Repair T2 choice:

```text
"제가 권한을 너무 빨리 말했습니다. 오늘은 새아버지가 어떤 권한을 가져야 하는지보다, 어떤 관계 행동부터 쌓을 수 있는지 보겠습니다."
```

### `child_loyalty_only`

```text
청소년 자녀: 제 마음을 먼저 물어봐 주는 건 좋아요. 그런데 그러면 또 엄마가 그 사람한테 미안하다고 하겠죠.
새아버지: 저는 여기서도 바깥사람처럼 느껴집니다.
어머니: 아이를 보호해야 하는 건 맞지만, 남편이 계속 밀려나는 것도 힘듭니다.
이정후: 자녀의 충성심을 존중하면서도 새 부부 하위체계를 사라지게 만들면 안 됩니다.
```

Repair T2 choice:

```text
"자녀의 속도를 지키되, 새아버지를 가족 밖으로 밀어내지는 않겠습니다. 오늘은 '부모 권한'과 '관계 쌓기'를 분리해서 보겠습니다."
```

### `mother_mediation`

```text
어머니: 제가 더 잘 설명하면 될 줄 알았습니다. 그런데 이제는 둘 다 저한테만 말합니다.
새아버지: 아이한테 직접 말하면 싸움이 되니까, 결국 아내에게 부탁하게 됩니다.
청소년 자녀: 엄마가 중간에 있으면 덜 무섭긴 한데, 그러면 엄마가 더 힘들어져요.
이정후: 중재가 갈등을 줄이는 듯하지만, 새 관계가 직접 생기는 것을 막고 있습니다.
```

Repair T2 choice:

```text
"어머니가 통역하지 않는 30초 대화를 실험해보겠습니다. 짧고 안전한 주제로 두 사람이 직접 말하고, 어머니는 끼어들지 않고 관찰합니다."
```

## T3. Loyalty Conflict And Couple Subsystem

Core scene:

```text
청소년 자녀: 새아버지를 받아들이면 아빠를 배신하는 것 같아요. 엄마는 그 말을 들으면 불편해할까 봐 안 했습니다.
어머니: 저는 네가 그런 생각을 하는 줄 알면서도 피했습니다. 그 말을 들으면 제가 나쁜 엄마가 된 것 같아서요.
새아버지: 저는 친아버지를 지우려는 게 아닙니다. 그런데 매번 비교 대상처럼 느껴지는 건 힘듭니다.
이정후: 충성심 갈등과 새 부부 하위체계가 동시에 보입니다. 둘 중 하나만 보호하면 구조는 다시 흔들립니다.
```

Key choices:

```text
A. "친아버지를 지우지 않아도 새아버지와의 관계를 천천히 만들 수 있다는 기준을 세워보겠습니다."
B. "이제는 새 가족이 되었으니 과거 가족 이야기는 줄이는 것이 좋겠습니다."
C. "어머니가 자녀에게 새아버지를 받아들이라고 더 분명히 말해야 합니다."
```

## T4. Core Structural Intervention

Target intervention:

```text
"오늘은 새아버지의 훈육 권한을 바로 세우지 않겠습니다. 대신 한 주 동안 새아버지는 지시가 아닌 관심 질문 하나, 자녀는 대답 여부와 길이를 정할 권리, 어머니는 대신 답하지 않기를 실험해보겠습니다."
```

Expected response:

```text
새아버지: 제가 당장 아버지처럼 인정받으려 했던 것 같습니다. 질문 하나부터 해보겠습니다.
청소년 자녀: 대답을 길게 안 해도 된다면 덜 부담스러울 것 같아요.
어머니: 제가 대신 설명하지 않는 게 제일 어려울 것 같습니다. 그래도 해봐야 할 것 같아요.
```

## T5. Closure

Best closure:

```text
"다음 주 과제는 세 가지입니다. 새아버지는 훈육 대신 하루 한 번 짧은 관심 질문, 자녀는 대답 길이를 스스로 정하기, 어머니는 두 사람 대화를 대신 번역하지 않고 끝난 뒤 자기 감정을 말하기입니다."
```

## Endings

| Ending | Condition | Result |
| --- | --- | --- |
| A. Staged Stepfamily Boundary | boundary route maintained | 관계 형성, 부부 하위체계, 자녀 충성심이 분리되어 다뤄짐 |
| B. Authority Before Attachment | authority route dominant | 새아버지 권한은 선포되지만 자녀 반발과 어머니 중재가 커짐 |
| C. Child Protected, Couple Split | loyalty-only route dominant | 자녀 감정은 보호되지만 새아버지가 가족 밖으로 밀림 |
| D. Mother As Permanent Bridge | mediation route dominant | 어머니가 계속 중재자가 되어 구조 변화가 일어나지 않음 |

## Dialogue Expansion Notes

- 청소년 자녀의 말은 날카롭지만 핵심은 충성심과 상실감이다.
- 새아버지는 권위적인 악역이 아니라 인정받지 못하는 조급함이 있다.
- 어머니는 중재자로 기능하지만, 그 역할이 가족 구조를 고착한다.
- 이정후는 "단계", "자리", "누가 누구에게 직접 말하는가"를 반복해서 짚는다.
