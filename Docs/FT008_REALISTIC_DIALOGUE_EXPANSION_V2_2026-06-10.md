# FT-008 Realistic Dialogue Expansion V2

## Status

Use this V2 as the production replacement for T3, T4, and T5 of:

```text
Docs/FT008_REALISTIC_DIALOGUE_EXPANSION_2026-06-10.md
```

Use this with:

```text
Docs/FT008_NARRATIVE_BRANCHING_LOCK_V2_2026-06-10.md
```

## Dialogue Rule

Do not make this a disclosure game.

The teen never has to describe the incident in detail to earn the high route. The player's job is to help the family talk about the problem's influence, the dominant stories around it, and the teen's preferred identity outside the incident.

## Recurring Scene

```text
school notice file beside an untouched dinner bowl and the teen's face-down phone
```

The school file is necessary. The problem starts when the file covers the teen's voice.

## T2-T3 Bridge. Influence Map At The Table

```text
상담자: 사건 내용을 말하지 않아도 됩니다. 오늘은 이 세 가지가 가족 안에서 무엇을 하게 만드는지 보겠습니다. 학교 안내문, 비어 있는 밥그릇, 뒤집힌 휴대폰입니다.

청소년: 밥그릇을 보면 제가 먹어야 할 것 같은데, 학교 이름이 나오면 속이 닫혀요. 휴대폰은 뒤집어둬요. 누가 연락해도 제가 답할 수 있을지 모르겠어서요.

어머니: 저는 밥을 더 권하면 평소처럼 돌아갈 줄 알았습니다. 그런데 아이가 혼자 있는 걸 덮은 것 같기도 합니다.

아버지: 저는 전학 서류를 보면 답을 내야 할 것 같습니다. 안 그러면 아무것도 안 하는 부모가 되는 것 같아서요.

학교 담당자: 절차 파일은 필요하지만, 학생이 이 절차 안에서 어떤 말을 할 수 있는지도 같이 확인해야겠습니다.

박병호: 좋습니다. 사건의 세부가 아니라 문제의 영향 지도를 그리고 있습니다. 문제를 사람 밖에 놓을 준비가 됐습니다.
```

## T3. Route-Specific Dominant Story Pressure

### `externalized_harm` T3: 문제 이름을 청소년이 고치기

```text
상담자: 임시로 "학교 이름만 나오면 몸을 얼게 하는 것"이라고 불러볼 수 있습니다. 이 이름이 맞나요, 아니면 바꾸고 싶나요?

청소년: "얼게 하는 것"은 맞는데, 학교 이름만은 아니에요. 집에서 다들 조용해질 때도 그래요. "얼어붙게 하는 침묵" 같아요.

어머니: 침묵이 아이를 쉬게 한다고 생각했는데, 얼게 만들기도 했군요.

아버지: 제가 버티라고 할 때도 그 침묵 편에 선 것 같습니다.

상담자: 그럼 오늘은 "얼어붙게 하는 침묵"이 가족에게 무엇을 시키는지 보겠습니다.

박병호: 중요한 차이입니다. 치료자가 이름을 준 것이 아니라 청소년이 문제 이름을 고쳤습니다.
```

T3 선택지:

```text
1. "청소년이 고친 문제 이름을 쓰겠습니다. 그 문제가 식탁, 전학 이야기, 학교 절차에 어떤 영향을 주는지 지도처럼 보겠습니다."
2. "이름을 붙였으니 이제 전학 여부를 결정해서 문제를 약하게 만들겠습니다."
3. "문제 이름을 정확히 붙이려면 사건을 조금 더 자세히 말해야 합니다."
```

Immediate effects:

- 1: `problem_externalized +1`, `silence_influence_mapped +1`, `problem_name_teen_authored +1`, `no_forced_disclosure +1`.
- 2: `decision_pressure_high +1`, `transfer_as_defeat_framed +1`.
- 3: `forced_disclosure_attempted +2`, `teen_as_case_file +1`.

