using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentContainer : MonoBehaviour
{
    [field: SerializeField] public Button Button { get; private set; }
    [SerializeField] TextMeshProUGUI ItemName;
    [SerializeField] Image Icon;

    public ItemType ItemType { get; private set; }
    public GameObject Prefab { get; private set; }
    public Material Material { get; private set; }

    public bool IsBought { get; private set; }
    public bool IsEquip { get; private set; }

    public void OnInit(EquipmentDataSO data)
    {
        this.ItemName.text = data.itemName;
        this.Icon.sprite = data.itemIcon;
        this.ItemType = data.itemType;
        this.Prefab = data.itemPrefab;
        this.Material = data.itemMat;

        IsBought = ES3.Load<bool>(data.itemName, false);
    }
}
