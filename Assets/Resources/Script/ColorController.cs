using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : Singleton<ColorController>
{
    public enum Color
    {
        Black,
        Blue,
        Brown,
        Green,
        Grey,
        Orange,
        Pink,
        Red,
        White,
        Yellow
    }

    public List<Material> materials = new List<Material>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Material GetMaterialColor(int colorIndex)
    {
        return materials[colorIndex];
    }
}
