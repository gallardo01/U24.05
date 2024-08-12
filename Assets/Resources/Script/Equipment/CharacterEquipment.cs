using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEquipment : MonoBehaviour
{
    [SerializeField] Transform rightHandRoot;
    [SerializeField] Transform leftHandRoot;
    [SerializeField] Transform hatRoot;
    [SerializeField] SkinnedMeshRenderer pantSkin;

    public void GetEquipMent(GameObject weaponItem, GameObject shieldItem, GameObject HatItem, Material material)
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
            Destroy(rightHandRoot.GetChild(0).gameObject);
        }
        Instantiate(item, rightHandRoot);
    }

    private void GetShield(GameObject item)
    {
        if (leftHandRoot.childCount != 0)
        {
            Destroy(leftHandRoot.GetChild(0).gameObject);
        }
        Instantiate(item, leftHandRoot);
    }

    private void GetHat(GameObject item)
    {
        if (hatRoot.childCount != 0)
        {
            Destroy(hatRoot.GetChild(0).gameObject);
        }
        Instantiate(item, hatRoot);
    }

    private void GetPant(Material material)
    {
        if (pantSkin.transform.childCount != 0)
        {
            Destroy(pantSkin.transform.GetChild(0).gameObject);
        }
        pantSkin.material = material;
    }

}
