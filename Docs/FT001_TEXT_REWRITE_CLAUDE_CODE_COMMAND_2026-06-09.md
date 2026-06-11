# FT-001 Text Rewrite Command For Claude Code

## Goal

Rewrite and polish only the FT-001 text for `Family Therapy Practicum`.

The goal is not to add new game systems. The goal is to make the FT-001 episode feel like a coherent family therapy visual novel session:

- natural Korean dialogue
- all four characters present in the same room
- family therapy concepts embedded through interaction, not exposition
- choices that sound like real counselor utterances
- feedback that teaches family therapy reasoning without sounding like a lecture
- emotionally believable mother/child/grandmother/teacher voices

## Project Location

```text
C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity
```

Primary file:

```text
Assets/Scripts/FamilyTherapyPracticumGame.cs
```

Reference docs:

```text
Docs/CHARACTER_NAME_REGISTRY.md
Docs/TEN_DOLLAR_COMMERCIAL_GAME_DESIGN_DOC.md
Docs/VISUAL_NOVEL_REMAKE_PLAN.md
Docs/DEMO_ROUTE_10_MINUTES.md
Docs/PRESENTATION_QA_SHEET.md
Docs/FT001_SEATED_ALL_CAST_IMAGE_GENERATION_COMMAND_2026-06-09.md
```

## Scope

Only rewrite FT-001 text content.

Do not redesign the UI.
Do not change scoring logic unless a label/text mismatch requires a tiny correction.
Do not rename characters.
Do not change genders.
Do not add new assets.
Do not edit unrelated cases.
Do not rewrite all 60 cases.

Allowed edits:

- FT-001 intro lines
- FT-001 setup dialogue lines
- FT-001 counselor choices
- FT-001 family reactions
- FT-001 feedback strings
- FT-001 supervisor notes
- FT-001 case file prose if needed
- FT-001 reflection questions if needed
- FT-001 labels/titles if needed

## Fixed Characters

Use these names exactly.

| ID | Name | Role |
| --- | --- | --- |
| `ft001_mother` | 박성빈 | mother |
| `ft001_child` | 이주형 | child |
| `ft001_grandmother` | 오선진 | maternal grandmother |
| `ft001_teacher` | 서건창 | homeroom teacher |
| `supervisor_system` | 김혜성 | family systems supervisor |

FT-001 case:

```text
FT-001 · 한부모 초등 자녀 가족
```

Basic situation:

- 박성빈 is a single mother working night shifts.
- 이주형 is an elementary-school-age boy showing school refusal and separation anxiety.
- 오선진 is the maternal grandmother. She worries but often speaks in a critical way.
- 서건창 is the homeroom teacher. He represents the school system and procedural pressure.
- The presenting problem is repeated morning conflict around school attendance.
- The therapeutic focus is not "who is wrong" but the recurring interaction cycle.

## Scene Premise

All four characters are in the room together.

Do not write scenes as if only the current speaker exists. Every line should make sense in a shared family therapy room where the other three people can hear and react.

When one character speaks, the text should imply the others are present:

- mother glances at child
- child avoids grandmother's eyes
- grandmother reacts to mother's wording
- teacher softens or stiffens when school procedure is mentioned

Do not overdo stage directions, but make the room feel populated.

## Tone

Korean dialogue should be natural and understated.

Avoid:

- textbook-like family therapy explanation from family members
- melodrama
- theatrical speeches
- long monologues
- child sounding like an adult therapist
- teacher sounding like a villain
- grandmother sounding only abusive
- mother sounding only guilty
- counselor choices that sound like essay sentences

Prefer:

- short spoken Korean
- emotionally plausible hesitation
- everyday words
- small defensive reactions
- indirect Korean family communication
- counselor utterances that are clear but not too long
- supervisor feedback that names the therapeutic reasoning after the interaction

## Family Therapy Lens

FT-001 should primarily teach family systems thinking:

- IP 고정 줄이기
- 순환 패턴 보기
- 증상의 관계 기능 보기
- 보호자 비난 완화
- 학교 체계가 가족 순환에 미치는 영향 보기
- 세대 간 걱정/비난 루프 보기
- 작은 행동 실험으로 연결하기

Other theories may appear in wrong or partial choices, but the recommended route should stay family systems oriented.

Do not make family members say terms like:

- "IP 고정"
- "순환 패턴"
- "삼각관계"
- "가족체계"
- "분화"

