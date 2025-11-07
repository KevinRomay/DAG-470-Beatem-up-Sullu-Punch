using UnityEngine;
using UnityEngine.UI;

public class BrilloController : MonoBehaviour
{
    [Header("Referencias UI")]
    public Slider sliderBrillo;
    public Image panelBrillo;

    private const string BRILLO_KEY = "Brillo";

    void Start()
    {
        // Cargar el valor guardado o usar 1 por defecto
        float brilloGuardado = PlayerPrefs.GetFloat(BRILLO_KEY, 1f);

        // Asignar valor al slider y aplicar brillo
        sliderBrillo.value = brilloGuardado;
        ActualizarBrillo(brilloGuardado);

        // Escuchar cambios del slider
        sliderBrillo.onValueChanged.AddListener(ActualizarBrillo);
    }

    public void ActualizarBrillo(float valor)
    {
        // Cambia la opacidad del panel que oscurece la pantalla
        Color c = panelBrillo.color;
        c.a = 1f - valor; // entre 0 (oscuro) y 1 (claro)
        panelBrillo.color = c;

        // Guardar preferencia
        PlayerPrefs.SetFloat(BRILLO_KEY, valor);
    }

    private void OnDisable()
    {
        PlayerPrefs.Save();
    }
}
