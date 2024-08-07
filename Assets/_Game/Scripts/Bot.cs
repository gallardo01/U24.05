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

    public override void InitCharacter(WeaponType weaponType, int level)
    {
        base.InitCharacter(weaponType, level);
        characterInfo.UpdateTextName("Bot_" + Random.Range(0, 9999).ToString());
        agent.speed = MoveSpeed;
        ChangeState(new WaitState());
    }

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        agent.SetDestination(destination);
    }

    public override void StopMove()
    {
        base.StopMove();
        ChangeState(null);
        SetDestination(tf.position);
    }

    protected override IEnumerator IEDead()
    {
        yield return StartCoroutine(base.IEDead());

        ResetCharacter();
        SimplePool.Despawn(this);
    }
}
