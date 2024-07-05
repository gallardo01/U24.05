using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Enemy>
{
    float timer;
    float randomTime;

    public void OnEnter(Enemy enemy)
    {
        enemy.ChangeAnim("idle");
        enemy.ResetPath();
        enemy.targetPos.Clear();
        timer = 0;
        randomTime = Random.Range(1f, 3f);
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;

        if (timer > randomTime)
        {
            if (enemy.Bricks.Count > 0)
            {
                enemy.ChangeState(new BridgeState());
            }
            else
            {
                enemy.ChangeState(new CollectState());
            }
        }      
    }

    public void OnExit(Enemy enemy)
    {
        
    }
}