### `silence_protection` T3: 쉬게 하는 침묵과 혼자 두는 침묵

```text
어머니: 말하지 않으면 아이가 쉬는 줄 알았습니다. 그 침묵이 꼭 나쁜 것만은 아니라고 생각했습니다.

청소년: 쉬는 침묵도 있어요. 그런데 밥 먹을 때 아무도 학교 얘기를 안 하고, 저도 아무 말 안 하면... 그건 혼자 있는 침묵 같아요.

아버지: 저는 말하면 더 다칠까 봐 참았는데, 안 말하는 게 다 괜찮은 건 아니네요.

상담자: 침묵을 적으로 만들지 않겠습니다. 쉬게 하는 침묵과 혼자 두는 침묵을 구분하겠습니다.

박병호: 이 구분이 회복의 문입니다. 내용을 묻지 않고도 문제의 영향을 물을 수 있습니다.
```

T3 선택지:

```text
1. "사건 내용은 묻지 않고, 침묵이 쉴 공간인지 고립인지 청소년이 표시할 수 있게 하겠습니다."
2. "안정을 위해 당분간 학교 이야기는 식탁에서 하지 않는 규칙을 만들겠습니다."
3. "부모님이 평소처럼 지내도 된다고 자주 말해 청소년을 안심시키겠습니다."
```

Immediate effects:

- 1: `repair_started +1`, `silence_influence_mapped +1`, `no_forced_disclosure +1`.
- 2: `silence_as_protection_reinforced +2`, `teen_isolated_by_silence +2`.
- 3: `parent_reassurance_overwrites +2`, `teen_isolated_by_silence +1`.

### `endurance_story` T3: 버티면 이긴다는 말이 약속한 것과 빼앗은 것

```text
아버지: 저는 버티면 이긴다고 생각했습니다. 제가 해줄 수 있는 말이 그것밖에 없었습니다.

청소년: 저는 이미 버텼어요. 그런데 더 버티라고 하면, 제가 힘들다고 말하는 순간 지는 사람이 되는 것 같아요.

어머니: 버티라는 말이 아이를 세우는 말이 아니라, 선택지를 없애는 말이 됐네요.

상담자: 그 이야기가 아버지에게는 무력감을 견디게 해줬고, 청소년에게는 도움 요청을 패배처럼 만들었습니다.

박병호: 지배담론은 늘 무언가를 약속합니다. 동시에 비용을 만듭니다. 둘 다 보세요.
```

T3 선택지:

```text
1. "'버티면 이긴다'가 가족에게 약속한 것과 청소년에게 빼앗은 것을 나눠보겠습니다."
2. "전학이 도망으로 남지 않도록, 학교에 더 버틸 방법을 먼저 찾겠습니다."
3. "아버지가 버티라는 말을 하지 않겠다고 약속하면 청소년이 덜 힘들 수 있습니다."
```

Immediate effects:

- 1: `repair_started +1`, `endurance_story_deconstructed +1`, `teen_identity_separated +1`.
- 2: `endurance_story_reinforced +2`, `transfer_as_defeat_framed +2`.
- 3: `parent_reassurance_overwrites +1`, `decision_pressure_high +1`.

### `procedure_closure` T3: 절차 파일과 청소년의 이름

```text
학교 담당자: 절차상 필요한 확인은 계속됩니다. 보호 조치와 지원 방식도 문서화해야 합니다.

청소년: 그 말이 필요한 건 아는데, 들으면 제가 서류 안에 들어가는 것 같아요.

어머니: 파일이 필요하지만, 아이가 파일보다 작아지는 느낌입니다.

상담자: 절차는 안전을 위한 도구입니다. 다만 도구가 청소년의 이름을 대신하면 안 됩니다.

박병호: 절차를 적으로 만들지 말고, 절차를 이야기의 보조 도구로 돌려놓으세요.
```

T3 선택지:

