using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("이동 및 대시 설정")]
    public float moveSpeed = 5f;
    public float dashSpeed = 15f;
    public float dashDuration = 0.15f;
    public float dashCooldown = 3f;

    [Header("UI 연출")]
    public TextMeshProUGUI dashUIText;

    private Rigidbody2D rb;
    private float moveInput;
    private bool isGameOver = false;

    private bool isDashing = false;
    private float dashTimeLeft;
    private float lastDashTime = -999f;
    private float dashDirection = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashDirection = 1f;
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
        if (moveInput != 0)
        {
            dashDirection = Mathf.Sign(moveInput);
        }

        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastDashTime + dashCooldown && !isDashing)
        {
            StartDash();
        }

        UpdateDashUI();
    }

    void FixedUpdate()
    {
        if (isGameOver) return;

        if (isDashing)
        {
            // Y축 기존 속도(통통 튀어오르는 물리력)를 그대로 유지합니다.
            rb.linearVelocity = new Vector2(dashDirection * dashSpeed, rb.linearVelocity.y);
            dashTimeLeft -= Time.fixedDeltaTime;

            if (dashTimeLeft <= 0)
            {
                EndDash();
            }
        }
        else
        {
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        }
    }

    void StartDash()
    {
        isDashing = true;
        dashTimeLeft = dashDuration;
        lastDashTime = Time.time;
    }

    void EndDash()
    {
        isDashing = false;
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    void UpdateDashUI()
    {
        if (dashUIText == null) return;

        if (Time.time < lastDashTime + dashCooldown)
        {
            dashUIText.color = new Color(0.5f, 0.5f, 0.5f, 1f);
        }
        else
        {
            dashUIText.color = Color.white;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isGameOver && collision.gameObject.GetComponent<Obstacle>() != null)
        {
            isGameOver = true;

            GameManager gm = FindFirstObjectByType<GameManager>();
            if (gm != null) gm.GameOver();

            ObstacleSpawner spawner = FindFirstObjectByType<ObstacleSpawner>();
            if (spawner != null) spawner.StopSpawning();

            Time.timeScale = 0f;
        }
    }
}