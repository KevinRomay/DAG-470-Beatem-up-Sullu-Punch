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


        float distanciaAlJugador = Vector2.Distance(transform.position, jugador.position);

        if (tienePermisoDeAtacar)
        {

            float miDistanciaDeAtaque = 0;
            if (ataqueEnemigo != null)
                miDistanciaDeAtaque = ataqueEnemigo.distanciaAtaque;
            else if (ataqueEnemigoDistancia != null)
                miDistanciaDeAtaque = ataqueEnemigoDistancia.distanciaAtaque;


            if (distanciaAlJugador <= miDistanciaDeAtaque)
            {
           
                movimiento.Detener();
                if (ataqueEnemigo != null) ataqueEnemigo.EjecutarAtaque();
                else if (ataqueEnemigoDistancia != null) ataqueEnemigoDistancia.EjecutarAtaque();
            }
            else
            {

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

            if (distanciaAlJugador <= movimiento.distanciaDeParada)
            {
             
                tienePermisoDeAtacar = GestorDeAgresividad.Instancia.SolicitarSlotDeAtaque(this);

             
                if (!tienePermisoDeAtacar)
                {
             
                    movimiento.Detener();
                }

            }
            else
            {

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