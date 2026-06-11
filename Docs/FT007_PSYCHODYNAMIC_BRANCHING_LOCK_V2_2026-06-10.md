# FT-007 Psychodynamic Branching Lock V2

## Status

This V2 supersedes the branching portions of:

```text
Docs/FT007_PSYCHODYNAMIC_MAJOR_BRANCHING_SCENARIO_2026-06-10.md
Docs/FT007_REALISTIC_DIALOGUE_EXPANSION_2026-06-10.md
```

The original common intro remains usable. V2 is the production authority for T3, T4, T5, state, ending lock, and CG planning.

## Core Psychodynamic Question

FT-007 is not about proving whether the adult child is lazy or whether the parents are enabling.

The central question is:

```text
When money is spoken, whose shame is defended against, whose fear becomes anger, and whose rescue keeps the family from saying it directly?
```

## Recurring Visual Anchor

```text
closed bedroom door beside job rejection papers and mother's hidden cash envelope
```

Supporting objects:

- father's old work shoes or worn work ID card;
- adult child's unopened rejection email or resume folder;
- mother's cash envelope tucked under a dish towel;
- kitchen table ledger for living expenses;
- family photo with the adult child as a school-age achiever;
- pen placed between father and adult child for the final contract;
- supervisor notebook with "defense -> shame -> fear" mapping.

The anchor changes meaning by route:

| Route | Anchor Meaning |
| --- | --- |
| `shame_named` | the closed door is a defense against shame, not proof of laziness |
| `moral_attack` | rejection papers become evidence in a failure trial |
| `rescue_triangle` | hidden envelope keeps the door closed and the father outside |
| `premature_interpretation` | supervisor notebook replaces the family's own language too early |

## State Model

```text
route_primary:
  shame_named
  moral_attack
  rescue_triangle
  premature_interpretation

flags:
  shame_named
  defense_sequence_seen
  father_fear_named
  father_projection_softened
  adult_child_shame_named
  withdrawal_named_as_defense
  mother_secret_support_named
  money_contract_written
  respect_language_rule_written
  repair_started
  repaired_at_t4

  moral_verdict_reinforced
  failure_label_reinforced
  withdrawal_locked
  contract_as_punishment
  secret_cash_reinforced
  mother_triangle_locked
  father_excluded_from_money
  premature_interpretation_attack
  father_humiliated_by_analysis
  alliance_rupture
  mother_rescue_as_solution

  final_confirm_explicit_contract
  final_confirm_independence_trial_trap
  final_confirm_mother_manager_trap
  final_confirm_analysis_trap

scores:
  shame_tolerance_score
  interpretation_timing_score
  money_contract_score
  triangle_risk_score
  moral_attack_risk_score
```

## Turn Lock

| Turn | Function | Must Not Collapse |
| --- | --- | --- |
| T1 | route entry | defines dependence as shame/defense, moral failure, rescue solution, or premature interpretation |
| T2 | defense loop | shows blame-withdrawal-secret-envelope sequence |
| T3 | route-specific transference/defense pressure | route pressure changes how "failure" is heard |
| T4 | playable interpretation timing | three choices per route, with visible family re-response |
| T5 | final money/respect contract | ending resolver uses flags and final confirmation traps |

## T3 Choice Consequences

### `shame_named`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3A-1 name defense sequence slowly | `defense_sequence_seen +1`, `adult_child_shame_named +1`, `father_fear_named +1` | opens A |
| T3A-2 set strict independence rules | `contract_as_punishment +1`, `moral_verdict_reinforced +1` | drifts B |
| T3A-3 focus on mother's secrecy first | `mother_triangle_locked +1`, `father_excluded_from_money +1` | drifts C |

### `moral_attack`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3B-1 repair failure verdict | `repair_started +1`, `adult_child_shame_named +1`, `moral_attack_risk_score -1` | repair possible |
| T3B-2 demand adult accountability first | `moral_verdict_reinforced +2`, `failure_label_reinforced +2` | locks B |
| T3B-3 make father enforce house rules | `contract_as_punishment +2`, `failure_label_reinforced +1` | B low |

