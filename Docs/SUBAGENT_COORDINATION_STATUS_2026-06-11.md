# 서브에이전트 병렬 작업 현황

## 목적

사용자 요청에 따라 서로 다른 성격과 관점을 가진 서브에이전트 여러 명을 병렬로 투입했다.

이번 병렬 작업의 목표는 이미지 생성 대기 시간 동안 다음 작업을 동시에 진전시키는 것이다.

- FT001 새 이미지 검수 자동화
- FT001 대사/선택지/슈퍼바이저 피드백 개선
- 선택 로그/대시보드 분석 설계
- 발표 리스크와 교수 피드백 대응 정리
- 새 regen 이미지가 들어왔을 때 Unity에서 우선 적용되도록 런타임 경로 정리

## 투입한 관점

### 1. QA 엔지니어 관점

성격:

까다로운 검수 담당. 이미지가 “대충 비슷함”으로 통과하지 않게 수량, 파일명, 해상도, 중복, 유사도를 기계적으로 확인하는 역할.

산출물:

- `Tools/ValidateFt001RegenImages.py`
- `Docs/FT001_REGEN_IMAGE_VALIDATION_WORKFLOW_2026-06-11.md`

핵심 내용:

- FT001 regen 이미지 기준을 67장으로 확정
- expected filename list 67개 포함
- PNG 수량 검사
- 누락/extra 파일 검사
- 1600x900 해상도 검사
- SHA256 완전 중복 검사
- perceptual hash 유사도 검사
- contact sheet 생성
- JSON 리포트 생성

검증:

`python Tools\ValidateFt001RegenImages.py --help` 정상 출력 확인.

현재 regen 폴더가 없으므로 기본 실행은 의도대로 실패한다.

현재 실패 상태:

- PNG count: `0 / 67`
- fail reasons: `input_dir_missing`, `png_count_not_67`, `missing_expected_files`

### 2. 가족치료 슈퍼바이저 + 내러티브 디자이너 관점

성격:

실제 상담 장면이 얕아 보이지 않도록 대사, 선택지, 슈퍼비전 피드백의 임상적 설득력을 보는 역할.

산출물:

- `Docs/FT001_DIALOGUE_CHOICE_SUPERVISION_REWRITE_BRIEF_2026-06-11.md`

핵심 내용:

- FT001을 “등교 거부 아동 문제”가 아니라 가족과 학교가 함께 만드는 아침 압박 순환 사례로 재정의
- 공통 도입부터 T1-T5까지 각 턴의 상담 목표를 분리
- 좋은/부분/위험 선택이 다음 장면 첫 대사, 가족 방어 수준, 다음 선택지, 김혜성 슈퍼비전에 남도록 설계
- 대사는 감정, 생활 맥락, 관계 기능을 담아 길게 쓰되 가족은 이론어를 쓰지 않게 하는 원칙 제안
- 김혜성은 가족체계 기본을 중심으로 관찰 근거와 다음 턴 목표를 짚는 차분한 피드백 구조로 제안

### 3. 데이터 분석가 + 대시보드 설계자 관점

성격:

게임이 단순 실행본으로 보이지 않게, 선택 로그를 실제 분석 가능한 데이터 구조로 바꾸는 역할.

산출물:

- `Docs/FT_CHOICE_LOG_DASHBOARD_SCHEMA_2026-06-11.md`

핵심 내용:

- `ft_choice_events.csv/json` 원천 이벤트 로그 설계
- 현재 `player_choice_log.csv/json` 세션 요약 로그와의 호환 구조
- FT001 5개 장면, 15개 선택지의 `choice_id`, `intervention_type`, `quality_band`, `risk_flag`
- `session_started`, `choice_selected`, `reaction_shown`, `ending_resolved` 등 event types
- 합성 에이전트 유형별 반복 플레이 분석 설계
- 대시보드 필터/정렬/비교 요구사항
- Unity 저장용 C# 필드명과 export용 snake_case 필드명 매핑

### 4. 발표 프로듀서 + 교수 피드백 대응 관점

성격:

10분 발표에서 설득력이 흐려지지 않도록 위험요소를 먼저 잡는 역할.

산출물:

- `Presentation/PRESENTATION_PRODUCER_NOTES_2026-06-11.md`

