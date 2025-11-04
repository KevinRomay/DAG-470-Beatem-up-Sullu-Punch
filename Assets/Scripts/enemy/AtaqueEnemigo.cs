using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueEnemigo : MonoBehaviour
{
    [Header("Configuración de Ataque")]
    public float distanciaAtaque = 1.2f;
    public float daño = 10f;
    public float tiempoEntreAtaques = 1.5f;

    [Header("Referencias (Asignar Manualmente)")]
    public Transform jugador;

    private PlayerHealth saludJugador;

    private float proximoAtaquePermitido = 0f;

    void Start()
    {

        if (jugador != null)
        {
            saludJugador = jugador.GetComponent<PlayerHealth>();
        }

        if (jugador == null)
        {
            Debug.LogError("AtaqueEnemigo no tiene asignado un Jugador en el Inspector.");
        }
        if (saludJugador == null)
        {
            Debug.LogError("AtaqueEnemigo no pudo encontrar el script 'ControlSalud' en el Jugador.");
        }
    }

    public void EjecutarAtaque()
    {
        if (saludJugador == null)
        {
            return; // No podemos atacar si no tenemos una referencia a la salud del jugador
        }

        if (Time.time > proximoAtaquePermitido)
        {
            proximoAtaquePermitido = Time.time + tiempoEntreAtaques;

            Debug.Log("¡Enemigo ataca al jugador y le hace " + daño + " de daño!");

            saludJugador.RecibirDaño(daño);
        }
    }
}