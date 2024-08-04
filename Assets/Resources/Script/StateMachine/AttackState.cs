using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackState : IState<Bot>
{
    float timer = 0f;
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnim("attack");
        bot.isAttack = true;
        bot.ChangeIsAttackBot();
    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;
        if (timer > 0.5f)
        {
            bot.characterRange.RemoveNullTarget();
            if(bot.characterRange.botInRange.Count > 0)
            {
                bot.OnAttack();
            }
            bot.ChangeState(new IdleState());
        }
    }

    public void OnExit(Bot bot)
    {
        bot.ChangeAnim("idle");
    }
}