using UnityEngine;
using System.Collections;

public class ExplosionEffect : MonoBehaviour
{
    [Header("시각 연출 설정")]
    public GameObject effectPrefab;
    public float shakeMagnitude = 0.05f;

    [Header("사운드 연출 설정")]
    public AudioClip explosionSFX; // <- 이 줄이 있어야 Inspector에 칸이 생깁니다!
    [Range(0f, 1f)] public float soundVolume = 0.8f;

    private bool hasExploded = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasExploded && collision.gameObject.GetComponent<Obstacle>() != null)
        {
            hasExploded = true;

            // 효과음 재생
            if (explosionSFX != null)
            {
                AudioSource.PlayClipAtPoint(explosionSFX, Camera.main.transform.position, soundVolume);
            }

            // 파티클 생성
            if (effectPrefab != null)
            {
                Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, 0f);
                Instantiate(effectPrefab, spawnPos, Quaternion.identity);
            }

            // 카메라 흔들림
            StartCoroutine(ShakeCamera());
        }
    }

    IEnumerator ShakeCamera()
    {
        Vector3 originalPos = Camera.main.transform.position;
        int shakeLoops = 10;

        for (int i = 0; i < shakeLoops; i++)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            Camera.main.transform.position = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
            yield return null;
        }

        Camera.main.transform.position = originalPos;
    }
}