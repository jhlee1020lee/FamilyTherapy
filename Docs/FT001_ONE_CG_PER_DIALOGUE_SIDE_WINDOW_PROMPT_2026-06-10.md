# FT001 대사별 1CG 제작 옆창 입력 명령

아래 블록을 이미지 생성 전용 창에 그대로 붙여넣는다.

```text
너는 Family Therapy Practicum의 이미지 생성 담당이다.

이번 작업은 FT001만 한다. FT002 이후는 절대 만들지 않는다.
목표는 "대사별 1장"이다. 요약 CG, 장면별 대표 CG, 30장 압축 버전은 폐기한다.

반드시 아래 문서를 먼저 읽고 그대로 실행해라.

C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity\Docs\FT001_LINE_BY_LINE_CG_SIDE_WINDOW_COMMAND_2026-06-10.md

이번 지시의 핵심 오버라이드:

1. FT001만 제작한다.
2. 최종 산출물은 총 67장이다.
   - 인트로 대사 5장
   - Turn 1~5 본 대사 30장
   - 선택 반응 15장
   - 이전 선택 carryover 대사 12장
   - 선택지 대기 화면 5장
3. 한 이미지가 여러 대사를 대표하면 실패다.
4. `FT001_CommercialBranching`의 기존 30장 압축 CG는 참고용으로만 본다. 최종본으로 복붙하지 않는다.
5. 최종 저장 폴더는 아래 하나만 사용한다.

C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity\Assets\Resources\VN\EventCG\FT001_LineByLineLocked

6. 후보/소스/검수 자료는 아래에 보관한다.

C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity\Docs\GeneratedSources\FT001_LineByLineLocked_20260610

절대 규격:

- 모든 이미지는 네이티브 1600x900 PNG.
- 정사각형 생성 후 16:9로 늘리기 금지.
- 16:9가 아닌 결과물을 억지로 크롭/리사이즈해서 통과 처리 금지.
- 이미지 안에 텍스트, 자막, UI, 말풍선, 워터마크 금지.
- 하단 25~30%는 게임 대사창이 올라가므로 복잡한 얼굴/핵심 손동작을 배치하지 않는다.
- 모든 파일명은 문서의 Shotlist 파일명을 정확히 사용한다.

구도 규칙:

- 가족 쪽 대사/반응/선택대기:
  Family Master Shot으로 만든다.
  상담자 김혜성의 자리에서 가족/학교 쪽을 바라보는 구도다.
  왼쪽 오선진, 중앙 왼쪽 박성빈, 중앙 오른쪽/앞 이주형, 오른쪽 서건창이 모두 보여야 한다.
  네 사람은 모든 가족 컷에서 같은 좌표, 같은 크기, 같은 의자, 같은 방 구조를 유지한다.
  화자만 표정/시선/손짓으로 강조하고 나머지는 미세 반응만 바꾼다.

- 김혜성 대사/반응:
  Hyesung Master Shot으로 만든다.
  가족 쪽에서 김혜성을 바라보는 반대 시점이다.
  김혜성은 반드시 한국인 성인 여성 가족체계 슈퍼바이저다.
  김혜성은 가족 4명과 같은 줄에 앉지 않는다.
  김혜성은 서건창과 닮으면 실패다.

인물 고정:

- 박성빈: 여성, 어머니, 야간 근무와 자녀 등교 거부 사이에서 소진됨.
- 이주형: 남자 초등학생, 분리불안과 등교 거부, 청소년처럼 보이면 실패.
- 오선진: 여성, 외조모, 걱정이 많고 말투가 단단함.
- 서건창: 남성, 담임교사, 가족 구성원이 아니라 학교 측 인물.
- 김혜성: 여성, 가족체계 슈퍼바이저, 가족 맞은편에 앉은 치료자/수퍼바이저.

레퍼런스 정책:

- 가족 4명 구도는 아래 계열을 기준으로 잡는다.
  C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity\Docs\GeneratedSources\FT001_ReferenceLocked_20260609\references

- 김혜성은 반드시 아래 여성 레퍼런스를 기준으로 한다.
  C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity\Docs\GeneratedSources\FT001_KH_FemaleRef_20260610

- 구버전 남성처럼 보이는 김혜성 레퍼런스는 쓰지 않는다.

작업 순서:

1. `FT001_LINE_BY_LINE_CG_SIDE_WINDOW_COMMAND_2026-06-10.md`의 Shotlist 1~67을 확인한다.
2. 먼저 1~10번만 생성한다.
3. 1~10번 결과를 contact sheet로 묶고 다음 기준으로 검수한다.
   - 1600x900인가
   - 가족 컷에서 네 명의 좌석/크기/카메라가 흔들리지 않는가
   - 김혜성은 여성이고 독립된 반대 시점인가
   - 그림 안에 글자/UI/말풍선이 없는가
   - 하단 대사창 영역이 너무 복잡하지 않은가
4. 통과한 이미지만 최종 폴더에 넣는다.
5. 실패 이미지는 후보 폴더에 남기고 재생성한다.
6. 10장 단위로 67번까지 반복한다.

완료 보고는 아래 형식으로 한다.

FT001 one-CG-per-dialogue production report
Batch: 01/07
Generated:
Accepted:
Rejected:
Final folder:
C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity\Assets\Resources\VN\EventCG\FT001_LineByLineLocked
Contact sheet:
Notes:
- Family Master Shot locked: yes/no
- Hyesung female reference used: yes/no
- Any ratio/stretch issue: yes/no
- Any text/UI inside image: yes/no
```

## 이 창에서 이후 처리할 일

옆 창이 67장 통과본을 `FT001_LineByLineLocked`에 넣으면, 이 창에서 Unity 코드의 FT001 CG 매핑을 기존 `FT001_CommercialBranching` 압축 매핑에서 `FT001_LineByLineLocked` 대사별 파일 매핑으로 바꾼다.

