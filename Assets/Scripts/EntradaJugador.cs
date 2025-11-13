using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EntradaJugador : MonoBehaviour
{
    public InputActionAsset acciones;

    public InputAction a_atacar;
    public InputAction a_interactuar;

    public CombateJugador combate;
    //referencia a interacciones jugador

    private DetectorJugadorNPC npcActivo; // NPC que notificó que está cerca


    private void OnEnable()
    {
        a_interactuar = acciones.FindAction("Interact");
        a_atacar = acciones.FindAction("Attack");

        DetectorJugadorNPC.OnJugadorCerca += RegistrarNPCActivo;

    }

    private void OnDisable()
    {
        DetectorJugadorNPC.OnJugadorCerca -= RegistrarNPCActivo;
    }

    // Start is called before the first frame update
    void Start()
    {
        combate = GetComponent<CombateJugador>();
        //interacciones = GetComponent<InteraccionesJugador>();
    }

    // Update is called once per frame
    void Update()
    {
        if (a_atacar.WasPressedThisFrame())
        {
            combate.Atacar();
        }

        //VALIDAR QUE HAYA UN NPC INTERACTUABLE CERCA
        if (a_interactuar.WasPressedThisFrame() )
        {
            npcActivo.IntentarInteractuar();
            //Tener metodo interactuar

        }
    }
    private void RegistrarNPCActivo(DetectorJugadorNPC detector)
    {
        npcActivo = detector;
    }
}