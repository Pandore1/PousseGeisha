using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalligraphyLine : MonoBehaviour
{
    // Start is called before the first frame update
    private LineRenderer _lineRender;
   [SerializeField] private List<Transform> _stepPoint;
    



    private void Awake()
    {
        _lineRender = GetComponent<LineRenderer>();
    }
    public void SetUpLine(List<Transform> _stepPoint)
    {
        _lineRender.positionCount = _stepPoint.Count;
        this._stepPoint = _stepPoint; 
    
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _stepPoint.Count; i++) {
            _lineRender.SetPosition(i,_stepPoint[i].position);

        }
     

    }
    
}
