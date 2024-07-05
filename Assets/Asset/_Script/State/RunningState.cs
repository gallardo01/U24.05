using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : IState<Bot>
{
    Vector3 target;
    public void OnEnter(Bot bot)
    {
        target = bot.stage.GetNearestBrick(bot);
        if (target == null)
        {
            bot.ChangeState(new IdleState());
        }
        target.y = bot.transform.position.y;
    }
    public void OnExecute(Bot bot)
    {
        bot.transform.position = Vector3.MoveTowards(bot.transform.position, target, 0.2f);
        if ((target - bot.transform.position).magnitude <0.1f)
        {
            bot.ChangeState(new IdleState());
        }
    }
    public void OnExit(Bot bot)
    {

    }

}
