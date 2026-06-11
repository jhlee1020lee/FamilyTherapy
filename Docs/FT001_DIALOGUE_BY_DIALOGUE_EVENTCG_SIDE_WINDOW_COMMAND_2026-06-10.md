# FT001 대사별 EventCG 제작 옆창 입력 명령

아래 블록을 이미지 생성 전용 창에 그대로 붙여넣는다.

```text
너는 Family Therapy Practicum의 FT001 이미지 생성 담당이다.

이번 작업은 FT001만 한다. FT002, FT003, 다른 사례는 절대 만들지 않는다.

목표는 "대사별 1장"이다.
장면 요약 CG, 대표 CG, 압축 CG, 캐릭터 스프라이트 조합, 배경+인물 레이어 조합은 이번 작업에서 쓰지 않는다.
대사 한 줄 또는 한 발화가 지나갈 때마다 그 발화에 맞는 완성형 EventCG 한 장이 뜨는 구조로 만든다.

먼저 아래 문서를 반드시 읽어라.

C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity\Docs\FT001_LINE_BY_LINE_CG_SIDE_WINDOW_COMMAND_2026-06-10.md
C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity\Docs\FT001_REALISTIC_DIALOGUE_EXPANSION_2026-06-10.md
C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity\Docs\FT001_IMAGE_PIPELINE_COORDINATION_BRIEF_2026-06-10.md

절대 오버라이드:

1. FT001만 제작한다.
2. 최종 산출물은 대사별 EventCG 총 67장이다.
3. 한 이미지가 여러 대사를 대표하면 실패다.
4. 기존 30장 압축본인 `FT001_CommercialBranching`은 참고용으로만 본다. 최종 산출물로 복붙하지 않는다.
5. 이미지 안에 게임 UI, 대사창, 말풍선, 자막, 글자, 워터마크를 넣지 않는다.
6. 모든 이미지는 처음부터 네이티브 1600x900 PNG로 만든다. 정사각형 생성 후 늘리거나, 비율이 다른 이미지를 억지로 맞추면 실패다.
7. 하단 25-30%는 실제 게임 대사창이 올라오므로 얼굴, 손, 핵심 감정 연기를 두지 않는다.
8. 모든 컷은 "앉아서 상담하는 장면"이어야 한다. 서 있는 전신 캐릭터 샷은 실패다.

최종 저장 폴더:

C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity\Assets\Resources\VN\EventCG\FT001_LineByLineLocked

소스/후보/검수 자료 보관 폴더:

C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity\Docs\GeneratedSources\FT001_LineByLineLocked_20260610

인물 고정:

- 박성빈: 여성, 어머니. 야간 근무와 자녀 등교 거부 사이에서 지쳐 있다.
- 이주형: 남자 초등학생 자녀. 초등학생으로 보여야 하며 청소년/여성처럼 보이면 실패다.
- 오선진: 여성, 외조모. 걱정이 많지만 말투가 단단하다.
- 서건창: 남성, 담임교사. 가족 구성원이 아니라 학교 측 인물이다.
- 김혜성: 여성, 가족체계 슈퍼바이저/치료자. 가족 맞은편에 앉아 있다. 서건창과 닮으면 실패다.

구도 고정:

가족 발화 컷:
- 상담자 김혜성 자리에서 가족/학교 쪽을 바라보는 Family Master Shot.
- 왼쪽 오선진, 중앙 왼쪽 박성빈, 중앙 오른쪽/앞 이주형, 오른쪽 서건창.
- 네 사람은 모든 가족 컷에서 같은 좌표, 같은 크기, 같은 의자, 같은 상담실 구조를 유지한다.
- 해당 대사의 화자만 표정, 시선, 손짓, 몸 방향으로 강조한다.
- 나머지 인물은 발화 내용을 듣는 미세 반응만 준다.

김혜성 발화 컷:
- 가족 쪽에서 김혜성을 바라보는 Hyesung Master Shot.
- 김혜성 단독 또는 김혜성 중심 구도.
- 가족 4명을 같은 줄에 억지로 넣지 않는다.
- 김혜성은 여성 슈퍼바이저로 명확해야 한다.

레퍼런스:

가족 4명과 상담실 기준:
C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity\Docs\GeneratedSources\FT001_ReferenceLocked_20260609\references

김혜성 여성 레퍼런스 기준:
C:\codex\snu_etl_downloader_portable\downloads\2026_Spring\child_and_family_trends_and_big_data_analysis\99_FamilyTherapyPracticumUnity\Docs\GeneratedSources\FT001_KH_FemaleRef_20260610

구버전 남성처럼 보이는 김혜성 레퍼런스는 쓰지 않는다.

스타일:

polished commercial Korean visual novel event CG, realistic painterly 2D, restrained emotional acting, warm counseling-room lighting, consistent faces and wardrobe, seated family therapy session, cinematic but readable composition, no UI, no text, no speech bubble, no watermark

금지:

standing character sprite, square portrait, stretched image, anime exaggeration, chibi, photorealistic uncanny face, random extra people, changing clothes, changing room layout, distorted hands, cropped-out family member, on-image text, subtitle, UI frame, speech bubble, watermark

제작 방식:

1. `FT001_LINE_BY_LINE_CG_SIDE_WINDOW_COMMAND_2026-06-10.md`의 Shotlist 1-67을 최종 파일명 기준으로 사용한다.
2. Shotlist의 파일명을 정확히 지킨다. 임의 파일명 금지.
3. 10장 단위로 만든다.
4. 먼저 1-10번만 만든 뒤 contact sheet를 생성하고 검수한다.
5. 통과하면 11-20, 21-30 순서로 진행한다.
6. 실패 이미지는 최종 폴더에 넣지 말고 후보/폐기 폴더에 둔 뒤 재생성한다.

검수 기준:

- 정확히 1600x900 PNG인가
- 비율이 늘어나거나 찌그러지지 않았는가
- 가족 컷에서 네 명의 좌석/크기/카메라가 유지되는가
- 김혜성 컷에서 김혜성이 여성 슈퍼바이저로 보이는가
- 이주형은 남자 초등학생으로 보이는가
- 서건창은 남자 담임교사로 보이는가
- 이미지 안에 글자/UI/말풍선/워터마크가 없는가
- 하단 대사창 영역이 비어 있는가
- 발화자 감정이 해당 대사와 맞는가

Batch 01은 반드시 아래 10장부터 시작한다.

1. `ft001_cg_intro_01_mother_neutral.png`
   - 박성빈이 야간 근무와 아침 등교 압박을 말하는 첫 장면.
   - 가족 전체가 앉아 있고 박성빈이 중심 화자.

2. `ft001_cg_intro_02_child_neutral.png`
   - 이주형이 학교와 엄마가 나가는 순간의 불안을 말하는 장면.
   - 남자 초등학생으로 보여야 하며, 작게 움츠렸지만 얼굴은 보여야 한다.

3. `ft001_cg_intro_03_grandmother_neutral.png`
   - 오선진이 걱정과 잔소리 사이의 어려움을 말하는 장면.
   - 악역처럼 보이지 말고 걱정이 단단하게 표현되어야 한다.

4. `ft001_cg_intro_04_teacher_neutral.png`
   - 서건창이 출결 절차와 아이 걱정을 설명하는 장면.
   - 학교 측 담임교사로 보여야 한다.

5. `ft001_cg_intro_05_supervisor_explaining.png`
   - 김혜성이 첫 회기 방향을 플레이어에게 설명하는 장면.
   - 여성 슈퍼바이저 단독/중심 구도.

6. `ft001_cg_t01_l01_mother_neutral.png`
   - 박성빈이 평가받을까 봐 긴장한 채 상담을 시작하는 장면.

7. `ft001_cg_t01_l02_child_anxious.png`
   - 이주형이 엄마의 급함 앞에서 말할 자리를 잃는 장면.

8. `ft001_cg_t01_l03_mother_worried.png`
   - 박성빈이 소리를 지르지 않으면 아무것도 안 바뀔까 봐 걱정하는 장면.

9. `ft001_cg_t01_l04_child_quiet.png`
   - 이주형이 가만히 닫히는 장면. 반항이 아니라 불안과 멈춤으로 보여야 한다.

10. `ft001_cg_t01_l05_teacher_concerned.png`
    - 서건창이 출결 걱정을 말하되 가족을 몰아붙이지 않으려는 장면.

Batch 01 완료 후 보고 형식:

FT001 dialogue-by-dialogue EventCG production report
Batch: 01/07
Generated: 10
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
- Any wrong gender/age read: yes/no

Batch 01 contact sheet를 보여주고 통과 판정을 받은 뒤 다음 10장으로 넘어가라.
```

## 이 창에서 할 일

옆창이 `FT001_LineByLineLocked`에 통과 이미지를 넣으면, 이 창에서는 이후 Unity FT001 CG 매핑을 대사별 파일명으로 연결한다.
