using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    public Transform player;                        // Reference to the player
    public GameObject[] platformPrefabs;            // Array of platform prefabs
    public GameObject[] backgroundPrefabs;          // Array of background prefabs
    private GameObject[] activePlatforms;           // Array to hold active platform instances
    private GameObject[] activeBackgrounds;         // Array to hold active background instances
    [Header("Distance Calculator")]
    private float platformWidth;                    // Width of each platform
    private float backgroundWidth;                  // Width of each background
    private int currentPlatformIndex = 0;           // Index of the current platform
    private int currentBackgroundIndex = 0;         // Index of the current background
    public float spawnBuffer;                      // Distance buffer for early spawning
    public float backgroundHeightOffset = 4f;       // Fixed height above the platform
    public float maxHeight = 4f;                    // Maximum height for the background (fixed)
    public GameObject GetCurrentPlatform()
    {
        return activePlatforms[currentPlatformIndex];
    }

    void Start()
    {
        // Ensure there are platform and background prefabs
        if (platformPrefabs.Length == 0 || backgroundPrefabs.Length == 0)
        {
            Debug.LogError("Please assign both platform and background prefabs.");
            return;
        }

        // Initialize the active platforms and backgrounds arrays
        activePlatforms = new GameObject[2];
        activeBackgrounds = new GameObject[2];

        // Instantiate the first platform at the start
        activePlatforms[currentPlatformIndex] = Instantiate(platformPrefabs[Random.Range(0, platformPrefabs.Length)]);
        activePlatforms[currentPlatformIndex].transform.position = Vector3.zero;  // Set initial position to zero

        // Calculate the width of a platform based on the bounds of the first one
        platformWidth = activePlatforms[currentPlatformIndex].GetComponent<SpriteRenderer>().bounds.size.x;
        spawnBuffer = platformWidth;

        // Instantiate the first background at the start with a fixed height
        activeBackgrounds[currentBackgroundIndex] = Instantiate(backgroundPrefabs[Random.Range(0, backgroundPrefabs.Length)]);
        activeBackgrounds[currentBackgroundIndex].transform.position = new Vector3(0, backgroundHeightOffset, 0);

        // Calculate the width of a background based on the bounds of the first one
        backgroundWidth = activeBackgrounds[currentBackgroundIndex].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // Platform spawning logic
        SpawnPlatform();

        // Background spawning logic
        SpawnBackground();
    }

    void SpawnPlatform()
    {
        // Reference to the current platform
        GameObject currentPlatform = activePlatforms[currentPlatformIndex];

        // Check if the player is approaching the edge of the current platform (with buffer)
        if (player.position.x > currentPlatform.transform.position.x + platformWidth - spawnBuffer)
        {
            int nextPlatformIndex = (currentPlatformIndex + 1) % activePlatforms.Length;
            if (activePlatforms[nextPlatformIndex] != null)
            {
                Destroy(activePlatforms[nextPlatformIndex]);
            }
            activePlatforms[nextPlatformIndex] = Instantiate(platformPrefabs[Random.Range(0, platformPrefabs.Length)]);
            activePlatforms[nextPlatformIndex].transform.position = currentPlatform.transform.position + Vector3.right * platformWidth;
            currentPlatformIndex = nextPlatformIndex;
        }
        else if (player.position.x < currentPlatform.transform.position.x - platformWidth + spawnBuffer)
        {
            int nextPlatformIndex = (currentPlatformIndex + 1) % activePlatforms.Length;
            if (activePlatforms[nextPlatformIndex] != null)
            {
                Destroy(activePlatforms[nextPlatformIndex]);
            }
            activePlatforms[nextPlatformIndex] = Instantiate(platformPrefabs[Random.Range(0, platformPrefabs.Length)]);
            activePlatforms[nextPlatformIndex].transform.position = currentPlatform.transform.position - Vector3.right * platformWidth;
            currentPlatformIndex = nextPlatformIndex;
        }
    }

    void SpawnBackground()
    {
        // Reference to the current background
        GameObject currentBackground = activeBackgrounds[currentBackgroundIndex];

        // Check if the player has completely moved past the current background's edge
        if (player.position.x > currentBackground.transform.position.x + backgroundWidth)
        {
            int nextBackgroundIndex = (currentBackgroundIndex + 1) % activeBackgrounds.Length;
            if (activeBackgrounds[nextBackgroundIndex] != null)
            {
                Destroy(activeBackgrounds[nextBackgroundIndex]);
            }
            activeBackgrounds[nextBackgroundIndex] = Instantiate(backgroundPrefabs[Random.Range(0, backgroundPrefabs.Length)]);

            // Set the new background position with a fixed Y position
            activeBackgrounds[nextBackgroundIndex].transform.position = new Vector3(currentBackground.transform.position.x + backgroundWidth, backgroundHeightOffset, 0);

            currentBackgroundIndex = nextBackgroundIndex;
        }
        else if (player.position.x < currentBackground.transform.position.x - backgroundWidth)
        {
            int nextBackgroundIndex = (currentBackgroundIndex + 1) % activeBackgrounds.Length;
            if (activeBackgrounds[nextBackgroundIndex] != null)
            {
                Destroy(activeBackgrounds[nextBackgroundIndex]);
            }
            activeBackgrounds[nextBackgroundIndex] = Instantiate(backgroundPrefabs[Random.Range(0, backgroundPrefabs.Length)]);

            // Set the new background position with a fixed Y position
            activeBackgrounds[nextBackgroundIndex].transform.position = new Vector3(currentBackground.transform.position.x - backgroundWidth, backgroundHeightOffset, 0);

            currentBackgroundIndex = nextBackgroundIndex;
        }
    }
}


