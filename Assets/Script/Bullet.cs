using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform target;
    float bulletDamage = 20f;
    public float bulletBuff;
    [SerializeField] float moveSpeed = 3f;

    private void Start()
    {
        this.bulletBuff = 1f;
    }
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
    public void BuffBulletDamage(float bulletBuff)
    { 
        this.bulletBuff = bulletBuff;
    }
    public float SetBulletDamage()
    {
        //Debug.Log(this.bulletBuff + " " + bulletDamage);
        return this.bulletBuff*this.bulletDamage;
    }
}
