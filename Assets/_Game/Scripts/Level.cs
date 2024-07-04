using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Level : MonoBehaviour
{
    public List<Floor> floors = new List<Floor>();
    public List<GameColor> colors = new List<GameColor>();

    [SerializeField] List<Transform> startPos = new List<Transform>();

    public void InitLevel()
    {
        colors = ColorController.Ins.GenerateColor();

        for (int i = 0; i < floors.Count; i++)
        {
            floors[i].InitFloor();
        }

        for (int i = 0; i < Constants.QUANTITY_CHARACTER; i++)
        {
            LevelManager.Ins.characters[i].OnInit(colors[i], startPos[i]);
            floors[0].GenerateBrick(colors[i], Constants.QUANTITY_BRICK_PER_COLOR);
        }
    }
}
