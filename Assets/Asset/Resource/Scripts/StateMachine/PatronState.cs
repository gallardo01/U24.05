using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PatronState : IState
{
    Vector3 target;

    public void OnEnter(Bot bot)
    {
        if(bot.currentStage.FindNearBrick(bot) != null)
        {
            target = bot.currentStage.FindNearBrick(bot).transform.position;
        }
        else
        {
            bot.ChangeState(new IdleState());
        }
    }

    public void OnExecute(Bot bot)
    {
        bot.BotMovement(target);
        if((Vector3.Distance(bot.transform.position, target) < 1f))
        {
            bot.ChangeState(new IdleState());
        }
    }

    public void OnExit(Bot bot)
    {

    }
}
