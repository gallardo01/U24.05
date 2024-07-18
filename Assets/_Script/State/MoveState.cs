using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;


public class MoveState : IState<Bot>
{
    Vector3 target;
    public void OnEnter(Bot bot)
    {
        bot.FindTarget();
        target = bot.target.position;
        target.y = bot.transform.position.y;
    }
    public void OnExecute(Bot bot)
    {
        Collider[] hitColliders = Physics.OverlapSphere(bot.transform.position, bot.detectionRadius);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("player") && bot.time > bot.cooldownTime)
            {
                bot.ChangeState(new AttackState());
                bot.isAttack = true;
                bot.time = 0;
                break;
            }
            else
            {
                Debug.Log("run");
                bot.agent.SetDestination(target);
                bot.ChangeAnim("run");
            }
        }    
        if ((target - bot.transform.position).magnitude < 0.5f)
        {       
            OnEnter(bot);
        }
    }
    public void OnExit(Bot bot)
    {

    }
}
