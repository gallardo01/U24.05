using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSkin : MonoBehaviour
{
    [SerializeField] Transform weapons;
    [SerializeField] Transform head;
    [SerializeField] Transform shield;
    [SerializeField] SkinnedMeshRenderer pants;
    public int weaponsId = 0;
    public GameObject weaponItem;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void PlayerEquipItems()
    {
        // xoa do` cu~ di
        foreach (Transform obj in weapons)
        {
            Destroy(obj.gameObject);
        }
        foreach (Transform obj in head)
        {
            Destroy(obj.gameObject);
        }
        foreach (Transform obj in shield)
        {
            Destroy(obj.gameObject);
        }
        int idWeapons = ItemJsonDatabase.Instance.GetIdOfItemsEquiped("Weapons");
        if (idWeapons > 0)
        {
            InitWeapons(idWeapons - 1);
        }
        int idHat = ItemJsonDatabase.Instance.GetIdOfItemsEquiped("Hat");
        if (idHat > 0)
        {
            InitHats(idHat - 1);
        }
        int idShield = ItemJsonDatabase.Instance.GetIdOfItemsEquiped("Shield");
        if (idShield > 0)
        {
            InitShield(idShield - 1);
        }
        int idPants = ItemJsonDatabase.Instance.GetIdOfItemsEquiped("Pants");
        if (idPants > 0)
        {
            InitPants(idPants - 1);
        }
    }

    public void RandomEquipItems()
    {
        InitWeapons(Random.Range(0, ItemDatabase.Instance.weapons.Count));
        InitPants(Random.Range(0, ItemDatabase.Instance.pantsMaterial.Count));
        InitHats(Random.Range(0, ItemDatabase.Instance.hats.Count));
        InitShield(Random.Range(0, ItemDatabase.Instance.shields.Count));
    }
    public void InitWeapons(int id)
    {
        weaponsId = id;
        GameObject weapon = ItemDatabase.Instance.GetWeaponsById(id);
        weaponItem = Instantiate(weapon, weapons);
    }

    public void InitHats(int id)
    {
        GameObject hat = ItemDatabase.Instance.GetHatsById(id);
        Instantiate(hat, head);
    }

    public void InitPants(int id)
    {
        Material pantsMaterial = ItemDatabase.Instance.GetPantsMaterialsById(id);
        pants.material = pantsMaterial;
    }

    public void InitShield(int id)
    {
        GameObject shield = ItemDatabase.Instance.GetShieldsById(id);
        Instantiate(shield, this.shield);
    }
}
