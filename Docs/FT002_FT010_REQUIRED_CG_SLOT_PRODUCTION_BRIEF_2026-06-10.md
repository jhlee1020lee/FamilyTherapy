# FT002~FT010 Required CG Slot Production Brief

이 문서는 이미지 생성 전용 창에 전달할 수 있는 FT002~FT010 CG 제작 기준이다.

## 현재 코드 상태

FT002~FT010은 이제 대사/반응/선택지 대기/엔딩 CG 슬롯이 코드에 연결되어 있다.

최신 audit 결과: 2026-06-10 Round 3 runtime audit.

| Case | Required CG slots | Available | Missing |
| --- | ---: | ---: | ---: |
| FT002 | 56 | 21 | 35 |
| FT003 | 41 | 0 | 41 |
| FT004 | 41 | 0 | 41 |
| FT005 | 41 | 0 | 41 |
| FT006 | 41 | 0 | 41 |
| FT007 | 41 | 0 | 41 |
| FT008 | 42 | 0 | 42 |
| FT009 | 41 | 0 | 41 |
| FT010 | 41 | 0 | 41 |

총 필요 수량: 385장.
현재 사용 가능: 21장.
현재 누락: 364장.

현재 런타임은 파일이 있으면 자동 사용하고, 없으면 기본 상담실 배경으로 fallback한다.

## 자동 생성된 슬롯 manifest

최신 빌드에서 아래 파일이 자동 생성된다.

```text
C:\Users\이종호\AppData\LocalLow\Family Therapy Practicum\Family Therapy Practicum\FamilyTherapyPracticumExports\family_therapy_practicum_cg_slot_manifest.json
```

현재 manifest는 `manifestVersion: 3`다. 이미지 생성 창은 이 manifest의 `cases[].slots[]`를 작업 큐로 삼고, 각 slot의 `filePath`를 실제 저장 경로로 사용하면 된다.

각 slot에는 파일 경로뿐 아니라 이미지 생성에 필요한 대사/화자/표정/선택/반응/엔딩 정보가 같이 들어 있다.

```json
{
  "resourcePath": "VN/EventCG/FT002/ft002_t01_l01_grandmother",
  "filePath": "Assets/Resources/VN/EventCG/FT002/ft002_t01_l01_grandmother.png",
  "exists": false,
  "slotType": "dialogue",
  "caseId": "FT-002",
  "caseTitle": "가족 상담 사례",
  "familyType": "조손가족",
  "recommendedTheoryId": "bowen",
  "caseSpecificVisualBrief": "Bowen 조손가족. 조모의 통제는 악역성이 아니라 상실 불안에서 나온다...",
  "keyProps": "11:37 휴대폰 화면; 조부의 접힌 신문; 거실/상담실 거리감; 아버지 이야기를 피하는 침묵",
  "safetyNegativePrompt": "do not portray the grandson as delinquent villain...",
  "turnNumber": 1,
  "lineNumber": 1,
  "turnTitle": "초기 합류와 문제 정의",
  "speakerId": "ft002_grandmother",
  "expressionId": "anxious",
  "position": "left",
  "text": "애가 밤 열한 시가 넘어도 안 들어와요. 그러니 휴대폰이라도 봐야 마음이 놓이죠. 안 그러면 무슨 일이 났나 싶어서요.",
  "supervisorNote": "처음부터 한 사람을 탓하지 말고, 걱정이 어떤 모양으로 표현되는지 보세요.",
  "composition": "family/session master shot with speaker emphasized but other participants still seated consistently",
  "promptHint": "Use the stable family/session master shot; keep all required participants seated in the same positions. Emphasize speaker ft002_grandmother with expression anxious..."
}
```

slotType은 `dialogue`, `reaction`, `choice_idle`, `ending` 중 하나다.

slotType별로 주로 볼 필드는 다르다.

- `dialogue`: `speakerId`, `expressionId`, `position`, `text`, `supervisorNote`, `promptHint`
- `reaction`: `choiceLabel`, `theoryId`, `interventionType`, `quality`, `feedback`, `familyReaction`, `reactionSpeakerId`, `reactionExpressionId`, `promptHint`
- `choice_idle`: `turnTitle`, `composition`, `promptHint`
- `ending`: `endingKey`, `endingLabel`, `routeEndingVisualState`, `composition`, `promptHint`

모든 slot에는 `caseSpecificVisualBrief`, `keyProps`, `safetyNegativePrompt`가 반복해서 들어간다. 생성 프롬프트를 만들 때는 반드시 아래 순서로 결합한다.

```text
1. cases[].masterShotPolicy
2. cases[].caseSpecificVisualBrief
3. slot.composition
4. slot.promptHint
5. slot.keyProps
6. slot.safetyNegativePrompt
7. globalNegativePrompt
```

이미지를 저장한 뒤 다시 audit을 돌리면 `availableCgSlotCount`가 증가하고 `missingCgSlotCount`가 감소해야 한다.

