# FT-004 Satir Branching Lock V2

## Status

This V2 supersedes the branching portions of:

```text
Docs/FT004_SATIR_MAJOR_BRANCHING_SCENARIO_2026-06-10.md
Docs/FT004_REALISTIC_DIALOGUE_EXPANSION_2026-06-10.md
```

The original common intro and T1 remain usable. V2 is the production authority for T3, T4, T5, state, ending lock, and CG planning.

## Core Satir Question

FT-004 is not simply about paperwork, language, or childcare access.

The central question is:

```text
When the folded childcare notice is on the table, does "괜찮아요" mean consent, fear, shame, survival, or a request that cannot yet be spoken?
```

## Recurring Visual Anchor

```text
folded childcare application notice with a red missing-document mark
```

Supporting objects:

- phone on speaker after a difficult institution call;
- document envelope containing mixed childcare paperwork;
- translated checklist with handwritten Korean notes;
- child drawing while watching the adults' voices;
- institution clipboard/tablet;
- caregiver's smile that stays after the call but disappears when alone.

The folded notice changes meaning by route:

| Route | Notice Meaning |
| --- | --- |
| `congruent_voice` | a real barrier the caregiver can face with a clear request |
| `placating_mask` | proof the caregiver must become smaller and try harder |
| `blame_loop` | evidence used by spouse to accuse and by caregiver to apologize |
| `institution_only` | a procedural object solved while family feeling remains unheard |
| `superreasonable_procedure` | a clean checklist that hides fear, shame, and access limits |

## State Model

```text
route_primary:
  congruent_voice
  placating_mask
  blame_loop
  institution_only
  superreasonable_procedure

flags:
  iceberg_named
  congruent_request_practiced
  spouse_reflection_practiced
  checklist_connected_to_help
  placating_accepted
  caregiver_alone_responsible
  blame_intensified
  spouse_fear_named
  child_tension_seen
  institution_as_only_solution
  institution_as_resource
  superreasonable_cover
  language_barrier_respected
  translation_delay_seen
  phone_pace_seen
  work_shift_pressure_seen
  emotion_bypassed
  repair_attempted
  unresolved_smile
  final_confirm_help
  final_confirm_compliance_trap
  final_confirm_bypass_trap

scores:
  congruence_score
  shame_safety_score
  blame_risk_score
  institutional_support_score
  child_witness_burden_score
```

## Turn Lock

| Turn | Function | Must Not Collapse |
| --- | --- | --- |
| T1 | route entry | defines whether "괜찮아요" is heard as feeling, compliance, blame, or procedure |
| T2 | first route scene | shows communication stance in family interaction |
| T3 | route-specific iceberg pressure | folded notice/phone/checklist changes meaning |
| T4 | playable congruent communication practice | three choices per route, visible family re-response |
| T5 | help request and institution plan | ending conditions use flags, not route count alone |

## T3 Choice Consequences

### `congruent_voice`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3A-1 name feeling and need | `iceberg_named +1`, `shame_safety_score +1` | opens high A |
| T3A-2 spouse takes all calls | `caregiver_alone_responsible -1`, `institution_as_only_solution +1` | risks D if voice disappears |
| T3A-3 practice Korean harder | `placating_accepted +1`, `emotion_bypassed +1` | drifts to B |

### `placating_mask`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3B-1 repair fast checklist move | `repair_attempted +1`, `checklist_connected_to_help +1` | opens B repaired/A |
| T3B-2 make better checklist | `caregiver_alone_responsible +2`, `placating_accepted +1` | locks quiet compliance unless repaired |
| T3B-3 spouse promises no anger | `spouse_fear_named -1`, `emotion_bypassed +1` | shallow calm, unresolved smile |

### `blame_loop`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3C-1 both name fear | `spouse_fear_named +1`, `blame_intensified -1`, `repair_attempted +1` | repair possible |
| T3C-2 spouse apologizes first | `blame_intensified +1`, `placating_accepted +1` | one-up/one-down persists |
| T3C-3 caregiver admits fault | `caregiver_alone_responsible +2`, `placating_accepted +2` | C/B low |

