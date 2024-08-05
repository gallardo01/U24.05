using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRange : MonoBehaviour
{
    public List<Character> botsInCircle = new List<Character>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RemoveNullTarget()
    {
        for(int i = 0; i <botsInCircle.Count; i++)
        {
            if (botsInCircle[i] == null || !botsInCircle[i].CompareTag("Bot"))
            {
                botsInCircle.Remove(botsInCircle[i]);
            }
        } 
    }

    public Transform GetNearestTarget()
    {
        float distanceMin = float.MaxValue;
        int index = 0;
        for (int i = 0; i < botsInCircle.Count; i++)
        {
            float distance = (transform.position - botsInCircle[i].transform.position).magnitude;
            if (distanceMin > distance)
            {
                distanceMin = distance;
                index = i;
            }
        }
        return botsInCircle[index].transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Bot bot = GetComponent<Bot>();
        if (other.CompareTag("Bot"))
        {
            botsInCircle.Add(other.GetComponent<Character>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Bot bot = GetComponent<Bot>();
        if (other.CompareTag("Bot"))
        {
            botsInCircle.Remove(other.GetComponent<Character>());
        }
    }
}
