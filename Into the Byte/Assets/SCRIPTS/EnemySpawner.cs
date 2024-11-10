using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;              // Array of enemy prefabs to spawn
    public InfiniteBackground infiniteBackground;  // Reference to the InfiniteBackground script
    public float spawnOffsetY = 1f;                // Offset for the enemy position above the platform

    private GameObject lastSpawnedPlatform;        // Track the last platform where enemies were spawned

    void Update()
    {
        SpawnEnemyOnPlatformEdge();
    }

    void SpawnEnemyOnPlatformEdge()
    {
        // Get the current platform in InfiniteBackground script
        GameObject currentPlatform = infiniteBackground.GetCurrentPlatform();

        // Ensure we have a valid platform and it is different from the last platform we checked
        if (currentPlatform != null && currentPlatform != lastSpawnedPlatform)
        {
            // Calculate the right edge position of the platform
            float platformWidth = currentPlatform.GetComponent<SpriteRenderer>().bounds.size.x;
            Vector3 spawnPosition = currentPlatform.transform.position + new Vector3(platformWidth / 2, spawnOffsetY, 0);

            // Spawn a random enemy at the right edge of the platform
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // Update the lastSpawnedPlatform to avoid spawning multiple enemies on the same platform
            lastSpawnedPlatform = currentPlatform;
        }
    }
}

