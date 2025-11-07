using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBuffSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs;  // Prefabs de los ítems
    public float spawnInterval = 5f;  // Cada cuánto aparecen
    public float spawnRadius = 5f;    // Radio donde aparecen
    public int maxItems = 5;

    private float timer;
    private int itemsSpawned= 0;

    void Update()
    {
        if (itemsSpawned >= maxItems) return; // Ya se generaron todos los ítems permitidos

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnItem();
        }

    }

    void SpawnItem()
    {
        if (GameObject.FindGameObjectsWithTag("ItemBuff").Length >= maxItems) return;

        Vector2 randomPos = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
        GameObject itemToSpawn = itemPrefabs[Random.Range(0, itemPrefabs.Length)];

        Instantiate(itemToSpawn, randomPos, Quaternion.identity);
        itemsSpawned++; // Aumenta el conteo de ítems generados

    }
}
