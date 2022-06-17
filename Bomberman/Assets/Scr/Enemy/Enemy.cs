using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Space(20)]
    
    private PathFindingController _pathFindingController;

    [SerializeField]
    private List<Transform> _pathPoints;

    private Vector3 _nextPoint;
    private int _currentPointIndex = 0;
    
    public void Start()
    {
        _pathFindingController = GetComponent<PathFindingController>();
        _nextPoint = GetEnemyNextPoint();
        Debug.Log(_nextPoint);
        this._pathFindingController.GoTo(_nextPoint);
    }
    
    void Update()
    {
        float distance = (_nextPoint - transform.position).magnitude;

        if(distance <= 0.01f) 
        {
            _nextPoint = GetEnemyNextPoint();
            this._pathFindingController.GoTo(_nextPoint);
        }
        
    }

    private Vector3 GetEnemyNextPoint()
    {
        if (this._pathPoints == null || this._pathPoints.Count == 0)
        {
            return Vector3.zero;
        }
        
        _currentPointIndex = (_currentPointIndex + 1) % this._pathPoints.Count;
        return this._pathPoints[_currentPointIndex].position;
    }
}
