using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalligraphyPoint : MonoBehaviour
{
    public bool StartLine;
    public int nbLine;
 
    public Sprite CollidedSprite;
    public SpriteRenderer nonCollided;
    // Start is called before the first frame update
    void Start()
    {
        nonCollided = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
}
