using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : IState<Bot>
{
    Vector3 target;
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnim("run");
        SeekTarget(bot);
    }

    public void OnExecute(Bot bot)
    {
        if (bot.isDestination)
        {
            // Nhat 5 vien gach di ve dich'
            if (bot.totalBricks >= 3)
            {
                bot.isRotate = true;
                bot.SetDestination(GameController.Ins.finishPoints.position);
            } else
            {
                bot.ChangeState(new IdleState());
            }
        }
    }

    public void OnExit(Bot bot)
    {
        bot.ChangeAnim("idle");
    }

    public void SeekTarget(Bot bot)
    {
        if (bot.stage.GetNearestBricks(bot) != null)
        {
            target = bot.stage.GetNearestBricks(bot).position;
        }
        else
        {
            bot.ChangeState(new IdleState());
        }

        bot.SetDestination(target);
    }
}
