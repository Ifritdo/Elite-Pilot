using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    // Función para reiniciar el juego
    public void RestartGame()
    {
        Time.timeScale = 1; // Devolvemos el tiempo a su flujo normal.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Recargamos la escena actual.
    }

    // Función para cerrar el juego
    public void ExitGame()
    {
        Application.Quit(); // Cierra la aplicación. Nota: Esto no funciona en el editor, solo en el juego compilado.
    }
}
