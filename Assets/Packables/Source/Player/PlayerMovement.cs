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
    public float _maxSpeed = 6f;
    [SerializeField]
    public float _additionalSpeed = 1.5f;

    public bool _doAnimation;    

    Vector3 rotation;

    Vector3 scaleChange = new Vector3(-0.0005f, -0.0005f, -0.0005f);


    public float rotationSpeed = 60f;

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
        if (_doAnimation)
        {
            doAnimation();
        }
        else
        {
            UpdateMovement();
        }
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
        if (_movementSpeed < _maxSpeed)
        {   
            _movementSpeed += _additionalSpeed;
            Invoke("ReduceSpeed", 20f);
        }
    }
    private void ReduceSpeed()
    {
        _movementSpeed -= _additionalSpeed;
    }

    public void disappearAnimation(Vector3 portalPostion)
    {
        _doAnimation = true;

        transform.position = portalPostion;
        _movement = Vector2.zero;
        _movementSpeed = 0;

        rb.isKinematic = true;
    }

    public void doAnimation()
    {

        rotation.z = rotationSpeed * Time.deltaTime;
        transform.Rotate(rotation, Space.Self);
        transform.localScale += scaleChange;
        if (transform.localScale.x <= 0)
        {
            Destroy(gameObject);
        }
    }

}
