# FT 선택 로그 대시보드 스키마 설계

- 작성일: 2026-06-11
- 대상 프로젝트: `99_FamilyTherapyPracticumUnity`
- 대상 사례: `FT-001` 한부모 초등 자녀 가족
- 목적: 가족치료 비주얼노벨의 선택 로그를 CSV/JSON으로 저장하고, 에이전트별 반복 플레이 결과를 탐색형 대시보드에서 필터/정렬/비교할 수 있게 만드는 구현 기준을 고정한다.

## 1. 설계 원칙

현재 Unity 빌드는 `player_choice_log.csv`, `player_choice_log.json`, `case_dataset.csv`, `case_dataset.json`, `dashboard.html`을 export할 수 있다. 현재 로그는 세션 단위 요약에 가깝다. 최종 분석용 설계는 두 층으로 나눈다.

1. 원천 이벤트 로그: 플레이 중 발생한 모든 선택, 장면 진입, 대사 노출, 반응, 점수 변화, 엔딩을 event row로 남긴다.
2. 파생 분석 테이블: 세션/턴/에이전트 단위로 집계해 대시보드가 빠르게 필터링하고 비교할 수 있게 한다.

CSV는 수업 발표와 간단한 데이터 분석용이다. JSON은 Unity 재현, 세션 replay, 대시보드 상세 drill-down용이다. 두 형식은 같은 필드명을 공유하되, JSON은 `metrics`, `choice`, `route`, `agent`, `device` 같은 중첩 객체를 허용한다.

## 2. CSV 원천 이벤트 스키마

권장 파일명: `ft_choice_events.csv`

| field | type | required | description |
|---|---:|---:|---|
| `event_id` | string | Y | UUID 또는 `session_id-seq` 형식의 고유 이벤트 ID |
| `session_id` | string | Y | 1회 플레이 세션 ID. 현재 구현의 `yyyyMMdd-HHmmss-FT-001` 형식을 유지 가능 |
| `event_seq` | int | Y | 세션 안 이벤트 순서. 1부터 증가 |
| `created_at` | string | Y | ISO-8601 로컬 시간 또는 UTC 시간 |
| `case_id` | string | Y | 예: `FT-001` |
| `chapter` | int | Y | 예: `1` |
| `script_kind` | string | Y | 예: `full_case_specific_v1` |
| `event_type` | string | Y | 아래 event type enum 참조 |
| `scene_id` | string | Y | 예: `ft001_t01`, `ft001_t04_reaction_a` |
| `scene_title` | string | Y | 예: `초기 합류와 문제 정의` |
| `turn_index` | int | N | 1-5. 장면/선택 이벤트가 아닌 경우 비움 |
| `dialogue_index` | int | N | 턴 내 대사 index. 선택 이벤트에서는 비움 |
| `speaker_id` | string | N | 예: `ft001_mother`, `supervisor_system` |
| `speaker_role` | string | N | `mother`, `child`, `teacher`, `grandmother`, `supervisor` |
| `expression_id` | string | N | 예: `softened`, `withdrawn`, `procedural` |
| `choice_id` | string | N | 예: `ft001_t01_a_joining` |
| `choice_rank` | int | N | 화면 표시 순서. A=1, B=2, C=3 |
| `choice_label` | string | N | 플레이어가 본 상담자 발화 |
| `selected_choice` | bool | N | 선택 이벤트면 `true`, 노출/hover 이벤트면 `false` |
| `theory_id` | string | N | 선택지에 연결된 이론 ID. 예: `system`, `cbft`, `procedure` |
| `selected_theory_id` | string | N | 플레이 전 플레이어/에이전트가 고른 이론 렌즈 |
| `recommended_theory_id` | string | Y | FT001은 `system` |
| `intervention_type` | string | N | 예: `joining`, `circular_mapping`, `ip_fixing` |
| `quality` | int | N | 선택지 원점수 0-100 |
| `quality_band` | string | N | `high`, `partial`, `risk` |
| `trust_before` | int | N | 선택 전 신뢰 점수 |
| `trust_after` | int | N | 선택 후 신뢰 점수 |
| `trust_delta` | int | N | `trust_after - trust_before` |
| `safety_before` | int | N | 선택 전 안전 점수 |
| `safety_after` | int | N | 선택 후 안전 점수 |
| `safety_delta` | int | N | `safety_after - safety_before` |
| `insight_before` | int | N | 선택 전 통찰 점수 |
| `insight_after` | int | N | 선택 후 통찰 점수 |
| `insight_delta` | int | N | `insight_after - insight_before` |
| `route_flags` | string | N | `|` 구분. 예: `joining|circular_mapping|feedback_task` |
| `risk_flag` | string | N | `none`, `ip_fixed`, `premature_contract`, `diagnostic_closure`, `academic_homework` 등 |
| `family_reaction` | string | N | 선택 후 가족 반응 요약 |
| `reaction_speaker_id` | string | N | 반응 중심 인물 |
| `reaction_expression_id` | string | N | 반응 표정 |
| `supervisor_feedback` | string | N | 선택 후 슈퍼바이저 피드백 |
| `ending_id` | string | N | 세션 종료 시 엔딩 ID |
| `agent_id` | string | N | 사람이면 익명 ID, 합성 에이전트면 agent ID |
| `agent_type` | string | N | 예: `system_aligned`, `risk_first`, `procedure_first` |
| `run_id` | string | N | 반복 실험의 실행 ID |
| `seed` | int | N | 합성 에이전트 또는 랜덤 선택 재현용 seed |
| `response_time_ms` | int | N | 선택지 표시 후 선택까지 걸린 시간 |
| `client_version` | string | N | Unity 빌드 버전 |

