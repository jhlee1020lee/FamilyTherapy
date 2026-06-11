# FT-006 Realistic Dialogue Expansion V2

## Status

Use this V2 as the production replacement for T3, T4, and T5 of:

```text
Docs/FT006_REALISTIC_DIALOGUE_EXPANSION_2026-06-10.md
```

Use this with:

```text
Docs/FT006_SATIR_ILLNESS_SIBLING_BRANCHING_LOCK_V2_2026-06-10.md
```

## Dialogue Rule

The well sibling should not sound like a trainee therapist.

They usually speak in short lines:

```text
"괜찮아요."
"그냥요."
"말하면 엄마가 울 것 같아서요."
"언니가 싫다는 건 아니에요."
```

The therapist and 김연주 can name the Satir frame, but family members should show it through interruptions, pauses, schedule-checking, apology flooding, and tight smiles.

## Recurring Scene

```text
hospital tote bag beside a closed school backpack
```

The hospital bag is not villainous. It represents real care, fear, and survival. The problem is that it keeps covering the school backpack.

## T2-T3 Bridge. Family Sculpture Base

Use this physical arrangement before the route-specific T3 scenes:

```text
상담자: 말로 설명하기 전에 자리로 놓아보겠습니다. 병원 가방은 이쪽, 학교가방은 반대쪽에 두겠습니다.

어머니는 병원 가방과 둘째 사이에 선다. 몸은 둘째 쪽에 있지만 시선은 계속 병원 가방 쪽으로 돌아간다.

아버지는 접힌 치료 일정표 옆에 앉는다. 일정표를 접으려다 다시 손을 얹는다.

둘째는 닫힌 학교가방 옆에 선다. 가방 지퍼를 만지지만 열지는 않는다.

조부모/도움 인물은 열쇠고리를 들고 방 가장자리에 선다. 도울 준비가 되어 있지만 중심에는 들어오지 않는다.

김연주: 좋은 가족조각입니다. 누가 나쁜지 보는 장면이 아닙니다. 병원 현실이 어디에 있고, 둘째의 하루가 어디에 놓였는지 보는 장면입니다.
```

Satir iceberg cues to keep alive through T3/T4:

```text
behavior: "괜찮아요", tight smile, closed backpack
coping stance: placating/protective good child
feelings: lonely, worried, angry for a second, scared
feelings-about-feelings: guilt about wanting attention
perception: "병원 이야기가 나오면 내 이야기는 나중이다"
expectation: "나는 이해해야 한다"
yearning: "내 하루도 물어봐 줬으면"
self-worth: "힘들다고 말해도 나쁜 아이가 아니다"
```

## T3. Route-Specific Iceberg Pressure

### `sibling_visible` T3: 병원 알람이 둘째 문장을 끊을 때

```text
둘째: 언니 걱정돼요. 근데 집에 오면... 제 얘기는 좀 나중에 해야 할 것 같아요.

어머니의 휴대폰에서 병원 알람이 울린다. 어머니가 화면을 보고 손을 멈춘다.

어머니: 미안해, 약 시간 알람이라서. 잠깐만.

둘째: 괜찮아요. 이게 더 중요하잖아요.

상담자: 방금 중요한 장면이 나왔습니다. 둘째의 말이 열리려는 순간 병원 알람이 들어왔고, 둘째는 바로 자기 말을 접었습니다.

아버지: 알람을 무시할 수는 없습니다. 하지만 그 순간 아이가 사라지는 것도 보입니다.

김연주: 빙산 아래를 보세요. 표면은 "괜찮아요"지만 아래에는 외로움, 죄책감, 방해하면 안 된다는 규칙이 있습니다.
```

T3 선택지:

