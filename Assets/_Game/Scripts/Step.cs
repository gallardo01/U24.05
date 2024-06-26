using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step : MonoBehaviour
{
    public Color color;

    [SerializeField] MeshRenderer meshRenderer;

    public void SetStepColor(Color color)
    {
        this.color = color;
        meshRenderer.material = ColorController.Ins.GetMaterialColor(color);
    }
}
