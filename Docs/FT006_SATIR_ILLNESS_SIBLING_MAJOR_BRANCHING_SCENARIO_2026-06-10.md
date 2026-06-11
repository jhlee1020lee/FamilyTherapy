# FT-006 Satir Illness Sibling Major Branching Scenario

## Goal

FT-006 focuses on a family organized around a child with a long-term illness. The presenting issue is the younger sibling's withdrawal, but the Satir/experiential focus is the sibling's "괜찮아요" posture, hidden loneliness, guilt, and the family's frozen emotional hierarchy around illness.

The player should help the family see the well sibling without blaming the sick child or shaming the parents.

## Cast

Names are not locked yet. Use role labels until final names are assigned.

| Character | Role | Surface statement | Hidden pressure |
| --- | --- | --- | --- |
| 둘째 | well sibling | "저는 괜찮아요. 언니/형이 더 힘들잖아요." | 외로움과 죄책감을 성숙함으로 감춤 |
| 어머니 | mother | "얘가 이해해줘서 고맙지만 걱정됩니다." | 아픈 자녀와 둘째 사이에서 죄책감 |
| 아버지 | father | "지금은 치료가 우선입니다." | 감정을 다루면 가족이 무너질까 봐 기능 유지에 집중 |
| 조부모/도움 인물 | support relative | "제가 집을 봐주긴 하지만 아이 마음은 잘 모르겠습니다." | 돌봄은 돕지만 정서 대화는 피함 |
| 김연주 | Satir supervisor | 빙산, 가족조각, 정서 확인 | 따뜻하고 천천히 숨은 감정을 만남 |

## Route Model

| Route ID | Name | Entry condition | Session tone |
| --- | --- | --- | --- |
| `sibling_visible` | 둘째 가시화 루트 | 괜찮다는 말 아래의 외로움을 안전하게 들음 | 둘째가 죄책감 없이 자기 욕구를 말함 |
| `illness_totalizing` | 질병 중심 루트 | 아픈 자녀 치료만 우선시 | 둘째가 다시 사라짐 |
| `parent_guilt_flood` | 부모 죄책감 범람 루트 | 부모 죄책감을 너무 직접 찌름 | 부모가 방어하거나 무너짐 |
| `cheerful_mask` | 성숙함 칭찬 루트 | 둘째의 어른스러움을 강화 | 둘째가 더 착한 아이 역할에 갇힘 |

## Five-Turn Flow

```text
Common intro
-> T1: 둘째의 괜찮다는 말을 어떻게 들을 것인가
-> T2: 가족조각으로 자리 보기
-> T3: 죄책감과 외로움의 빙산
-> T4: 일치형 감정 표현
-> T5: 둘째 시간과 가족 점검 의식
```

## Common Intro

```text
둘째: 저는 괜찮아요. 병원에 있는 언니가 더 힘들잖아요. 제가 힘들다고 말하면 엄마 아빠가 더 힘들어질 것 같아요.

어머니: 이 아이가 이렇게 말해줘서 고맙지만, 가끔 너무 어른처럼 굴어서 무섭습니다. 학교에서도 말수가 줄었다고 들었습니다.

아버지: 지금은 첫째 치료가 우선입니다. 둘째도 상황을 이해한다고 생각했는데, 요즘은 방에 오래 있습니다.

조부모/도움 인물: 제가 집안일은 봐주지만, 아이가 무슨 생각을 하는지는 잘 모르겠습니다. 물어보면 괜찮다고만 합니다.

김연주: 괜찮다는 말이 정말 괜찮음인지, 가족을 지키기 위한 말인지 천천히 보겠습니다.
```

## T1. Initial Definition

| Choice | Player line | Route impact | Immediate response |
| --- | --- | --- | --- |
| A | "괜찮다고 말하는 마음 안에 고마움 말고도 외로움이나 서운함이 같이 있는지 천천히 들어보고 싶습니다." | `sibling_visible +2` | 둘째가 작게 자기 이야기를 시작 |
| B | "지금은 첫째 치료가 가장 중요하니, 둘째가 이해할 수 있도록 설명을 더 해보겠습니다." | `illness_totalizing +2` | 둘째가 다시 착한 아이 역할로 들어감 |
| C | "부모님이 둘째를 너무 방치하신 것 같습니다." | `parent_guilt_flood +2` | 부모가 방어하거나 울며 대화가 흐려짐 |
| D | "둘째가 이렇게 의젓하게 버텨준 것은 큰 강점입니다." | `cheerful_mask +2` | 둘째가 칭찬받으며 더 감정을 숨김 |

## T2. Family Sculpture

### `sibling_visible`

```text
상담자: 말로 설명하기 어렵다면, 가족이 병원과 집 사이에서 어떻게 서 있는지 자리로 표현해볼까요?
둘째: 엄마 아빠는 병원 쪽에 있고, 저는 집에 혼자 있는 것 같아요. 그런데 제가 그쪽으로 가면 방해될 것 같아요.
어머니: 네가 그렇게 느끼는 줄 몰랐어. 저는 네가 잘 버틴다고만 생각했습니다.
김연주: 가족조각이 감정을 말보다 먼저 보여주고 있습니다. 이 장면에서 누구를 탓하지 말고 자리를 보게 하세요.
```

Good T2 choice:

```text
"둘째가 혼자 서 있는 자리를 부모님이 잠시 바라보고, 그 자리에 어떤 감정이 있을지 한 단어씩 말해보겠습니다."
```

