using UnityEngine;

public class IdleIndicator : MonoBehaviour
{
    [Header("Fade settings")]
    public CanvasGroup indicatorGroup;
    public float fadeSpeed = 2f;
    public float pauseTime = 0.3f;

    private bool isBlinking = false;
    private bool fadingIn = true;
    private float fadeTimer = 0f;

    private bool pendingHide = false; //  NUEVO

    [Header("Detección de inactividad")]
    public float idleTimeToShow = 5f;
    public Transform player;
    public float movementThreshold = 0.01f;

    private float idleTimer = 0f;
    private Vector3 lastPos;

    void Start()
    {
        lastPos = player.position;
        indicatorGroup.alpha = 0f;
    }

    void Update()
    {
        CheckIfIdle();

        if (isBlinking)
            BlinkEffect();
        else if (pendingHide)
            FinishAndHide();  //  Manejar apagado suave
    }

    
    // 1 Fade / Parpadeo
    
    void BlinkEffect()
    {
        if (fadingIn)
        {
            indicatorGroup.alpha += fadeSpeed * Time.deltaTime;

            if (indicatorGroup.alpha >= 1f)
            {
                indicatorGroup.alpha = 1f;

                fadeTimer += Time.deltaTime;
                if (fadeTimer >= pauseTime)
                {
                    fadingIn = false;
                    fadeTimer = 0f;
                }
            }
        }
        else
        {
            indicatorGroup.alpha -= fadeSpeed * Time.deltaTime;

            if (indicatorGroup.alpha <= 0f)
            {
                indicatorGroup.alpha = 0f;

                // si estamos esperando ocultar, este es el momento perfecto
                if (pendingHide)
                {
                    CompleteHide();
                    return;
                }

                fadeTimer += Time.deltaTime;
                if (fadeTimer >= pauseTime)
                {
                    fadingIn = true;
                    fadeTimer = 0f;
                }
            }
        }
    }

    
    // 2 Ocultado suave al detectar movimiento
    
    void FinishAndHide()
    {
        // Si esta en fade in  lo dejamos terminar y empezar fade out
        if (indicatorGroup.alpha > 0f)
        {
            indicatorGroup.alpha -= fadeSpeed * Time.deltaTime;
            if (indicatorGroup.alpha <= 0f)
            {
                CompleteHide();
            }
        }
    }

    void CompleteHide()
    {
        indicatorGroup.alpha = 0f;
        pendingHide = false;
        isBlinking = false;
    }

    
    // 3 Condicion de inactividad
    
    void CheckIfIdle()
    {
        float movement = Vector3.Distance(player.position, lastPos);

        if (movement > movementThreshold)
        {
            // jugador se movio no cortar el fade, esperar a que termine
            pendingHide = true;
            isBlinking = false;
            idleTimer = 0f;
        }
        else
        {
            idleTimer += Time.deltaTime;

            if (idleTimer >= idleTimeToShow)
            {
                isBlinking = true;
                pendingHide = false;
            }
        }

        lastPos = player.position;
    }



    public void StartBlinking()
    {
        pendingHide = false;
        isBlinking = true;
    }

    public void StopBlinking()
    {
        pendingHide = true;
        isBlinking = false;
    }

}








