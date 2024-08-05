using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : Singleton<ItemDatabase>
{
    public List<GameObject> weapons;
    public List<Material> pantsMaterial;
    public List<GameObject> hats;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject GetHatsById(int id)
    {
        return hats[id];
    }

    public GameObject GetWeaponsById(int id)
    {
        return weapons[id];
    }

    public Material GetPantsMaterialsById(int id)
    {
        return pantsMaterial[id];
    }
}
