using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void playBtn()
    {
        SceneManager.LoadSceneAsync("2ModeSelect");
    }

    public void settingsBtn()
    {
        SceneManager.LoadSceneAsync("5Settings UI*");
    }
    
    public void quitBtn()
    {
        Application.Quit();
    }
}