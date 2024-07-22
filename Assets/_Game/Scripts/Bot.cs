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

    protected override void Start()
    {
        base.Start();

        weaponType = WeaponType.Axe;
        Instantiate(WeaponManager.Ins.WeaponDataMap[weaponType].weaponHoldPrefab, weaponHoldParent);
    }

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

    protected override void OnDead()
    {
        base.OnDead();
        SimplePool.Despawn(this);       
    }
}
