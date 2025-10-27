using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Actúa como el "cerebro" del enemigo.
/// Decide qué acción tomar (Patrullar, Perseguir, Atacar) basándose en
/// la información de los otros componentes.
/// </summary>
public class ControladorEnemigo : MonoBehaviour
{
     public bool haVistoAlJugador;
    [Header("Referencias (Asignar Manualmente)")]
    // La referencia más importante. Arrastra tu GameObject "Jugador" aquí.
    public Transform jugador;

    [Header("Componentes del Enemigo")]
    // Este script necesita controlar a los otros.
    // Puedes asignarlos aquí o dejarlos que se auto-asignen.
     MovimientoEnemigo movimiento;
     AtaqueEnemigo ataque;
     DetectarJugador deteccion;
     SaludEnemigo salud;

    void Awake()
    {
        // Intenta encontrar los componentes automáticamente si no los asignaste en el Inspector
        if (movimiento == null) movimiento = GetComponent<MovimientoEnemigo>();
        if (ataque == null) ataque = GetComponent<AtaqueEnemigo>();
        if (deteccion == null) deteccion = GetComponent<DetectarJugador>();
        if (salud == null) salud = GetComponent<SaludEnemigo>();
    }

    void Update()
    {
        // --- 1. COMPROBACIÓN DE SEGURIDAD (Igual que antes) ---
        if (jugador == null)
        {
            Debug.LogWarning("ControladorEnemigo no tiene asignado un Jugador.");
            return;
        }

        // --- 2. PRIORIDAD MÁXIMA: MUERTE (Igual que antes) ---
        if (salud.estaMuerto)
        {
            movimiento.Detener();
            return;
        }

        // --- 3. LÓGICA DE DECISIÓN (MODIFICADA) ---

        // Primero, comprobamos si vemos al jugador (incluso por un instante)
        if (deteccion.isPlayerDetected)
        {
            // ¡Lo vimos! Activamos la memoria permanentemente.
            haVistoAlJugador = true;
        }

        // Ahora, todas nuestras decisiones se basan en la MEMORIA,
        // no en si lo estamos viendo "justo ahora".
        if (haVistoAlJugador)
        {
            // ¡YA LO VIMOS ALGUNA VEZ! PERSEGUIR PARA SIEMPRE.

            float distanciaAlJugador = Vector2.Distance(transform.position, jugador.position);

            if (distanciaAlJugador <= ataque.distanciaAtaque)
            {
                // A) Estamos en rango de ataque: ATACAR
                movimiento.Detener();
                ataque.EjecutarAtaque();
            }
            else
            {
                // B) Estamos fuera de rango de ataque: PERSEGUIR
                movimiento.PosicionarseParaAtacar(jugador);
            }
        }
        else
        {
            // C) AÚN NO LO HEMOS VISTO NUNCA: PATRULLAR
            movimiento.Patrullar();
        }
    }
}