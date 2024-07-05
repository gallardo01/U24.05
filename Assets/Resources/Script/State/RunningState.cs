using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : IState<Bot>
{
    Vector3 target;
    public void OnEnter(Bot bot)
    {
        if (bot.stage.GetNearestBricks(bot) != null)
        {
            target = bot.stage.GetNearestBricks(bot).position;
            target.y = bot.transform.position.y;
        } else
        {
            bot.ChangeState(new IdleState());
        }
    }

    public void OnExecute(Bot bot)
    {
        bot.transform.position = Vector3.MoveTowards(bot.transform.position, target, 0.02f);
        if((target-bot.transform.position).magnitude < 0.1f)
        {
            bot.ChangeState(new IdleState());
        }
    }

    public void OnExit(Bot bot)
    {

    }
}
