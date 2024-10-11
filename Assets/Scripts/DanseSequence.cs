using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DanseSequence : MonoBehaviour
{
    [SerializeField] private Sprite[] _danceMovements;
    //Les mouvements de la bonne Séquence qui a été enregistré
    [SerializeField] private List <Sprite> _correctSequence;
    [SerializeField] private List<Button> _buttonsMoves;
    private int _verifySequence;


    private Image _currentMove;
//    [SerializeField] private Image _currentMove;

    // Start is called before the first frame update
    void Start()
    {   
        _currentMove = GetComponent<Image>();
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
            buttonMove.interactable = false;
        });
        float nbMove = 0f;
        while (nbMove < GameManager.Instance.MoveLenght)
        {
            int randomMove = Random.Range(0, _danceMovements.Length);

            _currentMove.sprite = _danceMovements[randomMove];
            _correctSequence.Add(_danceMovements[randomMove]);
            nbMove++;

            yield return new WaitForSeconds(2);

        }
        Debug.Log("Fin Sequence");

        _buttonsMoves.ForEach(buttonMove =>
        {
            buttonMove.interactable = true;
        });

    }
  

    public void Blue()
    {
        _currentMove.sprite = _danceMovements[0];
        CheckCorrectBtn();

    }
    public void Red()
    {
        _currentMove.sprite = _danceMovements[1];
        CheckCorrectBtn();
    }
    public void Green()
    {
        _currentMove.sprite = _danceMovements[2];
        CheckCorrectBtn();
     
    }

    public void CheckCorrectBtn()
    {
        if (_currentMove.sprite == _correctSequence[_verifySequence])
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
        GameManager.Instance.MoveLenght++;
        GameManager.Instance.LevelBar.XpGain();
        _verifySequence = 0;
        _correctSequence.Clear();
        Invoke("StartSequence", 5f);
    }
}
