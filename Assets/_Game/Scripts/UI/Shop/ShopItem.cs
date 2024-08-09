using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem<ItemType> : MonoBehaviour, IShopItem where ItemType : Enum
{
    [SerializeField] protected ItemType itemType;
    [SerializeField] protected GameObject imageSelected, Lock, Equipped;
    [SerializeField] protected Button btnShopItem;
    [SerializeField] protected Image imageItem;

    protected bool isItemEquipped;
    protected bool isItemUnlocked;
    protected int price;
    public bool IsItemEquipped() => isItemEquipped;
    public bool IsItemUnlocked() => isItemUnlocked;
    public int GetPrice() => price;

    protected void Awake()
    {
        btnShopItem.onClick.AddListener(() =>
        {
            SelectItem();
        });

        this.RegisterListener(EventID.OnShopItemSelected, (param) =>
        {
            ShopItem<ItemType> selectedItem = param as ShopItem<ItemType>;
            if (selectedItem != null && selectedItem != this)
            {
                ChangeSelectState(false);
            }
        });

        this.RegisterListener(EventID.OnItemEquipped, (param) =>
        {
            ShopItem<ItemType> equippedItem = param as ShopItem<ItemType>;
            if (equippedItem != null && equippedItem != this)
            {
                ChangeEquipState(false);
            }
        });
    }

    public virtual void InitShopItem(ItemType itemType)
    {
        this.itemType = itemType;

        //------bring this to override---------------------
        //DataDetail<T> dataDetail = SkinManager.Ins.GetData<T>(t);
        //if (dataDetail != null)
        //{
        //    imageItem.sprite = dataDetail.imageSprite;
        //    price = dataDetail.price;
        //}
        //--------------------------------------------------

        isItemUnlocked = DataManager.Ins.IsItemUnlocked<ItemType>(itemType);
        Lock.SetActive(!isItemUnlocked);

        bool equipState = DataManager.Ins.GetCurrentItem<ItemType>().Equals(itemType);
        ChangeEquipState(equipState);
    }

    public void ChangeSelectState(bool state)
    {
        imageSelected.SetActive(state);
    }

    public void SelectItem()
    {
        ChangeSelectState(true);

        this.PostEvent(EventID.OnShopItemSelected, this);
    }

    public void EquipItem()
    {
        DataManager.Ins.SaveCurrentItem<ItemType>(itemType);
        ChangeEquipState(true);

        this.PostEvent(EventID.OnItemEquipped, this);
    }

    public void UnequipItem()
    {
        DataManager.Ins.SaveCurrentItem<ItemType>((ItemType)(object)0);
        ChangeEquipState(false);
    }

    protected void ChangeEquipState(bool state)
    {
        isItemEquipped = state;
        Equipped.SetActive(isItemEquipped);
    }

    public void UnlockItem()
    {
        DataManager.Ins.UnlockItem<ItemType>(itemType);

        isItemUnlocked = true;
        Lock.SetActive(false);
    }
}
