using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public NavMeshAgent agent;
    public Vector3 destination;
    public IState<Bot> currentState;

    public bool isDestination => Vector3.Distance
        (destination, Vector3.right*transform.position.x + Vector3.forward*transform.position.z ) < 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        ChangeState(new IdleState());
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnExecute(this);
    }

    public void ChangeState(IState<Bot> newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        if (currentState != newState)
        {
            currentState = newState;
            currentState.OnEnter(this);
        }
    }
    public void SetDestination(Vector3 des)
    {
        agent.enabled = true;
        destination = des;
        agent.SetDestination(des);
        destination.y = 0f;
    }

    public void ChangeIsAttackBot()
    {
        Invoke(nameof(ResetAttack), 1f);
    }   
    
    private void ResetAttack()
    {
        isAttack = false;
    }    
}
