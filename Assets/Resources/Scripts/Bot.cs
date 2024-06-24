using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Character
{ 
    IState<Bot> currentState;

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
