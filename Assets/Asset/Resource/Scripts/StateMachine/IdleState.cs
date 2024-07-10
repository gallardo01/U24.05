using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    float timer = 0;

    public void OnEnter(Bot bot)
    {
        bot.navMeshAgent.enabled = false;
        bot.ChangeAnim("idle");
    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;
        if(timer > 0.2f)
        {
            if (bot.GetBotBrick() <= 5)
            {
                bot.ChangeState(new PatronState());
            }
            else
            {
                bot.ChangeState(new LadderState());
            }
        }
    }

    public void OnExit(Bot bot)
    {

    }

}
