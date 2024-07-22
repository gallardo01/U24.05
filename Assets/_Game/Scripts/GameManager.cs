using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        SimplePool.Spawn(PoolType.Bot, new Vector3(10, 0, 10), Quaternion.identity);
        SimplePool.Spawn(PoolType.Bot, new Vector3(10, 0, 0), Quaternion.identity);
        SimplePool.Spawn(PoolType.Bot, new Vector3(0, 0, 10), Quaternion.identity);
    }
}
