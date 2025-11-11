using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaludJugador : MonoBehaviour
{
    [Header("Configuracion de Salud")]
    public float vidaMaxima = 100f;

    // Esta variable es le da por otros scripts (como el Controlador)
    [HideInInspector]
    public bool estaMuerto = false;

    // Variable interna para llevar la cuenta
    private float vidaActual;

    // Start is called before the first frame update
    void Start()
    {
        vidaActual = vidaMaxima;
        estaMuerto = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
      public void RecibirDanio(float cantidad)
    {
        // Si ya est  muerto, no puede recibir mas daño.
        if (estaMuerto)
        {
            return;
        }

        // Restamos el da o a la vida actual
        vidaActual -= cantidad;

        // Comprobamos si el da o ha matado al enemigo
        if (vidaActual <= 0)
        {
            vidaActual = 0; // Evitamos que la vida sea negativa
            estaMuerto = true;
            Debug.Log(gameObject.name + " ha sido derrotado.");
        }
    }
}