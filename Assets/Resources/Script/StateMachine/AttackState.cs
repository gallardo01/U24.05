using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private float timer = 0f;

    public void OnEnter(Bot bot)
    {
        bot.Attack(bot.target);
        bot.Throw(bot.target);
    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;
        if(timer > bot.attackDelay)
        {
            bot.ChangeState(new IdleState());
        }
    }

    public void OnExit(Bot bot)
    {

    }
}
