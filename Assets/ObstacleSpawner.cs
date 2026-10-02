using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab; // 생성할 장애물 프리팹
    public float spawnInterval = 1f;   // 생성 간격 (1초마다)
    public float xRange = 6.5f;        // 좌우 무작위 생성 범위

    void Start()
    {
        // spawnInterval 초마다 SpawnObstacle 함수를 반복 호출
        InvokeRepeating("SpawnObstacle", 0.5f, spawnInterval);
    }

    void SpawnObstacle()
    {
        // -xRange부터 xRange 사이의 랜덤한 X 위치 계산
        float randomX = Random.Range(-xRange, xRange);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, 0f);

        // 지정된 위치에 장애물 생성
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }
}