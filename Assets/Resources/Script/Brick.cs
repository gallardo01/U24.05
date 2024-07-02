using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int brickColor;
    public int brickPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetBrickPosition(int position)
    {
        brickPosition = position;
    }
    public void SetBrickColor(int color)
    {
        brickColor = color;
        GetComponent<MeshRenderer>().material = ColorController.Ins.GetMaterialColor(color);
    }
}