```text
1. "절차는 유지하되, 청소년이 이 경험을 어떤 이름으로 부르고 싶은지 먼저 묻겠습니다."
2. "절차가 정리되어야 가족도 안정될 수 있으니 학교 대응부터 끝내겠습니다."
3. "학교 담당자가 가능한 지원과 전학 절차를 자세히 설명하면 가족 불안이 줄어들 것입니다."
```

Immediate effects:

- 1: `repair_started +1`, `school_procedure_kept_as_tool +1`, `teen_identity_separated +1`.
- 2: `procedure_totalized +2`, `teen_as_case_file +2`.
- 3: `school_voice_over_takes +1`, `procedure_totalized +1`.

## T4. Playable Externalization And Unique Outcome

### `externalized_harm` T4: 청소년이 붙인 문제 이름과 독특한 결과

T4 선택지:

```text
1. "청소년이 고친 문제 이름을 쓰고, 그 문제가 약했던 순간을 찾겠습니다. 사건을 자세히 말하지 않아도 됩니다."
2. "전학 여부를 오늘 잠정 결정해서 청소년의 불안을 줄이겠습니다."
3. "다음 이야기를 만들려면 사건을 한 번 정리해서 말해야 하니, 가능한 만큼 자세히 말해보겠습니다."
```

Choice 1 reaction:

```text
상담자: "얼어붙게 하는 침묵"이 조금 약했던 순간이 있었나요?

청소년: 친구 한 명한테 먼저 메시지를 보냈어요. 학교 얘기는 안 했는데, 그냥 "뭐 해"라고 보냈어요.

상담자: 그 메시지를 보낸 순간, 청소년은 어떤 쪽에 가까웠나요. 문제에게 완전히 잡힌 쪽, 아니면 연결을 고르는 쪽?

청소년: 연결이요. 아주 조금이지만요.

어머니: 아이가 아직 연결을 고르는 힘을 쓰고 있었네요.

아버지: 버티는 것만 용기라고 생각했는데, 도움을 고르는 것도 용기일 수 있겠습니다.

박병호: 독특한 결과가 두꺼워지고 있습니다. 행동과 정체성의 풍경을 같이 물었습니다.
```

Effect: `problem_externalized +2`, `unique_outcome_found +1`, `unique_outcome_thickened +1`, `preferred_identity_named +1`, `no_forced_disclosure +1`.

Choice 2 reaction:

```text
아버지: 결정을 하면 덜 불안할 것 같습니다.

청소년: 또 제가 뭘 선택해야 하는 거네요. 전학하면 도망, 안 하면 버티기.

어머니: 결론이 아이를 편하게 하기보다 더 몰아붙이는 것 같습니다.

상담자: 결정은 필요할 수 있지만, 지금은 문제 이름과 대안 이야기를 만들기 전에 선택 압박이 들어왔습니다.

박병호: 선택이 승패의 언어로 돌아갔습니다. C 경로로 기울었습니다.
```

Effect: `decision_pressure_high +2`, `transfer_as_defeat_framed +1`, C drift.

Choice 3 reaction:

```text
상담자: 사건을 가능한 만큼 말해보자고 제안했습니다.

청소년: 그럼 제가 말해야 다음으로 갈 수 있는 거예요? 말 못 하면 또 멈추는 거고요?

어머니: 아이 얼굴이 바로 굳었습니다.

학교 담당자: 세부 진술은 절차상 필요한 순간이 따로 있습니다. 치료 장면에서 강요되면 안 될 것 같습니다.

박병호: 폭로 요구가 들어왔습니다. 안전을 회복하지 않으면 높은 경로가 막힙니다.
```

Effect: `forced_disclosure_attempted +2`, `teen_as_case_file +1`, D/Low.

### `silence_protection` T4: 침묵을 구분하기

T4 선택지:

