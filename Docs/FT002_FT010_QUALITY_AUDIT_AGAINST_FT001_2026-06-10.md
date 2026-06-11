# FT-002~FT-010 Quality Audit Against FT-001

Last updated: 2026-06-10

This audit compares FT-002 through FT-010 against the current FT-001 benchmark.

Benchmark files:

```text
Docs/FT001_MAJOR_BRANCHING_SCENARIO_2026-06-10.md
Docs/FT001_REALISTIC_DIALOGUE_EXPANSION_2026-06-10.md
```

Reviewed files:

```text
Docs/FT002_*_MAJOR_BRANCHING_SCENARIO_2026-06-10.md
Docs/FT003_*_MAJOR_BRANCHING_SCENARIO_2026-06-10.md
...
Docs/FT010_*_MAJOR_BRANCHING_SCENARIO_2026-06-10.md

Docs/FT002_REALISTIC_DIALOGUE_EXPANSION_2026-06-10.md
Docs/FT003_REALISTIC_DIALOGUE_EXPANSION_2026-06-10.md
...
Docs/FT010_REALISTIC_DIALOGUE_EXPANSION_2026-06-10.md
```

## Audit Method

Three sub-agents reviewed the scenario set from separate angles:

1. Clinical/family therapy fidelity.
2. Game branching and player consequence.
3. Dialogue quality, voice, and commercial VN feel.

I also checked the current files locally for:

- document existence;
- line counts;
- presence of intro/T1/T2/ending/implementation sections;
- major scenario route and ending coverage;
- obvious outdated wording or pending markers.

Current file evidence:

- FT-002~FT-010 all have major branching scenario docs.
- FT-002~FT-010 all have realistic dialogue expansion docs.
- Each realistic dialogue expansion doc includes common intro, T1, T2, ending dialogue, and implementation notes.
- No `pending` marker remains in `Docs/FT002_FT010_SCENARIO_PRODUCTION_INDEX_2026-06-10.md`.

## Executive Verdict

FT-002~FT-010 are usable as serious scenario drafts, but they are not yet equal to FT-001.

The core issue is not raw length. Most expansion docs are 300+ lines and structurally complete. The gap is quality of branching and commercial VN texture:

- FT-001 has stronger route-specific later scenes, especially T3/T4.
- FT-001 has named, concrete family members from the start.
- FT-001 uses a recurring sensory scene: morning, phone calls, door/absence, school pressure.
- FT-002~FT-010 often split at T1/T2, then reconverge into one good-core intervention.
- FT-002~FT-010 sometimes read like excellent training design documents rather than fully dramatized VN scenes.

Overall status:

```text
Major scenario structure: complete
Dialogue expansion structure: complete
FT-001 parity: not yet
Implementation readiness: partial, but revision recommended before Unity conversion
```

## Combined Scores

Scores below combine the three review angles: clinical fidelity, game branching, and dialogue/VN quality.

| Case | Clinical | Game/branching | Dialogue/VN | Combined | Verdict |
| --- | ---: | ---: | ---: | ---: | --- |
| FT-002 Bowen | 8.0 | 7.6 | 8.4 | 8.0 | Strong, close to FT-001 after branch deepening |
| FT-003 Structural | 8.0 | 7.3 | 7.7 | 7.7 | Solid theory, needs character specificity |
| FT-004 Satir | 8.5 | 7.5 | 7.9 | 8.0 | Clinically strong, label/route overlap issue |
| FT-005 Stepfamily Structural | 8.5 | 7.9 | 8.2 | 8.2 | Strongest post-FT001 candidate |
| FT-006 Satir Illness Sibling | 7.5 | 7.6 | 7.5 | 7.5 | Good frame, needs illness-family specificity |
| FT-007 Psychodynamic | 7.5 | 7.4 | 7.8 | 7.6 | Good conflict, theory language too exposed |
| FT-008 Narrative School Violence | 7.0 | 8.0 | 7.6 | 7.5 | Strong VN premise, safety/ethics underbuilt |
| FT-009 CBFT Postpartum | 8.0 | 7.8 | 8.0 | 7.9 | Strong, but safety plan must be more concrete |
| FT-010 Solution Parentification | 7.5 | 7.5 | 7.2 | 7.4 | Weakest VN feel, needs naming and scene texture |

