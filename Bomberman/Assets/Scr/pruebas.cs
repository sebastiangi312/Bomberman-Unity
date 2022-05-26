using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pruebas : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float speed = 2;
    [SerializeField]

    private Vector2 _velocity;

    [SerializeField]
    private Rigidbody2D _rb;

    [SerializeField]
    private GameObject _projectilePrefab;

    [SerializeField]
    private Transform _shootPoint;
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
            //Shoot
            GameObject projectile = Instantiate(_projectilePrefab);
            projectile.transform.position = _shootPoint.position;
            projectile.transform.rotation = _shootPoint.rotation;
            //projectile.transform.up = _shootPoint.up;
        }
    }

    private void FixedUpdate()
    {
     
        _rb.velocity = _velocity;
       
    }
}
