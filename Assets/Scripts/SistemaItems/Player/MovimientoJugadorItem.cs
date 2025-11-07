using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugadorItem : MonoBehaviour
{
    [Header("Configuración de movimiento")]
    [SerializeField] public float velocidad = 5f;
    [SerializeField] public float velocidadDiagonal = 3.5f;

    private Rigidbody2D rb;
    private Vector2 movimiento;

    // Guardamos la velocidad base para restaurarla si el jugador se potencia temporalmente
    private float velocidadBase;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        velocidadBase = velocidad;
    }

    void Update()
    {
        float movX = Input.GetAxisRaw("Horizontal");
        float movY = Input.GetAxisRaw("Vertical");

        movimiento = new Vector2(movX, movY).normalized;

        // Ajuste de velocidad diagonal para mantener consistencia
        if (movimiento.magnitude > 0.1f && Mathf.Abs(movimiento.x) > 0 && Mathf.Abs(movimiento.y) > 0)
            movimiento *= velocidadDiagonal;
        else
            movimiento *= velocidad;
    }

    void FixedUpdate()
    {
        rb.velocity = movimiento;
    }

    // Permite modificar temporalmente la velocidad (por ejemplo, al consumir una poción)
    public void ModificarVelocidad(float multiplicador, float duracion)
    {
        StopAllCoroutines(); // Detiene boosts anteriores
        StartCoroutine(CambiarVelocidadTemporal(multiplicador, duracion));
    }

    private System.Collections.IEnumerator CambiarVelocidadTemporal(float multiplicador, float duracion)
    {
        velocidad *= multiplicador;
        velocidadDiagonal *= multiplicador;

        yield return new WaitForSeconds(duracion);

        velocidad = velocidadBase;
        velocidadDiagonal = velocidadBase * 0.7f; // relación entre ambas velocidades
    }
}
