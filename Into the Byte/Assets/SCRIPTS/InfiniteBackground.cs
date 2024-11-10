using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//original script
public class InfiniteBackground : MonoBehaviour
{
    public Transform player;                  // Reference to the player
    public SpriteRenderer platformPrefab;     // Prefab for spawning platforms
    public int initialPlatformCount = 3;      // Number of platforms to spawn initially

    private SpriteRenderer[] platforms;       // Array to store platform instances
    private float platformWidth;              // Width of each platform

    void Start()
    {
        // Calculate the width of a single platform based on the prefab's bounds
        platformWidth = platformPrefab.bounds.size.x;

        // Initialize the platform array and spawn the initial platforms
        platforms = new SpriteRenderer[initialPlatformCount];
        for (int i = 0; i < initialPlatformCount; i++)
        {
            platforms[i] = Instantiate(platformPrefab, transform);
            platforms[i].transform.position = Vector3.right * platformWidth * i;
        }
    }

    void Update()
    {
        // Check each platform and reposition it if the player has moved past its edge
        for (int i = 0; i < platforms.Length; i++)
        {
            float rightEdgePosition = platforms[i].transform.position.x + platformWidth;

            // If the player has moved past the right edge of the platform
            if (player.position.x > rightEdgePosition)
            {
                // Find the rightmost platform
                SpriteRenderer rightmostPlatform = FindRightmostPlatform();

                // Move the current platform to the right of the rightmost platform
                platforms[i].transform.position = rightmostPlatform.transform.position + Vector3.right * platformWidth;
            }
        }
    }

    SpriteRenderer FindRightmostPlatform()
    {
        SpriteRenderer rightmost = platforms[0];
        for (int i = 1; i < platforms.Length; i++)
        {
            if (platforms[i].transform.position.x > rightmost.transform.position.x)
            {
                rightmost = platforms[i];
            }
        }
        return rightmost;
    }
}
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
