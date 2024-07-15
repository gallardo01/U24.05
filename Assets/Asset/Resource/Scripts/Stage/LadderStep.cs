using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderStep : MonoBehaviour
{
    public int stepColor = -1; public int StepColor => stepColor;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetStairColor(int color)
    {
        stepColor = color;
        meshRenderer.material = ColorController.Instance.GetColor(stepColor);

    }
}
