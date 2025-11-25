using UnityEngine;

public class CinematicaSecuestro : MonoBehaviour
{
    [SerializeField] private MovimientoJugador movJugador;
    [SerializeField] private GameObject vagonetaObj;
    public void TerminarCinematica()
    {
        Debug.Log("Cinemática de secuestro iniciada.");
        movJugador.SetState(MovementState.Normal);
        vagonetaObj.SetActive(false);
        // Aquí iría la lógica para iniciar la cinemática
    }
}
