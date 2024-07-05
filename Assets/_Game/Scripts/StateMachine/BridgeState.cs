using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeState : IState<Enemy>
{
    public void OnEnter(Enemy enemy)
    {
        if (enemy.targetBridgePos == Vector3.zero)
        {
            enemy.targetBridgePos = LevelManager.Ins.currentLevel.floors[enemy.CurrentFloor].GetBridgePos();
        }
    }

    public void OnExecute(Enemy enemy)
    {
        enemy.targetPos.Add(enemy.targetBridgePos);
        enemy.ChangeState(new RunState());
    }

    public void OnExit(Enemy enemy)
    {
        
    }
}
