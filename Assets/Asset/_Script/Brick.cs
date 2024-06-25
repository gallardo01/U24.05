using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int brickColor;

    public void SetBrickColor(int color)
    {
        brickColor = color;
        GetComponent<MeshRenderer>().material = ColorController.Instance.GetColor(color); 
    }
}
