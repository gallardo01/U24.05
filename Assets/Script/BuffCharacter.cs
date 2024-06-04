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
            Debug.Log(target.Count);
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
                    Debug.Log(i + "-" + j);
                    //if (i > 0 && grid[i-1,j] == gameObject)
                    //{
                    //    Debug.Log("thêm thằng bên dưới" + i + j);
                    //    target.Add(grid[i, j]);
                    //}
                    //if (i < 3 && grid[i+1,j] == gameObject)
                    //{
                    //    Debug.Log("thêm thằng bên trên" + i + j);
                    //    target.Add(grid[i, j]);
                    //}
                    //if (j > 0 && grid[i,j-1] == gameObject)
                    //{
                    //    Debug.Log("thêm thằng bên phải" + i + j);
                    //    target.Add(grid[i, j]);
                    //}
                    //if (j < 3 && grid[i, j + 1] == gameObject)
                    //{
                    //    Debug.Log("thêm thằng bên trái" + i + j);
                    //    target.Add(grid[i, j]);
                    //}
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
                Debug.Log(character.Key + ": " + character.Value);
                return character.Value;
            }
        }
        return null;
    }
}


