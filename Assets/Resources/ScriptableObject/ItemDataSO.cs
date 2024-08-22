using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TESTING
[CreateAssetMenu(fileName = "ItemDataSO", menuName = "ScriptableObject /ItemData ", order = 1)]
public class ItemDataSO : ScriptableObject
{
    [Title("Weapon Items")]
    [HorizontalGroup("Items", LabelWidth = 0)]
    [TableList]
    public Item[] items;
}

[System.Serializable]
public struct Item
{
    public string itemName;
    public ItemType itemType;
    public GameObject itemPrefab;
    public GameObject projectTilePrefab;
    public Material itemMat;
    public Sprite itemIcon;
    public int itemPrice;
    public StatData[] itemStats;
}

