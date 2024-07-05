using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Character
{
    public Vector3 target;
    IState<Bot> currentState;

    private void Start()
    {
        ChangeState(new IdleState());
    }

    private void Update()
    {
        currentState.OnExecute(this);
    }

    public void ChangeState(IState<Bot> state)
    {
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
