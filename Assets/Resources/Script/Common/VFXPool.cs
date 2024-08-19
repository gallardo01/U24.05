using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VFXPool
{
    private static GameObject[] prefabs;

    public static void Spawn(Vector3 positions, Quaternion quaternion ,Transform parent)
    {
        if (prefabs == null)
        {
            prefabs = Resources.LoadAll<GameObject>("VFX/HitEffect");
        }

        GameObject randomHitVFX = prefabs[Random.Range(0, prefabs.Length)];
        GameObject spawnObject = LeanPool.Spawn(randomHitVFX, positions, quaternion, parent);
        LeanPool.Despawn(spawnObject, 1.5f);
    }
}
