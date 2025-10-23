using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public TimerManager timerManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cura"))
        {
            float cantidadCura = playerHealth.vidaMax * 0.1f;
            playerHealth.RecibirDaño(-cantidadCura); // daño negativo = cura
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Tiempo"))
        {
            timerManager.AgregarTiempo(20f);
            Destroy(other.gameObject);
        }
    }
}


