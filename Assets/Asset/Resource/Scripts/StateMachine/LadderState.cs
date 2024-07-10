using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderState : IState
{
    Vector3 target;
    public void OnEnter(Bot bot)
    {
        bot.navMeshAgent.enabled = true;
        target = bot.currentStage.GetLadderPoint(bot.ColorIndex);
    }

    public void OnExecute(Bot bot)
    {
        bot.BotMovement(target);
        bot.RayCheckBrige();
        if(bot.GetBotBrick() < 1 || Vector3.Distance(bot.transform.position, target) < 1f)
        {
            bot.ChangeState(new IdleState());
        }
    }

    public void OnExit(Bot bot)
    {

    }
}
