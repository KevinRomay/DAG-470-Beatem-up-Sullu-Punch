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

        if (textoGameOver != null)
            textoGameOver.gameObject.SetActive(false);

        ActualizarBarra();
    }

    public void RecibirDaño(float cantidad)
    {
        if (juegoTerminado) return;

        vidaActual -= cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMax);

        ActualizarBarra();

        if (vidaActual <= 0)
        {
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        juegoTerminado = true;
        Time.timeScale = 0f;

        // Activar texto
        textoGameOver.gameObject.SetActive(true);
        textoGameOver.text = "GAME OVER";
        textoGameOver.color = new Color(1, 0, 0, 0); // Rojo pero invisible

        // === POSICIÓN PERSONALIZADA ===
        textoGameOver.rectTransform.anchoredPosition = new Vector2(-577f, 411f);

        // === FADE IN ===
        yield return StartCoroutine(FadeTexto(textoGameOver, 0f, 1f, 1f));

        yield return new WaitForSecondsRealtime(3f);

        // === FADE OUT ===
        yield return StartCoroutine(FadeTexto(textoGameOver, 1f, 0f, 1f));

        textoGameOver.gameObject.SetActive(false);

        GameOverMenu.Instance.MostrarMenu();
    }


    IEnumerator FadeTexto(TextMeshProUGUI texto, float alfaInicio, float alfaFinal, float duracion)
    {
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            tiempo += Time.unscaledDeltaTime;
            float t = tiempo / duracion;

            float alfa = Mathf.Lerp(alfaInicio, alfaFinal, t);
            texto.color = new Color(texto.color.r, texto.color.g, texto.color.b, alfa);

            yield return null;
        }

        // Asegurar alfa final exacto
        texto.color = new Color(texto.color.r, texto.color.g, texto.color.b, alfaFinal);
    }

    void ActualizarBarra()
    {
        if (barraDeVida != null)
            barraDeVida.fillAmount = vidaActual / vidaMax;
    }
}

