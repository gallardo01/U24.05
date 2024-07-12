using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] Transform behindPos;
    private GameColor color;
    private int floor;

    public Transform BehindPos => behindPos;
    public GameColor Color => color;
    public int Floor => floor;

    [SerializeField] List<MeshRenderer> gateParts = new List<MeshRenderer>();

    public void SetGateColor(GameColor color)
    {
        this.color = color;
        for (int i = 0; i < gateParts.Count; i++)
        {
            gateParts[i].material = GameController.Ins.GetMaterialColor(color);
        }
    }

    public void SetGateFloor(int floor)
    {
        this.floor = floor;
    }
}
