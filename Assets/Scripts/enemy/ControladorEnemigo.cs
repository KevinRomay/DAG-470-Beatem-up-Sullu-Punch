using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorEnemigo : MonoBehaviour
{

    [Header("Referencias (Asignar Manualmente)")]

    public Transform jugador;

    [Header("Componentes del Enemigo")]
    public AtaqueEnemigo ataqueEnemigo;
    public AtaqueEnemigoDistancia ataqueEnemigoDistancia;

    MovimientoEnemigo movimiento;
    //AtaqueEnemigo ataque;
    DetectarJugador deteccion;
    SaludEnemigo salud;
    public bool haVistoAlJugador = false;
    private bool tienePermisoDeAtacar = false;

    [Header("Lógica de Persecución")]
    [Tooltip("Distancia máxima a la que el enemigo perseguirá al jugador antes de rendirse.")]
    public float distanciaMaximaDePersecucion = 25f; // ¡Regla #1!
    private SpriteRenderer sprite; // ¡Regla #2!

    void Awake()
    {
        // Intenta encontrar los componentes automáticamente si no los asignaste en el Inspector
        if (movimiento == null) movimiento = GetComponent<MovimientoEnemigo>();
        // if (ataque == null) ataque = GetComponent<AtaqueEnemigo>();
        if (deteccion == null) deteccion = GetComponent<DetectarJugador>();
        if (salud == null) salud = GetComponent<SaludEnemigo>();

        sprite = GetComponentInChildren<SpriteRenderer>();

        if (sprite == null)
        {
            Debug.LogError("ControladorEnemigo no pudo encontrar un SpriteRenderer en sus hijos.", this.gameObject);
        }


    }

    // En ControladorEnemigo.cs

    void Update()
    {
        // --- (Tus comprobaciones de Seguridad, Muerte y Detección se quedan igual) ---
        if (jugador == null) return;
        if (salud.estaMuerto)
        {
            if (tienePermisoDeAtacar)
            {
                GestorDeAgresividad.Instancia.LiberarSlotDeAtaque(this);
                tienePermisoDeAtacar = false;
            }
            movimiento.Detener();
            return;
        }
        if (!haVistoAlJugador)
        {
            if (deteccion.isPlayerDetected)
            {
                haVistoAlJugador = true;
            }
            else
            {
                movimiento.Patrullar();
                return;
            }
        }

        // --- (Lógica de Decisión con las nuevas reglas) ---

        float distanciaAlJugador = Vector2.Distance(transform.position, jugador.position);

        // --- REGLA #1: PÉRDIDA DE INTERÉS (se queda igual) ---
        if (distanciaAlJugador > distanciaMaximaDePersecucion)
        {
            haVistoAlJugador = false;
            if (tienePermisoDeAtacar)
            {
                GestorDeAgresividad.Instancia.LiberarSlotDeAtaque(this);
                tienePermisoDeAtacar = false;
            }
            return;
        }

        // --- Definimos el tipo de enemigo y su rango UNA SOLA VEZ ---
        bool esMelee = (ataqueEnemigo != null);
        bool esDistancia = (ataqueEnemigoDistancia != null);
        float miDistanciaDeAtaque = 0;
        if (esMelee) miDistanciaDeAtaque = ataqueEnemigo.distanciaAtaque;
        else if (esDistancia) miDistanciaDeAtaque = ataqueEnemigoDistancia.distanciaAtaque;


        // --- Lógica de Permisos ---
        if (tienePermisoDeAtacar)
        {
            // ---- CASO A: TENGO PERMISO PARA ATACAR ----

            if (distanciaAlJugador <= miDistanciaDeAtaque)
            {
                // --- REGLA #2: LÍNEA DE FUEGO (se queda igual) ---
                if (sprite.isVisible)
                {
                    movimiento.Detener();
                    if (esMelee) ataqueEnemigo.EjecutarAtaque();
                    else if (esDistancia) ataqueEnemigoDistancia.EjecutarAtaque();
                }
                else
                {
                    movimiento.Detener();
                }
            }
            else
            {
                // No estoy en rango -> PERSEGUIR
                movimiento.PosicionarseParaAtacar(jugador);

                if (distanciaAlJugador > movimiento.distanciaDeParada * 1.5f)
                {
                    GestorDeAgresividad.Instancia.LiberarSlotDeAtaque(this);
                    tienePermisoDeAtacar = false;
                }
            }
        }
        else
        {
            // ---- CASO B: NO TENGO PERMISO (¡LÓGICA CORREGIDA!) ----

            if (esMelee)
            {
                // ---- Lógica de espera para MELEE ----
                if (distanciaAlJugador <= movimiento.distanciaDeParada)
                {
                    tienePermisoDeAtacar = GestorDeAgresividad.Instancia.SolicitarSlotDeAtaque(this);
                    if (!tienePermisoDeAtacar)
                    {
                        movimiento.Detener(); // Slots llenos, me detengo y espero
                    }
                }
                else
                {
                    movimiento.PosicionarseParaAtacar(jugador); // Estoy lejos, me acerco
                }
            }
            else if (esDistancia)
            {
                // ---- Lógica de espera para DISTANCIA (Corregida) ----

                // 1. ¿Estoy en mi "zona dorada" (lejos, pero en rango)?
                // (Asegúrate de que 'distanciaDeRodeo' sea pública en MovimientoEnemigo)
                if (distanciaAlJugador <= miDistanciaDeAtaque && distanciaAlJugador >= movimiento.distanciaDeRodeo)
                {
                    // Sí. Pido permiso para atacar.
                    tienePermisoDeAtacar = GestorDeAgresividad.Instancia.SolicitarSlotDeAtaque(this);
                    if (!tienePermisoDeAtacar)
                    {
                        movimiento.Detener(); // Slots llenos, me detengo y espero
                    }
                }
                else
                {
                    // No. Estoy fuera de mi zona (muy lejos o muy cerca).
                    // Me reposiciono usando la lógica de "Rodear" (MANTENERME LEJOS).
                    movimiento.RodearAlJugador(jugador);
                }
            }
        }
    }


    // Debug.Log(tienePermisoDeAtacar);
    public void ActivarPersecusion()
    {
        {
            haVistoAlJugador = true;
        }
    }
}
