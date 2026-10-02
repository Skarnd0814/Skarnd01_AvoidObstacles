using UnityEngine;
using UnityEngine.SceneManagement; // 씬 재시작을 위한 이름공간

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
        // 게임 오버 상태일 때 R 키를 누르면 현재 씬을 다시 로드(재시작)
        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Time.timeScale = 1f; // 일시정지 해제
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

    // 장애물과 부딪혔을 때 호출되는 함수
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 부딪힌 대상이 장애물(Obstacle)이라면
        if (collision.gameObject.GetComponent<Obstacle>() != null)
        {
            isGameOver = true;
            Time.timeScale = 0f; // 게임 시간을 멈춤 (게임 오버)
            Debug.Log("게임 오버! R 키를 눌러 다시 시작하세요.");
        }
    }
}