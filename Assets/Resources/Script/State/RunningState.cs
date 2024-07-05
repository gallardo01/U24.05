using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : IState<Bot>
{
    Vector3 target; 
    public void OnEnter(Bot bot)
    {
        bot.target = new Vector3(Random.Range(-9f, 9f), 0.35f, Random.Range(-9f, 9f));
        target = bot.target;
    }

    public void OnExecute(Bot bot)
    {
        bot.transform.position = Vector3.MoveTowards(bot.transform.position, target, 0.05f);
        if((target - bot.transform.position).magnitude < 0.1f)
        {
            bot.ChangeState(new IdleState());
        }
    }

    public void OnExit(Bot bot)
    {

    }
}
