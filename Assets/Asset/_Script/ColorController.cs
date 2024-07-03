using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : Singleton<ColorController>
{
    public enum ColorType
    {
        Blue,
        Gray,
        Green,
        Purple,
        Red,
        Yellow
    }
    public List<Material> materials = new List<Material>();

    public Material GetColor(int colorIndex)
    {
        return materials[colorIndex];
    }
}
