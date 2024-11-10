using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;              // Array of enemy prefabs to spawn
    public InfiniteBackground infiniteBackground;  // Reference to the InfiniteBackground script
    public float spawnOffsetY = 1f;                // Offset for the enemy position above the platform
    public float spawnInterval = 2f;               // Interval between enemy spawns
    public int maxEnemies;                     // Maximum number of enemies that can be active at once

    private GameObject lastSpawnedPlatform;        // Track the last platform where enemies were spawned
    private float timeSinceLastSpawn = 0f;         // Time elapsed since the last enemy spawn
    public List<GameObject> activeEnemies = new List<GameObject>(); // List to keep track of active enemies

    void Update()
    {
        // Update the time since the last spawn
        timeSinceLastSpawn += Time.deltaTime;

        // Check if we are allowed to spawn (within spawn interval and enemy limit not exceeded)
        if (timeSinceLastSpawn >= spawnInterval && activeEnemies.Count < maxEnemies)
        {
            SpawnEnemyOnPlatformEdge();
            timeSinceLastSpawn = 0f; // Reset the timer after spawning an enemy
        }

        // Remove any null entries from the list (for enemies that have been destroyed)
        activeEnemies.RemoveAll(enemy => enemy == null);
    }

    void SpawnEnemyOnPlatformEdge()
    {
        // Make sure the InfiniteBackground instance is assigned and accessible
        if (infiniteBackground == null)
        {
            Debug.LogError("InfiniteBackground reference not set in EnemySpawner.");
            return;
        }

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
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // Add the newly spawned enemy to the activeEnemies list
            activeEnemies.Add(newEnemy);

            // Update the lastSpawnedPlatform to avoid spawning multiple enemies on the same platform
            lastSpawnedPlatform = currentPlatform;
        }
    }
}

