using UnityEngine;

public class CreepMovement : MonoBehaviour
{
    // Start is called before the first frame updateprivate Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    [SerializeField]
    Vector2 _movement;
    public float _movementSpeed = 2f;
    [SerializeField]
    float _rayLength = 0.6f;
    int layerBlocks;

    bool objectDown;
    bool objectUp;
    bool objectLeft;
    bool objectRight;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        layerBlocks = LayerMask.GetMask("Block");
        changeAxisMovement();
        updateAnimation();
    }

    private void Update()
    {
        Movement();

    }

    private void Movement()
    {
        if (CheckInminentCollision() || _movement == Vector2.zero)
        {
            _movement = Vector2.zero;
            checkCollisions();
            if (!objectUp || !objectDown || !objectRight || !objectLeft)
            {
                changeAxisMovement();
                updateAnimation();
            }
            else
            {
                animator.SetFloat("Speed", 0);
            }

        }
    }

    private void updateAnimation()
    {
        animator.SetFloat("Speed", 1);
        animator.SetFloat("Horizontal", _movement.x);
        animator.SetFloat("Vertical", _movement.y);
        if(_movement.x < 0){
            sr.flipX = true;
        }
        else {
            sr.flipX = false;
        }
    }

    private void checkCollisions()
    {
        objectDown = Physics2D.Raycast(transform.position, Vector2.down, _rayLength, layerBlocks);
        objectUp = Physics2D.Raycast(transform.position, Vector2.up, _rayLength, layerBlocks);
        objectLeft = Physics2D.Raycast(transform.position, Vector2.left, _rayLength, layerBlocks);
        objectRight = Physics2D.Raycast(transform.position, Vector2.right, _rayLength, layerBlocks);


        Debug.DrawRay(transform.position, Vector2.down * _rayLength, Color.green, 1);
        Debug.DrawRay(transform.position, Vector2.up * _rayLength, Color.blue, 1);
        Debug.DrawRay(transform.position, Vector2.left * _rayLength, Color.cyan, 1);
        Debug.DrawRay(transform.position, Vector2.right * _rayLength, Color.black, 1);

        Debug.Log("Layer: " + layerBlocks + ";Down: " + objectDown + "; Up: " + objectUp + "; Left: " + objectLeft + "; Right: " + objectRight);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + _movement.normalized * _movementSpeed * Time.deltaTime);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().ReduceHealth();
        }
        else if (other.gameObject.tag == "Enemy")
        {
            _movement = _movement * -1;
            updateAnimation();
        }

    }


    private bool CheckInminentCollision()
    {
        if (_movement.x != 0)
        {
            return Physics2D.Raycast(transform.position, Vector2.right * _movement.x, 0.5f, layerBlocks);


        }
        else if (_movement.y != 0)
        {
            return Physics2D.Raycast(transform.position, Vector2.up * _movement.y, 0.5f, layerBlocks);


        }
        return false;

    }

    private void changeAxisMovement()
    {
        int rand = Random.Range(0, 4);
        if (rand == 0 & !objectRight)
        {
            moveRight();
        }

        else if (rand == 1 & !objectUp)
        {
            moveUp();
        }
        else if (rand == 2 & !objectDown)
        {
            moveDown();
        }

        else if (rand == 3 & !objectLeft)
        {
            moveLeft();
        }
        else
        {
            changeAxisMovement();
        }



    }

    private void moveUp()
    {
        _movement.x = 0;
        _movement.y = 1;
        Debug.Log("Creep has changed movement to y positive");
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    private void moveDown()
    {
        _movement.x = 0;
        _movement.y = -1;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        Debug.Log("Creep has changed movement to y negative");
    }
    private void moveLeft()
    {
        _movement.x = -1;
        _movement.y = 0;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        Debug.Log("Creep has changed movement to x negative");
    }
    private void moveRight()
    {
        _movement.x = 1;
        _movement.y = 0;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        Debug.Log("Creep has changed movement to x positive");
    }
}
