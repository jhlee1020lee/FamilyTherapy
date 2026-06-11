# Eliza UI Style Bible for Family Therapy Practicum

- 작성일: 2026-06-09
- 기준작: `Eliza` by Zachtronics
- 목적: 임시 네모 박스 UI를 버리고, 상담/기록/윤리 중심 비주얼노벨 UI 아트의 기준을 고정한다.

## 1. 핵심 방향

우리 게임의 UI는 `Eliza`를 강하게 참고한다.

카피할 것:

- 차분한 상담 프로그램 같은 화면 밀도.
- 인물 대화와 상담 스크립트를 가장 크게 두는 구성.
- 얇은 라인, 제한된 색, 넓은 여백.
- 화면을 꽉 채우지 않는 절제된 HUD.
- 기록/윤리/상담 프로토콜이 보이는 차가운 전문성.

카피하지 않을 것:

- 원본 로고, 캐릭터, 스크린샷, 아이콘, UI 이미지를 그대로 복제하지 않는다.
- 원본 색상값이나 패널 이미지를 픽셀 단위로 베끼지 않는다.
- 한국 가족치료 상담센터와 맞지 않는 미국식/기술회사식 텍스트는 쓰지 않는다.

## 2. 시각 톤

키워드:

- clinical
- calm
- restrained
- counseling interface
- ethical tension
- Korean family therapy center
- quiet visual novel

색:

- 배경: 어두운 회청색, 검정에 가까운 남색, 저채도 회색.
- 메인 패널: 따뜻한 흰색이 아니라 약간 차가운 off-white.
- 텍스트: 거의 검정 또는 거의 흰색.
- 포인트 컬러: 청록, 둔한 주황, 낮은 채도의 녹색 중 1개만 화면별로 사용.
- 금지: 두꺼운 베이지 종이 카드, 과한 나무 질감, 알록달록한 버튼, 그라데이션 장식.

형태:

- 1px-2px 선.
- 낮은 반투명 패널.
- 둥근 모서리는 4px-8px 수준.
- 버튼은 두꺼운 카드가 아니라 얇은 행 또는 선택 리스트.
- 패널 안 패널을 겹겹이 넣지 않는다.

## 3. 화면 문법

### 타이틀

- 배경은 상담실 또는 어두운 상담센터 로비.
- 제목은 좌측 또는 중앙 좌측.
- 메뉴는 작은 세로 리스트.
- 진행률, 통계, 여러 박스를 첫 화면에 노출하지 않는다.

### 대화 화면

- 캐릭터는 중앙/우측에 크게 둔다.
- 하단 대화창은 화면 하단 22%-28%.
- 화자 이름은 작은 name label.
- 상담자 선택 전까지 수치 HUD를 크게 보이지 않는다.
- 슈퍼바이저 코멘트는 작은 side note 또는 접히는 panel.

### 선택지 화면

- 선택지는 `정답 카드`가 아니라 `상담자 발화 리스트`처럼 보인다.
- 한 화면 선택지는 3-4개.
- 각 선택지는 얇은 행, 번호, 짧은 개입 태그를 가진다.
- 품질/점수는 선택 전에는 숨긴다.

### 사례 파일

- 의료 차트/상담 intake form처럼 보이되 앱 대시보드처럼 보이지 않게 한다.
- 좌측은 접수 정보, 우측은 슈퍼바이저의 짧은 briefing.
- 가족 관계도는 복잡한 그래프보다 간단한 labeled blocks.

### 슈퍼비전 리포트

- 결과표가 아니라 수련 기록 review screen.
- 총점보다 코멘트와 선택 경로가 먼저 보인다.
- 그래프는 작고 절제되게 둔다.

## 4. 이미지 생성 공통 프롬프트 규칙

모든 UI 목업 프롬프트에 포함한다:

```text
Use case: ui-mockup
Asset type: 1920x1080 visual novel UI screen mockup
Reference direction: strongly inspired by the restrained clinical interface language of Eliza by Zachtronics, but no copied assets, no original logos, no identical UI, no copyrighted characters.
Subject: Korean family therapy counseling simulation UI.
Style/medium: polished commercial visual novel UI mockup, clean 2D interface over a subdued counseling room background.
Composition: large readable panels, thin lines, restrained HUD, generous spacing, no crowded dashboard.
Text: use minimal placeholder Korean-like UI labels only, avoid long readable paragraphs.
Constraints: no pixel art, no thick decorative paper cards, no chunky game buttons, no clutter, no English title text, no watermark.
```

## 5. Unity 적용 기준

- 목업 승인 전에는 UI 에셋을 코드에 다시 얹지 않는다.
- 승인된 목업에서 다음 부품만 추출한다:
  - dialogue box
  - speaker nameplate
  - choice row
  - top HUD
  - side supervisor note
  - case file panel
  - result report panel
- Unity 텍스트는 이미지에 박지 않고 런타임 Text로 표시한다.
- 1024x768, 1280x720, 1600x900에서 `textOverflowCount=0`, `offscreenRectCount=0`이어야 한다.

