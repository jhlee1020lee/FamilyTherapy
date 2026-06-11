# FT001 CG 재생성 지시서: 중복 제거 + 구도 고정 + 일관성 강화

## 목적

FT001 line-by-line Event CG 67장을 다시 만든다.

이번 작업의 핵심은 단순히 이미지를 많이 만드는 것이 아니다. 다음 세 가지 문제를 해결해야 한다.

1. 각 대사마다 다른 이미지가 필요하지만, 현재는 중복되거나 거의 같은 이미지가 많다.
2. 이미지끼리 인물 얼굴, 의상, 방 분위기, 색감이 조금씩 달라 일관성이 부족하다.
3. 컷마다 카메라 위치와 구도가 미세하게 흔들려서 게임 진행 중 정신없게 보인다.

이번 재생성의 목표는 `상용 비주얼노벨의 Event CG 세트처럼 보이는 FT001 상담 장면 67장`이다.

## 2026-06-11 코드 기준 컷 수 정정

처음에는 기존 산출물 기준으로 50장을 재생성 대상으로 보았지만, 실제 Unity 코드의 `Ft001Line(...)`, `Ft001Choice(...)`, 선택지 idle 경로를 확인하면 필요한 컷은 67장이다.

구성:

- 실제 대사/반응/인트로/분기 slug: 62장
- 선택지 대기 화면 `choice_idle`: 5장
- 총계: 67장

따라서 이번 재생성은 50장이 아니라 67장을 기준으로 한다. 50장만 만들면 인트로, 선택 결과 이후 분기 첫 대사, 선택지 화면 일부가 기존 이미지나 commercial branching 이미지로 남아 “대사마다 다른 사진” 조건을 만족하지 못한다.

## 현재 확인된 문제

### 완전 중복

현재 manifest와 파일 해시 기준으로 아래 두 파일은 완전히 같은 이미지다.

- `ft001_cg_t02_reaction_c_child_withdrawn.png`
- `ft001_cg_t04_reaction_c_child_withdrawn.png`

이전 manifest에는 안전 필터 때문에 새 이미지를 만들지 못해 기존 컷을 재사용했다고 기록되어 있다.

이번 작업에서는 `fallback 재사용`을 금지한다. 특정 컷 생성이 막히면 같은 이미지를 복사하지 말고, 안전한 표현으로 장면을 재서술해서 새로 만든다.

### 거의 같은 이미지가 많음

현재 이미지들은 `대사별 이미지`라는 이름은 붙어 있지만, 실제로는 같은 마스터샷에서 표정 차이가 작거나 거의 보이지 않는 컷이 많다.

이번에는 각 대사마다 최소 하나 이상의 명확한 시각 차이를 둔다.

허용되는 차이:

- 말하는 사람의 입 모양
- 시선 방향
- 손 위치
- 몸 기울기
- 표정 강도
- 다른 인물이 듣는 반응
- 상담실 내 긴장감 변화

금지되는 방식:

- 같은 이미지를 파일명만 바꿔 재사용
- 아주 미세한 색감 차이만 있는 이미지
- 사람 위치와 표정이 거의 같은 이미지
- 안전 필터 회피를 이유로 기존 컷 복사

## 핵심 제작 원칙

### 1. 카메라는 적게, 행동은 다르게

이전 문제는 컷마다 구도가 조금씩 흔들린 것이다.

이번에는 카메라 구도를 많이 바꾸지 않는다. 대신 같은 구도 안에서 표정, 시선, 손동작, 몸 방향을 바꿔 대사별 차이를 만든다.

즉:

- 나쁜 방식: 매 컷마다 카메라 위치가 조금씩 달라짐
- 좋은 방식: 카메라는 고정, 인물의 반응만 달라짐

### 2. 모든 가족 상담 장면은 같은 상담실, 같은 좌석 배치

상담실:

