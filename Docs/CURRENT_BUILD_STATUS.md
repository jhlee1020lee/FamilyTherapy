# 현재 빌드 상태

- 작성일: 2026-06-08
- 프로젝트: `FamilyTherapyPracticumUnity`
- 실행본: `Builds/Windows/FamilyTherapyPracticum.exe`
- 메인 씬: `Assets/Scenes/FamilyTherapyPracticum.unity`

## 구현된 기능

- 한국어 우선 가족치료 비주얼노벨/상담 시뮬레이션 UI.
- 상용판 VN 엔진 1차 구현:
  - 배경 `Resources` 로딩
  - 캐릭터 `Resources` 로딩
  - 캐릭터 위치 배치
  - 현재 화자 강조
  - 대화창/화자 이름표
  - 다음 버튼
  - 선택지 카드
  - 선택 후 가족 반응 컷
  - 슈퍼바이저 노트
  - 누락 이미지 fallback 패널
  - 이미지가 없는 generic 사례도 역할 패널로 정상 진행
- 기능 우선 구현:
  - 상용 VN형 타이틀 화면 추가
  - 카드형 설명 위주의 메인화면 제거
  - 첫 화면 명령을 `START CAMPAIGN`, `CASE FILES`, `CONTINUE`, `SAVE / LOAD`, `ANALYTICS`로 재구성
  - 상담실 배경 위에 타이틀, 활성 사건 파일, 진행률 패널을 표시
  - `데모` 중심 진입 문구 제거
  - UI 이미지 스킨 적용:
    - `dialogue_box`
    - `speaker_nameplate`
    - `choice_card_question`
    - `choice_card_intervention`
    - `supervisor_note_panel`
    - `case_file_panel`
    - `metrics_hud`
  - 센터 로비/사례 파일 화면 추가
  - 60개 사례 직접 선택 가능
  - 3개 저장 슬롯 추가
  - 슬롯 1 자동 저장
  - 저장 데이터에 플레이 로그, 마지막 사례, AI 슈퍼바이저 설정 포함
- 상용판 자산 매니페스트:
  - 목표 자산 수: 약 750장
  - 현재 런타임 후보 자산: 762장
  - 현재 필수 적용 자산: 44장
  - 현재 사용 가능 필수 자산: 44장
  - 남은 누락 필수 자산: 없음
  - Chapter01 `FT-001`~`FT-010` 캐릭터 PNG는 폴더 스캔으로 자동 등록됨
  - CoreCases `FT-011`~`FT-024` 캐릭터 PNG는 폴더 스캔으로 자동 등록됨
- 합성 가족 사례 60개 데이터셋.
  - 1장 `FT-001`~`FT-010`은 수작업 에피소드형 핵심 사례.
  - 2장 이후는 빠른 실습용 자동 생성 사례.
- VN script registry:
  - `FT-001`은 5턴 전체 VN 회기 script 구현.
  - `FT-002`~`FT-010`은 `Assets/Resources/VN/Characters/Chapter01/{FT번호}`의 캐릭터 이미지를 자동 연결한 training VN script로 진입.
  - `FT-011`~`FT-024`는 `Assets/Resources/VN/Characters/CoreCases/{FT번호}`의 캐릭터 이미지를 자동 연결한 training VN script로 진입.
  - `FT-025`~`FT-060`은 이미지 없이도 플레이 가능한 generic VN training script 자동 생성.
  - 상용 핵심 사례 24개와 훈련 사례 36개가 모두 VN 루프로 진입 가능.
  - 기존 카드형 회기 화면은 fallback/training mode로 유지.
- 가족치료 이론 9개:
  - 가족체계 기본
  - Bowen 다세대
  - 전략적 가족치료
  - 구조적 가족치료
  - 경험적/Satir
  - 정신역동 가족치료
  - 인지행동 가족치료
  - 해결중심 가족치료
  - 이야기치료
- 슈퍼바이저 캐릭터 9명:
  - 각 이론 렌즈별 담당 슈퍼바이저 프로필과 회기 코멘트.
- 핵심 루프:
  - 메인 메뉴
  - START CAMPAIGN: `FT-001`부터 캠페인 시작
  - 사례 접수
  - 이론 렌즈 선택
  - 5턴 VN 회기 진행
  - 선택 후 가족 반응
  - 슈퍼비전 점수/해설
  - 사례/학습 로그 대시보드
- 1장 핵심 사례 구조:
  - 접수 메모
  - 가족 관계도
  - 초기 대화
  - 숨은 역동
  - 수련 목표
  - 슈퍼바이저 단서
  - 복기 질문
- Export:
  - `player_choice_log.csv`
  - `player_choice_log.json`
  - `case_dataset.csv`
  - `case_dataset.json`
  - `dashboard.html`
