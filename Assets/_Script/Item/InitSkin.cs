using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static ItemJSONDatabase;

public class InitSkin : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] Transform head;
    [SerializeField] Transform shield;
    [SerializeField] SkinnedMeshRenderer pant;
    public GameObject weaponEquiped;

    public Player self;
    public int itemDef;
    public int itemAtk;
   
    public void DeleteOldItem()
    {
        foreach (Transform transformChild in weapon)
        {
            Destroy(transformChild.gameObject);
        }
        foreach (Transform transformChild in head)
        {
            Destroy(transformChild.gameObject);
        }
        foreach (Transform transformChild in shield)
        {
            Destroy(transformChild.gameObject);
        }
        self.SetBaseStatAttack(0);
        self.SetBaseStatDefend(0);
    }
    public void PlayerEquipItem()
    {
        UserStat userStat = ItemJSONDatabase.instance.GetUserStat();
        string weaponName = ItemJSONDatabase.instance.CheckEquipItem("Weapon");
        if (weaponName != "Null")
        {
            GameObject weaponEquip = Resources.Load<GameObject>("Prefabs/Item/Weapon/" + weaponName);
            GameObject weapon = Instantiate(weaponEquip, this.weapon);
            weaponEquiped = weaponEquip;
        }
        else
        {
            GameObject weaponEquip = Resources.Load<GameObject>("Prefabs/Item/Weapon/axe_0");
            GameObject weapon = Instantiate(weaponEquip, this.weapon);
            weaponEquiped = weaponEquip;
        }

        string headName = ItemJSONDatabase.instance.CheckEquipItem("Head");
        if (headName != "Null")
        {
            GameObject headEquip = Resources.Load<GameObject>("Prefabs/Item/Head/" + headName);
            GameObject head = Instantiate(headEquip, this.head);
        }
        else
        {
            GameObject headEquip = Resources.Load<GameObject>("Prefabs/Item/Head/Arrow");
            GameObject head = Instantiate(headEquip, this.head);
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
        } else
        {
            this.pant.material = Resources.Load<Material>("Prefabs/Item/Pant/Pant_1");
        }
        itemAtk = userStat.atk;
        itemDef = userStat.def;
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
        GameObject shield = Instantiate(shieldChoose, this.shield);
    }
    private void InitPant(int number)
    {
        this.pant.material = ItemDatabase.instance.pants[number];
    }

    //private void AddStatToCharacter(string name)
    //{
    //    GameItem itemEquiped = ItemJSONDatabase.instance.GetStatOfItem(name);
    //    itemAtk = itemEquiped.item.atk;
    //    itemDef = itemEquiped.item.def;
    //    self.SetBaseStatAttack(itemAtk);
    //    self.SetBaseStatDefend(itemDef);
    //}
}
