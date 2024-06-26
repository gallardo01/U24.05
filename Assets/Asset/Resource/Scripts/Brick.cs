using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Brick : MonoBehaviour
{
    private int brickColor; public int BrickColor => brickColor;

    public void SetBrickColor(int color)
    {
        brickColor = color;
        GetComponent<MeshRenderer>().material = ColorController.Instance.GetColor(color);
    }

}
