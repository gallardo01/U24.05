using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : IState<Bot>
{
    // Tim` cau` ngan' nhat'
    // Nhat gach ngau nhien 3-5 vien ti le len cau` (chua co gach)
    // Nhat gach du? de? len cau` duong` ngan nhat
    List<Transform> target = new List<Transform>();
    int index = 0;
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnim("run");
        if (bot.stage.GetDestinationOfBot(bot))
        {
            target = bot.stage.GetPathDestination(bot);
            bot.SetDestination(target[0].position);
            index = 0;
            //Debug.Log("destination: " + target[0].position);
        } else
        {
            SeekTarget(bot);
        }
    }

    public void OnExecute(Bot bot)
    {
        if (bot.isDestination && index == target.Count-1)
        {
            bot.isRotate = true;
            bot.ChangeState(new IdleState());
        } else if(bot.isDestination && index < target.Count - 1)
        {
            bot.isRotate = true;
            index++;
            bot.SetDestination(target[index].position);
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
            index = 0;
            target.Add(bot.stage.GetNearestBricks(bot));
            bot.SetDestination(target[0].position);
        }
        else
        {
            bot.ChangeState(new IdleState());
        }

    }
}