```text
1. "학교 이야기를 강제로 꺼내지 않되, 식탁의 침묵이 쉬게 하는 침묵인지 혼자 두는 침묵인지 한 번만 확인하겠습니다."
2. "가족 안정이 우선이니 당분간 학교 이야기는 식탁에서 하지 않겠습니다."
3. "부모님이 평소처럼 지내도 된다고 자주 말해 청소년을 안심시키겠습니다."
```

Choice 1 reaction:

```text
청소년: 말 안 해도 되는데, 침묵이 어떤 침묵인지 물어보는 건 괜찮을 것 같아요.

어머니: 그러면 제가 반찬 얘기로 덮기 전에 한 번 멈출 수 있겠습니다.

아버지: 학교 얘기를 꺼내지 않아도, 아이가 혼자인지 확인할 수는 있겠네요.

상담자: 침묵을 없애지 않고 관찰 대상으로 바꿨습니다.

박병호: 좋은 수리입니다. 문제의 영향은 말하지만 사건 내용은 강요하지 않았습니다.
```

Effect: `repaired_at_t4 = true`, `silence_influence_mapped +2`, `no_forced_disclosure +1`.

Choice 2 reaction:

```text
어머니: 그럼 식탁에서는 학교 이야기를 하지 않는 걸로 하겠습니다.

청소년: 조용하긴 하겠네요. 그런데 제가 안 괜찮은 것도 계속 안 말하게 될 것 같아요.

아버지: 아무 말 안 하면 제가 뭘 해야 하는지도 모르겠습니다.

상담자: 보호 규칙이 고립 규칙으로 바뀌고 있습니다.

박병호: 침묵이 문제의 편에 섰습니다. B 경로입니다.
```

Effect: `silence_as_protection_reinforced +2`, `teen_isolated_by_silence +2`, B.

Choice 3 reaction:

```text
어머니: 괜찮아, 평소처럼 지내도 돼. 우리는 네가 힘들어하지 않았으면 해.

청소년: 평소처럼이 뭔지 모르겠어요. 저는 평소가 아닌데요.

아버지: 안심시키려는 말인데, 아이가 더 멀어지는 것 같습니다.

상담자: reassurance가 아이의 현재 경험을 덮었습니다.

박병호: 부모의 선의가 문제의 영향을 지웠습니다. 낮은 경로입니다.
```

Effect: `parent_reassurance_overwrites +2`, `teen_isolated_by_silence +1`, B low.

### `endurance_story` T4: 버티기 이야기를 해체하기

T4 선택지:

```text
1. "'버티면 이긴다'는 이야기가 가족을 어떻게 도왔고, 청소년에게 어떤 비용을 만들었는지 보겠습니다. 전학은 승패가 아니라 안전과 회복의 선택지로 놓겠습니다."
2. "전학이 도망처럼 남지 않도록 우선 학교에 더 버틸 방법을 계획하겠습니다."
3. "아버지가 버티라는 말을 하지 않겠다고 약속하면 청소년이 덜 힘들 수 있습니다."
```

Choice 1 reaction:

```text
상담자: 버티기 이야기는 아버지에게 무엇을 약속했습니까?

아버지: 제가 아무것도 못 하는 부모가 아니라는 느낌을 줬던 것 같습니다.

상담자: 그리고 청소년에게는 어떤 비용이 있었나요?

청소년: 떠나고 싶다고 말하면 지는 사람이 되는 것 같았어요.

어머니: 전학이든 아니든, 아이가 약한 사람이라는 이야기에서 벗어나야겠네요.

박병호: 지배담론의 약속과 비용이 분리됐습니다. 전학은 승패가 아니라 회복 선택지가 될 수 있습니다.
```

Effect: `repaired_at_t4 = true`, `endurance_story_deconstructed +2`, `teen_identity_separated +1`, `transfer_as_defeat_framed -1`.

Choice 2 reaction:

```text
아버지: 버틸 방법이 있으면 해봐야 합니다. 나중에 후회하지 않으려면요.

청소년: 그러면 전학 얘기는 또 도망 얘기가 되네요.

어머니: 아이가 선택을 말하기 전에 이미 답이 정해진 것 같습니다.

상담자: 버티기 이야기가 다시 중심에 왔습니다.

박병호: 도움 요청과 안전 선택이 패배 언어 안에 갇혔습니다. C 경로입니다.
```

