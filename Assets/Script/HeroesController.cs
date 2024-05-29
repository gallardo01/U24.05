using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroesController : MonoBehaviour
{

    public GameObject bulletPrefabs;
    // Start is called before the first frame update
    void Start()
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
            bullet.GetComponent<Bullet>().SetTarget(target.transform);
            //Vector2 direction = target.transform.position - transform.position;
            //direction = direction.normalized;
            //bullet.GetComponent<Rigidbody2D>().AddForce(direction * 800f);
        }
        StartCoroutine(AttackMonster());
    }

    private GameObject FindTarget()
    {
        GameObject[] listObj = GameObject.FindGameObjectsWithTag("Monster");
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < listObj.Length; i++)
        {
            if (listObj[i].activeInHierarchy == true)
            {
                list.Add(listObj[i]);
            }
        }
        if (list.Count > 0)
        {
            return list[Random.Range(0, list.Count)];
        } else
        {
            return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
