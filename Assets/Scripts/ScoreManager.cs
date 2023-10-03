using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;
    public static int score = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void AddPoints(int points)
    {
        score += points;
        instance.UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }

    public int GetScore()
    {
        return score;
    }

    public void DisplayFinalScore()
    {
        finalScoreText.text = "Game Over\nScore Final: " + score;
    }
}
