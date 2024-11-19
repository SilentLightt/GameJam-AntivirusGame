using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Scene


public class ButtonNavi : MonoBehaviour
{
    public List<GameObject> activeScenes;  // List of scenes to deactivate
    public List<GameObject> nextScenes;    // List of scenes to activate


    public void ButtonNextScene()
    {
        StartCoroutine(SwitchScenes());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator SwitchScenes()
    {
        // Add delay if needed
        yield return new WaitForSeconds(0f);

        // Deactivate current scenes
        foreach (GameObject scene in activeScenes)
        {
            if (scene != null)
            {
                scene.SetActive(false);
            }
        }

        // Activate next scenes
        foreach (GameObject scene in nextScenes)
        {
            if (scene != null)
            {
                scene.SetActive(true);
            }
        }
    }

}
