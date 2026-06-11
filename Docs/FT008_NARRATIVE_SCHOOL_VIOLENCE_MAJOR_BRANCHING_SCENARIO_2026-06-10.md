# FT-008 Narrative School Violence Major Branching Scenario

## Goal

FT-008 focuses on a family after a school violence incident. The teen wants to transfer, the parents insist on enduring, and the family avoids speaking about the event at meals. The narrative therapy focus is externalizing the problem, noticing dominant stories such as "버텨야 한다" and "도망치면 진다," and helping the teen recover identity outside the incident.

The player should not force disclosure of the event. The task is to separate the teen from the problem-saturated story and help the family choose language that does not trap the teen.

## Cast

Names are not locked yet. Use role labels until final names are assigned.

| Character | Role | Surface statement | Hidden pressure |
| --- | --- | --- | --- |
| 청소년 | bullied teen | "학교 이름만 들어도 속이 안 좋아요." | 사건이 자기 정체성을 덮어버림 |
| 어머니 | mother | "다시 꺼내면 아이가 더 힘들까 봐 말하지 않았어요." | 침묵이 보호라고 믿지만 아이와 멀어짐 |
| 아버지 | father | "전학하면 도망친 것처럼 남을까 봐 걱정됩니다." | 버티기 담론으로 무력감을 가림 |
| 학교 담당자 | school representative | "절차는 진행 중입니다." | 가족의 감정보다 행정 언어가 앞섬 |
| 박병호 | narrative supervisor | 외재화, 지배담론, 재저작을 짚음 | 사색적이고 언어에 민감 |

## Route Model

| Route ID | Name | Entry condition | Session tone |
| --- | --- | --- | --- |
| `externalized_harm` | 문제 외재화 루트 | 사건/침묵/버티기를 사람 밖으로 꺼냄 | 가족이 문제와 아이를 분리하기 시작 |
| `silence_protection` | 침묵 보호 루트 | 말을 꺼내지 않는 것을 계속 보호로 둠 | 표면 안정은 유지되나 아이 고립 심화 |
| `endurance_story` | 버티기 담론 루트 | 전학/회피를 패배로 규정 | 청소년이 자기 욕구를 숨김 |
| `procedure_closure` | 절차 종결 루트 | 학교 절차와 사실 확인만 우선 | 가족 정체성 이야기는 미해결 |

## Five-Turn Flow

```text
Common intro
-> T1: 사건을 어떻게 이름 붙일 것인가
-> T2: 침묵이 가족에게 하는 일 보기
-> T3: 버티기 이야기와 도망 이야기 해체
-> T4: 대안 이야기와 독특한 결과 찾기
-> T5: 다음 주 문제 약화 관찰 과제
```

## Common Intro

```text
청소년: 학교 이름만 들어도 속이 안 좋아요. 그런데 집에서도 그 얘기를 하면 분위기가 이상해져서, 그냥 아무 말 안 합니다.

어머니: 다시 꺼내면 아이가 더 힘들까 봐 말하지 않았습니다. 밥 먹을 때라도 평소처럼 지내게 하고 싶었습니다.

아버지: 전학하면 도망친 것처럼 남을까 봐 걱정됩니다. 아이가 나중에 후회할까 봐 쉽게 결정할 수 없습니다.

학교 담당자: 관련 절차는 진행 중입니다. 다만 가족이 어떤 지원을 원하는지는 아직 명확히 확인되지 않았습니다.

박병호: 사건이 아이를 설명하는 이름이 되어버렸는지 보세요. 먼저 문제를 사람 밖으로 꺼내야 가족이 다른 이야기를 시작할 수 있습니다.
```

## T1. Initial Definition

| Choice | Player line | Route impact | Immediate response |
| --- | --- | --- | --- |
| A | "오늘은 청소년이 문제가 아니라, 사건 이후 가족 안에 들어온 '침묵'과 '버텨야 한다는 말'이 어떤 영향을 주는지 보겠습니다." | `externalized_harm +2` | 청소년이 자기 탓이 아니라는 느낌을 받음 |
| B | "힘든 이야기는 건드리지 않고, 가족이 평소처럼 지낼 수 있는 방법을 먼저 찾겠습니다." | `silence_protection +2` | 부모는 안도, 청소년은 더 고립 |
| C | "전학은 도망처럼 보일 수 있으니, 버티는 힘을 키우는 방향을 보겠습니다." | `endurance_story +2` | 아버지는 동의, 청소년은 닫힘 |
| D | "학교 절차와 사실관계를 먼저 정리해 지원 방향을 결정하겠습니다." | `procedure_closure +2` | 안전한 듯하지만 가족 이야기는 사라짐 |

## T2. Route Scenes

### `externalized_harm`

```text
청소년: '침묵'이라고 하니까 조금 이상한데, 맞는 것 같아요. 그게 있으면 밥 먹을 때 다들 조용해지고, 저는 더 혼자인 것 같아요.
어머니: 저는 그 침묵이 아이를 지켜준다고 생각했습니다. 그런데 아이를 혼자 두는 침묵일 수도 있겠네요.
아버지: 버텨야 한다는 말도 제가 자주 했습니다. 그 말이 아이한테는 선택지가 없다는 뜻으로 들렸을 것 같습니다.
박병호: 문제를 밖으로 꺼내자 가족이 처음으로 그것의 영향을 관찰하고 있습니다.
```

