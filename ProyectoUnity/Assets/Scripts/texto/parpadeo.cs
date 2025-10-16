using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BlinkWarning : MonoBehaviour
{
    public TextMeshProUGUI countdownText; // Referencia al texto del contador (el que va de 30 a 0)
    public TextMeshProUGUI warningText;   // Texto "segui hijita"
    public Image warningImage;            // Imagen de la mano
    public float blinkSpeed = 0.5f;       // Velocidad de parpadeo

    private bool isBlinking = false;

    void Start()
    {
        // Ocultar al inicio
        warningText.gameObject.SetActive(false);
        warningImage.gameObject.SetActive(false);
    }

    void Update()
    {
        // Intentamos leer el valor actual del contador
        int currentTime = int.Parse(countdownText.text);

        if (currentTime <= 10 && currentTime > 0)
        {
            if (!isBlinking)
            {
                // Mostrar y arrancar parpadeo
                warningText.gameObject.SetActive(true);
                warningImage.gameObject.SetActive(true);
                StartCoroutine(Blink());
            }
        }
        else if (currentTime == 0)
        {
            // Ocultar al terminar
            StopAllCoroutines();
            warningText.gameObject.SetActive(false);
            warningImage.gameObject.SetActive(false);
        }
    }

    System.Collections.IEnumerator Blink()
    {
        isBlinking = true;
        while (true)
        {
            // Cambiamos alpha (visibilidad)
            warningText.enabled = !warningText.enabled;
            warningImage.enabled = !warningImage.enabled;
            yield return new WaitForSeconds(blinkSpeed);
        }
    }
}