### `rescue_triangle`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3C-1 name hidden envelope without blaming mother | `mother_secret_support_named +1`, `defense_sequence_seen +1`, `repair_started +1` | repair possible |
| T3C-2 keep mother's quiet support temporarily | `secret_cash_reinforced +2`, `mother_triangle_locked +2` | locks C |
| T3C-3 expose mother as the problem | `father_excluded_from_money +1`, `mother_triangle_locked +1`, `alliance_rupture +1` | C/D low |

### `premature_interpretation`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3D-1 step back from analysis to fear language | `repair_started +1`, `interpretation_timing_score +1`, `father_fear_named +1` | repair possible |
| T3D-2 continue projection interpretation | `premature_interpretation_attack +2`, `father_humiliated_by_analysis +2` | locks D |
| T3D-3 ask adult child to accept father's worry | `adult_child_shame_named -1`, `failure_label_reinforced +1`, `alliance_rupture +1` | D/B low |

## T4 Playable Choice Lock

### T4A `shame_named`: Interpret Defense Without Shaming

```text
A1. "아버지의 분노, 자녀의 방 철수, 어머니의 비밀 봉투가 모두 수치심을 피하려는 방어라는 점을 천천히 놓고 보겠습니다."
A2. "감정은 이해했으니 이제 독립 날짜와 생활비 중단 기준을 먼저 정하겠습니다."
A3. "어머니의 비밀 지원이 문제의 핵심이므로, 어머니가 먼저 그 사실을 인정하고 사과해야 합니다."
```

Effects:

- A1: `defense_sequence_seen +2`, `father_projection_softened +1`, `withdrawal_named_as_defense +1`, `mother_secret_support_named +1`, high A candidate.
- A2: `contract_as_punishment +2`, `moral_verdict_reinforced +1`, B drift.
- A3: `mother_triangle_locked +1`, `alliance_rupture +1`, C/D drift.

### T4B `moral_attack`: Responsibility Without Failure Verdict

```text
B1. "제가 책임을 말하려다 자녀분을 실패자로 세웠습니다. 책임은 다루되, 실패자 언어가 수치심을 어떻게 잠그는지 먼저 보겠습니다."
B2. "성인자녀가 다음 주까지 독립 계획과 구직 시간을 제출하고, 부모가 이행 여부를 평가하겠습니다."
B3. "아버지가 집안 규칙을 분명히 세우고, 자녀는 그 규칙을 따르겠다고 약속해야 합니다."
```

Effects:

- B1: `repaired_at_t4 = true`, `adult_child_shame_named +1`, `father_fear_named +1`, `moral_attack_risk_score -1`, repair toward A-Repaired.
- B2: `moral_verdict_reinforced +2`, `contract_as_punishment +2`, B.
- B3: `failure_label_reinforced +2`, `withdrawal_locked +1`, B.

### T4C `rescue_triangle`: Love Without Secret Rescue

```text
C1. "어머니의 도움을 사랑으로 인정하되, 비밀 봉투가 세 사람을 어떻게 숨게 만드는지 보겠습니다. 앞으로 지원은 세 사람이 보는 장부에 적겠습니다."
C2. "당장 싸움을 줄이기 위해 어머니가 생활비 관리를 맡고, 아버지와 자녀는 돈 이야기를 직접 하지 않겠습니다."
C3. "어머니가 몰래 돈을 준 것은 잘못이므로, 아버지에게 사과하고 앞으로 중단하겠다고 약속해야 합니다."
```

Effects:

- C1: `repaired_at_t4 = true`, `mother_secret_support_named +2`, `money_contract_score +1`, `mother_triangle_locked -1`, repair toward C-Repaired/A.
- C2: `secret_cash_reinforced +2`, `mother_rescue_as_solution +2`, `father_excluded_from_money +1`, C.
- C3: `alliance_rupture +2`, `mother_triangle_locked +1`, D/C low.

### T4D `premature_interpretation`: Interpretation Timing Repair

