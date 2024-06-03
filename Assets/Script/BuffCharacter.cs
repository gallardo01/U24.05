using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BuffCharacter: CharacterManager
{
    public Controller controller;
    public GameObject[,] grid = new GameObject[4, 4]; 
    new public GameObject[] FindTarget()
    {
        int count = 0;
        GameObject[] target = new GameObject[4];
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if(FintCharacterinDict(count))
                {
                    grid[i, j] = FintCharacterinDict(count);
                }
                count++;
            }
        }
        return target;
    }

    private GameObject FintCharacterinDict(int key)
    {
        foreach (var character in controller.characterDict)
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


