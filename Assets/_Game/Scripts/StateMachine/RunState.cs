using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IState<Enemy>
{
    int targetPosIndex;
    public void OnEnter(Enemy enemy)
    {
        enemy.ChangeAnim("run");
        targetPosIndex = 0;
        enemy.SetDestination(enemy.targetPos[targetPosIndex]);
    }

    public void OnExecute(Enemy enemy)
    {
        if (enemy.targetPos.Count <= 0)
        {                      
            enemy.ChangeState(new IdleState());
        }

        if (!enemy.CanMove(enemy.NextPoint))
        {
            enemy.transform.forward = -enemy.transform.forward;
            enemy.ChangeState(new IdleState());
        }

        if (enemy.IsDestionation)
        {
            if (targetPosIndex < enemy.targetPos.Count - 1)
            {
                targetPosIndex++;
                enemy.SetDestination(enemy.targetPos[targetPosIndex]);
            }
            else
            {
                enemy.targetPos.Clear();
            }
        }
    }

    public void OnExit(Enemy enemy)
    {

    }
}
