using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void OnEnter(Character character);
    void OnExecute(Character character);
    void OnExit(Character character);
}
