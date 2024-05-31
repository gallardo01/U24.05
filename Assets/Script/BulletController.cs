using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BulletController : GameUnit
{
    private GameObject target;
    private float speed = 5f;

    public int damage;

    void Update()
    {
        if (!target.activeInHierarchy)
        {
            //Destroy(gameObject);
            SimplePool.Despawn(this);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }

    public void SetTarget(GameObject target) 
    {
        this.target = target;
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
