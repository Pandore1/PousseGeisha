using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class DanseSequence : MonoBehaviour
{
    [SerializeField] private Timer _timerScript;
    //[SerializeField] private GameObject[] _danceMovements;
    //Les mouvements de la bonne Séquence qui a été enregistré
    [SerializeField] private List<GameObject> _correctSequence;
    [SerializeField] private List<GameObject> _buttonsMoves;
    [SerializeField] private Material _danceGlow;
    [SerializeField] private Material _defaultMaterial;
    private int _verifySequence;


    private GameObject _currentMove;

    public int MoveLenghtDebug=2;

    // Start is called before the first frame update

    public async void StartTimer()
    {
        _buttonsMoves.ForEach(buttonMove =>
        {
            buttonMove.GetComponent<Collider2D>().enabled = false;
        });
        _timerScript.StartCounter(3);
        await DelayAsync(3);
        StartSequence();
        //Invoke("StartSequence", 3);
    }
    public void StartSequence()
    {   
        StartCoroutine(MakeSequence());
    }
    // Update is called once per frame
    void Update()
    {
    }
  

    IEnumerator MakeSequence()
    {
        _buttonsMoves.ForEach(buttonMove =>
        {
            buttonMove.GetComponent<Collider2D>().enabled = false;
        });
        int nbMove = 0;
        while (nbMove < /*GameManager.Instance.MoveLenght*/MoveLenghtDebug)
        {
            int randomMove = Random.Range(0, _buttonsMoves.Count);

            _currentMove = _buttonsMoves[randomMove];
            _currentMove.GetComponent<SpriteRenderer>().material = _danceGlow;
            _correctSequence.Add(_buttonsMoves[randomMove]);
            nbMove++;

            yield return new WaitForSeconds(2.5f);
            _currentMove.GetComponent<SpriteRenderer>().material = _defaultMaterial;


        }
        Debug.Log("Fin Sequence");

        _buttonsMoves.ForEach(buttonMove =>
        {
            buttonMove.GetComponent<Collider2D>().enabled = true;
           
        });

    }
  

    public void Dance1()
    {
        _currentMove = _buttonsMoves[0];
        CheckCorrectBtn();

    }
    public void Dance2()
    {
       _currentMove = _buttonsMoves[1];
        CheckCorrectBtn();
    }
    public void Dance3()
    {
       _currentMove= _buttonsMoves[2];
        CheckCorrectBtn();
     
    }
    public void Dance4()
    {
       _currentMove= _buttonsMoves[3];
        CheckCorrectBtn();

    }
     //Vérifier si le bon mouvement a été réalisé
    public void CheckCorrectBtn()
    {
        _currentMove.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.5f);
        _currentMove.transform.DOScale(new Vector3(1, 1, 1), 0.2f).SetDelay(0.8f);

        Debug.Log("check");
        if (_currentMove== _correctSequence[_verifySequence])
        {
            _verifySequence++;
        }
        else
        {
            _currentMove.transform.DOShakeRotation(0.5f, new Vector3(0, 0, 50f), randomness: 0);
            FailSequence();
        }
        if (_verifySequence >= MoveLenghtDebug /*GameManager.Instance.MoveLenght*/)
        {
            SuccessSequence();
        }
    }
    private async void FailSequence() 
    {
        Debug.Log("choré manqué"); 
        _verifySequence = 0;
        _correctSequence.Clear();
        await DelayAsync(2);
        StartSequence();

    }

    private async void SuccessSequence()
    {
        Debug.Log("Choré réussite");

        _verifySequence = 0;
        MoveLenghtDebug++;
        _correctSequence.Clear();
        await DelayAsync(3);
        StartSequence();
      
        //Invoke("StartSequence", 2f);
        
       // GameManager.Instance.MoveLenght++;
        //ApplicationManager.Instance.LevelBar.XpGain();
    }

    public async Task DelayAsync(float secondDelay)
    {
        float startTime = Time.time;
        while (Time.time < startTime + secondDelay) await Task.Yield();


    }
}
