using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{


    [Header("Configuración de movimiento")]
    public float velocidad = 5f;
    public float velocidadDiagonal = 3.5f;

    [Header("Estado de movimiento")]
    [SerializeField] private MovementState currentState = MovementState.Normal;

    private Rigidbody2D rb;
    private Vector2 movimiento;

    public MovementState CurrentState => currentState;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Si el estado actual no permite movimiento, no procesamos entrada
        if (currentState != MovementState.Normal)
        {
            movimiento = Vector2.zero;
            return;
        }

        // Leer entrada de movimiento (horizontal y vertical)
        float movX = Input.GetAxisRaw("Horizontal");
        float movY = Input.GetAxisRaw("Vertical");

        movimiento = new Vector2(movX, movY).normalized;

        // Ajustar velocidad en diagonal para mantener consistencia
        if (movimiento.magnitude > 0.1f && Mathf.Abs(movimiento.x) > 0 && Mathf.Abs(movimiento.y) > 0)
        {
            movimiento *= velocidadDiagonal;
        }
        else
        {
            movimiento *= velocidad;
        }
    }

    void FixedUpdate()
    {
        // Mover al jugador usando Rigidbody2D
        rb.velocity = movimiento;
    }

    // API para controlar el estado desde otros sistemas (combate, cinemáticas, efectos)
    public void SetState(MovementState newState)
    {
        currentState = newState;

        // Si entramos en un estado que bloquea movimiento, aseguramos detener velocidad inmediata
        if (currentState != MovementState.Normal)
        {
            movimiento = Vector2.zero;
            if (rb != null) rb.velocity = Vector2.zero;
        }
    }

    public void ResetState()
    {
        SetState(MovementState.Normal);
    }

    // Utilitario: pregunta rápida si el jugador puede moverse
    public bool CanMove() => currentState == MovementState.Normal;
}
    public enum MovementState
    {
        Normal,     // jugador puede moverse
        Stunned,    // aturdido, no puede moverse ni actuar
        Busy,       // ocupado (ej. recogiendo, interactuando)
        Cinematic,  // control deshabilitado por cinemática
        Attacking   // atacando, bloqueo de movimiento
    }