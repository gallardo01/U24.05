using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [SerializeField] WeaponData weaponData;
    [SerializeField] SkinData skinData;

    public List<WeaponDataDetail> GetWeaponDataList()
    {
        return weaponData.WeaponDataList;
    }
}
