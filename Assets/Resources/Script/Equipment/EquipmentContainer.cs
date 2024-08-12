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

        if(data.itemName == "Default")
        {
            IsPurchase = true;
            IsEquip = true;
            return;
        }

        IsPurchase = ES3.Load<bool>("IsPurchase_" + ItemName.text, false);
    }

    public void Purchase()
    {
        ES3.Save<bool>("IsPurchase_" + ItemName.text, true);
        IsPurchase = true;
    }

    public void Equipped(bool check) 
    { 
        IsEquip = check; 
    }

}
