using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 50;
    private int bulletDamage;
    private Transform target;

    private void Start()
    {
        Invoke(nameof(AutoDestroy), 1f);
    }

    void Update()
    {
        if (target == null) return;
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * bulletSpeed);
    }

    public void SetDirection(Transform direction)
    {
        this.target = direction;
    }

    void AutoDestroy()
    {
        Destroy(gameObject);
    }

    public int GetBulletDamage()
    {
        return bulletDamage;
    }

    public void SetBulletDamage(int damage)
    {
        this.bulletDamage = damage;
    }
}