## 3. JSON 스키마

권장 파일명: `ft_choice_events.json`

```json
{
  "schema_version": "2026-06-11",
  "exported_at": "2026-06-11T10:30:00",
  "project": "FamilyTherapyPracticumUnity",
  "events": [
    {
      "event_id": "20260611-103000-FT-001-0007",
      "session_id": "20260611-103000-FT-001",
      "event_seq": 7,
      "created_at": "2026-06-11T10:31:22",
      "event_type": "choice_selected",
      "case": {
        "case_id": "FT-001",
        "chapter": 1,
        "family_type": "한부모 초등 자녀 가족",
        "script_kind": "full_case_specific_v1",
        "recommended_theory_id": "system"
      },
      "scene": {
        "scene_id": "ft001_t01",
        "scene_title": "초기 합류와 문제 정의",
        "turn_index": 1
      },
      "choice": {
        "choice_id": "ft001_t01_a_joining",
        "choice_rank": 1,
        "choice_label": "\"오늘 상담에서 각자 꼭 달라졌으면 하는 것과, 걱정되는 것을 한 문장씩 들어보고 싶습니다.\"",
        "theory_id": "system",
        "intervention_type": "joining",
        "quality": 90,
        "quality_band": "high"
      },
      "metrics": {
        "trust_before": 50,
        "trust_after": 62,
        "trust_delta": 12,
        "safety_before": 50,
        "safety_after": 60,
        "safety_delta": 10,
        "insight_before": 50,
        "insight_after": 61,
        "insight_delta": 11
      },
      "reaction": {
        "family_reaction": "박성빈은 숨을 고르며 고개를 끄덕입니다...",
        "reaction_speaker_id": "ft001_mother",
        "reaction_expression_id": "softened",
        "supervisor_feedback": "초기 합류와 순환 관찰이 모두 살아납니다."
      },
      "agent": {
        "agent_id": "agent_system_aligned_001",
        "agent_type": "system_aligned",
        "run_id": "ft001_batch_001",
        "seed": 1001,
        "selected_theory_id": "system"
      },
      "route": {
        "route_flags": ["joining"],
        "risk_flags": [],
        "ending_id": null
      }
    }
  ]
}
```

현재 구현의 `PlayerChoiceLog`는 세션 요약 JSON으로 유지하고, 위 이벤트 JSON을 추가하면 된다. 즉 `player_choice_log.json`은 세션별 결과표, `ft_choice_events.json`은 원자료로 둔다.

## 4. 현재 세션 요약 로그와의 호환

현재 `player_choice_log.csv`의 필드는 다음 분석 테이블로 유지한다.

