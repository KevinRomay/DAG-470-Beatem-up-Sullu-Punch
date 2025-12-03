using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public static GameOverMenu Instance;

    public GameObject panelGameOver;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        panelGameOver.SetActive(false);
    }

    public void MostrarMenu()
    {
        panelGameOver.SetActive(true);
    }

    public void Reintentar()
    {
        Time.timeScale = 1f; // Reactivar el tiempo
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
