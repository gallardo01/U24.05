using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Bot : Character
{
    private IState currentState;
    private NavMeshAgent agent;
    [HideInInspector] public Vector3 movePos;

    protected override void OnInit()
    {
        base.OnInit();
        agent = GetComponent<NavMeshAgent>();
        movePos = GetRandomPoint(transform.position, detectRadius * 2);
        ChangeState(new IdleState());
    }

    public void Move()
    {
        agent.isStopped = false;
        agent.SetDestination(movePos);
        ChangAnim("run");
    }

    public void Stop()
    {
        agent.isStopped = true;
    }

    private void Update()
    {
        Debug.Log(this.gameObject.name + " - " + currentState);
        if(currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    public Vector3 GetRandomPoint(Vector3 center, float range)
    {
        Vector3 result = Vector3.zero;
        
        for (int i = 0; i < 10; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 10f, NavMesh.AllAreas))
            {
                result = hit.position;
                return result;
            }
        }
        return result;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        ChangeState(null);
    }

    public void ChangeState(IState newState)
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
