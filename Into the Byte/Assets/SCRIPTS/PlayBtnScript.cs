using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayBtnScript : MonoBehaviour
{
   public void click_Play()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
