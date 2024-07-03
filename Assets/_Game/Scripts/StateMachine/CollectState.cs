using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectState : IState
{
    List<Vector3> brickPos = new List<Vector3>();

    public void OnEnter(Enemy enemy)
    {
        brickPos = LevelManager.Ins.currentLevel.floors[enemy.CurrentFloor].GetListBrickPos(enemy.Color);
    }

    public void OnExecute(Enemy enemy)
    {
        int quantityBrickToCollect = Random.Range(0, brickPos.Count);
        
        for (int i = 0; i < quantityBrickToCollect; i++)
        {
            enemy.targetPos.Add(brickPos[i]);
        }

        enemy.ChangeState(new RunState());
    }

    public void OnExit(Enemy enemy)
    {
        
    }
}
