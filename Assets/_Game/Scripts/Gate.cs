using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private Color color;
    private int floor;

    public Color Color => color;
    public int Floor => floor;

    [SerializeField] List<MeshRenderer> gateParts = new List<MeshRenderer>();

    public void SetGateColor(Color color)
    {
        this.color = color;
        for (int i = 0; i < gateParts.Count; i++)
        {
            gateParts[i].material = ColorController.Ins.GetMaterialColor(color);
        }
    }

    public void SetGateFloor(int floor)
    {
        this.floor = floor;
    }
}