### `institution_only`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3D-1 checklist plus help request | `institution_as_resource +1`, `checklist_connected_to_help +1` | opens D repaired/A |
| T3D-2 postpone emotion | `emotion_bypassed +2`, `institution_as_only_solution +1` | D low |
| T3D-3 institution handles all checks | `institution_as_only_solution +2`, `caregiver_alone_responsible -1` | D low, family unheard |

### `superreasonable_procedure`

| Choice | Effect | Later Lock |
| --- | --- | --- |
| T3E-1 slow the procedure and name what it covers | `superreasonable_cover -1`, `phone_pace_seen +1`, `emotion_bypassed -1` | repair possible |
| T3E-2 checklist first, feelings later | `superreasonable_cover +2`, `emotion_bypassed +2` | locks D/Low unless repaired |
| T3E-3 spouse handles objective parts | `institution_as_only_solution +1`, `caregiver_alone_responsible -1`, `superreasonable_cover +1` | solved-for-her low |

## T4 Playable Choice Lock

### T4A `congruent_voice`: Clear Request Or Disappearing Voice

```text
A1. "보호자분의 말을 해결책으로 바로 바꾸지 않고, '나는 전화할 때 무엇이 무섭고 무엇을 부탁하고 싶은지' 한 문장으로 말해보겠습니다."
A2. "배우자분이 앞으로 기관 전화를 대신 맡으면 보호자분의 부담이 줄어들 수 있습니다."
A3. "보호자분이 전화 표현을 더 연습하면 다음 신청에서는 덜 막힐 수 있습니다."
```

Effects:

- A1: `congruent_request_practiced +2`, `spouse_reflection_practiced +1`, `checklist_connected_to_help +1`, high A candidate.
- A2: `institution_as_only_solution +1`, caregiver voice risk.
- A3: `placating_accepted +1`, `caregiver_alone_responsible +1`, B drift.

### T4B `placating_mask`: Repair Compliance Or Make It Neater

```text
B1. "제가 체크리스트로 너무 빨리 갔습니다. 신청을 다시 하는 것과 동시에, 막힐 때 누구에게 어떤 말을 할지도 같이 정하겠습니다."
B2. "이번에는 보호자분이 실수하지 않도록 더 자세한 체크리스트와 한국어 표현 연습표를 만들겠습니다."
B3. "배우자분이 화내지 않겠다고 약속하면 보호자분도 덜 긴장할 수 있습니다."
```

Effects:

- B1: `repair_attempted +1`, `checklist_connected_to_help +2`, `congruent_request_practiced +1`, repaired route.
- B2: `caregiver_alone_responsible +2`, `placating_accepted +2`, B low.
- B3: `emotion_bypassed +1`, `unresolved_smile +1`, weak unresolved.

### T4C `blame_loop`: Fear Under Blame Or More Shame

```text
C1. "방금 대화가 억울함과 사과로만 흘렀습니다. 배우자분은 무엇이 무서운지, 보호자분은 무엇이 부끄러운지 한 문장씩 말해보겠습니다."
C2. "배우자분은 억울하더라도 지금은 보호자분에게 사과부터 하셔야 합니다."
C3. "보호자분이 더 명확히 준비하지 못한 부분을 인정해야 다음 신청 준비로 갈 수 있습니다."
```

Effects:

- C1: `spouse_fear_named +2`, `iceberg_named +1`, `blame_intensified -1`, repair candidate.
- C2: `blame_intensified +1`, one-up/one-down shift, C unresolved.
- C3: `caregiver_alone_responsible +2`, `placating_accepted +2`, C low.

### T4D `institution_only`: Resource Or Emotional Bypass

```text
D1. "통번역 지원과 서류 목록은 정리하겠습니다. 동시에 전화를 걸기 전 보호자분이 배우자에게 할 도움 요청 문장을 하나 연습하겠습니다."
D2. "절차가 정리됐으니 감정 이야기는 잠시 보류하고, 다음 신청까지 서류 준비에 집중하겠습니다."
D3. "기관 담당자가 가능한 확인을 모두 대신해주면 가족 갈등도 줄어들 수 있습니다."
```

