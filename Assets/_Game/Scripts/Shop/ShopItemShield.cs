using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemShield : ShopItem<ShieldType>
{
    public override void InitShopItem(ShieldType t)
    {
        base.InitShopItem(t);
        ShieldDataDetail shieldDataDetail = SkinManager.Ins.GetShieldData(t);
        if (shieldDataDetail != null)
        {
            imageItem.sprite = shieldDataDetail.imageSprite;
            price = shieldDataDetail.price;
        }
    }
}
