# FT001 Completion Progress 2026-06-08

## Summary

FT-001 is now the only active asset-completion target. Other cases, supervisors, backgrounds, UI, and bulk generation remain paused until FT-001 is complete and reviewed.

## Completion Target

Authoritative target list: `Docs/FT001_COMPLETION_TARGET_2026-06-08.md`

Expected final FT001 character sprite count: 26.

## Current Runtime Candidates

- `ft001_mother_neutral_phase0.png`
- `ft001_mother_worried_phase1.png`
- `ft001_mother_anxious_phase1.png`
- `ft001_mother_exhausted_phase1.png`
- `ft001_mother_defensive_phase1.png`
- `ft001_mother_tearful_phase1.png`
- `ft001_mother_listening_phase1.png`
- `ft001_mother_softened_phase1.png`
- `ft001_child_neutral_phase1.png`
- `ft001_child_anxious_phase1.png`
- `ft001_child_hesitant_phase1.png`
- `ft001_child_listening_phase1.png`
- `ft001_child_scared_phase1.png`
- `ft001_child_quiet_phase1.png`
- `ft001_child_relieved_phase1.png`
- `ft001_child_withdrawn_phase1.png`
- `ft001_grandmother_neutral_phase1.png`
- `ft001_grandmother_worried_phase1.png`
- `ft001_grandmother_critical_phase1.png`
- `ft001_grandmother_defensive_phase1.png`
- `ft001_grandmother_stubborn_phase1.png`
- `ft001_grandmother_softened_phase1.png`
- `ft001_teacher_neutral_phase1.png`
- `ft001_teacher_concerned_phase1.png`
- `ft001_teacher_procedural_phase1.png`
- `ft001_teacher_softened_phase1.png`

Current accepted/runtime FT001 character candidate count: 26.

## Attempted This Step

- Tried to generate `ft001_teacher_neutral`.
- Built-in `image_gen` failed twice with server errors.
- No placeholder, local recolor, or pixel edit was used as a substitute.

## Attempted 2026-06-09

- Re-read `FT001_COMPLETION_TARGET_2026-06-08.md` and current runtime `Characters/FT001` assets.
- Detected 8 reappeared UI placeholder PNGs under `Assets/Resources/VN/UI` and quarantined them at `Assets/_PlaceholderAudit/VN_2026-06-09_ReappearedUI/`.
- Generated and accepted `ft001_teacher_neutral_phase1.png`.
- Tried to generate `ft001_teacher_concerned`; the built-in image generation call failed twice.
- No placeholder, local recolor, or pixel edit was used as a substitute for `teacher_concerned`.
- Review sheet: `Docs/Phase1_FT001_Family_Review_Teacher_2026-06-09.png`.
- Detected the same 8 UI placeholder PNGs reappearing again and quarantined that second reappearance at `Assets/_PlaceholderAudit/VN_2026-06-09_ReappearedUI_2/`.
- Final validation for this step: runtime PNG count 8, `Characters/FT001` count 6, missing `.png.meta` count 0, PNG decode failures 0.

## Attempted 2026-06-09 Late Batch

- Re-read the FT001 target and verified runtime candidates before generating.
- Tried to generate `ft001_teacher_concerned`; the built-in image generation call failed twice.
- Generated and accepted `ft001_child_withdrawn_phase1.png`.
- No placeholder, local recolor, or pixel edit was used as a substitute for `teacher_concerned`.
- Review sheet: `Docs/Phase1_FT001_Family_Review_ChildWithdrawn_2026-06-09.png`.
- Detected the same 8 UI placeholder PNGs reappearing again and quarantined that third reappearance at `Assets/_PlaceholderAudit/VN_2026-06-09_ReappearedUI_3/`.
- Final validation for this step: runtime PNG count 9, `Characters/FT001` count 7, missing `.png.meta` count 0, PNG decode failures 0.

## Attempted 2026-06-09 Child Anxious Batch

- Detected 2 UI placeholder PNGs reappearing and quarantined them at `Assets/_PlaceholderAudit/VN_2026-06-09_ReappearedUI_4/`.
- Tried to generate `ft001_child_anxious`; the first built-in image generation call failed with a server error.
- Generated and accepted `ft001_child_anxious_phase1.png` on retry.
- Review sheet: `Docs/Phase1_FT001_Family_Review_ChildAnxious_2026-06-09.png`.
- Final validation for this step: runtime PNG count 10, `Characters/FT001` count 8, missing `.png.meta` count 0, PNG decode failures 0.

