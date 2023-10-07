using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Esta función se encargará de iniciar el juego
    public void StartGame()
    {
        // Asume que tu escena de juego se llama "GameScene". Cambia el nombre si es necesario.
        SceneManager.LoadScene("Game");
    }

    // Esta función se encargará de cerrar el juego
    public void ExitGame()
    {
        // Cierra la aplicación
        Application.Quit();

        // Esta línea es solo para que funcione el botón de salir en el Editor de Unity.
        // Puedes comentarla o eliminarla cuando vayas a compilar tu juego.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // Función para regresar al menú principal desde cualquier escena
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
