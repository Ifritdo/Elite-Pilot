using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private void OnEnable()
    {
        // Suscribirse al evento cuando el objeto se active
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Anular la suscripci�n al evento cuando el objeto se desactive
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Comprobar si la escena cargada es "Game"
        if (scene.name == "Game")
        {
            ScoreManager.instance.ResetScore();
        }
    }

    public void StartGame()
    {
        // Simplemente carga la escena. El reseteo del puntaje se manejar� en el evento.
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        // Cierra la aplicaci�n
        Application.Quit();

        // Esta l�nea es solo para que funcione el bot�n de salir en el Editor de Unity.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

