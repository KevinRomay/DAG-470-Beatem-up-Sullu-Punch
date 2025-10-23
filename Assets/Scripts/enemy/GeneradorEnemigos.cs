using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorEnemigos : MonoBehaviour
{
    // --- 1. El Molde ---
    // Aqu� le diremos a Unity qu� "molde" (Prefab) queremos usar.
    public GameObject prefabDelEnemigo;

    // --- 2. D�nde ---
    // Un simple punto para saber d�nde crear al enemigo.
    public Transform puntoDeAparicion;

    // --- 3. Cu�ndo ---
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
        Debug.Log("�Generando un enemigo!");

        // --- 4. El Comando M�gico ---
        // Esta l�nea le dice a Unity:
        // "Usa el molde (prefabDelEnemigo),
        // cr�alo en esta posici�n (puntoDeAparicion.position),
        // y sin ninguna rotaci�n especial (Quaternion.identity)."
        Instantiate(prefabDelEnemigo, puntoDeAparicion.position, Quaternion.identity);
    }
}
