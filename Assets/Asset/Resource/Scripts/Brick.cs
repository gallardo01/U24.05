using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Brick : MonoBehaviour
{
    private StageController brickStage; public StageController BrickStage => brickStage;
    private int brickNumber; public int BrickNumber => brickNumber;
    private int brickColor; public int BrickColor => brickColor;

    public void RefatoringBrick()
    {
        brickStage.SpawnNewBrick(brickNumber);
    }

    public void SetStage(StageController stageController)
    {
        this.brickStage = stageController;
    }
    public void SetBrickNumber(int number) 
    { 
        brickNumber = number; 
    }
    public void SetBrickColor(int color)
    {
        brickColor = color;
        GetComponent<MeshRenderer>().material = ColorController.Instance.GetColor(color);
    }



}
