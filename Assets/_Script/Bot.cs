using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Bot : Character
{
    IState<Bot> currentState;
    public NavMeshAgent agent;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        this.HPbar.GetComponent<HPbar>().SetHP();
        currentState = new IdleState();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (isAttack == false)
        {
            ChangeState(new MoveState());
        } else
        {
            currentState.OnEnter(this);
        }
        if (time > 0.9f)
        {
            isAttack = false;
        }

        currentState.OnExecute(this);
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
    public void FindTarget()
    {
        int random = Random.Range(0, GameController.instance.summonPoint.Count);
        target = GameController.instance.summonPoint[random];
    }
}
