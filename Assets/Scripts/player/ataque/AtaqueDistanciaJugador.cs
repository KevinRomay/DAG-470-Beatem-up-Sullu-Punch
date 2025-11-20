using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueDistanciaJugador : MonoBehaviour
{
    public GameObject proyectilPrefab;
    public Transform puntoDisparo;
    public float velocidadProyectil = 10f;
    public float tiempoEntreLanzamientos = 0.4f;

    private float proximoLanzamiento = 0f;
    private float umbralY = 0.15f;
    private bool proyectilUsado = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Lanzar()
    {
        if (proyectilUsado)
            return;
        if (Time.time < proximoLanzamiento)
            return;

        Transform objetivo = BuscarEnemigoAlineado();
                  if (objetivo == null)
                  {
                    return;
                  }

        if (proyectilPrefab != null && puntoDisparo != null)
        {
        GameObject bala = Instantiate(proyectilPrefab, puntoDisparo.position, puntoDisparo.rotation);

        ProyectilJugador pj = bala.GetComponent<ProyectilJugador>();
          if (pj != null)
          {
            pj.direccion = (objetivo.position.x < transform.position.x) ? Vector2.left : Vector2.right; // NUEVO
          }

        }
        proximoLanzamiento = Time.time + tiempoEntreLanzamientos;
        proyectilUsado = true;
    }
    private Transform BuscarEnemigoAlineado()
    {
        SaludEnemigo[] enemigos = FindObjectsOfType<SaludEnemigo>();

        Transform objetivo = null;
        float mejorDistancia = Mathf.Infinity;

        foreach (SaludEnemigo e in enemigos)
        {
            float dy = Mathf.Abs(transform.position.y - e.transform.position.y);
                  if (dy <= umbralY)
                  {
                    float dx = Mathf.Abs(transform.position.x - e.transform.position.x);
                          if (dx < mejorDistancia)
                          {
                            mejorDistancia = dx;
                            objetivo = e.transform;
                          }
                  }
        }
        return objetivo;
    }
}