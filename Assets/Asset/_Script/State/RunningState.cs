using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunningState : IState<Bot>
{
    Vector3 target;
    public void OnEnter(Bot bot)
    {
        int random = bot.PickRandomBrick();
        if (bot.backPack.childCount > random)
        {
            target = bot.target;
            
        }
        else
        {
            target = bot.stage.GetNearestBrick(bot);
            target.y = bot.transform.position.y;
        }

        if (target == null)
        {
            bot.ChangeState(new IdleState());
        }
        
    }
    public void OnExecute(Bot bot)
    {
        bot.RunToNextTarget(target);
        bot.ChangeAnim("run");
        if ((target - bot.transform.position).magnitude <0.1f)
        {
            bot.ChangeState(new IdleState());
        }

        Debug.DrawLine(bot.transform.position, target, Color.red, 1f);
    }
    public void OnExit(Bot bot)
    {

    }

}
