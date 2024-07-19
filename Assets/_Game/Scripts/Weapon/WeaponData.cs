using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour
{
    [SerializeField] List<WeaponDataDetail> weaponDataList;
    public List<WeaponDataDetail> WeaponDataList => weaponDataList;
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