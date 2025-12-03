ï»¿using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class TipoNPC : MonoBehaviour
{

    [Header("Datos del NPC")]
    [SerializeField] private string nombreNPC = "NPC sin nombre";
    [SerializeField] private GameObject iconoInteraccion;

    [SerializeField] private ItemSpawner itemSpawner;

    public enum TipoAccionNPC
    {
        Ninguna,
        DarItem,
        AbrirTienda,
        Cinematica,
        Automatico
    }

    [Header("DiÃ¡logo del NPC")]
    [TextArea(2, 6)]
    [SerializeField] private string[] lineasDialogo;

    [Header("AcciÃ³n al interactuar")]
    [SerializeField] private TipoAccionNPC tipoAccion = TipoAccionNPC.Ninguna;

    [Header("Referencias UI locales")]
    [SerializeField] private GameObject panelDialogo;
    [SerializeField] private TMP_Text textoNombre;
    [SerializeField] private TMP_Text textoDialogo;


    [Header("AnimaciÃ³n del panel")]
    [SerializeField] private float duracionAnimacion = 0.3f; // tiempo de animaciÃ³n
    [SerializeField] private Vector3 escalaMax = Vector3.one; // tamaÃ±o final del panel


    private int indiceLinea = 0;
    private bool dialogoActivo = false;
    private bool accionEjecutada = false;

    public TipoAccionNPC TipoAccion => tipoAccion;

    // SortingGroup cache para la actualizaciÃ³n dinÃ¡mica
    private SortingGroup sortingGroup;

    private void Awake()
    {
        if (panelDialogo != null)
            panelDialogo.SetActive(false);

        if (iconoInteraccion != null)
            iconoInteraccion.SetActive(false);

        // Cachear el SortingGroup del NPC (puede estar en el mismo GameObject o en hijos)
        sortingGroup = GetComponentInChildren<SortingGroup>();
    }

    public void MostrarIcono(bool mostrar)
    {
        if (iconoInteraccion != null)
            iconoInteraccion.SetActive(mostrar);
    }

    public void IniciarDialogo()
    {

        if (tipoAccion == TipoAccionNPC.DarItem && accionEjecutada)
        {
            Debug.Log("El NPC ya ha dado su item, no puede dar otro.");
            return;
        }

        // Si el NPC es de tipo cinematica, no muestra diálogo
        if (tipoAccion == TipoAccionNPC.Cinematica)
        {
            EjecutarAccion();
            return;
        }

        // Si no hay lÃ­neas, ejecutar directamente la acciÃ³n
        if (lineasDialogo.Length == 0)
        {
            EjecutarAccion();
            return;
        }

        if (panelDialogo == null)
            return;

        MostrarIcono(false);
        MostrarIcono(false);
        panelDialogo.transform.SetAsLastSibling();
        StartCoroutine(MostrarPanel());
    }

    public void SiguienteLinea()
    {
        if (!dialogoActivo) return;

        indiceLinea++;

        if (indiceLinea >= lineasDialogo.Length)
        {
            // Termina el diÃ¡logo y reciÃ©n ahÃ­ ejecuta la acciÃ³n
            CerrarDialogo(true);
            return;
        }

        textoDialogo.text = lineasDialogo[indiceLinea];
    }

    public void CerrarDialogo(bool finalizado = false)
    {
        dialogoActivo = false;

        // Solo ejecutar la acciÃ³n si el diÃ¡logo llegÃ³ al final
        if (finalizado && !accionEjecutada)
        {
            EjecutarAccion();
            accionEjecutada = true;
        }

        // Inicia animaciÃ³n de cierre SOLO si el panel estÃ¡ activo
        if (panelDialogo != null && panelDialogo.activeSelf)
        {
            StartCoroutine(OcultarPanel());
        }

        //Si el jugador sigue cerca, mostrar el icono
        if (finalizado && tipoAccion != TipoAccionNPC.Automatico)
            MostrarIcono(true);
    }

    private IEnumerator MostrarPanel()
    {
        panelDialogo.SetActive(true);
        panelDialogo.transform.localScale = Vector3.zero; // inicio desde 0
        float tiempo = 0f;

        while (tiempo < duracionAnimacion)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / duracionAnimacion;
            panelDialogo.transform.localScale = Vector3.Lerp(Vector3.zero, escalaMax, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }

        panelDialogo.transform.localScale = escalaMax;

        // Mostrar el texto despuÃ©s de la animaciÃ³n
        textoNombre.text = nombreNPC;
        textoDialogo.text = lineasDialogo[0];
        indiceLinea = 0;
        dialogoActivo = true;
        accionEjecutada = false;
    }

    private IEnumerator OcultarPanel()
    {
        float tiempo = 0f;
        Vector3 inicio = panelDialogo.transform.localScale;

        while (tiempo < duracionAnimacion)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / duracionAnimacion;
            panelDialogo.transform.localScale = Vector3.Lerp(inicio, Vector3.zero, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }

        panelDialogo.transform.localScale = Vector3.zero;
        panelDialogo.SetActive(false);
        textoDialogo.text = "";
        textoNombre.text = "";

        // Ahora sÃ­ mostramos el icono si corresponde
        if (tipoAccion != TipoAccionNPC.Automatico)
            MostrarIcono(true);
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
                Debug.Log("Solo diÃ¡logo, no hay acciÃ³n especial.");
                break;
            case TipoAccionNPC.DarItem:

                //if(accionEjecutada)return;
                //accionEjecutada = true;
                Debug.Log("El NPC dio un item al jugador.");
                if (itemSpawner != null)
                {
                    itemSpawner.SpawnItem();
                }
                else
                {
                    Debug.LogWarning("El ItemSpawner no está asignado en el NPC.");
                }
                break;
            case TipoAccionNPC.AbrirTienda:
                Debug.Log("Se abriÃ³ la tienda del NPC.");
                break;
            case TipoAccionNPC.Cinematica:
                Debug.Log("Se activÃ³ una cinemÃ¡tica del NPC.");
                break;
        }
    }



    /// <summary>
    /// Actualiza el sorting order del NPC en relaciÃ³n al jugador.
    /// Requiere que el jugador y el NPC tengan `SortingGroup`. 
    /// LÃ³gica: si el jugador estÃ¡ por debajo (y por tanto debe aparecer en primer plano),
    /// el NPC toma `playerOrder - 1`. Si el jugador estÃ¡ por encima, NPC toma `playerOrder + 1`.
    /// </summary>
    /// <param name="jugadorTransform">Transform del jugador</param>
    /// <param name="playerSortingGroup">SortingGroup del jugador (si se conoce, evita GetComponent)</param>
    /// <param name="offset">Offset adicional de orden si necesitas ajustar finamente</param>
    public void ActualizarOrdenSprite(Transform jugadorTransform, SortingGroup playerSortingGroup = null, int offset = 0)
    {
        if (sortingGroup == null)
            return; // no hay SortingGroup en el NPC

        if (jugadorTransform == null)
            return;

        // Obtener SortingGroup del jugador si no se pasÃ³
        if (playerSortingGroup == null)
            playerSortingGroup = jugadorTransform.GetComponentInChildren<SortingGroup>();

        if (playerSortingGroup == null)
            return; // ambos deben tener SortingGroup segÃºn tu especificaciÃ³n

        // Sincronizar capa y calcular orden relativo
        sortingGroup.sortingLayerID = playerSortingGroup.sortingLayerID;
        int playerOrder = playerSortingGroup.sortingOrder;

        float npcY = transform.position.y;
        float playerY = jugadorTransform.position.y;

        int desiredOrder;

        // Si el jugador estÃ¡ por debajo (Y menor) debe aparecer en primer plano -> playerOrder mayor que NPC
        if (playerY < npcY)
            desiredOrder = playerOrder - 1;
        else
            desiredOrder = playerOrder + 1;

        desiredOrder += offset;

        sortingGroup.sortingOrder = desiredOrder;
    }

}





