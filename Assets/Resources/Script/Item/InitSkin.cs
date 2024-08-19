using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSkin : MonoBehaviour
{
    [SerializeField]  Transform weapons;
    [SerializeField]  Transform shield;
    [SerializeField]  Transform head;
    [SerializeField]  SkinnedMeshRenderer pants;

    
    void Start()
    {
        
    }

    public void RandomEquipItem()
    {
        InitWeapon(Random.Range(0, ItemDatabase.Ins.weapons.Count));
        InitShield(Random.Range(0, ItemDatabase.Ins.shields.Count));
        InitHead(Random.Range(0, ItemDatabase.Ins.heads.Count));
        InitPants(Random.Range(0, ItemDatabase.Ins.pants.Count));
    }
    
    public void PlayerEquipItem()
    {
        
        // xoa do cu 
        
        foreach (Transform child in weapons)
        {
            Destroy(child.gameObject);
        }
        
        foreach (Transform child in shield)
        {
            Destroy(child.gameObject);
        }
        
        foreach (Transform child in head)
        {
            Destroy(child.gameObject);
        }
        
        
        int idWeapon = ItemJsonDatabase.Ins.GetIdOfItemsEquiped("Weapons");
        if (idWeapon > 0)
        {
            InitWeapon(idWeapon - 1);
        }
        int idShield = ItemJsonDatabase.Ins.GetIdOfItemsEquiped("Shield");
        if (idShield > 0)
        {
            InitShield(idShield - 1);
        }
        int idHat = ItemJsonDatabase.Ins.GetIdOfItemsEquiped("Hat");
        if (idHat > 0)
        {
            InitHead(idHat - 1);
        }
        int idPants = ItemJsonDatabase.Ins.GetIdOfItemsEquiped("Pants");
        if (idPants > 0)
        {
            InitPants(idPants - 1);
        }
    }
    
    public void InitWeapon(int index)
    {
        GameObject weapon = ItemDatabase.Ins.GetWeapon(index);
        Instantiate(weapon, weapons);
    }
    
    public void InitShield(int index)
    {
        GameObject shield = ItemDatabase.Ins.GetShield(index);
        Instantiate(shield, this.shield);
    }
    
    public void InitHead(int index)
    {
        GameObject head = ItemDatabase.Ins.GetHead(index);
        Instantiate(head, this.head);
    }
    
    public void InitPants(int index)
    {
        Material pants = ItemDatabase.Ins.GetPants(index);
        this.pants.material = pants;
    }
}
