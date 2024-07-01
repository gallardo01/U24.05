using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : Singleton<ColorController>
{
    public enum Color
    {
        Red,
        Orange,
        Green,
        Blue,
        Yellow,
        Purple,
    }

    [SerializeField] private List<Material> colors = new List<Material>();

    public Material GetColor(int index) => colors[index];

    public Material SetColor(Color color) => colors[((int)color)];

}
