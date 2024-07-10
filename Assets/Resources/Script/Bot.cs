using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    IState<Bot> currentState;
    public NavMeshAgent agent;
    //dynamic check distance, 
    //only check distance depend on X, Z axis (Y = 0) 
    public bool isDestination => Vector3.Distance(destination, Vector3.right * transform.position.x + Vector3.forward * transform.position.z) < 0.1f; 
    private Vector3 destination;

    public bool isRotate = true;

    private void Start()
    {
        ChangeState(new IdleState());
    }

    public void SetDestination(Vector3 position)
    {
        agent.enabled = true;
        destination = position;
        destination.y = 0;
        agent.SetDestination(position);
    }    

    private void Update()
    {
        currentState.OnExecute(this);
        //CanMove(transform.position);
        if (!CanMove(transform.position) && totalBricks == 0 && isRotate) 
        {
            isRotate = false;
            ChangeState(new IdleState());
        }
    }

    public void ChangeState(IState<Bot> state)
    {
        Debug.Log($"{gameObject.name}: {currentState} - {state}");
        if(currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = state; 
        if(currentState != null)
        {
            currentState.OnEnter(this);
        }
    }    

}
