using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DanseSequence : MonoBehaviour
{
    [SerializeField] private Sprite[] danceMovements;
   // [SerializeField] private Sprite[] correctSequence;
    private int _sequenceLenght;
    private Image _currentMove;
//    [SerializeField] private Image _currentMove;

    // Start is called before the first frame update
    void Start()
    {   
        _currentMove = GetComponent<Image>();
        _sequenceLenght = 5;
        MakeSequence();
    }

    // Update is called once per frame
    void Update()
    {
    }
    void MakeSequence()

    {
        Debug.Log("Création de la séquence");
        int randomMove = Random.Range(0, 3);
        _currentMove.sprite = danceMovements[randomMove];
   

        
    }
    public void Blue()
    {
       
    }
}
