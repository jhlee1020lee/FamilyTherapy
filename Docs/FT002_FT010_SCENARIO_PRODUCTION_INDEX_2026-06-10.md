# FT-002~FT-010 Scenario Production Index

Last updated: 2026-06-10

This document coordinates the scenario-writing pass for FT-002 through FT-010 while the image-generation window works on FT-001 assets.

## Benchmark

Use FT-001 as the quality benchmark:

```text
Docs/FT001_MAJOR_BRANCHING_SCENARIO_2026-06-10.md
Docs/FT001_REALISTIC_DIALOGUE_EXPANSION_2026-06-10.md
```

FT-002~FT-010 should not remain generic training-card cases. Each one needs:

- a clear family therapy theory focus;
- a realistic presenting scene;
- family members who speak from lived pressure rather than theory vocabulary;
- 5 playable turns;
- choices that sound like real trainee interventions;
- good, partial, and harmful choices that are all plausible;
- route-level consequences, not only score changes;
- endings that show what changed in the family system.

## Active Case Plan

| Case | Theory focus | Supervisor | Case problem | Scenario doc |
| --- | --- | --- | --- | --- |
| FT-002 | Bowen multigenerational | 안우진 | 조손 청소년 가족, 야간 귀가 지연 | `Docs/FT002_BOWEN_BRANCHING_LOCK_V3_2026-06-10.md` |
| FT-003 | Structural | 이정후 | 맞벌이 특수교육 자녀 가족, 치료 일정과 부부 갈등 | `Docs/FT003_STRUCTURAL_BRANCHING_LOCK_V2_2026-06-10.md` |
| FT-004 | Satir/Experiential | 김연주 | 이민 배경 다문화 가족, 통번역 장벽과 보육 탈락 | `Docs/FT004_SATIR_BRANCHING_LOCK_V2_2026-06-10.md` |
| FT-005 | Structural | 이정후 | 재혼가족, 새 부모-자녀 경계 갈등 | `Docs/FT005_STEPFAMILY_BRANCHING_LOCK_V2_2026-06-10.md` |
| FT-006 | Satir/Experiential | 김연주 | 장기질환 자녀 가족, 형제자매의 외로움 | `Docs/FT006_SATIR_ILLNESS_SIBLING_BRANCHING_LOCK_V2_2026-06-10.md` |
| FT-007 | Psychodynamic | 송성문 | 성인자녀 원가족 재결합, 경제 의존과 부모 분노 | `Docs/FT007_PSYCHODYNAMIC_BRANCHING_LOCK_V2_2026-06-10.md` |
| FT-008 | Narrative | 박병호 | 학교폭력 이후 가족 침묵 | `Docs/FT008_NARRATIVE_BRANCHING_LOCK_V2_2026-06-10.md` |
| FT-009 | Cognitive-behavioral family therapy | 정세영 | 산후 우울과 친족 지원망 부재 | `Docs/FT009_CBFT_BRANCHING_LOCK_V2_2026-06-10.md` |
| FT-010 | Solution-focused | 송지후 | 형제 돌봄 과부하와 청소년 부모화 | `Docs/FT010_SOLUTION_BRANCHING_LOCK_V2_2026-06-10.md` |

## Shared Structure

Each scenario doc uses the same production structure:

```text
1. Goal
2. Cast and surface/hidden needs
3. Route model
4. Five-turn flow
5. Common intro
6. T1 choices and route seed
7. Route-specific T2-T4 scenes
8. T5 closure
9. Endings
10. Dialogue expansion notes
```

## Naming Policy

FT-002 names are already locked in `Docs/CHARACTER_NAME_REGISTRY.md`.

For FT-003~FT-010, this pass uses role labels such as `어머니`, `아버지`, `자녀`, `청소년`, `보호자`, `배우자`, `동생` until the user assigns names. Dialogue should be written so names can be inserted later without rewriting the clinical logic.

## Production Rule

Do not write families as theory examples. Write theory through what the player notices and what the supervisor says.

Families should say things like:

```text
"제가 말하면 더 복잡해져요. 그냥 조용히 있는 게 낫습니다."
```

They should not say:

```text
"저는 회유형 의사소통을 하고 있습니다."
```

