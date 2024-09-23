using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingCharacter : MonoBehaviour
{
    public Camera _drawCamera;
    public GameObject _brushPrefab;

    LineRenderer _currentLineRenderer;
    Vector2 lastPosition;
    [SerializeField] private float _minDistance = 0.7f;
    [SerializeField] private float _maxDistance = 1f; 
    private void Start()
    {   if(Camera.main != null)
        {
            _drawCamera = Camera.main;

        }
    }
    private void Update()
    {
        Draw();
    }
    
    void Draw()
    {
       // Physics2D.OverlapPoint()
        if (Input.GetMouseButtonDown(0))
        {
            CreateBrush();
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = _drawCamera.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePosition);
            if (hit&&hit.gameObject.tag== "CalligraphyPoint")
            { 
                float distance =Vector2.Distance(hit.transform.position,lastPosition);
                if (distance>_maxDistance)
                {   
                    _currentLineRenderer = null;
                    CreateBrush();
                    lastPosition = mousePosition;

                }
                else
                {
                    AddPoint(mousePosition);
                    lastPosition = mousePosition;
                }
        

            }
        }
        else
        {
            _currentLineRenderer= null;
        }
    }

   
     void CreateBrush()
    {
        Vector2 mousePosition = _drawCamera.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(mousePosition);
        if (hit&&hit.gameObject.tag=="CalligraphyPoint")
        {
            GameObject brushInstance = Instantiate(_brushPrefab);

            _currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

            _currentLineRenderer.SetPosition(0, mousePosition);
            //_currentLineRenderer.SetPosition(1, mousePosition);


        }




    }
    void AddPoint(Vector2 pointPosition)
    {   
        _currentLineRenderer.positionCount++;
        int positionIndex=_currentLineRenderer.positionCount-1;
        _currentLineRenderer.SetPosition(positionIndex,pointPosition);
    }

   

}
