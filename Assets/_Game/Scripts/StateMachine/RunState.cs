using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IState
{
    public void OnEnter(Enemy enemy)
    {
        throw new System.NotImplementedException();
    }

    public void OnExecute(Enemy enemy)
    {
        for (int i = 0; i < enemy.targetPos.Count; i++)
        {
            // do move (targetPos[i] -> on complete -> do move ( [i+1])
        }

        if (enemy.targetPos.Count <= 0)
        {
            enemy.ChangeState(new IdleState());
        }
    }

    public void OnExit(Enemy enemy)
    {

    }
}