- 따뜻한 목재 톤의 가족상담실
- 창문은 왼쪽
- 벽면 책장과 문은 오른쪽
- 낮은 원형 또는 타원형 상담 테이블이 전경에 있음
- 푸른빛 또는 청록빛 패브릭 상담 의자
- 부드러운 오후 실내광
- 현실적인 한국 상담센터 분위기

가족 좌석 배치:

- 화면 왼쪽: 외조모 오선진
- 화면 중앙 왼쪽: 어머니 박성빈
- 화면 중앙 오른쪽: 자녀 이주형
- 화면 오른쪽: 담임 서건창

김혜성 슈퍼바이저:

- 여성
- 가족과 마주 앉은 상담자/슈퍼바이저 위치
- 김혜성이 말할 때는 가족 전체 마스터샷이 아니라 `김혜성 단독 또는 김혜성 중심 리버스샷`을 사용

### 3. 인물 디자인 고정

인물 외형은 모든 컷에서 유지한다.

박성빈:

- 30대 후반 한국 여성
- 피곤하지만 무너지지 않으려는 표정
- 베이지/회색 니트 또는 차분한 상의
- 자연스러운 갈색 머리, 낮게 묶거나 단정히 정리

이주형:

- 초등학생 한국 남자아이
- 회색 후디
- 작게 움츠린 자세
- 불안하거나 조심스러운 눈빛

오선진:

- 60대 후반 한국 여성
- 회색 웨이브 머리
- 니트 가디건
- 걱정과 비판이 섞인 표정

서건창:

- 30대 한국 남성 담임교사
- 안경
- 회색 재킷 또는 단정한 교사 복장
- 클립보드나 노트 가능

김혜성:

- 40대 한국 여성 슈퍼바이저
- 단정한 상담자 복장
- 침착하고 따뜻하지만 전문적인 표정
- 서건창과 절대 닮지 않게 할 것

### 4. 스타일 고정

모든 이미지는 다음 스타일을 유지한다.

- realistic cinematic Korean family therapy visual novel CG
- live-action still 느낌이지만 지나치게 광고 사진처럼 반짝이지 않음
- 상담 장면에 맞는 낮은 채도
- 피부와 의상 색감 일관
- 1600x900
- 16:9
- 텍스트, UI, 자막, 말풍선, 워터마크 없음

금지:

- 애니메이션풍
- 만화풍
- 게임 UI 포함
- 과도한 필름 그레인
- 극단적인 클로즈업
- 카메라가 컷마다 높이/각도를 바꾸는 구성
- 새 인물 추가
- 좌석 순서 변경

## 고정 카메라 슬롯

아래 슬롯만 사용한다. 임의의 새 카메라 구도를 만들지 않는다.

### CAM_A_FAMILY_WIDE

가족 네 명이 모두 보이는 정면 마스터샷.

사용 목적:

- 가족 전체의 긴장
- 어머니와 아이의 상호작용
- 외조모와 담임의 반응
- 선택지 idle

구도:

- 화면 왼쪽부터 외조모, 어머니, 자녀, 담임
- 모두 앉아 있음
- 전경에 상담 테이블
- 카메라 높이 eye level
- 35mm 렌즈 느낌

### CAM_B_MOTHER_MEDIUM

어머니 중심 medium shot. 아이는 옆에 일부 보일 수 있음.

사용 목적:

- 어머니가 말하거나 감정이 흔들리는 장면
- 어머니의 방어, 소진, 눈물, 완화 표현

구도:

- 어머니가 화면 중심
- 아이는 오른쪽 가장자리에 작게 보일 수 있음
- 카메라 위치와 크롭 고정

### CAM_C_CHILD_MEDIUM

자녀 중심 medium shot. 어머니 또는 담임이 일부 보일 수 있음.

사용 목적:

- 아이가 불안, 침묵, 위축, 안도감을 보이는 장면

구도:

- 아이가 화면 중심
- 어깨를 움츠리고 앉아 있음
- 카메라 위치와 크롭 고정

### CAM_D_GRANDMOTHER_MEDIUM

외조모 중심 medium shot.

사용 목적:

- 외조모의 비판, 걱정, 완화 반응

