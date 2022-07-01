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
    public Animator Animator => _animator;
    private AudioManager audioManager;
    private float currentTime, startTime, totalDistance;
    private List<Vector3Int> points;
    private int lastPos, pos;
    private Vector3Int currentGrid, nextGrid;
    private Vector3 auxMov = new Vector3(0.5f, 0.5f, 0.0f);

    public void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        // Inicia intentando movimiento hacia la derecha
        lastPos = 0;
        _animator.SetTrigger("IsMovRight");
        startTime = Time.time;
        currentGrid = nextGrid = tilemap.WorldToCell(transform.position);
        
        if(CanMove())
        {
            GetNextGrid();
            totalDistance = Vector3.Distance(currentGrid, nextGrid);
        }
    }

    void Update()
    {
        if (CanMove())
            Move();
    }
    
    private bool CanMove()
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
        
        CheckAvailableDirection(new Vector3Int(1,0,0), 0); // Derecha
        CheckAvailableDirection(new Vector3Int(0,-1,0), 1); // Abajo
        CheckAvailableDirection(new Vector3Int(-1,0,0), 2); // Izquierda
        CheckAvailableDirection(new Vector3Int(0,1,0), 3); // Arriba
        
        return points.Count > 0;
    }
    
    private void CheckAvailableDirection(Vector3Int direction, int pos)
    {
        Vector3Int cell = tilemap.WorldToCell(transform.position) + direction;
        Tile tile = tilemap.GetTile<Tile>(cell);
        
        if (tile != solidBlock && tile != explodableBlock /*|| Bomb*/)
        {
            points[pos] = cell;
        }
        else
        {
            points[pos] = currentGrid;
        }
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
            if (CanMove())
            {
                GetNextGrid();
                totalDistance = Vector3.Distance(currentGrid, nextGrid);
            }
        }
    }

    private void GetNextGrid()
    {
        pos = lastPos; // inicia intentando moverse en la dirección
                       // en la que venía moviendose
        while (points[pos] == currentGrid)
        {
            pos++;
            if (pos == points.Count) pos = 0;
        }
        nextGrid = points[pos];
        
        if (pos != lastPos) // Si la dirección del movimiento cambia,
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
        if (other.gameObject.tag == "Player")
        {
            audioManager.seleccionAudio(5, 1);
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
            GameManager.Instance.GameOver();
        }
    }
}