- Export 확장:
  - `case_dataset.csv`에 `is_handcrafted`, `family_map`, `learning_objective`, `supervisor_cue`, `reflection_question` 포함.
  - `player_choice_log.csv`에 5턴 선택 경로 `selected_interventions` 포함.
  - `player_choice_log.csv/json`에 VN 로그 필드 포함:
    - `vn_mode`
    - `vn_choice_path`
    - `vn_reaction_summary`
    - `turn_metric_deltas`
  - `dashboard.html`에 총 사례, 1장 수작업 사례, 이론, 슈퍼바이저, 회기 턴 요약 포함.
- 저장 파일:
  - `family_therapy_save_slot_1.json`
  - `family_therapy_save_slot_2.json`
  - `family_therapy_save_slot_3.json`
- 발표 보조 문서:
  - `Docs/DEMO_ROUTE_10_MINUTES.md`
  - `Docs/PRESENTATION_QA_SHEET.md`
- 캐릭터 이름/asset ID 문서:
  - `Docs/CHARACTER_NAME_REGISTRY.md`
  - 파일명은 역할/이론 ID 기준, 게임 표시명은 확정 이름 기준.
- 안전 고지:
  - 합성 사례
  - 실제 개인정보 미사용
  - 실제 상담/의료/법률/복지 판단 비대체
  - 선택형 AI 슈퍼바이저는 참고 코멘트로만 사용

## 검증 결과

- 씬 생성:
  - 로그: `Logs/create_scene_handcrafted_ch1.log`
  - 결과: `Family Therapy Practicum scene created`
- Windows 빌드:
  - 최신 로그: `Logs/corecases_asset_apply_build.log`
  - 결과: `Build Finished, Result: Success`
  - 결과: `Family Therapy Practicum build result: Succeeded`
- VN UI smoke:
  - 실행본: `Builds/Windows/FamilyTherapyPracticum.exe -batchmode -nographics -familyTherapyUiSmokeTest`
  - 최신 로그: `Logs/corecases_asset_apply_ui_smoke_rerun.log`
  - 결과: `FAMILY_THERAPY_PRACTICUM_UI_SMOKE completed=true`
  - 검증:
    - `hudCount=1`
    - `dialogueCount=1`
    - `characterImageCount=2`
  - 수정 내용: 캐릭터 holder의 `Image`와 `RawImage` 충돌로 VN 회기 화면이 배경만 남는 문제를 수정.
- 런타임 스모크:
  - 실행본: `Builds/Windows/FamilyTherapyPracticum.exe -batchmode -nographics -familyTherapySmokeTest`
  - 최신 로그: `Logs/corecases_asset_apply_smoke_rerun.log`
  - 결과: `FAMILY_THERAPY_PRACTICUM_SMOKE completed=true`
  - smoke JSON:
    - `completed=true`
    - `caseCount=60`
    - `chapterOneHandcraftedCount=10`
    - `theoryCount=9`
    - `supervisorCount=9`
    - `sessionTurnCount=5`
    - `visualNovelMode=true`
    - `vnScriptCount=60`
    - `vnPlayableCaseCount=60`
    - `commercialCoreVnScriptCount=24`
    - `trainingVnScriptCount=36`
    - `vnCharacterProfileCount=108`
    - `vnRequiredAssetCount=44`
    - `vnAvailableAssetCount=44`
    - `missingVnAssets=[]`
    - `ft001VnReady=true`
    - `commercialAssetTarget=750`
    - `commercialAssetCurrent=762`
    - `styleTestAssetCount=14`
    - `saveSystemReady=true`
    - `logCount=1`
    - `hasDashboardHtml=true`
    - `hasPlayerCsv=true`
    - `hasCaseDataset=true`
  - `player_choice_log.csv` 확인:
    - `vn_mode=true`
    - `vn_choice_path` 기록됨
    - `vn_reaction_summary` 기록됨

## Export 위치

런타임 스모크 기준:

```text
C:\Users\이종호\AppData\LocalLow\Family Therapy Practicum\Family Therapy Practicum\FamilyTherapyPracticumExports
```

## 다음 보강 우선순위

1. 상용 핵심 24개 사례의 generic VN script를 가족별 수작업 대사 script로 교체.
2. 새 표정 이미지가 들어오면 표정별 visual QA 후 같은 파일명으로 덮어쓰기.
3. 메인/로비/사례 파일 화면의 남은 기본 버튼을 이미지 스킨 버튼으로 교체.
4. 저장 슬롯 삭제/덮어쓰기 확인 모달과 설정 화면 polish.
5. 화면 캡처 기반 저해상도 UI 검증과 버튼 겹침 확인 추가.
