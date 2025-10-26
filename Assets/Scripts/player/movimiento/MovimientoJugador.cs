using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    [Header("Configuración de movimiento")]
    public float velocidad = 5f;
    public float velocidadDiagonal = 3.5f;

    private Rigidbody2D rb;
    private Vector2 movimiento;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Leer entrada de movimiento (horizontal y vertical)
        float movX = Input.GetAxisRaw("Horizontal");
        float movY = Input.GetAxisRaw("Vertical");

        movimiento = new Vector2(movX, movY).normalized;

        // Opcional: ajustar velocidad en diagonal para mantener consistencia
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
}
