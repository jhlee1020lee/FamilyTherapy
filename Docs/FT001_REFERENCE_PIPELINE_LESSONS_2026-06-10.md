# FT-001 Reference Pipeline Lessons

## Why This Exists

FT-001 exposed the main risk in AI-assisted VN asset generation: a single prompt can produce a beautiful image, but independent prompt-only generation drifts across a full case. Faces, age, clothing, room geometry, and role readability change just enough to break the feeling that the player is watching one continuous therapy session.

These lessons should be reused before producing other stages.

## Main Lessons

- Do not bulk-generate gameplay CGs from text-only prompts.
- Build a reference pack before gameplay CG generation.
- Treat a good-looking first image as a style candidate, not as production coverage.
- Generate character identity, room, seating, active-speaker, expression, and supervisor-view references separately.
- Keep all final full-scene CGs at the target runtime size from the beginning. For this pass that size is `1600x900`.
- Archive originals and normalized finals separately so rejected/accepted decisions can be audited.
- Use a manifest and contact sheet. A folder full of images is not enough evidence that coverage is complete.

## Minimum Reference Pack For Future Stages

Each future case should have these before bulk CG generation:

1. Cast identity sheet.
2. Empty room or stage background.
3. Master seating/composition sheet.
4. Identity close-up sheet per recurring character.
5. Expression sheet per recurring character.
6. Active-speaker master scene for the most important speakers.
7. Supervisor/counselor identity sheet if that person speaks on screen.
8. Supervisor/counselor viewpoint master if that person appears in-session.
9. Contact sheet of the final reference pack.
10. JSON or Markdown manifest listing every approved reference and every required gameplay CG.

## FT-001 Specific Decisions

- FT-001 gameplay should be driven by reference-locked all-cast CGs, not one-speaker-at-a-time sprites.
- The family four-person frame remains the default for family-member dialogue:
  - left: 오선진
  - center-left: 박성빈
  - center-right/front: 이주형
  - right: 서건창
- 김혜성 is not a fifth family member. In his speaking scenes he should read as therapist/supervisor, seated across from or near the family, with the family still visible when composition allows.
- 서건창 and 김혜성 must stay visually distinct:
  - 서건창: school representative, tie/blazer/clipboard, procedural concern.
  - 김혜성: therapist/supervisor, warmer clinical presence, softer professional styling.
- The lower 25-30% of every full-scene CG must remain clean for the dialogue UI.

## Prompting Lessons

- Strong negative constraints are necessary but not sufficient. Repetition of identity and role is more important than broad style wording.
- Avoid unsafe or overcharged wording for tense child scenes. Phrase difficult moments as restrained clinical-session states, such as "quietly looking down", "guarded", or "difficult conversation" rather than intensifying child distress language.
- For active speaker shots, specify who is speaking, how the others are listening, and that the other participants remain seated and visible.
- For supervisor shots, specify the camera relationship: supervisor in foreground/side foreground, family visible across the room or over the shoulder.
- Do not allow labels inside reference images. Put names and role notes in the manifest/documentation instead.

## QA Lessons

Reject or regenerate if:

- any required participant is missing;
- a character is standing in a therapy-session CG;
- the child is hidden, too small, aged up, or gender-ambiguous;
- the teacher reads as a family member;
- the supervisor reads as the teacher;
- mother and grandmother become too similar;
- bottom dialogue area is crowded;
- room layout changes enough to feel like a different counseling center;
- expression differences are unreadable or cartoonishly exaggerated;
- text, UI, watermark, or extra characters appear.

## Tooling Lessons

- Long Windows paths can exceed 260 characters under `Docs/GeneratedSources/...`; Python file operations may need `\\?\\` long-path prefixes.
- Always verify generated image dimensions after copying into the workspace.
- Keep source images in `selected_sources/` and normalized approved references in `references/`.
- A contact sheet should be regenerated after every reference-pack expansion.
