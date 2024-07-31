using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AttackState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        List<GameObject> listTarget = bot.FindTarget();
        float minHealth = Mathf.Infinity;
        if (listTarget.Count > 0)
        {
            for (int i = 0; i < listTarget.Count; i++)
            {
                if (listTarget[i].GetComponent<Character>().health < minHealth)
                {
                    minHealth = listTarget[i].GetComponent<Character>().health;
                    bot.target = listTarget[i];
                }
            }
        }
        else
        {
            bot.ChangeState(new IdleState());
        }
    }
    public void OnExecute(Bot bot)
    {
        if (bot.target != null)
        {
            bot.ChangeAnim("attack");
            bot.OnAttack();
            bot.time = 0;
            OnEnter(bot);
        }
        else
        {
            OnExit(bot);
        }
    }
    public void OnExit(Bot bot)
    {

    }
}
