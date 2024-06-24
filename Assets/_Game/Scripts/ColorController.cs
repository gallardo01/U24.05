using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : Singleton<ColorController>
{
    public enum Color
    {
        Blue,
        Brown,  
        Green,
        Orange,
        Pink,
        Red,     
        Yellow,
    }

    public enum SystemColor
    {
        Gray,
        White,
    }

    [SerializeField] List<Material> materials = new List<Material>();
    [SerializeField] List<Material> systemMaterials = new List<Material>();

    public List<int> colorsIndexUsed = new List<int>();
    public List<int> colorsIndexReady = new List<int>();


    private void Start()
    {
        OnInit();
    }

    public Material GetMaterialColor(int materialIndex)
    {
        return materials[materialIndex];
    }

    public void GenerateColor()
    {
        for (int i = 0; i < LevelManager.Ins.NumberColors; i++)
        {
            if (colorsIndexReady.Count > 0)
            {
                int index = Random.Range(0, colorsIndexReady.Count);
                colorsIndexUsed.Add(colorsIndexReady[index]);
                colorsIndexReady.RemoveAt(index);
            }
        }
    }

    public void OnInit()
    {
        for (int i = 0; i < materials.Count; i++)
        {
            colorsIndexReady.Add(i);
        }

        GenerateColor();
    }
}
