using UnityEngine;

public class DetectorDialogo : MonoBehaviour
{
    private TipoNPC npcCercano;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NPCDialogo"))
        {
            npcCercano = other.GetComponent<TipoNPC>();
            if (npcCercano != null)
            {
                npcCercano.MostrarIcono(true);

                // Si es una cinemática, ejecuta la acción automáticamente
                if (npcCercano.EsCinematica)
                {
                    npcCercano.EjecutarAccion();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPCDialogo") && npcCercano != null)
        {
            npcCercano.MostrarIcono(false);

            // Cerrar diálogo solo si estaba activo
            if (npcCercano.DialogoActivo)
                npcCercano.CerrarDialogo();

            npcCercano = null;
        }
    }

    private void Update()
    {
        if (npcCercano == null)
            return;

        // Mostrar icono solo si no está hablando
        npcCercano.MostrarIcono(!npcCercano.DialogoActivo);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!npcCercano.DialogoActivo)
                npcCercano.IniciarDialogo();
            else
                npcCercano.SiguienteLinea();
        }
    }
}






