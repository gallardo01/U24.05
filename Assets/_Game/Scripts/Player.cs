using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{   
    void Update()
    {
        moveDirection = JoystickControl.direct;

        if (moveDirection.magnitude > 0)
        {
            nextPoint = transform.position + moveDirection * Time.deltaTime * moveSpeed;
            
            if (CanMove(nextPoint))
            {
                transform.position = nextPoint;
            }

            transform.forward = moveDirection;
            ChangeAnim("run");
        }
        else
        {
            ChangeAnim("idle");
        }
    }   
}
