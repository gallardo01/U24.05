using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class ColorController : Singleton<ColorController>   
{
    public enum Color
    {
        White,
        Black,
        Yellow,
        Red,
        Green,
        Orange,
        Brown,
        Purple,
        Pink,
        Blue
    }
    // Start is called before the first frame update

    public List<Material> materials = new List<Material>(); 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    } 

    public Material GetMaterialColor(int colorIndex)
    {
        return materials[colorIndex];
    }
}

