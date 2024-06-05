using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffHeroesController : MonoBehaviour
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
            bullet.GetComponent<Bullet>().SetTarget(target.transform);
            /*Vector2 direction = target.transform.position - transform.position;
            Debug.Log(direction);
            direction.Normalize(); // Normalize chi lay huong, bien thanh vector don vi
            bullet.GetComponent<Rigidbody2D>().AddForce(direction* 0.01f);*/
        }
        StartCoroutine(AttackMonster());
    }

    private GameObject FindTarget()
    {
        GameObject[] listObj = GameObject.FindGameObjectsWithTag("Monster");
        List<GameObject> list = new List<GameObject>();
        for (int i = 0;i < listObj.Length; i++)
        {
            if (listObj[i].activeInHierarchy == true)
            {
                list.Add(listObj[i]);
            }
        }
        if (list.Count > 0)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }
        else { 
            return null; 
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Hero")) 
        {
            //Increase Shoot Speed
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
