using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IEnemyState
{
    private int _currentPointIndex = 0;
    
    public void OnEnter(Enemy enemy)
    {
    }

    public void OnUpdate(Enemy enemy)
    {
        if (enemy.PathFindingController.IsStopped)
        {
            enemy.PathFindingController.GoTo(GetEnemyNextPoint(enemy), () =>
            {
                enemy.StateMachineController.ChangeToState(EnemyStateType.Idle);
            });
        }
    }
    
    public void OnExit(Enemy enemy)
    {
        enemy.PathFindingController.Stop();
    }
    
    
    private Vector3 GetEnemyNextPoint(Enemy enemy)
    {
        if (enemy.EnemyConfig.PathPoints == null || enemy.EnemyConfig.PathPoints.Count == 0)
        {
            return Vector3.zero;
        }
        
        _currentPointIndex = (_currentPointIndex + 1) % enemy.EnemyConfig.PathPoints.Count;
        return enemy.EnemyConfig.PathPoints[_currentPointIndex].position;
    }
}