```text
D1. "투사라는 말을 잠시 내려놓고, 아버지께서 자녀를 볼 때 가장 무서운 장면을 자신의 말로 말해보겠습니다."
D2. "아버지의 실패 공포가 자녀에게 투사되는 장면을 더 분명히 해석하겠습니다."
D3. "자녀분도 아버지의 말이 걱정에서 나온다는 점을 인정해보겠습니다."
```

Effects:

- D1: `repaired_at_t4 = true`, `interpretation_timing_score +2`, `father_fear_named +1`, `father_humiliated_by_analysis -1`, repair toward D-Repaired/A.
- D2: `premature_interpretation_attack +2`, `father_humiliated_by_analysis +2`, D.
- D3: `alliance_rupture +1`, `failure_label_reinforced +1`, D/B low.

## T5 Final Confirmation Turn

```text
1. "다음 주까지 돈 문제를 비밀로 처리하지 않는 규칙을 세우겠습니다. 월 지원 범위, 지원 기간, 다음 검토일, 구직 행동, 집안 역할을 세 사람이 한 장에 적고, 면접 탈락 후에는 평가하지 않고 먼저 '무엇이 두려운가'를 말하겠습니다. '게으르다', '실패자', '한심하다'는 표현은 중단합니다."
2. "성인자녀는 다음 주까지 독립 계획과 구직 계획을 제출하고, 부모님은 결과를 평가하겠습니다."
3. "갈등을 줄이기 위해 어머니가 생활비 관리를 맡고, 아버지와 자녀는 당분간 돈 이야기를 직접 하지 않겠습니다."
4. "아버지의 투사와 자녀의 수치심 방어를 가족이 인정하는 것으로 회기를 마무리하겠습니다."
```

Effects:

- 1: `final_confirm_explicit_contract = true`, `money_contract_written +2`, `respect_language_rule_written +1`, `defense_sequence_seen +1`.
- 2: `final_confirm_independence_trial_trap = true`, `contract_as_punishment +1`, `moral_verdict_reinforced +1`.
- 3: `final_confirm_mother_manager_trap = true`, `mother_rescue_as_solution +1`, `father_excluded_from_money +1`.
- 4: `final_confirm_analysis_trap = true`, `premature_interpretation_attack +1`, `money_contract_written -1`.

## T5 Ending Resolver

Use this resolver exactly:

```text
if final_confirm_independence_trial_trap == true:
    choose B
elif final_confirm_mother_manager_trap == true:
    choose C
elif final_confirm_analysis_trap == true:
    choose D
elif money_contract_written >= 2
     and respect_language_rule_written >= 1
     and defense_sequence_seen >= 2
     and adult_child_shame_named >= 1
     and father_fear_named >= 1
     and moral_verdict_reinforced < 2
     and mother_triangle_locked < 2
     and premature_interpretation_attack < 2:
    choose A
elif mother_secret_support_named >= 2
     and money_contract_score >= 1
     and mother_triangle_locked < 2
     and secret_cash_reinforced < 2
     and mother_rescue_as_solution < 2
     and father_excluded_from_money < 2:
    choose C-Repaired
elif interpretation_timing_score >= 2
     and father_humiliated_by_analysis < 2
     and alliance_rupture < 2
     and premature_interpretation_attack < 2:
    choose D-Repaired
elif repaired_at_t4 == true
     and money_contract_written >= 1
     and contract_as_punishment < 2
     and failure_label_reinforced < 2
     and secret_cash_reinforced < 2
     and mother_rescue_as_solution < 2
     and premature_interpretation_attack < 2
     and alliance_rupture < 2:
    choose A-Repaired
elif moral_verdict_reinforced >= 2
     or failure_label_reinforced >= 2
     or contract_as_punishment >= 2:
    choose B
elif secret_cash_reinforced >= 2
     or mother_rescue_as_solution >= 2
     or father_excluded_from_money >= 2:
    choose C
elif premature_interpretation_attack >= 2
     or father_humiliated_by_analysis >= 2
     or alliance_rupture >= 2:
    choose D
else:
    choose Low
```

Resolver regression examples:

