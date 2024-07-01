using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public List<Step> steps = new List<Step>();

    public void InitBridge()
    {
        for (int i = 0; i < steps.Count; i++)
        {
            steps[i].SetStepIndex(i);
        }
    }
}
