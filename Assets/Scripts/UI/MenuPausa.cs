using UnityEngine;
using UnityEngine.SceneManagement; // Para reiniciar o cambiar de escena

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausaUI; // Asigna tu Canvas aquí

    private bool estaPausado = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (estaPausado)
                ReanudarJuego();
            else
                PausarJuego();
        }
    }

    public void PausarJuego()
    {
        menuPausaUI.SetActive(true);
        Time.timeScale = 0f; // Pausa el juego
        estaPausado = true;
    }

    public void ReanudarJuego()
    {
        menuPausaUI.SetActive(false);
        Time.timeScale = 1f; // Reanuda el juego
        estaPausado = false;
    }

    public void ReiniciarNivel()
    {
        Time.timeScale = 1f; // Asegúrate de reanudar antes de recargar
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