```text
1. "알람은 필요한 현실로 두되, 끊긴 둘째의 문장을 다시 이어보겠습니다. 언니 걱정과 외로움이 같이 있을 수 있습니다."
2. "둘째가 불안하지 않도록 약 시간과 치료 일정이 왜 중요한지 더 설명하겠습니다."
3. "부모님이 방금 둘째 말을 끊어서 미안하다고 먼저 충분히 표현하겠습니다."
```

Immediate effects:

- 1: `two_feelings_coexist +1`, `iceberg_below_okay_named +1`, `iceberg_layers_named +1`, `sibling_feeling_named +1`.
- 2: `treatment_explanation_overused +1`, `illness_totalized +1`.
- 3: `parent_guilt_flooded +1`, `apology_loop_reinforced +1`.

### `illness_totalizing` T3: 치료 일정표가 식탁을 덮을 때

```text
아버지: 다음 주도 병원 일정이 빡빡합니다. 이걸 먼저 정해야 합니다.

아버지가 접힌 치료 일정표를 펼친다. 둘째의 학교 안내장이 일정표 아래로 밀린다.

둘째: 저는 괜찮아요. 학교 건 그냥 나중에 해도 돼요.

어머니: 네 학교 일도 중요한데... 지금은 병원 연락부터 해야 해서.

상담자: 치료 일정표가 실제로 필요합니다. 동시에 둘째의 학교 안내장이 아래로 들어갔습니다.

김연주: 현실을 부정하지 말고, 현실이 누구의 목소리를 덮는지 보세요.
```

T3 선택지:

```text
1. "치료 일정표는 접어두지 않되, 지금은 둘째의 학교 안내장을 위에 올리고 한 감정만 묻겠습니다."
2. "둘째가 상황을 더 잘 이해하도록 다음 주 치료 일정을 자세히 설명하겠습니다."
3. "치료가 안정될 때까지 둘째 이야기는 짧게 확인하고 넘어가겠습니다."
```

Immediate effects:

- 1: `repair_started +1`, `illness_reality_kept +1`, `sibling_feeling_named +1`.
- 2: `treatment_explanation_overused +2`, `illness_totalized +2`.
- 3: `sibling_need_postponed +2`, `sibling_self_erases +1`.

### `parent_guilt_flood` T3: 엄마의 눈물이 방을 차지할 때

```text
어머니: 내가 정말 몰랐구나. 너를 너무 혼자 뒀구나. 엄마가 미안해.

어머니가 휴지를 쥔다. 둘째가 고개를 숙이다가 어머니 쪽으로 몸을 돌린다.

둘째: 엄마 울지 마세요. 저 진짜 괜찮아요.

아버지: 이렇게 되면 또 아이가 우리를 달래네요.

상담자: 부모님의 죄책감은 자연스럽지만, 지금 방 한가운데를 차지하면 둘째는 다시 부모님을 돌봅니다.

김연주: 죄책감을 없애라는 뜻이 아닙니다. 죄책감을 옆 의자에 앉히고, 둘째의 말을 중심에 둬야 합니다.
```

T3 선택지:

```text
1. "부모님 죄책감은 중요한 마음으로 인정하되, 지금은 둘째가 부모를 달래지 않게 한 문장만 반영하겠습니다."
2. "부모님이 미안했던 점을 충분히 말하고 둘째에게 사과하는 시간을 갖겠습니다."
3. "둘째도 부모님이 얼마나 힘들었는지 이해한다고 말해보겠습니다."
```

Immediate effects:

- 1: `parent_regulation_score +1`, `sibling_feeling_named +1`, `repair_started +1`.
- 2: `parent_guilt_flooded +2`, `apology_loop_reinforced +2`.
- 3: `sibling_parentifies +2`, `parent_guilt_flooded +1`.

### `cheerful_mask` T3: 대견하다는 말이 가면이 될 때