Effects:

- D1: `institution_as_resource +2`, `checklist_connected_to_help +1`, `congruent_request_practiced +1`, D repaired/A candidate.
- D2: `emotion_bypassed +2`, `institution_as_only_solution +1`, D low.
- D3: `institution_as_only_solution +2`, family voice bypassed, D low.

### T4E `superreasonable_procedure`: Clean Procedure Or Covered Feeling

```text
E1. "표는 만들겠습니다. 다만 각 칸 옆에 '막힐 때 다시 물어볼 문장'과 '누가 옆에서 확인할지'를 같이 적겠습니다."
E2. "감정 이야기는 다음 회기로 넘기고, 오늘은 발급처, 마감일, 통번역 예약 가능 시간만 정확히 정리하겠습니다."
E3. "객관적인 절차는 배우자분이 전담하고, 보호자분은 확인된 서류를 준비하는 역할로 나누겠습니다."
```

Effects:

- E1: `superreasonable_cover -1`, `checklist_connected_to_help +1`, `congruent_request_practiced +1`, repaired E.
- E2: `superreasonable_cover +2`, `emotion_bypassed +2`, E low.
- E3: `institution_as_only_solution +1`, `superreasonable_cover +1`, solved-for-her low.

## T5 Ending Lock

Ending priority must be deterministic. Use this resolver exactly:

```text
if final_confirm_compliance_trap == true:
    if blame_intensified >= 2:
        choose C
    elif institution_as_only_solution >= 2 or superreasonable_cover >= 2:
        choose D
    else:
        choose B
elif final_confirm_bypass_trap == true:
    if superreasonable_cover >= 2 and emotion_bypassed >= 2:
        choose E
    else:
        choose D
elif congruent_request_practiced >= 2
     and spouse_reflection_practiced >= 1
     and checklist_connected_to_help >= 1
     and blame_intensified < 2:
    choose A
elif repair_attempted >= 1
     and checklist_connected_to_help >= 2
     and emotion_bypassed < 2:
    choose A-Repaired
elif spouse_fear_named >= 2
     and iceberg_named >= 1
     and blame_intensified < 2:
    choose C-Repaired
elif institution_as_resource >= 2
     and congruent_request_practiced >= 1
     and institution_as_only_solution < 3:
    choose D-Repaired
elif superreasonable_cover >= 2
     and emotion_bypassed >= 2
     and congruent_request_practiced < 1:
    choose E
elif placating_accepted >= 2
     or caregiver_alone_responsible >= 2:
    choose B
elif blame_intensified >= 2
     or (caregiver_alone_responsible >= 2 and route_primary == blame_loop):
    choose C
elif institution_as_only_solution >= 2
     or emotion_bypassed >= 2:
    choose D
else:
    choose Low
```

Implementation must use only the explicit comparisons above. Do not add informal tie-break phrases.

## T5 Final Confirmation Turn

Before final prose, FT-004 should present one final route-aware confirmation choice:

```text
High/repaired option:
"체크리스트, 통번역 연결, 그리고 막힐 때 말할 도움 요청 문장을 한 장에 같이 남기겠습니다."

Quiet compliance trap:
"체크리스트를 더 자세히 만들고, 보호자분이 매일 확인하는 방식으로 마무리하겠습니다."

Institution bypass trap:
"기관과 배우자분이 절차를 주로 확인하고, 보호자분은 준비된 서류를 제출하는 방식으로 마무리하겠습니다."
```

Effects:

- high/repaired option: `final_confirm_help = true`, `checklist_connected_to_help +1`, `congruent_request_practiced +1`, `institution_as_resource +1`.
- quiet compliance trap: `final_confirm_compliance_trap = true`, `placating_accepted +1`, `caregiver_alone_responsible +1`, `unresolved_smile +1`.
- institution bypass trap: `final_confirm_bypass_trap = true`, `institution_as_only_solution +1`, `superreasonable_cover +1`.

