using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueEnemigoDistancia : MonoBehaviour
{
    [Header("Configuración de Ataque")]
    public float distanciaAtaque = 10f;
    public float dañoProyectil = 20f;
    public float tiempoEntreDisparos = 2f;
    [Header("Referencias de Proyectil")]
    public GameObject prefabProyectil;
    public Transform puntoDisparo;
    [Header("Referencias (Asignar Manualmente)")]
    public Transform jugador;

    private float proximoAtaquePermitido = 0f;

    public void EjecutarAtaque()
    {
        if (Time.time >= proximoAtaquePermitido)
        {
            
            proximoAtaquePermitido = Time.time + tiempoEntreDisparos;
            DispararProyectil();
        }
    }

    private void DispararProyectil()
    {

        if (prefabProyectil == null || puntoDisparo == null || jugador == null) 
        { 
          Debug.LogWarning("Faltan referencias en AtaqueEnemigoDistancia");
            return;
        }
        Debug.Log("Disparando proyectil hacia el jugador");
    }

}