## Next Pass

After all major branching scenario docs exist, create realistic dialogue expansion docs case by case:

```text
FT002_REALISTIC_DIALOGUE_EXPANSION_2026-06-10.md
...
FT010_REALISTIC_DIALOGUE_EXPANSION_2026-06-10.md
```

Those expansion docs should follow FT-001's style: 2-3 sentence utterances, realistic therapist reflections, and route-specific later scenes.

## Current Progress

Major branching scenario docs:

```text
FT-002 V3 gate passed
FT-003 V2 gate passed
FT-004 V3 gate passed
FT-005 V3 gate passed
FT-006 V3 gate passed
FT-007 V3 gate passed
FT-008 V3 gate passed
FT-009 V3 gate passed
FT-010 V3 gate passed
```

Realistic dialogue expansion docs:

```text
FT-002 V3 gate passed: Docs/FT002_REALISTIC_DIALOGUE_EXPANSION_V3_2026-06-10.md; gate record: Docs/FT002_V3_GATE_REVIEW_2026-06-10.md
FT-003 V2 gate passed: Docs/FT003_REALISTIC_DIALOGUE_EXPANSION_V2_2026-06-10.md; gate record: Docs/FT003_V2_GATE_REVIEW_2026-06-10.md
FT-004 V3 gate passed: Docs/FT004_REALISTIC_DIALOGUE_EXPANSION_V2_2026-06-10.md; gate record: Docs/FT004_V3_GATE_REVIEW_2026-06-10.md
FT-005 V3 gate passed: Docs/FT005_REALISTIC_DIALOGUE_EXPANSION_V2_2026-06-10.md; patch record: Docs/FT005_V3_PATCH_AND_GATE_TARGET_2026-06-10.md; gate record: Docs/FT005_V3_GATE_REVIEW_2026-06-10.md
FT-006 V3 gate passed: Docs/FT006_REALISTIC_DIALOGUE_EXPANSION_V2_2026-06-10.md; patch record: Docs/FT006_V3_PATCH_AND_GATE_TARGET_2026-06-10.md; gate record: Docs/FT006_V3_GATE_REVIEW_2026-06-10.md
FT-007 V3 gate passed: Docs/FT007_REALISTIC_DIALOGUE_EXPANSION_V2_2026-06-10.md; patch record: Docs/FT007_V3_PATCH_AND_GATE_TARGET_2026-06-10.md; gate record: Docs/FT007_V3_GATE_REVIEW_2026-06-10.md
FT-008 V3 gate passed: Docs/FT008_REALISTIC_DIALOGUE_EXPANSION_V2_2026-06-10.md; patch record: Docs/FT008_V3_PATCH_AND_GATE_TARGET_2026-06-10.md; gate record: Docs/FT008_V3_GATE_REVIEW_2026-06-10.md
FT-009 V3 gate passed: Docs/FT009_REALISTIC_DIALOGUE_EXPANSION_V2_2026-06-10.md; patch record: Docs/FT009_V3_PATCH_AND_GATE_TARGET_2026-06-10.md; gate record: Docs/FT009_V3_GATE_REVIEW_2026-06-10.md
FT-010 V3 gate passed: Docs/FT010_REALISTIC_DIALOGUE_EXPANSION_V2_2026-06-10.md; patch record: Docs/FT010_V3_PATCH_AND_GATE_TARGET_2026-06-10.md; gate record: Docs/FT010_V3_GATE_REVIEW_2026-06-10.md; CG command: Docs/FT010_COMMERCIAL_CG_GENERATION_COMMAND_2026-06-10.md
```

Next implementation order:

```text
1. FT-002~FT-010 document/scenario gate pass is complete as a planning baseline, not as final runtime completion.
2. Runtime still uses the shared focused-case ending key set; per-case route-specific repaired endings remain future work.
3. Keep role labels for FT-003~FT-010 until the user assigns final names.
4. Use each case's gate review document as the minimum quality floor for future edits, but verify against current Unity code before claiming completion.
5. Keep FT-010 CG instructions aligned with the 1600x900/no-stretch FT-001 image pipeline.
6. Next production phase remains image generation, Unity wiring validation, and runtime route-specific expansion.
```
