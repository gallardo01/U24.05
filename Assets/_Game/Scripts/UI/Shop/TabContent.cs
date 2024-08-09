using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabContent<ItemType> : MonoBehaviour, ITabContent where ItemType : Enum
{
    [SerializeField] protected ItemType itemType;
    [SerializeField] protected TabButton tabButton;
    [SerializeField] protected Transform contentParent;
    [SerializeField] protected List<ItemType> listItemType = new();
    [SerializeField] protected ShopItem<ItemType> shopItemPrefab;
    [SerializeField] protected List<ShopItem<ItemType>> listShopItem = new();

    protected void Awake()
    {
        this.RegisterListener(EventID.OnTabSelected, (param) =>
        {
            bool isSelected = (TabButton)param == tabButton;
            tabButton.SelectTab(isSelected);
            SelectTab(isSelected);
        });
    }
    protected void Start()
    {
        InitTabContent();
    }

    protected void InitTabContent()
    {
        GetListItemType(itemType);
        for (int i = 1; i < listItemType.Count; i++)
        {
            ShopItem<ItemType> shopItem = Instantiate(shopItemPrefab, contentParent);
            shopItem.InitShopItem(listItemType[i]);
            listShopItem.Add(shopItem);
        }
    }

    protected virtual void GetListItemType(ItemType itemType)
    {

    }

    public void SelectTab(bool isSelected)
    {
        gameObject.SetActive(isSelected);
        if (isSelected)
        {
            listShopItem[0].SelectItem();
        }
    }
}
