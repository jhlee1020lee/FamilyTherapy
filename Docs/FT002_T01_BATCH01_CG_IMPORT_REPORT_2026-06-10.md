# FT002 T01 Batch01 CG Import Report

## Summary

FT002 Turn 1 CG batch01 was produced and imported into Unity Resources.

Runtime folder:

```text
Assets/Resources/VN/EventCG/FT002/
```

Source archive:

```text
Docs/GeneratedSources/FT002_V3_CG_20260610/source/batch01/
```

Contact sheet:

```text
Docs/GeneratedSources/FT002_V3_CG_20260610/contact_sheets/ft002_t01_batch01_contact_sheet.png
```

## Imported Slots

| Slot | File |
| --- | --- |
| dialogue | `ft002_t01_l01_grandmother.png` |
| dialogue | `ft002_t01_l02_grandson.png` |
| dialogue | `ft002_t01_l03_grandfather.png` |
| dialogue | `ft002_t01_l04_grandmother.png` |
| dialogue | `ft002_t01_l05_grandson.png` |
| dialogue | `ft002_t01_l06_bowen.png` |
| choice idle | `ft002_t01_choice_idle.png` |
| reaction A | `ft002_t01_reaction_a_grandmother.png` |
| reaction B | `ft002_t01_reaction_b_grandson.png` |
| reaction C | `ft002_t01_reaction_c_grandmother.png` |

## Validation

Unity build:

```text
Logs/ft002_t01_batch01_build.log
Build Finished, Result: Success.
Family Therapy Practicum build result: Succeeded
```

VN data audit:

```text
Logs/ft002_t01_batch01_audit.log
```

Audit counts after import:

| Scope | Required | Available | Missing |
| --- | ---: | ---: | ---: |
| FT002 | 56 | 10 | 46 |
| FT002~FT010 total | 384 | 10 | 374 |

## Review Notes

- Family/session master shot is stable enough for T01 batch use.
- Supervisor shot for `supervisor_bowen` is separated as an opposite-side supervisor master shot.
- All final runtime PNGs were normalized to `1600x900`.
- No slot should be treated as final project completion until all FT002~FT010 CG slots reach `missingCgSlotCount == 0`.

