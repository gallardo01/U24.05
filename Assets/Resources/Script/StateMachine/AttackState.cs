using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Character>
{
    private float timer;
    public void OnEnter(Character character)
    {
        character.Attack(character.target);
    }

    public void OnExecute(Character character)
    {
        timer += Time.deltaTime;
        if(timer > character.attackDelay)
        {
            character.ChangeState(new IdleState());
        }
    }

    public void OnExit(Character character)
    {

    }
}
