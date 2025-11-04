using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gestiona todo el movimiento físico del enemigo.
/// Ejecuta las acciones de patrullar, perseguir o detenerse.
/// </summary>
public class MovimientoEnemigo : MonoBehaviour
{
    [Header("Configuración de Patrulla")]
    [SerializeField] private float velocidadPatrulla = 2f;
    [SerializeField] private Transform puntoA;
    [SerializeField] private Transform puntoB;

    [Header("Configuración de Persecución")]
    [SerializeField] private float velocidadPersecucion = 4f;

    [Header("Configuración de Rodeo")]
    [SerializeField] private float distanciaDeRodeo = 4f;
    public float distanciaDeParada = 1.0f;

    // Referencias a componentes
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    // Variables internas para la patrulla
    private Vector2 destinoActual;
    private bool yendoHaciaB = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // Buscamos el SpriteRenderer en los hijos (por si el sprite es un objeto hijo)
        sprite = GetComponentInChildren<SpriteRenderer>();

        // Asignar el primer destino de la patrulla
        if (puntoA != null)
        {
            destinoActual = puntoA.position;
        }
    }

    /// <summary>
    /// Acción: Moverse entre el punto A y el punto B.
    /// </summary>
    public void Patrullar()
    {
        // Si no hay puntos de patrulla, no hace nada y se detiene
        if (puntoA == null || puntoB == null)
        {
            Detener();
            return;
        }

        // 1. Moverse hacia el destino actual
        Vector2 direccion = (destinoActual - (Vector2)transform.position).normalized;
        rb.velocity = direccion * velocidadPatrulla;

        // 2. Voltear el sprite según la dirección del MOVIMIENTO
        // (En patrulla, sí queremos que mire hacia donde camina)
        if (direccion.x < 0)
        {
            sprite.flipX = true; // Mirando a la izquierda
        }
        else if (direccion.x > 0)
        {
            sprite.flipX = false; // Mirando a la derecha
        }

        // 3. Comprobar si llegó al destino
        if (Vector2.Distance(transform.position, destinoActual) < 0.1f)
        {
            // Cambiar de destino
            yendoHaciaB = !yendoHaciaB;
            destinoActual = yendoHaciaB ? puntoB.position : puntoA.position;
        }
    }

    /// <summary>
    /// Acción: Perseguir al jugador y voltear para mirarlo.
    /// </summary>
    public void PosicionarseParaAtacar(Transform jugador)
    {
        // 1. Moverse hacia el jugador
        Vector2 direccion = (jugador.position - transform.position).normalized;
        rb.velocity = direccion * velocidadPersecucion;

        // 2. Voltear el sprite para que siempre mire al JUGADOR
        // (Lógica que corregimos antes)
        float direccionHaciaJugador = jugador.position.x - transform.position.x;
        if (direccionHaciaJugador < 0)
        {
            sprite.flipX = true; // Mirando a la izquierda (donde está el jugador)
        }
        else if (direccionHaciaJugador > 0)
        {
            sprite.flipX = false; // Mirando a la derecha (donde está el jugador)
        }
    }

    public void RodearAlJugador(Transform jugador)
    {
        float distanciaAlJugador = Vector2.Distance(transform.position, jugador.position);

        if(distanciaAlJugador > distanciaDeRodeo)
        {
            // Moverse hacia el jugador si está demasiado lejos
            Vector2 direccion = (jugador.position - transform.position).normalized;
            rb.velocity = direccion * velocidadPatrulla;
        }
        else if (distanciaAlJugador < distanciaDeRodeo)
        {
            // Alejarse del jugador si está demasiado cerca
            Vector2 direccion = (transform.position - jugador.position).normalized;
            rb.velocity = direccion * velocidadPatrulla;
        }
        else
        {
            // Si está a la distancia correcta, detenerse
            Detener();
        }

        float direccionHaciaJugador = jugador.position.x - transform.position.x;

        if (direccionHaciaJugador < 0)
        {
            sprite.flipX = true; // Mirando a la izquierda (donde está el jugador)
        }
        else if (direccionHaciaJugador > 0)
        {
            sprite.flipX = false; // Mirando a la derecha (donde está el jugador)
        }
        
    }


    public void Detener()
    {
        rb.velocity = Vector2.zero;
    }
}