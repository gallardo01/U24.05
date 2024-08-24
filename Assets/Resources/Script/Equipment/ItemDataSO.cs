using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TESTING
[CreateAssetMenu(fileName = "ItemDataSO", menuName = "ScriptableObject /ItemData ", order = 1)]
public class ItemDataSO : ScriptableObject
{
    [Title("Weapon Items")]
    [TableList]
    public Item[] weaponItems; 

    [Title("Shield Items")]
    [TableList]
    public Item[] shieldItems; 

    [Title("Hat Items")]
    [TableList]
    public Item[] hatItems; 

    [Title("Pant Items")]
    [TableList]
    public Item[] pantItems; 
}

[System.Serializable]
public struct Item
{
    public string itemName;
    public ItemType itemType;
    public GameObject itemPrefab;
    public GameObject projectTilePrefab;
    public Material itemMaterial;
    public Sprite itemIcon;
    public int itemPrice;
    public StatData[] itemStats;
}

[System.Serializable]
public enum Stat
{
    MoveSpeed,
    AttackSpeed,
    RangeAttack,
}

[System.Serializable]
public struct StatData
{
    public Stat stat;
    public float index;
}

