using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BuffCharacter : CharacterManager
{
    GameController controller;
    new public GameObject FindTarget()
    {
        GameObject[] character = GameObject.FindGameObjectsWithTag("character");
        List<GameObject> target = new List<GameObject>();
        GameObject[,] arrayAvalableCharacter = new GameObject[4,4];
        for (int i = 0; i < arrayAvalableCharacter; i++)
        {
            
        }

    }
}


