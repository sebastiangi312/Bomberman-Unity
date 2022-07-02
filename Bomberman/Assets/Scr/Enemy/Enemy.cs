using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private Tilemap tilemap;
    [SerializeField]
    private Tile solidBlock;
    [SerializeField]
    private Tile explodableBlock;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Rigidbody2D _rb;
    public Animator Animator => _animator;
    private AudioManager audioManager;
    [SerializeField]
    private Bomberman player;
    private bool r;
    private bool l;
    private bool u;
    private bool d;
    private float vertical;
    private float horizontal;

    private bool imDead = false;
    private float timeDead = 1f;

    private float currentTime, startTime, totalDistance;
    private List<Vector3Int> points;
    private bool isMoving;
    private int lastPos, pos;
    private Vector3Int currentGrid, nextGrid;
    private Vector3 auxMov = new Vector3(0.5f, 0.5f, 0.0f);
    public void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        startTime = Time.time;
        currentGrid = nextGrid = tilemap.WorldToCell(transform.position);
        
        if(CanMove(false))
        {
            isMoving = true;
            lastPos = -1;
            GetNextGrid();
            totalDistance = Vector3.Distance(currentGrid, nextGrid);
        }
        else
        {
            isMoving = false;
            _animator.SetBool("IsNotMoving", true);
        }
    }

    void Update()
    {
        if (isMoving)
            Move();
        else
        {
            if (CanMove(false))
            {
                isMoving = true;
                lastPos = -1;
                GetNextGrid();
                totalDistance = Vector3.Distance(currentGrid, nextGrid);
            }
        }

        if (imDead)
        {
            timeDead -= Time.deltaTime;
        }
        if (timeDead <= 0)
        {
            Destroy(gameObject);
        }

    }
    
    private bool CanMove(bool bomb)
    {
        points = new List<Vector3Int>();
        /*
         * Este arreglo tiene las cuatro pocisiones a las que
         * podría ir el enemigo en este orden:
         * 0 - Derecha
         * 1 - Abajo
         * 2 - Izquierda
         * 3 - Arriba
         * Todas las posiciones inician con el valor actual
         * para diferenciar de cuando pueda pasar o cuando no.
         */
        points.Add(currentGrid);
        points.Add(currentGrid);
        points.Add(currentGrid);
        points.Add(currentGrid);
        int availablePoints = 0;
        availablePoints += CheckAvailableDirection(new Vector3Int(1,0,0), 0) + // Derecha
                           CheckAvailableDirection(new Vector3Int(0,-1,0), 1) + // Abajo
                           CheckAvailableDirection(new Vector3Int(-1,0,0), 2) + // Izquierda
                           CheckAvailableDirection(new Vector3Int(0,1,0), 3); // Arriba
        if (bomb)
        {
            availablePoints--;
            points[lastPos] = currentGrid;
        }
        Vector2 _dir = new Vector2(horizontal, vertical);
        _dir.Normalize();
        _velocity = speed * _dir;

        return availablePoints > 0;
    }
    
    private int CheckAvailableDirection(Vector3Int direction, int pos)
    {
        Vector3Int cell = tilemap.WorldToCell(transform.position) + direction;
        Tile tile = tilemap.GetTile<Tile>(cell);
        
        if (tile != solidBlock && tile != explodableBlock)
        {
            points[pos] = cell;
            return 1;
        }
        return 0;
    }

    private void Move()
    {
        /*
         * La variable "auxMov" centra al enemigo en la Grid correspondiente
         */
        if (Vector3.Distance(transform.position - auxMov, (Vector3)nextGrid) > 0.1f)
        {
            currentTime = Time.time;
            float distanceCovered = (currentTime - startTime) * speed;
            float fraction = distanceCovered / totalDistance;
            transform.position = Vector3.Lerp(currentGrid, nextGrid, fraction) + auxMov;
        }
        else
        {
            startTime = Time.time;
            transform.position = (Vector3) nextGrid + auxMov;
            currentGrid = nextGrid;
            if (CanMove(false))
            {
                GetNextGrid();
                totalDistance = Vector3.Distance(currentGrid, nextGrid);
            }
            else
            {
                isMoving = false;
                _animator.SetBool("IsNotMoving", true);
            }
        }
    }
    private void GetNextGrid()
    {
        if (lastPos == -1) pos = 0;
        else pos = lastPos;  // inicia intentando moverse en la dirección
                             // en la que venía moviendose
        while (points[pos] == currentGrid)
        {
            pos = Random.Range(0, points.Count);
        }
        nextGrid = points[pos];
        
        if (pos != lastPos || lastPos == -1) // Si la dirección del movimiento cambia
                                             // o si es la primera vez que se ejecuta,
                                             // también cambia la animación
            ChangeAnimation();
    }

    private void ChangeAnimation()
    {
        if (pos == 0)
            _animator.SetTrigger("IsMovRight");
        else if (pos == 1)
            _animator.SetTrigger("IsMovDown");
        else if (pos == 2)
            _animator.SetTrigger("IsMovLeft");
        else if (pos == 3)
            _animator.SetTrigger("IsMovUp");
        lastPos = pos;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Player")

        {
            other.gameObject.GetComponent<Bomberman>().bombermanDead();
        }
        else if (other.gameObject.tag == "Bomb")
        {
            currentGrid = tilemap.WorldToCell(transform.position);
            if(CanMove(true))
            {
                isMoving = true;
                GetNextGrid();
                totalDistance = Vector3.Distance(currentGrid, nextGrid);
            }
            else
            {
                isMoving = false;
                _animator.SetBool("IsNotMoving", true);   
            }
        }
    }

    public void enemyDead()
    {
        imDead = true;
        audioManager.seleccionAudio(1, 1);
        _animator.SetTrigger("IsDeathing");
        GetComponent<CapsuleCollider2D>().enabled = false;
    }
}
