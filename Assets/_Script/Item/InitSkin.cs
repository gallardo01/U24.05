using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemJSONDatabase;

public class InitSkin : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] Transform head;
    [SerializeField] Transform shield;
    [SerializeField] SkinnedMeshRenderer pant;

    public GameObject weaponEquiped;
   
    public void DeleteOldItem(string type)
    {
        switch (type)
        {
            case "Weapon":
                foreach (Transform transformChild in weapon)
                {
                    Destroy(transformChild.gameObject);
                }
                break;
            case "Head":
                foreach (Transform transformChild in head)
                {
                    Destroy(transformChild.gameObject);
                }
                break;
            case "Shield":
                foreach (Transform transformChild in shield)
                {
                    Destroy(transformChild.gameObject);
                }
                break;
        }
    }
    public void PlayerEquipItem()
    {
        string weaponName = ItemJSONDatabase.instance.CheckEquipItem("Weapon");
        if (weaponName != "Null")
        {
            GameObject weaponEquip = Resources.Load<GameObject>("Prefabs/Item/Weapon/" + weaponName);
            GameObject weapon = Instantiate(weaponEquip, this.weapon);
            weaponEquiped = weaponEquip;
        }

        string headName = ItemJSONDatabase.instance.CheckEquipItem("Head");
        if (headName != "Null")
        {
            GameObject headEquip = Resources.Load<GameObject>("Prefabs/Item/Head/" + headName);
            GameObject head = Instantiate(headEquip, this.head);
        } else
        {
            Debug.Log("ok");
        }

        string shieldName = ItemJSONDatabase.instance.CheckEquipItem("Shield");
        if (shieldName != "Null")
        {
            GameObject shieldEquip = Resources.Load<GameObject>("Prefabs/Item/Shield/" + shieldName);
            GameObject shield = Instantiate(shieldEquip, this.shield);
        }

        string pantName = ItemJSONDatabase.instance.CheckEquipItem("Pant");
        if (pantName != "Null")
        {
            this.pant.material = Resources.Load<Material>("Prefabs/Item/Pant/" + pantName);
        }
    }

    public void BotEquipItem()
    {
        InitWeapon(Random.Range(0, ItemDatabase.instance.weapons.Count));
        InitHead(Random.Range(0, ItemDatabase.instance.heads.Count));
        InitShield(Random.Range(0, ItemDatabase.instance.shields.Count));
        InitPant(Random.Range(0, ItemDatabase.instance.pants.Count));
    }
    private void InitWeapon(int number)
    {
        GameObject weaponChoose = ItemDatabase.instance.weapons[number];
        GameObject weapon = Instantiate(weaponChoose, this.weapon);
        weaponEquiped = weaponChoose;
    }
    private void InitHead(int number)
    {
        GameObject headChoose = ItemDatabase.instance.heads[number];
        GameObject head = Instantiate(ItemDatabase.instance.heads[number], this.head);
    }
    private void InitShield(int number)
    {
        GameObject shieldChoose = ItemDatabase.instance.shields[number];
        GameObject shield = Instantiate(ItemDatabase.instance.shields[number], this.shield);
    }
    private void InitPant(int number)
    {
        this.pant.material = ItemDatabase.instance.pants[number];
    }
}
