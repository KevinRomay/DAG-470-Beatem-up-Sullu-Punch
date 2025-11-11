using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class GeneradorPorTrigger : MonoBehaviour
{
    [Header("Configuración de Spawneo")]
    [SerializeField] public GameObject[] prefabsEnemigos;
    [SerializeField] public int cantidadEnemigos;
    [SerializeField] public Transform[] puntodeSpawn;
    [SerializeField] public float retrasoEntreSpawn = 0.5f;

    [Header("Referencias Globales")]
    // Arrastra tu GameObject del Jugador aquí
    public Transform jugador;

    [Header("Bloqueo de Cámara (Cinemachine)")]
    [SerializeField] GameObject camaraConfiner;
    
    private bool yaSeHaActivado = false;
    private List<GameObject> enemigosVivos = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        if(camaraConfiner != null)
        {
            camaraConfiner.SetActive(false);
        }

        if (jugador == null)
        {
            Debug.LogError("¡ERROR! El GeneradorPorTrigger no tiene una referencia al Jugador.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
 
        if(other.CompareTag("Player") && !yaSeHaActivado)
        {
            yaSeHaActivado = true;
            Debug.Log("Jugador entró en el área de generación de enemigos.");

            if (camaraConfiner != null)
            {
                camaraConfiner.SetActive(true);
            }
            StartCoroutine(RutinaDeSpawneo());
            StartCoroutine(ChequearFinDeCombate());
        }   
    }
    private IEnumerator RutinaDeSpawneo()
    {
        for (int i = 0; i < cantidadEnemigos; i++)
        {
            // Elegir un prefab aleatorio
            GameObject prefabElegido = prefabsEnemigos[Random.Range(0, prefabsEnemigos.Length)];
            // Elegir un punto de spawn aleatorio
            Transform puntoElegido = puntodeSpawn[Random.Range(0, puntodeSpawn.Length)];
            // Instanciar el enemigo
            GameObject nuevoEnemigo = Instantiate(prefabElegido, puntoElegido.position, Quaternion.identity);

            // Le decimos al nuevo enemigo quién es el jugador.
            if (nuevoEnemigo != null)
            {
                nuevoEnemigo.GetComponent<ControladorEnemigo>().jugador = this.jugador;
                nuevoEnemigo.GetComponent<AtaqueEnemigo>().jugador = this.jugador;
                nuevoEnemigo.GetComponent<DetectarJugador>().jugador = this.jugador;
            }
            enemigosVivos.Add(nuevoEnemigo);
            // Esperar antes de spawnear el siguiente
            yield return new WaitForSeconds(retrasoEntreSpawn);
        }
    }
    private IEnumerator ChequearFinDeCombate()
    {
        while (true)
        {
            enemigosVivos.RemoveAll(enemigo => enemigo == null);

            if (enemigosVivos.Count == 0)
            {
                break;
            }
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("Pelea Terminada. Desbloqueando Camara");
        if (camaraConfiner != null)
        {
            camaraConfiner.SetActive(false);
        }
        Destroy(this.gameObject);
    }
}

