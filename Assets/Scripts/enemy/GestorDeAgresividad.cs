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
        // La lógica correcta:
        // "Si la Instancia YA EXISTE (no es null) Y no soy yo mismo..."
        if (Instancia != null && Instancia != this)
        {
            // "...entonces soy un duplicado. Destruirme."
            Destroy(gameObject);
        }
        else
        {
            // "...si no, significa que soy el primero. Yo soy la Instancia."
            Instancia = this;
            Debug.Log("¡GESTOR DE AGRESIVIDAD ESTÁ VIVO Y LISTO!");
        }
    }

    private void OnDestroy()
    {
        // Si este objeto era la Instancia...
        if (Instancia == this)
        {
            // ... "grita" en la consola que está muriendo.
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