구도:

- 외조모가 화면 중심
- 어머니가 오른쪽 가장자리에 일부 보일 수 있음
- 카메라 위치와 크롭 고정

### CAM_E_TEACHER_MEDIUM

담임 중심 medium shot.

사용 목적:

- 담임의 절차적 설명, 걱정, 완화 반응

구도:

- 담임이 화면 중심
- 노트/클립보드 가능
- 카메라 위치와 크롭 고정

### CAM_F_SUPERVISOR_REVERSE

김혜성 슈퍼바이저 중심 리버스샷.

사용 목적:

- 김혜성이 말하는 모든 컷
- 슈퍼바이저 질문, 설명, 승인, 성찰

구도:

- 김혜성이 화면 중심
- 가족은 어깨 너머 실루엣이나 흐릿한 뒷모습으로만 일부 가능
- 김혜성을 서건창처럼 보이게 만들지 말 것
- 여성 슈퍼바이저 단독/중심 구도

## 컷별 재생성 표

아래 67개 파일을 전부 새로 만든다. 각 컷은 지정한 카메라 슬롯을 사용하고, `visible difference`를 반드시 반영한다.

| 파일명 | 카메라 | 핵심 인물 | visible difference |
| --- | --- | --- | --- |
| ft001_cg_t01_choice_idle.png | CAM_A_FAMILY_WIDE | 전체 | 모두 침묵, 상담 시작 전 긴장, 손은 무릎 위 |
| ft001_cg_t01_l01_mother_neutral.png | CAM_B_MOTHER_MEDIUM | 어머니 | 어머니가 조심스럽게 말 시작, 입 살짝 열림, 손가락을 맞잡음 |
| ft001_cg_t01_l02_child_anxious.png | CAM_C_CHILD_MEDIUM | 자녀 | 아이가 시선을 아래로 피함, 후디 소매를 잡음 |
| ft001_cg_t01_l03_mother_worried.png | CAM_B_MOTHER_MEDIUM | 어머니 | 어머니 눈썹이 올라가고 걱정이 커짐, 한 손이 가슴 쪽 |
| ft001_cg_t01_l04_child_quiet.png | CAM_C_CHILD_MEDIUM | 자녀 | 아이가 더 움츠림, 입을 다물고 어머니 쪽을 곁눈질 |
| ft001_cg_t01_l05_teacher_concerned.png | CAM_E_TEACHER_MEDIUM | 담임 | 담임이 노트를 보며 조심스럽게 설명, 걱정스러운 표정 |
| ft001_cg_t01_l06_supervisor_explaining.png | CAM_F_SUPERVISOR_REVERSE | 김혜성 | 김혜성이 차분히 설명, 손바닥을 낮게 펼침 |
| ft001_cg_t01_reaction_a_mother_softened.png | CAM_B_MOTHER_MEDIUM | 어머니 | 어머니 표정이 조금 풀림, 어깨가 내려감 |
| ft001_cg_t01_reaction_b_child_withdrawn.png | CAM_C_CHILD_MEDIUM | 자녀 | 아이가 방어적으로 몸을 더 접음, 시선 완전히 아래 |
| ft001_cg_t01_reaction_c_teacher_procedural.png | CAM_E_TEACHER_MEDIUM | 담임 | 담임이 절차 설명으로 돌아감, 클립보드를 들어 올림 |
| ft001_cg_t02_choice_idle.png | CAM_A_FAMILY_WIDE | 전체 | 이전보다 어머니와 외조모 사이 긴장 증가, 아이는 고개 숙임 |
| ft001_cg_t02_l01_mother_defensive.png | CAM_B_MOTHER_MEDIUM | 어머니 | 어머니가 방어적으로 몸을 뒤로 뺌, 손이 굳음 |
| ft001_cg_t02_l02_child_quiet.png | CAM_C_CHILD_MEDIUM | 자녀 | 아이가 말하려다 멈춤, 입술을 누름 |
| ft001_cg_t02_l03_mother_exhausted.png | CAM_B_MOTHER_MEDIUM | 어머니 | 어머니가 눈을 잠깐 감거나 아래를 봄, 피로감 강조 |
| ft001_cg_t02_l04_child_hesitant.png | CAM_C_CHILD_MEDIUM | 자녀 | 아이가 아주 작게 말하려는 듯 고개를 듦 |
| ft001_cg_t02_l05_teacher_procedural.png | CAM_E_TEACHER_MEDIUM | 담임 | 담임이 출석/절차를 설명, 표정은 딱딱하지만 악의 없음 |
| ft001_cg_t02_l06_supervisor_explaining.png | CAM_F_SUPERVISOR_REVERSE | 김혜성 | 김혜성이 가족 전체를 바라보며 설명, 메모펜을 내려놓음 |
| ft001_cg_t02_reaction_a_mother_softened.png | CAM_B_MOTHER_MEDIUM | 어머니 | 어머니가 고개를 끄덕이며 조금 누그러짐 |
| ft001_cg_t02_reaction_b_mother_defensive.png | CAM_B_MOTHER_MEDIUM | 어머니 | 어머니가 팔을 모으고 방어적인 표정 |
| ft001_cg_t02_reaction_c_child_withdrawn.png | CAM_C_CHILD_MEDIUM | 자녀 | 아이가 의자 깊숙이 물러남, 두 손을 꽉 잡음 |
| ft001_cg_t03_choice_idle.png | CAM_A_FAMILY_WIDE | 전체 | 외조모가 앞으로 기울고, 어머니가 긴장, 아이는 중앙에서 작아 보임 |
| ft001_cg_t03_l01_grandmother_critical.png | CAM_D_GRANDMOTHER_MEDIUM | 외조모 | 외조모가 비판적으로 말함, 손가락을 살짝 세움 |
| ft001_cg_t03_l02_mother_exhausted.png | CAM_B_MOTHER_MEDIUM | 어머니 | 어머니가 외조모 말을 듣고 지친 표정, 시선 아래 |
| ft001_cg_t03_l03_grandmother_worried.png | CAM_D_GRANDMOTHER_MEDIUM | 외조모 | 외조모의 비판 아래 걱정이 드러남, 손을 모음 |
| ft001_cg_t03_l04_mother_tearful.png | CAM_B_MOTHER_MEDIUM | 어머니 | 어머니 눈가가 젖음, 입술을 다물고 버팀 |
| ft001_cg_t03_l05_child_scared.png | CAM_C_CHILD_MEDIUM | 자녀 | 아이가 외조모와 어머니 사이를 불안하게 봄 |
| ft001_cg_t03_l06_supervisor_questioning.png | CAM_F_SUPERVISOR_REVERSE | 김혜성 | 김혜성이 부드럽게 질문, 고개 살짝 기울임 |
| ft001_cg_t03_reaction_a_grandmother_softened.png | CAM_D_GRANDMOTHER_MEDIUM | 외조모 | 외조모 표정이 누그러짐, 손을 내려놓음 |
| ft001_cg_t03_reaction_b_grandmother_defensive.png | CAM_D_GRANDMOTHER_MEDIUM | 외조모 | 외조모가 방어적으로 몸을 세움, 입술 굳음 |
| ft001_cg_t03_reaction_c_child_hesitant.png | CAM_C_CHILD_MEDIUM | 자녀 | 아이가 조심스럽게 어머니 쪽을 봄, 말할지 망설임 |
| ft001_cg_t04_choice_idle.png | CAM_A_FAMILY_WIDE | 전체 | 상담 중반, 모두 조금 지친 상태, 김혜성 발화 전 긴장 |
| ft001_cg_t04_l01_supervisor_questioning.png | CAM_F_SUPERVISOR_REVERSE | 김혜성 | 김혜성이 순환 질문을 던짐, 펜을 든 손 |
| ft001_cg_t04_l02_mother_worried.png | CAM_B_MOTHER_MEDIUM | 어머니 | 어머니가 아이를 걱정스럽게 봄, 몸이 아이 쪽으로 기움 |
| ft001_cg_t04_l03_child_quiet.png | CAM_C_CHILD_MEDIUM | 자녀 | 아이가 어머니 시선을 느끼고 작게 고개를 숙임 |
| ft001_cg_t04_l04_mother_listening.png | CAM_B_MOTHER_MEDIUM | 어머니 | 어머니가 말하지 않고 듣는 자세, 손을 풀기 시작 |
| ft001_cg_t04_l05_child_hesitant.png | CAM_C_CHILD_MEDIUM | 자녀 | 아이가 아주 작게 입을 열려 함, 시선은 상담자 쪽 |
| ft001_cg_t04_l06_supervisor_explaining.png | CAM_F_SUPERVISOR_REVERSE | 김혜성 | 김혜성이 선택의 의미를 설명, 부드럽지만 단호함 |
| ft001_cg_t04_reaction_a_supervisor_approving.png | CAM_F_SUPERVISOR_REVERSE | 김혜성 | 김혜성이 작게 고개를 끄덕임, 승인/격려 표정 |
| ft001_cg_t04_reaction_b_mother_anxious.png | CAM_B_MOTHER_MEDIUM | 어머니 | 어머니가 불안해져 손을 다시 움켜쥠 |
| ft001_cg_t04_reaction_c_child_withdrawn.png | CAM_C_CHILD_MEDIUM | 자녀 | 새로 생성 필수, t02와 복사 금지, 아이가 상담자 시선을 피하며 몸을 닫음 |
| ft001_cg_t05_choice_idle.png | CAM_A_FAMILY_WIDE | 전체 | 회기 후반, 긴장은 남아 있지만 약간 풀림 |
| ft001_cg_t05_l01_teacher_concerned.png | CAM_E_TEACHER_MEDIUM | 담임 | 담임이 아이를 걱정하는 쪽으로 말함, 절차보다 관계적 표정 |
| ft001_cg_t05_l02_mother_softened.png | CAM_B_MOTHER_MEDIUM | 어머니 | 어머니가 아이를 향해 부드럽게 몸을 돌림 |
| ft001_cg_t05_l03_child_relieved.png | CAM_C_CHILD_MEDIUM | 자녀 | 아이가 아주 작게 안도, 어깨가 내려감 |
| ft001_cg_t05_l04_grandmother_softened.png | CAM_D_GRANDMOTHER_MEDIUM | 외조모 | 외조모가 걱정스럽지만 비판을 줄임, 시선이 부드러움 |
| ft001_cg_t05_l05_teacher_softened.png | CAM_E_TEACHER_MEDIUM | 담임 | 담임이 노트를 내려놓고 가족을 바라봄 |
| ft001_cg_t05_l06_supervisor_reflective.png | CAM_F_SUPERVISOR_REVERSE | 김혜성 | 김혜성이 회기를 정리하며 성찰적 표정 |
| ft001_cg_t05_reaction_a_mother_softened.png | CAM_B_MOTHER_MEDIUM | 어머니 | 어머니가 눈물이 조금 남은 채 안도, 손을 느슨하게 폄 |
| ft001_cg_t05_reaction_b_child_scared.png | CAM_C_CHILD_MEDIUM | 자녀 | 아이가 다시 불안해짐, 하지만 t03 scared와 다른 손/시선 |
| ft001_cg_t05_reaction_c_teacher_procedural.png | CAM_E_TEACHER_MEDIUM | 담임 | 담임이 다시 절차를 언급하지만 목소리는 낮아진 느낌 |

