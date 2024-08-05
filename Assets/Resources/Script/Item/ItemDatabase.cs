using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : Singleton<ItemDatabase>
{
    public List<GameObject> weapons;
    public List<GameObject> shields;
    public List<GameObject> heads;
    public List<Material> pants;
    // Start is called before the first frame update
    void Start()
    {
        
    }

   public GameObject GetWeapon(int index)
    {
        return weapons[index];
    }
   
   public GameObject GetShield(int index)
    {
        return shields[index];
    }
    
    public GameObject GetHead(int index)
    {
        return heads[index];
    }
    
    public Material GetPants(int index)
    {
        return pants[index];
    }
}
