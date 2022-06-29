using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Bomberman : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 1;
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

    [SerializeField]
    private float timeSpawnBomb = 1f;
    private float timeSpawnBombAux;

    private bool powerBomb;
    [SerializeField]
    private float timePowerBomb = 10f;
    private float auxTimePowerBomb;
    private AudioManager audioManager;
    
    private int cantidadEnemy;
    private HashSet<int> enemyDestroy = new HashSet<int>();



    void Start()
    {
        // _animator = GetComponents<Animator>();
        timeSpawnBombAux = timeSpawnBomb;
        timeSpawnBomb = 0f;
        powerBomb = false;
        auxTimePowerBomb = timePowerBomb;
        audioManager = FindObjectOfType<AudioManager>();

        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
            if (go.tag == "Enemy")
            {
                cantidadEnemy++;
            }

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(cantidadEnemy);
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

        Vector2 _dir = new Vector2(horizontal, vertical);
        _dir.Normalize();
        _velocity = _speed * _dir;
        timeSpawnBomb -= Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {

            if (timeSpawnBomb <= 0)
            {
                audioManager.seleccionAudio(4, 1);
                GameObject projectile = Instantiate(_bombPrefab);
                projectile.transform.position = _bombPoint.position;
                projectile.transform.rotation = _bombPoint.rotation;
                timeSpawnBomb = timeSpawnBombAux;
            }


        }
        if (powerBomb)
        {
            timePowerBomb -= Time.deltaTime;
            if (timePowerBomb < 0)
            {
                timePowerBomb = 0f;
                powerBomb = false;
            }
        }

        if (enemyDestroy.Count == cantidadEnemy)
        {
            audioManager.seleccionAudio(7, 1);
            gameObject.SetActive(false);
            GameManager.Instance.GameOver();
        }

    }

    public void SetSpeed(float speed)
    {
        this._speed = speed;
    }

    public float GetSpeed()
    {
        return this._speed;
    }

    private void FixedUpdate()
    {
        _rb.velocity = _velocity;
    }

    public void activaPowerUpBomb()
    {
        powerBomb = true;
        timePowerBomb = auxTimePowerBomb;
        Debug.Log(powerBomb);
        return;
    }

    public bool getPowerBomb()
    {
        return powerBomb;
    }

    public void enemyDestroyed(GameObject go)
    {
         enemyDestroy.Add(go.GetInstanceID());
    }

}
