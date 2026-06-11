# FT002 T02 Batch02 CG Import Report

## Summary

FT002 Turn 2 CG batch02 was produced and imported into Unity Resources.

Runtime folder:

```text
Assets/Resources/VN/EventCG/FT002/
```

Source archive:

```text
Docs/GeneratedSources/FT002_V3_CG_20260610/source/batch02/
```

Contact sheet:

```text
Docs/GeneratedSources/FT002_V3_CG_20260610/contact_sheets/ft002_t02_batch02_contact_sheet.png
```

## Imported Slots

| Slot | File |
| --- | --- |
| dialogue | `ft002_t02_l01_grandmother.png` |
| dialogue | `ft002_t02_l02_grandson.png` |
| dialogue | `ft002_t02_l03_grandfather.png` |
| dialogue | `ft002_t02_l04_grandmother.png` |
| dialogue | `ft002_t02_l05_grandson.png` |
| dialogue | `ft002_t02_l06_bowen.png` |
| choice idle | `ft002_t02_choice_idle.png` |
| reaction A | `ft002_t02_reaction_a_grandmother.png` |
| reaction B | `ft002_t02_reaction_b_grandson.png` |
| reaction C | `ft002_t02_reaction_c_grandson.png` |

## Validation

Unity build:

```text
Logs/ft002_t02_batch02_build.log
Build Finished, Result: Success.
Family Therapy Practicum build result: Succeeded
```

VN data audit:

```text
Logs/ft002_t02_batch02_audit.log
```

Smoke:

```text
Logs/ft002_t02_batch02_smoke.log
FAMILY_THERAPY_PRACTICUM_SMOKE completed=true
```

Audit counts after import:

| Scope | Required | Available | Missing |
| --- | ---: | ---: | ---: |
| FT002 | 56 | 20 | 36 |
| FT002~FT010 total | 384 | 20 | 364 |

## Review Notes

- Turn 2 family master shot remains consistent with Turn 1.
- Bowen supervisor shot uses the separate Ahn Woojin view.
- Reaction C required a neutral prompt due image-generation safety filtering; final image still captures the route state: family closes down and the center youth withdraws.
- All runtime PNGs are normalized to `1600x900`.

