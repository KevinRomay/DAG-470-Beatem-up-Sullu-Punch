using System.Collections;
using TMPro;
using UnityEngine;

public class TipoNPC : MonoBehaviour
{
    [Header("Datos del NPC")]
    [SerializeField] private string nombreNPC = "NPC sin nombre";
    [SerializeField] private GameObject iconoInteraccion;

    public enum TipoAccionNPC
    {
        Ninguna,        // Solo di�logo
        DarItem,        // El NPC da un objeto
        AbrirTienda,    // Abre la interfaz de tienda
        Cinematica      // Activa una cinem�tica
    }

    [Header("Di�logo del NPC")]
    [TextArea(2, 6)]
    [SerializeField] private string[] lineasDialogo;

    [Header("Acci�n al interactuar")]
    [SerializeField] private TipoAccionNPC tipoAccion = TipoAccionNPC.Ninguna;

    [Header("Referencias UI locales")]
    [SerializeField] private GameObject panelDialogo;
    [SerializeField] private TMP_Text textoNombre;
    [SerializeField] private TMP_Text textoDialogo;

    private int indiceLinea = 0;
    private bool dialogoActivo = false;

    private void Awake()
    {
        if (panelDialogo != null)
            panelDialogo.SetActive(false);

        if (iconoInteraccion != null)
            iconoInteraccion.SetActive(false);
    }

    //  M�todo p�blico para controlar el icono de interacci�n
    public void MostrarIcono(bool mostrar)
    {
        if (iconoInteraccion != null)
            iconoInteraccion.SetActive(mostrar);
    }

    public void IniciarDialogo()
    {
        if (lineasDialogo.Length == 0 || panelDialogo == null)
            return;

        // Ocultar icono mientras se muestra el di�logo
        MostrarIcono(false);

        panelDialogo.SetActive(true);

        // Traer panel al frente en el Canvas
        panelDialogo.transform.SetAsLastSibling();

        textoNombre.text = nombreNPC;
        textoDialogo.text = lineasDialogo[0];
        indiceLinea = 0;
        dialogoActivo = true;
    }

    public void SiguienteLinea()
    {
        if (!dialogoActivo) return;

        indiceLinea++;

        if (indiceLinea >= lineasDialogo.Length)
        {
            CerrarDialogo();
            return;
        }

        textoDialogo.text = lineasDialogo[indiceLinea];
    }

    public void CerrarDialogo()
    {
        if (panelDialogo != null)
            panelDialogo.SetActive(false);

        textoDialogo.text = "";
        textoNombre.text = "";
        dialogoActivo = false;

        EjecutarAccion();

    }

    public bool DialogoActivo => dialogoActivo;


    public void EjecutarAccion()
    {
        switch (tipoAccion)
        {
            case TipoAccionNPC.Ninguna:
                Debug.Log("Solo di�logo, no hay acci�n especial.");
                break;

            case TipoAccionNPC.DarItem:
                // Mostramos mensaje en consola o UI simulando que dio un item
                Debug.Log($"NPC dio el item");
                break;

            case TipoAccionNPC.AbrirTienda:
                // Mostramos mensaje simulando que se abri� la tienda
                Debug.Log("Se abri� la tienda del NPC.");
                break;

            case TipoAccionNPC.Cinematica:
                // Mostramos mensaje simulando que se activ� una cinem�tica
                Debug.Log("Se activ� una cinem�tica del NPC.");
                break;
        }
    }


}


