using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombateJugador : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float attackRadius = 0.6f;
    [SerializeField] private float attackDistance = 0.2f;
    [SerializeField] private Vector2 attackOffset = new Vector2(0.5f, 0f);
    [SerializeField] private LayerMask attackLayerMask = ~0;

    [SerializeField] private Animator animator;
    private bool isAttacking = false;
    private MovimientoJugador movimientoJugador;
    private Coroutine attackCoroutine;

    [SerializeField] private float attackDelay;

    void Awake()
    {
        movimientoJugador = GetComponent<MovimientoJugador>();
    }

    void Update()
    {
        // Input puede venir por eventos o por Input.GetButtonDown, según tu sistema
        // Este script expone el método publico Atacar() para ser invocado desde UI/InputActions o botones.
    }

    // Método público que ejecuta el ataque: realiza un CircleCast2D para leer colisiones
    public void Atacar()
    {
        if (isAttacking) return;

        IniciarAtaque();

        // Determinar origen y dirección del cast según hacia dónde mira el jugador
        float facing = Mathf.Sign(transform.localScale.x);
        Vector2 origin = (Vector2)transform.position + new Vector2(attackOffset.x * facing, attackOffset.y);
        Vector2 direction = new Vector2(facing, 0f);

        // Ejecutar CircleCastAll para obtener todos los hits en la dirección
        RaycastHit2D[] hits = Physics2D.CircleCastAll(origin, attackRadius, direction, attackDistance, attackLayerMask);

        if (hits == null || hits.Length == 0)
        {
            Debug.Log("ATACANDO: no se detectaron colisiones");
        }
        else
        {
            foreach (var hit in hits)
            {
                if (hit.collider == null) continue;
                Debug.Log($"ATACANDO: colision con '{hit.collider.name}' (point: {hit.point}, distance: {hit.distance})");
                // Posteriormente: aplicar daño si implementa IDañable, etc.
            }
        }

        // Iniciamos la espera de 1 segundo y luego llamamos a FinAtaque
        if (attackCoroutine != null)
            StopCoroutine(attackCoroutine);
        attackCoroutine = StartCoroutine(AttackEndAfterDelay(attackDelay));
    }

    void IniciarAtaque()
    {
        isAttacking = true;

        // En vez de desactivar el componente, cambiamos el estado de movimiento
        if (movimientoJugador != null)
        {
            movimientoJugador.SetState(MovimientoJugador.MovementState.Attacking);
        }

        Debug.Log("Iniciando ataque - movimiento bloqueado por estado 'Attacking'");
        //animator?.SetTrigger("Attack"); // opcional: activa animación
    }

    // Coroutine que espera un delay (segundos) y luego llama a FinAtaque
    private IEnumerator AttackEndAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        attackCoroutine = null;
        FinAtaque();
    }

    // Este método debe llamarse al final de la animación de ataque (Animation Event)
    public void FinAtaque()
    {
        // Evita ejecutar la lógica de finalización si ya no estamos en estado de ataque
        if (!isAttacking) return;

        isAttacking = false;

        // Si hay una coroutine programada, la detenemos (evita llamadas duplicadas)
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }

        if (movimientoJugador != null)
        {
            movimientoJugador.ResetState(); // vuelve a Normal
        }

        Debug.Log("Ataque terminado - estado de movimiento restaurado a 'Normal'");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        float facing = 1f;
        if (Application.isPlaying && transform != null) facing = Mathf.Sign(transform.localScale.x);
        Vector2 origin = (Vector2)transform.position + new Vector2(attackOffset.x * facing, attackOffset.y);
        Gizmos.DrawWireSphere(origin, attackRadius);
        Vector2 dir = new Vector2(facing, 0f);
        Gizmos.DrawLine(origin, origin + dir * attackDistance);
        Gizmos.DrawWireSphere(origin + dir * attackDistance, 0.05f);
    }
}
