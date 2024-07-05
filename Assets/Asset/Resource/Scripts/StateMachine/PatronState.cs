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
            target.y = bot.transform.position.y;
        }
        else
        {
            bot.ChangeState(new IdleState());
        }
    }

    public void OnExecute(Bot bot)
    {
        bot.BotMovement(target);
        if((bot.transform.position - target).magnitude < 0.1f)
        {
            bot.ChangeState(new IdleState());
        }
    }

    public void OnExit(Bot bot)
    {

    }

    //public void FindNewTarget()
    //{
    //    target = new Vector3(Random.Range(-9, 9), 0.5f, Random.Range(-9, 9));
    //}
}
