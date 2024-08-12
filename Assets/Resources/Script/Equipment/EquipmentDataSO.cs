using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentDataSO", menuName = "ScriptableObject /EquipmentData ", order = 0)]
public class EquipmentDataSO : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public GameObject itemPrefab;
    public GameObject projectTilePrefab;
    public Material itemMat;
    public Sprite itemIcon;
    public int price;

}
