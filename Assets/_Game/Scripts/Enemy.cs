using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    private IState<Enemy> currentState;

    public Vector3 targetBridgePos = Vector3.zero;

    public List<Vector3> targetPos = new List<Vector3>();

    [SerializeField] NavMeshAgent agent;

    private Vector3 destination;
    public bool IsDestionation => Vector3.Distance(tf.position, destination + (tf.position.y - destination.y) * Vector3.up) < 0.1f;

    public Vector3 NextPoint => nextPoint;

    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }

        //nextPoint = tf.position + tf.forward * Time.deltaTime * moveSpeed;
        nextPoint = tf.position + tf.forward * 0.5f;
    }

    public void ChangeState(IState<Enemy> newState)
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

    public override void OnInit(GameColor color, Transform transform)
    {
        base.OnInit(color, transform);
        ChangeState(new IdleState());
    }

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        agent.SetDestination(destination);
    }

    public void ResetPath()
    {
        agent.ResetPath();
        agent.velocity = Vector3.zero;
    }

    protected override void NextFloor(Gate gate)
    {
        base.NextFloor(gate);
        targetBridgePos = Vector3.zero;
        ChangeState(new IdleState());
    }
}
