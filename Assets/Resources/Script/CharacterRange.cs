using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRange : MonoBehaviour
{
    public List<Character> botInRange = new List<Character>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RemoveNullTarget()
    {
        for (int i = 0; i < botInRange.Count; i++)
        {
            if (botInRange[i] == null || !botInRange[i].CompareTag("Bot"))
            {
                botInRange.Remove(botInRange[i]);
            }
        }
    }

    public Transform GetNearestTarget()
    {
        float distanceMin = float.MaxValue;
        int index = 0;
        for (int i = 0; i < botInRange.Count; i++)
        {
            float distance = (transform.position - botInRange[i].transform.position).magnitude;
            if (distanceMin > distance)
            {
                distanceMin = distance;
                index = i;
            }
        }
        return botInRange[index].transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bot")) {
            botInRange.Add(other.GetComponent<Character>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Bot"))
        {
            botInRange.Remove(other.GetComponent<Character>());
        }
    }
}
