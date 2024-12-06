using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


public class SceneEnter : MonoBehaviour
{
    public static float _homeEnter = 0; //La maison n'a pas été entré

    // Start is called before the first frame update
    void Start()
    {
        if (_homeEnter == 0)
        {
            _homeEnter = 1;
            PlayerPrefs.SetFloat("homeEnter", _homeEnter);
            GoTutorial();
    

        }
  
    }
    private async void GoTutorial()
    {
        await GameManager.DelayAsync(2);

        ApplicationManager.Instance.SceneSwitcher.SwitchScene("Shikomi");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
