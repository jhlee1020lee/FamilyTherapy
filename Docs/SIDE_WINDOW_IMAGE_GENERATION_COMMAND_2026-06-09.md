# 옆 창 이미지 생성 전담 지시문 2026-06-09

아래 지시문 전체를 옆 창에 전달한다. 옆 창은 이미지 생성만 담당한다.

```text
너의 역할은 `Family Therapy Practicum Unity` 프로젝트의 상용판 이미지 생성 전담이다.

프로젝트 경로:
C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity

먼저 참고할 기준 문서:
C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity\Docs\LARGE_SCALE_STEAM_GAME_BENCHMARK_AND_REDESIGN_2026-06-09.md

이 기준 문서는 Steam/상용 VN·상담·추리·내러티브 게임 30개 이상을 UI, 그래픽, 진행, 캐릭터, 대사 관점으로 정리한 것이다. 이미지 생성은 이 문서의 방향을 따른다.

절대 역할 분리:
- 너는 이미지 생성, 이미지 검수, 생성 파일 정리만 한다.
- Unity 코드 수정, 문서 수정, 빌드, 테스트, Git 작업은 하지 않는다.
- 다른 창이 코드와 적용 검증을 담당한다.

프로젝트 목표:
- 데모가 아니라 10~15달러급 상용 가족치료 비주얼노벨/상담 시뮬레이션을 목표로 한다.
- 최종 자산 목표는 약 750장 규모다.
- 지금은 우선 FT-001 사례와 핵심 UI를 상용판 첫 챕터처럼 보이게 만드는 것이 1순위다.
- 참고 방향은 Coffee Talk, VA-11 Hall-A, Vampire Therapist, Ace Attorney 계열의 화면 문법이다.
- 특정 상용 게임의 그림체, 캐릭터, UI를 복제하지 말고, 상담/교육 프로젝트에 맞는 독자 스타일로 만든다.

단일 UI 기준작:
- UI 디자인 기준은 `Coffee Talk`로 고정한다.
- 자유롭게 새로운 UI를 발명하지 말고, Coffee Talk식 "고정 장소 + 캐릭터 중심 + 하단 대화창 + 적은 HUD + 따뜻한 실내 분위기" 문법을 상담실 게임으로 번역한다.
- 원본 캐릭터, 배경, 로고, 아이콘, UI 이미지를 복제하지 않는다.
- 카페가 아니라 한국 가족치료 상담센터로 바꾼다.
- 커피 제조 UI의 대응물은 상담 개입 덱, 사례 파일, 슈퍼바이저 노트다.

현재 승인된 그림 방향:
- 상담드라마풍 세미애니.
- 현대 한국 상담센터, 학교, 가정, 복지기관 분위기.
- 과장된 애니, 치비, 공포 병원, 선정적 연출, 모바일 상담 앱 느낌 금지.
- 현실적인 한국 인물 비율, 차분한 감정, 따뜻하지만 전문적인 색감.
- 캐릭터는 투명 배경 PNG로 런타임에 바로 쓸 수 있게 만든다.
- 배경은 16:9 비주얼노벨 배경으로 만든다.
- UI는 텍스트 없는 투명 PNG 또는 16:9 오버레이 후보로 만든다.

가장 중요한 최신 수정:
- 이주형은 남자 초등학생이다.
- 서건창은 남자 담임교사다.
- 이전 생성 후보에서 이주형과 서건창이 여성처럼 보여 런타임에서 잘못 표시됐다.
- 앞으로 이주형/서건창이 여성, 중학생 이상, 성인처럼 보이면 실패 처리한다.
- 캐릭터 정체성은 고정하고 표정만 바꾼다. 표정마다 성별, 나이대, 헤어스타일, 의상 실루엣이 바뀌면 실패다.

현재 긴급 교정으로 코드에 연결된 임시 파일:
- Assets\Resources\VN\Characters\FT001\ft001_child_male_neutral_phase1.png
- Assets\Resources\VN\Characters\FT001\ft001_teacher_male_neutral_phase1.png

하지만 최종 목표는 임시 파일 유지가 아니라 아래 정식 표정 세트를 다시 생성하는 것이다:
- Assets\Resources\VN\Characters\FT001\ft001_child_<expression>_phase1.png
- Assets\Resources\VN\Characters\FT001\ft001_teacher_<expression>_phase1.png

이름/ID 매핑:
- 박성빈: ft001_mother, 어머니, 여성
- 이주형: ft001_child, 자녀, 남자 초등학생
- 오선진: ft001_grandmother, 외조모, 여성 60~70대
- 서건창: ft001_teacher, 담임, 남성 30~40대
- 김혜성: supervisor_system, 가족체계 기본 슈퍼바이저
- 안우진: supervisor_bowen, Bowen 슈퍼바이저
- 김윤하: supervisor_strategic, 전략적 슈퍼바이저
- 이정후: supervisor_structural, 구조적 슈퍼바이저
- 김연주: supervisor_satir, Satir 슈퍼바이저
- 송성문: supervisor_psychodynamic, 정신역동 슈퍼바이저
- 정세영: supervisor_cbft, 인지행동 가족치료 슈퍼바이저
- 송지후: supervisor_solution, 해결중심 슈퍼바이저
- 박병호: supervisor_narrative, 이야기치료 슈퍼바이저

저장 위치:
- 캐릭터 런타임 후보:
  Assets\Resources\VN\Characters\FT001\
  Assets\Resources\VN\Characters\Supervisors\
- 배경:
  Assets\Resources\VN\Backgrounds\
- UI:
  Assets\Resources\VN\UI\
- 이벤트 CG:
  Assets\Resources\VN\EventCG\
- 생성 원본/검수용:
  Docs\GeneratedSources\

파일명 규칙:
- 기존 런타임 파일을 바로 덮어쓰지 말고 먼저 `_candidate01`, `_candidate02` 또는 `_v2`를 붙인다.
- 예시:
  ft001_child_anxious_phase1_candidate01.png
  ft001_teacher_concerned_phase1_candidate01.png
- 최종 승인 후 코드 적용 창이 정식 파일명으로 교체한다.

공통 캐릭터 프롬프트:
Create a transparent-background waist-up visual novel character sprite of a Korean [role] for a serious commercial family therapy counseling simulation. Modern Korean counseling drama tone, mature semi-anime style, realistic proportions, subtle facial emotion, clean linework, soft painterly shading, grounded contemporary clothing, professional but warm. Front-facing with slight 3/4 angle, consistent character identity across expressions. No text, no watermark, no chibi, no fantasy, no hospital horror, no photorealistic stock photo.

공통 배경 프롬프트:
Create a 16:9 detailed painterly visual novel background for a modern Korean family therapy counseling simulation game. Warm professional atmosphere, grounded contemporary Korean setting, clear empty left/center/right foreground space for character sprites, polished commercial game concept art. No text, no logos, no people, no horror, no hospital ward, no photorealistic stock photo look.

공통 UI 프롬프트:
Create a polished visual novel UI asset for a serious Korean family therapy counseling simulation game. Professional counseling record and warm paper-file aesthetic, subtle teal/warm wood/neutral palette, clean readable shape, commercial game UI polish, no readable text unless explicitly requested, transparent PNG where possible, no logo, no watermark.

우선순위 1: 이주형/서건창 정식 표정 세트 재생성

이주형 `ft001_child`, 남자 초등학생:
- neutral
- anxious
- withdrawn
- scared
- quiet
- relieved
- hesitant
- listening

외형 고정:
- 한국 남자 초등학생.
- 짧은 검은 머리.
- 왜소하거나 긴장한 자세.
- 후드집업 또는 편한 등교복 느낌.
- 여학생처럼 보이는 긴 머리, 치마, 여성적 얼굴형 금지.

서건창 `ft001_teacher`, 남자 담임교사:
- neutral
- concerned
- procedural
- softened

외형 고정:
- 한국 남성 30~40대.
- 단정한 머리.
- 셔츠, 니트, 재킷, 교사다운 차분한 복장.
- 여성 교사처럼 보이는 헤어/얼굴/체형 금지.

우선순위 2: FT-001 전체 가족 표정 세트

박성빈 `ft001_mother`:
- neutral
- anxious
- defensive
- exhausted
- softened
- worried
- tearful
- listening

오선진 `ft001_grandmother`:
- neutral
- critical
- worried
- defensive
- softened
- stubborn

김혜성 `supervisor_system`:
- neutral
- explaining
- warning
- approving
- questioning
- reflective

우선순위 3: FT-001 배경 4장
- counseling_room_day
- counseling_room_evening
- counseling_room_tense
- supervision_room_day

우선순위 4: VN UI 자산 8장
- dialogue_box
- speaker_nameplate
- choice_card_question
- choice_card_intervention
- supervisor_note_panel
- case_file_panel
- metrics_hud
- session_result_sheet

UI 요구:
- 현재 네모 박스 느낌을 줄이고 상용 VN처럼 정돈된 레이어감이 있어야 한다.
- 대화창은 캐릭터 하반신 일부만 덮고 얼굴과 핵심 포즈를 가리지 않아야 한다.
- 버튼과 패널에는 텍스트를 직접 넣지 않는다. 텍스트는 Unity 쪽에서 얹는다.
- 1600x900 기준에서 선명해야 한다.

우선순위 5: 슈퍼바이저 9명 6표정 세트

대상:
- supervisor_system 김혜성
- supervisor_bowen 안우진
- supervisor_strategic 김윤하
- supervisor_structural 이정후
- supervisor_satir 김연주
- supervisor_psychodynamic 송성문
- supervisor_cbft 정세영
- supervisor_solution 송지후
- supervisor_narrative 박병호

표정:
- neutral
- explaining
- questioning
- warning
- approving
- reflective

검수 기준:
- 파일명, 캐릭터 ID, 표정 ID가 정확해야 한다.
- 같은 캐릭터는 표정이 달라도 동일 인물로 보여야 한다.
- 이주형/서건창 성별 오류는 절대 통과시키지 않는다.
- 해상도는 캐릭터 최소 세로 1400px 이상, 배경 1920x1080 이상을 목표로 한다.
- 캐릭터 배경은 투명해야 한다. 투명 처리가 어렵다면 크로마키 원본과 투명 PNG를 모두 저장한다.
- 이미지에 글자, 워터마크, 로고가 있으면 실패다.

작업 보고 형식:
1. 생성한 파일 목록
2. 실패하거나 다시 뽑아야 하는 파일 목록
3. 이주형/서건창 성별 검수 결과
4. 스타일 일관성 코멘트
5. 다음 배치에서 생성할 추천 목록
```
