using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateJugadorAD : MonoBehaviour
{
    public GameObject proyectilPrefab;
    public Transform puntoLanzamiento;
    public float velocidad = 10f;
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
        GameObject proj = Instantiate(proyectilPrefab, puntoLanzamiento.position, puntoLanzamiento.rotation);
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * velocidad;
    }
}
