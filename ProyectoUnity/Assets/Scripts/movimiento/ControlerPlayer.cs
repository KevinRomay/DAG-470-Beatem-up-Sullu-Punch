using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlerPlayer : MonoBehaviour
{
    [SerializeField] private bool puedoMov = true;
    [SerializeField] private bool obstaq = false;
    [SerializeField] private Rigidbody2D cuerpo;
    [SerializeField] private float velocidad = 2f;
    [SerializeField] private float fuerzaSalto = 5f;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    // Definición de estados
    private enum EstadosDeMovimiento
    {
        Aturdido,
        Muerto,
        Bloqueado,
        Libre
    }

    [SerializeField] private EstadosDeMovimiento estadoActual = EstadosDeMovimiento.Libre;

    private bool isJumping = false; // Por defecto es false

    void Start()
    {
        if (cuerpo == null)
            cuerpo = GetComponent<Rigidbody2D>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (animator == null)
            animator = GetComponent<Animator>();
    }

    void Update()
    {
        // --- Movimiento horizontal ---
        float movX = Input.GetAxis("Horizontal");
        float movY = Input.GetAxis("Vertical");

        float movement = new Vector2(movX, movY).magnitude;
        animator.SetFloat("movement", movement);

        if ((movX != 0 || movY != 0) && puedoMov && estadoActual == EstadosDeMovimiento.Libre)
        {
            if (!obstaq)
                cuerpo.velocity = new Vector2(velocidad * movX, velocidad * movY);
            else
                cuerpo.velocity = Vector2.zero;
        }
        else
        {
            cuerpo.velocity = new Vector2(0, cuerpo.velocity.y);
        }

        // --- Rotar sprite ---
        if (movX > 0.01f)
            spriteRenderer.flipX = false;
        else if (movX < -0.01f)
            spriteRenderer.flipX = true;

        // --- Saltar con tecla U ---
        if (Input.GetKeyDown(KeyCode.U) && !isJumping)
        {
           // cuerpo.velocity = new Vector2(cuerpo.velocity.x, fuerzaSalto);
            isJumping = true;
            animator.SetBool("isJumping", true);
        }
    }

    // --- Función para resetear isJumping al terminar la animación ---
    public void TerminoSalto()
    {
        isJumping = false;
        animator.SetBool("isJumping", false);
    }
}
