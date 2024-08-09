using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabContentHair : TabContent<HairType>
{
    private void Start()
    {
        for (int i = 1; i < SkinManager.Ins.listHairType.Count; i++)
        {
            ShopItem<HairType> shopItem = Instantiate(shopItemPrefab, contentParent);
            shopItem.InitShopItem(SkinManager.Ins.listHairType[i]);
        }
    }
}
