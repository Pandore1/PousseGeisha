using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManekiNeko : MonoBehaviour
{
    [SerializeField] private string [] _dialogueLines;
    [SerializeField] private TMPro.TMP_Text _dialogueComponent;
    [SerializeField] private GameObject _dialogueCanva;
    [SerializeField]private int _index;

    [SerializeField]private float _dialogueSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        _dialogueComponent.text=string.Empty;    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
         
            if (_dialogueComponent.text == _dialogueLines[_index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                //_dialogueComponent.text = _dialogueLines[_index];
            }
        }
    }
    public void StartDialogue()
    {
        _index = 0;
        _dialogueCanva.SetActive(true);
      
        StartCoroutine(TypeDialogue());

    }
    IEnumerator TypeDialogue()
    {   
        foreach(char c in _dialogueLines[_index].ToCharArray())
        {
            _dialogueComponent.text += c;
            yield return new WaitForSeconds(_dialogueSpeed);
        }
     
    }
    void NextLine()
    {
        if(_index < _dialogueLines.Length - 1)
        {
            _index++;
            _dialogueComponent.text=string.Empty;
            StartCoroutine(TypeDialogue());
        }
        else
        {
            _dialogueCanva.SetActive(false);

        }
    }
}
