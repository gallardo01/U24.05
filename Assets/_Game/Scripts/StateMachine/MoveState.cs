using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        bot.isMoving = true;
        bot.ChangeAnim(Constants.ANIM_RUN);
    }

    public void OnExecute(Bot bot)
    {

    }

    public void OnExit(Bot bot)
    {

    }
}
