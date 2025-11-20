using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PantallaCargaController : MonoBehaviour
{
    public string escenaDestino = "PruebaTrancisiones"; // Nombre exacto de la escena siguiente
    public float tiempoEspera = 5f; // segundos

    void Start()
    {
        StartCoroutine(CargarDespuesDeTiempo());
    }

    IEnumerator CargarDespuesDeTiempo()
    {
        yield return new WaitForSeconds(tiempoEspera);
        SceneManager.LoadScene(escenaDestino);
    }
}