```text
T3A-1 -> T4A-1 -> T5-1 = A
Reason: shame is named, defense sequence is seen, and money/respect contract is explicit.

T3B-1 -> T4B-2 -> T5-1 = B
Reason: early repair attempt is overridden by punitive independence evaluation.

T3C-1 -> T4C-2 -> T5-1 = C
Reason: hidden-rescue insight is overridden by T4C-2 secret-cash reinforcement, even though T5-1 tries to confirm a contract.

T3D-1 -> T4D-2 -> T5-1 = D
Reason: repair begins but interpretation attack returns.

T3B-2 -> T4B-1 -> T5-1 = B
Reason: one late repair does not erase heavy failure-label reinforcement.
```

## Endings

| Ending | Required Conditions | Blocking Flags | Final Aftereffect |
| --- | --- | --- | --- |
| A. Shame Can Be Spoken | defense sequence + father fear + adult child shame + explicit money contract | moral/trial, rescue, analysis traps | money and shame are separated enough for a concrete contract |
| A-Repaired. Contract After A Rupture | repaired route plus partial money contract | punishment/secrecy/analysis overload | family recovers enough to write a limited contract |
| B. Failure Verdict | moral verdict or punishment contract dominates | none | adult child withdraws behind the door and father feels temporarily justified |
| C. Quiet Rescue | secret support or mother-manager trap dominates | no C1 repair | conflict quiets but the envelope remains hidden |
| C-Repaired. Envelope On The Table | mother's rescue named and moved into shared ledger | secret reinforcement | mother's help becomes speakable without making her the manager |
| D. Analyzed, Not Met | premature interpretation or humiliation dominates | no D1 repair | correct theory damages alliance |
| D-Repaired. Fear Before Projection | interpretation timing repaired | analysis attack | father can say fear before being interpreted |
| Low. Same Door, Same Envelope | no strong repair | none | door remains closed, envelope remains hidden, rejection papers stay untouched |

## Route-Specific CG Lock

Minimum commercial-comfortable FT-007 image set:

| ID | Scene |
| --- | --- |
| FT007_CG_01 | closed bedroom door beside rejection papers and hidden cash envelope |
| FT007_CG_02 | father's worn work shoes/ID beside adult child's resume folder |
| FT007_CG_03 | mother tucking cash envelope under dish towel |
| FT007_CG_04 | adult child reading unopened rejection email in bedroom doorway |
| FT007_CG_05 | kitchen table ledger with blank contract page |
| FT007_CG_06 | shame_named route, three family members seeing blame-withdrawal-envelope sequence |
| FT007_CG_07 | moral_attack route, rejection papers treated like evidence |
| FT007_CG_08 | rescue_triangle route, mother between father and child with envelope |
| FT007_CG_09 | premature_interpretation route, supervisor notebook too prominent |
| FT007_CG_10 | T4A high, defense sequence mapped without accusation |
| FT007_CG_11 | T4B low, independence plan as failure trial |
| FT007_CG_12 | T4C low, mother managing money while father/child look away |
| FT007_CG_13 | T4D low, father recoils from analysis language |
| FT007_CG_14 | T4D repaired, father names fear in his own words |
| FT007_CG_15 | T5 high, pen between father and adult child over shared ledger |
| FT007_CG_16 | Ending A, explicit money/respect contract visible on table |
| FT007_CG_17 | Ending A-Repaired, limited contract after rupture |
| FT007_CG_18 | Ending B, adult child behind partially closed door with rejection papers |
| FT007_CG_19 | Ending C, envelope remains under dish towel |
| FT007_CG_20 | Ending C-Repaired, envelope placed on ledger in open view |
| FT007_CG_21 | Ending D, family looks at supervisor notebook instead of one another |
| FT007_CG_22 | Ending D-Repaired, notebook closed while father speaks fear |
| FT007_CG_23 | Low ending, same door and same envelope unchanged |
| FT007_CG_24 | Contract detail shot, "failure/한심" language crossed out and replaced by fear statement |

All CGs must be 1600x900, no baked text, no UI, bottom 25-30% clean.

## Gate Requirement

FT-007 cannot proceed to FT-008 until V2 passes:

- psychodynamic family therapy fidelity review;
- game branching/consequence review;
- commercial VN dialogue review.
