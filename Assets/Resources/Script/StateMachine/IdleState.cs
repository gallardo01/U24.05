using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        bot.ChangAnim("idle");
        bot.DetectTarget();
    }
    public void OnExecute(Bot bot)
    {
        if (bot.target != null)
        {
            bot.ChangeState(new AttackState());
        }
        else bot.ChangeState(new PatronState());
    }
    public void OnExit(Bot bot)
    {
        
    }
}
