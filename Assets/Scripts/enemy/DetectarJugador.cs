using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarJugador : MonoBehaviour
{
    [Header("Configuración de Detección")]
    public float rangoDeteccion = 5f;
    [Tooltip("Capas que bloquearán la visión del enemigo (ej: Muros, Obstaculos)")]
    public LayerMask capasDeObstaculos; // <<-- NUEVA VARIABLE

    [Header("Referencias")]
    public Transform jugador;

    [HideInInspector]
    public bool isPlayerDetected = false;

    private void Start()
    {
        if (jugador == null)
            jugador = GameObject.FindGameObjectWithTag("Player")?.transform; // El '?' es un atajo para evitar errores si no lo encuentra
    }

    void Update()
    {
        if (jugador == null)
        {
            isPlayerDetected = false;
            return;
        }

        // 1. Primero, comprobamos si está en el rango (usando la versión optimizada)
        float distanciaCuadrada = (jugador.position - transform.position).sqrMagnitude;
        if (distanciaCuadrada <= (rangoDeteccion * rangoDeteccion))
        {
            // 2. Si está en rango, lanzamos un rayo para ver si hay obstáculos
            Vector2 direccionAlJugador = (jugador.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direccionAlJugador, rangoDeteccion, capasDeObstaculos);

            // Si el rayo NO golpea nada (o sea, no hay obstáculos en medio)...
            if (hit.collider == null)
            {
                isPlayerDetected = true;
            }
            // Opcional: Si el rayo SÍ golpea algo, y ese algo es el jugador, también es válido.
            else if (hit.transform == jugador)
            {
                isPlayerDetected = true;
            }
            else // Si el rayo golpea un obstáculo
            {
                isPlayerDetected = false;
            }
        }
        else
        {
            isPlayerDetected = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoDeteccion);

        // También dibujamos la línea de visión para depuración
        if (isPlayerDetected)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, jugador.position);
        }
    }
}