using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : Singleton<ColorController>
{
    [SerializeField] List<ColorMaterial> colorMaterials = new List<ColorMaterial>();
    [SerializeField] List<Color> colorsSystem = new List<Color>();

    private Dictionary<Color, Material> materials = new Dictionary<Color, Material>();

    public List<Color> colorsUsed = new List<Color>();
    public List<Color> colorsReady = new List<Color>();



    public Material GetMaterialColor(Color color)
    {
        return materials[color];
    }

    public void GenerateColor()
    {
        HashSet<Color> systemColorsSet = new(colorsSystem);

        for (int i = 0; i < colorMaterials.Count; i++)
        {
            materials.Add(colorMaterials[i].color, colorMaterials[i].material);

            if (!systemColorsSet.Contains(colorMaterials[i].color))
            {
                colorsReady.Add(colorMaterials[i].color);
            }
        }

        for (int i = 0; i < Constants.QUANTITY_COLOR_GENERATE; i++)
        {
            if (colorsReady.Count > 0)
            {
                int temp = Random.Range(0, colorsReady.Count);
                colorsUsed.Add(colorsReady[temp]);
                colorsReady.RemoveAt(temp);
            }
        }
    }
}

[System.Serializable]
public class ColorMaterial
{
    public Color color;
    public Material material;
}
