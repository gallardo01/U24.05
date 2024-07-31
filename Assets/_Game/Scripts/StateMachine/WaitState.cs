using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnim(Constants.ANIM_IDLE);
    }

    public void OnExecute(Bot bot)
    {
        if (!GameManager.Ins.IsState(GameState.Mainmenu))
        {
            bot.ChangeState(new MoveState());
        }
    }

    public void OnExit(Bot bot)
    {

    }
}
