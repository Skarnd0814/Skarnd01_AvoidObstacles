using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;      // 시간 표시 UI
    public GameObject gameOverText;        // GAME OVER 텍스트 오브젝트

    private float survivedTime = 0f;
    private bool isGameOver = false;

    void Start()
    {
        // 시작 시 GAME OVER 문구가 꺼져있는지 한 번 더 확인
        if (gameOverText != null)
        {
            gameOverText.SetActive(false);
        }
    }

    void Update()
    {
        if (isGameOver) return;

        survivedTime += Time.deltaTime;

        if (scoreText != null)
        {
            scoreText.text = survivedTime.ToString("F1") + "s";
        }
    }

    public void GameOver()
    {
        isGameOver = true;

        // 게임 오버 시 GAME OVER 텍스트 UI 켜기
        if (gameOverText != null)
        {
            gameOverText.SetActive(true);
        }
    }
}