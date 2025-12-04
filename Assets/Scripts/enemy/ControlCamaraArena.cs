using UnityEngine;
using Cinemachine; // Necesario

public class ControlCamaraArena : MonoBehaviour
{
    [Header("Arrastra aquí tu pequeña Arena de Combate")]
    public Collider2D arenaDeCombate;

    // --- CAMBIO AQUÍ: Usamos CinemachineConfiner2D ---
    private CinemachineConfiner2D confiner;
    private Collider2D limitesOriginalesDelNivel;

    private void Start()
    {
        // --- CAMBIO AQUÍ: Buscamos la versión 2D ---
        confiner = FindObjectOfType<CinemachineConfiner2D>();

        if (confiner != null)
        {
            // Guardamos tus límites originales (el nivel completo)
            limitesOriginalesDelNivel = confiner.m_BoundingShape2D;
        }
        else
        {
            Debug.LogError("¡ERROR! No encontré el componente 'CinemachineConfiner2D' en tu escena.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BloquearCamara();
        }
    }

    void BloquearCamara()
    {
        if (confiner != null && arenaDeCombate != null)
        {
            Debug.Log("🔒 CAMBIANDO A LÍMITES DE ARENA");

            // Activamos la arena pequeña
            arenaDeCombate.gameObject.SetActive(true);

            // Cambiamos el confiner para que use la arena pequeña
            confiner.m_BoundingShape2D = arenaDeCombate;

            // Suavizamos el cambio
            confiner.InvalidateCache();

            // Apagamos este trigger
            GetComponent<Collider2D>().enabled = false;
        }
    }

    // Llama a esto cuando ganes la pelea
    public void DesbloquearCamara()
    {
        if (confiner != null)
        {
            Debug.Log("🔓 RESTAURANDO LÍMITES DEL NIVEL");

            // Restauramos los límites grandes
            confiner.m_BoundingShape2D = limitesOriginalesDelNivel;
            confiner.InvalidateCache();

            // Apagamos la arena pequeña
            if (arenaDeCombate != null) arenaDeCombate.gameObject.SetActive(false);
        }
    }
}