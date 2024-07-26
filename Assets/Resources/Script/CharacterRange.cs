using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRange : MonoBehaviour
{
    public List<Character> botInRange = new List<Character>();
    public Bot currentTarget = null;

    public void RemoveNullTarget()
    {
        for (int i = 0; i < botInRange.Count; i++)
        {
            if (botInRange[i] == null)
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
                float distance = (transform.position - botInRange[i].transform.position).sqrMagnitude;
                if (distance < distanceMin)
                {
                    distanceMin = distance;
                    index = i;
                }
            }
            return botInRange[index].transform;
        }
    }

    public void SetNearestTarget()
    {
        RemoveNullTarget();
        if (botInRange.Count > 0 && currentTarget == null)
        {
            currentTarget = botInRange[0].GetComponent<Bot>(); // The nearest bot is at the start of the list
            if (currentTarget != null)
            {
                currentTarget.SetTarget();
            }
        }
    }
    
    // OnTriggerEnter is called when the Collider other enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bot"))
        {
            Character bot = other.GetComponent<Character>();
            botInRange.Add(bot);
            botInRange.Sort((bot1, bot2) => Vector3.Distance(transform.position, bot1.transform.position)
                .CompareTo(Vector3.Distance(transform.position, bot2.transform.position)));
            if (currentTarget == null)
            {
                SetNearestTarget();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Bot"))
        {
            Bot bot = other.GetComponent<Bot>();
            if (bot != null)
            {
                bot.targetCircle.SetActive(false);
                if (bot == currentTarget) // If the current target is leaving the range
                {
                    currentTarget = null; // Reset the current target
                }
            }
            botInRange.Remove(other.GetComponent<Character>());
            botInRange.Sort((bot1, bot2) => Vector3.Distance(transform.position, bot1.transform.position)
                .CompareTo(Vector3.Distance(transform.position, bot2.transform.position)));
            if (currentTarget == null)
            {
                SetNearestTarget();
            }
        }
    }
    
}
