using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
       
    }

    public void OnExecute(Bot bot)
    {
        bot.ChangeState(new RunningState());
    }

    public void OnExit(Bot bot)
    {
        
    }
}