```text
조부모/도움 인물: 그래도 얘가 참 대견해요. 어린데도 투정 한 번 안 하고.

둘째가 웃는다. 입꼬리는 올라가지만 손은 닫힌 학교가방 끈을 잡고 있다.

둘째: 네. 저 잘하고 있어요.

어머니: 그 말을 들으면 고마운데, 또 미안해집니다.

상담자: 대견하다는 말이 강점을 보는 말일 수도 있지만, 계속 괜찮아야 한다는 역할이 될 수도 있습니다.

김연주: Satir식으로는 생존 자세를 봐야 합니다. "착한 아이" 자세가 아이를 지켜줬지만, 이제는 감정을 숨기게 할 수 있습니다.
```

T3 선택지:

```text
1. "잘 버틴 힘과 외로운 마음을 같이 놓겠습니다. 강하다는 말이 힘들지 않다는 뜻은 아닙니다."
2. "둘째가 가족을 위해 참아준 점을 더 인정하고 칭찬하겠습니다."
3. "부모님이 바쁘니 조부모가 둘째 마음을 더 자주 들어주도록 하겠습니다."
```

Immediate effects:

- 1: `two_feelings_coexist +1`, `sibling_feeling_named +1`, `repair_started +1`.
- 2: `cheerful_mask_reinforced +2`, `maturity_praised_as_role +2`.
- 3: `support_relative_substitution +2`, `sibling_need_postponed +1`.

## T4. Playable Satir Intervention

### `sibling_visible` T4: 두 마음을 일치되게 말하기

T4 선택지:

```text
1. "둘째는 '언니가 걱정되지만 집에 혼자 있을 때 외롭다'를 말하고, 부모는 사과보다 먼저 들은 마음을 되돌려주겠습니다."
2. "부모님이 둘째에게 얼마나 미안했는지 충분히 말하고 안아주겠습니다."
3. "병원 일정표를 함께 보며 왜 가족 시간이 줄었는지 자세히 설명하겠습니다."
```

Choice 1 reaction:

```text
둘째: 언니가 걱정돼요. 근데 집에 오면 아무도 제 하루를 안 물어봐서... 좀 외로워요.

어머니: 너는 언니를 미워하는 게 아니라, 네 하루도 봐달라는 말이구나.

아버지: 네가 외롭다는 말이 치료를 방해하는 말은 아니구나. 내가 그걸 구분하지 못했습니다.

둘째: 그렇게 말하니까 제가 나쁜 애 같지는 않아요.

김연주: 일치형 대화입니다. 둘째가 두 마음을 같이 말했고, 부모는 사과로 덮지 않고 들은 마음을 반영했습니다.
```

Effect: `two_feelings_coexist +2`, `parent_reflects_without_apology +2`, `family_sculpture_repositioned +1`, `iceberg_layers_named +1`, `sibling_feeling_named +1`.

Choice 2 reaction:

```text
어머니: 엄마가 너무 미안해. 네가 얼마나 혼자였을지 생각하면...

둘째: 엄마, 울지 마세요. 저 괜찮아요. 그냥 말한 거예요.

아버지: 나도 미안하다는 말밖에 안 나오는데, 아이가 우리를 달래고 있습니다.

상담자: 사과가 필요할 때도 있지만, 지금은 사과가 둘째의 감정을 다시 밀어냈습니다.

김연주: 죄책감이 방을 차지했습니다. 둘째의 문장은 아직 끝나지 않았습니다.
```

Effect: `parent_guilt_flooded +2`, `apology_loop_reinforced +1`, C drift.

Choice 3 reaction:

```text
아버지: 이 일정 때문에 우리가 늦게 오고, 전화가 많고, 병원에 자주 가야 합니다.

둘째: 알아요. 언니가 아프니까요. 저는 이해해요.

어머니: 또 이해한다는 말로 돌아갔네요.

상담자: 설명은 정확하지만, 둘째의 외로움은 설명되지 않았습니다.

김연주: 정보가 감정을 대신했습니다. 치료 현실은 유지하되 감정 문장을 잃으면 안 됩니다.
```

