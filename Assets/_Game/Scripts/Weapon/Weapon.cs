using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] WeaponType weaponType;
    [SerializeField] PoolType poolType;

    public void InitWeapon(Vector3 startPoint, Vector3 moveDirection, float attackRange, int level)
    {
        // SetPath() Virtual
        // move -> last Path -> despawn
    }
}
