using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : Singleton<ColorController>
{
    [SerializeField] List<ColorMaterial> colorMaterials = new List<ColorMaterial>();
    [SerializeField] List<GameColor> colorsSystem = new List<GameColor>();

    private Dictionary<GameColor, Material> materials = new Dictionary<GameColor, Material>();

    public List<GameColor> colorsUsed = new List<GameColor>();
    public List<GameColor> colorsReady = new List<GameColor>();



    public Material GetMaterialColor(GameColor color)
    {
        return materials[color];
    }

    public List<GameColor> GenerateColor()
    {
        colorsUsed.Clear();
        colorsReady.Clear();

        HashSet<GameColor> systemColorsSet = new(colorsSystem);

        for (int i = 0; i < colorMaterials.Count; i++)
        {
            materials.Add(colorMaterials[i].color, colorMaterials[i].material);

            if (!systemColorsSet.Contains(colorMaterials[i].color))
            {
                colorsReady.Add(colorMaterials[i].color);
            }
        }

        for (int i = 0; i < Constants.QUANTITY_CHARACTER; i++)
        {
            if (colorsReady.Count > 0)
            {
                int temp = Random.Range(0, colorsReady.Count);
                colorsUsed.Add(colorsReady[temp]);
                colorsReady.RemoveAt(temp);
            }
        }

        return colorsUsed;
    }
}

[System.Serializable]
public class ColorMaterial
{
    public GameColor color;
    public Material material;
}
