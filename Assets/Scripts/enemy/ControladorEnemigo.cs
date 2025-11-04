using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public bool haVistoAlJugador = false;
    private bool tienePermisoDeAtacar = false;

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

        // --- 1. COMPROBACIÓN DE SEGURIDAD ---
        if (jugador == null) return;

        // --- 2. PRIORIDAD MÁXIMA: MUERTE ---
        if (salud.estaMuerto)
        {
            // Si morimos, liberamos nuestro slot de ataque si lo teníamos
            if (tienePermisoDeAtacar)
            {
                // Le avisamos al "árbitro" que nuestro slot está libre
                GestorDeAgresividad.Instancia.LiberarSlotDeAtaque(this);
                tienePermisoDeAtacar = false;
            }
            movimiento.Detener();
            return; // Fin del Update
        }

        // --- 3. LÓGICA DE DETECCIÓN (Patrullar) ---
        // Si no hemos visto al jugador, nos quedamos en modo Patrulla
        if (!haVistoAlJugador)
        {
            if (deteccion.isPlayerDetected)
            {
                haVistoAlJugador = true; // ¡Lo vimos!
            }
            else
            {
                // Aún no lo hemos visto, seguimos patrullando
                movimiento.Patrullar();
                return; // Fin del Update
            }
        }

        // --- 4. LÓGICA DE DECISIÓN BASADA EN PERMISOS ---
        // (Esta parte solo se ejecuta si haVistoAlJugador es true)

        float distanciaAlJugador = Vector2.Distance(transform.position, jugador.position);

        if (tienePermisoDeAtacar)
        {
            // ---- CASO A: TENGO PERMISO PARA ATACAR ----
            // Lógica normal de Perseguir y Atacar

            // Determinamos la distancia de ataque correcta
            float miDistanciaDeAtaque = 0;
            if (ataqueEnemigo != null)
                miDistanciaDeAtaque = ataqueEnemigo.distanciaAtaque;
            else if (ataqueEnemigoDistancia != null)
                miDistanciaDeAtaque = ataqueEnemigoDistancia.distanciaAtaque;

            // 1. ¿Estoy en rango?
            if (distanciaAlJugador <= miDistanciaDeAtaque)
            {
                // Sí -> ATACAR
                movimiento.Detener();
                if (ataqueEnemigo != null) ataqueEnemigo.EjecutarAtaque();
                else if (ataqueEnemigoDistancia != null) ataqueEnemigoDistancia.EjecutarAtaque();
            }
            else
            {
                // No -> PERSEGUIR
                // Si me alejo demasiado (ej: el jugador huye), libero mi slot
                // (Asegúrate de que 'distanciaDeParada' sea pública en MovimientoEnemigo)
                if (distanciaAlJugador > movimiento.distanciaDeParada * 1.5f)
                {
                    GestorDeAgresividad.Instancia.LiberarSlotDeAtaque(this);
                    tienePermisoDeAtacar = false;
                }
                movimiento.PosicionarseParaAtacar(jugador);
            }
        }
        else
        {
            // ---- CASO B: NO TENGO PERMISO (Estoy en espera) ----

            // 1. ¿Estoy lo suficientemente cerca para pedir permiso?
            if (distanciaAlJugador <= movimiento.distanciaDeParada)
            {
                // Sí. Pido permiso al "árbitro"
                tienePermisoDeAtacar = GestorDeAgresividad.Instancia.SolicitarSlotDeAtaque(this);

                // Si pedí permiso PERO no me lo dieron (slots llenos)...
                if (!tienePermisoDeAtacar)
                {
                    // ...entonces me detengo y espero mi turno.
                    movimiento.Detener();
                }
                // (Si SÍ me dieron permiso, 'tienePermisoDeAtacar' será true
                // y el próximo frame entraré en el "CASO A" y atacaré).
            }
            else
            {
                // No. Estoy lejos. Mi trabajo es ACERCARME (Perseguir).
                // ¡Este es el cambio clave!
                movimiento.PosicionarseParaAtacar(jugador);
            }

        }




            // Debug.Log(tienePermisoDeAtacar);


        }
    public void ActivarPersecusion()
    {
        haVistoAlJugador = true;
    }
}