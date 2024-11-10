using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//public class InfiniteBackground : MonoBehaviour
//{
//    public Transform player;                        // Reference to the player
//    public GameObject[] platformPrefabs;            // Array of platform prefabs
//    public Vector3[] spawnPositions;                // Array of custom spawn positions
//    private int currentPlatformIndex = 0;           // Tracks the current platform in spawnPositions
//    private GameObject currentPlatform;             // Holds reference to the active platform

//    void Start()
//    {
//        // Ensure there are platform prefabs and spawn positions
//        if (platformPrefabs.Length == 0 || spawnPositions.Length == 0)
//        {
//            Debug.LogError("Please assign platform prefabs and spawn positions.");
//            return;
//        }

//        // Instantiate the initial platform at the first spawn position
//        currentPlatform = Instantiate(platformPrefabs[Random.Range(0, platformPrefabs.Length)]);
//        currentPlatform.transform.position = spawnPositions[currentPlatformIndex];
//    }

//    void Update()
//    {
//        // Check if the player has reached the spawn trigger point for the next platform
//        if (currentPlatformIndex < spawnPositions.Length - 1 &&
//            player.position.x > spawnPositions[currentPlatformIndex].x)
//        {
//            // Destroy the old platform, instantiate and position a new one at the next spawn position
//            Destroy(currentPlatform);
//            currentPlatformIndex++;
//            currentPlatform = Instantiate(platformPrefabs[Random.Range(0, platformPrefabs.Length)]);
//            currentPlatform.transform.position = spawnPositions[currentPlatformIndex];
//        }
//    }
//}
//working script
public class InfiniteBackground : MonoBehaviour
{
    public Transform player;                        // Reference to the player
    public GameObject[] platformPrefabs;            // Array of platform prefabs
    private GameObject[] activePlatforms;           // Array to hold active platform instances
    private float platformWidth;                    // Width of each platform
    private int currentPlatformIndex = 0;           // Index of the current platform
    private float spawnBuffer =2;                      // Distance buffer for early spawning

    void Start()
    {
        // Ensure there are platform prefabs
        if (platformPrefabs.Length == 0)
        {
            Debug.LogError("Please assign platform prefabs in the platformPrefabs array.");
            return;
        }

        // Initialize the active platforms array
        activePlatforms = new GameObject[2];

        // Instantiate only the first platform at the start
        activePlatforms[currentPlatformIndex] = Instantiate(platformPrefabs[Random.Range(0, platformPrefabs.Length)]);
        activePlatforms[currentPlatformIndex].transform.position = Vector3.zero;  // Set initial position to zero

        // Calculate the width of a platform based on the bounds of the first one
        platformWidth = activePlatforms[currentPlatformIndex].GetComponent<SpriteRenderer>().bounds.size.x;

        // Set the spawn buffer as a fraction of the platform width (e.g., half the width)
        spawnBuffer = platformWidth * 0.5f;
    }

    void Update()
    {
        // Reference to the current platform
        GameObject currentPlatform = activePlatforms[currentPlatformIndex];

        // Check if the player is approaching the edge of the current platform (with buffer)
        if (player.position.x > currentPlatform.transform.position.x + platformWidth - spawnBuffer)
        {
            // Get the next platform index
            int nextPlatformIndex = (currentPlatformIndex + 1) % activePlatforms.Length;

            // Destroy the old platform, then instantiate and position a new one to the right of the current platform
            if (activePlatforms[nextPlatformIndex] != null)
            {
                Destroy(activePlatforms[nextPlatformIndex]);
            }
            activePlatforms[nextPlatformIndex] = Instantiate(platformPrefabs[Random.Range(0, platformPrefabs.Length)]);
            activePlatforms[nextPlatformIndex].transform.position = currentPlatform.transform.position + Vector3.right * platformWidth;

            // Update the platform index
            currentPlatformIndex = nextPlatformIndex;
        }
        else if (player.position.x < currentPlatform.transform.position.x - platformWidth + spawnBuffer)
        {
            // Get the next platform index
            int nextPlatformIndex = (currentPlatformIndex + 1) % activePlatforms.Length;

            // Destroy the old platform, then instantiate and position a new one to the left of the current platform
            if (activePlatforms[nextPlatformIndex] != null)
            {
                Destroy(activePlatforms[nextPlatformIndex]);
            }
            activePlatforms[nextPlatformIndex] = Instantiate(platformPrefabs[Random.Range(0, platformPrefabs.Length)]);
            activePlatforms[nextPlatformIndex].transform.position = currentPlatform.transform.position - Vector3.right * platformWidth;

            // Update the platform index
            currentPlatformIndex = nextPlatformIndex;
        }
    }

}

//original script
//public class InfiniteBackground : MonoBehaviour
//{
//    public Transform player;              // Reference to the player
//    public SpriteRenderer Platform;    // First background sprite
//    public SpriteRenderer Platform2;    // Second background sprite

//    private float platformWidth;        // Width of each background sprite

//    void Start()
//    {
//        // Calculate the width of the background based on its bounds
//        platformWidth = Platform.bounds.size.x;

//        // Position the second background to the right of the first background
//        Platform2.transform.position = Platform.transform.position + Vector3.right * platformWidth;
//    }

//    void Update()
//    {
//        // Check if the player has moved past the edge of background1
//        if (player.position.x > Platform.transform.position.x + platformWidth)
//        {
//            // Move background1 to the right of background2
//            Platform.transform.position = Platform2.transform.position + Vector3.right * platformWidth;

//            // Swap the backgrounds
//            SwapBackgrounds();
//        }
//        else if (player.position.x < Platform.transform.position.x - platformWidth)
//        {
//            // Move background1 to the left of background2
//            Platform.transform.position = Platform2.transform.position - Vector3.right * platformWidth;

//            // Swap the backgrounds
//            SwapBackgrounds();
//        }
//    }

//    void SwapBackgrounds()
//    {
//        // Swap background1 and background2 references
//        var temp = Platform;
//        Platform = Platform2;
//        Platform2 = temp;
//    }
//}
