using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

// Asegúrate de que el nombre del archivo y la clase sean "AtaqueEnemigo"
public class AtaqueJugador : MonoBehaviour
{
    [Header("Configuración de Ataque")]
    public float distanciaAtaque = 1.2f;
    public float daño = 10f;
    public float tiempoEntreAtaques = 1.5f; // Un poco más de tiempo para que se sienta más rítmico

    private float proximoAtaquePermitido = 0f;

    // Referencias
    private Animator anim;
    private Transform jugador;
    private ControlSalud saludJugador; // Asegúrate que tu script de salud del jugador se llame así

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        // Buscamos al jugador y su componente de salud al inicio
        GameObject objJugador = GameObject.FindGameObjectWithTag("Player");
        if (objJugador != null)
        {
            jugador = objJugador.transform;
            saludJugador = objJugador.GetComponent<ControlSalud>(); // O el nombre que tenga tu script de salud
        }
    }

    // Este método es llamado por el ControladorEnemigo
    public void EjecutarAtaque()
    {
        // Solo podemos iniciar un ataque si ha pasado el tiempo de cooldown
        if (Time.time > proximoAtaquePermitido)
        {
            proximoAtaquePermitido = Time.time + tiempoEntreAtaques;

            // Le decimos al Animator que inicie el ataque. ¡Nada más!
            anim.SetTrigger("Atacar");
        }
    }

    // --- ¡ESTA ES LA FUNCIÓN CLAVE! ---
    // Este método será llamado por el Animation Event en el frame del golpe.
    // Asegúrate de que el nombre sea exacto.
    public void EventoDeDañoAnimacion()
    {
        if (jugador == null || saludJugador == null) return;

        // Volvemos a comprobar la distancia. El jugador pudo haberse movido.
        float distancia = Vector2.Distance(transform.position, jugador.position);

        if (distancia <= distanciaAtaque)
        {
            // Aplicamos el daño al jugador
            Debug.Log("¡Enemigo golpea al jugador!");
            // saludJugador.RecibirDaño(daño); // Descomenta esta línea
        }
    }

    // Opcional: Para depuración, dibuja el rango de ataque en la escena
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaAtaque);
    }
}
