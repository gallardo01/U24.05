using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Character : ManagerChar
{
    //public Bullet bulletPrefabs;
    //float time;
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

    //private void Update()
    //{
    //    time += Time.deltaTime;
    //    if (time >= 3f)
    //    {
    //        GameObject target = FindTarget();
    //        if (target != null)
    //        {
    //            Bullet bullet = Instantiate(bulletPrefabs, transform.position, Quaternion.identity);
    //            bullet.SetTargetFire(target.transform);
    //        }
    //        time = 0f;
    //    }
    //}
    public override GameObject FindTarget()
    {
        GameObject[] monsterArray = GameObject.FindGameObjectsWithTag("monster");
        List<GameObject> monsterList = new List<GameObject>();
        for (int i = 0; i < monsterArray.Length; i++)
        {
            if (monsterArray[i].activeInHierarchy == true)
            {
                monsterList.Add(monsterArray[i]);
            }
        }
        if (monsterList.Count>0)
        {
            return monsterList[Random.Range(0, monsterList.Count)];
        }
        else
        {
            return null;
        }
    }
}
