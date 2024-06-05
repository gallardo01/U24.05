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
    private void Start()
    {
        StartCoroutine(BuffAttack());
    }
    IEnumerator BuffAttack()
    {
        yield return new WaitForSeconds(5f);
        List<GameObject> target = FindTarget();
        GameObject[] characterObject = GameObject.FindGameObjectsWithTag("character");
        if (target != null)
        {
            for (int i = 0; i < characterObject.Length; i++)
            {
                if (target.Contains(characterObject[i]))
                {
                    Character character = characterObject[i].GetComponent<Character>();
                    character.CheckBuffActiveOnCharacter(true);
                }
            }
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
                if (grid[i,j] == gameObject)
                {
                    if (i > 0 && grid[i-1,j] != null)
                    {
                        if (grid[i-1,j].CompareTag("character"))
                        {
                            target.Add(grid[i - 1, j]);
                        }
                    }
                    if (i < 3 && grid[i + 1, j] != null)
                    {
                        if (grid[i + 1, j].CompareTag("character"))
                        {
                            target.Add(grid[i + 1, j]);
                        }
                    }
                    if (j > 0 && grid[i, j - 1] != null)
                    {
                        if (grid[i, j - 1].CompareTag("character"))
                        {
                            target.Add(grid[i, j - 1]);
                        }
                    }
                    if (j < 3 && grid[i, j + 1] != null)
                    {
                        if (grid[i, j + 1].CompareTag("character"))
                        {
                            target.Add(grid[i, j + 1]);
                        }
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
                grid[i, j] = FintCharacterInDict(count);
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


