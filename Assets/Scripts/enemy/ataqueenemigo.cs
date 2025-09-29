using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Referencias")]
    public Transform enemyFeet;     // Pie del enemigo
    public Transform playerFeet;    // Pie del jugador
    private Animator animator;      // Animator del enemigo

    [Header("Parámetros")]
    public float verticalTolerance = 0.8f; // Tolerancia vertical para atacar

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TryPunch();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TryPunch();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Deja de atacar al separarse
            animator.SetBool("isPunching", false);
        }
    }

    private void TryPunch()
    {
        if (enemyFeet == null || playerFeet == null)
            return;

        float diffFeetY = Mathf.Abs(playerFeet.position.y - enemyFeet.position.y);

        if (diffFeetY <= verticalTolerance)
        {
            animator.SetBool("isPunching", true); // Activar animación
            Debug.Log("¡Enemigo ataca!");
        }
        else
        {
            animator.SetBool("isPunching", false); // Desactivar si no están alineados
        }
    }
}

