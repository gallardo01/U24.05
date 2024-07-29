using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRange : MonoBehaviour
{
    public List<Character> botInRange = new List<Character>();
    public Bot currentTarget = null;

    public void RemoveNullTarget()
    {
        for (int i = botInRange.Count - 1; i >= 0; i--)
        {
            if (botInRange[i] == null || !botInRange[i].CompareTag("Bot"))
            {
                botInRange.RemoveAt(i);
            }
        }
    }

    public Transform GetNearestTarget()
    {
        if (botInRange.Count == 0)
        {
            return null;
        }
        else
        {
            float distanceMin = float.MaxValue;
            int index = 0;
            for (int i = 0; i < botInRange.Count; i++)
            {
                if (botInRange[i] == null) continue; // Add null check
                float distance = (transform.position - botInRange[i].transform.position).sqrMagnitude;
                if (distance < distanceMin)
                {
                    distanceMin = distance;
                    index = i;
                }
            }
            return botInRange[index]?.transform; // Add null check
        }
    }

    public void SetNearestTarget()
    {
        RemoveNullTarget();
        if (botInRange.Count > 0)
        {
            Transform nearestTargetTransform = GetNearestTarget();
            if (nearestTargetTransform == null) return; // Add null check
            Bot nearestBot = nearestTargetTransform.GetComponent<Bot>();
            if (nearestBot != null)
            {
                if (currentTarget != null && currentTarget != nearestBot)
                {
                    currentTarget.RemoveTarget(); // Remove the target circle from the old currentTarget
                }
                currentTarget = nearestBot;
                currentTarget.SetTarget(); // Show the target circle on the new currentTarget
            }
        }
    }

    // OnTriggerEnter is called when the Collider other enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bot"))
        {
            Bot bot = other.GetComponent<Bot>();
            if (bot != null)
            {
                bot.SetTarget(); // Show the target circle
            }
            botInRange.Add(other.GetComponent<Character>());
            SetNearestTarget();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Bot"))
        {
            Bot bot = other.GetComponent<Bot>();
            if (bot != null)
            {
                if (bot == currentTarget) // If the current target is leaving the range
                {
                    currentTarget = null; // Reset the current target
                    SetNearestTarget(); // Set the nearest target
                    bot.targetCircle.SetActive(false);
                }
            }
            botInRange.Remove(other.GetComponent<Character>());
        }
    }
}