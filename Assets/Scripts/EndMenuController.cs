using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndMenuController : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText; // Referencia al texto donde mostrarás el puntaje final.

    private void Start()
    {
        DisplayFinalScore(); // Mostramos el puntaje final cuando se carga la escena.
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        ScoreManager.instance.ResetScore(); // Resetea el puntaje al reiniciar el juego.
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        // Cierra la aplicación
        Application.Quit();

        // Esta línea es solo para que funcione el botón de salir en el Editor de Unity.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void DisplayFinalScore()
    {
        int finalScore = ScoreManager.score; // Obtiene el puntaje final del ScoreManager.
        finalScoreText.text = "Game Over\nFinal Score: " + finalScore;
    }
}
