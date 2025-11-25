using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gestiona la salud del enemigo.
/// Recibe da�o y reporta si el enemigo est� muerto.
/// </summary>
public class SaludEnemigo : MonoBehaviour
{
    [Header("Configuraci�n de Salud")]
    [SerializeField] private float vidaMaxima = 100f;

    // Esta variable es le�da por otros scripts (como el Controlador)
    [HideInInspector]
    public bool estaMuerto = false;

    // Variable interna para llevar la cuenta
    [SerializeField] private float vidaActual;

    void Start()
    {
        // Al empezar, el enemigo tiene la vida al m�ximo
        vidaActual = vidaMaxima;
        estaMuerto = false;
    }

    /// <summary>
    /// M�todo p�blico para que otros scripts (como un proyectil o el ataque del jugador)
    /// puedan infligir da�o a este enemigo.
    /// </summary>
    /// <param name="cantidad">La cantidad de da�o recibido.</param>
    public void RecibirDaño(float cantidad)
    {
        // Si ya est� muerto, no puede recibir m�s da�o.
        if (estaMuerto)
        {
            return;
        }

        // Restamos el da�o a la vida actual
        vidaActual -= cantidad;

        // Comprobamos si el da�o ha matado al enemigo
        if (vidaActual <= 0)
        {
            vidaActual = 0; // Evitamos que la vida sea negativa
            estaMuerto = true;
            Debug.Log(gameObject.name + " ha sido derrotado.");
        }
    }
}