using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    public int stairColor; public int StairColor => stairColor;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetStairColor(int color)
    {
        stairColor = color;
        meshRenderer.material = ColorController.Instance.GetColor(stairColor);

    }
}
