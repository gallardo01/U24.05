using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Character : MonoBehaviour
{
    public GameObject bulletPrefabs;
    float time;
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
            GameObject bullet = Instantiate(bulletPrefabs, transform.position, Quaternion.identity);
            Vector2 direction = target.transform.position - transform.position;
            direction = direction.normalized;
            bullet.GetComponent<Rigidbody2D>().AddForce(direction*300f);
        }
        StartCoroutine(AttackMonster());
    }

    //private void Update()
    //{

    //    time += Time.deltaTime;
    //    if (time >=3f)
    //    {
    //        GameObject target = FindTarget();
    //        if (target != null)
    //        {
    //            GameObject bullet = Instantiate(bulletPrefabs, transform.position, Quaternion.identity);
    //        }
    //        time = 0f;
    //    } 
    //}
    GameObject FindTarget()
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
    //private void AttackMonster()
    //{
    //    GameObject target = FindTarget();
    //    if (target != null)
    //    {
    //        GameObject bullet = Instantiate(bulletPrefabs, transform.position, Quaternion.identity);
    //        bullet.transform.position = Vector2.MoveTowards(transform.position, target.transform.position, 0.1f);
    //    }
    //}
}
