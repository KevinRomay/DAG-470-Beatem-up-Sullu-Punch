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

        // --- ¡¡ESTE ES EL CÓDIGO QUE FALTABA!! ---

        // 1. Calcular la rotación para apuntar al jugador
        Vector2 direccion = (jugador.position - puntoDisparo.position).normalized;
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        Quaternion rotacion = Quaternion.Euler(0, 0, angulo);

        // 2. Crear (Instanciar) la bala en la posición y rotación correctas
        Instantiate(prefabProyectil, puntoDisparo.position, rotacion);
    }

}
