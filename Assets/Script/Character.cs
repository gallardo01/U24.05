using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Character : CharacterManager
{
    public Bullet bulletPrefabs;

    private void Start()
    {
        StartCoroutine(AttackMonster());
    }

    IEnumerator AttackMonster()
    {
        yield return new WaitForSeconds(3f);
        GameObject target = FindTarget();
        if (target != null)
        {
            Bullet bullet = Instantiate(bulletPrefabs, transform.position, Quaternion.identity);
            bullet.SetTargetFire(target.transform);
        }
        StartCoroutine(AttackMonster());
    }

    public void CheckBuffActiveOnCharacter(bool check)
    {
        if (check)
        {
            bulletPrefabs.BuffBulletDamage(2f);
        }
        else
        {
            bulletPrefabs.BuffBulletDamage(1f);
        }
    }
}
