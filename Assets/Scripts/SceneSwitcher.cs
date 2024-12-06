using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    
    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        if (sceneName == "Menu")
        {
            ApplicationManager.Instance._levelBar.SetActive(false);
        }
        else
        {
            ApplicationManager.Instance._levelBar.SetActive(true);

        }
    }
}
