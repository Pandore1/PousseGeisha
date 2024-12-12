
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject Fade;


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
