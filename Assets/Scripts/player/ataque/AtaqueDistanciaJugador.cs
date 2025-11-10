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
        if (Time.time < proximoLanzamiento)
            return;

        proximoLanzamiento = Time.time + tiempoEntreLanzamientos;

        if (proyectilPrefab != null && puntoDisparo != null)
        {
            GameObject bala = Instantiate(proyectilPrefab, puntoDisparo.position, puntoDisparo.rotation);
            Rigidbody2D rb = bala.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direccion = transform.localScale.x < 0 ? Vector2.left : Vector2.right;
                rb.velocity = direccion * velocidadProyectil;
            }
        }
    }
}
