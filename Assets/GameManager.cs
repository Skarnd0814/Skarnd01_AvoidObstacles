using UnityEngine;
using TMPro; // TextMeshPro 관련 기능 사용

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // 화면에 보여줄 UI 텍스트
    private float survivedTime = 0f;
    private bool isGameOver = false;

    void Update()
    {
        if (isGameOver) return;

        // 게임이 진행 중일 때 버틴 시간을 계속 더함
        survivedTime += Time.deltaTime;

        // UI 텍스트 업데이트 (소수점 첫째 자리까지 표시)
        if (scoreText != null)
        {
            scoreText.text = survivedTime.ToString("F1") + "s";
        }
    }

    // 게임 오버 호출용 함수
    public void GameOver()
    {
        isGameOver = true;
    }
}