using System;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Collider2D))]
public class DetectorJugadorNPC : MonoBehaviour
{
    private TipoNPC npc;
    private bool jugadorCerca = false;

    // Evento que notifica al jugador que puede interactuar con este NPC
    public static event Action<DetectorJugadorNPC> OnJugadorCerca;

    private void Awake()
    {
        if (npc == null)
            npc = GetComponentInParent<TipoNPC>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;

            // Si NO es automatico, mostrar icono
            if (npc.TipoAccion != TipoNPC.TipoAccionNPC.Automatico)
                npc.MostrarIcono(true);

            OnJugadorCerca?.Invoke(this);

            npc.ActualizarOrdenSprite(other.transform, other.GetComponentInChildren<SortingGroup>());

            // Si es cinematica, ejecuta accion aparte
            if (npc.EsCinematica)
                npc.EjecutarAccion();

            //  Si es automatico, iniciar dialogo de inmediato
            if (npc.TipoAccion == TipoNPC.TipoAccionNPC.Automatico)
                npc.IniciarDialogo();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Mantener actualizado el orden mientras el jugador se mueva dentro del trigger
        if (other.CompareTag("Player"))
        {
            npc.ActualizarOrdenSprite(other.transform, other.GetComponentInChildren<SortingGroup>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            npc.MostrarIcono(false);
            npc.CerrarDialogo(false);
        }
    }

    public void IntentarInteractuar()
    {
        if (!jugadorCerca || npc == null) return;

        // Los automaticos no usan interacción manual
        if (npc.TipoAccion == TipoNPC.TipoAccionNPC.Automatico) return;

        if (!npc.DialogoActivo)
            npc.IniciarDialogo();
        else
            npc.SiguienteLinea();
    }




}

