# Steam Visual Novel Benchmark Notes 2026-06-09

목적: 현재 빌드의 그래픽이 조잡하고 학습 대시보드처럼 보인다는 피드백을 반영하기 위해, Steam의 대화형/치료형/상담형 VN 게임에서 바로 적용 가능한 UI 방향을 정리한다.

확장 기준 문서: `Docs/LARGE_SCALE_STEAM_GAME_BENCHMARK_AND_REDESIGN_2026-06-09.md`

이 문서는 1차 빠른 적용 메모다. 대규모 벤치마크와 전면 리디자인 기준은 확장 기준 문서를 우선한다.

## 참고 타이틀

### Vampire Therapist

- Steam: https://store.steampowered.com/app/2481020/Vampire_Therapist/
- 상담/치료 개념을 내러티브와 캐릭터 대화 안에서 배우게 하는 구조를 참고한다.
- 치료 개념을 별도 대시보드로 밀어내기보다, 슈퍼바이저 노트와 대화 선택지 안에 녹이는 방향이 맞다.

### Coffee Talk

- Steam: https://store.steampowered.com/app/914800/Coffee_Talk/
- 인물의 고민을 듣는 대화 중심 시뮬레이터라는 점을 참고한다.
- 화면은 장소 분위기, 캐릭터, 대화창에 집중하고 상시 정보 패널은 최소화한다.

### VA-11 Hall-A: Cyberpunk Bartender Action

- Steam: https://store.steampowered.com/app/447530/VA11_HallA_Cyberpunk_Bartender_Action/
- 플레이어가 일을 수행하면서 손님의 이야기를 듣는 구조를 참고한다.
- 반복 업무 UI가 있더라도 핵심 감정선은 인물 표정과 대화 리듬으로 전달된다.

### Phoenix Wright: Ace Attorney Trilogy

- Steam: https://store.steampowered.com/app/787480/Phoenix_Wright_Ace_Attorney_Trilogy/
- 캐릭터 소개, 증언, 선택/추궁 흐름을 참고한다.
- 장면에 들어가기 전에 누가 누구인지 짧게 잡아주는 구성이 플레이어의 혼란을 줄인다.

## 현재 빌드에 반영할 원칙

- 스테이지 진입 직후 등장인물 자기소개를 먼저 보여준다.
- 상시 등장인물 목록 패널은 제거하거나 축소한다. 인물 정보는 자기소개와 대화창 nameplate에서 전달한다.
- 메인 화면과 회기 화면은 데이터/진행률보다 분위기, 인물, 대화 진입을 우선한다.
- 치료 개념은 별도 설명문보다 슈퍼바이저 노트와 선택지 피드백으로 전달한다.
- 선택지 화면은 “학습 문제”가 아니라 “상담자 개입 선택”처럼 보여야 한다.
- 캐릭터 성별/나이/역할은 절대 흔들리면 안 된다. 이주형은 남자 초등학생, 서건창은 남자 담임교사다.

## 이번 적용 결정

- `BeginVnCase` 이후 바로 회기 대사로 들어가지 않고 `등장인물 소개` 시퀀스를 먼저 재생한다.
- FT-001 소개 라인은 박성빈, 이주형, 오선진, 서건창, 김혜성 순서로 진행한다.
- 기존 스테이지의 좌측 등장인물 목록 패널은 제거한다.
- 이주형/서건창은 기존 여성처럼 보이는 스프라이트 대신 남성 임시 교정 스프라이트를 런타임 기본 컷으로 우선 사용한다.
- 이미지 생성 전담 창에는 두 캐릭터를 남성으로 재생성해야 한다는 지시를 명시한다.
