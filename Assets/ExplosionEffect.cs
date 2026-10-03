using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    [Header("시각 연출 설정")]
    public GameObject effectPrefab;

    [Header("사운드 연출 설정")]
    public AudioClip soundVolume; // 충돌 효과음 오디오 클립
    [Range(0f, 1f)]
    public float volume = 1.0f;   // 소리 크기 (기본값 1.0)

    private AudioSource audioSource;

    void Awake()
    {
        // 오브젝트에 AudioSource가 없으면 자동으로 추가
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // 2D 사운드 설정 및 시간 정지 영향 받지 않도록 고정
        audioSource.spatialBlend = 0f; // 100% 2D 사운드로 설정
        audioSource.playOnAwake = false;
        audioSource.ignoreListenerPause = true; // 게임 일시정지 시에도 소리 출력
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Obstacle>() != null)
        {
            // 1. 파티클 이펙트 생성
            if (effectPrefab != null)
            {
                Instantiate(effectPrefab, transform.position, Quaternion.identity);
            }

            // 2. 효과음(SFX) 확실한 2D 재생
            if (soundVolume != null && audioSource != null)
            {
                audioSource.PlayOneShot(soundVolume, volume);
            }

            // 3. 게임 오버 및 시간 정지 처리
            GameManager gm = FindFirstObjectByType<GameManager>();
            if (gm != null) gm.GameOver();

            ObstacleSpawner spawner = FindFirstObjectByType<ObstacleSpawner>();
            if (spawner != null) spawner.StopSpawning();

            Time.timeScale = 0f;
        }
    }
}