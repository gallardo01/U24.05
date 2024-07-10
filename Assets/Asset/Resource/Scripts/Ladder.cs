using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] List<LadderStep> ladderSteps;
    public Transform ladderEndPoint;
    public Transform ladderStartPoint;

    public int GetLadderStepColors(int color)
    {
        int numberOfStep = 0;
        for(int i = 0; i < ladderSteps.Count; i++)
        {
            if (ladderSteps[i].StepColor == color)
            {
                numberOfStep++;
            }
        }
        return numberOfStep;
    }
}
