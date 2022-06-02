using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[Serializable]
public class EnemyConfig 
{
    public EnemyStateType initialState = EnemyStateType.Idle;

    [Header("Moving")]
    public float IdleTime = 1;
    public float PathfindingRefreshTime = 1;
    public List<Transform> PathPoints;

}
