using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : Singleton<SkinManager>
{
    private Dictionary<PantsType, PantsDataDetail> dictPantsData = new();
    private Dictionary<HairType, HairDataDetail> dictHairData = new();
    private Dictionary<ShieldType, ShieldDataDetail> dictShieldData = new();
    //private Dictionary<PantsType, DataDetail<PantsType>> dictPantsData1 = new();
    //private Dictionary<HairType, DataDetail<HairType>> dictHairData1 = new();
    //private Dictionary<ShieldType, DataDetail<ShieldType>> dictShieldData1 = new();
    public List<PantsType> listPantsType = new();
    public List<HairType> listHairType = new();
    public List<ShieldType> listShieldType = new();

    private void Awake()
    {
        InitSkinData();
    }

    private void InitSkinData()
    {
        List<PantsDataDetail> pantsDataList = DataManager.Ins.pantsData.GetPantsDataList();
        for (int i = 0; i < pantsDataList.Count; i++)
        {
            dictPantsData.Add(pantsDataList[i].pantsType, pantsDataList[i]);
            listPantsType.Add(pantsDataList[i].pantsType);
        }

        List<HairDataDetail> hairDataList = DataManager.Ins.hairData.GetHairDataList();
        for (int i = 0; i < hairDataList.Count; i++)
        {
            dictHairData.Add(hairDataList[i].hairType, hairDataList[i]);
            listHairType.Add(hairDataList[i].hairType);
        }

        List<ShieldDataDetail> shieldDataList = DataManager.Ins.shieldData.GetShieldDataList();
        for (int i = 0; i < shieldDataList.Count; i++)
        {
            dictShieldData.Add(shieldDataList[i].shieldType, shieldDataList[i]);
            listShieldType.Add(shieldDataList[i].shieldType);
        }
    }

    //public DataDetail<T> GetData<T>(T t) where T : Enum
    //{
    //    if (t is HairType hairType)
    //    {
    //        if (dictHairData1.TryGetValue(hairType, out DataDetail<HairType> hairData))
    //        {
    //            return hairData as DataDetail<T>;
    //        }
    //    }
    //    else if (t is ShieldType shieldType)
    //    {
    //        if (dictShieldData1.TryGetValue(shieldType, out DataDetail<ShieldType> shieldData))
    //        {
    //            return shieldData as DataDetail<T>;
    //        }
    //    }
    //    else if (t is PantsType pantsType)
    //    {
    //        if (dictPantsData1.TryGetValue(pantsType, out DataDetail<PantsType> pantsData))
    //        {
    //            return pantsData as DataDetail<T>;
    //        }
    //    }

    //    return null;
    //}

    public HairDataDetail GetHairData(HairType hairType)
    {
        if (dictHairData.TryGetValue(hairType, out HairDataDetail hairData))
        {
            return hairData;
        }
        else
        {
            return null;
        }
    }

    public ShieldDataDetail GetShieldData(ShieldType shieldType)
    {
        if (dictShieldData.TryGetValue(shieldType, out ShieldDataDetail shieldData))
        {
            return shieldData;
        }
        else
        {
            return null;
        }
    }

    public PantsDataDetail GetPantsData(PantsType pantsType)
    {
        if (dictPantsData.TryGetValue(pantsType, out PantsDataDetail pantsData))
        {
            return pantsData;
        }
        else
        {
            return null;
        }
    }

    public PantsType GetRandomPants()
    {
        return listPantsType[Random.Range(0, listPantsType.Count)];
    }

    public HairType GetRandomHair()
    {
        return listHairType[Random.Range(0, listHairType.Count)];
    }

    public ShieldType GetRandomShield()
    {
        return listShieldType[Random.Range(0, listShieldType.Count)];
    }
}
