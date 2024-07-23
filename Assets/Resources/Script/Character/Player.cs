using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public enum State
    {
        Attack,
        Move,
    }

    private State state;
    private float timer = 0f;
    private bool isOnAttack;
    private Vector3 moveDirection;

    public override void OnInit()
    {
        base.OnInit();
        GetComponent<Rigidbody>().isKinematic = false;
        state = State.Move;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (JoystickControl.direct.magnitude > 0)
        {
            CancelInvoke(nameof(OnThrow));
            state = State.Move;
            timer = 0f;
        }

        switch (state)
        {
            case State.Attack:
                OnAttack();
                break;
            case State.Move:
                Move();
                break;
        }
    }

    private void Move()
    {
        moveDirection = JoystickControl.direct;
        if (moveDirection.magnitude > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDirection, Time.deltaTime * moveSpeed);
            transform.forward = moveDirection;
            ChangAnim("run");
        }
        else
        {
            ChangAnim("idle");
        }

        if(timer > 0 && target != null)
        {
            timer = 0f;
            state = State.Attack;
        }
    }

    void OnAttack()
    {
        if(!isOnAttack)
        {
            Attack(target.transform);
            Invoke(nameof(OnThrow), attackDelay);
        }

        isOnAttack = true;

        if(timer > attackDelay)
        {
            StopAttack();
        }
    }

    private void OnThrow()
    {
        Throw(target.transform);
    }
    
    private void StopAttack()
    {
        isOnAttack = false;
        timer = 0;
        state = State.Move;
    }

    public override void OnDeath()
    {
        base.OnDeath();
        GetComponent<Rigidbody>().isKinematic = true;
        PlayersManager.Instance.Reborn();
        this.enabled = false;
    }
}
