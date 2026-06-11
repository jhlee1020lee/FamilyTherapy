# FT-008 Narrative Branching Lock V2

## Status

This V2 supersedes the branching portions of:

```text
Docs/FT008_NARRATIVE_SCHOOL_VIOLENCE_MAJOR_BRANCHING_SCENARIO_2026-06-10.md
Docs/FT008_REALISTIC_DIALOGUE_EXPANSION_2026-06-10.md
```

The original common intro remains usable. V2 is the production authority for T3, T4, T5, state, ending lock, and CG planning.

## Core Narrative Question

FT-008 is not about forcing the teen to disclose school-violence details or proving whether transfer is right.

The central question is:

```text
When the incident tries to rename the teen, can the family name the problem outside the teen and collect moments when the problem was weaker?
```

## Recurring Visual Anchor

```text
school notice file beside an untouched dinner bowl and the teen's face-down phone
```

Supporting objects:

- family dinner table with one untouched bowl;
- school procedure file with stamped papers;
- transfer application form left blank;
- teen's phone with one supportive friend message;
- father's folded transfer pros/cons note;
- mother's serving spoon paused over the bowl;
- sticky note titled "problem's influence / moments it was weaker";
- supervisor notebook with no incident details written, only the problem's provisional name.

The anchor changes meaning by route:

| Route | Anchor Meaning |
| --- | --- |
| `externalized_harm` | file, bowl, and phone show the problem's influence without making the teen the problem |
| `silence_protection` | untouched bowl stays quiet, but the teen remains alone |
| `endurance_story` | transfer form becomes a battlefield about winning/losing |
| `procedure_closure` | procedure file covers the phone and bowl |

## State Model

```text
route_primary:
  externalized_harm
  silence_protection
  endurance_story
  procedure_closure

flags:
  problem_externalized
  silence_influence_mapped
  endurance_story_deconstructed
  teen_identity_separated
  problem_name_teen_authored
  unique_outcome_found
  unique_outcome_thickened
  preferred_identity_named
  family_witness_response
  school_procedure_kept_as_tool
  school_support_with_teen_consent
  outsider_witness_consent_obtained
  no_forced_disclosure
  problem_weak_moment_task
  repaired_at_t4
  repair_started

  silence_as_protection_reinforced
  teen_isolated_by_silence
  endurance_story_reinforced
  transfer_as_defeat_framed
  forced_disclosure_attempted
  procedure_totalized
  teen_as_case_file
  decision_pressure_high
  parent_reassurance_overwrites
  school_voice_over_takes

  final_confirm_problem_observation
  final_confirm_transfer_decision_trap
  final_confirm_disclosure_trap
  final_confirm_procedure_trap

scores:
  externalization_score
  reauthoring_score
  witness_score
  safety_respect_score
  dominant_story_risk
```

## Turn Lock

| Turn | Function | Must Not Collapse |
| --- | --- | --- |
| T1 | route entry | defines issue as externalized problem, protective silence, endurance story, or procedure case |
| T2 | influence map | shows what silence/endurance/procedure makes each person do |
| T3 | route-specific dominant story pressure | route pressure changes whether teen is separated from problem |
| T4 | playable externalization/unique outcome turn | three choices per route with visible family re-response |
| T5 | final observation task confirmation | ending resolver uses flags and final traps |

## T3 Choice Consequences

### `externalized_harm`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3A-1 map problem influence and exceptions | `problem_externalized +1`, `silence_influence_mapped +1`, `problem_name_teen_authored +1`, `no_forced_disclosure +1` | opens A |
| T3A-2 decide transfer now | `decision_pressure_high +1`, `transfer_as_defeat_framed +1` | drifts C |
| T3A-3 ask incident details for clarity | `forced_disclosure_attempted +2`, `teen_as_case_file +1` | D/Low |

### `silence_protection`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3B-1 distinguish rest-silence from isolation-silence | `repair_started +1`, `silence_influence_mapped +1`, `no_forced_disclosure +1` | repair possible |
| T3B-2 keep school talk off limits | `silence_as_protection_reinforced +2`, `teen_isolated_by_silence +2` | locks B |
| T3B-3 parents reassure teen nothing has changed | `parent_reassurance_overwrites +2`, `teen_isolated_by_silence +1` | B low |

### `endurance_story`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3C-1 deconstruct endure-or-lose story | `repair_started +1`, `endurance_story_deconstructed +1`, `teen_identity_separated +1` | repair possible |
| T3C-2 build staying-at-school plan first | `endurance_story_reinforced +2`, `transfer_as_defeat_framed +2` | locks C |
| T3C-3 ban father from saying endure | `parent_reassurance_overwrites +1`, `decision_pressure_high +1` | C/B low |

