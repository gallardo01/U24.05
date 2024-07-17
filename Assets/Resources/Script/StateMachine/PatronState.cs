using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronState : IState<Character> 
{
    public void OnEnter(Character character)
    {
        character.InvokeRepeating(nameof(character.DetectTarget), 0f, character.detectDelay);
    }

    public void OnExecute(Character character)
    {
        if(character.target == null)
        {
            character.Move();
        }
        else
        {
            character.ChangeState(new AttackState());
        }
    }

    public void OnExit(Character character)
    {

    }
}
