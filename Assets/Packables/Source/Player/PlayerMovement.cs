using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;
    [SerializeField]
    Vector2 _movement;
    [SerializeField]
    public float _movementSpeed = 3f;
    [SerializeField]
    public float _additionalSpeed = 2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + _movement.normalized * _movementSpeed * Time.deltaTime);
    }

    void Update()
    {
        UpdateMovement();
    }
    private void UpdateMovement()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
        if (_movement.x < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
        animator.SetFloat("Horizontal", _movement.x);
        animator.SetFloat("Vertical", _movement.y);
        animator.SetFloat("Speed", _movement.sqrMagnitude);
    }

    public void BoostSpeed()
    {
        _movementSpeed += _additionalSpeed;
        Invoke("ReduceSpeed", 20f);
    }
    private void ReduceSpeed()
    {
        _movementSpeed -= _additionalSpeed;
    }
}
