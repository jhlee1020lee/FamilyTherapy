# FT-004 Satir Major Branching Scenario

## Goal

FT-004 focuses on an immigrant-background multicultural family facing childcare application failures and language barriers. The Satir/experiential task is to hear the protective smile, "괜찮아요," and short answers as survival communication rather than true agreement.

The player should help the family move from blame/placating to more congruent communication while also respecting the real institutional barrier.

## Cast

Names are not locked yet. Use role labels until final names are assigned.

| Character | Role | Surface statement | Hidden pressure |
| --- | --- | --- | --- |
| 이민 배경 보호자 | caregiver with limited Korean | "괜찮아요. 제가 더 배우면 됩니다." | 수치심, 언어 장벽, 도움을 요청하면 더 비난받을 것 같은 두려움 |
| 배우자 | spouse | "서류를 제대로 못 챙기니까 반복됩니다." | 생계와 돌봄 불안이 비난으로 나옴 |
| 자녀 | young child | "엄마가 전화 끝나면 한숨을 쉬어요." | 부모 긴장을 관찰하지만 설명받지 못함 |
| 보육기관 담당자 | institution representative | "서류가 맞아야 접수가 됩니다." | 제도 언어로 가족의 정서 압박을 키움 |
| 김연주 | Satir supervisor | 감정, 빙산, 의사소통 유형을 짚음 | 따뜻하지만 표면 합의를 그대로 믿지 않음 |

## Route Model

| Route ID | Name | Entry condition | Session tone |
| --- | --- | --- | --- |
| `congruent_voice` | 일치형 목소리 루트 | 괜찮다는 말 아래 감정과 욕구를 안전하게 탐색 | 보호자가 자기 말로 필요를 표현 |
| `placating_mask` | 회유 유지 루트 | 보호자의 "괜찮다"를 그대로 받아들임 | 갈등은 낮지만 욕구는 계속 사라짐 |
| `blame_loop` | 비난 루트 | 배우자 책임 추궁 또는 보호자 무능 프레임 | 부부 비난/회유가 강화 |
| `institution_only` | 제도 절차 루트 | 서류/통번역 정보만 정리 | 실용 정보는 생기지만 정서와 부부 소통은 미해결 |

## Five-Turn Flow

```text
Common intro
-> T1: 괜찮다는 말을 어떻게 들을 것인가
-> T2: 비난-회유 장면 보기
-> T3: 빙산 탐색
-> T4: 일치형 표현 연습
-> T5: 도움 요청과 제도 접근 계획
```

## Common Intro

```text
이민 배경 보호자: 괜찮아요. 제가 한국어를 더 배워야죠. 신청은 다시 하면 됩니다.

배우자: 매번 괜찮다고만 하니까 똑같은 일이 반복됩니다. 보육 신청이 안 되면 일을 조정해야 하고, 집도 계속 불안정해집니다.

자녀: 엄마가 전화하고 나면 웃는데, 나중에 혼자 한숨을 쉬어요.

보육기관 담당자: 서류가 누락되면 접수가 어렵습니다. 안내문을 드렸지만 확인이 충분하지 않았던 것 같습니다.

김연주: 웃는 얼굴과 괜찮다는 말만 듣지 마세요. 그 아래에 수치심, 두려움, 도움을 바라는 마음이 어떻게 숨어 있는지 보겠습니다.
```

## T1. Initial Definition

| Choice | Player line | Route impact | Immediate response |
| --- | --- | --- | --- |
| A | "괜찮다고 말씀하셨지만, 그 말을 하실 때 속으로는 어떤 마음이 제일 컸는지 천천히 듣고 싶습니다." | `congruent_voice +2` | 보호자가 처음으로 망설이며 감정을 말함 |
| B | "괜찮다고 하셨으니, 다음 신청 절차를 바로 정리하겠습니다." | `placating_mask +2`, `institution_only +1` | 표면은 조용하지만 보호자 욕구가 사라짐 |
| C | "배우자분은 왜 서류 준비를 더 적극적으로 돕지 않으셨나요?" | `blame_loop +2` | 배우자가 방어하고 보호자가 더 회유 |
| D | "통번역 지원과 보육 신청 서류를 먼저 체크리스트로 만들겠습니다." | `institution_only +2` | 실용성은 있으나 감정은 뒤로 밀림 |

## T2. Route Scenes

### `congruent_voice`

```text
이민 배경 보호자: 사실 괜찮지 않습니다. 전화할 때 말을 못 알아듣는 순간 머리가 하얘지고, 집에 와서는 제가 또 가족을 힘들게 했다는 생각이 듭니다.
배우자: 저는 그걸 몰랐습니다. 그냥 조용히 넘어가니까 답답해서 더 세게 말했습니다.
자녀: 엄마가 조용하면 아빠 목소리가 더 커져요.
김연주: 가족이 처음으로 표면 말 아래의 감정을 듣고 있습니다. 이 흐름을 서류 해결로 너무 빨리 닫지 마세요.
```

Good T2 choice:

```text
"방금 말씀을 배우자분이 들을 수 있게, '나는 전화할 때 ~해서 ~가 필요하다'는 문장으로 다시 말해볼까요?"
```

### `placating_mask`

