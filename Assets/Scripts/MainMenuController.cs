using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Esta función se encargará de iniciar el juego
    public void StartGame()
    {
        // Asume que tu escena de juego se llama "GameScene". Cambia el nombre si es necesario.
        SceneManager.LoadScene("GameScene");
    }

    // Esta función se encargará de cerrar el juego
    public void ExitGame()
    {
        // Cierra la aplicación
        Application.Quit();

        // Esta línea es solo para que funcione el botón de salir en el Editor de Unity.
        // Puedes comentarla o eliminarla cuando vayas a compilar tu juego.
        UnityEditor.EditorApplication.isPlaying = false;
    }

    // Si decides tener un botón que regrese al menú desde la pantalla de GameOver, puedes usar esta función
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
