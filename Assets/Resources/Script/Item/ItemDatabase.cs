using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : Singleton<ItemDatabase>
{
    public List<GameObject> weapons;
    public List<GameObject> heads;
    public List<GameObject> shields;
    public List<Material> pants;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetWeaponById(int id)
    {
        return weapons[id];
    }
    public Material GetPantsMaterialById(int id)
    {
        return pants[id];
    }

    public GameObject GetHeadById(int id)
    {
        return heads[id];
    }    

    public GameObject GetShieldById(int id)
    {
        return shields[id];
    }    
}
