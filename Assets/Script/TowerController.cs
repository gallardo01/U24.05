using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float m_BulletSpeed = 100f;

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating(nameof(Shooting), 1f, 1f);
        StartCoroutine(Attack());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject FindTarget()
    {
        GameObject[] listObj = GameObject.FindGameObjectsWithTag("Monster");
        List<GameObject> list = new List<GameObject>();
        for(int i = 0; i < listObj.Length; i++)
        {
            if(listObj[i].activeInHierarchy == true)
            {
                list.Add(listObj[i]);
            }    
        }
        if(list.Count > 0)
        {
            return list[Random.Range(0, list.Count)];
        }
        else
        {
            return null;
        }

    }

    public void Shooting()
    {
        Instantiate(bulletPrefab.transform, gameObject.transform.position, Quaternion.identity);
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(3f);
        GameObject target = FindTarget();
        if(target != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, gameObject.transform.position, Quaternion.identity);
            Vector2 direction = target.transform.position - transform.position;
            //direction = direction.normalized; //bien ve vector don vi de khong anh huong luc
            //Quaternion rotation = Quaternion.LookRotation(direction);
            //bullet.transform.rotation = rotation;

            bullet.GetComponent<Rigidbody2D>().AddForce(direction * m_BulletSpeed);
            Debug.Log($"speed: {direction * m_BulletSpeed}");
        }
        StartCoroutine(Attack());
    }
}
