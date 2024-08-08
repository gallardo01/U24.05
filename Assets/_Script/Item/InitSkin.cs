using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSkin : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] Transform head;
    [SerializeField] Transform shield;
    [SerializeField] SkinnedMeshRenderer pant;
    // OnStart is called before the first frame update
    void Start()
    {
        InitWeapon(Random.Range(0, ItemDatabase.instance.weapons.Count));
        InitHead(Random.Range(0, ItemDatabase.instance.heads.Count));
        InitShield(Random.Range(0, ItemDatabase.instance.shield.Count));
        InitPant(Random.Range(0, ItemDatabase.instance.pants.Count));
    }

    private void InitWeapon(int number)
    {
        GameObject weapon = Instantiate(ItemDatabase.instance.weapons[number], this.weapon);
    }
    private void InitHead(int number)
    {
        GameObject weapon = Instantiate(ItemDatabase.instance.heads[number], this.head);
    }
    private void InitShield(int number)
    {
        GameObject weapon = Instantiate(ItemDatabase.instance.shield[number], this.shield);
    }
    private void InitPant(int number)
    {
        this.pant.material = ItemDatabase.instance.pants[number];
    }
}
