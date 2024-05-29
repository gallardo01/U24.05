using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttackController : HeroBase
{
    public GameObject bulletPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AttackMonster());
    }

    IEnumerator AttackMonster()
    {
        yield return new WaitForSeconds(1f);
        GameObject target = FindTarget();
        if (target != null)
        {
            GameObject bullet = Instantiate(bulletPrefabs, transform.position, Quaternion.identity);
            bullet.GetComponent<BulletController>().SetTarget(target);

            //Vector2 direction = target.transform.position - transform.position;
            //direction = direction.normalized;
            //bullet.GetComponent<Rigidbody2D>().AddForce(direction * 800f);

            //StartCoroutine(DestroyBulletAfterTime(bullet, 2f));
        }
        StartCoroutine(AttackMonster());
    }

    private GameObject FindTarget()
    {
        if (Controller.Ins.monsterAlive.Count > 0)
        {
            return Controller.Ins.monsterAlive[Random.Range(0, Controller.Ins.monsterAlive.Count)];
        }
        else
        {
            return null;
        }
    }

    //IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    if (bullet != null)
    //    {
    //        Destroy(bullet);
    //    }
    //}

    // Update is called once per frame
    void Update()
    {

    }
}