최신 검증 로그:

```text
Logs/ft002_ft010_round3_build_rerun.log
Logs/ft002_ft010_round3_vn_audit_rerun.log
Logs/ft002_ft010_round3_smoke_rerun.log
```

## 저장 위치

각 사례별 최종 CG는 아래 폴더에 저장한다.

```text
Assets/Resources/VN/EventCG/FT002/
Assets/Resources/VN/EventCG/FT003/
Assets/Resources/VN/EventCG/FT004/
Assets/Resources/VN/EventCG/FT005/
Assets/Resources/VN/EventCG/FT006/
Assets/Resources/VN/EventCG/FT007/
Assets/Resources/VN/EventCG/FT008/
Assets/Resources/VN/EventCG/FT009/
Assets/Resources/VN/EventCG/FT010/
```

Unity Resources path 기준은 아래처럼 잡힌다.

```text
VN/EventCG/FT002/ft002_t01_l01_grandmother
VN/EventCG/FT002/ft002_t01_reaction_a_grandmother
VN/EventCG/FT002/ft002_t01_choice_idle
VN/EventCG/FT002/ft002_ending_a_integrated
```

실제 파일은 `.png`로 저장한다.

```text
Assets/Resources/VN/EventCG/FT002/ft002_t01_l01_grandmother.png
```

## 파일명 규칙

### 대사 CG

```text
ftXXX_tYY_lZZ_speaker.png
```

예:

```text
ft003_t01_l01_mother.png
ft006_t03_l02_mother.png
ft010_t05_l03_supervisor.png
```

### 선택 반응 CG

```text
ftXXX_tYY_reaction_a_speaker.png
ftXXX_tYY_reaction_b_speaker.png
ftXXX_tYY_reaction_c_speaker.png
```

예:

```text
ft004_t03_reaction_a_spouse.png
ft008_t01_reaction_b_teen.png
ft009_t05_reaction_c_spouse.png
```

### 선택지 대기 CG

```text
ftXXX_tYY_choice_idle.png
```

예:

```text
ft005_t02_choice_idle.png
```

### 엔딩 CG

각 사례마다 최소 아래 6종을 만든다.

```text
ftXXX_ending_a_integrated.png
ftXXX_ending_b_repaired.png
ftXXX_ending_b_partial.png
ftXXX_ending_c_key_risk_unrepaired.png
ftXXX_ending_d_closed_or_harmful.png
ftXXX_ending_d_safety_unresolved.png
```

FT009는 산후 안전 사례라 `d_safety_unresolved`가 특히 중요하다.

## 제작 우선순위

한 번에 385장을 모두 만들기보다 아래 순서로 진행한다.

1. FT002 `exists:false` 슬롯 35장
2. FT009 전체 41장
3. FT008 전체 42장
4. FT010 전체 41장
5. FT003~FT007 각 41장

이유:

- FT002는 이미 대사 밀도 30줄이라 가장 완성형에 가깝다.
- FT009는 안전/위기 판단이 포함되어 교육적 중요도가 높다.
- FT008/FT010은 이야기치료와 해결중심의 게임적 차이가 잘 드러난다.
- FT003~FT007은 같은 방식으로 순차 확장한다.

## 이미지 규격

모든 CG:

```text
1600x900
16:9
PNG
no UI
no subtitle
no speech bubble
no watermark
bottom 25-30% visually clean for dialogue UI
```

정사각형으로 만든 뒤 늘리지 않는다.

## 공통 스타일

```text
polished commercial Korean visual novel CG, realistic painterly 2D, restrained emotional acting, warm counseling-room lighting, natural seated counseling posture, consistent character identity, cinematic but readable composition, no text, no UI, no watermark
```

## 구도 원칙

FT002~FT010은 FT001처럼 “같은 장면에서 배우들의 표정과 손짓만 바뀌는” 방식을 따른다.

각 사례마다 먼저 2개의 master shot을 확정한다.

1. 가족/내담자 쪽 master shot
2. 슈퍼바이저 쪽 master shot

가족/내담자 대사와 선택 반응은 family master shot을 유지한다.

슈퍼바이저 대사는 supervisor master shot을 유지한다.

엔딩 CG는 같은 상담실 안에서 회기 결과의 정서 상태를 보여준다. 별도 판타지/상징 이미지로 만들지 않는다.

## audit 통과 기준

이미지를 넣은 뒤 아래 명령으로 확인한다.

```powershell
.\Builds\Windows\FamilyTherapyPracticum.exe -familyTherapyVnDataAudit -batchmode -nographics -logFile .\Logs\ft002_ft010_cg_asset_audit.log
```

통과 기준:

```text
availableCgSlotCount == requiredCgSlotCount
missingCgSlotCount == 0
```

현재 감사 기준은 총 385장 중 FT002 21장이 사용 가능하고, FT003~FT010은 아직 사용 가능 CG가 없다. 이미지가 들어오면 audit 숫자가 바로 줄어야 한다.
