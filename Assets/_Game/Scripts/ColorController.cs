using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : Singleton<ColorController>
{
    [SerializeField] List<Material> materials = new List<Material>();

    public List<int> colorsIndexUsed = new List<int>();
    public List<int> colorsIndexReady = new List<int>();



    public Material GetMaterialColor(Color color)
    {
        return materials[(int) color];
    }

    public void GenerateColor()
    {
        for (int i = 2; i < materials.Count; i++)
        {
            colorsIndexReady.Add(i);
        }

        for (int i = 0; i < Constants.QUANTITY_COLOR_GENERATE; i++)
        {
            if (colorsIndexReady.Count > 0)
            {
                int temp = Random.Range(0, colorsIndexReady.Count);
                colorsIndexUsed.Add(colorsIndexReady[temp]);
                colorsIndexReady.RemoveAt(temp);
            }
        }
    }
}
