using System.Collections;
using UnityEngine;

public class DetectorDialogo : MonoBehaviour
{
    private TipoNPC npcCercano;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NPCDialogo"))
        {
            npcCercano = other.GetComponent<TipoNPC>();

            // Solo activamos el icono si NO hay diálogo activo
            if (npcCercano != null && npcCercano.IconoInteraccion != null
                && !ManagerDialogo.Instance.DialogoActivo)
            {
                npcCercano.IconoInteraccion.SetActive(true);
            }

            Debug.Log("NPC detectado: " + npcCercano.name);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPCDialogo") && npcCercano != null)
        {
            // Oculta el icono del NPC
            if (npcCercano.IconoInteraccion != null)
                npcCercano.IconoInteraccion.SetActive(false);

            // Cierra el diálogo si está activo
            if (ManagerDialogo.Instance.DialogoActivo)
                ManagerDialogo.Instance.CerrarDialogo();

            npcCercano = null;
            Debug.Log("Ya no estás detectando al NPC");
        }
    }

    private void Update()
    {
        if (npcCercano == null || ManagerDialogo.Instance == null)
            return;

        // Si hay diálogo activo, aseguramos que el icono esté desactivado
        if (ManagerDialogo.Instance.DialogoActivo)
        {
            if (npcCercano.IconoInteraccion != null)
                npcCercano.IconoInteraccion.SetActive(false);
        }
        else
        {
            // Si no hay diálogo activo y el icono está apagado, lo volvemos a activar
            if (npcCercano.IconoInteraccion != null && !npcCercano.IconoInteraccion.activeSelf)
                npcCercano.IconoInteraccion.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!ManagerDialogo.Instance.DialogoActivo)
            {
                ManagerDialogo.Instance.IniciarDialogo(npcCercano);
            }
            else
            {
                ManagerDialogo.Instance.MostrarSiguienteLinea();
            }
        }
    }
}



