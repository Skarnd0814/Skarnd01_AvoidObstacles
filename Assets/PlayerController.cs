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
        if (collision.gameObject.GetComponent<Obstacle>() != null)
        {
            isGameOver = true;
            Time.timeScale = 0f;

            // GameManager를 찾아 게임 오버 상태를 알림
            GameManager gm = FindFirstObjectByType<GameManager>();
            if (gm != null)
            {
                gm.GameOver();
            }

            Debug.Log("게임 오버! R 키를 눌러 다시 시작하세요.");
        }
    }
}