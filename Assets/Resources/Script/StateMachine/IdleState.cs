using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Bot>
{
    private float timer = 0f;
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnim("idle");       
    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;
        if (timer > 2f)
        {
            bot.ChangeState(new RunningState());
        }
        bot.characterRange.RemoveNullTarget();
        if (bot.characterRange.botInRange.Count > 0 && !bot.isAttack)
        {
            bot.ChangeState(new AttackState());
        }
    }

    public void OnExit(Bot bot)
    {
        
    }
}
