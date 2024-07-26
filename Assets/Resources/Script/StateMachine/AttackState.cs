using UnityEngine;

public class AttackState : IState<Bot>
{
    float timer = 0f;
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnim("attack");
        bot.isAttack = true;
        bot.ChangeIsAttackBot();
    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;
        if(timer > 0.5f)
        {
            bot.ChangeIsAttackBot();
            if(bot.range.botInRange.Count > 0)
            {
                bot.Throw();
            }
            bot.ChangeState(new IdleState());
        }    
    }

    public void OnExit(Bot bot)
    {

    }
}