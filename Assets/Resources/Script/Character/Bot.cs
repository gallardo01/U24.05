using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    private IState<Bot> currentState;
    private NavMeshAgent agent;

    protected override void OnInit()
    {
        base.OnInit();
        agent = GetComponent<NavMeshAgent>();
    }

    public override void Move()
    {
        agent.SetDestination(moveDirection);
        if((transform.position - moveDirection).magnitude < 0.1f)
        {
            moveDirection = transform.position + Random.insideUnitSphere.normalized * detectRadius;
                //RandomPoint(transform.position, 10, out Vector3 result);
        }
    }


    private Vector3 RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return result;
            }
        }
        result = Vector3.zero;
        return result;
    }

    public void ChangeState(IState<Bot> newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
}
