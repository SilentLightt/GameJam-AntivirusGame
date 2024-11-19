using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void playBtn()
    {
        SceneManager.LoadSceneAsync(2);
        Time.timeScale = 1f; // Resume the game
    }

    public void settingsBtn()
    {
        SceneManager.LoadSceneAsync(5);
    }
    
    public void quitBtn()
    {
        Application.Quit();
    }
    public void backToMainMenu() 
    {
        SceneManager.LoadSceneAsync(1);
        Time.timeScale = 1f; // Resume the game

    }
    public void playMainGame()
    {
        SceneManager.LoadSceneAsync(3);
        Time.timeScale = 1f; // Resume the game

    }
}
