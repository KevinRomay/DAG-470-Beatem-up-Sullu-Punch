using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarJugador : MonoBehaviour
{
    [Header("Configuraci�n de Detecci�n")]
    public float rangoDeteccion = 5f;
    [Tooltip("Capas que bloquear�n la visi�n del enemigo (ej: Muros, Obstaculos)")]
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

        // 1. Primero, comprobamos si est� en el rango (usando la versi�n optimizada)
        float distanciaCuadrada = (jugador.position - transform.position).sqrMagnitude;
        if (distanciaCuadrada <= (rangoDeteccion * rangoDeteccion))
        {
            // 2. Si est� en rango, lanzamos un rayo para ver si hay obst�culos
            Vector2 direccionAlJugador = (jugador.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direccionAlJugador, rangoDeteccion, capasDeObstaculos);

            // Si el rayo NO golpea nada (o sea, no hay obst�culos en medio)...
            if (hit.collider == null)
            {
                isPlayerDetected = true;
            }
            // Opcional: Si el rayo S� golpea algo, y ese algo es el jugador, tambi�n es v�lido.
            else if (hit.transform == jugador)
            {
                isPlayerDetected = true;
            }
            else // Si el rayo golpea un obst�culo
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

        // Tambi�n dibujamos la l�nea de visi�n para depuraci�n
        if (isPlayerDetected)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, jugador.position);
        }
    }
}