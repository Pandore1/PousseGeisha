using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ApplicationManager : MonoBehaviour
{   public static ApplicationManager Instance;
    // Start is called before the first frame update
    [Header("Différents scripts")]
    public SceneSwitcher SceneSwitcher;
    public LevelBar LevelBar;
    private void Awake()
    {
        //on crée l'instance du singleton si elle n'existe pas déja
        if (Instance != null && Instance != this)
        {
            //sinon on la detryut
            Destroy(this.gameObject);
            return;
        }
        Instance = this;//affectation de l'instance

        //on veut rester activé dans toutes les scènes
        DontDestroyOnLoad(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        

    }
    public void Quit()
    {
        //utilisation des compilations dynamique
#if UNITY_EDITOR
        //on arrete le mode play de l'éditeur
        EditorApplication.isPlaying = false;
#else

        //on quitte le build

        Application.Quit();
#endif
    }
}
