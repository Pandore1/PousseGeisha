using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    // Start is called before the first frame update
    [Header("Diff�rents scripts")]
    public SceneSwitcher SceneSwitcher;
    public LevelBar LevelBar;
 
    
    [Header("Mini-jeux niveau")]
    public int MoveLenght=2; //Nombre de mouvement dans la danse
    public int CharacterNb = 1;

    private void Awake()
    {
        //on cr�e l'instance du singleton si elle n'existe pas d�ja
        if (Instance != null && Instance != this)
        {
            //sinon on la detryut
            Destroy(this);

        }

        Instance = this;//affectation de l'instance
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
