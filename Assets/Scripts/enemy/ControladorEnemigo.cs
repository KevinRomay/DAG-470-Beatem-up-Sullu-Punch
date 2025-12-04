using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorEnemigo : MonoBehaviour
{
    // Estados del enemigo
    public enum EstadoEnemigo { Idle, Patrullando, Persiguiendo, Atacando, Herido, Muerto }
    [SerializeField] public EstadoEnemigo estadoActual;

    // Referencias a otros scripts
    MovimientoEnemigo movimiento;
    AtaqueJugador ataque;
    DetectarJugador deteccion;
    SaludEnemigo salud;
    //public AnimacionEnemigo animaciones;

    // Referencia al jugador
    public Transform jugador;
    private Coroutine rutinaDeAtaque = null;

    // Animator
    private Animator anim;
    private void Awake()
    {
        movimiento = GetComponent<MovimientoEnemigo>();
        ataque = GetComponent<AtaqueJugador>();
        deteccion = GetComponent<DetectarJugador>();
        salud = GetComponent<SaludEnemigo>();
        anim = GetComponentInChildren<Animator>();
    }
    void Start()
    {

        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        CambiarEstado(EstadoEnemigo.Patrullando);
    }

    // Dentro de la clase ControladorEnemigo

    void Update()
    {
        // Si está muerto, no hace nada más.
        if (salud.estaMuerto)
        {
            if (estadoActual != EstadoEnemigo.Muerto) CambiarEstado(EstadoEnemigo.Muerto);
            return;
        }

        // La máquina de estados principal. Cada estado llama a su propia lógica.
        switch (estadoActual)
        {
            case EstadoEnemigo.Patrullando:
                ComportamientoDePatrulla();
                break;
            case EstadoEnemigo.Persiguiendo:
                ComportamientoDePersecucion();
                break;
            case EstadoEnemigo.Atacando:
                // El comportamiento de ataque ahora se gestiona con una coroutine
                break;
        }
    }

    private void ComportamientoDePatrulla()
    {
        movimiento.Patrullar();

        // Condición para cambiar de estado: si detecta al jugador.
        if (deteccion.isPlayerDetected)
        {
            CambiarEstado(EstadoEnemigo.Persiguiendo);
        }
    }

    private void ComportamientoDePersecucion()
    {
        // Usa el movimiento de posicionamiento que discutimos
        movimiento.PosicionarseParaAtacar(jugador);

        float distanciaAlJugador = Vector2.Distance(transform.position, jugador.position);

        // Condición para cambiar a ATACAR
        if (distanciaAlJugador <= ataque.distanciaAtaque)
        {
            CambiarEstado(EstadoEnemigo.Atacando);
        }
        // Condición para volver a PATRULLAR
        else if (!deteccion.isPlayerDetected)
        {
            CambiarEstado(EstadoEnemigo.Patrullando);
        }
    }

    private IEnumerator RutinaEstadoAtacando()
    {
        movimiento.Detener();
        ataque.EjecutarAtaque(); // Esto dispara el trigger de la animación

        // Esperamos a que la animación termine. Puedes ajustar este tiempo
        // para que coincida con la duración de tu animación de ataque.
        yield return new WaitForSeconds(1.0f);

        // Después de atacar, volvemos a perseguir
        CambiarEstado(EstadoEnemigo.Persiguiendo);
        rutinaDeAtaque = null;
    }
    // Dentro de la clase ControladorEnemigo

    public void CambiarEstado(EstadoEnemigo nuevoEstado)
    {
        if (estadoActual == nuevoEstado) return;

        estadoActual = nuevoEstado;

        // Actualizamos el Animator
        switch (estadoActual)
        {
            case EstadoEnemigo.Patrullando:
            case EstadoEnemigo.Persiguiendo:
                // La animación de caminar se controla por la velocidad en MovimientoEnemigo
                break;
            case EstadoEnemigo.Atacando:
                // Iniciamos la rutina de ataque SÓLO si no se está ejecutando ya.
                if (rutinaDeAtaque == null)
                {
                    rutinaDeAtaque = StartCoroutine(RutinaEstadoAtacando());
                }
                break;
            case EstadoEnemigo.Herido:
                anim.SetTrigger("Herido");
                break;
            case EstadoEnemigo.Muerto:
                movimiento.Detener();
                anim.SetTrigger("Morir");
                // Aquí podrías desactivar colliders, etc.
                break;
        }
    }


    // en el ControladorEnemigo cuando reciba daño.



    public void RecibirDaño(float cantidad)
    {
        salud.RecibirDaño(cantidad);
        if (!salud.estaMuerto)
        {
            CambiarEstado(EstadoEnemigo.Herido);
        }
    }
}