## Attempted 2026-06-09 Child Scared/Hesitant Batch

- Verified runtime state before generation: `Characters/FT001` count 8 and no UI placeholder files in runtime.
- Generated and accepted `ft001_child_scared_phase1.png`.
- Generated and accepted `ft001_child_hesitant_phase1.png`.
- Review sheet: `Docs/Phase1_FT001_Family_Review_ChildScaredHesitant_2026-06-09.png`.
- Final validation for this step: runtime PNG count 12, `Characters/FT001` count 10, missing `.png.meta` count 0, PNG decode failures 0.

## Attempted 2026-06-09 Child Completion Batch

- Verified runtime state before generation: `Characters/FT001` count 10 and no UI placeholder files in runtime.
- Tried to generate `ft001_child_listening`; the first built-in image generation call failed with a server error.
- Generated and accepted `ft001_child_listening_phase1.png` on retry.
- Generated and accepted `ft001_child_quiet_phase1.png`.
- Tried to generate `ft001_child_relieved`; the first built-in image generation call failed with a server error.
- Generated and accepted `ft001_child_relieved_phase1.png` on retry.
- Review sheet: `Docs/Phase1_FT001_Family_Review_ChildComplete_2026-06-09.png`.
- Final validation for this step: runtime PNG count 15, `Characters/FT001` count 13, missing `.png.meta` count 0, PNG decode failures 0.

## Attempted 2026-06-09 Grandmother Completion Batch

- Verified runtime state before generation: `Characters/FT001` count 13.
- Detected 8 UI placeholder PNGs reappearing and quarantined them at `Assets/_PlaceholderAudit/VN_2026-06-09_ReappearedUI_5/`.
- Generated and accepted `ft001_grandmother_worried_phase1.png`.
- Tried to generate `ft001_grandmother_critical`; the first built-in image generation call failed with a server error.
- Generated and accepted `ft001_grandmother_softened_phase1.png`.
- Generated and accepted `ft001_grandmother_critical_phase1.png` on retry.
- Generated and accepted `ft001_grandmother_defensive_phase1.png`.
- Generated and accepted `ft001_grandmother_stubborn_phase1.png`.
- Copied selected chroma-key source images to `Docs/GeneratedSources/FT001_Grandmother_2026-06-09/`.
- Review sheet: `Docs/Phase1_FT001_Family_Review_GrandmotherComplete_2026-06-09.png`.
- Detected the same 8 UI placeholder PNGs reappearing again and quarantined that sixth reappearance at `Assets/_PlaceholderAudit/VN_2026-06-09_ReappearedUI_6/`.
- Final validation for this step: runtime PNG count 20, `Characters/FT001` count 18, missing `.png.meta` count 0, PNG decode failures 0.

## Attempted 2026-06-09 Teacher Completion Batch

- Verified existing `ft001_teacher_neutral_phase1.png` as the teacher base identity.
- Generated and accepted `ft001_teacher_concerned_phase1.png`.
- Generated and accepted `ft001_teacher_procedural_phase1.png`.
- Tried to generate `ft001_teacher_softened`; the first built-in image generation call failed with a server error.
- Generated and accepted `ft001_teacher_softened_phase1.png` on retry.
- Copied selected chroma-key source images to `Docs/GeneratedSources/FT001_Teacher_2026-06-09/`.
- Review sheet: `Docs/Phase1_FT001_Family_Review_TeacherComplete_2026-06-09.png`.
- Detected the same 8 UI placeholder PNGs reappearing again and quarantined that seventh reappearance at `Assets/_PlaceholderAudit/VN_2026-06-09_ReappearedUI_7/`.
- Final validation for this step: runtime PNG count 23, `Characters/FT001` count 21, missing `.png.meta` count 0, PNG decode failures 0.

## Attempted 2026-06-09 Mother Final Batch

