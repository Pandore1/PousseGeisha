using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

public class LevelBar : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _levelText;
    [SerializeField] private RectTransform _levelFill;
  

  
    // Update is called once per frame
    void Update()
    {
        _levelText.text= GameManager.Instance.CurrentLevel.ToString();
    

    }
    public void XpGain()
    {
        GameManager.Instance.TaskMade++;
        if (GameManager.Instance.TaskMade == GameManager.Instance.TotalTask)
        {
            GameManager.Instance.LevelUp();
         
           
        }
        _levelFill.GetComponent<RectTransform>().DOScaleX(((GameManager.Instance.TaskMade / GameManager.Instance.TotalTask) * GetComponent<RectTransform>().rect.width),0.5f);
    }
}
