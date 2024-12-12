
using UnityEngine;
using System.Threading.Tasks;

using DG.Tweening;

public class TeaCeremony : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject[] _itemsList;
    [SerializeField] private TMPro.TMP_Text _objectFoundTxt;
    [SerializeField] private TMPro.TMP_Text _totalObjectFindTxt;
    private int _totalObjectNb;
    private int _objectFoundNb;


    //Animate Object
    [Header("Objet interractif")]
    [SerializeField] private Material _glowMaterial;
    [SerializeField] private GameObject _hishakuBucket;
    [SerializeField] private GameObject _bucket;
    [SerializeField] private GameObject _chasenBridge;
    [SerializeField] private GameObject _chawanLilypad;
    [SerializeField] private GameObject _lilypad;
    [SerializeField] private GameObject _bird;
    [SerializeField] private GameObject _teapotBird;
    [SerializeField] private GameObject _chashakuTree;
    [SerializeField] private GameObject _chakin;

    [SerializeField] private GameObject _basket;
    [SerializeField] private GameObject _chaireBasket;
    [SerializeField] private GameObject _branchGrove;
    [SerializeField] private GameObject _fukusaGrove;

   

    void Start()
    {
        _itemsList = GameObject.FindGameObjectsWithTag("ItemToFind");
        _totalObjectNb = _itemsList.Length;
        _totalObjectFindTxt.text = _totalObjectNb.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        _objectFoundTxt.text = _objectFoundNb.ToString();
    }
    public void FoundObject(string objectName)
    {
    
        foreach (GameObject item in _itemsList) {
            
            TMPro.TMP_Text itemName = item.transform.Find("ObjectName").GetComponent<TMPro.TMP_Text>();
            if (itemName.text == objectName) {
               
                item.gameObject.SetActive(false);
                _objectFoundNb++;
                if (_objectFoundNb == _totalObjectNb)
                {
                    ApplicationManager.Instance.LevelBar.XpGain();
                    ApplicationManager.Instance.SwitchScene("Maison");
                }
                break;
            }
        }
    }
    private async void AnimateObjectFind(GameObject objectFound)
    {
        objectFound.GetComponent<SpriteRenderer>().material = _glowMaterial;
        objectFound.transform.DORotate(new Vector3(0,0,260f), 1f).SetDelay(1.2f);
        objectFound.transform.DOScale(new Vector3(0f, 0f, 0f),1f).SetDelay(1.1f);
       await GameManager.DelayAsync(2);

        objectFound.SetActive(false);
    }
   

    public void FlipBucket()
    {
        _bucket.transform.DORotate(new Vector3(0, 0, -90), 1f, RotateMode.LocalAxisAdd);
        Invoke("FoundHishaku", 0.5f);
    }
    public async void FoundHishaku()
    {
        _hishakuBucket.transform.DOMove(new Vector3(-4f,-4f,0),1f);
        await GameManager.DelayAsync(1);
        FoundObject("Hishaku");
        AnimateObjectFind(_hishakuBucket);
    }

    public async void FoundChasen()
    {
        _chasenBridge.GetComponent<SpriteRenderer>().sortingOrder = 20;
        _chasenBridge.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f);
        await GameManager.DelayAsync(0.5f);

        FoundObject("Chasen");
        AnimateObjectFind (_chasenBridge);
    }
    public async void FoundChawan()
    {
        _lilypad.transform.DOMove(new Vector3(-0.53f,-1.1f,0),2).SetEase(Ease.OutSine);
        await GameManager.DelayAsync(1);
        FoundObject("Chawan");
        AnimateObjectFind(_chawanLilypad);
    }
    public async void FoundChashaku()
    {
        _chashakuTree.transform.DORotate(new Vector3(0, 0, 1000), 1f);
        _chashakuTree.transform.DOMove(new Vector3(5, 2, 0), 1f);

        await GameManager.DelayAsync(1);
        FoundObject("Chashaku");
        AnimateObjectFind(_chashakuTree);

    } 
    public async void FoundChakin()
    {
        await GameManager.DelayAsync(1);
        FoundObject("Chakin");
        AnimateObjectFind(_chakin);

    }

    public  void OverTurnBasket()
    {
        _basket.transform.DORotate(new Vector3(0,0,90f), 0.5f);

        Invoke("FallChaire", 0.5f);
        FallChaire();
    }
    public async void FallChaire()
    {   
        Rigidbody2D _bodyChaire=_chaireBasket.GetComponent<Rigidbody2D>();
        _bodyChaire.isKinematic = false;
        Vector2 rollDirection = new Vector2(-2f, -1f); 
        await GameManager.DelayAsync(1);
        _bodyChaire.AddForce(rollDirection * 0.5f);
        FoundObject("Cha-ire");
        AnimateObjectFind(_chaireBasket);

    }
    
    public async void FlyBird()
    { 
        Animator _animatorBird = _bird.GetComponent<Animator>();
        _animatorBird.SetTrigger("Fly");
        _teapotBird.GetComponent<SpriteRenderer>().sortingOrder =3;
        await GameManager.DelayAsync(1);
        FoundObject("Théière");

        AnimateObjectFind(_teapotBird);

    }
   

    public void ShakeBranch()
    {
       _branchGrove.transform.DOShakeRotation(1,new Vector3(0, 90, 0f), randomness: 0);
        _branchGrove.transform.DORotate(new Vector3(0,0,90f), 2f, RotateMode.LocalAxisAdd).SetDelay(0.5f);
        Invoke("FallFukusa", 1f);
   
        

    }
    public async void FallFukusa()
    {
        _fukusaGrove.GetComponent<SpriteRenderer>().sortingOrder = 20;
    
        await GameManager.DelayAsync(1);
        AnimateObjectFind(_fukusaGrove);
    }
  
}
