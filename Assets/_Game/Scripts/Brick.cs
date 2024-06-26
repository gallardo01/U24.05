using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : GameUnit
{
    public Color color;

    [SerializeField] MeshRenderer meshRenderer;
    public Collider brickCollider;

    public void SetBrickColor(Color color)
    {
        this.color = color;
        meshRenderer.material = ColorController.Ins.GetMaterialColor(color);
    }    
}
