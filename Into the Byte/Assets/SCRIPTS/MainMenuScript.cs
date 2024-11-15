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

    }
    
    public void quitBtn()
    {

    }
}
