using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Bot>
{
    float timer = 0f;

    public void OnEnter(Bot bot)
    {
        bot.GetComponent<Rigidbody>().velocity = Vector3.zero;
        bot.agent.enabled = false;
        timer = 0f;
    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;

        if(timer > 0.05f)
        {
            bot.ChangeState(new RunningState());
        }
    }

    public void OnExit(Bot bot)
    {

    }
}
