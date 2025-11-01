using JetBrains.Annotations;
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
    
    [Header("Referencias (Asignar Manualmente)")]
    // La referencia más importante. Arrastra tu GameObject "Jugador" aquí.
    public Transform jugador;

    [Header("Componentes del Enemigo")]
    public AtaqueEnemigo ataqueEnemigo;
    public AtaqueEnemigoDistancia ataqueEnemigoDistancia;

     MovimientoEnemigo movimiento;
     //AtaqueEnemigo ataque;
     DetectarJugador deteccion;
     SaludEnemigo salud;
    public bool haVistoAlJugador;

    void Awake()
    {
        // Intenta encontrar los componentes automáticamente si no los asignaste en el Inspector
        if (movimiento == null) movimiento = GetComponent<MovimientoEnemigo>();
       // if (ataque == null) ataque = GetComponent<AtaqueEnemigo>();
        if (deteccion == null) deteccion = GetComponent<DetectarJugador>();
        if (salud == null) salud = GetComponent<SaludEnemigo>();
    }

    void Update()
    {
        if (jugador == null)
        {
            Debug.LogWarning("ControladorEnemigo no tiene asignado un Jugador.");
            return;
        }

        if (salud.estaMuerto)
        {
            movimiento.Detener();
            return;
        }

        if (deteccion.isPlayerDetected)
        {
            haVistoAlJugador = true;
        }

        if (haVistoAlJugador)
        {
            if (ataqueEnemigo != null)
            {
                float distanciaAlJugador = Vector2.Distance(transform.position, jugador.position);

                if (distanciaAlJugador <= ataqueEnemigo.distanciaAtaque)
                {
                    // A) ESTÁ LO SUFICIENTE CERCA: ATACAR
                    movimiento.Detener();
                    ataqueEnemigo.EjecutarAtaque();
                }
                else
                {
                    // B) LO HEMOS VISTO Y ESTÁ LEJOS: PERSEGUIR
                    movimiento.PosicionarseParaAtacar(jugador);
                }
            }
            else if (ataqueEnemigoDistancia != null)
            {
                float distanciaAlJugador = Vector2.Distance(transform.position, jugador.position);
                if (distanciaAlJugador <= ataqueEnemigoDistancia.distanciaAtaque)
                {
                    // A) ESTÁ LO SUFICIENTE CERCA: ATACAR
                    movimiento.Detener();
                    ataqueEnemigoDistancia.EjecutarAtaque();
                }
                else
                {
                    // B) LO HEMOS VISTO Y ESTÁ LEJOS: PERSEGUIR
                    movimiento.PosicionarseParaAtacar(jugador);
                }
            }
            else
            { 
             movimiento.Patrullar();
            }

        }
        else
        {
            // C) AÚN NO LO HEMOS VISTO NUNCA: PATRULLAR
            movimiento.Patrullar();
        }
        
    }
    public void ActivarPersecusion()
    {
        haVistoAlJugador = true;
    }
}