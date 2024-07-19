using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PatronState : IState
{
    private float timer = 0f;

    public void OnEnter(Bot bot)
    {
        bot.Move();
    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;

        if (Vector3.Distance(bot.transform.position, bot.movePos) < 1f)
        {
            bot.movePos = bot.GetRandomPoint(bot.transform.position, bot.detectRadius * 2);
            bot.Move();
        }

        if (bot.target != null && timer > bot.detectDelay)
        {
            bot.ChangeState(new AttackState());
        }
    }

    public void OnExit(Bot bot)
    {
        bot.Stop();
    }
}
