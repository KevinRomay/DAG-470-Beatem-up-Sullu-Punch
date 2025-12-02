using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< Updated upstream
public class SaludEnemigo : MonoBehaviour
{
    [Header("Vida del enemigo")]
    public float vidaMaxima = 100f;
    [HideInInspector]
    public float vidaActual;
=======
/// <summary>
/// Gestiona la salud del enemigo.
/// Recibe da√±o y reporta si el enemigo esta muerto.
/// </summary>
public class SaludEnemigo : MonoBehaviour
{
    [Header("Configuracion de Salud")]
    public float vidaMaxima = 100f;

    // Esta variable es leÔøΩda por otros scripts (como el Controlador)
>>>>>>> Stashed changes
    [HideInInspector]
    public bool estaMuerto = false;

    private ControladorEnemigo controlador;
    void Awake() // Cambiamos Start por Awake para asegurar que la referencia estÈ lista
    {
        InicializarVida();
        // --- A—ADE ESTA LÕNEA ---
        controlador = GetComponent<ControladorEnemigo>();
    }


    // Inicializa la vida del enemigo
    public void InicializarVida()
    {
<<<<<<< Updated upstream
=======
        // Al empezar, el enemigo tiene la vida al maximo
>>>>>>> Stashed changes
        vidaActual = vidaMaxima;
        estaMuerto = false;
    }

<<<<<<< Updated upstream
    // MÈtodo para recibir daÒo
    public void RecibirDaÒo(float cantidad)
    {
        if (estaMuerto) return;

        vidaActual -= cantidad;

        // DespuÈs de bajar la vida, le avisamos al cerebro.

=======
    /// <summary>
    /// Metodo publico para que otros scripts (como un proyectil o el ataque del jugador)
    /// puedan infligir da√±o a este enemigo.
    /// </summary>
    /// <param name="cantidad">La cantidad de da√±o recibido.</param>
    public void RecibirDa√±o(float cantidad)
    {
        // Si ya esta muerto, no puede recibir mss da√±o.
        if (estaMuerto)
        {
            return;
        }

        // Restamos el da√±o a la vida actual
        vidaActual -= cantidad;

        // Comprobamos si el da√±o ha matado al enemigo
>>>>>>> Stashed changes
        if (vidaActual <= 0)
        {
            vidaActual = 0;
            estaMuerto = true;
<<<<<<< Updated upstream
        }
    }

    // Opcional: mÈtodo para curar al enemigo
    public void Curar(float cantidad)
    {
        if (estaMuerto) return;

        vidaActual += cantidad;
        if (vidaActual > vidaMaxima)
            vidaActual = vidaMaxima;
    }
}
=======
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
>>>>>>> Stashed changes
