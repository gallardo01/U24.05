using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IdleState : IState<Bot>
{
    float timer;
    float randomTime;

    public void OnEnter(Bot bot)
    {
        bot.isMoving = false;
        bot.ChangeAnim(Constants.ANIM_IDLE);
        timer = 0;
        randomTime = Random.Range(0.5f, 2f);
    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;

        if (timer > randomTime)
        {
            if (bot.targetInRange.Count == 0)
            {
                bot.ChangeState(new MoveState());
            }
            else
            {
                timer = 0;
            }    
        }
    }

    public void OnExit(Bot bot)
    {
        
    }
}
