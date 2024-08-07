using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PantsData", menuName = "ScriptableObjects/PantsData", order = 2)]
public class PantsData : ScriptableObject
{
    [SerializeField] List<PantsDataDetail> pantsDataList;

    public List<PantsDataDetail> GetPantsDataList()
    {
        return new List<PantsDataDetail>(pantsDataList);
    }
}


[System.Serializable]
public class PantsDataDetail
{
    public PantsType pantsType;
    public int price;
    public Sprite pantImageSprite;
    public Material pantsMaterial;
    public BuffType buffType;
}