using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Định nghĩa một giao diện cho các trạng thái của đối tượng game
public interface IState
{
    void Enter();
    void Execute();
    void Exit();
}

// Định nghĩa lớp StateMachine để quản lý trạng thái của đối tượng game
public class StateMachine
{
    private IState currentState;

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.Enter();
        }
    }

    public void Execute()
    {
        if (currentState != null)
        {
            currentState.Execute();
        }
    }
}
