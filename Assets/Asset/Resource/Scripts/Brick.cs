using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Brick : MonoBehaviour
{
    //ObjectPool<Brick> pool;

    //private void Awake()
    //{
    //    pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, false, 10, 20);
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(SpawnBrick.Instance.RespawnBrick(transform.position, 2f));
            transform.position = new Vector3(100f, 100f, 100f);
        }
    }
}
