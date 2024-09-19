using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Calligraphy : MonoBehaviour
{
    [SerializeField] private List<Transform>  _stepPoints;
    [SerializeField] private CalligraphyLine _Callygraphyline;
    [SerializeField] private GameObject _pointPrefab;
    [SerializeField] private List<Transform> _goodSteps;
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        _Callygraphyline.SetUpLine(_stepPoints);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {/*
            GameObject newPoint=Instantiate(_pointPrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition),Quaternion.identity);
            newPoint.transform.position=new Vector3(newPoint.transform.position.x,newPoint.transform.position.y,-5);
            _stepPoints.Add(newPoint.transform);
            _Callygraphyline.SetUpLine(_stepPoints);*/
           
        }
    }
    private void MakePoint()
    {
        GameObject newPoint = Instantiate(_pointPrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        newPoint.transform.position = new Vector3(newPoint.transform.position.x, newPoint.transform.position.y, -5);
        _stepPoints.Add(newPoint.transform);
        _Callygraphyline.SetUpLine(_stepPoints);
        if (newPoint.transform.position != _goodSteps[index].transform.position)
        {
            Debug.Log("Fail");
            Debug.Log(newPoint.transform.position);
        };
    }
}
