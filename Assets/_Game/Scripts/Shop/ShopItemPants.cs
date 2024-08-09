using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemPants : ShopItem<PantsType>
{
    public override void InitShopItem(PantsType t)
    {
        base.InitShopItem(t);
        PantsDataDetail pantsDataDetail = SkinManager.Ins.GetPantsData(t);
        if (pantsDataDetail != null)
        {
            imageItem.sprite = pantsDataDetail.imageSprite;
            price = pantsDataDetail.price;
        }
    }
}
