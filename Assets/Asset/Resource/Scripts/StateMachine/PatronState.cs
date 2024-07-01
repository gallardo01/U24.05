using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronState : IState
{
    public void OnEnter(Bot bot)
    {
        bot.FindCurrentBricks();
    }

    public void OnExecute(Bot bot)
    {
        bot.BotMovement();
    }

    public void OnExit(Bot bot)
    {

    }
}
