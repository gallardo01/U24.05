using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    private IState<Bot> currentState;   

    [SerializeField] NavMeshAgent agent;

    private Vector3 destination;
    public bool IsDestionation => Vector3.Distance(tf.position, destination + (tf.position.y - destination.y) * Vector3.up) < 0.1f;


    protected override void Update()
    {
        base.Update();

        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
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

    public override void InitCharacter(Transform NodeStart, WeaponType weaponType, int level)
    {
        base.InitCharacter(NodeStart, weaponType, level);
        agent.speed = MoveSpeed;
        ChangeState(new MoveState());
    }

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        agent.SetDestination(destination);
    }

    public override void ResetCharacter()
    {
        base.ResetCharacter();
        SetDestination(tf.position);
        targetedImage.SetActive(false);
    }
    public override void OnDead()
    {
        base.OnDead();      
        ResetCharacter();
        SimplePool.Despawn(this);
    }
}
