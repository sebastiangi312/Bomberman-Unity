using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    private float _moveToPatrolAt = 0;
    
    public void OnEnter(Enemy enemy)
    {
        Debug.Log("Idle: OnEnter");
        _moveToPatrolAt = Time.time + enemy.EnemyConfig.IdleTime;
    }

    public void OnUpdate(Enemy enemy)
    {
        Debug.Log("Idle: OnUpdate");
        
        //After X sec -> To patrol
        if (Time.time > _moveToPatrolAt)
        {
            enemy.StateMachineController.ChangeToState(EnemyStateType.Patrol);
            return;
        }
        
    }

    public void OnExit(Enemy enemy)
    {
        Debug.Log("Idle: OnExit");
    }
}
