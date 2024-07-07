using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Debug.Log("toa do ban dau" + target);
            target.y = bot.transform.position.y;
            Debug.Log("toa do sau" + target);
        }

        if (target == null)
        {
            bot.ChangeState(new IdleState());
        }
        
    }
    public void OnExecute(Bot bot)
    {
        Vector3 direction = (target - bot.transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(direction);
            bot.transform.rotation = newRotation;
        }   
        bot.transform.position = Vector3.MoveTowards(bot.transform.position, target, 0.03f);
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
