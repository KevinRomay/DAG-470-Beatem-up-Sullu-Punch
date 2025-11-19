using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EntradaJugador : MonoBehaviour
{
    public InputActionAsset acciones;

    public InputAction a_atacar;
    public InputAction a_interactuar;
    public InputAction a_lanzar;

    public CombateJugador combate;
    //referencia a interacciones jugador

    private DetectorJugadorNPC npcActivo; // NPC que notificó que está cerca

    private void OnEnable()
    {
        a_interactuar = acciones.FindAction("Interact");
        DetectorJugadorNPC.OnJugadorCerca += RegistrarNPCActivo;

        a_atacar = acciones.FindAction("Attack");
        a_lanzar = acciones.FindAction("Throw");
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
        if (a_interactuar.WasPressedThisFrame())
        {
            npcActivo.IntentarInteractuar();
            //Tener metodo interactuar

        }
        if (a_lanzar.WasPressedThisFrame())
        {
        AtaqueDistanciaJugador lanzamiento = GetComponent<AtaqueDistanciaJugador>();
        if (lanzamiento != null)
           lanzamiento.Lanzar();
        }
    }
    private void RegistrarNPCActivo(DetectorJugadorNPC detector)
    {
        npcActivo = detector;
    }
}
