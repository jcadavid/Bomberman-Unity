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

    int layerBlocks;



    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        layerBlocks = LayerMask.GetMask("Block");
        changeAxisMovement();
    }

    private void Update() {
        CheckCollision();
    }

   

    private void FixedUpdate() {
        rb.MovePosition(rb.position + _movement.normalized * _movementSpeed * Time.deltaTime);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Do something..
        }
        
    }
  

     private void CheckCollision()
    {
        if(_movement.x != 0){
            if(Physics2D.Raycast(transform.position,Vector2.right*_movement.x,0.5f,layerBlocks)){
                _movement.x = 0;
                changeAxisMovement();
                Debug.Log("Collision detected X");
            };            
        }
        else if(_movement.y != 0){
            if(Physics2D.Raycast(transform.position,Vector2.up*_movement.y,0.5f,layerBlocks)){
                _movement.y = 0;
                changeAxisMovement();
                Debug.Log("Collision detected Y");
            };            
        }
        
    }

    private void changeAxisMovement()
    {

        bool objectDown= Physics2D.Raycast(transform.position,Vector2.down,0.5f,layerBlocks);
        bool objectUp= Physics2D.Raycast(transform.position,Vector2.up,0.5f,layerBlocks);
        bool objectLeft= Physics2D.Raycast(transform.position,Vector2.left,0.5f,layerBlocks);
        bool objectRight= Physics2D.Raycast(transform.position,Vector2.right,0.5f,layerBlocks);
        

        Debug.DrawRay(transform.position,Vector2.down,Color.green,7);
        Debug.DrawRay(transform.position,Vector2.up,Color.green,7);
        Debug.DrawRay(transform.position,Vector2.left,Color.green,7);
        Debug.DrawRay(transform.position,Vector2.right,Color.green,7);

        Debug.Log("Layer: " + layerBlocks + ";Down: " + objectDown + "; Up: " + objectUp + "; Left: " + objectLeft + "; Right: " + objectRight);

        
        float rand = Random.Range(0,10);
        if (rand < 2.5f & !objectRight)
        {
            _movement.x = 1;
            _movement.y = 0;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            sr.flipX = false;
            Debug.Log("Creep has changed movement to x positive");
        }

        else if (rand < 5f & !objectUp)
        {
            _movement.x = 0;
            _movement.y = 1;
            Debug.Log("Creep has changed movement to y positive");
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        else if (rand < 7.5f & !objectDown)
        {
            _movement.x = 0;
            _movement.y = -1;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            Debug.Log("Creep has changed movement to y negative");
        }
       
        else if (rand < 10f & !objectLeft)
        {
            _movement.x = -1;
            _movement.y = 0;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            sr.flipX = true;
            Debug.Log("Creep has changed movement to x negative");
        }
        else{
            changeAxisMovement();
        }
        
        animator.SetFloat("Speed",1);
        animator.SetFloat("Horizontal",_movement.x);
        animator.SetFloat("Vertical",_movement.y);
        
        
    }
}