| current field | dashboard meaning | target handling |
|---|---|---|
| `session_id` | 세션 고유 ID | 그대로 유지 |
| `case_id`, `chapter`, `family_type` | 사례 필터 | 그대로 유지 |
| `selected_theory`, `recommended_theory`, `matched` | 이론 렌즈 비교 | 이름 필드 외에 ID 필드 추가 권장 |
| `score`, `trust`, `safety`, `insight` | 최종 성과 지표 | 세션 집계 테이블의 핵심 metric |
| `risk_level` | 사례 위험도 | 사례 필터 및 위험도 그룹 비교 |
| `missed_concepts` | 놓친 가족치료 개념 | 텍스트 검색/태그화 |
| `selected_interventions` | 선택 발화 path | 이벤트 로그에서 재구성 가능 |
| `vn_choice_path` | `intervention_type:choice` path | route sequence 분석에 사용 |
| `route_flags` | 개입 유형 요약 | sequence mining, ending 비교에 사용 |
| `ending_id` | 결말 | 엔딩 필터/비교 축 |
| `vn_reaction_summary` | 가족 반응 요약 | drill-down tooltip |
| `turn_metric_deltas` | 턴별 변화 | 별도 `turn_summary` 테이블로 정규화 권장 |
| `created_at` | 시간 필터 | 그대로 유지 |

## 5. FT001 장면 및 선택지 event type

FT001은 5개 장면, 각 장면 3개 선택지로 구성된다. 모든 선택지는 상담자 발화 단위로 저장하고, 내부 분석용으로 `choice_id`, `intervention_type`, `risk_flag`, `quality_band`를 붙인다.

### 5.1 공통 event types

| event_type | 발생 시점 | required fields |
|---|---|---|
| `session_started` | 새 플레이 시작 | `session_id`, `case_id`, `agent_id`, `selected_theory_id` |
| `case_intake_opened` | 접수 메모/사례 브리핑 진입 | `case_id`, `scene_id` |
| `theory_lens_selected` | 플레이어가 이론 렌즈 선택 | `selected_theory_id`, `recommended_theory_id` |
| `scene_entered` | VN 턴 진입 | `scene_id`, `turn_index`, `scene_title` |
| `dialogue_line_viewed` | 대사 1줄 노출 | `dialogue_index`, `speaker_id`, `expression_id` |
| `choice_deck_opened` | 3개 선택지 표시 | `scene_id`, `turn_index` |
| `choice_shown` | 선택지 row/card 표시 | `choice_id`, `choice_rank`, `choice_label` |
| `choice_selected` | 플레이어/에이전트가 선택 | `choice_id`, `intervention_type`, metric before/after/delta |
| `reaction_shown` | 가족 반응 장면 표시 | `reaction_speaker_id`, `family_reaction` |
| `supervisor_feedback_shown` | 피드백 표시 | `supervisor_feedback`, `quality` |
| `route_flags_updated` | route token 갱신 | `route_flags`, `risk_flags` |
| `turn_completed` | 턴 종료 | `turn_index`, `trust_delta`, `safety_delta`, `insight_delta` |
| `ending_resolved` | 최종 엔딩 결정 | `ending_id`, `score`, `trust`, `safety`, `insight` |
| `session_exported` | CSV/JSON/HTML export | `export_path`, `schema_version` |
| `dashboard_opened` | 대시보드 열람 | `dashboard_version`, `filter_state` |

### 5.2 FT001 turn map

| turn | scene_id | scene_title | 분석 초점 |
|---:|---|---|---|
| 1 | `ft001_t01` | 초기 합류와 문제 정의 | IP 고정 여부, 초기 합류, 순환 관찰 시작 |
| 2 | `ft001_t02` | 가족역동 개념화 | 아침 장면의 순환 mapping, 학교 연락 루프 |
| 3 | `ft001_t03` | 감정과 구조 단서 확인 | 외조모 비판 아래 걱정, 보호자 고립, 정서 안전 |
| 4 | `ft001_t04` | 핵심 개입 선택 | 순환질문, 행동계약 성급성, 진단적 종결 위험 |
| 5 | `ft001_t05` | 다음 주 과제와 복기 | 피드백 루프 실험, 아이에게 책임 전가, 학습 과제 혼동 |

### 5.3 FT001 choice catalog

