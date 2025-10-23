using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public float tiempoTotal = 100f;
    private float tiempoActual;
    public float escalaTiempo = 1f / 3f;

    public TextMeshProUGUI textoTiempo;

    void Start()
    {
        tiempoActual = tiempoTotal;
        ActualizarTextoTiempo();
    }

    void Update()
    {
        if (tiempoActual > 0)
        {
           
            tiempoActual -= Time.deltaTime * escalaTiempo;
            ActualizarTextoTiempo();
        }
        else
        {
            tiempoActual = 0;
            ActualizarTextoTiempo();
            Debug.Log("⏰ Tiempo agotado!");
        }
    }

    public void AgregarTiempo(float cantidad)
    {
        tiempoActual += cantidad;
        if (tiempoActual > tiempoTotal)
            tiempoActual = tiempoTotal;
    }

    void ActualizarTextoTiempo()
    {
        textoTiempo.text = "Tiempo " + Mathf.CeilToInt(tiempoActual).ToString();
    }
}
