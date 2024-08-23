using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEquipment : MonoBehaviour
{
    [SerializeField] Transform rightHandRoot;
    [SerializeField] Transform leftHandRoot;
    [SerializeField] Transform hatRoot;
    [SerializeField] SkinnedMeshRenderer pantSkin;
    [SerializeField] Character character;

    public void SetEquipMent(GameObject weaponItem, GameObject shieldItem, GameObject HatItem, Material material, GameObject projectile)
    {
        GetWeapon(weaponItem);
        GetShield(shieldItem);
        GetHat(HatItem);
        GetPant(material);
    }

    private void GetWeapon(GameObject item)
    {
        if(rightHandRoot.childCount != 0)
        {
            rightHandRoot.Clear();
        }
        Instantiate(item, rightHandRoot);
    }

    private void GetShield(GameObject item)
    {
        if (leftHandRoot.childCount != 0)
        {
            leftHandRoot.Clear();
        }
        Instantiate(item, leftHandRoot);
    }

    private void GetHat(GameObject item)
    {
        if (hatRoot.childCount != 0)
        {
            hatRoot.Clear();
        }
        Instantiate(item, hatRoot);
    }

    private void GetPant(Material material)
    {
        pantSkin.material = material;
    }

}
