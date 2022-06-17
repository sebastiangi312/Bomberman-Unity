using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public bool mustPatrol;

    public Rigidbody2D rb;

    public float walkSpeed;

    public Collider2D bodyCollider;

    public LayerMask groundLayer;
    
    public void Start()
    {
        mustPatrol = true;
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
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
        //rb.velocity = new Vector2(rb.velocity.x, walkSpeed * Time.fixedDeltaTime);
    }

    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
    }
}
