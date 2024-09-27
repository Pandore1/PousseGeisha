using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrawingCharacter : MonoBehaviour
{
    public Camera _drawCamera;
    public GameObject _brushPrefab;
    private Collider2D _lastHitCollider;
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
     
     
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = _drawCamera.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePosition);
            if (hit&&hit.gameObject.tag== "CalligraphyPoint")
            {
                CalligraphyPoint pointScript = hit.GetComponent<CalligraphyPoint>();
                if (pointScript != null) {
                    pointScript.nonCollided.sprite = pointScript.CollidedSprite;
                    //  float distance =Vector2.Distance(hit.transform.position,lastPosition);
                    if (_currentLineRenderer == null || pointScript.StartLine == true)
                    {

                        CreateBrush(hit, mousePosition);
                        lastPosition = mousePosition;

                    }
                    else if (pointScript.nbLine==_lastHitCollider.GetComponent<CalligraphyPoint>().nbLine)
                    {   
                        AddPoint(mousePosition);
                        lastPosition = mousePosition;
                       
                    }
                    _lastHitCollider = hit;
                    hit.enabled = false;
                }

               
            }

        }
      
        else
        {
            _lastHitCollider = null;
            _currentLineRenderer= null;
        }
    }

   
     void CreateBrush(Collider2D hit, Vector2 mousePosition)
    {
        GameObject brushInstance = Instantiate(_brushPrefab);

        _currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

        _currentLineRenderer.SetPosition(0, mousePosition);
        _lastHitCollider = hit;



    }
    void AddPoint(Vector2 pointPosition)
    {   
        _currentLineRenderer.positionCount++;
        int positionIndex=_currentLineRenderer.positionCount-1;
        _currentLineRenderer.SetPosition(positionIndex,pointPosition);

    }

   

}
