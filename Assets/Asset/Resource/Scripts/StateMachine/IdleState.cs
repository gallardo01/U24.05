using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    float timer = 0;

    public void OnEnter(Bot bot)
    {

    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;
        if(timer > 0.2f)
        {
            bot.ChangeState(new PatronState());
        }
    }

    public void OnExit(Bot bot)
    {

    }
}
