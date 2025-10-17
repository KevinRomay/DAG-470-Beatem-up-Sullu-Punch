using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaludEnemigo : MonoBehaviour
{
    [Header("Vida del enemigo")]
    public float vidaMaxima = 100f;
    [HideInInspector]
    public float vidaActual;
    [HideInInspector]
    public bool estaMuerto = false;

    private ControladorEnemigo controlador;
    void Awake() // Cambiamos Start por Awake para asegurar que la referencia est� lista
    {
        InicializarVida();
        // --- A�ADE ESTA L�NEA ---
        controlador = GetComponent<ControladorEnemigo>();
    }


    // Inicializa la vida del enemigo
    public void InicializarVida()
    {
        vidaActual = vidaMaxima;
        estaMuerto = false;
    }

    // M�todo para recibir da�o
    public void RecibirDa�o(float cantidad)
    {
        if (estaMuerto) return;

        vidaActual -= cantidad;

        // Despu�s de bajar la vida, le avisamos al cerebro.
        if (controlador != null)
        {
            controlador.InformarDa�oRecibido();
        }

        if (vidaActual <= 0)
        {
            vidaActual = 0;
            estaMuerto = true;
        }
    }

    // Opcional: m�todo para curar al enemigo
    public void Curar(float cantidad)
    {
        if (estaMuerto) return;

        vidaActual += cantidad;
        if (vidaActual > vidaMaxima)
            vidaActual = vidaMaxima;
    }
}