### 코드 호출 기준 추가 필수 컷

아래 컷들은 현재 50장 세트에는 없지만 실제 Unity 코드에서 호출된다. 반드시 새로 만든다.

| 파일명 | 카메라 | 핵심 인물 | visible difference |
| --- | --- | --- | --- |
| ft001_cg_intro_01_mother_neutral.png | CAM_B_MOTHER_MEDIUM | 어머니 | 인물 소개, 어머니가 자신을 소개하며 피로를 억누르는 표정 |
| ft001_cg_intro_02_child_neutral.png | CAM_C_CHILD_MEDIUM | 자녀 | 인물 소개, 아이가 조심스럽게 앉아 있음, 아직 깊은 불안 전 단계 |
| ft001_cg_intro_03_grandmother_neutral.png | CAM_D_GRANDMOTHER_MEDIUM | 외조모 | 인물 소개, 걱정이 있지만 비판 전의 단정한 표정 |
| ft001_cg_intro_04_teacher_neutral.png | CAM_E_TEACHER_MEDIUM | 담임 | 인물 소개, 담임이 정중하게 앉아 가족을 살핌 |
| ft001_cg_intro_05_supervisor_explaining.png | CAM_F_SUPERVISOR_REVERSE | 김혜성 | 인물 소개, 김혜성이 회기 목표를 설명 |
| ft001_cg_t02_l00_branch_mother_open.png | CAM_B_MOTHER_MEDIUM | 어머니 | T1 좋은 선택 이후, 어머니가 덜 몰린 상태로 장면을 떠올림 |
| ft001_cg_t02_l00_branch_child_closed.png | CAM_C_CHILD_MEDIUM | 자녀 | T1 위험 선택 이후, 아이가 고쳐져야 할 사람처럼 느껴 닫힘 |
| ft001_cg_t02_l00_branch_teacher_cautious.png | CAM_E_TEACHER_MEDIUM | 담임 | T1 절차 선택 이후, 담임이 조심스럽게 절차 이야기를 이어감 |
| ft001_cg_t03_l00_branch_child_links_pattern.png | CAM_C_CHILD_MEDIUM | 자녀 | T2 좋은 선택 이후, 아이가 패턴을 작게 연결해 말함 |
| ft001_cg_t03_l00_branch_mother_defensive.png | CAM_B_MOTHER_MEDIUM | 어머니 | T2 위험 선택 이후, 어머니가 책임을 떠안은 듯 방어적 |
| ft001_cg_t03_l00_branch_mother_cautious.png | CAM_B_MOTHER_MEDIUM | 어머니 | T2 부분 선택 이후, 도움은 됐지만 아직 조심스러운 표정 |
| ft001_cg_t04_l00_branch_grandmother_softened.png | CAM_D_GRANDMOTHER_MEDIUM | 외조모 | T3 좋은 선택 이후, 외조모가 덜 억울해하며 부드러워짐 |
| ft001_cg_t04_l00_branch_grandmother_stubborn.png | CAM_D_GRANDMOTHER_MEDIUM | 외조모 | T3 위험 선택 이후, 외조모가 말문을 닫고 몸을 세움 |
| ft001_cg_t04_l00_branch_child_exception.png | CAM_C_CHILD_MEDIUM | 자녀 | T3 해결중심 선택 이후, 아이가 예외 장면을 조심스럽게 말함 |
| ft001_cg_t05_l00_branch_teacher_adjusts.png | CAM_E_TEACHER_MEDIUM | 담임 | T4 좋은 선택 이후, 담임이 학교 연락 조정을 받아들임 |
| ft001_cg_t05_l00_branch_child_scared.png | CAM_C_CHILD_MEDIUM | 자녀 | T4 진단식 선택 이후, 아이가 다시 개인 문제로 좁혀진 느낌 |
| ft001_cg_t05_l00_branch_mother_anxious.png | CAM_B_MOTHER_MEDIUM | 어머니 | T4 행동계약 선택 이후, 어머니가 실패 부담을 걱정 |

