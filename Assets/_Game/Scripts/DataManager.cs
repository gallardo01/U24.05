using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public WeaponData weaponData;
    public PantsData pantsData;
    public HairData hairData;
    public ShieldData shieldData;


    public void UnlockItem<ItemType>(ItemType itemType) where ItemType : Enum
    {
        string itemTypeName = typeof(ItemType).Name;
        PlayerPrefs.SetInt(Constants.PP_IS_UNLOCK_ITEM + itemTypeName + itemType.ToString(), 1);
    }

    public bool IsItemUnlocked<ItemType>(ItemType itemType) where ItemType : Enum
    {
        string itemTypeName = typeof(ItemType).Name;
        return PlayerPrefs.GetInt(Constants.PP_IS_UNLOCK_ITEM + itemTypeName + itemType.ToString(), 0) == 1;
    }

    public void SaveCurrentItem<ItemType>(ItemType itemType) where ItemType : Enum
    {
        string itemTypeName = typeof(ItemType).Name;
        PlayerPrefs.SetInt(Constants.PP_CURRENT_ITEM + itemTypeName, Convert.ToInt32(itemType));
    }

    public ItemType GetCurrentItem<ItemType>() where ItemType : Enum
    {
        string itemTypeName = typeof(ItemType).Name;
        return (ItemType)(object)PlayerPrefs.GetInt(Constants.PP_CURRENT_ITEM + itemTypeName, 0);
    }

    public void SavePlayerName(string name)
    {
        PlayerPrefs.SetString(Constants.PP_PLAYER_NAME, name);
    }

    public string GetPlayerName()
    {
        return PlayerPrefs.GetString(Constants.PP_PLAYER_NAME, "Player");
    }

    public void SaveCurrentGold(int gold)
    {
        PlayerPrefs.SetInt(Constants.PP_CURRENT_GOLD, gold);
    }

    public int GetCurrentGold()
    {
        return PlayerPrefs.GetInt(Constants.PP_CURRENT_GOLD, 0);
    }

    public void AddGold(int gold)
    {
        int currentGold = GetCurrentGold();
        currentGold += gold;
        SaveCurrentGold(currentGold);
    }
}
