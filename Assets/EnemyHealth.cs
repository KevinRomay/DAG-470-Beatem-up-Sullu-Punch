using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag.Equals("da�oPersonaje"))
        {
            ControlSFX.Instance.ReproducirSonido("grito1");
        }
    }
}
