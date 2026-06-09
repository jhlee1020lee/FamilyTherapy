# UI Asset Production Plan

- 작성일: 2026-06-09
- 대상 프로젝트: `99_FamilyTherapyPracticumUnity`
- 목적: 현재 네모 박스 기반 UI를 상용 비주얼노벨/상담 시뮬레이션에 어울리는 이미지 UI로 교체한다.

## 1. 현재 상태

현재 Unity 코드는 아래 8개 UI 리소스를 이미 기대한다.

```text
VN/UI/dialogue_box
VN/UI/speaker_nameplate
VN/UI/choice_card_question
VN/UI/choice_card_intervention
VN/UI/supervisor_note_panel
VN/UI/case_file_panel
VN/UI/metrics_hud
VN/UI/session_result_sheet
```

하지만 현재 `Assets/Resources/VN/UI`에는 최종 UI PNG가 없다. 그래서 런타임은 단색 패널과 버튼으로 fallback 표시된다.

중요: 과거 762장 placeholder 기준 문서는 현재 상태와 맞지 않는다. 기존 placeholder는 최종물로 계산하지 않는다.

## 2. 제작 방향

기본 UI 미감은 **상담 파일풍 비주얼노벨 UI**로 고정한다.

- 따뜻한 종이 파일, 클립보드, 상담 기록지 느낌.
- 상담실 배경의 목재/베이지/청록 계열과 어울리는 색감.
- 상용 VN처럼 정돈된 테두리, 탭, 얇은 라인, 부드러운 그림자 사용.
- 교육용 전문성이 보여야 하며 모바일 앱, 병원 차트, 카페 메뉴판처럼 보이면 reject.
- 이미지 안에는 한국어 텍스트를 넣지 않는다. 모든 텍스트는 Unity 텍스트로 얹는다.

## 3. 1차 생성 대상

| 파일명 | 권장 해상도 | 용도 | 제작 기준 |
| --- | ---: | --- | --- |
| `dialogue_box.png` | 1600x320 | 하단 대화창 | 중앙 본문 영역은 완전히 비우고 장식은 상단/코너/외곽에만 둔다. |
| `speaker_nameplate.png` | 560x88 | 화자 이름표 | 짧은 탭 형태, 이름이 들어갈 중앙 영역은 비운다. |
| `choice_card_question.png` | 1200x140 | 질문/다음 버튼/중립 선택지 | 읽기 쉬운 넓은 중앙, 과한 장식선 금지. |
| `choice_card_intervention.png` | 1200x140 | 상담 개입 선택지 | question과 같은 계열이되 청록 포인트를 조금 더 분명히 둔다. |
| `supervisor_note_panel.png` | 640x360 | 슈퍼바이저 노트 | 어두운 반투명 기록 카드 느낌, 본문 영역은 비운다. |
| `case_file_panel.png` | 1024x1024 | 사례 파일/메뉴/선택 패널 | 종이 파일/클립보드 느낌, 내부 정보 구역은 넓게 비운다. |
| `metrics_hud.png` | 1600x96 | 상단 HUD | 얇고 가벼운 정보 바, 숫자와 텍스트가 들어갈 영역을 비운다. |
| `session_result_sheet.png` | 1400x960 | 결과 리포트 | 상담 평가서/수련 기록지 느낌, 큰 본문 영역과 섹션 구분만 둔다. |

## 4. 공통 생성 프롬프트

```text
Create a polished visual novel UI asset for a serious Korean family therapy counseling simulation game.

Asset: <asset name>
Use: <where it appears in the game>
Style: warm counseling case-file aesthetic, premium visual novel UI, subtle paper texture, muted teal accents, dark ink linework, warm wood-compatible palette.
Composition: clean empty central text-safe area, decorative edges only, readable shape, 9-slice friendly border, no visual clutter.
Constraints: no readable text, no logo, no watermark, no icons that imply hospital horror, no mobile app look, no decorative line crossing the text area.
Output: standalone UI panel asset, transparent outside rounded edges where appropriate.
```

