using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Calligraphy : MonoBehaviour
{
    [SerializeField] private List<Transform>  _stepPoints;
    [SerializeField] private CalligraphyLine _CallygraphyLine;
    [SerializeField] private GameObject _pointPrefab;
    [SerializeField] private List<Transform> _goodSteps;
    [SerializeField] private List<Transform> _startLine;
    
    private int _index = 0;

    private float _allowedDistance = 3f;
    // Start is called before the first frame update
    void Start()
    {
        _CallygraphyLine.SetUpLine(_stepPoints);
    }

    // Update is called once per frame
    void Update()
    {/*
        if (Input.GetMouseButtonDown(0))   
        {
            MakePoint(); 
        }*/
    }
    public void MakePoint(GameObject BtnPoint)
    {
    
      //  BtnPoint.transform.position = new Vector3(BtnPoint.transform.position.x, BtnPoint.transform.position.y, BtnPoint.transform.position.z);
        
        _stepPoints.Add(BtnPoint.transform);
        Debug.Log("start " + _index);
        _CallygraphyLine.SetUpLine(_stepPoints);
        /*
         Good Bad Point
      GameObject newPoint = Instantiate(_pointPrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
      newPoint.transform.position = new Vector3(newPoint.transform.position.x, newPoint.transform.position.y, -5);
      _stepPoints.Add(newPoint.transform);

      float distanceTarget = Vector2.Distance(newPoint.transform.position, _goodSteps[_index].transform.position);
      if (distanceTarget <= _allowedDistance)
      {
          Debug.Log("Yeah " + distanceTarget);

          if ((_index+1) !=5)
          {
              Debug.Log("Continue");
              _index++;

          }
          else
          {
              Debug.Log("NewLine "+_index);
              Debug.Log("Fin caractère");
             // GameObject newLine = Instantiate(_linePrefab);

          }
      }
      else
      {
          Debug.Log("Fail " + distanceTarget);
      }*/


        Debug.Log("end "+_index);
    }
   
}
