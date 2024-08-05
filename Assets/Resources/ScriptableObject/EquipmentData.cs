using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentDataSO", menuName = "ScriptableObject /EquipmentData ", order = 0)]
public class EquipmentData : ScriptableObject
{
    public GameObject[] weapons;
    public GameObject[] head;
    public GameObject[] shield;
    public SkinnedMeshRenderer[] pantMaterial;


}
