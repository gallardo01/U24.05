using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : Singleton<ColorController>
{

    public enum Colors
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
        Yellow, 
        Count
    }

    public List<Material> materials = new List<Material>();


    void Start()
    {
        
    }

    public Material GetMaterialColor(int colorIndex)
    {
        return materials[colorIndex];
    }
}