## 5. 단계별 작업

### Phase 0. UI 스타일 보드

먼저 8개 UI가 한 장에 배치된 스타일 보드를 만든다.

목표:

- 현재 상담실 배경과 캐릭터 스타일에 어울리는지 확인한다.
- 종이 파일풍, 청록 포인트, 어두운 잉크 라인이 과하지 않은지 본다.
- 텍스트 없는 빈 UI 영역이 충분한지 확인한다.

저장 위치:

```text
Docs/UI_Style_Board_2026-06-09.png
```

### Phase 1. 핵심 8종 개별 PNG 생성

스타일 보드가 통과하면 8개 UI를 개별 PNG로 생성한다.

저장 위치:

```text
Assets/Resources/VN/UI/dialogue_box.png
Assets/Resources/VN/UI/speaker_nameplate.png
Assets/Resources/VN/UI/choice_card_question.png
Assets/Resources/VN/UI/choice_card_intervention.png
Assets/Resources/VN/UI/supervisor_note_panel.png
Assets/Resources/VN/UI/case_file_panel.png
Assets/Resources/VN/UI/metrics_hud.png
Assets/Resources/VN/UI/session_result_sheet.png
```

덮어쓰기 정책:

- 기존 최종 PNG가 있으면 바로 덮어쓰지 않는다.
- 새 버전은 `_v2`, `_v3`로 저장하고 검수 후 교체한다.
- `.png.meta`는 Unity import 후 함께 유지한다.

### Phase 2. Unity 적용

이미지가 준비되면 코드 적용을 별도 단계로 진행한다.

- `UseDecorativeUiSkins`를 `true`로 바꾼다.
- `RawImage` stretch 적용이 텍스트를 가리면 Sprite 기반 9-slice 적용으로 바꾼다.
- 패널 padding과 버튼 높이를 텍스트 안전 영역 기준으로 조정한다.
- 선택지 버튼은 긴 문장에서도 중앙 장식과 충돌하지 않아야 한다.

### Phase 3. 검수

아래 화면을 1600x900 기준으로 캡처해 확인한다.

```text
01_main_menu.png
05_ft001_dialogue.png
07_ft001_choice_deck.png
13_supervision_report.png
```

통과 기준:

- 텍스트가 이미지 장식에 가려지지 않는다.
- 패널이 캐릭터 얼굴/몸을 불필요하게 가리지 않는다.
- 한국어 텍스트는 전부 Unity 텍스트로 표시되어 선명하다.
- 이미지 안에 깨진 글자, 로고, 워터마크가 없다.
- 상담센터/기록지/비주얼노벨 느낌이 동시에 난다.

## 6. Reject 기준

아래 중 하나라도 있으면 최종 UI로 쓰지 않는다.

- 중앙 본문 영역에 장식선이 지나간다.
- 한국어 또는 의미 있는 글자가 이미지 안에 들어간다.
- 의료 차트, 병원, 공포, 모바일 앱, 카페 메뉴판처럼 보인다.
- 버튼이 너무 화려해서 선택지 텍스트보다 시선이 앞선다.
- 배경과 캐릭터보다 UI가 지나치게 밝거나 어둡다.
- 16:9 캡처에서 텍스트 판독성이 떨어진다.

## 7. 검증 명령

```powershell
& 'C:\Program Files\Unity\Hub\Editor\6000.4.5f1\Editor\Unity.exe' -batchmode -quit -projectPath 'C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity' -executeMethod FamilyTherapyPracticumBuilder.BuildWindows -logFile 'C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity\Logs\ui_asset_apply_build.log'

& 'C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity\Builds\Windows\FamilyTherapyPracticum.exe' -batchmode -nographics -familyTherapyUiSmokeTest -logFile 'C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity\Logs\ui_asset_apply_ui_smoke.log'

& 'C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity\Builds\Windows\FamilyTherapyPracticum.exe' -screen-fullscreen 0 -screen-width 1600 -screen-height 900 -familyTherapyVisualAudit -logFile 'C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity\Logs\ui_asset_apply_visual.log'
```

