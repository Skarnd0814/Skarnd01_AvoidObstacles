using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab; // 생성할 장애물 프리팹
    public float xRange = 6.5f;        // 스폰 좌우 범위

    [Header("스폰 간격 난이도 설정")]
    public float initialSpawnInterval = 1.0f; // 시작 스폰 간격 (1초)
    public float minSpawnInterval = 0.2f;     // 최소 스폰 간격 (0.2초)
    public float spawnIntervalDecreaseRate = 0.02f; // 초당 감소할 간격 시간

    private float currentSpawnInterval;
    private float timer = 0f;
    private float spawnTimer = 0f;
    private bool isGameOver = false;

    void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
    }

    void Update()
    {
        if (isGameOver) return;

        // 경과 시간 누적
        timer += Time.deltaTime;
        spawnTimer += Time.deltaTime;

        // 시간에 따라 스폰 간격 감소 (난이도 상승)
        currentSpawnInterval = Mathf.Max(minSpawnInterval, initialSpawnInterval - (timer * spawnIntervalDecreaseRate));

        // 설정된 스폰 간격마다 장애물 생성
        if (spawnTimer >= currentSpawnInterval)
        {
            SpawnObstacle();
            spawnTimer = 0f; // 타이머 리셋
        }
    }

    void SpawnObstacle()
    {
        float randomX = Random.Range(-xRange, xRange);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, 0f);

        GameObject newObstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);

        // 시간에 따라 낙하 속도도 살짝 증가 (Rigidbody2D 중력 스케일 조절)
        Rigidbody2D rb = newObstacle.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // 기본 중력(1.0)에서 시간이 지날수록 중력이 커져서 빠르게 떨어짐
            rb.gravityScale = 1.0f + (timer * 0.03f);
        }
    }

    // 게임 오버 시 스폰 정지용 함수
    public void StopSpawning()
    {
        isGameOver = true;
    }
}