using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    [SerializeField] List<WeaponDataDetail> weaponDataList;

    public List<WeaponDataDetail> GetWeaponDataList()
    {
        return new List<WeaponDataDetail>(weaponDataList);
    }
}

[System.Serializable]
public class WeaponDataDetail
{
    public WeaponType weaponType;
    public PoolType poolType;
    public int price;
    public GameObject weaponHoldPrefab;
    public List<WeaponSkinDetail> weaponSkinList;
}

[System.Serializable]
public class WeaponSkinDetail
{
    public Sprite weaponImageSprite;
    public Material material;
}