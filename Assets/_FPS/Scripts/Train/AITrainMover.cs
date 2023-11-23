using System;
using UnityEngine;
using UnityEngine.AI;

public class AITrainMover : MonoBehaviour
{
    public NavMeshAgent agent;

    public event Action OnReachPoint;
    private Vector3 _currentPointToMove;


    public void MoveToPoint(Vector3 point)
    {
        _currentPointToMove = point;
        agent.SetDestination(point);
    }
    private void Update()
    {
        if (agent.remainingDistance < 0.1f)
        {
            OnReachPoint?.Invoke();
        }
    }
}
