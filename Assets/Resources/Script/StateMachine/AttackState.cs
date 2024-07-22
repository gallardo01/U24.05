// using System.Collections;
// using System.Collections.Generic;
// using Unity.VisualScripting;
// using UnityEngine;
//
// public class AttackState : IState<Bot>
// {
//     public void OnEnter(Bot bot)
//     {
//         Debug.Log("Entering AttackingState");
//         bot.ChangeAnim("attack");
//     }
//
//     public void OnExecute(Bot bot)
//     {
//         bot.characterRange.RemoveNullTarget();
//         if (bot.characterRange.botInRange.Count > 0)
//         {
//             Character target = bot.characterRange.GetNearestTarget().GetComponent<Character>();
//             if (target != null)
//             {
//                 bot.AttackTarget();
//             }
//         }
//         else
//         {
//             bot.ChangeState(new RunningState());
//         }
//     }
//
//     public void OnExit(Bot bot)
//     {
//         bot.ChangeAnim("idle");
//     }
// }
//
