using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float horizontalInput;

    // runs every time you load
    private void Awake () {
        // get references to components from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // runs every frame
    private void Update() {
        horizontalInput = Input.GetAxis("Horizontal");


        // flips player to face left / right when changing direction
        if (horizontalInput > 0.01f) {
            transform.localScale = Vector3.one;
        } else if (horizontalInput < -0.01f) {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Set animator parameters
        anim.SetBool("grounded", isGrounded());

        // set player's velocity
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        
        if (!isGrounded()) {
            body.gravityScale = 0;
            body.velocity = Vector2.zero;
        } else {
            body.gravityScale = 1;
        }

        if(Input.GetKey(KeyCode.Space)) {
            Jump();
        }
    }

    private void Jump() {
        if (isGrounded()) {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        
    }

    // check if box cast with ground layer has occured
    private bool isGrounded() {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        
        return raycastHit.collider != null;
    }
}
