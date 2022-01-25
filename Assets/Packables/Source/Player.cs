using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public float _movementSpeed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;
       
    Vector2 _movement;

    


    [SerializeField]
    public int _hp = 1;


    private void Start()
    {
        Init();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + _movement.normalized * _movementSpeed * Time.deltaTime);
    }

    void Update()
    {
       /* if (Input.GetKeyDown(KeyCode.Space))
        {
            BombermanEvent.OnCreateBombEvent?.Invoke(this);
        }*/
        UpdateMovement();        
    }

     private void Init()
    {
        rb=GetComponent<Rigidbody2D>();
        sr= GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

    }
    private void UpdateMovement(){
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
        if(_movement.x < 0){
            sr.flipX = true;
        }
        else{
            sr.flipX = false;
        }
        animator.SetFloat("Horizontal", _movement.x);
        animator.SetFloat("Vertical", _movement.y);
        animator.SetFloat("Speed", _movement.sqrMagnitude);
    }    
}