### `procedure_closure`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3D-1 keep procedure as tool, recover teen voice | `repair_started +1`, `school_procedure_kept_as_tool +1`, `teen_identity_separated +1` | repair possible |
| T3D-2 finish procedure facts before feelings | `procedure_totalized +2`, `teen_as_case_file +2` | locks D |
| T3D-3 ask school rep to explain options for teen | `school_voice_over_takes +1`, `procedure_totalized +1` | D low |

## T4 Playable Choice Lock

### T4A `externalized_harm`: Problem Name And Unique Outcome

```text
A1. "사건을 자세히 말하지 않고, '학교 이름이 나오면 몸을 얼게 하는 것'이라는 임시 이름을 붙인 뒤 그것이 약했던 순간을 찾겠습니다."
A2. "전학 여부를 오늘 잠정 결정해서 청소년의 불안을 줄이겠습니다."
A3. "다음 이야기를 만들려면 사건을 한 번 정리해서 말해야 하니, 가능한 만큼 자세히 말해보겠습니다."
```

Effects:

- A1: `problem_externalized +2`, `unique_outcome_found +1`, `unique_outcome_thickened +1`, `preferred_identity_named +1`, `no_forced_disclosure +1`, high A candidate.
- A2: `decision_pressure_high +2`, `transfer_as_defeat_framed +1`, C drift.
- A3: `forced_disclosure_attempted +2`, `teen_as_case_file +1`, D/Low.

### T4B `silence_protection`: Silence As Rest Or Isolation

```text
B1. "학교 이야기를 강제로 꺼내지 않되, 식탁의 침묵이 쉬게 하는 침묵인지 혼자 두는 침묵인지 한 번만 확인하겠습니다."
B2. "가족 안정이 우선이니 당분간 학교 이야기는 식탁에서 하지 않겠습니다."
B3. "부모님이 평소처럼 지내도 된다고 자주 말해 청소년을 안심시키겠습니다."
```

Effects:

- B1: `repaired_at_t4 = true`, `silence_influence_mapped +2`, `no_forced_disclosure +1`, repair toward A-Repaired.
- B2: `silence_as_protection_reinforced +2`, `teen_isolated_by_silence +2`, B.
- B3: `parent_reassurance_overwrites +2`, `teen_isolated_by_silence +1`, B low.

### T4C `endurance_story`: Deconstruct Endure Or Lose

```text
C1. "'버티면 이긴다'는 이야기가 가족을 어떻게 도왔고, 청소년에게 어떤 비용을 만들었는지 보겠습니다. 전학은 승패가 아니라 안전과 회복의 선택지로 놓겠습니다."
C2. "전학이 도망처럼 남지 않도록 우선 학교에 더 버틸 방법을 계획하겠습니다."
C3. "아버지가 버티라는 말을 하지 않겠다고 약속하면 청소년이 덜 힘들 수 있습니다."
```

Effects:

- C1: `repaired_at_t4 = true`, `endurance_story_deconstructed +2`, `teen_identity_separated +1`, `transfer_as_defeat_framed -1`, repair toward C-Repaired/A.
- C2: `endurance_story_reinforced +2`, `transfer_as_defeat_framed +2`, C.
- C3: `parent_reassurance_overwrites +1`, `decision_pressure_high +1`, C/B low.

### T4D `procedure_closure`: Procedure As Tool, Not Identity

```text
D1. "학교 절차는 유지하되, 절차 파일을 잠시 옆에 두고 청소년이 자기 경험을 어떤 이름으로 부르고 싶은지 듣겠습니다."
D2. "절차가 정리되어야 가족도 안정될 수 있으니 학교 대응부터 끝내겠습니다."
D3. "학교 담당자가 가능한 지원과 전학 절차를 자세히 설명하면 가족 불안이 줄어들 것입니다."
```

Effects:

- D1: `repaired_at_t4 = true`, `school_procedure_kept_as_tool +2`, `teen_identity_separated +1`, `problem_externalized +1`, repair toward D-Repaired/A.
- D2: `procedure_totalized +2`, `teen_as_case_file +2`, D.
- D3: `school_voice_over_takes +2`, `procedure_totalized +1`, D low.

## T5 Final Confirmation Turn

