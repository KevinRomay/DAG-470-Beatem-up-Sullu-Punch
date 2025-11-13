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

    [Header("Diálogo del NPC")]
    [TextArea(2, 6)]
    [SerializeField] private string[] lineasDialogo;

    [Header("Acción al interactuar")]
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
        // Si el NPC es de tipo cinematica, no muestra diálogo
        if (tipoAccion == TipoAccionNPC.Cinematica)
        {
            EjecutarAccion();
            return;
        }

        // Si no hay líneas, ejecutar directamente la acción
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
            // Termina el diálogo y recién ahí ejecuta la acción
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

        // Solo ejecutar la acción si el diálogo llegó al final
        if (finalizado && !accionEjecutada)
        {
            EjecutarAccion();
            accionEjecutada = true;
        }
        if (finalizado)
        {
            // Si el jugador sigue cerca, volver a mostrar el icono
            MostrarIcono(true);
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
                Debug.Log("Solo diálogo, no hay acción especial.");
                break;
            case TipoAccionNPC.DarItem:
                Debug.Log($"NPC dio un item.");
                break;
            case TipoAccionNPC.AbrirTienda:
                Debug.Log("Se abrió la tienda del NPC.");
                break;
            case TipoAccionNPC.Cinematica:
                Debug.Log("Se activó una cinemática del NPC.");
                break;
        }
    }
}



