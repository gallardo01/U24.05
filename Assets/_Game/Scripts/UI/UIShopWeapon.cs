using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShopWeapon : UICanvas
{
    [SerializeField] Button btnQuit, btnBuy, btnEquip;
    [SerializeField] GameObject Equipped;
    [SerializeField] TextMeshProUGUI textGold, textName, textPrice;

    [SerializeField] Image imageItem;
    [SerializeField] Transform contentParent;
    [SerializeField] List<WeaponType> listWeaponType = new();

    string weaponName;
    int price;
    int currentItemIndex;

    private void Awake()
    {
        btnQuit.onClick.AddListener(() =>
        {
            CloseDirectly();
            UIManager.Ins.OpenUI<UIMainmenu>();
        });

        this.RegisterListener(EventID.OnGoldChanged, (param) =>
        {
            UpdateTextGold(DataManager.Ins.GetCurrentGold());
        });

        UpdateTextGold(DataManager.Ins.GetCurrentGold());
        InitShopWeapon();
    }

    private void InitShopWeapon()
    {
        listWeaponType = WeaponManager.Ins.listWeaponType;
        for (int i = 0; i < listWeaponType.Count; i++)
        {
            if (DataManager.Ins.GetCurrentItem<WeaponType>() == listWeaponType[i])
            {
                currentItemIndex = i;
                InitItem(currentItemIndex);
                break;
            }
        }
    }

    private void InitItem(int index)
    {
        WeaponDataDetail weaponDataDetail = WeaponManager.Ins.GetWeaponData(listWeaponType[index]);
        if (weaponDataDetail != null)
        {
            weaponName = weaponDataDetail.name;
            price = weaponDataDetail.price;
            textName.text = weaponName;
            textPrice.text = price.ToString();
        }
    }

    private void NextItem()
    {
        int tempIndex = currentItemIndex + 1;
        if (tempIndex < listWeaponType.Count && tempIndex > 0)
        {
            currentItemIndex = tempIndex;
        }
    }

    private void PreviousItem()
    {
        int tempIndex = currentItemIndex - 1;
        if (tempIndex < listWeaponType.Count && tempIndex > 0)
        {

        }
    }

    private void RefreshButton()
    {
        btnBuy.gameObject.SetActive(false);
        btnEquip.gameObject.SetActive(false);
        Equipped.SetActive(false);

        if (DataManager.Ins.GetCurrentItem<WeaponType>() == listWeaponType[currentItemIndex])
        {
            Equipped.SetActive(true);
            return;
        }
        if (DataManager.Ins.IsItemUnlocked<WeaponType>(listWeaponType[currentItemIndex]))
        {
            btnEquip.gameObject.SetActive(true);
            return;
        }
        btnBuy.gameObject.SetActive(true);
    }

    private void UpdateTextGold(int gold)
    {
        textGold.text = gold.ToString();
    }

    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChangeGameState(GameState.ShopWeapon);      
    }
}
