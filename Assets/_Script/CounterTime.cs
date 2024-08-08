using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CounterTime : MonoBehaviour
{
    UnityAction playerAction;
    private float time;
    public void OnStart(UnityAction playerAction, float time)
    {
        this.playerAction = playerAction;
        this.time = time;
    }

    public void Excecute()
    {
        if (time>0)
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                Exit();
            }
        }
    }
    public void Exit()
    {
        playerAction?.Invoke();
    }
    public void Cancel()
    {
        playerAction = null;
    }
}
