# FT002~FT010 v3 Manifest 기반 이미지 생성 창 명령

아래 블록을 이미지 생성 전용 창에 그대로 붙여넣는다.

```text
너는 Family Therapy Practicum의 FT002~FT010 CG 제작 담당이다.

이번 작업은 FT002~FT010만 한다. FT001은 건드리지 않는다.
현재 목표는 상용 비주얼노벨 수준으로, manifest에 정의된 모든 required CG slot을 실제 PNG로 제작하는 것이다.

먼저 아래 두 파일을 읽어라.

1. 제작 브리프
C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity\Docs\FT002_FT010_REQUIRED_CG_SLOT_PRODUCTION_BRIEF_2026-06-10.md

2. 자동 생성 v3 manifest
C:\Users\이종호\AppData\LocalLow\Family Therapy Practicum\Family Therapy Practicum\FamilyTherapyPracticumExports\family_therapy_practicum_cg_slot_manifest.json

manifest는 반드시 `manifestVersion: 3`이어야 한다.

작업 범위:

- FT002: 56장
- FT003: 41장
- FT004: 41장
- FT005: 41장
- FT006: 41장
- FT007: 41장
- FT008: 42장
- FT009: 41장
- FT010: 41장

총 385장이다.

제작 순서:

1. FT002 `exists:false` 슬롯 35장만 먼저 만든다. `exists:true` 슬롯은 품질 재검수에서 실패했을 때만 재생성한다.
2. 그 다음 FT009, FT008, FT010을 만든다.
3. 그 다음 FT003~FT007을 만든다.
4. 한 번에 전부 만들지 말고, 한 case 안에서도 10장 단위 batch로 만들고 contact sheet를 만든다.
5. batch마다 아래 기준으로 자체 검수한 뒤 통과본만 `filePath`에 저장한다.

각 slot에서 사용할 필드:

- 저장 위치: `slot.filePath`
- 장면 종류: `slot.slotType`
- 케이스 구도/소품: `slot.caseSpecificVisualBrief`, `slot.keyProps`
- 금지 표현: `slot.safetyNegativePrompt`, manifest의 `globalNegativePrompt`
- 대사 CG: `slot.speakerId`, `slot.expressionId`, `slot.text`, `slot.supervisorNote`, `slot.promptHint`
- 반응 CG: `slot.choiceLabel`, `slot.feedback`, `slot.familyReaction`, `slot.reactionSpeakerId`, `slot.reactionExpressionId`, `slot.promptHint`
- 선택지 대기 CG: `slot.turnTitle`, `slot.composition`, `slot.promptHint`
- 엔딩 CG: `slot.endingKey`, `slot.endingLabel`, `slot.routeEndingVisualState`, `slot.promptHint`

프롬프트 결합 순서:

1. `case.masterShotPolicy`
2. `case.caseSpecificVisualBrief`
3. `slot.composition`
4. `slot.promptHint`
5. `slot.keyProps`
6. `slot.safetyNegativePrompt`
7. `globalNegativePrompt`

절대 규격:

- 모든 최종 이미지는 네이티브 1600x900 PNG.
- 정사각형 생성 후 16:9로 늘리기 금지.
- 이미지 안에 텍스트, 자막, UI, 말풍선, 워터마크 금지.
- 하단 25~30%는 게임 대사창 영역이므로 핵심 얼굴/손/소품을 두지 않는다.
- 각 case마다 family/session master shot과 supervisor master shot을 먼저 고정하고, 이후 컷에서 좌석/화각/인물 크기를 흔들지 않는다.
- 케이스별 소품은 slot마다 억지로 다 넣지 말고, 해당 장면에 맞는 1~3개만 자연스럽게 배치한다.

중요한 게이트:

- `exists: true`인 슬롯은 이미 파일이 있다는 뜻이지만, 품질이 낮거나 비율이 깨졌으면 재생성한다.
- `exists: false`인 슬롯은 반드시 새로 생성한다.
- 최종 파일명은 manifest의 `filePath` 그대로 사용한다.
- 소스/후보/탈락본은 아래 폴더에 보관한다.

C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity\Docs\GeneratedSources\FT002_FT010_V3_CG_20260610

완료 보고 형식:

FT002~FT010 v3 CG production report
Case:
Batch:
Generated:
Accepted:
Rejected:
Final folder:
Contact sheet:
Manifest used:
C:\Users\이종호\AppData\LocalLow\Family Therapy Practicum\Family Therapy Practicum\FamilyTherapyPracticumExports\family_therapy_practicum_cg_slot_manifest.json
Notes:
- Native 1600x900 confirmed: yes/no
- Master shot locked: yes/no
- Case visual brief followed: yes/no
- Safety negative prompt followed: yes/no
- Any text/UI/watermark: yes/no
```

## 이 창에서 확인할 일

이미지 생성 창이 파일을 넣은 뒤 아래 명령으로 audit을 다시 돌린다.

```powershell
.\Builds\Windows\FamilyTherapyPracticum.exe -familyTherapyVnDataAudit -batchmode -nographics -logFile .\Logs\ft002_ft010_after_cg_import_audit.log
```

최종 통과 기준:

```text
FT002~FT010 전부 availableCgSlotCount == requiredCgSlotCount
FT002~FT010 전부 missingCgSlotCount == 0
```
