using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DanseSequence : MonoBehaviour
{
    //[SerializeField] private GameObject[] _danceMovements;
    //Les mouvements de la bonne Séquence qui a été enregistré
    [SerializeField] private List<GameObject> _correctSequence;
    [SerializeField] private List<GameObject> _buttonsMoves;
    [SerializeField] private Material _danceGlow;
    private int _verifySequence;


    private GameObject _currentMove;

    // Start is called before the first frame update
    void Start()
    {   
        StartSequence();
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
        float nbMove = 0f;
        while (nbMove < GameManager.Instance.MoveLenght)
        {
            int randomMove = Random.Range(0, _buttonsMoves.Count);

            _currentMove = _buttonsMoves[randomMove];
            _currentMove.GetComponent<SpriteRenderer>().material = _danceGlow;
            _correctSequence.Add(_buttonsMoves[randomMove]);
            nbMove++;

            yield return new WaitForSeconds(2);
            _currentMove.GetComponent<SpriteRenderer>().material =default;


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

    public void CheckCorrectBtn()
    {
        Debug.Log("check");
        if (_currentMove== _correctSequence[_verifySequence])
        {
            _verifySequence++;
        }
        else
        {
            FailSequence();

            
        }
        if (_verifySequence >= GameManager.Instance.MoveLenght)
        {
            SuccessSequence();
        }
    }
    private void FailSequence() 
    {
        Debug.Log("choré manqué"); 
        _verifySequence = 0;
        _correctSequence.Clear();
        Invoke("StartSequence", 5f);
        StartSequence();

    }

    private void SuccessSequence()
    {
        Debug.Log("Choré réussite");

        _verifySequence = 0;
        _correctSequence.Clear();
        Invoke("StartSequence", 5f);
        GameManager.Instance.MoveLenght++;
        ApplicationManager.Instance.LevelBar.XpGain();
    }
}