### 선택지 idle 필수 컷

선택지 화면에서도 구도 흔들림이 보이면 게임 리듬이 깨진다. 아래 5장도 반드시 만든다.

| 파일명 | 카메라 | 핵심 인물 | visible difference |
| --- | --- | --- | --- |
| ft001_cg_t01_choice_idle.png | CAM_A_FAMILY_WIDE | 전체 | 첫 선택 직전, 모두 긴장한 상담 초반 분위기 |
| ft001_cg_t02_choice_idle.png | CAM_A_FAMILY_WIDE | 전체 | 아침 순환을 그리기 전, 어머니와 아이가 조심스러움 |
| ft001_cg_t03_choice_idle.png | CAM_A_FAMILY_WIDE | 전체 | 외조모 발화 이후 긴장, 어머니가 작아진 상태 |
| ft001_cg_t04_choice_idle.png | CAM_A_FAMILY_WIDE | 전체 | 핵심 순환질문 전, 모두 피곤하지만 집중함 |
| ft001_cg_t05_choice_idle.png | CAM_A_FAMILY_WIDE | 전체 | 마무리 선택 전, 긴장은 남았지만 약간 완화됨 |

## 생성 절차

### 1단계: reference bible 먼저 만들기

최종 67장을 바로 만들지 말고 먼저 reference bible을 만든다.

