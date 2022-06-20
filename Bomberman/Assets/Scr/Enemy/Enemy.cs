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
        r = true; // Inicia moviendose a la derecha
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
        
        Vector2 _dir  = new Vector2(horizontal, vertical);
        _dir.Normalize();
        _velocity = speed * _dir;
    }
    
    private void FixedUpdate()
    {   
        _rb.velocity = _velocity;  
    }

    void Flip()
    {
        Vector3 auxMov;
        Vector3 pos = transform.position;
        mustPatrol = false;
        if (r)
        {
            _animator.SetTrigger("IsMovDown");
            horizontal = 0.0f;
            vertical = -1.0f;
            auxMov = new Vector3(transform.position.x - 0.05f, transform.position.y);
            transform.position = auxMov;
            
            r = false;
            d = true;
        }
        else if (d)
        {
            _animator.SetTrigger("IsMovLeft");
            horizontal = -1.0f;
            vertical = 0.0f;
            auxMov = new Vector3(transform.position.x, transform.position.y + 0.05f);
            transform.position = auxMov;
            
            d = false;
            l = true;
        }
        else if (l)
        {
            _animator.SetTrigger("IsMovUp");
            horizontal = 0.0f;
            vertical = 1.0f;
            auxMov = new Vector3(transform.position.x + 0.05f, transform.position.y);
            transform.position = auxMov;
            
            l = false;
            u = true;
        }
        else if (u)
        {
            _animator.SetTrigger("IsMovRight");
            horizontal = 1.0f;
            vertical = 0.0f;
            auxMov = new Vector3(transform.position.x, transform.position.y - 0.05f);
            transform.position = auxMov;
            
            u = false;
            r = true;
        }
        mustPatrol = true;
    }
}
