using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;      // 점수 표시 UI
    public GameObject gameOverText;        // GAME OVER 텍스트 오브젝트

    private float survivedTime = 0f;
    private float currentScore = 0f;
    private bool isGameOver = false;

    [Header("점수 설정")]
    public float baseScorePerSecond = 10f; // 기본 초당 점수 (초반: 1초에 10점)
    public float scoreAccelRate = 0.5f;    // 시간에 따른 점수 가속도 (높을수록 빠르게 증가)

    void Start()
    {
        if (gameOverText != null)
        {
            gameOverText.SetActive(false);
        }
    }

    void Update()
    {
        if (isGameOver) return;

        survivedTime += Time.deltaTime;

        // [핵심] 기본 점수 + (시간 경과에 따른 가속 보너스 점수)
        // 시간이 지날수록 초당 더해지는 점수 폭이 점점 커집니다.
        float currentScoreMultiplier = baseScorePerSecond + (survivedTime * scoreAccelRate);
        currentScore += currentScoreMultiplier * Time.deltaTime;

        if (scoreText != null)
        {
            // 정수로 반올림하여 UI에 표시
            scoreText.text = Mathf.FloorToInt(currentScore).ToString();
        }
    }

    public void GameOver()
    {
        isGameOver = true;

        if (gameOverText != null)
        {
            gameOverText.SetActive(true);
        }
    }
}