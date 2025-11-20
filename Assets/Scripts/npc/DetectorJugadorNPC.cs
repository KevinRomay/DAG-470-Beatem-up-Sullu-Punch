using UnityEngine;
using System;

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
            npc.MostrarIcono(true);

            // Notifica al jugador
            OnJugadorCerca?.Invoke(this);

            if (npc.EsCinematica)
                npc.EjecutarAccion();
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

        if (!npc.DialogoActivo)
            npc.IniciarDialogo();
        else
            npc.SiguienteLinea();
    }
}

