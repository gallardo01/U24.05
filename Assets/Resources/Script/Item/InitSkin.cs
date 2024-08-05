using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSkin : MonoBehaviour
{
    [SerializeField] Transform weapons;
    [SerializeField] Transform head;
    [SerializeField] Transform shield;
    [SerializeField] SkinnedMeshRenderer pants;

    // Start is called before the first frame update
    void Start()
    {
        InitWeapons(Random.Range(0, ItemDatabase.Instance.weapons.Count));
        InitPants(Random.Range(0, ItemDatabase.Instance.pantsMaterial.Count));
        InitHats(Random.Range(0, ItemDatabase.Instance.hats.Count));
    }

    public void InitWeapons(int id)
    {
        GameObject weapon = ItemDatabase.Instance.GetWeaponsById(id);
        Instantiate(weapon, weapons);
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
}
