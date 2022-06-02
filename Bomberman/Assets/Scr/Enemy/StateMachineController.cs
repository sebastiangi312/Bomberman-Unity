using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineController
{
    private Dictionary<EnemyStateType, IEnemyState> _states;

    private Enemy _enemy;
    private EnemyStateType _currentState = EnemyStateType.Idle;
    
    public void Init(Enemy enemy)
    {
        _enemy = enemy;

        _states = new Dictionary<EnemyStateType, IEnemyState>();
        _states.Add(EnemyStateType.Idle, new EnemyIdleState());
        _states.Add(EnemyStateType.Patrol, new EnemyPatrolState());
        
        ChangeToState(_enemy.EnemyConfig.initialState);
    }

    
    public void OnUpdate()
    {
        if (_states.ContainsKey(_currentState))
        {
            IEnemyState state = _states[_currentState];
            state.OnUpdate(_enemy); 
        }
    }

    public void ChangeToState(EnemyStateType newState)
    {
        if (_states.ContainsKey(_currentState))
        {
            IEnemyState state = _states[_currentState];
            state.OnExit(_enemy);
        }
        
        _currentState = newState;

        if (_states.ContainsKey(_currentState))
        {
            IEnemyState state = _states[_currentState];
            state.OnEnter(_enemy);
        }
    }
}