Effect: `treatment_explanation_overused +2`, `illness_totalized +1`, B drift.

### `illness_totalizing` T4: 치료 현실과 둘째 감정 같이 두기

T4 선택지:

```text
1. "치료 일정표는 접어두지 않되, 지금 3분은 둘째의 하루와 감정만 듣겠습니다."
2. "둘째가 불안하지 않도록 치료 상황을 더 자세히 공유하겠습니다."
3. "치료가 안정될 때까지 둘째 감정 대화는 짧게 확인만 하겠습니다."
```

Choice 1 reaction:

```text
상담자: 치료 일정표는 여기 그대로 둡니다. 다만 3분 동안은 학교가방을 앞에 놓겠습니다.

둘째: 오늘 미술 시간에 그린 게 있었는데, 보여주려다가 그냥 넣었어요.

어머니: 네가 보여주려던 걸 내가 보지 못했구나. 지금 보여줄 수 있으면 보고 싶어.

아버지: 치료 일정을 접는 게 아니라, 잠깐 옆에 놓는 연습이 필요하겠습니다.

김연주: 수리 방향입니다. 질병 현실을 부정하지 않고도 둘째의 빙산을 볼 수 있습니다.
```

Effect: `repaired_at_t4 = true`, `illness_reality_kept +1`, `sibling_feeling_named +1`, `parent_reflects_without_apology +1`.

Choice 2 reaction:

```text
아버지: 내일 검사가 있고, 다음 주에는 결과가 나옵니다. 그래서 우리가 계속 긴장해 있는 겁니다.

둘째: 네. 그러면 제가 더 조용히 있으면 되겠네요.

어머니: 설명을 들을수록 아이가 자기 자리를 더 접는 것 같습니다.

상담자: 불안을 줄이려는 설명이 둘째에게는 "내 얘기는 나중"이라는 신호가 됐습니다.

김연주: 병원이 가족 언어를 독점했습니다. B 경로가 강화됩니다.
```

Effect: `treatment_explanation_overused +2`, `illness_totalized +2`, B.

Choice 3 reaction:

```text
상담자: 짧게 확인만 하면 현실적으로 편할 수 있습니다. 그런데 둘째는 무엇을 배우게 될까요.

둘째: 제가 길게 말하면 안 되는 거죠. 알겠어요.

어머니: 그 말이 제일 무섭습니다. 아이가 또 알아서 접었습니다.

아버지: 안정될 때까지 기다리자는 말은 끝이 없을 수도 있겠네요.

김연주: 감정 대화가 무기한 연기됐습니다. 가족은 선하지만 둘째는 다시 사라집니다.
```

Effect: `sibling_need_postponed +2`, `sibling_self_erases +1`, B/Low.

### `parent_guilt_flood` T4: 죄책감을 옆 의자에 앉히기

T4 선택지:

```text
1. "부모님 죄책감은 옆에 두고, 둘째가 어른을 달래지 않도록 부모가 한 문장만 반영하겠습니다."
2. "부모님이 둘째에게 충분히 사과하고, 그동안 얼마나 미안했는지 말하겠습니다."
3. "둘째가 부모님도 힘들었다는 점을 이해한다고 말해보겠습니다."
```

Choice 1 reaction:

```text
상담자: 어머니의 미안함은 이 옆 의자에 잠시 둡니다. 지금은 둘째 말을 한 문장으로만 돌려주세요.

어머니: 너는 엄마를 원망하려는 게 아니라, 네 하루도 물어봐 달라는 말이구나.

둘째: 네. 엄마를 울리려고 한 건 아니에요.

아버지: 나도 미안하다는 말보다 먼저 들어야겠네요. 네가 우리를 달래지 않아도 되게요.

김연주: 좋은 수리입니다. 부모 죄책감이 사라진 게 아니라, 아이를 덮지 않게 조절됐습니다.
```

