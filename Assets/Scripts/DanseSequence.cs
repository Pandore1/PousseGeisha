using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DanseSequence : MonoBehaviour
{
    [SerializeField] private Sprite[] _danceMovements;
    [SerializeField] private List <Sprite> _correctSequence;
   // [SerializeField] private Sprite[] correctSequence;
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
  
    public void Blue()
    {
       
    }

    IEnumerator MakeSequence()
    {
        float nbMove = 0f;
        Debug.Log("Création de la séquence");
        while (nbMove < _moveLenght)
        {
            int randomMove = Random.Range(0, 2);
            Debug.Log("item sequence" + randomMove);

            _currentMove.sprite = _danceMovements[randomMove];
            _correctSequence.Add(_danceMovements[randomMove]);
            nbMove++;

            yield return new WaitForSeconds(2);

        }
        Debug.Log("Fin Sequence");
        

    }
}
