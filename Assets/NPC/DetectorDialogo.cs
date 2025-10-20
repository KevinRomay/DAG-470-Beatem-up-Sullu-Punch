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
                npcCercano.MostrarIcono(true); //  Usamos el método público
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPCDialogo") && npcCercano != null)
        {
            npcCercano.MostrarIcono(false); //  Usamos el método público
            npcCercano.CerrarDialogo();
            npcCercano = null;
        }
    }

    private void Update()
    {
        if (npcCercano == null)
            return;

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





