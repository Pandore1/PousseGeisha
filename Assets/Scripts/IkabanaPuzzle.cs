
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IkabanaPuzzle : MonoBehaviour
{
    [Header("Élément Puzzle")]
    [Range(1,6)]
    [SerializeField] private int _difficulty=4;
    [SerializeField] private Transform _gameHolder;
    [SerializeField] private Transform _piecePrefab;

    [Header("UI Élements")]
    [SerializeField] private List<Texture2D> _puzzleTexture;
    [SerializeField] private Transform levelSelectPanel;
    [SerializeField] private Image levelSelectPrefab;
    [SerializeField] private GameObject playAgainBtn;
    [SerializeField] private GameObject _tutorialBtn;
    private List<Transform> _pieces;
    private Vector2Int dimension;
    private float width;
    private float height;

    private Vector3 offset;
    private Transform _draggingPiece=null;
    private int piecesCorrect;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.TrainingPhaseIndex==0)
        {
            _tutorialBtn.SetActive(true);

            Image piece = Instantiate(levelSelectPrefab, levelSelectPanel);
            piece.sprite = Sprite.Create(_puzzleTexture[0], new Rect(0, 0, _puzzleTexture[0].width, _puzzleTexture[0].height), Vector2.zero);
            piece.GetComponent<Button>().onClick.AddListener(delegate { StartPuzzle(_puzzleTexture[0]); });


        }
        else
        {
            foreach (Texture2D puzzleModel in _puzzleTexture)
            {
                Image piece = Instantiate(levelSelectPrefab, levelSelectPanel);
                piece.sprite = Sprite.Create(puzzleModel, new Rect(0, 0, puzzleModel.width, puzzleModel.height), Vector2.zero);
                piece.GetComponent<Button>().onClick.AddListener(delegate { StartPuzzle(puzzleModel); });
            }

        }
   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero);
       
            if (hit&&hit.collider.gameObject.tag=="PuzzlePiece") { 

                _draggingPiece=hit.transform;
                offset=_draggingPiece.position- Camera.main.ScreenToWorldPoint(Input.mousePosition);
                offset += Vector3.back;
            }
        }
        if (_draggingPiece && Input.GetMouseButtonUp(0))
        {
            SnapDisable();
            _draggingPiece.position += Vector3.forward;
            _draggingPiece = null;
        }

        if (_draggingPiece)
        {
            Vector3 newPosition= Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPosition += offset;
            _draggingPiece.position = newPosition;  
        }
    }
   
    public void StartPuzzle(Texture2D puzzleTexture)
    {
        levelSelectPanel.gameObject.SetActive(false);

        //Créer un nouvelle de liste pour stocker les différentes pièces de casse-tête
        _pieces = new List<Transform>();

        //Calculer la taille des différents morceau basé sur la difficulté
        dimension = GetDimensions(puzzleTexture, _difficulty);
        CreatePuzzlePiece(puzzleTexture);

        //Placer pièce random autour du board
        Scatter();
        UpdateBorder();
        piecesCorrect = 0;
    }

    //Vérifier si les dimensions du puzzle sont verticale ou horizontale

    Vector2Int GetDimensions(Texture2D puzzleTexture,int difficulty)
    {
        Vector2Int dimension=Vector2Int.zero;
        if (puzzleTexture.width < puzzleTexture.height)
        {
            dimension.x = difficulty;
            dimension.y = (difficulty * puzzleTexture.height) / puzzleTexture.width;
        }
        else
        {
            dimension.x= (difficulty * puzzleTexture.width) / puzzleTexture.height;
            dimension.y = difficulty;
        }
        return dimension;
    }
    void CreatePuzzlePiece(Texture2D puzzleTexture)
    {
        height = 1f / dimension.y;
        float aspect=(float)puzzleTexture.width /puzzleTexture.height;
        width = aspect / dimension.x;
        for (int row=0; row < dimension.y; row++)
        {
            for(int col=0; col < dimension.x; col++)
            {
                Transform piece = Instantiate(_piecePrefab, _gameHolder);
                piece.localPosition = new Vector3(
                    (-width * dimension.x / 2) + (width * col) + (width / 2),
                    (-height * dimension.y/2) + (height * row) + (height / 2),-1
                );
                piece.localScale = new Vector3(width, height, 1f);
                piece.name = $"Piece {(row * dimension.x) + col}";
                _pieces.Add( piece );


                //Assigner la texture au pièce
                //Nécessaire d'avoir la largeur et la hauteur  normaliser entre 0 et 1 pour utiliser UV
                float width1 = 1f / dimension.x;
                float height1 = 1f / dimension.y;

                //Coordonée UV sont dans l'ordre antihoraire (0,0), (1,0)...
                Vector2[] uv = new Vector2[4];
                uv[0] =new Vector2(width1*col,height1*row);
                uv[1]=new Vector2(width1*(col+1),height1*row);
                uv[2] = new Vector2(width1 * col, height1 * (row + 1));
                uv[3] = new Vector2(width1 * (col + 1), height1 * (row + 1));
                Mesh mesh=piece.GetComponent<MeshFilter>().mesh;
                mesh.uv= uv;
                piece.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", puzzleTexture);
            }
        }
    }


    private void Scatter()
    {

        float orthoHeight=Camera.main.orthographicSize;
        float screenAspect=(float)Screen.width / Screen.height;
        float orthoWidth = (screenAspect * orthoHeight);


        //Vérifier que les pièces soit séparé des côtés
        float pieceWidth = width * _gameHolder.localScale.x;
        float pieceHeight=height * _gameHolder.localScale.y;
        orthoHeight-= pieceHeight;
        orthoWidth-= pieceWidth;
        //Placer chaque pièce random

        foreach(Transform piece in _pieces)
        {
            float x =Random.Range(-orthoWidth, orthoWidth);
            float y=Random.Range(-orthoHeight, orthoHeight);
            piece.position=new Vector3 (x,y,-1);   
        }


    }
    private void UpdateBorder()
    { 

        //Faire la texture qui encadre le puzzle
        LineRenderer lineRenderer= _gameHolder.GetComponent<LineRenderer>();
        
        float halfWidth=(width*dimension.x)/2;
        float halfHeight=(height*dimension.y)/2;
        lineRenderer.SetPosition(0,new Vector3(-halfWidth,halfHeight,0));
        lineRenderer.SetPosition(1, new Vector3(halfWidth, halfHeight, 0));
        lineRenderer.SetPosition(2, new Vector3(halfWidth, -halfHeight, 0));
        lineRenderer.SetPosition(3, new Vector3(-halfWidth, -halfHeight, 0));
        lineRenderer.enabled = true;
    }

    private void SnapDisable()
    {
        
        int pieceIndex = _pieces.IndexOf(_draggingPiece);


        //Coordonnée row et colonne
        int col = pieceIndex % dimension.x;
        int row = pieceIndex / dimension.x;
        //Check if close enough
        Vector2 targetPosition = new((-width * dimension.x / 2) + (width * col) + (width / 2), (-height * dimension.y / 2) + (height * row) + (height / 2));
        if (Vector2.Distance(_draggingPiece.localPosition, targetPosition) < width / 1.5)
        {
            _draggingPiece.localPosition = targetPosition;

            //Désactiver collider pour pas bouger
            _draggingPiece.GetComponent<BoxCollider2D>().enabled = false;
            piecesCorrect++;
            if (piecesCorrect == _pieces.Count)
            {
                Debug.Log("BtnRestart");
              
                if (GameManager.Instance.TrainingPhaseIndex == 0)
                {
                    foreach (Transform piece in _pieces)
                    {

                        Destroy(piece.gameObject);
                    }
                    _pieces.Clear();
                    _difficulty++;
                    ApplicationManager.Instance.LevelBar.XpGain();
                }
                else
                {
                    playAgainBtn.SetActive(true);
                }
            }
        }
    }

    public void RestartGame()
    {
        //Détruire les pièces
        foreach (Transform piece in _pieces) { 

            Destroy(piece.gameObject);
        }
       
        _difficulty++;
        _pieces.Clear();
       _gameHolder.GetComponent<LineRenderer>().enabled=false;
        //Montrer sélecteur de niveau
        playAgainBtn.SetActive(false);
        levelSelectPanel.gameObject.SetActive(true);
        ApplicationManager.Instance.LevelBar.XpGain();


   
    }

}