필수 reference:

1. `FT001_ref_family_wide_locked.png`
2. `FT001_ref_mother_medium_locked.png`
3. `FT001_ref_child_medium_locked.png`
4. `FT001_ref_grandmother_medium_locked.png`
5. `FT001_ref_teacher_medium_locked.png`
6. `FT001_ref_hyesung_supervisor_reverse_locked.png`

이 6장을 승인 기준으로 삼는다.

### 2단계: 각 컷은 reference를 기준으로 변주

각 최종 CG는 위 reference 중 하나를 기반으로 만든다.

중요:

- 카메라 위치를 다시 발명하지 말 것
- 인물 좌석을 바꾸지 말 것
- 얼굴을 새 배우처럼 바꾸지 말 것
- 같은 reference에서 표정/손/시선/몸 기울기만 변주할 것

### 3단계: 중복 검사

완료 후 다음을 반드시 검사한다.

- 파일 수: 67장
- 해상도: 전부 1600x900
- 완전 중복: 0개
- 명백한 재사용 컷: 0개
- 파일명 누락: 0개
- extra 파일: 0개
- contact sheet 생성

중복 판단 기준:

- 같은 이미지 복사: 실패
- 색감만 조금 다른 이미지: 실패
- 표정/손/시선 차이가 육안으로 2초 안에 안 보임: 실패
- 같은 대화 턴에서 연속 2장이 너무 비슷함: 실패

