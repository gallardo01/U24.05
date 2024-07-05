using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class IdleState : IState<Bot>
{
    float timer = 0f;
    // Start is called before the first frame update
    public void OnEnter(Bot bot)
    {
        timer = 0f;
    }
    public void OnExecute(Bot bot)
    {
        timer +=Time.deltaTime;
        if (timer > 1f)
        {
            bot.ChangeState(new RunningState());
        }
    }
    public void OnExit(Bot bot)
    {

    }
}
