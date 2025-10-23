using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Nombre de la escena
    [SerializeField] private string sceneToLoad = "ControlPlayer";

    // Botón para iniciar el juego
    public void OnStartButtonPressed()
    {
        // Cargar la escena
        SceneManager.LoadScene(sceneToLoad);
    }
}