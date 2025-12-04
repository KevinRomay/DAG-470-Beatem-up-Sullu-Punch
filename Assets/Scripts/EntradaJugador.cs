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

    // Nuevas acciones
    public InputAction a_jump;
    public InputAction a_run;

    public CombateJugador combate;
    //referencia a interacciones jugador

    private DetectorJugadorNPC npcActivo; // NPC que notificó que está cerca

    private MovimientoJugador movimientoScript;

    private void OnEnable()
    {
        a_interactuar = acciones.FindAction("Interact");
        DetectorJugadorNPC.OnJugadorCerca += RegistrarNPCActivo;

        a_atacar = acciones.FindAction("Attack");
        a_lanzar = acciones.FindAction("Throw");

        // Buscar nuevas acciones. Asegúrate que los nombres coincidan en el InputActionAsset: "Jump" y "Run"
        a_jump = acciones.FindAction("Jump");
        a_run = acciones.FindAction("Run");

        // Habilitar las acciones que vamos a leer por código
        a_lanzar?.Enable();
        a_jump?.Enable();
        a_run?.Enable();
    }

    private void OnDisable()
    {
        a_lanzar?.Disable();
        a_jump?.Disable();
        a_run?.Disable();
        DetectorJugadorNPC.OnJugadorCerca -= RegistrarNPCActivo;
    }

    // Start is called before the first frame update
    void Start()
    {
        combate = GetComponent<CombateJugador>();
        movimientoScript = GetComponent<MovimientoJugador>();
        //interacciones = GetComponent<InteraccionesJugador>();
    }

    // Update is called once per frame
    void Update()
    {
        if (a_atacar != null && a_atacar.WasPressedThisFrame())
        {
            combate.Atacar();
        }

        //VALIDAR QUE HAYA UN NPC INTERACTUABLE CERCA
        if (a_interactuar != null && a_interactuar.WasPressedThisFrame())
        {
            if (npcActivo != null)
                npcActivo.IntentarInteractuar();
            //Tener metodo interactuar
        }

        if (a_lanzar != null && a_lanzar.WasPressedThisFrame())
        {
            AtaqueDistanciaJugador lanzamiento = GetComponent<AtaqueDistanciaJugador>();
            if (lanzamiento != null)
               lanzamiento.Lanzar();
        }

        // Salto: usar WasPressedThisFrame para detectar inicio de salto
        if (a_jump != null && a_jump.WasPressedThisFrame())
        {
            movimientoScript?.IntentarSaltar();
        }

        // Carrera: lectura continua (hold). Si tu acción Run es un botón, ReadValue<float>() > 0.5f funciona.
        if (a_run != null)
        {
            float runVal = 0f;
            try
            {
                runVal = a_run.ReadValue<float>();
            }
            catch
            {
                // si la acción está configurada como Button puede lanzar excepción al leer float; usar IsPressed en su lugar
                if (a_run.triggered) runVal = 1f;
            }
            bool running = runVal > 0.5f;
            movimientoScript?.SetRun(running);
        }
    }
    private void RegistrarNPCActivo(DetectorJugadorNPC detector)
    {
        npcActivo = detector;
    }
}
