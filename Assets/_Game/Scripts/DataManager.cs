using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [SerializeField] WeaponData weaponData;

    public List<WeaponDataDetail> GetWeaponDataList()
    {
        return weaponData.WeaponDataList;
    }
}
