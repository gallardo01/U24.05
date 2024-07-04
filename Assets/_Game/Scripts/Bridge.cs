using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public List<Step> steps = new List<Step>();
    public Vector3 bridgeEndPoint;
    public void InitBridge()
    {
        for (int i = 0; i < steps.Count; i++)
        {
            steps[i].SetStepIndex(i);
        }

        bridgeEndPoint = steps[steps.Count - 1].transform.position;
    }
}
