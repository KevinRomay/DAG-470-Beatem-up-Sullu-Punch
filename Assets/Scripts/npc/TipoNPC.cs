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
        Ninguna,
        DarItem,
        AbrirTienda,
        Cinematica
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
    private bool accionEjecutada = false;

    private void Awake()
    {
        if (panelDialogo != null)
            panelDialogo.SetActive(false);

        if (iconoInteraccion != null)
            iconoInteraccion.SetActive(false);
    }

    public void MostrarIcono(bool mostrar)
    {
        if (iconoInteraccion != null)
            iconoInteraccion.SetActive(mostrar);
    }

    public void IniciarDialogo()
    {
        // Si el NPC es de tipo cinematica, no muestra di�logo
        if (tipoAccion == TipoAccionNPC.Cinematica)
        {
            EjecutarAccion();
            return;
        }

        // Si no hay l�neas, ejecutar directamente la acci�n
        if (lineasDialogo.Length == 0)
        {
            EjecutarAccion();
            return;
        }

        if (panelDialogo == null)
            return;

        MostrarIcono(false);
        panelDialogo.SetActive(true);
        panelDialogo.transform.SetAsLastSibling();

        textoNombre.text = nombreNPC;
        textoDialogo.text = lineasDialogo[0];
        indiceLinea = 0;
        dialogoActivo = true;
        accionEjecutada = false;
    }

    public void SiguienteLinea()
    {
        if (!dialogoActivo) return;

        indiceLinea++;

        if (indiceLinea >= lineasDialogo.Length)
        {
            // Termina el di�logo y reci�n ah� ejecuta la acci�n
            CerrarDialogo(true);
            return;
        }

        textoDialogo.text = lineasDialogo[indiceLinea];
    }

    public void CerrarDialogo(bool finalizado = false)
    {
        if (panelDialogo != null)
            panelDialogo.SetActive(false);

        textoDialogo.text = "";
        textoNombre.text = "";
        dialogoActivo = false;

        // Solo ejecutar la acci�n si el di�logo lleg� al final
        if (finalizado && !accionEjecutada)
        {
            EjecutarAccion();
            accionEjecutada = true;
        }
    }

    public bool DialogoActivo => dialogoActivo;
    public bool EsCinematica => tipoAccion == TipoAccionNPC.Cinematica;

    public void EjecutarAccion()
    {
        if (accionEjecutada) return;

        accionEjecutada = true;

        switch (tipoAccion)
        {
            case TipoAccionNPC.Ninguna:
                Debug.Log("Solo di�logo, no hay acci�n especial.");
                break;
            case TipoAccionNPC.DarItem:
                Debug.Log($"NPC dio un item.");
                break;
            case TipoAccionNPC.AbrirTienda:
                Debug.Log("Se abri� la tienda del NPC.");
                break;
            case TipoAccionNPC.Cinematica:
                Debug.Log("Se activ� una cinem�tica del NPC.");
                break;
        }
    }
}



