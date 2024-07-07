
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Bot : Character
{
    public Vector3 target;
    public NavMeshAgent agent;
    IState<Bot> currentState;

    private void Start()
    {
        ChangeState(new IdleState());
    }
    private void Update()
    {
        if (backPack.childCount == 0)
        {
            currentState.OnEnter(this);
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
    public int PickRandomBrick()
    {
        int random = Random.RandomRange(7, 15);
        return random;
    }
    public void RunToNextFloor()
    {
         agent.SetDestination(target);
        Debug.Log("daden");
    }

    new private void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Stage"))
        {
            target = other.GetComponent<StartStage>().stage.SetRandomFinishPoint().position;
            currentState.OnEnter(this);
            Debug.Log("lenlevel 2");
        }
    }
}
