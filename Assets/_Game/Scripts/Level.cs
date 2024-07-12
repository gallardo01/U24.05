using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Level : MonoBehaviour
{
    public List<Floor> floors = new List<Floor>();

    [SerializeField] List<Transform> startPos = new List<Transform>();
    public Transform finishPos;

    public void InitLevel()
    {
        GameController.Ins.GenerateColor();

        for (int i = 0; i < floors.Count; i++)
        {
            floors[i].InitFloor();
        }

        for (int i = 0; i < GameController.Ins.colorsUsed.Count; i++)
        {
            floors[0].GenerateBrick(GameController.Ins.colorsUsed[i], Constants.QUANTITY_BRICK_PER_COLOR);
        }

        GameController.Ins.InitCharacter(startPos);

        UIManager.Ins.OpenUI<UIGameplay>();
    }

    public void DestroyLevel()
    {
        GameController.Ins.ClearCharacterBrick();

        for (int i = 0; i < floors.Count; i++)
        {
            floors[i].ClearAllBrick();
        }
    }
}
