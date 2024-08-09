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
        InitHairs(Random.Range(0, ItemDatabase.Instance.hairs.Count));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitWeapons(int id)
    {
        GameObject weapon = ItemDatabase.Instance.GetWeaponsById(id);
        Instantiate(weapon, weapons);
    }

    public void InitPants(int id)
    {
        Material weapon = ItemDatabase.Instance.GetPantsMaterialById(id);
        pants.material = weapon;
    }

    public void InitHairs(int id)
    {
        GameObject hair = ItemDatabase.Instance.GetHairsById(id);
        Instantiate(hair, head);
    }
}