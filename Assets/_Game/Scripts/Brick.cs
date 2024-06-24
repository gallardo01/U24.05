using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int brickColor;

    [SerializeField] MeshRenderer meshRenderer;

    public void SetBrickColor(int brickColor)
    {
        this.brickColor = brickColor;
        meshRenderer.material = ColorController.Ins.GetMaterialColor(brickColor);
    }    
}
