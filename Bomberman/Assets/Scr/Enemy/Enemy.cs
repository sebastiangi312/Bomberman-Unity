using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Space(20)]

    [SerializeField]
    private EnemyConfig _enemyConfig;
    
    private PathFindingController _pathFindingController;
    private StateMachineController _stateMachineController;
    
    public EnemyConfig EnemyConfig => _enemyConfig;
    public PathFindingController PathFindingController => _pathFindingController;
    public StateMachineController StateMachineController => _stateMachineController;
    
    public void Start()
    {
        _pathFindingController = GetComponent<PathFindingController>();
        _stateMachineController = new StateMachineController();
        _stateMachineController.Init(this);      
    }
    
    void Update()
    {
        _stateMachineController.OnUpdate();
    }
}
