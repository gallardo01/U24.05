using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectState : IState<Enemy>
{
    List<Vector3> brickPos;

    public void OnEnter(Enemy enemy)
    {
        brickPos = LevelManager.Ins.currentLevel.floors[enemy.CurrentFloor].GetListBrickPos(enemy.Color);        
    }

    public void OnExecute(Enemy enemy)
    {
        if (brickPos == null)
        {
            enemy.ChangeState(new IdleState());
        }
        else
        {
            int quantityBrickToCollect = Random.Range(1, brickPos.Count);

            for (int i = 0; i < quantityBrickToCollect; i++)
            {
                enemy.targetPos.Add(brickPos[i]);
            }

            enemy.ChangeState(new RunState());
        }        
    }

    public void OnExit(Enemy enemy)
    {
        
    }
}
