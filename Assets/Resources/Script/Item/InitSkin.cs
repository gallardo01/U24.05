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
        InitWeapon(Random.Range(0, ItemDatabase.Ins.weapons.Count));
        InitShield(Random.Range(0, ItemDatabase.Ins.shields.Count));
        InitHead(Random.Range(0, ItemDatabase.Ins.heads.Count));
        InitPants(Random.Range(0, ItemDatabase.Ins.pants.Count));
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
