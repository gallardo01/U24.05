using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{ 
    IState<Bot> currentState;
    public NavMeshAgent agent;

    private Vector3 destination;
    public bool isDestination => Vector3.Distance(destination, Vector3.right * transform.position.x + Vector3.forward * transform.position.z) < 0.2f;
    public bool isRotate = true;

    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        currentState.OnExecute(this);
        if (!CanMove(transform.position) && totalBrick == 0 && isRotate)
        {
            isRotate = false;
            ChangeState(new IdleState());
        }
    }

    public void ChangeState(IState<Bot> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = state;
        if (currentState != null) 
        {
            currentState.OnEnter(this); 
        }
    }
}