| choice_id | turn | rank | theory_id | intervention_type | quality_band | risk_flag |
|---|---:|---:|---|---|---|---|
| `ft001_t01_a_joining` | 1 | 1 | `system` | `joining` | `high` | `none` |
| `ft001_t01_b_ip_fixing` | 1 | 2 | `cbft` | `ip_fixing` | `risk` | `ip_fixed` |
| `ft001_t01_c_paperwork` | 1 | 3 | `procedure` | `paperwork` | `partial` | `premature_procedure` |
| `ft001_t02_a_circular_mapping` | 2 | 1 | `system` | `circular_mapping` | `high` | `none` |
| `ft001_t02_b_parent_directive` | 2 | 2 | `structural` | `parent_directive` | `partial` | `parent_blame` |
| `ft001_t02_c_pressure` | 2 | 3 | `strategic` | `pressure` | `risk` | `child_responsibilized` |
| `ft001_t03_a_emotion_reflection` | 3 | 1 | `satir` | `emotion_reflection` | `high` | `none` |
| `ft001_t03_b_correction` | 3 | 2 | `cbft` | `correction` | `partial` | `premature_correction` |
| `ft001_t03_c_exception` | 3 | 3 | `solution` | `exception` | `partial_high` | `none` |
| `ft001_t04_a_circular_question` | 4 | 1 | `system` | `circular_question` | `high` | `none` |
| `ft001_t04_b_premature_contract` | 4 | 2 | `cbft` | `premature_contract` | `partial` | `control_before_function` |
| `ft001_t04_c_diagnostic_closure` | 4 | 3 | `procedure` | `diagnostic_closure` | `risk` | `diagnostic_closure` |
| `ft001_t05_a_feedback_task` | 5 | 1 | `system` | `feedback_task` | `high` | `none` |
| `ft001_t05_b_compliance_promise` | 5 | 2 | `strategic` | `compliance_promise` | `risk` | `child_promise_only` |
| `ft001_t05_c_academic_homework` | 5 | 3 | `procedure` | `academic_homework` | `risk` | `therapy_learning_confused` |

`quality_band` 기준은 기본값으로 `high >= 80`, `partial = 50-79`, `risk < 50`을 둔다. `ft001_t03_c_exception`처럼 점수는 78이지만 교육적으로 의미 있는 선택은 `partial_high`로 표시해 대시보드에서 위험 선택과 구분한다.

## 6. 에이전트 유형별 반복 플레이 분석 설계

실제 학생 로그가 적거나 없을 때는 합성 에이전트 반복 플레이로 분석 설계를 검증한다. 각 agent는 같은 FT001 스크립트를 여러 seed로 플레이한다. 목표는 "정답률"보다 어떤 상담 판단 성향이 어떤 route와 metric 변화를 만드는지 비교하는 것이다.

### 6.1 agent profile schema

권장 파일명: `ft_agent_profiles.json`

| field | type | description |
|---|---:|---|
| `agent_id` | string | 고유 ID |
| `agent_type` | string | 분석 그룹 |
| `display_name` | string | 대시보드 표시명 |
| `selected_theory_id` | string | 시작 이론 렌즈 |
| `choice_policy` | string | 선택 규칙 설명 |
| `risk_tolerance` | float | 위험 선택 허용도 0-1 |
| `exploration_rate` | float | seed별 변동성 |
| `tie_break_rule` | string | 동점 선택 처리 |

### 6.2 권장 에이전트 유형

| agent_type | 선택 경향 | 검증 질문 |
|---|---|---|
| `system_aligned` | `system` 선택지와 순환 관찰을 우선 | 가족체계 렌즈가 FT001 추천 route와 얼마나 일치하는가 |
| `emotion_joining` | 합류, 정서 반영, 안전감 상승 선택 우선 | Satir식 정서 합류가 초기 안전감에 어떤 효과를 내는가 |
| `procedure_first` | 서류, 진단, 과제 확인 등 절차 선택 우선 | 절차 중심 선택이 신뢰/통찰을 낮추는 구간은 어디인가 |
| `behavior_contract_first` | 행동계약, 약속, 기준 설정을 빠르게 선택 | 통제/계약이 기능 이해보다 앞설 때 어떤 반응이 생기는가 |
| `solution_exception` | 예외 탐색과 작은 과제 선택 우선 | 해결중심 선택이 중간 route에서 보완적 효과를 내는가 |
| `risk_first` | 위험 회피와 구조화를 우선하되 과잉 절차화 가능 | 위험 민감성이 교육적으로 필요한 구조화와 어떻게 구분되는가 |
| `random_explorer` | seed 기반 무작위 선택 | baseline route/score 분포는 어떤가 |
| `novice_ip_fixing` | 아이 행동을 먼저 문제로 보는 선택 경향 | IP 고정이 반복될 때 엔딩과 metric이 어떻게 악화되는가 |

### 6.3 반복 실험 단위

권장 최소 실행:

- FT001 단일 사례: agent type 8개 x seed 30개 = 240 sessions
- 각 session은 5 turns x 핵심 event 약 8-15개 = 약 2,000-3,600 event rows
- 대시보드 집계 단위: `agent_type`, `selected_theory_id`, `ending_id`, `route_signature`, `risk_flag_count`

### 6.4 파생 지표

