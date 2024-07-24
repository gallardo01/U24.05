using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        Debug.Log("Enter");
        bot.ChangeAnim("idle");
    }
    public void OnExecute(Bot bot)
    {
        
        if (bot.time >1f)
        {
            bot.ChangeState(new MoveState());
        }
    }
    public void OnExit(Bot bot)
    {

    }
}