Effect: `repaired_at_t4 = true`, `parent_regulation_score +2`, `parent_reflects_without_apology +1`, `sibling_parentifies -1`.

Choice 2 reaction:

```text
어머니: 엄마가 너무 많이 놓쳤어. 네가 얼마나 외로웠을지 생각하면 내가 견딜 수가 없어.

둘째: 아니에요. 엄마도 힘들었잖아요. 제가 괜찮다고 했잖아요.

아버지: 아이가 또 우리를 위로하고 있습니다.

상담자: 사과가 길어지면서 둘째가 다시 부모를 보호하는 자리로 갔습니다.

김연주: C 경로입니다. 죄책감이 둘째의 빙산을 덮었습니다.
```

Effect: `parent_guilt_flooded +2`, `apology_loop_reinforced +2`, C.

Choice 3 reaction:

```text
상담자: 부모님의 고통을 이해하라는 말은 조심해야 합니다. 둘째가 이미 너무 많이 이해하고 있습니다.

둘째: 네. 엄마 아빠가 더 힘든 거 알아요. 저는 괜찮아요.

어머니: 그 말을 듣고 싶었던 게 아닌데, 또 그렇게 만들었네요.

아버지: 우리가 위로받는 쪽으로 바뀌었습니다.

김연주: 아이가 부모의 조절자가 됐습니다. 죄책감과 성숙한 아이 역할이 같이 강화됩니다.
```

Effect: `sibling_parentifies +2`, `cheerful_mask_reinforced +1`, C/D low.

### `cheerful_mask` T4: 강함과 외로움 같이 두기

T4 선택지:

```text
1. "잘 버틴 점과 외로운 점을 동시에 놓겠습니다. 둘째는 강해서 괜찮은 아이가 아니라, 강하면서도 외로운 아이일 수 있습니다."
2. "둘째가 가족을 위해 해준 일을 부모님이 더 많이 인정하고 칭찬하겠습니다."
3. "조부모가 둘째의 정서 대화를 맡아 부모 부담을 줄이겠습니다."
```

Choice 1 reaction:

```text
상담자: 너는 잘 버틴 아이이기도 하고, 외로운 아이이기도 합니다. 둘 중 하나만 고르지 않아도 됩니다.

둘째: 그럼 제가 잘 못 버틴 날도 말해도 돼요?

어머니: 그래. 잘 버틴 날만 네가 우리 아이인 건 아니야.

아버지: 대견하다는 말이 부탁처럼 들리지 않게 조심하겠습니다.

김연주: 좋은 Satir 개입입니다. 생존 자세를 존중하면서도 그 아래 감정을 허락했습니다.
```

Effect: `repaired_at_t4 = true`, `two_feelings_coexist +2`, `cheerful_mask_reinforced -1`, `sibling_feeling_named +1`.

Choice 2 reaction:

```text
아버지: 네가 버텨줘서 정말 고맙다. 너 때문에 우리가 버틸 수 있었어.

둘째: 그럼 앞으로도 잘해야겠네요.

어머니: 고맙다는 말이 아이에게 짐이 되는 게 보입니다.

상담자: 칭찬이 강점 확인이 아니라 역할 고정이 됐습니다.

김연주: 좋은 아이 가면이 더 단단해졌습니다. D 경로입니다.
```

Effect: `cheerful_mask_reinforced +2`, `maturity_praised_as_role +2`, D.

Choice 3 reaction:

```text
조부모/도움 인물: 제가 더 자주 물어볼 수는 있습니다. 학교 얘기도 들어주고요.

둘째: 할머니한테 말하면 엄마 아빠는 병원에만 있어도 되는 거예요?

어머니: 그런 뜻은 아니었는데, 네가 그렇게 들을 수 있겠구나.

상담자: 도움 인물은 필요하지만, 부모의 직접 듣기를 대체하면 둘째는 또 부모 밖으로 밀립니다.

김연주: 돌봄 분담이 정서 대체가 되면 낮은 경로입니다.
```