## 8. 다음 작업

1. `Docs/UI_Style_Board_2026-06-09.png`를 먼저 만든다.
2. 스타일 보드가 통과하면 8개 개별 PNG를 생성한다.
3. Unity 적용 코드를 켠 뒤 visual audit으로 텍스트 충돌 여부를 확인한다.

## 9. 2026-06-09 적용 기록

진행 상태:

- `Docs/UI_Style_Board_2026-06-09.png` 생성 완료.
- `Assets/Resources/VN/UI` 아래 핵심 8종 PNG 생성 완료.
- `UseDecorativeUiSkins`를 `true`로 전환 완료.
- VN 메인 메뉴, 대화창, 선택지, 슈퍼바이저 노트, 사례 파일 패널, HUD, 슈퍼비전 리포트에 UI 스킨 적용 완료.
- 캐릭터 로더가 `*_phase1`, `*_phase0` 접미사 리소스를 자동 탐색하도록 수정해 FT-001 자녀/가족 이미지가 fallback 박스 대신 표시되게 함.

검증 결과:

- Windows 빌드 성공: `Logs/ui_asset_apply_rebuild_after_report.log`
- UI smoke 성공: `family_therapy_practicum_ui_smoke_result.json`
- 1600x900 visual audit 성공: `visual_audit_1600x900/visual_audit_result.json`
- 핵심 VN 화면 `05_ft001_dialogue.png`, `07_ft001_choice_deck.png`는 텍스트 오버플로우 0건.
- `13_supervision_report.png`에 결과지/액션 패널 스킨 적용 확인.

주의:

- 이번 UI 이미지는 이미지 생성 서버 실패 때문에 로컬 래스터 생성 방식으로 만든 1차 적용용 스킨이다.
- 기능 연결과 화면 충돌 검수는 통과했지만, 상용 출시급 최종 UI 아트로 보려면 별도 이미지 생성/디자이너 리터치 버전으로 교체하는 단계가 필요하다.

## 10. 2026-06-09 최종 확인 기록

추가 적용:

- `Assets/Resources/VN/UI`에 핵심 8종 PNG와 `.png.meta`를 복원해 런타임 리소스로 유지했다.
- UI 스킨 적용 방식을 `RawImage` 단순 stretch에서 `Image.Type.Sliced` 기반 Sprite 생성으로 변경했다.
- 메트릭 카드, 사례 접수, 대시보드 텍스트 높이를 조정해 visual audit의 텍스트 overflow를 제거했다.
- 최종 UI 스킨은 텍스트 없는 상담 파일풍 래스터 에셋이며, 한국어 문구는 모두 Unity 텍스트로 렌더링한다.

최종 검증:

- Windows 빌드 성공: `Logs/ui_asset_final_verified_build.log`, `Logs/ui_asset_final_zero_build.log`
- 최신 assembly 기준 UI smoke 성공: `Logs/ui_asset_latest_assembly_ui_smoke.log`
- 최신 assembly 기준 1600x900 visual audit 성공: `Logs/ui_asset_latest_assembly_visual.log`
- `family_therapy_practicum_ui_smoke_result.json`: `completed=true`, `hudCount=1`, `dialogueCount=1`, `error=""`
- `visual_audit_1600x900/visual_audit_result.json`: 전체 `textOverflowCount=0`, 전체 `offscreenRectCount=0`
- 계획의 필수 캡처 화면 `01_main_menu`, `05_ft001_dialogue`, `07_ft001_choice_deck`, `13_supervision_report` 모두 텍스트 overflow 0건.

주의:

- Unity 로그의 licensing handshake 메시지는 빌드 실패가 아니라 에디터 라이선스 토큰 갱신 경고이며, 빌드 결과는 `Succeeded`로 확인했다.
- 과거 placeholder 격리 폴더에 동일 UI 파일 사본이 남아 있을 수 있으나, 최종 런타임 리소스는 `Assets/Resources/VN/UI`의 8종이다.
