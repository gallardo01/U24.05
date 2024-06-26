using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Character : MonoBehaviour
{
    public SkinnedMeshRenderer character;
    public int colorIndex = 0;
    public void SetPlayerColor(int color)
    {
        colorIndex = color;
        character.material = ColorController.Instance.GetColor(colorIndex);
    }
}
