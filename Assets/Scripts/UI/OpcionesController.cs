using UnityEngine;
using UnityEngine.UI;

public class OpcionesController : MonoBehaviour
{
    [Header("Referencias UI")]
    public Slider sliderVolumen;
    public Slider sliderBrillo;
    public AudioSource audioSource;
    public Image panelBrillo;

    void Start()
    {
        sliderVolumen.value = PlayerPrefs.GetFloat("Volumen", 1f);
        sliderBrillo.value = PlayerPrefs.GetFloat("Brillo", 1f);
        ActualizarVolumen(sliderVolumen.value);
        ActualizarBrillo(sliderBrillo.value);

        sliderVolumen.onValueChanged.AddListener(ActualizarVolumen);
        sliderBrillo.onValueChanged.AddListener(ActualizarBrillo);
    }

    public void ActualizarVolumen(float valor)
    {
        audioSource.volume = valor;
        PlayerPrefs.SetFloat("Volumen", valor);
    }

    public void ActualizarBrillo(float valor)
    {
        Color c = panelBrillo.color;
        c.a = 1f - valor;
        panelBrillo.color = c;
        PlayerPrefs.SetFloat("Brillo", valor);
    }
}

