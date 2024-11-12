using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ParallaxController : MonoBehaviour
{
    public Transform player;                        // Reference to the player
    public GameObject[] platformPrefabs;            // Array of platform prefabs
    public GameObject[] backgroundPrefabs;          // Array of background prefabs
    private GameObject[] activePlatforms;           // Array to hold active platform instances
    private GameObject[] activeBackgrounds;         // Array to hold active background instances
    [Header("Distance Calculation")]
    public float platformWidth;                    // Width of each platform
    private float backgroundWidth;                  // Width of each background
    private int currentPlatformIndex = 0;           // Index of the current platform
    private int currentBackgroundIndex = 0;         // Index of the current background
    public float spawnBuffer = 4f;                  // Adjustable distance buffer for early spawning
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

        // Check if the player is approaching the right edge of the current platform (with buffer)
        if (player.position.x > currentPlatform.transform.position.x + platformWidth - spawnBuffer)
        {
            //Debug.Log("Player approaching the right edge of the platform. Spawning new platform.");

            int nextPlatformIndex = (currentPlatformIndex + 1) % activePlatforms.Length;
            if (activePlatforms[nextPlatformIndex] != null)
            {
                Destroy(activePlatforms[nextPlatformIndex]);
            }
            activePlatforms[nextPlatformIndex] = Instantiate(platformPrefabs[Random.Range(0, platformPrefabs.Length)]);
            activePlatforms[nextPlatformIndex].transform.position = currentPlatform.transform.position + Vector3.right * platformWidth;
            currentPlatformIndex = nextPlatformIndex;
        }
        // Check if the player is approaching the left edge of the current platform (with buffer)
        else if (player.position.x < currentPlatform.transform.position.x - platformWidth + spawnBuffer)
        {
           // Debug.Log("Player approaching the left edge of the platform. Spawning new platform.");

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
//public class ParallaxController : MonoBehaviour
//{
//    // Array of parallax layers
//    public Transform[] layers;

//    // The speed at which the layers move (based on distance from camera)
//    public float[] parallaxSpeeds;

//    private Vector3 lastCameraPosition;
//    private Camera cam;

//    void Start()
//    {
//        cam = Camera.main;
//        lastCameraPosition = cam.transform.position;
//    }

//    void Update()
//    {
//        // Calculate the camera movement delta
//        Vector3 deltaMovement = cam.transform.position - lastCameraPosition;

//        // Move each layer based on the speed and the delta movement
//        for (int i = 0; i < layers.Length; i++)
//        {
//            float parallax = deltaMovement.x * parallaxSpeeds[i];
//            layers[i].position += new Vector3(parallax, 0, 0);
//        }

//        // Update last camera position for the next frame
//        lastCameraPosition = cam.transform.position;
//    }
//}


