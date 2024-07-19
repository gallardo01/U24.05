using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] int weapondamage;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody rb;

    private Vector3 direction;
    private float rotationY;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Shoot(Vector3 direction, int weapondamage)
    {
        this.direction = direction;
        this.weapondamage = weapondamage;
        rb.velocity = this.direction * moveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.layer)
        {
            case 7:
                other.GetComponent<Character>().TakeDamage(weapondamage);
                LeanPool.Despawn(this.gameObject);
                break;
            case 8:
                other.GetComponent<Character>().TakeDamage(weapondamage);
                LeanPool.Despawn(this.gameObject);
                break;
            //case 0:
            //    LeanPool.Despawn(this.gameObject);
            //    break;
        }
    }
}
