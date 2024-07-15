using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Brick : MonoBehaviour
{
    private StageController brickStage; public StageController BrickStage => brickStage;
    private int brickNumber; public int BrickNumber => brickNumber;
    private int brickColor; public int BrickColor => brickColor;

    public void OnInit(StageController stage, int brickNumber, int brickColor)
    {
        GetComponent<BoxCollider>().enabled = true;
        SetStage(stage);
        SetBrickNumber(brickNumber);
        SetBrickColor(brickColor);
    }

    public void RefatoringBrick()
    {
        brickStage.brickList.Remove(this);
        brickStage.SpawnNewBrick(brickNumber);
    }

    public void SetStage(StageController stage)
    {
        this.brickStage = stage;
    }
    public void SetBrickNumber(int brickNumber) 
    {
        this.brickNumber = brickNumber; 
    }
    public void SetBrickColor(int brickColor)
    {
        this.brickColor = brickColor;
        GetComponent<MeshRenderer>().material = ColorController.Instance.GetColor(this.brickColor);
    }
}
