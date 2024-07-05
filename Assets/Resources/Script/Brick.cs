using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int brickColor;
    public int brickPosition;
    public StageController stage;

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
    public void SetStage(StageController stage)
    {
        this.stage = stage;
    }

    public void RemoveBrick()
    {
        stage.listBricks.Remove(this);
    }
}
