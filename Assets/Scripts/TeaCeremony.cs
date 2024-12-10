using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            Debug.Log(item.ToString());
            TMPro.TMP_Text itemName = item.transform.Find("ObjectName").GetComponent<TMPro.TMP_Text>();
            //Debug.Log(itemName.text);
            if (itemName.text == objectName) {
               
                item.gameObject.SetActive(false);
                _objectFoundNb++;
                if (_objectFoundNb == _totalObjectNb)
                {
                    Debug.Log("Yeah!");
                   // ApplicationManager.Instance.LevelBar.XpGain();
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
        await DelayAsync(1);
        FoundObject("Hishaku");
        AnimateObjectFind(_hishakuBucket);
    }

    public async void FoundChasen()
    {
        _chasenBridge.GetComponent<SpriteRenderer>().sortingOrder = 20;
        _chasenBridge.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f);
        await DelayAsync(0.5f);
        FoundObject("Chasen");
        AnimateObjectFind (_chasenBridge);
    }
    public async void FoundChawan()
    {
        _lilypad.transform.DOMove(new Vector3(0,-0.9f,0),2).SetEase(Ease.OutSine);
        await DelayAsync(1);
        //await GameManager.DelayAsync(1);
        FoundObject("Chawan");
        AnimateObjectFind(_chawanLilypad);
    }


    public void OverTurnBasket()
    {
        _basket.transform.DORotate(new Vector3(0,0,90f), 0.5f);
        Invoke("FallChaire", 0.2f);
    }
    public async void FallChaire()
    {   
        Rigidbody2D _bodyChaire=_chaireBasket.GetComponent<Rigidbody2D>();
        _bodyChaire.isKinematic = false;
        Vector2 rollDirection = new Vector2(-2f, -1f); 
        await DelayAsync(1);
        //await GameManager.DelayAsync(1);
        _bodyChaire.AddForce(rollDirection * 0.5f);
      
    }
    
    public async void FlyBird()
    { 
        Animator _animatorBird = _bird.GetComponent<Animator>();
        _animatorBird.SetTrigger("Fly");
        _teapotBird.GetComponent<SpriteRenderer>().sortingOrder =3;
    await DelayAsync(1);
        //await GameManager.DelayAsync(1);
    
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
    
        await DelayAsync(1);
        //await GameManager.DelayAsync(1);
        AnimateObjectFind(_fukusaGrove);
    }
    public static async Task DelayAsync(float secondDelay)
    {
        float startTime = Time.time;
        while (Time.time < startTime + secondDelay) await Task.Yield();


    }
}
