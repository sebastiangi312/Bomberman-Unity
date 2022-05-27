using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomberman : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float speed = 2;
    [SerializeField]

    private Vector2 _velocity;

    [SerializeField]
    private Rigidbody2D _rb;

    [SerializeField]
    private GameObject _bombPrefab;

    [SerializeField]
    private Transform _bombPoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

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
