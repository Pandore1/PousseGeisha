using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrawingCharacter : MonoBehaviour
{
    public Camera _drawCamera;

    private Collider2D _lastHitCollider;
    Vector2 lastPosition;

    [Header ("Brush")]
    [SerializeField] private List<GameObject> _brushList;
    [SerializeField] private GameObject _brushPrefab;
    [SerializeField] private LineRenderer _currentLineRenderer;

   

    [Header("Caracter")]
    [SerializeField] private GameObject[] _caracterList;
    [SerializeField] private GameObject _nextCaracterBtn;
    private void Start()
    {
        Debug.Log(GameManager.Instance.CharacterNb);
        if(Camera.main != null)
        {
            _drawCamera = Camera.main;

        }
        _caracterList[GameManager.Instance.CharacterNb].SetActive(true);

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
                    if (_currentLineRenderer == null && pointScript.StartLine == true)
                    {

                        CreateBrush(hit, mousePosition);
                        lastPosition = mousePosition;

                    }
                    else if (pointScript.nbLine==_lastHitCollider.GetComponent<CalligraphyPoint>().nbLine)
                    {   
                        AddPoint(mousePosition);
                        lastPosition = mousePosition;
                    

                    }
                    hit.enabled = false;
                    _lastHitCollider = hit;
                  
                }

               
            }

        }
        else if (Input.GetMouseButtonUp(0))
        {
           CheckCaracterEnd();
           
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
        _brushList.Add(brushInstance);

        _currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

        _currentLineRenderer.SetPosition(0, mousePosition);
        _lastHitCollider = hit;
    }
    public void CheckCaracterEnd()
    {
        Debug.Log("Check");

        bool isCaracterEnd = false;
        GameObject[] _calligraphyTable = GameObject.FindGameObjectsWithTag("CalligraphyPoint");
        Debug.Log(_calligraphyTable.Length);
        foreach (GameObject point in _calligraphyTable)
        {
            if (!point.gameObject.GetComponent<Collider2D>().enabled)
            {
                isCaracterEnd = true;
            }
            else
            {
                isCaracterEnd = false;
                break;
            }
        }
        if (isCaracterEnd) {

            Debug.Log("Yeah");
            
           _nextCaracterBtn.SetActive(true);
           ApplicationManager.Instance.LevelBar.XpGain();
        

        }
    }
     public void HideCaracter()
    {   foreach(GameObject brush in _brushList)
        {
            Destroy(brush); //Détruire tous les line renderer
        }
        _brushList.Clear();
        foreach (GameObject caracter in _caracterList)
        {
            caracter.SetActive(false);
        }
        if (_caracterList.Length != GameManager.Instance.CharacterNb + 1)
        {
            _nextCaracterBtn.SetActive(false);
            GameManager.Instance.CharacterNb++;

        }
        _caracterList[GameManager.Instance.CharacterNb].SetActive(true);
        

    }

    void AddPoint(Vector2 pointPosition)
    {   
        _currentLineRenderer.positionCount++;
        int positionIndex=_currentLineRenderer.positionCount-1;
        _currentLineRenderer.SetPosition(positionIndex,pointPosition);

    }

   

}
