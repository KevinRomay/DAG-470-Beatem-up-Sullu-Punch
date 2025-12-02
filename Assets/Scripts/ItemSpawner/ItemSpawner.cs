using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    // Array para almacenar todos los Prefabs de ítems posibles
    [Tooltip("Arrastra todos los Prefabs de ítems que pueden aparecer aquí.")]
    public GameObject[] possibleItemPrefabs;

    // Opcional: Para evitar que el mismo ítem se repita dos veces seguidas
    private int lastSpawnedIndex = -1;

    // Este es el método que llamará tu NPC
    public void SpawnItem()
    {
        if (possibleItemPrefabs == null || possibleItemPrefabs.Length == 0)
        {
            Debug.LogError("¡No hay Prefabs de ítems asignados en el Spawner!");
            return;
        }

        int randomIndex = GetRandomUniqueIndex();
        GameObject selectedPrefab = possibleItemPrefabs[randomIndex];
        Vector3 spawnPosition = transform.position;

        // Instanciar (crear) el ítem en la escena
        GameObject spawnedItem = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

        Debug.Log("Ítem aleatorio '" + selectedPrefab.name + "' spwneado en: " + spawnPosition);
    }

    private int GetRandomUniqueIndex()
    {
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, possibleItemPrefabs.Length);
        }
        while (randomIndex == lastSpawnedIndex && possibleItemPrefabs.Length > 1);

        lastSpawnedIndex = randomIndex;
        return randomIndex;
    }
}