| Ending | Required Conditions | Blocking Flags | Final Aftereffect |
| --- | --- | --- | --- |
| A. A Voice Beside The Checklist | `congruent_request_practiced >= 2`, `spouse_reflection_practiced >= 1`, `checklist_connected_to_help >= 1` | `blame_intensified >= 2` or any final trap | caregiver has one help request sentence beside the checklist |
| A-Repaired. Procedure With A Voice | `repair_attempted >= 1`, `checklist_connected_to_help >= 2`, `emotion_bypassed < 2` | none | route began poorly but procedure and feeling reconnect |
| B. Quiet Compliance | `placating_accepted >= 2` or `caregiver_alone_responsible >= 2` | no congruent repair | checklist improves, caregiver becomes smaller |
| C. Blame Translated As Failure | `blame_intensified >= 2` or (`caregiver_alone_responsible >= 2` and blame route) | `spouse_fear_named >= 2` if repaired | spouse's fear remains accusation, caregiver apologizes |
| C-Repaired. Fear Under Blame | `spouse_fear_named >= 2`, `iceberg_named >= 1` | `blame_intensified >= 2` | spouse names fear without making caregiver the failure |
| D. Paper Solved, Family Unheard | `institution_as_only_solution >= 2` or `emotion_bypassed >= 2` | `institution_as_resource >= 2` and request practiced | procedure improves, family communication does not |
| D-Repaired. Institution As Resource | `institution_as_resource >= 2`, `congruent_request_practiced >= 1` | none | institution helps without replacing family request |
| E. Clean Checklist, Covered Iceberg | `superreasonable_cover >= 2`, `emotion_bypassed >= 2` | no request practice | everything sounds rational, but the smile returns |
| Low. The Same Smile Returns | `unresolved_smile >= 1` or no repair flags | no strong repair | caregiver smiles after the call and exhales alone again |

## Route-Specific CG Lock

Minimum commercial-comfortable FT-004 image set:

| ID | Scene |
| --- | --- |
| FT004_CG_01 | folded childcare notice with red missing-document mark on table |
| FT004_CG_02 | caregiver smiling after phone call, hand still on phone |
| FT004_CG_02B | speakerphone on table, institution voice present while caregiver's finger hovers over end-call |
| FT004_CG_02C | document envelope with mixed papers, spouse's hand and caregiver's hand both near it |
| FT004_CG_03 | spouse looking at household calendar/work shift while notice is folded |
| FT004_CG_04 | child drawing quietly while watching adults' voices |
| FT004_CG_05 | institution representative with checklist/tablet, not centered as savior |
| FT004_CG_06 | congruent route, caregiver unfolds notice and names fear |
| FT004_CG_07 | placating route, caregiver gathers papers alone |
| FT004_CG_08 | blame route, spouse leans toward notice while caregiver apologizes |
| FT004_CG_09 | institution route, clipboard/tablet becomes central object |
| FT004_CG_10 | T4A clear request practice |
| FT004_CG_11 | T4B repaired, checklist beside help request sentence |
| FT004_CG_12 | T4B low, neater checklist but caregiver alone |
| FT004_CG_13 | T4C repaired, spouse names fear and caregiver remains upright |
| FT004_CG_14 | T4C low, apology posture returns |
| FT004_CG_15 | T4D repaired, institution support on side table while couple speaks |
| FT004_CG_16 | T4D low, institution paperwork solved but family apart |
| FT004_CG_17 | superreasonable route, clean checklist centered while caregiver's hand tightens |
| FT004_CG_18 | Ending A, voice beside checklist |
| FT004_CG_19 | Low ending, same smile after phone call |

All CGs must be 1600x900, no baked text, no UI, bottom 25-30% clean.

## Gate Requirement

FT-004 cannot proceed to FT-005 until V2 passes:

- Satir/experiential family therapy fidelity review;
- game branching/consequence review;
- commercial VN dialogue review.
