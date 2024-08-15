using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemJSONDatabase;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    public List<GameObject> weapons;
    public List<GameObject> heads;
    public List<GameObject> shields;
    public List<Material> pants;

    private void Awake()
    {
        instance = this;
    }

    public GameObject EquipWeapon (int id)
    {
        return weapons[id];
    }
}