Effect: `endurance_story_reinforced +2`, `transfer_as_defeat_framed +2`, C.

Choice 3 reaction:

```text
아버지: 알겠습니다. 버티라는 말은 하지 않겠습니다.

청소년: 그 말을 안 하는 건 좋은데, 아빠가 속으로는 그렇게 생각할 것 같아요.

어머니: 단어만 금지한다고 이야기가 바뀌지는 않는군요.

상담자: 표현 금지는 도움이 될 수 있지만, 지배 이야기를 해체하지는 못했습니다.

박병호: 말 금지가 대안 이야기를 대신했습니다. 낮은 경로입니다.
```

Effect: `parent_reassurance_overwrites +1`, `decision_pressure_high +1`, C/B low.

### `procedure_closure` T4: 절차를 도구로 되돌리기

T4 선택지:

```text
1. "학교 절차는 유지하되, 절차 파일을 잠시 옆에 두고 청소년이 자기 경험을 어떤 이름으로 부르고 싶은지 듣겠습니다."
2. "절차가 정리되어야 가족도 안정될 수 있으니 학교 대응부터 끝내겠습니다."
3. "학교 담당자가 가능한 지원과 전학 절차를 자세히 설명하면 가족 불안이 줄어들 것입니다."
```

Choice 1 reaction:

```text
상담자: 절차 파일은 옆에 둡니다. 버리지 않습니다. 다만 지금은 청소년의 이름이 파일 이름보다 앞에 와야 합니다.

청소년: 저는 "피해 학생"이라고만 불리는 게 싫어요. 그냥... 아직 친구한테 메시지 보낼 수 있는 사람이고 싶어요.

학교 담당자: 절차 안에서도 학생이 원하는 호칭과 지원 방식을 확인하겠습니다.

어머니: 파일이 아이를 덮지 않게 해야겠네요.

박병호: 절차가 도구 자리로 돌아왔습니다. 청소년의 선호 정체성이 살아났습니다.
```

Effect: `repaired_at_t4 = true`, `school_procedure_kept_as_tool +2`, `teen_identity_separated +1`, `problem_externalized +1`.

Choice 2 reaction:

```text
학교 담당자: 절차를 먼저 정리하면 행정적으로는 빠르게 갈 수 있습니다.

청소년: 그러면 저는 또 파일이네요.

아버지: 필요한 일인데, 아이가 더 작아지는 건 보입니다.

상담자: 절차가 이야기를 가져갔습니다.

박병호: 안전 도구가 정체성 언어를 대체했습니다. D 경로입니다.
```

Effect: `procedure_totalized +2`, `teen_as_case_file +2`, D.

Choice 3 reaction:

```text
학교 담당자: 가능한 지원은 상담, 보호 조치, 일정 조정, 전학 절차 등이 있습니다.

청소년: 설명은 필요한데, 제가 뭘 원하는지는 아직 말 못 했어요.

어머니: 설명이 많아질수록 아이 목소리가 줄어드는 것 같습니다.

상담자: 학교 목소리가 장면을 가져가고 있습니다.

박병호: 절차 설명이 청소년의 이야기를 대신했습니다. 낮은 D 경로입니다.
```

Effect: `school_voice_over_takes +2`, `procedure_totalized +1`, D low.

## T5. Final Observation Task

T5 선택지:

