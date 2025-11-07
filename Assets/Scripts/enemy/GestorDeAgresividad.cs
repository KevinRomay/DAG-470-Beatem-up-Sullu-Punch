using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorDeAgresividad : MonoBehaviour
{

    public static GestorDeAgresividad Instancia { get; private set; }

    public int maxAtacantesSimultaneos = 2;

    private List<ControladorEnemigo> atacantesActuales = new List<ControladorEnemigo>();

    private void Awake()
    {

        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instancia = this;
            //Debug.Log("¡GESTOR DE AGRESIVIDAD ESTÁ VIVO Y LISTO!");
        }
    }

    private void OnDestroy()
    {
        if (Instancia == this)
        {
          //  Debug.LogError("¡¡ALERTA!! ¡El GestorDeAgresividad (Instancia) acaba de ser DESTRUIDO!");
            Instancia = null; // Limpiamos la instancia
        }
    }


    public bool SolicitarSlotDeAtaque(ControladorEnemigo enemigo)
    {
        if (atacantesActuales.Count >= maxAtacantesSimultaneos)
        {
            return false;
        }

        if (!atacantesActuales.Contains(enemigo))
        {
            atacantesActuales.Add(enemigo);
        }
        return true;

    }
    
    public void LiberarSlotDeAtaque(ControladorEnemigo enemigo)
    {
        if (atacantesActuales.Contains(enemigo))
        {
            atacantesActuales.Remove(enemigo);
        }
    }

}
