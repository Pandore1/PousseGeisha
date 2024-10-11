using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelBar : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _levelText;
    [SerializeField] private RectTransform _levelFill;
    private float    _currentLevel;
    private float _taskMade=0;
    private float _totalTask = 5;
  
    // Update is called once per frame
    void Update()
    {
        _levelText.text= _currentLevel.ToString();
    

    }
    public void XpGain()
    {
        _taskMade++;
        if (_taskMade == _totalTask)
        {
            _currentLevel++;
            _taskMade = 0;
        }
        Debug.Log(_taskMade);
        _levelFill.sizeDelta = new Vector2((_taskMade / _totalTask) * GetComponent<RectTransform>().rect.width, _levelFill.sizeDelta.y);
      
    }
}
