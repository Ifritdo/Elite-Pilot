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
        // Anular la suscripción al evento cuando el objeto se desactive
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
        // Simplemente carga la escena. El reseteo del puntaje se manejará en el evento.
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
}