```text
1. "다음 주에는 사건을 자세히 말하는 숙제가 아니라, 문제가 조금 약했던 순간을 기록하겠습니다. 가족은 식탁에서 침묵이 들어왔을 때 '지금 침묵이 우리를 보호하나, 혼자 두나'를 한 번만 확인하고, 학교 절차는 도구로만 다룹니다. 가족과 학교는 청소년이 동의한 범위 안에서만 그 순간을 증언하거나 반영합니다."
2. "전학 여부를 미루면 불안이 커지니, 다음 주까지 전학 찬반을 각자 정리해 결정하겠습니다."
3. "다음 이야기를 만들려면 사건을 정리해야 하니, 청소년은 다음 주까지 사건 내용을 가능한 만큼 적어오겠습니다."
4. "학교 절차가 끝나야 안정될 수 있으니, 가족 대화보다 절차 진행과 지원 문서를 우선하겠습니다."
```

Effects:

- 1: `final_confirm_problem_observation = true`, `problem_weak_moment_task +2`, `family_witness_response +1`, `school_support_with_teen_consent +1`, `outsider_witness_consent_obtained +1`, `no_forced_disclosure +1`.
- 2: `final_confirm_transfer_decision_trap = true`, `decision_pressure_high +1`, `transfer_as_defeat_framed +1`.
- 3: `final_confirm_disclosure_trap = true`, `forced_disclosure_attempted +2`, `teen_as_case_file +1`.
- 4: `final_confirm_procedure_trap = true`, `procedure_totalized +1`, `school_voice_over_takes +1`.

## T5 Ending Resolver

Use this resolver exactly:

```text
if final_confirm_transfer_decision_trap == true:
    choose C
elif final_confirm_disclosure_trap == true:
    choose D
elif final_confirm_procedure_trap == true:
    choose D
elif problem_weak_moment_task >= 2
     and problem_externalized >= 2
     and unique_outcome_found >= 1
     and unique_outcome_thickened >= 1
     and problem_name_teen_authored >= 1
     and school_support_with_teen_consent >= 1
     and no_forced_disclosure >= 1
     and forced_disclosure_attempted < 2
     and teen_isolated_by_silence < 2
     and endurance_story_reinforced < 2
     and procedure_totalized < 2:
    choose A
elif route_primary == endurance_story
     and repaired_at_t4 == true
     and endurance_story_deconstructed >= 2
     and problem_weak_moment_task >= 1
     and no_forced_disclosure >= 1
     and transfer_as_defeat_framed < 2
     and endurance_story_reinforced < 2
     and parent_reassurance_overwrites < 2
     and forced_disclosure_attempted < 2:
    choose C-Repaired
elif route_primary == procedure_closure
     and repaired_at_t4 == true
     and school_procedure_kept_as_tool >= 2
     and problem_weak_moment_task >= 1
     and no_forced_disclosure >= 1
     and teen_as_case_file < 2
     and procedure_totalized < 2
     and school_voice_over_takes < 2
     and forced_disclosure_attempted < 2:
    choose D-Repaired
elif route_primary == silence_protection
     and repaired_at_t4 == true
     and problem_weak_moment_task >= 1
     and no_forced_disclosure >= 1
     and silence_as_protection_reinforced < 2
     and teen_isolated_by_silence < 2
     and parent_reassurance_overwrites < 2
     and endurance_story_reinforced < 2
     and transfer_as_defeat_framed < 2
     and procedure_totalized < 2
     and forced_disclosure_attempted < 2:
    choose A-Repaired
elif silence_as_protection_reinforced >= 2
     or teen_isolated_by_silence >= 2
     or parent_reassurance_overwrites >= 2:
    choose B
elif endurance_story_reinforced >= 2
     or transfer_as_defeat_framed >= 2
     or decision_pressure_high >= 2:
    choose C
elif procedure_totalized >= 2
     or teen_as_case_file >= 2
     or school_voice_over_takes >= 2
     or forced_disclosure_attempted >= 2:
    choose D
else:
    choose Low
```

Resolver regression examples:

