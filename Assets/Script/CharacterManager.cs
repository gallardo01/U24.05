using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterManager : MonoBehaviour
{
    public Bullet bulletPrefabs;
    public IEnumerator AttackMonster()
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
    public abstract GameObject FindTarget();
}
