using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Định nghĩa một giao diện cho các trạng thái của đối tượng game
public interface IState<T>
{
    public void OnEnter(T t);
    public void OnExecute(T t);
    public void OnExit(T t);
}

