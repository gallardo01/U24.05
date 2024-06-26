using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Level : MonoBehaviour
{
    public List<Floor> floors = new List<Floor>();

    public void InitLevel()
    {
        ColorController.Ins.GenerateColor();

        for (int i = 0; i < floors.Count; i++)
        {
            floors[i].InitFloor();
        }

        for (int i = 0; i < Constants.QUANTITY_COLOR_GENERATE; i++)
        {
            floors[0].GenerateBrick((Color)ColorController.Ins.colorsIndexUsed[i], Constants.QUANTITY_BRICK_PER_COLOR);
        }
        //this.PostEvent(EventID.OnInitLevel, startPos.position);
    }
}
