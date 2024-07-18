using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnim("idle");
    }
    public void OnExecute(Bot bot)
    {
        bot.ChangeState(new MoveState());
    }
    public void OnExit(Bot bot)
    {

    }
}
