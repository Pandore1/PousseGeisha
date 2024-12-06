using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationManager : MonoBehaviour
{   public static ApplicationManager Instance;

    [SerializeField] private KeyCode _quitKey = KeyCode.Escape;//touche escape par d�faut mais peut modifier

    [Header("Diff�rents scripts")]
    public SceneSwitcher SceneSwitcher;
    public LevelBar LevelBar;

    public GameObject _levelBar;
    private void Awake()
    {
        //on cr�e l'instance du singleton si elle n'existe pas d�ja
        if (Instance != null && Instance != this)
        {
            //sinon on la detryut
            Destroy(this.gameObject);
            return;
        }
        Instance = this;//affectation de l'instance

        //on veut rester activ� dans toutes les sc�nes
        DontDestroyOnLoad(gameObject);

    }
   
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(_quitKey))
        {
            Quit();
        }
      

    }
    public void Quit()
    {
        //utilisation des compilations dynamique
#if UNITY_EDITOR
        //on arrete le mode play de l'�diteur
        EditorApplication.isPlaying = false;
#else

        //on quitte le build

        Application.Quit();
#endif
    }
}