- Created current mother comparison sheet: `Docs/Phase1_FT001_Mother_CurrentBeforeFinal_2026-06-09.png`.
- Confirmed the existing mother candidates are too visually similar for final coverage, especially `ft001_mother_angry_contained_phase1.png`.
- Tried to generate `ft001_mother_anxious`; the built-in image generation call failed with a server error.
- Tried to generate `ft001_mother_exhausted`; the built-in image generation call failed with a server error.
- Tried to generate `ft001_mother_tearful`; the built-in image generation call failed with a server error.
- No placeholder, local recolor, duplicated sprite, or pixel edit was used as a substitute for the missing mother expressions.
- Current validation after the completed teacher step remains: runtime PNG count 23, `Characters/FT001` count 21, missing `.png.meta` count 0, PNG decode failures 0.

## Attempted 2026-06-09 Mother Retry

- Re-verified current runtime state before retry: runtime PNG count 23, `Characters/FT001` count 21, missing `.png.meta` count 0, PNG decode failures 0.
- Confirmed no UI placeholder files were present in `Assets/Resources/VN/UI` before retry.
- Retried `ft001_mother_anxious`; the built-in image generation call failed with a server error.
- Retried `ft001_mother_exhausted` with a simplified prompt; the built-in image generation call failed with a server error.
- Retried `ft001_mother_defensive` with a simplified prompt; the built-in image generation call failed with a server error.
- No project asset was created or modified as a substitute for these failed mother expressions.

## Attempted 2026-06-09 Final Mother Completion

- Re-verified current runtime state before retry: runtime PNG count 23, `Characters/FT001` count 21, missing `.png.meta` count 0, PNG decode failures 0, and no UI placeholder files in runtime.
- Generated and accepted `ft001_mother_anxious_phase1.png`.
- Tried to generate `ft001_mother_exhausted`; the first built-in image generation call failed with a server error.
- Generated and accepted `ft001_mother_defensive_phase1.png`.
- Generated and accepted `ft001_mother_tearful_phase1.png`.
- Generated and accepted `ft001_mother_listening_phase1.png`.
- Generated and accepted `ft001_mother_softened_phase1.png`.
- Generated and accepted `ft001_mother_exhausted_phase1.png` on retry.
- Copied selected chroma-key source images to `Docs/GeneratedSources/FT001_Mother_2026-06-09_Final/`.
- Quarantined superseded `ft001_mother_angry_contained_phase1.png` and its meta at `Assets/_PlaceholderAudit/FT001_Mother_Superseded_2026-06-09/`.
- Review sheet: `Docs/Phase1_FT001_Family_Review_MotherComplete_2026-06-09.png`.

## Attempted 2026-06-09 Child Distinctness Cleanup

- Full 26-sprite review showed the previous child candidates were target-complete but still too visually similar at contact-sheet scale.
- Generated and accepted stronger replacements for `ft001_child_anxious_phase1.png`, `ft001_child_hesitant_phase1.png`, `ft001_child_listening_phase1.png`, `ft001_child_relieved_phase1.png`, and `ft001_child_withdrawn_phase1.png`.
- Tried to regenerate `ft001_child_quiet`; the built-in image generation call failed with server errors, so the existing quiet candidate was retained after review.
- Quarantined superseded child files and metas at `Assets/_PlaceholderAudit/FT001_Child_Superseded_2026-06-09/`.
- Copied selected chroma-key source images to `Docs/GeneratedSources/FT001_Child_2026-06-09_DistinctReplacements/`.
- Review sheets: `Docs/Phase1_FT001_Family_Review_ChildDistinct_2026-06-09.png` and `Docs/Phase1_FT001_Full_Review_26Sprites_2026-06-09.png`.
- Updated `Assets/Scripts/FamilyTherapyPracticumGame.cs` so `RequiredVnAssetPaths` includes the missing FT-001 target expressions.
- Final validation for this step: runtime PNG count 28, `Characters/FT001` count 26, target count 26, missing targets 0, extra FT001 files 0, missing `.png.meta` count 0, PNG decode failures 0, FT001 sprites without alpha 0.

## Remaining High-Priority Gaps

- No FT-001 character sprite coverage gaps remain.
- Final review sheet exists at `Docs/Phase1_FT001_Full_Review_26Sprites_2026-06-09.png`.
- Remaining broader-app assets outside FT-001, such as supervisors, UI skins, and additional backgrounds, are intentionally paused and not counted as FT-001 completion work.

## Next Recommended Batch

- Perform user visual review of `Docs/Phase1_FT001_Full_Review_26Sprites_2026-06-09.png`.
- If approved, proceed to the next explicitly requested scope after FT-001.
