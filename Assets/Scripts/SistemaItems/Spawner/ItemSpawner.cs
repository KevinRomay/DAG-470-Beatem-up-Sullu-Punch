using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs;  // Prefabs de los ítems
    public float spawnDelay = 5f;     // Tiempo antes del primer spawn
    public float spawnRadius = 5f;    // Radio donde aparecerá
    public int maxItems = 1;          // Máximo de ítems activos

    private bool hasSpawned = false;

    void Start()
    {
        StartCoroutine(SpawnItemWithDelay());
    }

    IEnumerator SpawnItemWithDelay()
    {
        yield return new WaitForSeconds(spawnDelay);
        SpawnItem();
    }

    void SpawnItem()
    {
        if (hasSpawned) return;

        Vector2 randomPos = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
        GameObject itemToSpawn = itemPrefabs[Random.Range(0, itemPrefabs.Length)];

        Instantiate(itemToSpawn, randomPos, Quaternion.identity);
        hasSpawned = true;
    }
}
