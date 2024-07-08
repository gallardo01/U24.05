using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrigeState : IState
{
    Vector3 target;
    public void OnEnter(Bot bot)
    {
        target = bot.currentStage.BotBrickPoint.position;
    }

    public void OnExecute(Bot bot)
    {
        bot.BotMovement(target);
        if(Vector3.Distance(bot.transform.position, target) < 2f)
        {
            bot.ChangeState(new IdleState());
        }
    }

    public void OnExit(Bot bot)
    {

    }
}
