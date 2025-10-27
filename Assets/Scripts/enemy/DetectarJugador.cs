using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Revisa constantemente si el jugador est� dentro de un rango definido.
/// Su estado (isPlayerDetected) es le�do por el ControladorEnemigo.
/// </summary>
public class DetectarJugador : MonoBehaviour
{
    [Header("Configuraci�n de Detecci�n")]
    public float rangoDeteccion = 5f;

    [Header("Referencia (Asignar Manualmente)")]
    // Arrastra el GameObject del Jugador aqu�
    public Transform jugador;

    // Esta es la variable de "salida" que leer� el ControladorEnemigo
    // La ocultamos del inspector porque no queremos cambiarla manualmente
    [HideInInspector]
    public bool isPlayerDetected = false;

    void Start()
    {
        // Importante: Notificar si el programador olvid� asignar al jugador
        if (jugador == null)
        {
            Debug.LogError("DetectarJugador: No se ha asignado al Jugador en el Inspector!", this.gameObject);
        }
    }

    void Update()
    {
        // Si no hay jugador, no podemos detectar nada
        if (jugador == null)
        {
            isPlayerDetected = false;
            return;
        }

        // 1. Calcular la distancia al jugador
        float distancia = Vector2.Distance(transform.position, jugador.position);

        // 2. Actualizar el estado de detecci�n
        // (Esto es una forma corta de hacer un if/else)
        isPlayerDetected = (distancia <= rangoDeteccion);
    }

    // --- Ayuda Visual en el Editor ---
    // Dibuja un c�rculo en el editor de Unity para ver el rango de detecci�n
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
    }
}