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
    //IEnumerator AttackMonster()
    //{
    //    yield return new WaitForSeconds(3f);
    //    GameObject target = FindTarget();
    //    if (target != null)
    //    {
    //        GameObject bullet = Instantiate(bulletPrefabs, transform.position, Quaternion.identity);
    //        Vector2 direction = target.transform.position - transform.position;
    //        direction = direction.normalized;
    //        bullet.GetComponent<Rigidbody2D>().AddForce(direction*300f);
    //    }
    //    StartCoroutine(AttackMonster());
    //}

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
}