### `illness_totalizing`

```text
둘째: 저는 이해해요. 언니가 아프니까 어쩔 수 없어요.
어머니: 이렇게 또 말하니까 고맙긴 한데, 마음이 더 불편합니다.
아버지: 치료가 우선이라는 말이 틀린 건 아니지만, 둘째 얘기는 또 뒤로 간 느낌입니다.
김연주: 사실은 맞는 말이지만 치료적 초점은 사라졌습니다. 둘째가 다시 가족 주변부로 물러났습니다.
```

Repair T2 choice:

```text
"치료가 중요하다는 사실은 유지하되, 그 사실 때문에 둘째 마음을 묻지 못했던 장면을 지금은 따로 보겠습니다."
```

### `parent_guilt_flood`

```text
어머니: 방치했다는 말을 들으니 제가 정말 나쁜 엄마였던 것 같습니다.
아버지: 저희도 버티느라 그랬습니다. 일부러 둘째를 모른 척한 건 아닙니다.
둘째: 제가 말해서 엄마 아빠가 혼나는 것 같아요. 그럼 말 안 할래요.
김연주: 죄책감을 너무 세게 건드리면 둘째는 다시 부모를 보호합니다.
```

Repair T2 choice:

```text
"제가 부모님을 탓하는 방식으로 들리게 말했습니다. 오늘은 누구 잘못을 찾기보다, 가족이 너무 힘든 상황에서 둘째 마음이 어디로 밀렸는지 보겠습니다."
```

### `cheerful_mask`

```text
둘째: 제가 잘하고 있는 거면 계속 괜찮아야 하는 거죠?
어머니: 그 말이 더 아프네요. 얘가 힘들어도 잘하는 아이로만 있어야 했던 것 같아요.
김연주: 성숙함을 칭찬할 때도 그 성숙함이 감정을 숨기는 역할을 하는지 확인해야 합니다.
```

Repair T2 choice:

```text
"잘 버틴 점은 인정하지만, 잘 버틴 아이도 외롭거나 화날 수 있습니다. 오늘은 그 둘을 같이 놓고 보겠습니다."
```

## T3. Iceberg

Core scene:

```text
둘째: 언니가 아픈데 제가 서운하다고 말하면 나쁜 사람 같아요. 그래서 그냥 학교에서도 조용히 있습니다.
어머니: 네가 괜찮다고 할 때마다 저는 안심하고 싶어서 믿어버렸던 것 같습니다.
아버지: 저는 가족이 무너지지 않게 하려고 치료 이야기만 했습니다. 그런데 그게 너한테는 아무도 묻지 않는 것처럼 들렸겠네요.
김연주: 표면의 의젓함 아래에 외로움, 죄책감, 부모를 보호하려는 마음이 같이 있습니다.
```

Key choices:

```text
A. "둘째가 언니를 걱정하는 마음과 자기 외로움을 동시에 가질 수 있다는 걸 가족이 확인해보겠습니다."
B. "부모님은 앞으로 둘째에게 더 미안하다고 자주 말해야 합니다."
C. "첫째 치료 일정이 줄어들 수 있는지부터 확인해야겠습니다."
```

## T4. Core Satir Intervention

Target intervention:

```text
"둘째는 '나는 언니가 걱정되지만, 집에 혼자 있을 때 외롭다'처럼 두 마음을 같이 말해보고, 부모님은 바로 사과하기보다 들은 마음을 되돌려 주세요."
```

Expected response:

```text
둘째: 저는 언니가 걱정돼요. 그런데 집에 오면 아무도 제 하루를 묻지 않아서 외로워요.
어머니: 너는 언니를 미워하는 게 아니라, 네 자리도 봐달라는 말이구나.
아버지: 치료 이야기를 멈추면 무너질까 봐 무서웠는데, 네 하루를 묻는 게 치료를 포기하는 건 아니네요.
```

## T5. Closure

Best closure:

```text
"다음 주는 병원 일정과 별개로 둘째에게만 묻는 10분을 정하겠습니다. 그 시간에는 첫째 치료 보고가 아니라 둘째의 하루, 감정, 필요한 도움만 듣습니다. 실패하면 다시 정하는 규칙까지 같이 적겠습니다."
```

## Endings

| Ending | Condition | Result |
| --- | --- | --- |
| A. The Well Child Has A Place | sibling visible route maintained | 둘째가 죄책감 없이 외로움을 말하고 부모가 짧은 점검 의식을 합의 |
| B. Illness Owns The Family | illness route dominant | 첫째 치료 중심 구조가 유지되어 둘째는 더 조용해짐 |
| C. Guilt Takes The Room | guilt route dominant | 부모 죄책감이 장면을 덮고 둘째가 다시 부모를 보호 |
| D. Good Child Trap | cheerful route dominant | 둘째가 성숙함을 칭찬받아 감정을 더 숨김 |

## Dialogue Expansion Notes

- 첫째를 악역화하거나 부담으로 쓰지 않는다. 화면에는 없지만 가족의 사랑과 불안 중심이다.
- 둘째는 긴 설명보다 "괜찮다" 뒤에 작은 균열이 나는 방식으로 말한다.
- 부모의 죄책감은 다루되, 죄책감이 대화를 독점하지 않게 한다.
- 김연주는 감정 단어를 부드럽게 제안하고, 두 마음이 공존할 수 있음을 강조한다.
