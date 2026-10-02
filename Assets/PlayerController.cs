using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private float moveInput;
    private bool isGameOver = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            return;
        }

        moveInput = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        if (isGameOver) return;

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 장애물과 부딪혔는지 확인
        if (!isGameOver && collision.gameObject.GetComponent<Obstacle>() != null)
        {
            isGameOver = true;

            // GameManager 게임 오버 호출
            GameManager gm = FindFirstObjectByType<GameManager>();
            if (gm != null)
            {
                gm.GameOver();
            }

            // 장애물 스포너 생성 정지 호출
            ObstacleSpawner spawner = FindFirstObjectByType<ObstacleSpawner>();
            if (spawner != null)
            {
                spawner.StopSpawning();
            }

            // 시간 정지
            Time.timeScale = 0f;

            // Debug.Log("게임 오버! 파티클 테스트 중."); <-- 이 줄을 지워주시면 됩니다!
        }
    }
}