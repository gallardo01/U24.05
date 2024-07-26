using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] int weapondamage;
    [SerializeField] bool needRotate;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody rb;

    private Character whoProjectile;
    private Vector3 direction;
    private float rotationY;

    public void Shoot(Vector3 direction, int weapondamage, Character whoProjectile)
    {
        this.direction = direction;
        this.weapondamage = weapondamage;
        this.whoProjectile = whoProjectile;
        rb.velocity = this.direction * moveSpeed;
        if (needRotate) animator.Play("Rotate");
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.layer)
        {
            case 7:
                other.GetComponent<Character>().TakeDamage(weapondamage, whoProjectile);
                LeanPool.Despawn(this.gameObject);
                break;
            case 8:
                other.GetComponent<Character>().TakeDamage(weapondamage, whoProjectile);
                LeanPool.Despawn(this.gameObject);
                break;
        }
    }
}
