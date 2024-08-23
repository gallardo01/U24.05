using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AttackState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        List<GameObject> listTarget = bot.FindTarget();
        bot.ChooseTargetPriority(listTarget);
    }
    public void OnExecute(Bot bot)
    {
        if (bot.target != null && bot.isHiding == false)
        {
            bot.anim.SetTrigger("attack");
            bot.OnAttack();
            bot.time = 0;
            if (bot.health < 50)
            {
                bot.isHiding = true;
                bot.ChangeState(new MoveState());
            } else
            {
                OnEnter(bot);
            }
        }
        else
        {
            bot.ChangeState(new MoveState());
        }
    }
    public void OnExit(Bot bot)
    {
    }
}
