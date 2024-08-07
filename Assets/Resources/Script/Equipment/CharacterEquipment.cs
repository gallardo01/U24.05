using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEquipment : MonoBehaviour
{
    [SerializeField] Transform rightHandRoot;
    [SerializeField] Transform leftHandRoot;
    [SerializeField] Transform hatRoot;
    [SerializeField] SkinnedMeshRenderer pantSkin;

    public void GetWeapon(GameObject item)
    {
        if(rightHandRoot.childCount != 0)
        {
            Destroy(rightHandRoot.GetChild(0).gameObject);
        }
        Instantiate(item, rightHandRoot);
    }

    public void GetShield(GameObject item)
    {
        if (leftHandRoot.childCount != 0)
        {
            Destroy(leftHandRoot.GetChild(0).gameObject);
        }
        Instantiate(item, leftHandRoot);
    }

    public void GetHat(GameObject item)
    {
        if (hatRoot.childCount != 0)
        {
            Destroy(hatRoot.GetChild(0).gameObject);
        }
        Instantiate(item, hatRoot);
    }

    public void GetPant(Material material)
    {
        if (pantSkin.transform.childCount != 0)
        {
            Destroy(pantSkin.transform.GetChild(0).gameObject);
        }
        pantSkin.material = material;
    }

}