핵심 위험요소:

- 분석이 실제 구현이 아니라 계획처럼 보일 위험
- AI 이미지 쇼케이스처럼 보여 수업 목표가 흐려질 위험
- FT001과 FT002-FT010 완성도 차이를 과장할 위험
- 가족치료/임상 효과를 검증된 것처럼 말할 위험
- 10분 발표가 기능 나열로 무너질 위험

## 내가 직접 처리한 통합 작업

### 1. 이미지 재생성 지시서 보정

파일:

`Docs/FT001_CG_REGEN_STRICT_CONTINUITY_COMMAND_2026-06-11.md`

수정 내용:

- 처음 50장 기준으로 잡았던 것을 코드 기준 67장으로 정정
- 실제 Unity 코드의 `Ft001Line(...)`, `Ft001Choice(...)`, 선택지 idle 경로를 기준으로 컷 수 재산정
- 62개 대사/반응/인트로/분기 컷 + 5개 choice idle 컷 = 67장
- 기존 50장 세트에 없던 intro/branch 컷을 추가 필수 목록으로 넣음
- fallback 재사용 금지 유지
- 카메라 슬롯 6개 고정 유지

### 2. 런타임 이미지 경로 우선순위 수정

파일:

`Assets/Scripts/FamilyTherapyPracticumGame.cs`

수정 내용:

`Ft001CgPath(string slug)`가 이제 다음 순서로 이미지를 찾는다.

1. `VN/EventCG/FT001_LineByLineLocked_Regen_20260611/ft001_cg_{slug}`
2. `VN/EventCG/FT001_LineByLineLocked/ft001_cg_{slug}`
3. `VN/EventCG/FT001_CommercialBranching/{mapped_name}`
4. `VN/EventCG/FT001/ft001_cg_{slug}`

의미:

- 새 67장 regen 세트가 들어오면 코드 수정 없이 최우선 적용된다.
- 새 폴더가 없거나 일부 이미지가 빠져도 현재 실행 가능한 상태는 유지된다.
- 기존 commercial branching 이미지는 fallback으로만 남는다.

### 3. 런타임 적용 노트 작성

파일:

`Docs/FT001_REGEN_RUNTIME_INTEGRATION_NOTES_2026-06-11.md`

핵심 내용:

- 왜 67장이 필요한지
- 코드가 어떤 순서로 이미지를 로드하는지
- 새 이미지가 들어오면 어떤 검수와 빌드 절차를 밟아야 하는지
- 검수 전 기존 active 폴더를 덮어쓰지 말아야 하는 이유

## 현재 상태

완료:

- 서브에이전트 4명 작업 완료
- QA 스크립트 생성 완료
- 대사/선택지/슈퍼비전 개선 브리프 생성 완료
- 로그/대시보드 스키마 생성 완료
- 발표 프로듀서 노트 생성 완료
- FT001 이미지 재생성 지시서 67장 기준으로 보정 완료
- Unity 런타임 이미지 경로 우선순위 보정 완료

대기:

- 이미지 생성 창에서 67장 regen 이미지 생성
- 생성 결과를 `Assets/Resources/VN/EventCG/FT001_LineByLineLocked_Regen_20260611`에 저장

다음 실행 명령:

```powershell
cd C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity
python Tools\ValidateFt001RegenImages.py
```

고정 리포트/컨택트시트 경로로 실행:

```powershell
python Tools\ValidateFt001RegenImages.py `
  --report Docs\GeneratedSources\FT001_RegenValidation_20260611\latest_report.json `
  --contact-sheet Docs\GeneratedSources\FT001_RegenValidation_20260611\latest_contact_sheet.png `
  --overwrite
```

## 다음 판단 기준

새 이미지 67장이 들어오면 아래 순서로 진행한다.

1. QA 스크립트 실행
2. 실패하면 missing/duplicate/similar 컷 목록을 이미지 생성 창에 재작업 지시
3. 통과하면 Unity 리빌드
4. 1600x900 실행 캡처 재생성
5. 발표용 `PPT_Selected_Images_2026-06-10` 후보를 새 캡처 기준으로 교체
6. Claude Design 브리프에서 이미지 기준만 새 캡처로 갱신
