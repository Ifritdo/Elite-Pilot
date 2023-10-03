using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject victoryCanvas;
    public GameObject gameOverCanvas;

    public static GameOverManager instance;

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

    public void PlayerLost()
    {
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0;
        ScoreManager.instance.DisplayFinalScore();
    }

    public void PlayerWon()
    {
        victoryCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
