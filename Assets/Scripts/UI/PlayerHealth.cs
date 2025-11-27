using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vida")]
    public float vidaMax = 100f;
    public float vidaActual;

    [Header("UI")]
    public Image barraDeVida;
    public TextMeshProUGUI textoGameOver;

    private bool juegoTerminado = false;

    void Start()
    {
        vidaActual = vidaMax;

        // Ocultar texto al inicio
        if (textoGameOver != null)
            textoGameOver.gameObject.SetActive(false);

        ActualizarBarra();
    }

    public void RecibirDaño(float cantidad)
    {
        if (juegoTerminado) return;

        vidaActual -= cantidad;
        if (vidaActual < 0) vidaActual = 0;

        ActualizarBarra();

        if (vidaActual == 0)
        {
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        juegoTerminado = true;

        // Detener movimiento del juego
        Time.timeScale = 0f;

        // Mostrar texto
        if (textoGameOver != null)
        {
            textoGameOver.text = "GAME OVER";
            textoGameOver.color = Color.red;
            textoGameOver.gameObject.SetActive(true);
        }

        // Esperar 3 segundos reales (porque timeScale = 0)
        yield return new WaitForSecondsRealtime(3f);

        // Mostrar menú Game Over
        GameOverMenu.Instance.MostrarMenu();
    }

    void ActualizarBarra()
    {
        if (barraDeVida != null)
            barraDeVida.fillAmount = vidaActual / vidaMax;
    }
}