Those terms may appear in supervisor feedback, case file, or post-choice explanation, not in family dialogue.

## Structure To Polish

Locate `FT-001` in `Assets/Scripts/FamilyTherapyPracticumGame.cs`.

Polish these areas:

1. `BuildVnIntroLines()`
   - Each character introduction should sound natural.
   - Intro lines should be short enough to fit the VN dialogue box.
   - Keep:
     - "주형이 엄마 박성빈입니다."
     - "해솔초등학교 4학년 이주형입니다." or equivalent fake school/grade wording.
     - "주형이 할머니입니다."
     - "주형이 담임입니다."

2. FT-001 turn setup lines
   - Keep five-turn session structure.
   - Make each turn emotionally and clinically distinct.

3. FT-001 choices
   - Each choice should sound like something a trainee counselor might actually say.
   - Good choice should feel clinically warm and systemic.
   - Bad choices should be plausible mistakes, not absurd strawmen.
   - Keep choice length readable in UI.

4. Family reactions
   - Reactions should show how the family responds.
   - Mention at least two characters when useful.
   - Avoid generic "family feels better" wording.

5. Supervisor notes/feedback
   - Explain why a choice works or fails.
   - Use concise clinical language.
   - Do not over-explain.

## Proposed Five-Turn Arc

Keep or adapt this arc.

### Turn 1: Initial Joining And Problem Definition

Focus:

- Do not identify one culprit.
- Invite each person to name worry and desired change.
- Establish safety.

Family emotional state:

- mother exhausted and defensive
- child anxious and quiet
- grandmother watchful
- teacher procedural

### Turn 2: Mapping The Morning Cycle

Focus:

- Sequence the morning scene.
- Connect child freeze, mother urgency, school calls, grandmother criticism.

Family emotional state:

- mother starts seeing the pattern
- child says less but reveals function of staying still
- teacher begins to see school contact as part of the loop

### Turn 3: Grandmother And Intergenerational Pressure

Focus:

- Separate grandmother's worry from critical delivery.
- Help mother name how help becomes judgment.
- Keep child from becoming the symptom-carrier.

Family emotional state:

- grandmother defensive but worried
- mother hurt
- child tracks adult tension

### Turn 4: Symptom Function And Reframe

Focus:

- School refusal as a way of slowing separation and morning panic.
- Do not blame the child.
- Translate behavior into relational signal.

Family emotional state:

- mother moves from guilt to curiosity
- child becomes slightly more able to speak
- teacher recognizes pressure effects

### Turn 5: Small Experiment And Next Session

Focus:

- One small morning routine experiment.
- Everyone has one role.
- Make the plan observable next week.

Family emotional state:

- not "solved"
- slightly safer
- concrete next step

## Choice Design

Each turn should have three choices:

1. Recommended systemic choice
2. Plausible but partial/too narrow choice
3. Risky choice that fixes blame or closes exploration

The wrong choices should be believable:

- not cartoonishly bad
- not insulting
- something a rushed trainee might actually say

Example choice style:

Good:

```text
"아침 장면을 순서대로 같이 살펴보겠습니다. 주형이가 멈추면 어머니는 무엇을 하게 되고, 그다음 학교 연락은 어떻게 이어지나요?"
```

Too narrow:

```text
"우선 등교 준비 규칙을 더 분명하게 정해보겠습니다."
```

Risky:

```text
"주형이가 학교에 가겠다고 여기서 약속하는 게 먼저일 것 같습니다."
```

## Output Requirements

After editing:

1. Summarize what text was changed.
2. List the exact code locations changed.
3. Run a quick search to confirm old awkward phrases are gone:

```powershell
rg -n "이주형이에요|성빈이 엄마|초점:|누구 잘못|무조건|약속해볼" Assets/Scripts/FamilyTherapyPracticumGame.cs
```

4. Build or at minimum run a compile check if practical.
5. Do not leave syntax errors.

## Important Existing User Preferences

- User does not want `초점:` text shown under choices.
- User wants all FT-001 characters present in scenes.
- User wants commercial-game quality, not demo language.
- User does not want repeated "demo" framing.
- User wants Korean text, not English UI text.
- User prefers natural family therapy training game language over academic lecture tone.

## Deliverable

Modify `Assets/Scripts/FamilyTherapyPracticumGame.cs` directly.

Do not only propose changes. Implement the rewritten FT-001 text.

