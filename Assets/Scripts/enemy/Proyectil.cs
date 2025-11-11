using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public float velocidad = 5f;
    public float daño = 10f;
    public float vidaUtil = 3f;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();


        rb.velocity = transform.right * velocidad;


        Destroy(gameObject, vidaUtil);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth saludJugador = other.GetComponent<PlayerHealth>();


            if(saludJugador != null)
            {
                saludJugador.RecibirDaño(daño);
            }

            Destroy(gameObject);
        }
        else if (other.CompareTag("Escenario"))
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
