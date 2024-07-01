using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step : MonoBehaviour
{
    private Color color;
    private int stepIndex;

    public Color Color => color;
    public int StepIndex => stepIndex;

    [SerializeField] MeshRenderer meshRenderer;

    public void SetStepColor(Color color)
    {
        this.color = color;
        meshRenderer.material = ColorController.Ins.GetMaterialColor(color);
    }

    public void SetStepIndex(int stepIndex)
    {
        this.stepIndex = stepIndex;
    }
}