## FT-001 Benchmark Strengths

FT-001 currently does these better than the rest:

1. Branches feel like different therapy sessions, not just different feedback labels.
2. Later scenes shift by route, especially T3/T4.
3. Family members are named and visually imaginable.
4. Repeated concrete scene objects create memory: morning, school call, silence, mother's work pressure.
5. Endings show different family agreements, not just different supervisor judgments.
6. Good and bad choices are plausible enough that a trainee might actually choose them.

## Cross-Case Problems

### 1. T3-T5 reconverge too much

Most FT-002~FT-010 episodes split clearly at T1/T2, but T3/T4 often return to the ideal core intervention. This makes the design read as:

```text
early reaction changes -> core correct teaching scene -> ending label
```

FT-001 is stronger because later route states stay alive longer.

Required fix:

```text
For every case, add at least two route-specific T3/T4 continuations:
- one recovery route;
- one damaged-but-playable route.
```

### 2. Good choices look too obviously correct

Good choices are often the longest, most balanced, most theory-aligned options. Bad choices are sometimes visibly dangerous before selection.

Examples to revise:

- FT-008: "전학은 도망처럼 보일 수 있으니..." is too obviously harmful.
- FT-009: "우울감과 위험 신호를 확인하기 전에..." is too obviously wrong.
- FT-007: "투사" language makes the mistake too textbook.

Required fix:

```text
Make incorrect choices sound clinically tempting:
- practical but premature;
- empathic but one-sided;
- safety-oriented but over-controlling;
- theory-informed but mistimed.
```

### 3. Functional role labels reduce VN immersion

The biggest dialogue immersion problem is role-label naming.

Weak labels:

```text
이민 배경 보호자
치료사/기관 담당자
조부모/도움 인물
누나
보호자
이웃 교사/지원 인물
청소년 자녀
자녀
```

FT-001 feels stronger partly because the characters are already people, not roles.

Required fix:

```text
Assign display names for FT-003~FT-010 before final dialogue polish.
Keep internal IDs role-based, but use real display names in dialogue.
```

### 4. Safety and ethics need stronger handling

Most important cases:

- FT-008 school violence aftermath.
- FT-009 postpartum depression.

FT-008 currently has a strong narrative therapy frame, but it risks implying that externalizing language can replace safety/protection work.

FT-009 includes safety language, but for a postpartum high-risk case it is still too abstract.

Required fix:

```text
FT-008 must explicitly include:
- current school/online/commute safety;
- remaining perpetrator contact;
- self-harm screening;
- school protection measures;
- confidentiality and reporting/record limits;
- separation of safety procedure and narrative work.

FT-009 must explicitly include:
- frequency/intensity of disappearance/self-harm thoughts;
- intent/plan/means check;
- infant safety;
- not being alone during high-risk windows;
- medical/psychiatric or emergency linkage;
- clear escalation threshold.
```

### 5. Repeated sentence patterns weaken character voice

Repeated patterns:

```text
좋은 시작입니다.
좋은 마무리입니다.
회복하려면...
중요하지만...
동시에...
~처럼 들립니다.
먼저 보겠습니다.
```

These are useful as drafting scaffolds but should not remain this dense in final VN dialogue.

Required fix:

```text
Rewrite at least 30% of therapist/supervisor reflection lines into:
- short pauses;
- uncertain reflections;
- direct process questions;
- silence acknowledgements;
- concrete image-based observations.
```

## Case-By-Case Findings

## FT-002 Bowen

Score: 8.0

Strengths:

- 김선기의 상실 불안, 박준현의 아버지 비교 방어, 박석민의 회피적 중재가 strong.
- Bowen concepts are mostly translated into lived interaction.
- I-position closure is concrete.

