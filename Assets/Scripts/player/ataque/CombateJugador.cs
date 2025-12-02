using System.Collections.Generic;
using UnityEngine;

public class CombateJugador : MonoBehaviour
{
    public float daño = 10f;
    public float tiempoEntreAtaques = 0.4f;

    private float proximoAtaquePermitido = 0f;
    public Collider2D zonaAtaque;  
    private List<SaludEnemigo>enemigosDetectados = new List <SaludEnemigo>();

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

        enemigosDetectados.Clear();

        StartCoroutine(ActivarZonaAtaque());
    }

    private System.Collections.IEnumerator ActivarZonaAtaque()
    {
        zonaAtaque.enabled = true;
        yield return new WaitForSeconds(0.1f);  // pequeño tiempo donde el golpe existe
        zonaAtaque.enabled = false;
        GolpearUnSoloEnemigo();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!zonaAtaque.enabled)
            return;

        SaludEnemigo enemigo = other.GetComponent<SaludEnemigo>();

        if (enemigo != null)
        {
            enemigo.RecibirDaño(daño);
            Debug.Log("Golpeaste: " + other.name);
            enemigosDetectados.Add(enemigo);
        }
    }
    private void GolpearUnSoloEnemigo()
    {
        if (enemigosDetectados.Count == 0)
        return;
        SaludEnemigo enemigoFinal = null;
        float mejorDistX = Mathf.Infinity;
        foreach (SaludEnemigo e in enemigosDetectados)
        {
            float dx = Mathf.Abs(transform.position.x - e.transform.position.x);
            if (dx < mejorDistX)
            {
                mejorDistX = dx;
                enemigoFinal = e;
            }
        }
        if (enemigoFinal != null)
        {
            enemigoFinal.RecibirDaño(daño);
            Debug.Log("Golpeaste a: " + enemigoFinal.name);
        }
    }
        public void Patear()
    {
        if (Time.time < proximoAtaquePermitido)
        return;

    proximoAtaquePermitido = Time.time + tiempoEntreAtaques;

    float dañoOriginal = daño;
    daño = daño * 1.5f;
    enemigosDetectados.Clear();
    StartCoroutine(ActivarZonaAtaque());

    daño = dañoOriginal; 
    }
}