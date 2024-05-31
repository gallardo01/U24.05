using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public GameObject FindTarget()
    {
        GameObject[] monsterArray = GameObject.FindGameObjectsWithTag("monster");
        List<GameObject> monsterList = new List<GameObject>();
        for (int i = 0; i < monsterArray.Length; i++)
        {
            if (monsterArray[i].activeInHierarchy == true)
            {
                monsterList.Add(monsterArray[i]);
            }
        }
        if (monsterList.Count > 0)
        {
            return monsterList[Random.Range(0, monsterList.Count)];
        }
        else
        {
            return null;
        }
    }
}
