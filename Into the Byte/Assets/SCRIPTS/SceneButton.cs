using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Scene
using UnityEngine.UI;


public class SceneButton : MonoBehaviour
{
    public string sceneName; // Name of the scene to reload

    public void SceneLoader()
    {
        // Load the scene by name
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f; // Resume the game
    }
}
