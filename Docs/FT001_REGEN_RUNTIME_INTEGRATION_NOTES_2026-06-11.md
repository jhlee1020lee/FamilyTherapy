# FT001 Regen 이미지 런타임 적용 노트

## 왜 이 문서를 만들었나

FT001 이미지 문제는 단순히 새 이미지를 생성하는 문제만이 아니다.

실제 Unity 코드에서는 FT001 CG가 여러 경로로 나뉘어 로드되고 있었다.

- 기존 line-by-line locked 폴더
- commercial branching 폴더
- convention fallback 폴더

이 상태에서는 새 이미지를 만들어도 코드가 그 이미지를 보지 않으면 게임 화면은 바뀌지 않는다. 따라서 새 67장 세트가 들어왔을 때 우선 적용되도록 런타임 경로 우선순위를 정리했다.

## 현재 코드 적용 방식

파일:

`Assets/Scripts/FamilyTherapyPracticumGame.cs`

함수:

`Ft001CgPath(string slug)`

현재 우선순위:

1. `VN/EventCG/FT001_LineByLineLocked_Regen_20260611/ft001_cg_{slug}`
2. `VN/EventCG/FT001_LineByLineLocked/ft001_cg_{slug}`
3. `VN/EventCG/FT001_CommercialBranching/{mapped_name}`
4. `VN/EventCG/FT001/ft001_cg_{slug}`

즉, 새 이미지 생성 창이 아래 폴더에 67장 PNG를 넣으면 그 이미지가 최우선으로 사용된다.

`Assets/Resources/VN/EventCG/FT001_LineByLineLocked_Regen_20260611`

## 왜 67장인가

처음에는 기존 산출물 기준으로 50장이라고 보았지만, 실제 코드 기준으로는 67장이 맞다.

구성:

- `Ft001Line(...)`, `Ft001Choice(...)`가 직접 호출하는 대사/반응/인트로/분기 slug: 62개
- 선택지 idle 화면: 5개
- 총계: 67개

50장만 만들면 다음 항목이 빠진다.

- 인트로 5장
- 선택 결과 이후 다음 턴으로 들어가는 branch line 12장
- 선택지 idle 5장

이 경우 게임은 일부 화면에서 기존 commercial branching 이미지로 fallback한다. 그러면 “각 대사마다 다른 사진”이라는 요구사항이 깨진다.

## 현재 자원 기준 검증

2026-06-11 확인 기준:

- 현재 코드가 직접 호출하는 대사/반응/인트로/분기 slug 62개는 모두 어떤 경로로든 resolve된다.
- 현재 `FT001_LineByLineLocked`에서 resolve되는 항목: 45개
- 현재 `FT001_CommercialBranching` fallback으로 resolve되는 항목: 17개
- 새 regen 폴더가 아직 없으므로 regen 우선순위는 대기 상태다.

## 새 이미지가 들어오면 해야 할 일

1. 이미지 생성 창이 아래 폴더에 PNG 67장을 저장한다.

   `Assets/Resources/VN/EventCG/FT001_LineByLineLocked_Regen_20260611`

2. QA 검수 스크립트를 실행한다.

   예상 스크립트:

   `Tools/ValidateFt001RegenImages.py`

3. 검수 기준을 통과해야 한다.

   - PNG 67장
   - 파일명 누락 0
   - extra 파일 0
   - 1600x900 67장
   - 완전 중복 0
   - 육안상 재사용 컷 0
   - contact sheet 생성

4. Unity를 다시 빌드한다.

5. `FamilyTherapyPracticum.exe`를 1600x900으로 실행해 visual audit 캡처를 새로 뜬다.

6. Presentation 패키지의 런타임 캡처를 새 이미지 기준으로 교체한다.

## 주의

새 regen 폴더는 검수 전까지 기존 active 폴더를 덮어쓰지 않는다.

이유:

- 새 생성 이미지가 일부 누락될 수 있다.
- 구도나 인물 일관성이 다시 흔들릴 수 있다.
- 안전 필터 때문에 fallback 복사가 섞일 수 있다.
- 검수 전 덮어쓰면 현재 실행 가능한 상태를 잃을 수 있다.

## 최종 목표

최종적으로는 FT001의 모든 대사, 반응, 인트로, 분기, 선택지 idle 화면이 67장짜리 새 locked CG 세트를 우선 사용해야 한다.

그 상태가 되어야 사용자가 지적한 문제를 해결했다고 볼 수 있다.

- 중복 컷 제거
- 장면별 명확한 차이
- 인물/의상/상담실 일관성
- 고정 카메라 슬롯으로 안정적인 게임 리듬
