using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void playBtn()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void settingsBtn()
    {
        SceneManager.LoadSceneAsync(4);
    }
    
    public void quitBtn()
    {
        Application.Quit();
    }
    public void backToMainMenu() 
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void playMainGame()
    {
        SceneManager.LoadSceneAsync(2);
    }
}
