//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.UIElements;

//public class Scroller : MonoBehaviour
//{
//    public Transform cam; // Main Camera
//    public Vector3 camStartPos;
//    public float distance; // distance between the camera start position and its current position
//    public GameObject[] backgrounds;
//    public Material[] mat;
//    public float[] backSpeed;
//    public float farthestBack;
//    [Range(0.01f, 0.05f)]
//    public float parallaxSpeed;

//    void Start()
//    {
//        cam = Camera.main.transform; camStartPos = cam.position;
//        int backCount = transform.childCount; mat = new Material[backCount]; backSpeed = new float[backCount];
//        backSpeed = new float[backCount];
//        backgrounds = new GameObject[backCount];
//        for (int i = 0; i < backCount; i++) // find the farhthest background
//        {
//            backgrounds[i] = transform.GetChild(i).gameObject;
//            mat[i] = backgrounds[i].GetComponent<Renderer>().material;
//        }
//        BackSpeedCalculate(backCount);
//    }

//    void BackSpeedCalculate(int backCount)
//    {
//        for (int i = 0; i < backCount; i++) // find the farhthest background
//        {
//            if ((backgrounds[i].transform.position.z - cam.position.z) > farthestBack)
//            {
//                farthestBack = backgrounds[i].transform.position.z - cam.position.z;
//            }

//        }
//        for (int i = 0; i < backCount; i++) // set the speed of backgrounds
//        {
//            backSpeed[i] = 1 - (backgrounds[i].transform.position.z - cam.position.z) / farthestBack;
//        }

//    }
//    private void LateUpdate()
//    {
//        distance = cam.position.x - camStartPos.x;
//        transform.position = new Vector3( cam.position.x,transform.position.y,0);
//        for (int i = 0; i < backgrounds.Length; i++)
//        {
//            float speed = backSpeed[i] * parallaxSpeed;
//            mat[i].SetTextureOffset("_MainTex", new Vector2(distance, 0)*speed);
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    public Transform cam; // Main Camera
    public Vector3 camStartPos;
    public float distance; // distance between the camera start position and its current position
    public GameObject[] backgrounds;
    public SpriteRenderer[] spriteRenderers; // Array for SpriteRenderers instead of Materials
    public float[] backSpeed;
    public float farthestBack;
    [Range(0.01f, 0.05f)]
    public float parallaxSpeed;

    void Start()
    {
        cam = Camera.main.transform;
        camStartPos = cam.position;

        int backCount = transform.childCount;
        spriteRenderers = new SpriteRenderer[backCount]; // Initialize spriteRenderer array
        backSpeed = new float[backCount];
        backgrounds = new GameObject[backCount];

        for (int i = 0; i < backCount; i++) // Get all the backgrounds and their SpriteRenderers
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            spriteRenderers[i] = backgrounds[i].GetComponent<SpriteRenderer>(); // Get the SpriteRenderer
        }

        BackSpeedCalculate(backCount);
    }

    void BackSpeedCalculate(int backCount)
    {
        for (int i = 0; i < backCount; i++) // Find the farthest background
        {
            if ((backgrounds[i].transform.position.z - cam.position.z) > farthestBack)
            {
                farthestBack = backgrounds[i].transform.position.z - cam.position.z;
            }
        }

        for (int i = 0; i < backCount; i++) // Set the speed of backgrounds
        {
            backSpeed[i] = 1 - (backgrounds[i].transform.position.z - cam.position.z) / farthestBack;
        }
    }

    private void LateUpdate()
    {
        distance = cam.position.x - camStartPos.x;
        transform.position = new Vector3(cam.position.x, transform.position.y, 0); // Update scroller position

        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speed = backSpeed[i] * parallaxSpeed;

            // Manipulate the position of the background directly to achieve the parallax effect
            Vector3 offset = new Vector3(distance * speed, 0, 0);
            spriteRenderers[i].transform.position = backgrounds[i].transform.position + offset;
        }
    }
}
