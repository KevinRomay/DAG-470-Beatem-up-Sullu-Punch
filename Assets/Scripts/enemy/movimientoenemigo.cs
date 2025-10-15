using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float velocidad = 3f;           // Velocidad del enemigo
    [SerializeField] private Transform puntoA;              // Primer punto de patrulla
    [SerializeField] private Transform puntoB;             // Segundo punto de patrulla
    [SerializeField] private float distanciaOptimaAtaque = 1.2f;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Vector2 destino;       // Posición a la que se mueve actualmente
    private bool yendoHaciaB = true;

    private Animator anim;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        // Inicializar destino al primer punto de patrulla
        if (puntoA != null)
            destino = puntoA.position;
    }

    void Update()
    {
        // Actualiza el parámetro "Velocidad" en el Animator con la velocidad real del Rigidbody
        if (anim != null)
        {
            anim.SetFloat("Velocidad", rb.velocity.magnitude);
        }
    }

    // -----------------------------
    // Patrullar entre puntos A y B
    // -----------------------------
    public void Patrullar()
    {
        if (puntoA == null || puntoB == null)
        {
            Detener(); // Si no hay puntos, se detiene
            return;
        }

        Vector2 direccion = ((Vector2)destino - (Vector2)transform.position).normalized;
        rb.velocity = direccion * velocidad;

        if (sprite != null && direccion.x != 0)
            sprite.flipX = direccion.x < 0;

        if (Vector2.Distance(transform.position, destino) < 0.1f)
        {
            yendoHaciaB = !yendoHaciaB;
            destino = yendoHaciaB ? puntoB.position : puntoA.position;
        }
    }

    // -----------------------------
    // Seguir al jugador
    // -----------------------------
    public void PosicionarseParaAtacar(Transform jugador) // No necesitamos distanciaAtaque aquí si la obtenemos del AtaqueJugador
    {
        if (jugador == null)
        {
            Detener();
            return;
        }

        Vector2 miPosicion = transform.position;
        Vector2 posJugador = jugador.position;

        float distanciaX = Mathf.Abs(miPosicion.x - posJugador.x);
        float diferenciaY = miPosicion.y - posJugador.y;

        Vector2 direccionMovimiento = Vector2.zero;

        // Prioridad 1: Alinearse en el eje Y
        if (Mathf.Abs(diferenciaY) > 0.1f) // Si no estamos alineados verticalmente
        {
            direccionMovimiento.y = Mathf.Sign(posJugador.y - miPosicion.y); // Moverse hacia la Y del jugador
        }

        // Prioridad 2: Ajustar la posición en el eje X para mantener distancia óptima
        // Si está lejos, se acerca. Si está muy cerca, intenta mantener la distancia.
        if (distanciaX > distanciaOptimaAtaque)
        {
            direccionMovimiento.x = Mathf.Sign(posJugador.x - miPosicion.x); // Moverse hacia el jugador en X
        }
        else if (distanciaX < distanciaOptimaAtaque * 0.8f) // Si está un poco más cerca de lo óptimo, retrocede ligeramente
        {
            direccionMovimiento.x = -Mathf.Sign(posJugador.x - miPosicion.x);
        }
        // Si ya está en la distancia óptima en X, direccionMovimiento.x se queda en 0.

        rb.velocity = direccionMovimiento.normalized * velocidad; // Aplicar el movimiento

        // Voltear sprite según dirección de movimiento horizontal
        if (sprite != null && rb.velocity.x != 0)
            sprite.flipX = rb.velocity.x < 0;
    }

    // -----------------------------
    // Detener movimiento
    // -----------------------------
    public void Detener()
    {
        rb.velocity = Vector2.zero;
    }
}