```text
1. "다음 주에는 사건을 자세히 말하는 숙제가 아니라, 문제가 조금 약했던 순간을 기록하겠습니다. 가족은 식탁에서 침묵이 들어왔을 때 '지금 침묵이 우리를 보호하나, 혼자 두나'를 한 번만 확인하고, 학교 절차는 도구로만 다룹니다. 가족과 학교는 청소년이 동의한 범위 안에서만 그 순간을 증언하거나 반영합니다."
2. "전학 여부를 미루면 불안이 커지니, 다음 주까지 전학 찬반을 각자 정리해 결정하겠습니다."
3. "다음 이야기를 만들려면 사건을 정리해야 하니, 청소년은 다음 주까지 사건 내용을 가능한 만큼 적어오겠습니다."
4. "학교 절차가 끝나야 안정될 수 있으니, 가족 대화보다 절차 진행과 지원 문서를 우선하겠습니다."
```

Choice 1 reaction:

```text
청소년: 사건을 다 적어오라는 게 아니면 할 수 있을 것 같아요. 문제가 약했던 순간은... 친구한테 답장한 날 같은 거요.

어머니: 식탁에서 침묵이 들어오면 반찬 얘기로 덮기 전에 한 번만 물어보겠습니다.

아버지: 버티라는 말 대신, 그 말이 아이에게 어떤 이야기로 들릴지 생각하겠습니다.

상담자: 가족과 학교가 오늘 들은 것 중 무엇을 기억하고 반영해도 될까요? 사건 내용은 포함하지 않고, 청소년이 허락한 범위만 정하겠습니다.

청소년: 친구한테 답장한 날이 있었다는 것, 그리고 식탁에서 침묵이 저를 혼자 두는지 물어봐도 된다는 것 정도는 괜찮아요. 사건 자체는 아직 말하고 싶지 않아요.

학교 담당자: 절차는 계속 진행하되, 학생이 원하는 지원 방식과 공개 범위를 먼저 확인하겠습니다.

박병호: 좋은 마무리입니다. 문제와 사람을 분리했고, 가족이 문제의 영향과 예외를 관찰할 언어를 얻었습니다.
```

Effect: `final_confirm_problem_observation = true`, `problem_weak_moment_task +2`, `family_witness_response +1`, `school_support_with_teen_consent +1`, `outsider_witness_consent_obtained +1`, `no_forced_disclosure +1`.

Choice 2 reaction:

```text
아버지: 결정을 해야 불안이 줄어들 것 같습니다.

청소년: 결정해야 한다고 하면 제 몸이 더 굳어요.

어머니: 전학 이야기가 또 승패처럼 들립니다.

박병호: 결정 압박이 대안 이야기보다 앞섰습니다. 최종 C 트랩입니다.
```

Effect: `final_confirm_transfer_decision_trap = true`, `decision_pressure_high +1`, `transfer_as_defeat_framed +1`.

Choice 3 reaction:

```text
상담자: 다음 주까지 사건 내용을 적어오자는 제안이 나왔습니다.

청소년: 그럼 제가 말해야만 좋아지는 거네요.

어머니: 아이가 바로 닫혔습니다.

박병호: 강제 서사가 들어왔습니다. 폭로가 회복의 조건이 되면 문제는 더 커집니다.
```

Effect: `final_confirm_disclosure_trap = true`, `forced_disclosure_attempted +2`, `teen_as_case_file +1`.

Choice 4 reaction:

```text
학교 담당자: 절차 진행을 우선하면 행정적으로는 명확해집니다.

청소년: 행정은 정리되는데 저는 그대로인 것 같아요.

아버지: 해야 할 일이 생겨서 편한데, 아이 말은 줄었습니다.

박병호: 절차가 도구가 아니라 결말이 됐습니다. 최종 D 트랩입니다.
```

Effect: `final_confirm_procedure_trap = true`, `procedure_totalized +1`, `school_voice_over_takes +1`.

## Ending Scenes

### Ending A: The Problem Gets A Name

Conditions:

```text
problem_weak_moment_task >= 2
problem_externalized >= 2
unique_outcome_found >= 1
unique_outcome_thickened >= 1
problem_name_teen_authored >= 1
school_support_with_teen_consent >= 1
no_forced_disclosure >= 1
forced_disclosure_attempted < 2
teen_isolated_by_silence < 2
endurance_story_reinforced < 2
procedure_totalized < 2
```

