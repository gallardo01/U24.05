using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


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
        bot.agent.SetDestination(newPos);
        if (!bot.agent.pathPending && bot.agent.remainingDistance < 0.1f)
        {
            bot.ChangeAnim("idle");
            bot.ChangeState(new AttackState());
        }
    }
    public void OnExit(Bot bot)
    {

    }
}
