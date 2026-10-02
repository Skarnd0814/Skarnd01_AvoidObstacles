using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI 연결")]
    public TextMeshProUGUI scoreText;      // 현재 점수 UI
    public TextMeshProUGUI highScoreText;  // 최고 점수 UI (선택 사항)
    public GameObject gameOverText;        // GAME OVER 텍스트/패널

    private float survivedTime = 0f;
    private float currentScore = 0f;
    private int highScore = 0;
    private bool isGameOver = false;

    [Header("점수 설정")]
    public float baseScorePerSecond = 10f; // 기본 초당 점수
    public float scoreAccelRate = 0.5f;    // 시간에 따른 점수 가속도

    void Start()
    {
        // 저장된 최고 점수 불러오기 (없으면 기본값 0)
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (gameOverText != null)
        {
            gameOverText.SetActive(false);
        }

        UpdateScoreUI();
    }

    void Update()
    {
        if (isGameOver) return;

        survivedTime += Time.deltaTime;

        // 시간에 따른 가속 점수 계산
        float currentScoreMultiplier = baseScorePerSecond + (survivedTime * scoreAccelRate);
        currentScore += currentScoreMultiplier * Time.deltaTime;

        int intScore = Mathf.FloorToInt(currentScore);

        // 현재 점수가 최고 점수를 넘어서면 실시간으로 갱신
        if (intScore > highScore)
        {
            highScore = intScore;
            PlayerPrefs.SetInt("HighScore", highScore); // 저장
        }

        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = Mathf.FloorToInt(currentScore).ToString();
        }

        if (highScoreText != null)
        {
            highScoreText.text = "BEST: " + highScore;
        }
    }

    public void GameOver()
    {
        isGameOver = true;

        // 게임 오버 시 최고 점수를 최종 저장
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();

        if (gameOverText != null)
        {
            gameOverText.SetActive(true);
        }
    }
}