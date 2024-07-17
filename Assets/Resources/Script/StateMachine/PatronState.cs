using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronState : IState<Bot> 
{
    public void OnEnter(Bot bot)
    {
        bot.InvokeRepeating(nameof(bot.DetectTarget), 1f, bot.detectDelay);
    }

    public void OnExecute(Bot bot)
    {
        if(bot.target == null)
        {
            bot.Move();
        }
        else
        {
            bot.ChangeState(new AttackState());
        }
    }

    public void OnExit(Bot bot)
    {

    }
}
