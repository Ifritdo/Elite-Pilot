using UnityEngine;
using UnityEngine.UI;
using TMPro; // A�adimos el namespace para trabajar con TextMeshPro.

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // Singleton para acceder f�cilmente al ScoreManager desde otros scripts.

    public TextMeshProUGUI scoreText; // Cambiamos Text por TextMeshProUGUI para trabajar con TextMeshPro.
    public TextMeshProUGUI finalScoreText; // Referencia directa para el texto final del puntaje.
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

    // M�todo para a�adir puntos.
    public static void AddPoints(int points)
    {
        score += points;
        instance.UpdateScoreUI();
    }

    // Actualizar la UI.
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
