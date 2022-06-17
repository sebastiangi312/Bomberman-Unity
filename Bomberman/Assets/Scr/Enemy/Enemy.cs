using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 1;
    
    [HideInInspector]
    public bool mustPatrol;

    [SerializeField]
    private Rigidbody2D _rb;

    public float walkSpeed;

    public Collider2D bodyCollider;

    public LayerMask groundLayer;
    [SerializeField]
    private Animator _animator;
    private Vector2 _velocity;
    private bool r;
    private bool l;
    private bool u;
    private bool d;
    private float vertical;
    private float horizontal;

    public void Start()
    {
        vertical = 0.0f;
        horizontal = 1.0f;
        mustPatrol = true;
        r = true;
        l = false;
        d = false;
        u = false;
    }
    
    void Update()
    {
        if(mustPatrol)
        {
            Patrol();
        }
        
    }

    void Patrol() 
    {

        if(bodyCollider.IsTouchingLayers(groundLayer)) 
        {
            Flip();
        }
        
        Debug.Log(transform.position.x);
        Vector2 _dir  = new Vector2(horizontal, vertical);
        _dir.Normalize();
        _velocity = speed * _dir;
        
        //if (r || l)
        //{
        //    rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);    
        //}
        //else
        //{
        //    rb.velocity = new Vector2(rb.velocity.y, walkSpeed * Time.fixedDeltaTime);
        //}
        //rb.velocity = new Vector2(rb.velocity.x, walkSpeed * Time.fixedDeltaTime);
    }
    
    private void FixedUpdate()
    {   
        _rb.velocity = _velocity;  
    }

    void Flip()
    {
        /*
        if (r) Debug.Log("Right: True");
        else Debug.Log("Right: False");
        
        if (l) Debug.Log("Left: True");
        else Debug.Log("Left: False");
        
        if (u) Debug.Log("Up: True");
        else Debug.Log("Up: False");
        
        if (d) Debug.Log("Down: True");
        else Debug.Log("Down: False");
        */
        Vector3 a;
        Vector3 pos = transform.position;
        mustPatrol = false;
        if (r)
        {
            _animator.SetTrigger("IsMovDown");
            horizontal = 0.0f;
            vertical = -1.0f;
            a = new Vector3(transform.position.x - 0.05f, transform.position.y);
            transform.position = a;
            
            
            //pos +=  dir.y * speed * Time.deltaTime * transform.up; //X = V * T
            //pos +=  dir.x * speed * Time.deltaTime * transform.right;
        
            //Vector3 movement = speed * Time.deltaTime * dir;
            //movement = transform.rotation * movement;
            //pos += movement;
        
            //transform.position = pos;
            
            
            r = false;
            d = true;
        }
        else if (d)
        {
            _animator.SetTrigger("IsMovLeft");
            horizontal = -1.0f;
            vertical = 0.0f;
            a = new Vector3(transform.position.x, transform.position.y + 0.05f);
            transform.position = a;
            
            d = false;
            l = true;
        }
        else if (l)
        {
            _animator.SetTrigger("IsMovUp");
            horizontal = 0.0f;
            vertical = 1.0f;
            a = new Vector3(transform.position.x + 0.05f, transform.position.y);
            transform.position = a;
            
            l = false;
            u = true;
        }
        else if (u)
        {
            _animator.SetTrigger("IsMovRight");
            horizontal = 1.0f;
            vertical = 0.0f;
            a = new Vector3(transform.position.x, transform.position.y - 0.05f);
            transform.position = a;
            
            u = false;
            r = true;
        }
        mustPatrol = true;
    }
}