//working script
//public class InfiniteBackground : MonoBehaviour
//{
//    public Transform player;                        // Reference to the player
//    public GameObject[] platformPrefabs;            // Array of platform prefabs
//    private GameObject[] activePlatforms;           // Array to hold active platform instances
//    private float platformWidth;                    // Width of each platform
//    private int currentPlatformIndex = 0;           // Index of the current platform
//    private float spawnBuffer =2;                      // Distance buffer for early spawning

//    void Start()
//    {
//        // Ensure there are platform prefabs
//        if (platformPrefabs.Length == 0)
//        {
//            Debug.LogError("Please assign platform prefabs in the platformPrefabs array.");
//            return;
//        }

//        // Initialize the active platforms array
//        activePlatforms = new GameObject[2];

//        // Instantiate only the first platform at the start
//        activePlatforms[currentPlatformIndex] = Instantiate(platformPrefabs[Random.Range(0, platformPrefabs.Length)]);
//        activePlatforms[currentPlatformIndex].transform.position = Vector3.zero;  // Set initial position to zero

//        // Calculate the width of a platform based on the bounds of the first one
//        platformWidth = activePlatforms[currentPlatformIndex].GetComponent<SpriteRenderer>().bounds.size.x;

//        // Set the spawn buffer as a fraction of the platform width (e.g., half the width)
//        spawnBuffer = platformWidth * 0.5f;
//    }

//    void Update()
//    {
//        // Reference to the current platform
//        GameObject currentPlatform = activePlatforms[currentPlatformIndex];

//        // Check if the player is approaching the edge of the current platform (with buffer)
//        if (player.position.x > currentPlatform.transform.position.x + platformWidth - spawnBuffer)
//        {
//            // Get the next platform index
//            int nextPlatformIndex = (currentPlatformIndex + 1) % activePlatforms.Length;

//            // Destroy the old platform, then instantiate and position a new one to the right of the current platform
//            if (activePlatforms[nextPlatformIndex] != null)
//            {
//                Destroy(activePlatforms[nextPlatformIndex]);
//            }
//            activePlatforms[nextPlatformIndex] = Instantiate(platformPrefabs[Random.Range(0, platformPrefabs.Length)]);
//            activePlatforms[nextPlatformIndex].transform.position = currentPlatform.transform.position + Vector3.right * platformWidth;

//            // Update the platform index
//            currentPlatformIndex = nextPlatformIndex;
//        }
//        else if (player.position.x < currentPlatform.transform.position.x - platformWidth + spawnBuffer)
//        {
//            // Get the next platform index
//            int nextPlatformIndex = (currentPlatformIndex + 1) % activePlatforms.Length;

//            // Destroy the old platform, then instantiate and position a new one to the left of the current platform
//            if (activePlatforms[nextPlatformIndex] != null)
//            {
//                Destroy(activePlatforms[nextPlatformIndex]);
//            }
//            activePlatforms[nextPlatformIndex] = Instantiate(platformPrefabs[Random.Range(0, platformPrefabs.Length)]);
//            activePlatforms[nextPlatformIndex].transform.position = currentPlatform.transform.position - Vector3.right * platformWidth;

//            // Update the platform index
//            currentPlatformIndex = nextPlatformIndex;
//        }
//    }

//}

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
