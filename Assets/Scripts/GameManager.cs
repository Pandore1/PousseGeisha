using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    // Start is called before the first frame update
 
    
    [Header("Mini-jeux niveau")]
    public int MoveLenght=2; //Nombre de mouvement dans la danse
    public int CharacterNb = 0;
    public int PuzzleMade = 0;

    [Header("LevelBar")]
    [SerializeField] private TMPro.TMP_Text _levelText;
    [SerializeField] private RectTransform _levelFill;

    public float TaskMade = 0;
    public float TotalTask = 5;
    public float CurrentLevel=0;
    [Header("Geisha")]
    [SerializeField] private string[] TrainingPhase;
    public int TrainingPhaseIndex = 0;

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
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        
    
    }
    public static async Task DelayAsync(float secondDelay)
    {
        float startTime = Time.time;
        while (Time.time < startTime + secondDelay) await Task.Yield();


    }
    public void LevelUp()
    {
       CurrentLevel++;
        if (CurrentLevel == 1)
        {
            TrainingPhaseIndex++;
        }
   TaskMade = 0;
    }
}
