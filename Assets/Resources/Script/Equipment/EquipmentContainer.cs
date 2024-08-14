using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentContainer : MonoBehaviour
{
    [field: SerializeField] public Button Button { get; private set; }
    [field :SerializeField] public TextMeshProUGUI ItemName { get; private set; }
    [SerializeField] Image Icon;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] GameObject equipmentPrice;
    [SerializeField] GameObject purchaseIcon;
    [SerializeField] GameObject outline;
    [SerializeField] RectTransform rectTransform;

    public ItemType ItemType { get; private set; }
    public GameObject Prefab { get; private set; }
    public GameObject Projectile { get; private set; }
    public Material Material { get; private set; }
    public int Price { get; private set; }

    public bool IsPurchase { get; private set; }
    public bool IsEquip { get; private set; }

    public void OnInit(EquipmentDataSO data)
    {
        this.ItemName.text = data.itemName;
        this.Icon.sprite = data.itemIcon;
        this.ItemType = data.itemType;
        this.Prefab = data.itemPrefab;
        this.Projectile = data.projectTilePrefab;
        this.Material = data.itemMat;
        this.Price = data.price;
        this.priceText.text = Price.ToString();

        if(data.itemName == "Default")
        {
            SavePurchase();            
            SaveEquipped(true);
            return;
        }

        IsPurchase = ES3.Load<bool>("IsPurchase_" + ItemName.text, false);
        if (!IsPurchase) SetPurchase(false);
        else SetPurchase(true);

    }

    public void SavePurchase()
    {
        ES3.Save<bool>("IsPurchase_" + ItemName.text, true);
        IsPurchase = true;
        SetPurchase(true);
    }

    public void SaveEquipped(bool check) 
    {
        IsEquip = check;
        outline.SetActive(check);
    }

    private void SetPurchase(bool check)
    {
        purchaseIcon.SetActive(check);
        equipmentPrice.SetActive(!check);
    }

    public void Select()
    {
        rectTransform.localScale = Vector3.one;
        Button.transform.DOScale(transform.localScale * 1.2f, 0.3f);
    }

    public void UnSelect()
    {
        rectTransform.localScale = Vector3.one;
    }
}
