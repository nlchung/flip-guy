using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _acceleration = 1.5f;
    [SerializeField] private float _maxVelocity = 25;
    [SerializeField] private float _currVelocity = 0;
    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode right;
    private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // add acceleration to velocity
        AccelerateX();
        // make sure velocity isn't over thet maximum
        if (_currVelocity >= _maxVelocity)
        {
            _currVelocity = _maxVelocity;
        } 
        else if (_currVelocity <= -_maxVelocity)
        {
            _currVelocity = -_maxVelocity;
        }
        // move character
        _rb.velocity = new Vector2(_currVelocity, _rb.velocity.y);
        

    }

    void AccelerateX()
    {
        if (Input.GetKey(left))
        {
            _currVelocity -= _acceleration;
        }
        else if (Input.GetKey(right))
        {
            _currVelocity += _acceleration;
        } 
        else {
            if (_currVelocity > 0 && _currVelocity != 0)
            {
                _currVelocity -= _acceleration;
            }
            else if (_currVelocity < 0 && _currVelocity != 0)
            {
                _currVelocity += _acceleration;
            }
        }
    }
}
