using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;          // Referencia al jugador
    public Transform enemyFeet;       // Punto de pies del enemigo
    public Transform playerFeet;      // Punto de pies del jugador
    private Animator animator;        // Referencia al Animator
    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer

    [Header("Parámetros")]
    public float moveSpeed = 3f;         // Velocidad de movimiento
    public float detectionRange = 25f;   // Rango de visión en X o Y
    public float stopDistance = 1.5f;    // Distancia mínima antes de atacar
    public float verticalTolerance = 0.8f; // Margen vertical (pies) para ataque

    private bool isChasing = false; // Estado actual

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (player == null || enemyFeet == null || playerFeet == null)
            return;

        float diffX = Mathf.Abs(player.position.x - transform.position.x);
        float diffFeetY = Mathf.Abs(playerFeet.position.y - enemyFeet.position.y);

        if (diffX <= detectionRange && diffFeetY <= detectionRange)
        {
            float distance = Vector2.Distance(transform.position, player.position);

            // Flip según dirección del jugador
            if (player.position.x < transform.position.x)
                spriteRenderer.flipX = false;  // Voltear a la izquierda
            else
                spriteRenderer.flipX = true; // Voltear a la derecha

            if (distance > stopDistance)
            {
                // Enemigo persigue → activar animación caminar
                isChasing = true;
                animator.SetBool("isWalking", true);

                Vector2 direction = (player.position - transform.position).normalized;
                transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
            }
            else
            {
                // Está en rango de ataque
                isChasing = false;
                animator.SetBool("isWalking", false);

                if (diffFeetY <= verticalTolerance)
                {
                    Debug.Log("Enemigo ataca al jugador");
                }
            }
        }
        else
        {
            // No ve al player → volver a Idle
            isChasing = false;
            animator.SetBool("isWalking", false);
        }
    }
}
