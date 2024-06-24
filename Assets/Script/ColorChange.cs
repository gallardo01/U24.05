using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : Singleton<ColorChange>
{
    public enum Color
    {
        Black,
        Blue,
        Brown,
        Gray,
        Green,
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

   public Material GetMaterialColor(int  colorIndex)
    {
        return materials[colorIndex];
    }
}
