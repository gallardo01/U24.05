using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinData", menuName = "ScriptableObjects/SkinData", order = 2)]
public class SkinData : ScriptableObject
{
    [SerializeField] List<HairDataDetail> hairDataList;
    [SerializeField] List<ShieldDataDetail> shieldDataList;
}

[System.Serializable]
public class HairDataDetail
{
    public HairType hairType;
    public int price;
    public Sprite hairImageSprite;
    public GameObject HairPrefab;
    public BuffType buffType;
}


[System.Serializable]
public class ShieldDataDetail
{
    public ShieldType shieldType;
    public int price;
    public Sprite shieldImageSprite;
    public GameObject ShieldPrefab;
    public BuffType buffType;
}