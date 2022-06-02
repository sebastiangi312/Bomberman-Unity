using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStateType 
{
    Idle,
    Patrol
}


public interface IEnemyState 
{
    public void OnEnter(Enemy enemy);
    public void OnUpdate(Enemy enemy);
    public void OnExit(Enemy enemy);
}
