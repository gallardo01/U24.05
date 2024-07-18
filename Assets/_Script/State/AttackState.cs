using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AttackState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnim("attack");
    }
    public void OnExecute(Bot bot)
    {
        bot.FireWeapon(bot.weaponPrefabs);
        bot.ChangeState(new MoveState());
    }
    public void OnExit(Bot bot)
    {

    }
}
