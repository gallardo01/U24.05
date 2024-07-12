using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderState : IState
{
    Vector3 target;
    Ladder targetLadder;
    public void OnEnter(Bot bot)
    {
        bot.navMeshAgent.enabled = true;
        targetLadder = bot.currentStage.GetTargetLadder(bot.ColorIndex);
        target = targetLadder.ladderStartPoint.position;
    }

    public void OnExecute(Bot bot)
    {
        bot.BotMovement(target);
        bot.RayCheckBrige();

        if(bot.GetBotBrick() < 1 || Vector3.Distance(bot.transform.position, target) < 1f)
        {
            if (target == targetLadder.ladderStartPoint.position)
            {
                target = targetLadder.ladderEndPoint.position;
            }
            else
            {
                bot.ChangeState(new IdleState());
            }
        }
    }

    public void OnExit(Bot bot)
    {

    }
}
