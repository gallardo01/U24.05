using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public override void Move()
    {
        moveDirection = JoystickControl.direct;
        if(moveDirection.magnitude > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDirection, Time.deltaTime * moveSpeed);
            transform.forward = moveDirection;
            ChangAnim("run");
        }
        else
        {
            ChangAnim("idle");
        }
    }
}
