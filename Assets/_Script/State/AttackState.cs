using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AttackState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        
        Collider[] hitColliders = Physics.OverlapSphere(bot.transform.position, bot.detectionRadius);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("player") && bot.time > bot.cooldownTimeAttack)
            {
                bot.isAttack = true;
                bot.ChangeAnim("attack");
                OnExecute(bot);
                bot.time = 0;
                break;
            }
            else
            {
                bot.ChangeState(new MoveState());
            }
        }
    }
    public void OnExecute(Bot bot)
    {
        bot.FireWeapon(bot.weaponPrefabs,bot.target.gameObject);

    }
    public void OnExit(Bot bot)
    {

    }
}
