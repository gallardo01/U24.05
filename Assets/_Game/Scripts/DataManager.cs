using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [SerializeField] SkinData skinData;

    public GameColor GetPlayerSkin()
    {
        return GameColor.Red;
    }

    public List<Skin> GetSkinList()
    {
        return skinData.Skins;
    }

    public Skin GetSkin(GameColor color)
    {
        return skinData.GetSkin(color);
    }
}
