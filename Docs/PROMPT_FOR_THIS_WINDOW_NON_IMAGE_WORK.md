# 이 창용 프롬프트: 상용판 비이미지 구현 전담

아래 내용은 현재 창에서 이어서 할 작업 지시다. 이 창은 이미지 생성을 하지 않고, **상용판 비주얼노벨 게임 시스템 전체**를 담당한다.

```text
너의 역할은 `Family Therapy Practicum Unity` 프로젝트의 상용판 비이미지 구현 전담이다.

프로젝트 경로:
C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity

핵심 목표:
- 이 프로젝트는 단순 기말 데모가 아니라 10~15달러급 상용 가족치료 비주얼노벨/상담 시뮬레이션을 목표로 한다.
- 참고 방향은 Coffee Talk, VA-11 Hall-A, Syntherapy, Ace Attorney의 구조와 화면 문법이다.
- 이미지 창은 약 750장 규모의 상용판 자산을 PHASE 단위로 만든다.
- 이 창은 그 자산을 실제 게임 시스템에 연결할 Unity 런타임, 데이터 구조, 스크립트 구조, export, 검증 체계를 만든다.

절대 규칙:
- 이 창에서는 이미지 생성 도구를 사용하지 않는다.
- 이미지 생성은 별도 창이 담당한다.
- 이 창은 Unity 코드, 데이터 구조, VN 엔진, 콘텐츠 스크립트, 문서, 빌드, 스모크 테스트만 담당한다.
- 기존 60개 사례, 9개 가족치료 이론, 9명 슈퍼바이저, 점수/export 기능은 유지한다.
- 기존 사용자 변경을 되돌리지 않는다.
- 파일명은 이름이 아니라 역할/이론 ID 기준으로 유지한다.
- 게임 표시명은 확정 이름을 사용한다.
- 성별과 이미지 방향은 현재 생성된 이미지 기준을 유지한다.

확정 이름과 파일 ID:
- 박성빈: `ft001_mother`
- 이주형: `ft001_child`
- 오선진: `ft001_grandmother`
- 서건창: `ft001_teacher`
- 김혜성: `supervisor_system`
- 안우진: `supervisor_bowen`
- 김윤하: `supervisor_strategic`
- 이정후: `supervisor_structural`
- 김연주: `supervisor_satir`
- 송성문: `supervisor_psychodynamic`
- 정세영: `supervisor_cbft`
- 송지후: `supervisor_solution`
- 박병호: `supervisor_narrative`

현재 이미지 스타일 테스트 폴더:
Assets\ConceptArt\StyleTest_2026-06-08

현재 생성된 이미지:
- background_counseling_room_day.png
- ft001_child_front.png
- ft001_mother_front.png
- ft001_teacher_front.png
- supervisor_system_front.png
- supervisor_bowen_front.png
- supervisor_strategic_front.png
- supervisor_structural_front.png
- supervisor_satir_front.png
- supervisor_psychodynamic_front.png

이미지 창이 이어서 만들 이미지:
- ft001_grandmother_front.png
- supervisor_cbft_front.png
- supervisor_solution_front.png
- supervisor_narrative_front.png
- 이후 PHASE 1: FT-001 상용 회기용 44장
- 이후 PHASE 2~5: 총 약 750장 상용판 자산

상용판 비이미지 구현 목표:
1. 비주얼노벨 엔진
   - 배경 로딩
   - 캐릭터 스프라이트 로딩
   - 캐릭터 위치 배치
   - 현재 화자 강조
   - 대화창
   - 화자 이름표
   - 다음 버튼
   - 선택지 카드
   - 선택 후 가족 반응
   - 슈퍼바이저 컷인
   - HUD
   - 슈퍼비전 리포트

2. 콘텐츠 스크립트 구조
   - 핵심 사례 24개를 비주얼노벨 script로 확장할 수 있어야 한다.
   - 실습 사례 36개는 짧은 훈련형 script를 붙일 수 있어야 한다.
   - FT-001만 하드코딩하고 끝내지 않는다.
   - FT-001은 첫 구현 단위이지만, 전체 VN 엔진이 24개 핵심 사례로 확장 가능해야 한다.

3. 자산 매니페스트
   - 어떤 case가 어떤 background, characters, expressions, UI asset을 필요로 하는지 선언한다.
   - 이미지 파일이 없어도 게임이 깨지지 않고 fallback을 보여줘야 한다.
   - smoke JSON에서 누락 asset 목록을 출력한다.
   - 이미지 창이 만든 자산을 `Assets\Resources\VN`에 배치하면 자동으로 읽을 수 있어야 한다.

4. 데이터/export 확장
   - 기존 case/player CSV/JSON export는 유지한다.
   - VN 선택 로그를 추가한다.
   - 선택 당시 등장 캐릭터, 선택한 개입 유형, 반응 캐릭터, 감정 변화, 턴별 신뢰/안전감/통찰 변화가 export 가능해야 한다.

5. 상용판 진행 구조
   - 메인 메뉴는 카드형 도구 느낌에서 상용 게임 메뉴 느낌으로 이동해야 한다.
   - 센터 로비 화면을 도입한다.
   - 사례 파일 화면을 종이 파일/썸네일/관계도 중심으로 바꿀 수 있어야 한다.
   - 회기 화면은 VN 화면을 기본으로 한다.
   - 기존 카드형 회기 화면은 fallback/training mode로 남긴다.

권장 구현 순서:

PHASE A. 프로젝트 구조와 매니페스트 준비
- `Assets\Resources\VN\Characters`
- `Assets\Resources\VN\Backgrounds`
- `Assets\Resources\VN\UI`
- `Assets\Resources\VN\EventCG`
- 스타일 테스트 이미지는 계속 `Assets\ConceptArt\StyleTest_2026-06-08`에 둔다.
- 런타임 후보 이미지는 `Assets\Resources\VN` 아래로 복사해서 사용한다.
- 이미지가 아직 ConceptArt에만 있으면 임시로 그 자산을 복사하거나 fallback 경로로 참조한다.

PHASE B. VN 데이터 타입 추가
- `VnCharacterProfile`
  - id
  - displayName
  - role
  - baseAssetPath
  - defaultExpression
- `VnExpressionAsset`
  - characterId
  - expressionId
  - resourcePath
- `VnDialogueLine`
  - speakerId
  - expressionId
  - position
  - text
  - supervisorNote
- `VnChoice`
  - label
  - theoryId
  - interventionType
  - quality
  - feedback
  - familyReaction
  - reactionSpeakerId
  - reactionExpressionId
- `VnCaseScript`
  - caseId
  - chapter
  - backgroundId
  - characters
  - turns
- `VnTurn`
  - title
  - setupLines
  - choices

PHASE C. FT-001을 첫 상용 단위로 구현
- `START CAMPAIGN`은 `FT-001`부터 캠페인 VN 화면으로 진입한다.
- 박성빈, 이주형, 서건창, 오선진, 김혜성을 등장시킨다.
- 아직 오선진 이미지가 없으면 fallback panel을 보여준다.
- 5턴 회기 전체가 대화 진행과 선택지로 작동해야 한다.
- 선택지는 기존 점수 로직과 연결한다.
- 5턴이 끝나면 기존 슈퍼비전 리포트로 이동하되, 김혜성 컷인/코멘트를 보여줄 수 있는 구조로 확장한다.

PHASE D. 상용판 확장 가능한 case script registry
- FT-001 스크립트를 registry에 등록한다.
- FT-002~FT-010은 placeholder script entry를 만든다.
- 핵심 사례 24개를 나중에 추가할 수 있도록 registry 구조를 만든다.
- script가 없는 사례는 기존 카드형 회기 화면으로 fallback한다.

PHASE E. 자산 누락 검증과 smoke JSON 확장
- smoke JSON에 추가:
  - `visualNovelMode`
  - `vnScriptCount`
  - `vnCharacterProfileCount`
  - `vnRequiredAssetCount`
  - `vnAvailableAssetCount`
  - `missingVnAssets`
  - `ft001VnReady`
  - `commercialAssetTarget`
  - `commercialAssetCurrent`
- 이미지 창이 자산을 추가할수록 이 수치가 올라가야 한다.

PHASE F. 문서 갱신
- `Docs/VISUAL_NOVEL_REMAKE_PLAN.md`
  - 상용판 750장 목표 유지
  - 이름/파일 ID 정책 유지
  - VN 엔진 구조와 자산 매니페스트 구조 추가
- `Docs/CURRENT_BUILD_STATUS.md`
  - VN 시스템 진행 상태
  - 현재 이미지 수
  - 빌드/스모크 로그
- `Docs/CHARACTER_NAME_REGISTRY.md`
  - 이름과 파일 ID 분리 유지
- `Docs/PROMPT_FOR_THIS_WINDOW_NON_IMAGE_WORK.md`
  - 이 문서 자체도 최신 상태로 유지

PHASE G. 검증
- Unity Windows 빌드 성공
- 런타임 스모크 성공
- FT-001 VN 진입 가능
- 누락 이미지 fallback 확인
- export 파일 생성 확인
- smoke JSON 수치 확인
- 가능하면 실행본을 열어 사용자가 볼 수 있게 한다.

완료 기준:
- 이 창의 목표는 “이미지 몇 장을 기다리는 것”이 아니라, 이미지 창에서 만들어질 750장 자산을 받아 상용판 게임에 연결할 수 있는 구조를 만드는 것이다.
- 첫 구현은 FT-001이지만, 설계와 코드 구조는 24개 핵심 사례와 60개 전체 사례로 확장 가능해야 한다.
- 결과물이 다시 카드형 교육 도구처럼 보이면 실패다. VN 화면과 상용 게임 루프가 기본 방향이어야 한다.
```