Good T2 choice:

```text
"그 침묵이 식탁에 들어오면 각자 무엇을 하게 되는지, 그리고 그 침묵이 약했던 순간이 있었는지 찾아보겠습니다."
```

### `silence_protection`

```text
청소년: 말하지 않으면 덜 힘든 것 같기도 한데, 아무도 모르는 사람이 되는 것 같아요.
어머니: 저는 보호하려고 했는데, 아이가 혼자 견디게 한 걸 수도 있겠네요.
박병호: 침묵은 때로 쉼이지만, 계속되면 문제의 편이 됩니다.
```

Repair T2 choice:

```text
"힘든 내용을 억지로 말하지는 않겠습니다. 대신 말하지 않는 방식이 청소년을 보호하는지, 더 혼자 두는지부터 같이 보겠습니다."
```

### `endurance_story`

```text
청소년: 또 버티라는 말이네요. 저는 이미 계속 버텼는데, 그게 부족했던 건가요?
아버지: 그런 뜻은 아니었습니다. 다만 나중에 후회할까 봐 걱정했습니다.
박병호: 버티기 이야기가 아버지에게는 걱정의 언어지만, 아이에게는 패배를 금지하는 명령처럼 들립니다.
```

Repair T2 choice:

```text
"버티자는 말이 걱정에서 나온 것은 알겠습니다. 동시에 그 말이 청소년에게 어떤 이야기를 강요했는지 살펴보겠습니다."
```

### `procedure_closure`

```text
학교 담당자: 절차상 필요한 확인은 이어갈 수 있습니다.
청소년: 절차 얘기를 들으면 제가 다시 사건 파일이 된 느낌이에요.
어머니: 필요한 건 알지만, 아이가 더 작아지는 것 같습니다.
박병호: 절차는 안전을 위한 도구입니다. 하지만 이야기 전체를 절차가 가져가면 아이의 목소리가 사라집니다.
```

Repair T2 choice:

```text
"절차는 유지하되, 절차 밖에서 청소년이 자기 경험을 어떤 이름으로 부르고 싶은지 같이 듣겠습니다."
```

## T3. Deconstructing Dominant Stories

Core scene:

```text
청소년: 저는 이미 도망친 사람처럼 느껴져요. 학교를 생각하면 몸이 먼저 피하는데, 그러면 제가 약한 사람 같아요.
어머니: 저는 아이가 더 다칠까 봐 말을 줄였는데, 그게 아이를 약한 사람 이야기 안에 혼자 둔 것 같습니다.
아버지: 전학하면 진다는 말도 제가 만든 이야기일 수 있겠네요. 사실은 제가 아무것도 못 해준 것 같아서 무서웠습니다.
박병호: 이제 지배담론이 보입니다. '버텨야 이긴다'는 이야기가 누구를 돕고 누구를 고립시키는지 물어야 합니다.
```

Key choices:

```text
A. "버티기 이야기가 가족을 어떻게 도왔고, 동시에 청소년에게 어떤 비용을 만들었는지 나눠보겠습니다."
B. "전학 여부를 오늘 결론 내면 가족 불안이 줄어들 것 같습니다."
C. "사건을 자세히 말해야 다음 이야기를 만들 수 있습니다."
```

## T4. Core Narrative Intervention

Target intervention:

```text
"이 문제에 임시 이름을 붙여보겠습니다. 예를 들어 '학교 이름만 나오면 몸을 얼게 하는 것'이라고 부를 수 있습니다. 그 문제가 조금 약했던 순간, 청소년이 자기답게 행동했던 순간이 있었나요?"
```

Expected response:

```text
청소년: 지난주에 친구 한 명한테는 제가 먼저 메시지를 보냈어요. 학교 얘기는 안 했지만, 그때는 제가 완전히 숨은 사람은 아닌 것 같았어요.
어머니: 그건 몰랐습니다. 아이가 아직 연결을 만들고 있었네요.
아버지: 버티는 것만 용기라고 생각했는데, 도움을 고르는 것도 용기일 수 있겠습니다.
```

## T5. Closure

Best closure:

```text
"다음 주에는 사건을 자세히 말하는 숙제가 아니라, 그 문제가 조금 약했던 순간을 기록해보겠습니다. 가족은 식탁에서 침묵이 들어왔을 때 '지금 침묵이 우리를 보호하나, 혼자 두나'를 한 번만 확인합니다."
```

## Endings

| Ending | Condition | Result |
| --- | --- | --- |
| A. The Problem Gets A Name | externalized route maintained | 청소년이 문제와 자신을 분리하고 가족이 침묵의 영향을 관찰 |
| B. Protected Into Isolation | silence route dominant | 침묵이 유지되어 표면은 조용하지만 청소년 고립 심화 |
| C. Endure Or Lose | endurance route dominant | 전학/도움 요청이 패배 이야기 안에 갇힘 |
| D. Case File Without Voice | procedure route dominant | 절차는 정리되지만 청소년 정체성 회복은 미뤄짐 |

## Dialogue Expansion Notes

- 사건 세부 묘사는 선정적으로 쓰지 않는다.
- 청소년에게 폭로를 강요하지 않는다.
- 부모의 침묵과 버티기 언어는 보호 의도와 비용을 함께 가진다.
- 박병호는 언어를 천천히 바꿔 가족이 문제와 사람을 분리하게 한다.