Problems:

- `control_contract` route is too conventional.
- `loss_touched_too_fast` and `triangle_avoidance` are interesting but do not get enough later-route continuation.
- T5 bad option "Bowen 가족치료 개념" sounds like class homework rather than a family-facing intervention.

Fix priority:

```text
Replace theory-homework option with a more realistic bad closure:
"오늘은 각자 느낀 점을 정리해서 다음 시간에 말해보겠습니다."
Then show why it is vague and does not change the interaction pattern.
```

## FT-003 Structural

Score: 7.7

Strengths:

- Parent subsystem and child-as-carrier structure are clear.
- The therapy bag / treatment schedule scene is strong.
- Professional takeover route has good potential.

Problems:

- `아버지`, `어머니`, `자녀` role labels keep the family generic.
- Child's developmental/therapy-specific needs need more concrete texture.
- `professional_takeover` can feel like a repeated procedure route instead of a unique structural failure.

Fix priority:

```text
Add case-specific details:
- therapy bag;
- waiting-room fatigue;
- sensory exhaustion;
- missed appointment pressure;
- one concrete provider recommendation that conflicts with home life.
```

## FT-004 Satir

Score: 8.0

Strengths:

- "괜찮아요" below-surface work is effective.
- Language barrier, shame, and spouse's livelihood anxiety are connected.
- Satir congruent expression intervention is clear.

Problems:

- `이민 배경 보호자` label breaks VN immersion badly.
- `placating_mask` and `institution_only` overlap in outcome.
- Spouse's temporary relief when placating works could be dramatized more.

Fix priority:

```text
Name the caregiver and spouse.
Split placating vs institution routes:
- placating route: family appears calmer, but caregiver disappears emotionally;
- institution route: paperwork improves, but relationship remains uncoordinated.
```

## FT-005 Stepfamily Structural

Score: 8.2

Strengths:

- Strongest non-FT001 candidate.
- Loyalty conflict, stepfather invisibility, and mother's mediation burden are compelling.
- Structural staged-boundary intervention fits the case.

Problems:

- `child_loyalty_only` route currently looks too obviously partial-correct.
- Words like 권한, 관계, 가족 밖 repeat too much.
- It needs more seduction: protecting the child should feel emotionally rewarding before showing its cost.

Fix priority:

```text
Make child-protection route initially feel successful:
- child opens up;
- mother relaxes;
- stepfather goes quiet.
Then reveal later that the couple/stepfamily structure has split further.
```

## FT-006 Satir Illness Sibling

Score: 7.5

Strengths:

- The well sibling's "괜찮아요" and mixed feelings are good.
- Family sculpture and two-feelings intervention fit Satir.

Problems:

- Too close to FT-004's "괜찮아요 / iceberg / two minds" structure.
- Long-term illness family realities are under-specified.
- First child's presence is too abstract, even if off-screen.

Fix priority:

```text
Add concrete illness-family pressure:
- hospital schedule;
- overnight care;
- medication or treatment rhythm;
- sibling's abandoned activity;
- first child's guilt or reaction through a message/phone note.
```

## FT-007 Psychodynamic

Score: 7.6

Strengths:

- Shame, anger, withdrawal, and secret support work well.
- Father anger opening into fear is a good emotional arc.

Problems:

- "투사", "수치심", "방어", "비밀 지원" appear too conceptually.
- `premature_interpretation` is too obviously a theory mistake.
- Some lines feel like explanatory notes rather than family speech.

Fix priority:

```text
Replace exposed theory language with lived language:
- "투사" -> "아버지의 두려움이 자녀분에게 실패라는 이름으로 붙는 것"
- family reaction to theory language -> "그런 어려운 말로 저를 설명하지 마세요."
```

## FT-008 Narrative School Violence

Score: 7.5

Strengths:

- The premise is strong and VN-friendly.
- It avoids forcing trauma disclosure.
- Externalizing silence/endurance works as a narrative therapy frame.

Problems:

