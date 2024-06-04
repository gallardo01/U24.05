using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.GraphicsBuffer;
using Debug = UnityEngine.Debug;

public class BuffCharacter: CharacterManager
{
    public GameObject[,] grid = new GameObject[4, 4];
    public GameObject characterPrefabs;
    private void Start()
    {
        StartCoroutine(BuffAttack());
    }
    IEnumerator BuffAttack()
    {
        yield return new WaitForSeconds(3f);
        List<GameObject> target = FindTarget();
        if (target != null)
        {
        }
        StartCoroutine(BuffAttack());
    }

    new public List<GameObject> FindTarget()
    {
        List<GameObject> target = new List<GameObject>();
        AddCharacterToArray();
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                Debug.Log("gà");
                if (grid[i,j].CompareTag("character"))
                {
                    Debug.Log("vịt");
                    if (grid[i-1,j] == gameObject || grid[i + 1, j] == gameObject || grid[i, j-1] == gameObject || grid[i, j+1] == gameObject)
                    {
                        target.Add(grid[i, j]);
                    }
                }
            }
        }
        return target;
    }
    public void AddCharacterToArray()
    {
        int count = 0;
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if(FintCharacterInDict(count))
                {
                    grid[i, j] = FintCharacterInDict(count);
                }
                count++;
            }
        }
    }

    private GameObject FintCharacterInDict(int key)
    {
        foreach (var character in Controller.Instance.characterDict)
        {
            if(key == character.Key)
            {
                return character.Value;
            }
        }
        return null;
    }
}


