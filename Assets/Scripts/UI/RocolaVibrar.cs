using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocolaVibrar : MonoBehaviour
{
    public float intensidad = 0.05f; // Qué tan lejos se mueve
    public float velocidad = 0.05f;  // Cada cuánto tiempo cambia la posición

    private Vector3 posicionInicial;
    private float tiempoSiguienteMovimiento = 0f;

    void Start()
    {
        posicionInicial = transform.localPosition;
    }

    void Update()
    {
        if (Time.time >= tiempoSiguienteMovimiento)
        {
            // Calculamos un pequeño offset aleatorio
            float offsetX = Random.Range(-intensidad, intensidad);
            float offsetY = Random.Range(-intensidad, intensidad);

            // Aplicamos la vibración
            transform.localPosition = posicionInicial + new Vector3(offsetX, offsetY, 0f);

            // Calculamos el próximo cambio
            tiempoSiguienteMovimiento = Time.time + velocidad;
        }
    }
}

