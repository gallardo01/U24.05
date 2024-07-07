using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int brickColor;
    public int brickPosition;
    public StageControler stage;

    public void SetBrickColor(int color)
    {
        brickColor = color;
        GetComponent<MeshRenderer>().material = ColorController.Instance.GetColor(color); 
    }
    public void SetBrickPosition(int brickPosition)
    {
        this.brickPosition = brickPosition;
    }

    public void SetStage(StageControler stage)
    {
        this.stage = stage;
    }
    public void Removed()
    {
        stage.bricksList.Remove(this);
    }
}
