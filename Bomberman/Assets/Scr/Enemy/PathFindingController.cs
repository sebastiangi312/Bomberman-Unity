using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathFindingController : MonoBehaviour
{
    private NavMeshAgent _agent;
    
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {     }

    public void GoTo(Vector3 position)
    {
        _agent.SetDestination(position);
    }

    public void Stop()
    {
        _agent.isStopped = true;
        _agent.velocity = Vector3.zero;
    }
}
