using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BuffArea : CharacterManager
{
    bool buffActive = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("character"))
        {
            buffActive = true;
            Debug.Log(buffActive);
        }
    }
    public bool CheckBuffActive()
    {
        return buffActive;
    }
}


