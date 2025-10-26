using UnityEngine;

public class CombateJugador : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool isAttacking = false;
    private MovimientoJugador movimientoJugador;

    void Awake()
    {
        movimientoJugador = GetComponent<MovimientoJugador>();
    }

    void Update()
    {
        // Ataca con el botón Fire1 (por defecto: click izquierdo o Ctrl)
        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            IniciarAtaque();
        }
    }

    void IniciarAtaque()
    {
        isAttacking = true;
        if (movimientoJugador != null)
            movimientoJugador.enabled = false; // Desactiva movimiento
        Debug.Log("Atacando");
        // Aquí podrías activar la animación de ataque si lo deseas
    }

    // Este método debe llamarse al final de la animación de ataque (Animation Event)
    public void FinAtaque()
    {
        isAttacking = false;
        if (movimientoJugador != null)
            movimientoJugador.enabled = true; // Reactiva movimiento
        Debug.Log("Ataque terminado");
    }
}
