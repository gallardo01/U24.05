using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int brickColor;
    public int brickPosition;
    public List<Transform> brickStage = new List<Transform>();

    public void SetBrickColor(int color)
    {
        brickColor = color;
        GetComponent<MeshRenderer>().material = ColorController.Instance.GetColor(color); 
    }
    public void SetBrickPosition(int brickPosition, List<Transform> brickStage)
    {
        this.brickPosition = brickPosition;
        this.brickStage = brickStage;
    }
}
