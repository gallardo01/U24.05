using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRange : MonoBehaviour
{
    public List<Character> botInRange = new List<Character>();

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
    // OnTriggerEnter is called when the Collider other enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called with " + other.gameObject.name);

        if (other.gameObject.CompareTag("Bot"))
        {
            Character bot = other.GetComponent<Character>();
            botInRange.Add(other.GetComponent<Character>());
            bot.AttackTarget();
        }
    }

    // OnTriggerExit is called when the Collider other has stopped touching the trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Bot"))
        {
            botInRange.Remove(other.GetComponent<Character>());

        }
    }
    
}
