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
            nextPoint = tf.position + moveDirection * Time.deltaTime * moveSpeed;
            
            if (CanMove(nextPoint))
            {
                tf.position = nextPoint;
            }

            tf.forward = moveDirection;
            ChangeAnim("run");
        }
        else
        {
            ChangeAnim("idle");
        }
    }

    public override void OnInit(GameColor color, Transform transform)
    {
        base.OnInit(color, transform);
        this.PostEvent(EventID.OnInitPlayer, tf);
    }
}