- Biggest clinical risk: safety/protection protocol is too thin.
- Procedure route risks implying that procedure steals voice, instead of procedure and narrative work both being necessary.
- "침묵", "버티기", "문제" repeat until some lines feel conceptual.

Fix priority:

```text
Add a safety-first narrative frame:
"이야기를 다시 꺼내기 전에, 지금도 그 문제가 학교나 온라인에서 계속 닿고 있는지 확인하겠습니다."
Then separate:
- protection procedure;
- family story repair;
- teen's preferred language.
```

## FT-009 CBFT Postpartum

Score: 7.9

Strengths:

- Automatic thought, behavior chain, and action contract are clear.
- "Today tonight" plan gives concrete gameplay outcome.
- Stronger than most in implementation readiness.

Problems:

- Safety plan needs more high-risk specificity.
- Some therapist lines are checklist-like and reduce emotional weight.
- Bad safety-missed choice is too obviously wrong.

Fix priority:

```text
Add explicit safety protocol:
- how often disappearance thoughts occur;
- whether there is intent/plan/means;
- infant safety check;
- who stays awake/nearby during risk window;
- medical/emergency contact threshold.
```

## FT-010 Solution-Focused Parentification

Score: 7.4

Strengths:

- "1 point less" is a good solution-focused game mechanic.
- Parentification risk is acknowledged.
- 30-minute self-time closure is concrete.

Problems:

- Weakest VN immersion due to labels: `누나`, `보호자`, `이웃 교사/지원 인물`.
- Numeric task language feels too worksheet-like.
- External-support route overlaps with FT-004/008 procedure/external system routes.

Fix priority:

```text
Name the adolescent, guardian, younger sibling, and helper.
Make the 30 minutes a scene, not just a number:
- school bag still on;
- sibling's rice bowl;
- message from teacher;
- unfinished club form.
```

## Priority Revision Plan

### Phase 1: Safety and ethics

1. Revise FT-008 for school violence safety/protection.
2. Revise FT-009 for postpartum depression safety protocol.

These should happen before Unity implementation.

### Phase 2: FT-001-level branching

3. Add route-specific T3/T4 continuations for each case.
4. Add a `fragile_repair`-like middle route where applicable.
5. Make low endings show unique family aftereffects, not just supervisor warnings.

### Phase 3: VN immersion

6. Assign names to FT-003~FT-010 cast.
7. Replace functional role labels in dialogue.
8. Add one recurring sensory object/scene per case.

Suggested anchors:

| Case | Anchor |
| --- | --- |
| FT-003 | therapy bag / car ride / waiting-room fatigue |
| FT-004 | phone call / untranslated form / forced smile |
| FT-005 | dinner table / unanswered greeting / bedroom door |
| FT-006 | hospital wristband / empty dining chair / school notice |
| FT-007 | closed bedroom door / bank transfer message / job email |
| FT-008 | school uniform / lunch table silence / phone notification |
| FT-009 | 2 a.m. crying / bottle water / unread message |
| FT-010 | rice bowl / school bag / club application / 30-minute window |

### Phase 4: Dialogue polish

9. Rewrite repetitive therapist/supervisor patterns.
10. Reduce exposed theory terms in family-facing dialogue.
11. Give repeated supervisors different tones across their cases:
    - 이정후 in FT-003: firmer structural coach.
    - 이정후 in FT-005: slower and more cautious about stepfamily pacing.
    - 김연주 in FT-004: careful with culture/language shame.
    - 김연주 in FT-006: gentler, child-centered, slower with guilt.

## Implementation Recommendation

Do not convert all FT-002~FT-010 documents into Unity yet.

Recommended order:

```text
1. Patch FT-008 and FT-009 safety issues.
2. Patch FT-002 and FT-007 exposed theory-language issues.
3. Assign names for FT-003~FT-010.
4. Deepen route-specific T3/T4 for FT-005, FT-008, FT-009 first.
5. Then start Unity conversion from FT-002 onward.
```

The current documents are strong enough to continue development, but FT-001 parity requires at least one more revision pass.
