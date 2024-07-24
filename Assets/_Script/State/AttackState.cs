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
                    bot.target = listTarget[i].transform;
                }
            }
        }
        else
        {
            bot.ChangeState(new MoveState());
        }
    }
    public void OnExecute(Bot bot)
    {
        if (bot.target != null)
        {
            bot.ChangeAnim("attack");
            bot.FireWeapon(bot.weaponPrefabs, bot.target.gameObject);
            bot.time = 0;
            OnEnter(bot);
        }
    }
    public void OnExit(Bot bot)
    {

    }
}
