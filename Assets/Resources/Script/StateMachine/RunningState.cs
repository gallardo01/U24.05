using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunningState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        SetNewRandomDestination(bot);
    }

    // public void OnExecute(Bot bot)
    // {
    //     if (!bot.agent.pathPending &&
    //         (bot.agent.remainingDistance <= bot.agent.stoppingDistance || bot.agent.velocity.magnitude < 0.1f))
    //     {
    //         SetNewRandomDestination(bot);
    //     }
    //     else if (IsPathBlocked(bot))
    //     {
    //         SetNewRandomDestination(bot);
    //     }
    // }
    
    public void OnExecute(Bot bot)
    {
        if (!bot.agent.pathPending &&
            (bot.agent.remainingDistance <= bot.agent.stoppingDistance || bot.agent.velocity.magnitude < 0.1f))
        {
            SetNewRandomDestination(bot);
        }
        else if (IsPathBlocked(bot))
        {
            Vector3 newDirection;
            if (TryFindNewDirection(bot, out newDirection))
            {
                Vector3 newDestination = bot.transform.position + newDirection * 10; // Move 10 units in the new direction
                bot.agent.SetDestination(newDestination);
            }
            else
            {
                SetNewRandomDestination(bot); // Fallback to random destination if no clear direction is found
            }
        }
    }

    public void OnExit(Bot bot)
    {
        bot.ChangeAnim("idle");
    }

    private void SetNewRandomDestination(Bot bot)
    {       
        Vector3 randomPoint = GetRandomPoint(bot.transform.position, 10, bot.agent.areaMask);
        bot.agent.SetDestination(randomPoint);
        bot.ChangeAnim("run");
    }

    private Vector3 GetRandomPoint(Vector3 center, float range, int areaMask)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, range, areaMask))
        {
            return hit.position;
        }
        return center; // Return the center if no valid point is found
    }
    
    
    private bool IsPathBlocked(Bot bot)
    {
        // Implement logic to check if the path is blocked
        // This could involve raycasting or checking the NavMesh path status
        NavMeshPath path = new NavMeshPath();
        bot.agent.CalculatePath(bot.agent.destination, path);
        if (path.status != NavMeshPathStatus.PathComplete)
        {
            return true; // Path is blocked
        }
        return false; // Path is clear
    }
    
    private bool TryFindNewDirection(Bot bot, out Vector3 newDirection)
    {
        Vector3 forward = bot.transform.forward;
        RaycastHit hit;
        if (Physics.Raycast(bot.transform.position, forward, out hit, 5f)) // Check for obstacle within 5 units in front
        {
            // Attempt to find a new direction by rotating the forward vector
            for (int angle = 45; angle <= 180; angle += 45)
            {
                // Check both left and right directions
                Vector3 left = Quaternion.Euler(0, -angle, 0) * forward;
                if (!Physics.Raycast(bot.transform.position, left, 5f))
                {
                    newDirection = left;
                    return true;
                }

                Vector3 right = Quaternion.Euler(0, angle, 0) * forward;
                if (!Physics.Raycast(bot.transform.position, right, 5f))
                {
                    newDirection = right;
                    return true;
                }
            }
        }
        newDirection = Vector3.zero;
        return false;
    }
}
