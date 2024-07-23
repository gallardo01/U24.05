using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.RuleTile.TilingRuleOutput;


public class MoveState : IState<Bot>
{
    Vector3 newPos;
    public void OnEnter(Bot bot)
    {
        newPos = bot.RandomNavSphere(bot.transform.position, bot.randomRadius, -1);
    }
    public void OnExecute(Bot bot)
    {
        bot.ChangeAnim("run");
        if (bot.isAttack == false)
        {
            if (!bot.IsObstacleDetected())
            {
                bot.agent.SetDestination(newPos);
            }else
            {
                bot.ChangeDirection(bot.transform.position, bot.randomRadius, -1);
            }
            if (!bot.agent.pathPending && bot.agent.remainingDistance < 0.5f)
            {
                bot.ChangeState(new AttackState());
            }
        }
    }
    public void OnExit(Bot bot)
    {

    }
}