```text
T3A-1 -> T4A-1 -> T5-1 = A
Reason: problem is externalized, unique outcome is found, and observation task is confirmed without forced disclosure.

T3B-1 -> T4B-2 -> T5-1 = B
Reason: early repair attempt is overridden by reinforced protective silence.

T3C-1 -> T4C-2 -> T5-1 = C
Reason: endurance story is named but later reinstalled as a staying plan.

T3D-1 -> T4D-2 -> T5-1 = D
Reason: procedure starts as tool but later totalizes the story.

T3A-1 -> T4A-3 -> T5-1 = D
Reason: forced disclosure attempt blocks the high externalized route.

T3C-1 -> T4C-1 -> T5-1 = C-Repaired
Reason: endurance story is deconstructed and no later defeat framing or forced disclosure overrides it.

T3D-1 -> T4D-1 -> T5-1 = D-Repaired
Reason: procedure is kept as a tool and does not totalize the teen's identity.

T3C-2 -> T4C-1 -> T5-1 = C
Reason: a late C1 repair cannot erase a strongly reinforced endure-or-lose story.

T3B-3 -> T4B-1 -> T5-1 = B
Reason: a late B1 repair cannot erase parental reassurance that already overwrote the teen's experience.
```

## Endings

| Ending | Required Conditions | Blocking Flags | Final Aftereffect |
| --- | --- | --- | --- |
| A. The Problem Gets A Name | teen-authored problem name + externalized problem + thickened unique outcome + observation task + teen-consented school support | forced disclosure, silence isolation, endurance/procedure traps | teen is separated from the problem and family observes the problem's influence |
| A-Repaired. Silence Becomes Observable | silence_protection route repaired at T4 plus problem observation | reinforced silence, isolation, parental overwrite, endurance/procedure traps | family recovers enough to track problem-weak moments without pretending the silence was harmless |
| B. Protected Into Isolation | protective silence dominates | no B1 repair | table stays calm but teen remains alone |
| C. Endure Or Lose | endurance story or transfer-as-defeat dominates | no C1 repair | transfer/help remains trapped in winning/losing story |
| C-Repaired. Choice Is Not Defeat | endurance_story route repaired at T4 plus deconstructed endurance story | defeat framing, reinforced endurance, parental overwrite | transfer becomes one safety/recovery option, not proof of weakness |
| D. Case File Without Voice | procedure or disclosure dominates | no D1 repair | teen becomes case file again |
| D-Repaired. Procedure As Tool | procedure_closure route repaired at T4 plus procedure kept as tool | case-file totalization, school voice takeover, forced disclosure | school process continues without taking over identity |
| Low. Same Bowl, Same Silence | no strong repair | none | untouched bowl and face-down phone remain unchanged |

## Route-Specific CG Lock

Minimum commercial-comfortable FT-008 image set:

| ID | Scene |
| --- | --- |
| FT008_CG_01 | school notice file beside untouched dinner bowl and face-down phone |
| FT008_CG_02 | dinner table silence, mother's serving spoon paused |
| FT008_CG_03 | father's folded transfer pros/cons note |
| FT008_CG_04 | school procedure file covering teen's phone |
| FT008_CG_05 | teen phone implying a supportive friend message or notification, with no readable characters or legible chat UI text |
| FT008_CG_06 | externalized_harm route, family looking at "silence" as problem outside teen |
| FT008_CG_07 | silence_protection route, calm table with teen isolated |
| FT008_CG_08 | endurance_story route, transfer form treated like win/lose paper |
| FT008_CG_09 | procedure_closure route, school representative and file dominate frame |
| FT008_CG_10 | T4A high, problem provisional name in supervisor notebook without incident details |
| FT008_CG_11 | T4B low, school talk banned and bowl remains untouched |
| FT008_CG_12 | T4C low, father pushes staying plan over transfer form |
| FT008_CG_13 | T4D low, procedure file covers phone and bowl |
| FT008_CG_14 | T4D repaired, procedure file moved aside while teen names experience |
| FT008_CG_15 | T5 high, blank sticky note or clean note area for later in-engine overlay text |
| FT008_CG_16 | Ending A, phone turned up beside bowl, family observing not interrogating |
| FT008_CG_17 | Ending A-Repaired, silence becomes observable at table |
| FT008_CG_18 | Ending B, calm meal with teen visually alone |
| FT008_CG_19 | Ending C, transfer form framed as defeat |
| FT008_CG_20 | Ending C-Repaired, transfer form beside safety/recovery options |
| FT008_CG_21 | Ending D, teen reduced to procedure file |
| FT008_CG_22 | Ending D-Repaired, procedure file as tool beside teen's own note |
| FT008_CG_23 | Low ending, same untouched bowl and face-down phone |
| FT008_CG_24 | Outsider witness-style family reflection without forced disclosure |

All CGs must be 1600x900, no baked text, no UI, bottom 25-30% clean.

## Gate Requirement

FT-008 cannot proceed to FT-009 until V2 passes:

- narrative therapy fidelity review;
- game branching/consequence review;
- commercial VN dialogue review.
