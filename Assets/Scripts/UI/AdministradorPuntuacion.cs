using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AdministradorPuntuacion : MonoBehaviour
{
    [Header("Configuración del Puntaje")]
    public int puntajeActual = 0;
    public TextMeshProUGUI textoPuntaje;

    [Header("Teclas de prueba")]
    public KeyCode teclaEnemigoComun = KeyCode.L;
    public KeyCode teclaCombo = KeyCode.P;
    public KeyCode teclaMeta = KeyCode.I;
    public KeyCode teclaObjeto = KeyCode.J;
    public KeyCode teclaGuardar = KeyCode.G;
    public KeyCode teclaReiniciar = KeyCode.R;

    void Start()
    {
        // Cargar puntaje anterior si existe
        puntajeActual = PlayerPrefs.GetInt("PuntajeGuardado", 0);
        ActualizarTextoPuntaje();
    }

    void Update()
    {
        // ---- TECLAS DE TESTEO ----
        if (Input.GetKeyDown(teclaEnemigoComun))
            SumarPuntos(100, "Derrotó enemigo común");

        if (Input.GetKeyDown(teclaCombo))
            SumarPuntos(250, "Realizó combo");

        if (Input.GetKeyDown(teclaMeta))
            SumarPuntos(500, "Llegó a la meta");

        if (Input.GetKeyDown(teclaObjeto))
            SumarPuntos(50, "Recolectó objeto");

        // ---- GUARDAR / REINICIAR ----
        if (Input.GetKeyDown(teclaGuardar))
            GuardarPuntaje();

        if (Input.GetKeyDown(teclaReiniciar))
            ReiniciarPuntaje();
    }

    public void SumarPuntos(int cantidad, string motivo = "")
    {
        puntajeActual += cantidad;
        ActualizarTextoPuntaje();

        if (!string.IsNullOrEmpty(motivo))
            Debug.Log($"🏆 +{cantidad} puntos ({motivo})");
    }

    public void GuardarPuntaje()
    {
        PlayerPrefs.SetInt("PuntajeGuardado", puntajeActual);
        PlayerPrefs.Save();
        Debug.Log($"💾 Puntaje guardado: {puntajeActual}");
    }

    public void ReiniciarPuntaje()
    {
        puntajeActual = 0;
        PlayerPrefs.DeleteKey("PuntajeGuardado");
        ActualizarTextoPuntaje();
        Debug.Log("🔄 Puntaje reiniciado.");
    }

    void ActualizarTextoPuntaje()
    {
        if (textoPuntaje != null)
            textoPuntaje.text = "Puntaje " + puntajeActual.ToString();
    }
}