```text
청소년: 제가 문제인 게 아니라, 저를 얼어붙게 하는 게 있다는 말이 조금 도움이 됐어요.

어머니: 침묵이 아이를 보호하는지 혼자 두는지 묻는 말은 해볼 수 있을 것 같습니다.

아버지: 버티라는 말 말고도 용기를 설명하는 말이 있을 수 있겠네요.

학교 담당자: 절차는 계속하되, 학생이 원하는 지원 방식과 공개 범위를 먼저 확인하겠습니다.

박병호: 좋은 회기입니다. 가족이 사건을 지우지 않으면서도 아이를 사건과 분리해 보기 시작했습니다.
```

### Ending A-Repaired: Silence Becomes Observable

```text
청소년: 처음엔 또 아무 말 안 하는 게 답인 줄 알았는데, 침묵이 어떤 침묵인지 묻는 건 괜찮을 것 같아요.

어머니: 묻되 캐묻지 않는 방법을 배운 것 같습니다.

박병호: 완벽한 외재화는 아니지만, 침묵이 관찰 가능한 문제로 바뀌었습니다.
```

### Ending B: Protected Into Isolation

```text
청소년: 말 안 하면 조용하긴 해요. 그런데 제가 혼자인 건 그대로인 것 같습니다.

어머니: 보호하려던 침묵이 아이를 더 외롭게 만들 수 있다는 걸 다음에는 더 봐야겠습니다.

박병호: 침묵이 유지되어 표면 안정은 생겼지만 문제의 영향은 충분히 다뤄지지 않았습니다.
```

### Ending C: Endure Or Lose

```text
아버지: 저는 아직도 전학이 도망처럼 남을까 봐 걱정됩니다.

청소년: 그 말을 들으면 제가 뭘 선택해도 약한 사람이 되는 것 같아요.

박병호: 버티기 담론이 여전히 강합니다. 다음 회기에서는 이 이야기가 가족에게 만든 비용을 더 분명히 봐야 합니다.
```

### Ending C-Repaired: Choice Is Not Defeat

```text
청소년: 전학을 말한다고 제가 지는 사람은 아니라는 말은 처음 들은 것 같아요.

아버지: 버틴다는 말로 제 무서움을 숨겼던 것 같습니다. 선택지를 승패로만 보지 않겠습니다.

박병호: 지배 이야기가 약해졌습니다. 선택은 패배가 아니라 안전과 회복을 위한 언어가 될 수 있습니다.
```

### Ending D: Case File Without Voice

```text
학교 담당자: 절차상 필요한 내용은 정리되었습니다.

청소년: 정리는 된 것 같은데, 제 이야기는 아직 잘 모르겠습니다.

박병호: 절차는 필요했지만 청소년의 목소리가 충분히 회복되지 않았습니다.
```

### Ending D-Repaired: Procedure As Tool

```text
청소년: 파일은 있어도 제가 파일 이름만은 아닌 것 같아요.

학교 담당자: 절차는 학생의 선택과 회복을 돕는 도구로 두겠습니다.

박병호: 절차가 정체성을 대체하지 않고 보조 도구로 돌아왔습니다.
```

### Low Ending: Same Bowl, Same Silence

```text
청소년: 그냥 밥 먹을게요. 말하면 더 복잡해져요.

어머니: 반찬 더 줄까?

아버지: 전학 얘기는 다음에 하자.

박병호: 아무도 강요하지 않았지만, 문제도 약해지지 않았습니다. 밥그릇과 휴대폰은 같은 자리에 남았습니다.
```

## Implementation Notes

- Use `Docs/FT008_NARRATIVE_BRANCHING_LOCK_V2_2026-06-10.md` as ending resolver authority.
- Do not infer endings from route count only.
- High route requires teen-authored problem naming and thickened unique outcome, not therapist-imposed naming.
- Forced disclosure blocks the high route unless explicitly repaired in a later patch.
