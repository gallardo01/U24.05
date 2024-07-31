using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public NavMeshAgent agent;
    private IState<Bot> currentState;
    public GameObject targetCircle;

    // Start is called before the first frame update
    void Start()
    {
        ChangeAnim("idle");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null && !isDead)
        {
            currentState.OnExecute(this);
        }
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
    

    public void SetTarget()
    {
        targetCircle.transform.position = transform.position;
        targetCircle.SetActive(true);
    }
    
    
    public void RemoveTarget()
    {
        targetCircle.SetActive(false);
    }

    public void Stop()
    {
        agent.enabled = false;
        OnInit();
    }
    
    public override void OnDeath()
    {
        ChangeState(null);
        agent.enabled = false;
        RemoveTarget();
        // Bot chet
        base.OnDeath();
    }
    
    public override void OnAttack()
    {
        base.OnAttack();
    }
    
    public override void OnInit()
    {
        ChangeState(new IdleState());
        targetCircle.SetActive(false);
        base.OnInit();
    }
}
