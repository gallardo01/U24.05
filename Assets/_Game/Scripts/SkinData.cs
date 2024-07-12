using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinData : MonoBehaviour
{
    [SerializeField] List<Skin> skins;
    public List<Skin> Skins => skins;

    public Skin GetSkin(GameColor color)
    {
        for (int i = 0; i < skins.Count; i++)
        {
            if (color == skins[i].color)
            {
                return skins[i];
            }
        }

        return null;
    }
}



[System.Serializable]
public class Skin
{
    public GameColor color;
    public SkinUnlockType unlockType;
    public int price;
    public Texture2D skinImage;

}
