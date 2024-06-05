using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform target;
    float bulletDamage = 20f;
    float bulletBuff;
    [SerializeField] float moveSpeed = 3f;

    private void Update()
    {
        if (target != null && target.gameObject.activeInHierarchy)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        } else
        {
            Destroy(gameObject);
        }
        
    }
    public void SetTargetFire(Transform target)
    {
       this.target = target;
    }
    public float BuffBulletDamage(bool check)
    {
        if (check)
        {
            bulletBuff = 2;
        } else
        {
            bulletBuff = 1;
        }
        return this.bulletBuff;
    }
    public float SetBulletDamage()
    {
        Debug.Log(this.bulletBuff + " " + bulletDamage);
        return this.bulletBuff*this.bulletDamage;
    }
}
