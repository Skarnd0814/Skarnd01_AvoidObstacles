using UnityEngine;
using System.Collections;

public class ExplosionEffect : MonoBehaviour
{
    [Header("시각 연출 설정")]
    public GameObject effectPrefab; // 파티클 프리팹
    public float shakeMagnitude = 0.05f; // 진동 강도

    private bool hasExploded = false;

    // A. 충돌 감지 함수 (메인 로직)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 장애물과 부딪혔고, 아직 연출이 실행되지 않았다면
        if (!hasExploded && collision.gameObject.GetComponent<Obstacle>() != null)
        {
            hasExploded = true;

            // 1. 파티클 생성
            if (effectPrefab != null)
            {
                Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, 0f);
                Instantiate(effectPrefab, spawnPos, Quaternion.identity);
            }

            // 2. [cite: 핵심] 카메라 흔들림 코루틴 호출 (이제 정상적으로 찾을 수 있습니다.)
            StartCoroutine(ShakeCamera());
        }
    } // [cite: <- OnCollisionEnter2D 함수는 여기서 확실히 닫혀야 합니다.]

    // B. [cite: 핵심] 카메라 흔들림 코루틴 함수 정의 (함수 바깥, 클래스 내부에 위치)
    // 이전 답변의 for 루프 방식을 적용하여 진동이 멈추도록 했습니다.
    IEnumerator ShakeCamera()
    {
        Vector3 originalPos = Camera.main.transform.position;

        // 1. 흔들림 프레임 수(loops)를 정합니다. (예: 10프레임)
        int shakeLoops = 100;

        // 2. 정해진 프레임 수만큼만 확실하게 흔듭니다.
        for (int i = 0; i < shakeLoops; i++)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            Camera.main.transform.position = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            // 시간 정지 상태에서도 프레임을 그리기 위해 unscaleddeltaTime 대신 null 사용
            yield return null;
        }

        // 3. 루프 종료 후 반드시 카메라를 원래 위치로 완벽 복원
        Camera.main.transform.position = originalPos;
    }
}