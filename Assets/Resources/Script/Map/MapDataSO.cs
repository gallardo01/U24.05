using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapDataSO", menuName = "ScriptableObject / MapDataSO" , order = 2)]
public class MapDataSO : ScriptableObject
{
    [TableList] public MapData[] mapDatas;
}

[System.Serializable]
public struct MapData
{
    public GameObject prefab;
    public string name; 
    public Sprite icon;
    public float price;
}