Effect: `support_relative_substitution +2`, `sibling_need_postponed +1`, Low/D.

## T5. Final Ritual Confirmation

T5 선택지:

```text
1. "다음 주에는 병원 일정과 별개로 둘째에게만 묻는 10분을 정하겠습니다. 그 시간에는 첫째 치료 보고가 아니라 둘째의 하루, 감정, 필요한 도움만 듣고, 실패하면 누구 잘못인지 따지지 않고 다시 시간을 정합니다."
2. "부모님은 둘째가 불안하지 않게 첫째 치료 상황을 매일 자세히 설명하겠습니다."
3. "부모님은 둘째에게 미안함과 고마움을 충분히 표현하고, 둘째가 이해해준 점을 인정하겠습니다."
4. "부모님 부담이 크니 조부모가 둘째의 마음 대화를 맡고 부모는 치료에 집중하겠습니다."
```

Choice 1 reaction:

```text
어머니: 10분이면 할 수 있을 것 같습니다. 그 시간에는 병원 얘기로 돌리지 않겠습니다.

아버지: 실패하면 내가 또 놓쳤다고 무너지지 않고, 시간을 다시 정하겠습니다.

둘째: 그 시간에는 학교에서 있었던 얘기도 해도 돼요?

조부모/도움 인물: 제가 그 10분 동안 집안일을 맡겠습니다. 대신 아이 마음을 부모 대신 듣는 자리는 만들지 않겠습니다.

김연주: 좋은 마무리입니다. 감정 확인이 구체적 의식으로 이어졌습니다.
```

Effect: `final_confirm_sibling_ritual = true`, `sibling_time_scheduled +2`, `parent_reflects_without_apology +1`.

Choice 2 reaction:

```text
아버지: 매일 치료 상황을 설명하면 아이가 덜 불안할 것 같습니다.

둘째: 네. 그러면 제가 더 이해하면 되겠네요.

어머니: 또 아이가 자기 얘기를 접고 이해하는 아이가 됐습니다.

김연주: 치료 보고가 둘째 시간을 대체했습니다. 최종 B 트랩입니다.
```

Effect: `final_confirm_treatment_briefing_trap = true`, `treatment_explanation_overused +1`, `illness_totalized +1`.

Choice 3 reaction:

```text
어머니: 미안하고 고마워. 네가 이해해줘서 우리가 버텼어.

둘째: 네. 저 괜찮아요. 앞으로도 잘할게요.

아버지: 고마움이 또 부탁처럼 들렸습니다.

김연주: 사과와 칭찬이 둘째의 가면을 강화했습니다. 최종 C 트랩입니다.
```

Effect: `final_confirm_apology_trap = true`, `apology_loop_reinforced +1`, `cheerful_mask_reinforced +1`.

Choice 4 reaction:

```text
조부모/도움 인물: 제가 아이 이야기를 더 들어보겠습니다.

둘째: 그러면 엄마 아빠한테 말하지 않아도 되는 거죠?

어머니: 도움은 필요하지만, 우리가 빠지는 건 아니어야겠네요.

김연주: 정서적 부모 자리가 대체됐습니다. 최종 외주화 트랩입니다.
```

Effect: `final_confirm_outsource_trap = true`, `support_relative_substitution +1`, `sibling_need_postponed +1`.

## Ending Scenes

### Ending A: The Well Child Has A Place

Conditions:

```text
sibling_time_scheduled >= 2
two_feelings_coexist >= 2
parent_reflects_without_apology >= 1
iceberg_layers_named >= 1
illness_totalized < 2
parent_guilt_flooded < 2
cheerful_mask_reinforced < 2
```