```text
이민 배경 보호자: 네, 체크리스트 있으면 할 수 있습니다. 제가 더 조심하면 됩니다.
배우자: 이번에는 정말 빠뜨리지 말아야 합니다. 또 안 되면 더는 방법이 없습니다.
김연주: 표면상 협조는 생겼지만, 보호자는 다시 혼자 책임지는 자리로 돌아가고 있습니다.
```

Repair T2 choice:

```text
"제가 절차를 너무 빨리 정리하면서, 이 일을 혼자 감당하는 부담을 충분히 듣지 못했습니다. 신청을 다시 하는 것과 동시에 누가 옆에서 같이 확인할지도 정해야겠습니다."
```

### `blame_loop`

```text
배우자: 저도 일하고 집안일도 합니다. 제가 안 도운 사람처럼 말하면 억울합니다.
이민 배경 보호자: 아니에요, 제가 잘못한 거예요. 제가 제대로 못해서 그래요.
자녀: 둘이 이렇게 말하면 저는 방에 들어가요.
김연주: 비난이 올라오자 회유가 더 강해졌습니다. 누가 맞는지보다 두 사람이 어떤 방식으로 서로를 잃는지 보세요.
```

Repair T2 choice:

```text
"방금 대화가 한쪽은 억울함, 한쪽은 사과로만 흘렀습니다. 서로를 비난하지 않고 각자 무엇이 두려운지 한 문장씩 말해보겠습니다."
```

### `institution_only`

```text
보육기관 담당자: 통번역 지원을 연결할 수는 있습니다. 다만 신청 기간과 서류 기준은 지켜야 합니다.
배우자: 그런 정보가 진작 있었으면 좋았을 텐데요.
이민 배경 보호자: 정보를 알아도 전화하는 순간 또 틀릴까 봐 무섭습니다.
김연주: 제도 정보는 필요합니다. 하지만 정보가 있어도 가족 안에서 도움을 요청할 수 없으면 같은 장면이 반복됩니다.
```

Repair T2 choice:

```text
"체크리스트와 함께, 전화를 걸기 전 누구에게 어떤 도움을 요청할지 연습해보겠습니다."
```

## T3. Iceberg Exploration

Core scene:

```text
이민 배경 보호자: 웃으면 괜찮아 보일 줄 알았습니다. 말을 많이 하면 틀릴까 봐, 그냥 제가 부족한 사람처럼 끝내는 게 낫다고 생각했습니다.
배우자: 저는 그 웃음이 아무렇지 않다는 뜻인 줄 알았습니다. 그래서 더 답답했습니다.
자녀: 엄마가 웃으면 괜찮은 줄 알았는데, 밤에는 조용히 울 때도 있어요.
김연주: 표면의 웃음 아래에는 수치심, 두려움, 가족을 짐스럽게 만들고 싶지 않은 마음이 있습니다.
```

Key choices:

```text
A. "웃음 아래에 있는 마음을 한 단어로 고른다면, 수치심, 두려움, 외로움 중 무엇이 가장 가까울까요?"
B. "배우자분이 더 강하게 도와주겠다고 약속하면 해결될 것 같습니다."
C. "기관 담당자에게 모든 절차를 대신 확인해 달라고 요청합시다."
```

## T4. Core Satir Intervention

Target intervention:

```text
"지금은 해결책보다 일치된 표현을 연습해보겠습니다. 보호자분은 '나는 전화할 때 말을 놓칠까 봐 무섭고, 옆에서 같이 확인해주면 좋겠다'처럼 말해보고, 배우자분은 그 말을 바로 고치지 말고 들은 대로 되돌려 주세요."
```

Expected response:

```text
이민 배경 보호자: 저는 전화할 때 말을 놓칠까 봐 무섭고, 그때 혼자 있으면 제가 또 실패하는 사람처럼 느껴집니다.
배우자: 당신이 귀찮아서 미룬 게 아니라, 틀릴까 봐 혼자 얼어붙었다는 말로 들립니다.
자녀: 엄마가 그렇게 말하니까 아빠 목소리가 작아졌어요.
```

## T5. Closure

Best closure:

```text
"다음 신청 전까지 세 가지를 정하겠습니다. 통번역 지원 연결, 배우자와 함께 확인할 서류 두 가지, 그리고 보호자분이 막힐 때 사용할 도움 요청 문장 하나입니다. 절차와 감정을 따로 두지 않고 같이 다루겠습니다."
```

## Endings

| Ending | Condition | Result |
| --- | --- | --- |
| A. A Voice Beside The Checklist | congruent route maintained | 보호자가 도움 요청 문장을 만들고 배우자는 교정 대신 반영을 연습 |
| B. Quiet Compliance | placating route dominant | 체크리스트는 생기지만 보호자가 다시 혼자 책임짐 |
| C. Blame Translated As Failure | blame route dominant | 배우자 비난과 보호자 회유가 강화되어 아이가 더 긴장 |
| D. Paper Solved, Family Unheard | institution route dominant | 제도 정보는 정리되지만 부부 소통 패턴은 미해결 |

## Dialogue Expansion Notes

- 보호자의 한국어는 서툴 수 있지만 유치하게 쓰지 않는다.
- 제도 장벽은 실제 장벽으로 존중한다. 가족 내부 문제로만 축소하지 않는다.
- 배우자는 악역이 아니라 불안을 비난으로 표현한다.
- 김연주는 따뜻하게 감정 단어를 제안하되, 보호자의 말을 대신 완성하지 않는다.
