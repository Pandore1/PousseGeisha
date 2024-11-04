using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TeaCeremony : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject[] _itemsList;
    [SerializeField] private TMPro.TMP_Text _objectFoundTxt;
    [SerializeField] private TMPro.TMP_Text _totalObjectFindTxt;
    private int _totalObjectNb;
    private int _objectFoundNb;
   

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
            //Debug.Log(itemName.text);
            if (itemName.text == objectName) {
               
                item.gameObject.SetActive(false);
                _objectFoundNb++;
                if (_objectFoundNb == _totalObjectNb)
                {
                   // Debug.Log("Yeah!");
                    //GameManager.Instance.LevelBar.XpGain();
                }
                break;
            }
        }
    }
}
