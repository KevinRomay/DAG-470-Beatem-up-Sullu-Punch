using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Revisa constantemente si el jugador está dentro de un rango definido.
/// Su estado (isPlayerDetected) es leído por el ControladorEnemigo.
/// </summary>
public class DetectarJugador : MonoBehaviour
{
    [Header("Configuración de Detección")]
    public float rangoDeteccion = 5f;

    [Header("Referencia (Asignar Manualmente)")]
    // Arrastra el GameObject del Jugador aquí
    public Transform jugador;

    // Esta es la variable de "salida" que leerá el ControladorEnemigo
    // La ocultamos del inspector porque no queremos cambiarla manualmente
    [HideInInspector]
    public bool isPlayerDetected = false;

    void Start()
    {
        // Importante: Notificar si el programador olvidó asignar al jugador
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

        // 2. Actualizar el estado de detección
        // (Esto es una forma corta de hacer un if/else)
        isPlayerDetected = (distancia <= rangoDeteccion);
    }

    // --- Ayuda Visual en el Editor ---
    // Dibuja un círculo en el editor de Unity para ver el rango de detección
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
    }
}