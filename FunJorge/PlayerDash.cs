using UnityEngine;
using System.Collections;

public class PlayerDash : MonoBehaviour
{
    [Header("Configuración del Dash")]
    [SerializeField] private float dashSpeed = 10f;        // Velocidad del dash
    [SerializeField] private float dashDuration = 0.2f;    // Duración del dash
    [SerializeField] private float dashCooldown = 1f;      // Tiempo entre dashes

    private bool _isDashing = false;                       // Indica si el jugador está dashing
    private float _nextDashTime = 0f;                      // Controla el cooldown
    private Rigidbody2D _rigidbody2D;                      // Referencia al Rigidbody
    private Animator _animator;                            // Referencia al Animator

    // Propiedad de solo lectura (abstracción)
    public bool IsDashing => _isDashing;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleDashInput();
    }

    /// <summary>
    /// Detecta si el jugador presiona el botón de dash y si está disponible.
    /// </summary>
    private void HandleDashInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= _nextDashTime)
        {
            StartCoroutine(PerformDash());
        }
    }

    /// <summary>
    /// Ejecuta el dash del jugador.
    /// </summary>
    private IEnumerator PerformDash()
    {
        _isDashing = true;
        _nextDashTime = Time.time + dashCooldown;

        // Dispara animación
        _animator?.SetTrigger("Dash");

        // Calcula dirección según hacia dónde mira el jugador
        float direction = Mathf.Sign(transform.localScale.x);
        float dashEndTime = Time.time + dashDuration;

        while (Time.time < dashEndTime)
        {
            _rigidbody2D.velocity = new Vector2(direction * dashSpeed, _rigidbody2D.velocity.y);
            yield return null;
        }

        _rigidbody2D.velocity = Vector2.zero;
        _isDashing = false;
    }
}
