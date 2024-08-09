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
    public Material Mat { get; private set; }

    public void OnInit(string name,ItemType type , GameObject prefab, Material mat, Sprite icon )
    {
        this.ItemName.text = name;
        this.Icon.sprite = icon;
        this.ItemType = type;
        this.Prefab = prefab;
        this.Mat = mat;
    }
}
