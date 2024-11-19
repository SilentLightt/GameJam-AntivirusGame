using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel; 
    public bool isPaused; // Tracks whether the game is paused

    void Update()
    {
        // Check if the ESC key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame(); 
            }
            else
            {
                PauseGame(); 
            }
        }
    }

    // Function to pause the game
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Pauses the game
        pausePanel.SetActive(true); // Show the pause panel
    }

    // Function to resume the game
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Resumes the game
        pausePanel.SetActive(false); // Hide the pause panel
    }

}
