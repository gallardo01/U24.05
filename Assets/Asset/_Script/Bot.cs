
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Bot : Character
{
    public Vector3 target;
    public NavMeshAgent agent;
    //public bool isDestination => Vector3.Distance(target,Vector3) < 0.1f;
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
        int random = Random.Range(7, 15);
        return random;
    }
    public void RunToNextTarget(Vector3 target)
    {
        agent.SetDestination(target);
    }

    new private void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Stage"))
        {
            target = other.GetComponent<StartStage>().stage.SetRandomFinishPoint().position;
            currentState.OnEnter(this);
        }
    }
}
