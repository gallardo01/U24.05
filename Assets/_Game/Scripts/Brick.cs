using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : GameUnit
{
    private GameColor color;

    public GameColor Color => color;

    [SerializeField] MeshRenderer meshRenderer;
    public Collider brickCollider;

    public void SetBrickColor(GameColor color)
    {
        this.color = color;
        meshRenderer.material = GameController.Ins.GetMaterialColor(color);
    }    
}
