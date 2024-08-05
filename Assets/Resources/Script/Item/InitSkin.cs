using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSkin : MonoBehaviour
{
    [SerializeField] Transform weaponTF; 
    [SerializeField] Transform headTF; 
    [SerializeField] Transform shieldTF; 

    [SerializeField] SkinnedMeshRenderer pants; 

    // Start is called before the first frame update
    void Start()
    {
        InitWeapons(Random.Range(0, ItemDatabase.Instance.weapons.Count - 1));
        InitPants(Random.Range(0, ItemDatabase.Instance.pants.Count - 1));
        InitHead(Random.Range(0, ItemDatabase.Instance.heads.Count - 1));
        InitShield(Random.Range(0, ItemDatabase.Instance.shields.Count - 1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitWeapons(int id)
    {
        GameObject weapon = ItemDatabase.Instance.GetWeaponById(id);
        Instantiate(weapon, weaponTF);
    }

    public void InitPants(int id)
    {
        Material material = ItemDatabase.Instance.GetPantsMaterialById(id);
        pants.material = material;
    }

    public void InitHead(int id)
    {
        GameObject head = ItemDatabase.Instance.GetHeadById(id);
        Instantiate(head, headTF);
    }    

    public void InitShield(int id)
    {
        GameObject shield = ItemDatabase.Instance.GetShieldById(id);
        Instantiate(shield, shieldTF);
    }    
}
