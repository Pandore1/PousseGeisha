using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DanseSequence : MonoBehaviour
{
    [SerializeField] private Sprite[] _danceMovements;
    //Les mouvements de la bonne Séquence qui a été enregistré
    [SerializeField] private List <Sprite> _correctSequence;
    private int _verifySequence;


    private int _moveLenght;
    private Image _currentMove;
//    [SerializeField] private Image _currentMove;

    // Start is called before the first frame update
    void Start()
    {   
        _currentMove = GetComponent<Image>();
        _moveLenght = 5;
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
        float nbMove = 0f;
        Debug.Log("Création de la séquence");
        while (nbMove < _moveLenght)
        {
            int randomMove = Random.Range(0, _danceMovements.Length);
            Debug.Log("item sequence" + randomMove);

            _currentMove.sprite = _danceMovements[randomMove];
            _correctSequence.Add(_danceMovements[randomMove]);
            nbMove++;

            yield return new WaitForSeconds(2);

        }
        Debug.Log("Fin Sequence");
        

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
            Debug.Log("Yeah!");
            _verifySequence++;
        }
        else
        {
            Debug.Log("Fail");
            FailSequence();

            
        }
        if (_verifySequence >= _moveLenght)
        {
            EndSequence();
        }
    }
    private void FailSequence() 
    {
        Debug.Log("choré manqué"); 
        _verifySequence = 0;
        _correctSequence.Clear();
        StartSequence();

    }

    private void EndSequence()
    {
        Debug.Log("Choré réussite");
        _moveLenght++;
        _verifySequence = 0;
        _correctSequence.Clear();
        StartSequence();
    }
}
