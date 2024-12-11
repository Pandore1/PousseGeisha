using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalligraphyPoint : MonoBehaviour
{
    public bool StartLine;
    public int nbLine;
 
    public Sprite CollidedSprite;
    public Sprite NoCollideSprite;
    public SpriteRenderer nonCollided;
    // Start is called before the first frame update
    void Start()
    {
        nonCollided = GetComponent<SpriteRenderer>();
        nonCollided.sprite = NoCollideSprite;
    }
   
 

    // Update is called once per frame
}
