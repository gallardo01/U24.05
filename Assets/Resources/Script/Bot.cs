using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public NavMeshAgent agent;
    private IState<Bot> currentState;
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

    public void ChangeIsAttackBot()
    {
        Invoke("ResetAttack", 1f);
    }
    
    private void ResetAttack()
    {
        isAttack = false;
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

    public void OnDestroy()
    {
        indicator.gameObject.SetActive(false);
    }
}
