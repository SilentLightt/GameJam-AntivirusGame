using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoEndScene : MonoBehaviour
{
    // Reference to the VideoPlayer component
    private VideoPlayer videoPlayer;

    // Name of the scene to load after the video ends
    [SerializeField] private string nextSceneName;

    void Start()
    {
        // Get the VideoPlayer component attached to the GameObject
        videoPlayer = GetComponent<VideoPlayer>();

        // Subscribe to the loopPointReached event, which is called when the video ends
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd;
        }
    }

    void Update()
    {
        // Check if the player presses the Space key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SkipIntro();
        }
    }

    // Method called when the video finishes playing
    private void OnVideoEnd(VideoPlayer vp)
    {
        LoadNextScene();
    }

    // Method to skip the intro video
    private void SkipIntro()
    {
        Debug.Log("Intro skipped.");
        LoadNextScene();
    }

    // Load the next scene
    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    // Optional: Unsubscribe from the event to avoid memory leaks
    void OnDestroy()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }
}
