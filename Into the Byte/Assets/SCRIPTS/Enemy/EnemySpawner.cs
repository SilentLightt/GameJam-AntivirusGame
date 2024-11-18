//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//public class EnemySpawner : MonoBehaviour
//{
//    public GameObject[] enemyPrefabs;              // Array of enemy prefabs to spawn
//    public InfiniteBackground infiniteBackground;  // Reference to the InfiniteBackground script
//    public float spawnOffsetY = 1f;                // Offset for the enemy position above the platform
//    public float spawnInterval = 2f;               // Interval between enemy spawns
//    public int maxEnemies;                     // Maximum number of enemies that can be active at once

//    private GameObject lastSpawnedPlatform;        // Track the last platform where enemies were spawned
//    private float timeSinceLastSpawn = 0f;         // Time elapsed since the last enemy spawn
//    public List<GameObject> activeEnemies = new List<GameObject>(); // List to keep track of active enemies

//    void Update()
//    {
//        // Update the time since the last spawn
//        timeSinceLastSpawn += Time.deltaTime;

//        // Check if we are allowed to spawn (within spawn interval and enemy limit not exceeded)
//        if (timeSinceLastSpawn >= spawnInterval && activeEnemies.Count < maxEnemies)
//        {
//            SpawnEnemyOnPlatformEdge();
//            timeSinceLastSpawn = 0f; // Reset the timer after spawning an enemy
//        }

//        // Remove any null entries from the list (for enemies that have been destroyed)
//        activeEnemies.RemoveAll(enemy => enemy == null);
//    }

//    void SpawnEnemyOnPlatformEdge()
//    {
//        // Make sure the InfiniteBackground instance is assigned and accessible
//        if (infiniteBackground == null)
//        {
//            Debug.LogError("InfiniteBackground reference not set in EnemySpawner.");
//            return;
//        }

//        // Get the current platform in InfiniteBackground script
//        GameObject currentPlatform = infiniteBackground.GetCurrentPlatform();

//        // Ensure we have a valid platform and it is different from the last platform we checked
//        if (currentPlatform != null && currentPlatform != lastSpawnedPlatform)
//        {
//            // Calculate the right edge position of the platform
//            float platformWidth = currentPlatform.GetComponent<SpriteRenderer>().bounds.size.x;
//            Vector3 spawnPosition = currentPlatform.transform.position + new Vector3(platformWidth / 2, spawnOffsetY, 0);

//            // Spawn a random enemy at the right edge of the platform
//            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
//            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

//            // Add the newly spawned enemy to the activeEnemies list
//            activeEnemies.Add(newEnemy);

//            // Update the lastSpawnedPlatform to avoid spawning multiple enemies on the same platform
//            lastSpawnedPlatform = currentPlatform;
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject[] enemies;
    [SerializeField] GameObject[] items;

    [Header("Wave Settings")]
    [SerializeField] int maxWaves = 10;
    [SerializeField] float spawnInterval = 3f;
    private float spawnTimer;
    [SerializeField] int currentWave = 1;
    [SerializeField] int enemiesPerWave = 3;
    [SerializeField] int enemiesRemainingToSpawn;
    [SerializeField] List<GameObject> activeEnemies = new List<GameObject>();

    private void Start()
    {
        spawnTimer = spawnInterval;
        enemiesRemainingToSpawn = enemiesPerWave;
        StartWave();
    }

    private void Update()
    {
        if (currentWave > maxWaves) return;

        spawnTimer -= Time.deltaTime;

        // Spawn enemies at intervals if we still have some to spawn
        if (spawnTimer <= 0 && enemiesRemainingToSpawn > 0)
        {
            SpawnEnemy();
            enemiesRemainingToSpawn--;
            spawnTimer = spawnInterval;
        }

        // Check if all active enemies have been defeated
        if (enemiesRemainingToSpawn == 0 && activeEnemies.Count == 0)
        {
            SpawnRandomItem();
            PrepareNextWave();
        }
    }

    private void SpawnEnemy()
    {
        if (spawnPoints.Length == 0 || enemies.Length == 0) return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemyPrefab = enemies[Random.Range(0, enemies.Length)];
        GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        activeEnemies.Add(spawnedEnemy);
    }

    private void SpawnRandomItem()
    {
        if (items.Length == 0) return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject itemPrefab = items[Random.Range(0, items.Length)];
        Instantiate(itemPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    private void PrepareNextWave()
    {
        if (currentWave < maxWaves)
        {
            currentWave++;
            enemiesPerWave++;
            enemiesRemainingToSpawn = enemiesPerWave;
            spawnTimer = spawnInterval; // Reset the spawn timer for the next wave
            Debug.Log("Wave " + currentWave + " starting with " + enemiesPerWave + " enemies.");
        }
        else
        {
            Debug.Log("All waves completed!");
        }
    }

    private void StartWave()
    {
        Debug.Log("Starting wave: " + currentWave);
    }

    public void EnemyDefeated(GameObject enemy)
    {
        if (activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy);
            Debug.Log("Enemy defeated and removed. Active enemies remaining: " + activeEnemies.Count);
        }
        else
        {
            Debug.LogWarning("Tried to remove an enemy that was not in the active list.");
        }
    }
}





