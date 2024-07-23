using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CounterTime
{
    UnityAction playerAction;
    float timer;

    public void OnEnter(UnityAction playerAction, float timer)
    {
        this.playerAction = playerAction;
    }

    public void Execute()
    {
        if(timer < 0)
        {
            timer -= Time.deltaTime;
            if(timer <0) OnExit();
        }
    }

    public void OnExit()
    {
        playerAction?.Invoke();
    }

    public void Cancel()
    {

    }

}