| metric | formula | use |
|---|---|---|
| `final_score` | 현재 `score` | 최종 성과 비교 |
| `trust_gain` | final trust - initial trust | 동맹/합류 효과 |
| `safety_gain` | final safety - initial safety | 정서적 안전감 변화 |
| `insight_gain` | final insight - initial insight | 가족 순환 이해 변화 |
| `high_quality_rate` | high 선택 수 / 5 | route 안정성 |
| `risk_choice_count` | risk 선택 수 | 위험한 상담 판단 빈도 |
| `system_alignment_rate` | `theory_id == recommended_theory_id` 선택 수 / 5 | FT001 추천 렌즈 일치 |
| `route_signature` | intervention_type sequence | 반복 route 비교 |
| `repair_after_rupture` | risk 선택 뒤 high/repair 선택 발생 여부 | 초보자 회복 가능성 분석 |
| `family_voice_balance` | reaction speaker 분포 | 특정 가족 구성원만 중심화했는지 확인 |

## 7. 대시보드 요구사항

대시보드는 "결과 장식"이 아니라 탐색 도구여야 한다. 발표 자료 기준으로 필터, 정렬, 비교 중 2개 이상이 실제 동작해야 하며, 이 설계에서는 세 기능 모두를 목표로 한다.

### 7.1 필터

필수 필터:

- `case_id`: 기본값 `FT-001`, 향후 `FT-002`-`FT-010` 확장
- `agent_type`: 사람/합성 에이전트 그룹
- `selected_theory_id`: 플레이 시작 렌즈
- `choice_theory_id`: 실제 선택지 이론
- `intervention_type`: `joining`, `circular_mapping`, `emotion_reflection` 등
- `quality_band`: `high`, `partial`, `risk`
- `risk_flag`: IP 고정, 성급한 진단, 아이 책임 전가 등
- `turn_index`: FT001의 1-5 장면
- `ending_id`: 결말 그룹
- `score_range`, `trust_gain_range`, `safety_gain_range`, `insight_gain_range`

권장 필터:

- `reaction_speaker_id`: 누가 주로 반응했는지
- `response_time_ms`: 빠른 선택/긴 숙고 비교
- `run_id` 또는 `seed`: 반복 실험 batch 비교

### 7.2 정렬

세션 테이블 정렬:

- `final_score` 내림차순
- `trust_gain`, `safety_gain`, `insight_gain` 내림차순
- `risk_choice_count` 내림/오름차순
- `system_alignment_rate` 내림차순
- `created_at` 최신순

선택지/턴 테이블 정렬:

- 선택 빈도 내림차순
- 평균 metric delta 내림차순
- 위험 flag 빈도 내림차순
- turn 순서순

### 7.3 비교

필수 비교 뷰:

1. agent type 비교: agent별 평균 점수, 위험 선택 수, route signature top 5
2. theory lens 비교: 시작 렌즈와 실제 선택 이론의 일치/불일치
3. turn 비교: FT001 어느 장면에서 metric이 가장 크게 갈리는지
4. route 비교: `A-A-A-A-A` best route와 procedure/risk route의 metric 차이

권장 시각화:

- 세션 summary table
- 턴별 metric delta line chart
- 선택지 빈도 stacked bar
- route signature Sankey 또는 alluvial chart
- risk flag heatmap: `agent_type x turn_index`
- reaction speaker distribution bar

## 8. 수업의 탐색형 대시보드 요건과 연결

수업 요구사항을 데이터 분석 산출물로 번역하면 다음과 같다.

| 수업 요건 | 이 프로젝트에서의 구현 |
|---|---|
| 데이터가 분석 가능한 형태여야 함 | 선택을 event row로 저장하고 CSV/JSON export |
| 필터 | 사례, 이론 렌즈, 선택 유형, 위험 flag, 에이전트 유형 필터 |
| 정렬 | 점수, metric gain, 위험 선택 수, route 일치율 정렬 |
| 비교 | 에이전트 유형, 이론 렌즈, route, 장면별 metric 비교 |
| 탐색 질문 | 어떤 개입 패턴이 신뢰/안전/통찰을 높이는가 |
| 설명 가능성 | 선택지 발화, 가족 반응, 슈퍼바이저 피드백을 drill-down으로 표시 |

핵심 탐색 질문:

- FT001에서 순환질문 route를 많이 선택한 세션은 실제로 `trust`, `safety`, `insight`가 모두 상승하는가?
- 절차 중심 선택은 항상 나쁜가, 아니면 위험 확인이 필요한 장면에서만 유용한가?
- `emotion_reflection`과 `exception` 선택은 추천 이론이 아니어도 route 회복에 기여하는가?
- IP 고정 선택이 한 번 발생한 뒤 다음 턴에서 회복 선택을 하면 엔딩이 얼마나 개선되는가?
- 에이전트 유형별로 가족 구성원 중 누구의 반응을 더 많이 끌어내는가?

## 9. Unity 저장 필드명 제안

Unity C# 내부에서는 camelCase를 쓰더라도 export 필드는 snake_case로 고정한다. 이후 대시보드와 Python/R 분석이 편해진다.

### 9.1 이벤트 저장용 C# 모델 후보

```csharp
[Serializable]
public sealed class ChoiceLogEvent
{
    public string eventId;
    public string sessionId;
    public int eventSeq;
    public string createdAt;
    public string schemaVersion;
    public string clientVersion;

    public string caseId;
    public int chapter;
    public string familyType;
    public string scriptKind;
    public string eventType;

    public string sceneId;
    public string sceneTitle;
    public int turnIndex;
    public int dialogueIndex;
    public string speakerId;
    public string speakerRole;
    public string expressionId;

    public string choiceId;
    public int choiceRank;
    public string choiceLabel;
    public string theoryId;
    public string selectedTheoryId;
    public string recommendedTheoryId;
    public string interventionType;
    public int quality;
    public string qualityBand;
    public string riskFlag;

    public int trustBefore;
    public int trustAfter;
    public int trustDelta;
    public int safetyBefore;
    public int safetyAfter;
    public int safetyDelta;
    public int insightBefore;
    public int insightAfter;
    public int insightDelta;

    public string routeFlags;
    public string familyReaction;
    public string reactionSpeakerId;
    public string reactionExpressionId;
    public string supervisorFeedback;
    public string endingId;

    public string agentId;
    public string agentType;
    public string runId;
    public int seed;
    public int responseTimeMs;
}
```

### 9.2 Export field naming

| C# field | CSV/JSON field |
|---|---|
| `eventId` | `event_id` |
| `sessionId` | `session_id` |
| `eventSeq` | `event_seq` |
| `createdAt` | `created_at` |
| `schemaVersion` | `schema_version` |
| `clientVersion` | `client_version` |
| `caseId` | `case_id` |
| `scriptKind` | `script_kind` |
| `eventType` | `event_type` |
| `sceneId` | `scene_id` |
| `turnIndex` | `turn_index` |
| `choiceId` | `choice_id` |
| `choiceLabel` | `choice_label` |
| `theoryId` | `theory_id` |
| `selectedTheoryId` | `selected_theory_id` |
| `recommendedTheoryId` | `recommended_theory_id` |
| `interventionType` | `intervention_type` |
| `qualityBand` | `quality_band` |
| `riskFlag` | `risk_flag` |
| `trustDelta` | `trust_delta` |
| `safetyDelta` | `safety_delta` |
| `insightDelta` | `insight_delta` |
| `routeFlags` | `route_flags` |
| `reactionSpeakerId` | `reaction_speaker_id` |
| `reactionExpressionId` | `reaction_expression_id` |
| `supervisorFeedback` | `supervisor_feedback` |
| `endingId` | `ending_id` |
| `agentType` | `agent_type` |
| `responseTimeMs` | `response_time_ms` |

## 10. 구현 우선순위

1. 현재 `PlayerChoiceLog` 유지: 세션 요약 CSV/JSON은 계속 export한다.
2. `ChoiceLogEvent` 추가: `scene_entered`, `dialogue_line_viewed`, `choice_deck_opened`, `choice_selected`, `reaction_shown`, `turn_completed`, `ending_resolved`부터 저장한다.
3. FT001 choice catalog 상수화: `choice_id`, `quality_band`, `risk_flag`를 선택지 생성 시 함께 저장한다.
4. 에이전트 반복 플레이 runner 추가: agent profile과 seed를 받아 자동 선택하고 `agent_id`, `agent_type`, `run_id`를 로그에 남긴다.
5. 대시보드 v2: 필터/정렬/비교 UI를 `dashboard.html`에 추가한다.

최소 제출 가능 기준은 `ft_choice_events.csv/json`에 `choice_selected`와 `ending_resolved`가 저장되고, 대시보드에서 `agent_type`, `intervention_type`, `quality_band` 필터와 `score`, `risk_choice_count` 정렬, agent 비교표가 동작하는 상태다.
