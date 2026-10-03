# Skarnd01_AvoidObstacles
첫 번째 유니티 2D 게임 개발

# 🎮 Avoid Obstacles (2D Arcade Game)

> **Unity 6**로 개발한 아케이드 스타일의 2D 장애물 피하기 게임입니다.  
> 세련된 네온 비주얼, 반응형 오디오, 파티클 연출, 그리고 **대시(Dash) 스킬**을 통한 긴장감 넘치는 조작감을 제공합니다.

---

## 🛠️ 개발 환경 및 사용 기술 (Tech Stack)

* **Engine:** Unity 6 (6000.0.32f1)
* **Language:** C#
* **Render Pipeline:** 2D Universal Render Pipeline (URP)
* **UI System:** TextMeshPro
* **Assets:** AI-Generated Retro Neon Background & Custom Arcade SFX/BGM

---

## ✨ 핵심 기능 (Features)

* 🏃 **플레이어 컨트롤 & 대시 스킬**
  * `A / D` 또는 방향키로 수평 이동
  * `Spacebar` 입력 시 바라보는 방향으로 순간 대시 (3초 쿨타임)
  * TextMeshPro 기반의 UI 쿨타임 색상 전환 (쿨타임 중: 회색, 완료: 흰색)
* 💥 **시각 & 타격감 연출 (Juice & VFX)**
  * 장애물 충돌 시 프레임 독립적(`unscaledDeltaTime`) 카메라 흔들림(Shake) 효과
  * 파티클 시스템을 활용한 묵직한 폭발 이펙트 연출
* 🎵 **오디오 시스템 (SFX & BGM)**
  * 무한 루프 배경음악(BGM) 자동 재생
  * 충돌 시 `AudioSource.PlayClipAtPoint`를 활용해 일시정지 상태에서도 끊김 없는 SFX 출력
* 📈 **점수 및 게임 관리**
  * 버틴 시간에 따른 가속 점수(Score Acceleration) 연동
  * `PlayerPrefs` 기반의 최고 점수(Best Score) 영구 저장

---

## 📅 개발 커밋 기록 (Development History)

프로젝트 개발 과정에서 진행된 주요 기능별 커밋 기록입니다.

### 2026-10-03
* **`b5908ef`** - 플레이어 대시 효과음(SFX) 추가 및 오디오 설정 최적화
* **`2dab1fb`** - 플레이어 대시(Dash) 스킬 및 UI 쿨타임 시스템 구현
* **`9b96bb7`** - 게임 배경 이미지 추가 및 스프라이트 레이어 설정
* **`6c66529`** - BGM 및 충돌 효과음(SFX) 오디오 시스템 구현
* **`b8bef29`** - 충돌 파티클 연출 및 카메라 흔들림 효과 구현

### 2026-10-02
* **`ce309b9`** - 시간 경과에 따른 가속 점수(Score Acceleration) 로직 구현
* **`858c7f7`** - GameManager 연동 GAME OVER 텍스트 UI 연출 및 활성화 로직 구현
* **`92d1b59`** - TextMeshPro를 활용한 실시간 타이머 UI(ScoreText) 구현 및 GameManager 연동