```text
둘째: 언니가 걱정되는 마음이랑 제가 외로운 마음을 같이 말해도 되는 것 같아요.

어머니: 10분 동안은 네 하루를 듣겠습니다. 미안하다고만 하지 않고, 네 말을 끝까지 들을게요.

아버지: 병원 가방 옆에 네 학교가방도 같이 둘게. 둘 다 우리 가족 이야기니까.

김연주: 좋은 회기입니다. 질병을 부정하지 않으면서도 둘째의 자리를 다시 보이게 만들었습니다.
```

### Ending A-Repaired: The Backpack Opens Late

```text
둘째: 처음에는 또 병원 얘기만 하는 줄 알았는데, 제 얘기도 잠깐 할 수 있었어요.

어머니: 늦었지만 네 가방을 보겠습니다. 오늘은 네가 뭘 가지고 왔는지 듣고 싶어.

김연주: 완벽한 회기는 아니지만 수리되었습니다. 가족이 치료 현실과 둘째 마음을 동시에 놓기 시작했습니다.
```

### Ending B: Illness Owns The Family

```text
둘째: 저는 이해해요. 언니 치료가 먼저니까요.

아버지: 설명은 더 잘한 것 같은데, 아이 표정이 더 조용해졌습니다.

김연주: 치료 현실은 존중됐지만 질병이 가족 언어를 소유했습니다. 둘째의 가방은 다시 닫혔습니다.
```

### Ending C: Guilt Takes The Room

```text
어머니: 미안하다는 말밖에 못 하겠습니다. 내가 너무 놓쳤습니다.

둘째: 엄마 울지 마세요. 저 괜찮아요.

김연주: 부모 죄책감이 장면을 덮었습니다. 둘째가 다시 부모를 달래는 자리로 갔습니다.
```

### Ending C-Repaired: Guilt On The Side Chair

```text
어머니: 미안함은 있지만, 그 말로 네 말을 막지 않겠습니다.

둘째: 엄마가 안 울려고 하니까 저도 끝까지 말할 수 있을 것 같아요.

김연주: 부모의 죄책감이 사라진 것이 아니라 조절됐습니다. 그래서 둘째가 부모를 돌보지 않아도 됐습니다.
```

### Ending D: Good Child Trap

```text
둘째: 제가 잘 버틴 거면 앞으로도 잘해야 할 것 같아요.

아버지: 대견하다는 말이 아이에게 더 큰 짐이 됐습니다.

김연주: 강점은 보였지만 외로움은 충분히 말해지지 않았습니다. 좋은 아이 가면이 강화됐습니다.
```

### Ending D-Repaired: Strong And Lonely

```text
둘째: 잘한 날도 있고, 싫은 날도 있다고 말해도 되는 거죠?

어머니: 그래. 네가 강하다는 말이 네가 안 외롭다는 뜻은 아니야.

김연주: 생존 자세를 존중하면서도 그 아래 감정을 허락했습니다.
```

### Low-Outsourced: The Key Ring Takes Over

```text
조부모/도움 인물: 제가 아이를 더 챙기겠습니다.

둘째: 엄마 아빠는 계속 병원에 있어도 되는 거죠?

김연주: 도움은 늘었지만 부모의 직접 듣기는 대체됐습니다. 정서적 자리는 아직 비어 있습니다.
```

### Low Ending: Okay Means Invisible

```text
둘째: 괜찮아요. 그냥 지금처럼 하면 돼요.

어머니: 그 말이 편하지 않고 무섭습니다.

김연주: 가족 모두 선의가 있지만 구조는 그대로입니다. 괜찮다는 말이 다시 문을 닫았습니다.
```

## Implementation Notes

- Use `Docs/FT006_SATIR_ILLNESS_SIBLING_BRANCHING_LOCK_V2_2026-06-10.md` as ending resolver authority.
- Do not infer endings from route count only.
- The sick child is loved and real, but does not need to appear as a blaming target.
- The strongest scenes show parents resisting the impulse to explain, apologize, or praise too quickly.
