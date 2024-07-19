using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    //private float timer = 0f;

    public void OnEnter(Bot bot)
    {
        bot.ChangAnim("idle");
    }

    public void OnExecute(Bot bot)
    {
        //timer += Time.deltaTime;
        //if (timer < 0.2f) return;

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
