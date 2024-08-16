using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunningState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnim("run");
        bot.SetDestination(SeekTarget());
    }

    public void OnExecute(Bot bot)
    {
        if (bot.isDestination)
        {
            bot.ChangeState(new IdleState());
        }
    }

    public void OnExit(Bot bot)
    {

    }

    private Vector3 SeekTarget()
    {
        for (int i = 0; i < 20; i++)
        {
            Vector3 destination = new Vector3(Random.Range(-5f, 36f), 0f, Random.Range(-26f, 13f));
            NavMeshHit hit;
            if (NavMesh.SamplePosition(destination, out hit, 10f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
        return new Vector3(Random.Range(-5f, 36f), 0f, Random.Range(-26f, 13f));
    }
}
