using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gestiona la salud del enemigo.
/// Recibe daño y reporta si el enemigo esta muerto.
/// </summary>
public class SaludEnemigo : MonoBehaviour
{
    [Header("Configuracion de Salud")]
    public float vidaMaxima = 100f;

    // Esta variable es le�da por otros scripts (como el Controlador)
    [HideInInspector]
    public bool estaMuerto = false;

    private ControladorEnemigo controlador;
    private float vidaActual;
    void Awake() // Cambiamos Start por Awake para asegurar que la referencia est� lista
    {
        InicializarVida();
        // --- A�ADE ESTA L�NEA ---
        controlador = GetComponent<ControladorEnemigo>();
    }


    // Inicializa la vida del enemigo
    public void InicializarVida()
    {
        // Al empezar, el enemigo tiene la vida al maximo
        vidaActual = vidaMaxima;
        estaMuerto = false;
    }

    /// <summary>
    /// Metodo publico para que otros scripts (como un proyectil o el ataque del jugador)
    /// puedan infligir daño a este enemigo.
    /// </summary>
    /// <param name="cantidad">La cantidad de daño recibido.</param>
    public void RecibirDaño(float cantidad)
    {
        // Si ya esta muerto, no puede recibir mss daño.
        if (estaMuerto)
        {
            return;
        }

        // Restamos el daño a la vida actual
        vidaActual -= cantidad;

        // Comprobamos si el daño ha matado al enemigo
        if (vidaActual <= 0)
        {
            vidaActual = 0;
            estaMuerto = true;
            Debug.Log(gameObject.name + " ha sido derrotado.");
            Morir();
        }
    }

    public void Morir()
    {
        if (GameManager.instancia != null)
        GameManager.instancia.RegistrarEnemigoDerrotado();
    } 
}