using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

public class ActivadorCinematica : MonoBehaviour
{
    [Header("Jugador y cámara")]
    [SerializeField] private MovimientoJugador movJugador;
    [SerializeField] private Collider2D colJugador;
    [SerializeField] private CinemachineVirtualCamera camJugador; // la que sigue al jugador

    [Header("Cinemática")]
    [SerializeField] private Transform puntoCinematica;           // punto donde la cámara se moverá
    [SerializeField] private PlayableDirector timeline;

    [Header("Opcional")]
    [SerializeField] private float velocidadCam = 5f;             // velocidad de la cámara para moverse al punto
    [SerializeField] private float smoothTime = 0.3f;             // tiempo de suavizado al volver

    private bool activada = false;
    private Vector3 camOffset;   // offset original para mantener Follow

    private void Awake()
    {
        if (camJugador != null && camJugador.Follow != null)
        {
            camOffset = camJugador.transform.position - camJugador.Follow.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activada) return;
        if (!other.CompareTag("Player")) return;

        activada = true;
        StartCoroutine(RutinaCinematica());
    }

    private System.Collections.IEnumerator RutinaCinematica()
    {
        // Bloquear movimiento
        movJugador.SetState(MovementState.Cinematic);
        colJugador.enabled = false;

        Transform jugadorTransform = movJugador.transform;

        // Desactivar Follow temporalmente
        camJugador.Follow = null;

        // Mover la cámara suavemente al punto de la cinemática
        yield return StartCoroutine(MoverCamara(camJugador.transform.position, puntoCinematica.position, velocidadCam));

        // Reproducir timeline
        timeline.Play();

        // Esperar a que termine
        while (timeline.state == PlayState.Playing)
            yield return null;

        // Volver la cámara suavemente hacia el jugador usando SmoothDamp y manteniendo offset
        yield return StartCoroutine(MoverCamaraSuave(camJugador.transform.position, jugadorTransform.position + camOffset, smoothTime));

        // Restaurar Follow
        camJugador.Follow = jugadorTransform;

        // Devolver control al jugador
        movJugador.SetState(MovementState.Normal);
        colJugador.enabled = true;
    }

    // Mover la cámara con MoveTowards
    private System.Collections.IEnumerator MoverCamara(Vector3 start, Vector3 end, float velocidad)
    {
        float distancia;
        do
        {
            distancia = Vector3.Distance(camJugador.transform.position, end);
            camJugador.transform.position = Vector3.MoveTowards(camJugador.transform.position, end, velocidad * Time.deltaTime);
            yield return null;
        } while (distancia > 0.05f);
    }

    // Mover la cámara con SmoothDamp
    private System.Collections.IEnumerator MoverCamaraSuave(Vector3 start, Vector3 end, float smoothTime)
    {
        Vector3 velocidad = Vector3.zero;
        while (Vector3.Distance(camJugador.transform.position, end) > 0.01f)
        {
            camJugador.transform.position = Vector3.SmoothDamp(camJugador.transform.position, end, ref velocidad, smoothTime);
            yield return null;
        }
        camJugador.transform.position = end; // asegurar posición final exacta
    }
}





