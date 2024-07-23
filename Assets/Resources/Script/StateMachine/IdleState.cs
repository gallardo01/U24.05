using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    public void OnEnter(Bot bot)
    {
        bot.ChangAnim("idle");
    }

    public void OnExecute(Bot bot)
    {
        if (bot.target == null)
        {
            bot.ChangeState(new PatronState());
        }
        else bot.ChangeState(new AttackState());
    }

    public void OnExit(Bot bot)
    {
        
    }
}
