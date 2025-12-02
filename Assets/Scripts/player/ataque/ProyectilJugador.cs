using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilJugador : MonoBehaviour
{
    public float velocidad = 10f;
<<<<<<< Updated upstream
    public float daño = 10f;
    public float vidaUtil = 3f;
=======
    public float daño = 50f;
    public float vidaUtil = 1f;
>>>>>>> Stashed changes

    private Rigidbody2D rb;
    [HideInInspector]
    public Vector2 direccion = Vector2.right;

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = direccion * velocidad;

        Destroy(gameObject, vidaUtil);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     private void OnTriggerEnter2D(Collider2D other)
    {
        SaludEnemigo enemigo = other.GetComponent<SaludEnemigo>();
        if (enemigo != null)
        {
            enemigo.RecibirDaño(daño);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Escenario"))
        {
            Destroy(gameObject);
        }
    }
<<<<<<< Updated upstream
}
=======
}
>>>>>>> Stashed changes
