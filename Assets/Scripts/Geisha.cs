using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geisha : MonoBehaviour
{
    [SerializeField] private float _walkingSpeed = 1f;
    [SerializeField] private float _brakePower = 2f;
    private Animator _animator;
    private Rigidbody2D _body;
    private float _dirX = 0f;
    private float _dirY = 0f;
    private bool _isWalking = false;
 
    
    // Start is called before the first frame update
    void Start()
    {

        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _dirX = Input.GetAxisRaw("Horizontal");
        _dirY = Input.GetAxisRaw("Vertical");
        _isWalking = _dirX != 0||_dirY!=0;
        
        float scaleX = transform.localScale.x;
           if (_dirY == -1)
        {
            _animator.SetBool("WalkingUp", _isWalking);
        }
        else if (_dirY == 1)
        {
            _animator.SetBool("WalkingDown", _isWalking);
        }
        if (_dirX != 0)
        {
            _animator.SetBool("WalkingSide", _isWalking);
        }  
    else if(_dirX==1)
        {
            scaleX = -1f;
         
        }
        else if (_dirX ==-1)
        {
            scaleX = 1f;
        
        }
     
        transform.localScale = new Vector3(scaleX, 1, 1);
    }
    private void FixedUpdate()
    {
        if (_isWalking){
            //_body.AddForce(Vector2.right * _dirX * _walkingSpeed, ForceMode2D.Impulse);
            _body.velocity = new Vector2(_dirX*_walkingSpeed,_dirY*_walkingSpeed);

        }
        else
        {
            _body.velocity = new Vector2(Mathf.Lerp(_body.velocity.x, 0, _brakePower * Time.deltaTime),Mathf.Lerp (_body.velocity.y, 0, _brakePower * Time.deltaTime));
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Furniture")
        {

        }
    }
}
