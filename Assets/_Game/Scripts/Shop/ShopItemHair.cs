using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemHair : ShopItem<HairType>
{
    public override void InitShopItem(HairType t)
    {
        base.InitShopItem(t);
        HairDataDetail hairDataDetail = SkinManager.Ins.GetHairData(t);
        if (hairDataDetail != null)
        {
            imageItem.sprite = hairDataDetail.imageSprite;
            price = hairDataDetail.price;
        }
    }
}
