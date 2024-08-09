using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem<T> : MonoBehaviour, IShopItem where T : Enum
{
    [SerializeField] protected T t;
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
            ChangeSelectState(true);
            this.PostEvent(EventID.OnShopItemSelected, this);
        });

        this.RegisterListener(EventID.OnShopItemSelected, (param) =>
        {
            ShopItem<T> selectedItem = param as ShopItem<T>;
            if (selectedItem != null && selectedItem != this)
            {
                ChangeSelectState(false);
            }
        });

        this.RegisterListener(EventID.OnItemEquipped, (param) =>
        {
            ShopItem<T> equippedItem = param as ShopItem<T>;
            if (equippedItem != null && equippedItem != this)
            {
                ChangeEquipState(false);
            }
        });
    }

    public virtual void InitShopItem(T t)
    {
        this.t = t;

        //------bring this to override---------------------
        //DataDetail<T> dataDetail = SkinManager.Ins.GetData<T>(t);
        //if (dataDetail != null)
        //{
        //    imageItem.sprite = dataDetail.imageSprite;
        //    price = dataDetail.price;
        //}
        //--------------------------------------------------

        isItemUnlocked = DataManager.Ins.IsItemUnlocked<T>(t);
        Lock.SetActive(!isItemUnlocked);

        bool equipState = DataManager.Ins.GetCurrentItem<T>().Equals(t);
        ChangeEquipState(equipState);
    }

    public void ChangeSelectState(bool state)
    {
        imageSelected.SetActive(state);
    }

    public void EquipItem()
    {
        DataManager.Ins.SaveCurrentItem<T>(t);
        ChangeEquipState(true);

        this.PostEvent(EventID.OnItemEquipped, this);
    }

    public void UnequipItem()
    {
        DataManager.Ins.SaveCurrentItem<T>((T)(object)0);
        ChangeEquipState(false);
    }

    protected void ChangeEquipState(bool state)
    {
        isItemEquipped = state;
        Equipped.SetActive(isItemEquipped);
    }

    public void UnlockItem()
    {
        DataManager.Ins.UnlockItem<T>(t);

        isItemUnlocked = true;
        Lock.SetActive(false);
    }
}
