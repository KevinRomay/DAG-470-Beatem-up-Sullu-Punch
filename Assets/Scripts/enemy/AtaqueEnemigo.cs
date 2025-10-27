using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gestiona la lógica de ataque del enemigo.
/// Define el daño, el rango y el cooldown.
/// Es llamado por el ControladorEnemigo.
/// </summary>
public class AtaqueEnemigo : MonoBehaviour
{
    [Header("Configuración de Ataque")]
    public float distanciaAtaque = 1.2f;
    public float daño = 10f;
    public float tiempoEntreAtaques = 1.5f;

    [Header("Referencias (Asignar Manualmente)")]
    // Arrastra el GameObject del Jugador aquí
    public Transform jugador;

    // Referencia al script de salud del jugador
    private PlayerHealth saludJugador;

    // Variable interna para controlar el cooldown
    private float proximoAtaquePermitido = 0f;

    void Start()
    {
        // Busca el script de salud en el jugador que le asignamos
        if (jugador != null)
        {
            saludJugador = jugador.GetComponent<PlayerHealth>();
        }

        // Damos un aviso si falta algo
        if (jugador == null)
        {
            Debug.LogError("AtaqueEnemigo no tiene asignado un Jugador en el Inspector.");
        }
        if (saludJugador == null)
        {
            Debug.LogError("AtaqueEnemigo no pudo encontrar el script 'ControlSalud' en el Jugador.");
        }
    }

    /// <summary>
    /// Intenta ejecutar un ataque. Es llamado por el ControladorEnemigo.
    /// </summary>
    public void EjecutarAtaque()
    {
        // 1. Comprobar si el jugador y su salud son válidos
        if (saludJugador == null)
        {
            return; // No podemos atacar si no tenemos una referencia a la salud del jugador
        }

        // 2. Comprobar si ha pasado el tiempo de cooldown
        if (Time.time > proximoAtaquePermitido)
        {
            // 3. Reiniciar el cooldown
            proximoAtaquePermitido = Time.time + tiempoEntreAtaques;

            // 4. Aplicar el daño directamente (ya que no usamos eventos de animación)
            Debug.Log("¡Enemigo ataca al jugador y le hace " + daño + " de daño!");

            // Asegúrate de que tu script ControlSalud tenga un método público llamado "RecibirDaño"
            saludJugador.RecibirDaño(daño);
        }
    }
}