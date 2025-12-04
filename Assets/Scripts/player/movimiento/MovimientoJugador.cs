using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    [Header("Configuración de movimiento")]
    public float velocidad = 5f;
    public float velocidadDiagonal = 3.5f;

    [Header("Carrera")]
    [Tooltip("Multiplicador aplicado a `velocidad` cuando el jugador corre.")]
    public float runSpeedMultiplier = 1.7f;
    [Tooltip("Si true, el jugador mantiene control horizontal durante el salto.")]
    public bool airControl = true;

    [Header("Salto (simulado)")]
    [Tooltip("Fuerza inicial vertical (eje Z simulado)")]
    public float jumpForce = 6f;
    [Tooltip("Aceleración hacia abajo aplicada mientras está en el aire")]
    public float gravity = 18f;
    [Tooltip("Transform que contiene la representación visual (sprite) que se elevará en el salto")]
    public Transform visualRoot;

    [Header("Animador")]
    [Tooltip("Animator que controla las animaciones del jugador (si no se asigna, se intentará obtener del `visualRoot`).")]
    public Animator animator;

    [Tooltip("Nombre del parámetro float que representa la intensidad de movimiento (0..1).")]
    public string paramSpeed = "Speed";
    [Tooltip("Nombre del parámetro bool que indica carrera.")]
    public string paramIsRunning = "IsRunning";
    //[Tooltip("Nombre del parámetro float para dirección X (opcional).")]
    //public string paramMoveX = "MoveX";
    //[Tooltip("Nombre del parámetro float para dirección Y (opcional).")]
    //public string paramMoveY = "MoveY";
    [Tooltip("Trigger para el inicio del salto.")]
    public string paramJumpStart = "JumpStart";
    [Tooltip("Bool que indica que está cayendo.")]
    public string paramIsFalling = "IsFalling";
    [Tooltip("Trigger para el aterrizaje.")]
    public string paramLand = "Land";
    [Tooltip("Tiempo de damping para el parámetro `Speed`.")]
    public float speedDampTime = 0.08f;

    [Header("Estado de movimiento")]
    [SerializeField] private MovementState currentState = MovementState.Normal;

    [Header("Offset visual (para PSB)")]
    [Tooltip("Altura desde el pivot original hasta el suelo del personaje")]
    public float visualOffsetY = 0f; // ajusta según tu personaje

    private Rigidbody2D rb;
    private Vector2 movimiento;

    //Flip
    private bool mirandoDerecha = true;

    // Estado de carrera/salto
    private bool isRunning = false;

    // Variables para salto simulado
    private bool isAirborne = false;
    private float altitud = 0f; // altura simulada
    private float velZ = 0f;    // velocidad vertical simulada
    private bool isFalling = false;

    public MovementState CurrentState => currentState;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (visualRoot == null)
        {
            // por defecto usar el primer hijo como visual si existe
            if (transform.childCount > 0)
                visualRoot = transform.GetChild(0);
        }

        if (animator == null && visualRoot != null)
            animator = visualRoot.GetComponent<Animator>();
    }

    void Update()
    {
        // Si el estado actual no permite movimiento, no procesamos entrada
        if (currentState != MovementState.Normal)
        {
            movimiento = Vector2.zero;
            UpdateAnimatorMovement(Vector2.zero);
            return;
        }

        // Leer entrada de movimiento (horizontal y vertical)
        float movX = Input.GetAxisRaw("Horizontal");
        float movY = Input.GetAxisRaw("Vertical");
        Flip(movX);

        Vector2 raw = new Vector2(movX, movY);

        // Si estamos en el aire y no hay control aéreo, bloqueamos la componente vertical del mundo (no la altura simulada)
        if (isAirborne && !airControl)
        {
            raw.y = 0f;
        }

        movimiento = raw.normalized;

        // Ajustar velocidad en diagonal para mantener consistencia
        float currentSpeed = isRunning ? velocidad * runSpeedMultiplier : velocidad;
        float currentDiag = isRunning ? velocidadDiagonal * runSpeedMultiplier : velocidadDiagonal;

        if (movimiento.magnitude > 0.1f && Mathf.Abs(movimiento.x) > 0 && Mathf.Abs(movimiento.y) > 0)
        {
            movimiento *= currentDiag;
        }
        else
        {
            movimiento *= currentSpeed;
        }

        // Actualizar parámetros del Animator (velocidad y dirección)
        UpdateAnimatorMovement(raw);

        // Actualizar física del salto simulado (en Update para respuesta más fina)
        if (isAirborne)
        {
            velZ -= gravity * Time.deltaTime;
            altitud += velZ * Time.deltaTime;

            // Detectar el inicio de la caída (pasó el ápice)
            if (!isFalling && velZ < 0f)
            {
                isFalling = true;
                if (animator != null)
                    animator.SetBool(paramIsFalling, true);
            }

            if (altitud <= 0f)
            {
                // tocar suelo
                altitud = 0f;
                velZ = 0f;
                isAirborne = false;
                isFalling = false;
                UpdateVisualPosition();
                OnLand();
            }
            else
            {
                UpdateVisualPosition();
            }
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
            if (animator != null)
                animator.SetFloat(paramSpeed, 0f);
        }
    }

    public void ResetState()
    {
        SetState(MovementState.Normal);
    }

    // Utilitario: pregunta rápida si el jugador puede moverse
    public bool CanMove() => currentState == MovementState.Normal;

    // Métodos públicos para input externo (EntradaJugador u otros)
    public void SetRun(bool run)
    {
        isRunning = run;
        if (animator != null)
            animator.SetBool(paramIsRunning, isRunning);
    }

    public bool IntentarSaltar()
    {
        if (isAirborne) return false;        // no doble salto por defecto
        if (currentState != MovementState.Normal) return false;

        // iniciar salto simulado
        isAirborne = true;
        velZ = jumpForce;
        altitud = 0f;
        isFalling = false;

        if (animator != null)
        {
            animator.SetBool(paramIsFalling, false);
            animator.SetTrigger(paramJumpStart); // inicio del salto
        }

        UpdateVisualPosition();
        OnJump();
        return true;
    }

    private void UpdateVisualPosition()
    {
        if (visualRoot != null)
        {
            // elevamos la representación visual en Y local para simular salto
            // (no tocamos posición global del Rigidbody)
            Vector3 local = visualRoot.localPosition;
            local.y = altitud + visualOffsetY; // sumamos offset fijo
            visualRoot.localPosition = local;
        }
    }

    private void UpdateAnimatorMovement(Vector2 rawDirection)
    {
        if (animator == null) return;

        float currentMax = isRunning ? velocidad * runSpeedMultiplier : velocidad;
        currentMax = Mathf.Max(0.0001f, currentMax);
        float speedPercent = Mathf.Clamp01(movimiento.magnitude / currentMax);

        // Damped float para suavizar la transición del blend tree
        animator.SetFloat(paramSpeed, speedPercent, speedDampTime, Time.deltaTime);

        // Opcional: pasar dirección cruda para blend-trees 2D/4-direcciones
        //if (!string.IsNullOrEmpty(paramMoveX)) animator.SetFloat(paramMoveX, rawDirection.x);
        //if (!string.IsNullOrEmpty(paramMoveY)) animator.SetFloat(paramMoveY, rawDirection.y);

        // Estado de running
        animator.SetBool(paramIsRunning, isRunning);
    }

    private void OnJump()
    {
        // punto para reproducir efectos/animaciones/sounds
        // ejemplo: GetComponent<AudioSource>()?.PlayOneShot(clipJump);
    }

    private void OnLand()
    {
        if (animator != null)
        {
            animator.SetBool(paramIsFalling, false);
            animator.SetTrigger(paramLand);
        }
        // punto para reproducir efectos/sonidos de aterrizaje
    }

    private void Flip(float direccionX)
    {
        if (direccionX > 0 && !mirandoDerecha)
        {
            visualRoot.localScale = new Vector3(1, 1, 1);
            mirandoDerecha = true;
        }
        else if (direccionX < 0 && mirandoDerecha)
        {
            visualRoot.localScale = new Vector3(-1, 1, 1);
            mirandoDerecha = false;
        }
    }
}

public enum MovementState
{
    Normal,     // jugador puede moverse
    Stunned,    // aturdido, no puede moverse ni actuar
    Busy,       // ocupado (ej. recogiendo, interactuando)
    Cinematic,  // control deshabilitado por cinemática
    Attacking   // atacando, bloqueo de movimiento
}