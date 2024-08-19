using DG.Tweening;
using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public enum State
    {
        Idle,
        Move,
        Attack,
    }

    public State state {  get; private set; }
    private float timer = 0f;
    private bool isOnAttack;
    private Vector3 moveDirection;
    private Vector3 targetPos;

    public override void OnInit()
    {
        base.OnInit();
        state = State.Move;
        GetComponent<Rigidbody>().isKinematic = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (MobileJoystick.Instance.GetMoveVector().magnitude > 0)
        {
            CancelInvoke(nameof(OnThrow));
            ChangeToMove();
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
        moveDirection = new Vector3(MobileJoystick.Instance.GetMoveVector().x, 0, MobileJoystick.Instance.GetMoveVector().y);
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

        if(timer > 0.25f && target != null)
        {
            state = State.Attack;
            timer = 0f;
        }
    }

    void OnAttack()
    {
        if(!isOnAttack)
        {
            Attack(target.transform);
            targetPos = target.transform.position;
            Invoke(nameof(OnThrow), 0.25f);
            isOnAttack = true;
            SoundManager.PLayerTalk();
        }

        if (timer > attackDelay)
        {
            ChangeToMove();
        }        
    }

    private void OnThrow()
    {
        Throw(targetPos);
        SoundManager.Throw();
    }

    private void ChangeToMove()
    {
        isOnAttack = false;
        timer = 0;
        state = State.Move;
    }

    public override void OnDeath(Character killerCharacter)
    {
        base.OnDeath(killerCharacter);
        GetComponent<Rigidbody>().isKinematic = true;
    }
}
