using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject bulletPrefabs;
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
            bullet.GetComponent<Rigidbody2D>().AddForce(direction*100f);
        }
        StartCoroutine(AttackMonster());
    }

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

}
