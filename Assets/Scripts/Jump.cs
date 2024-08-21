using Unity.VisualScripting;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private float _velocity = 1.5f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private KeyCode jump;

    private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(jump) && IsGrounded())
        {
            _rb.velocity = Vector2.up * _velocity;
        }
    }

    private bool IsGrounded(){
        return Physics2D.OverlapCircle(_groundCheck.position, 0.2f, groundLayer);
    }
}
