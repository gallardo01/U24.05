using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private Vector3 moveDirection;

    [Header("Setting")]
    [SerializeField] float moveSpeed;

    void Update()
    {
        Move();
    }

    private void Move()
    {
        moveDirection = JoystickControl.direct.normalized;
        if(moveDirection.magnitude > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDirection, Time.deltaTime * moveSpeed);
            ChangAnim("run");
        }
        else
        {
            ChangAnim("idle");
        }
    }

}
