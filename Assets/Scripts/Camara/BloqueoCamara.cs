using Cinemachine;
using UnityEngine;

public class BloqueoCamara : MonoBehaviour
{
    [Header("Cámara y objetivos")]
    public CinemachineVirtualCamera vCam;
    public Transform camBlockPoint;
    public Transform playerTransform;     // importante: Transform, no GameObject
    public GameObject barreras;

    [Header("Indicador de inactividad")]
    public IdleIndicator idleIndicator;   // referencia al script del panel

    private bool combateActivo = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!combateActivo && other.transform == playerTransform)
        {
            ActivarCombate();
        }
    }

    private void Update()
    {
        // Salida manual del combate con tecla Z
        if (combateActivo && Input.GetKeyDown(KeyCode.Z))
        {
            TerminarCombateManual();
        }
    }

    // ------------------------------
    //  INICIO DEL COMBATE
    // ------------------------------
    void ActivarCombate()
    {
        combateActivo = true;

        // bloquear cámara
        vCam.Follow = camBlockPoint;
        vCam.LookAt = camBlockPoint;

        // activar barreras
        barreras.SetActive(true);

        Debug.Log("Combate iniciado.");
    }

    // ------------------------------
    //  TERMINAR COMBATE (MANUAL con Z)
    // ------------------------------
    void TerminarCombateManual()
    {
        combateActivo = false;

        // devolver la cámara al jugador
        vCam.Follow = playerTransform;
        vCam.LookAt = playerTransform;

        // desactivar barreras
        barreras.SetActive(false);

        // activar el parpadeo del panel
        if (idleIndicator != null)
            idleIndicator.StartBlinking();

        Debug.Log("Combate terminado manualmente con Z.");
    }

    // ------------------------------
    //  FUTURO: cuando se derroten los enemigos
    // ------------------------------
    public void OnEnemigosDerrotados()
    {
        // Aquí pondrás la condición real cuando tengas tus enemigos
        Debug.Log("Enemigos derrotados: aquí debes llamar TerminarCombateManual() o una versión automática.");

        // Ejemplo:
        // TerminarCombateAutomático();
    }
}
