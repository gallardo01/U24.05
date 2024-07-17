using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Character>
{
    float timer = 0;

    public void OnEnter(Character character)
    {
        character.ChangAnim("idle");
        character.InvokeRepeating(nameof(character.DetectTarget), 0f, character.detectDelay);
    }
    public void OnExecute(Character character)
    {
        if (character.target != null)
        {
            character.ChangeState(new AttackState());
        }
        else character.ChangeState(new PatronState());
    }
    public void OnExit(Character character)
    {
        
    }
}
