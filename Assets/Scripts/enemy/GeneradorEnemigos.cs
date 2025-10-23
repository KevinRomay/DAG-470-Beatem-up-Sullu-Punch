using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorEnemigos : MonoBehaviour
{
    // --- 1. El Molde ---
    // Aquí le diremos a Unity qué "molde" (Prefab) queremos usar.
    public GameObject prefabDelEnemigo;

    // --- 2. Dónde ---
    // Un simple punto para saber dónde crear al enemigo.
    public Transform puntoDeAparicion;

    // --- 3. Cuándo ---
    // Usaremos la tecla 'G' para probarlo.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GenerarEnemigo();
        }
    }

    void GenerarEnemigo()
    {
        Debug.Log("¡Generando un enemigo!");

        // --- 4. El Comando Mágico ---
        // Esta línea le dice a Unity:
        // "Usa el molde (prefabDelEnemigo),
        // créalo en esta posición (puntoDeAparicion.position),
        // y sin ninguna rotación especial (Quaternion.identity)."
        Instantiate(prefabDelEnemigo, puntoDeAparicion.position, Quaternion.identity);
    }
}
