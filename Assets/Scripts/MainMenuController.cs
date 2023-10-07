using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Esta funci�n se encargar� de iniciar el juego
    public void StartGame()
    {
        // Asume que tu escena de juego se llama "GameScene". Cambia el nombre si es necesario.
        SceneManager.LoadScene("Game");
    }

    // Esta funci�n se encargar� de cerrar el juego
    public void ExitGame()
    {
        // Cierra la aplicaci�n
        Application.Quit();

        // Esta l�nea es solo para que funcione el bot�n de salir en el Editor de Unity.
        // Puedes comentarla o eliminarla cuando vayas a compilar tu juego.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // Funci�n para regresar al men� principal desde cualquier escena
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
