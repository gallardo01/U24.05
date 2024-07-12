using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step : MonoBehaviour
{
    private GameColor color;
    private int stepIndex;

    public GameColor Color => color;
    public int StepIndex => stepIndex;

    [SerializeField] MeshRenderer meshRenderer;

    public void SetStepColor(GameColor color)
    {
        this.color = color;
        meshRenderer.material = GameController.Ins.GetMaterialColor(color);
    }

    public void SetStepIndex(int stepIndex)
    {
        this.stepIndex = stepIndex;
    }
}
