# FT001 New Capture Status

## What Changed

- Applied the new FT-001 line-by-line locked CG set from:
  - `Assets/Resources/VN/EventCG/FT001_LineByLineLocked`
- Verified final CG count:
  - `50/50` PNG files
- Verified final CG dimensions:
  - `50/50` are exactly `1600x900`
- Rebuilt the Windows executable so the new Resources images are packed into runtime:
  - `Builds/Windows/FamilyTherapyPracticum.exe`
- Generated fresh `1600x900` visual audit captures.

## New Capture Folder

Runtime capture output:

`C:\Users\이종호\AppData\LocalLow\Family Therapy Practicum\Family Therapy Practicum\FamilyTherapyPracticumExports\visual_audit_1600x900`

Copied presentation copy:

`Presentation/FT001_ClaudeDesign_PPT_Package_2026-06-10/03_runtime_screenshots_1600x900`

## Old Capture Cleanup

Previous visual audit folders were moved to:

`C:\Users\이종호\AppData\LocalLow\Family Therapy Practicum\Family Therapy Practicum\FamilyTherapyPracticumExports\archive_before_ft001_line_by_line_20260610_223150`

Nothing was deleted; old captures are archived for comparison.

## Verification

Build log:

`Logs/ft001_line_by_line_rebuild.log`

Runtime visual audit log:

`Logs/ft001_line_by_line_visual_1600.log`

Visual audit result:

`visual_audit_result.json`

Summary:

- `completed`: true
- `screenWidth`: 1600
- `screenHeight`: 900
- all captured screens: `offscreenRectCount=0`
- all captured screens: `textOverflowCount=0`

## Notes

- The new CGs are displayed as full-scene 16:9 images and no longer show the earlier stretched/square-image problem.
- The bottom dialogue UI still covers the lower part of the CG by design. This is acceptable for current prototype captures, but final UI art can still be refined later.
