using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomberman : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float speed = 1;
    [SerializeField]

    private Vector2 _velocity;

    [SerializeField]
    private Rigidbody2D _rb;

    [SerializeField]
    private GameObject _bombPrefab;

    [SerializeField]
    private Transform _bombPoint;
    
    [SerializeField]
    private Animator _animator;
    
    void Start()
    {
        // _animator = GetComponents<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        if (vertical != 0.0f)
        {
            if (vertical < 0) _animator.SetTrigger("IsMovDown");
            else if (vertical > 0) _animator.SetTrigger("IsMovUp");
        } 
        else if (horizontal != 0.0f)
        {
            if (horizontal < 0) _animator.SetTrigger("IsMovLeft");
            else if (horizontal > 0) _animator.SetTrigger("IsMovRight");
        }
        _animator.SetBool("IsNotMoving", horizontal == 0.0f && vertical == 0.0f);
        
        Vector2 _dir  = new Vector2(horizontal, vertical);
        _dir.Normalize();
        _velocity = speed * _dir;

        if (Input.GetButtonDown("Jump"))
        {
            
            GameObject projectile = Instantiate(_bombPrefab);
            projectile.transform.position = _bombPoint.position;
            projectile.transform.rotation = _bombPoint.rotation;
            
        }
    }

    private void FixedUpdate()
    {   
        _rb.velocity = _velocity;  
    }
}
