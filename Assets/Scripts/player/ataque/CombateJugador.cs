using UnityEngine;

public class CombateJugador : MonoBehaviour
{
    public float daño = 10f;
    public float tiempoEntreAtaques = 0.4f;

    private float proximoAtaquePermitido = 0f;
    public Collider2D zonaAtaque;  // arrastra el BoxCollider2D del hijo aquí

    private void Start()
    {
        if (zonaAtaque == null)
           Debug.LogError("Zona de ataque NO está asignada en el Inspector!");
        else
           zonaAtaque.enabled = false; 
    }

    public void Atacar()
    {
        if (Time.time < proximoAtaquePermitido)
            return;

        proximoAtaquePermitido = Time.time + tiempoEntreAtaques;

        StartCoroutine(ActivarZonaAtaque());
    }

    private System.Collections.IEnumerator ActivarZonaAtaque()
    {
        zonaAtaque.enabled = true;
        yield return new WaitForSeconds(0.1f);  // pequeño tiempo donde el golpe existe
        zonaAtaque.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!zonaAtaque.enabled)
            return;
        float diferenciaY = Mathf.Abs(transform.position.y - other.transform.position.y);
            if (diferenciaY > 0.15f)
            return;    

        SaludEnemigo enemigo = other.GetComponent<SaludEnemigo>();

        if (enemigo != null)
        {
            enemigo.RecibirDaño(daño);
            Debug.Log("Golpeaste: " + other.name);
        }
    }
}