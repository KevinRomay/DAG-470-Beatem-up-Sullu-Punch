using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gestiona la salud del enemigo.
/// Recibe daño y reporta si el enemigo está muerto.
/// </summary>
public class SaludEnemigo : MonoBehaviour
{
    [Header("Configuración de Salud")]
    public float vidaMaxima = 100f;

    // Esta variable es leída por otros scripts (como el Controlador)
    [HideInInspector]
    public bool estaMuerto = false;

    // Variable interna para llevar la cuenta
    private float vidaActual;

    void Start()
    {
        // Al empezar, el enemigo tiene la vida al máximo
        vidaActual = vidaMaxima;
        estaMuerto = false;
    }

    /// <summary>
    /// Método público para que otros scripts (como un proyectil o el ataque del jugador)
    /// puedan infligir daño a este enemigo.
    /// </summary>
    /// <param name="cantidad">La cantidad de daño recibido.</param>
    public void RecibirDaño(float cantidad)
    {
        // Si ya está muerto, no puede recibir más daño.
        if (estaMuerto)
        {
            return;
        }

        // Restamos el daño a la vida actual
        vidaActual -= cantidad;

        // Comprobamos si el daño ha matado al enemigo
        if (vidaActual <= 0)
        {
            vidaActual = 0; // Evitamos que la vida sea negativa
            estaMuerto = true;
            Debug.Log(gameObject.name + " ha sido derrotado.");
        }
    }
}