## 납품 위치

새 이미지는 기존 폴더를 덮어쓰지 말고 아래 새 폴더에 저장한다.

`Assets/Resources/VN/EventCG/FT001_LineByLineLocked_Regen_20260611`

검수 후 통과하면 그때 런타임 적용 폴더로 교체한다.

생성 manifest와 contact sheet는 아래에 저장한다.

`Docs/GeneratedSources/FT001_LineByLineLocked_Regen_20260611`

필수 산출물:

- 최종 PNG 67장
- reference bible 6장
- 전체 contact sheet 1장
- 카메라 슬롯별 contact sheet 6장
- `ft001_regen_manifest.json`
- 중복 검사 결과
- 문제 컷 목록

## 최종 합격 기준

아래 조건을 전부 만족해야 합격이다.

- 대사/반응/인트로/분기/선택지 idle CG 67장이 모두 존재한다.
- 완전 중복 이미지가 없다.
- 안전 필터 때문에 기존 이미지를 복사한 fallback이 없다.
- 가족 네 명의 좌석 순서가 모든 가족 컷에서 유지된다.
- 김혜성은 모든 supervisor 컷에서 여성 슈퍼바이저로 보인다.
- 서건창과 김혜성이 혼동되지 않는다.
- 구도는 6개 카메라 슬롯 안에서만 반복된다.
- 컷마다 표정, 손, 시선, 몸 기울기 중 최소 하나가 뚜렷하게 다르다.
- 게임으로 넘겨 보았을 때 카메라가 흔들리는 느낌이 없다.
- 이미지끼리 색감, 조명, 얼굴, 의상, 상담실 구조가 일관된다.
