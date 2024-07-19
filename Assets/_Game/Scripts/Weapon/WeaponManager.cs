using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager>
{
    private Dictionary<WeaponType, WeaponDataDetail> weaponDataMap = new Dictionary<WeaponType, WeaponDataDetail>();
    public Dictionary<WeaponType, WeaponDataDetail> WeaponDataMap => weaponDataMap;

    private void Awake()
    {
        InitWeaponDataMap();
    }

    private void InitWeaponDataMap()
    {
        List<WeaponDataDetail> weaponDataList = DataManager.Ins.GetWeaponDataList();

        for (int i = 0; i < weaponDataList.Count; i++)
        {
            weaponDataMap.Add(weaponDataList[i].weaponType, weaponDataList[i]);
        }
    }

    public void InitWeapon(WeaponType weaponType, float levelScale, Vector3 startPoint, Vector3 moveDirection, float attackRange)
    {
        Weapon weapon = (Weapon)SimplePool.Spawn(weaponDataMap[weaponType].poolType, startPoint, Quaternion.identity);
        weapon.transform.localScale = new Vector3(levelScale, levelScale, levelScale);
        weapon.InitWeapon(startPoint, moveDirection, attackRange);
    }
}