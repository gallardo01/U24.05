using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    
    private int m_Damage = 30;
    private Transform target;
    void Start()
    {
        StartCoroutine(AutoDestroy());
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }    

    public void SetDamage(int damage)
    {
        this.m_Damage = damage;
    }    

    void Update()
    {
        if (target != null && target.gameObject.activeInHierarchy) //target.gameObject.activeInHierarchy this for disable not destroy
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, 0.005f);
        }
        else
        {
            Destroy(gameObject);
        } 
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Monster")
        {
            collision.GetComponent<MonsterController>().TakeDamage(m_Damage);
            Destroy(gameObject);
        }
    }

    IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }    
}
