using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Projectile : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] int weapondamage;
    [SerializeField] bool needRotate;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody rb;

    private Character sender;
    private Vector3 direction;

    public void Shoot(Vector3 direction, int weapondamage, Character sender)
    {
        this.direction = direction;
        this.weapondamage = weapondamage;
        this.sender = sender;
        rb.velocity = this.direction * moveSpeed;
        if (needRotate) animator.Play("Rotate");
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.layer)
        {
            case 7:
                other.GetComponent<Character>().TakeDamage(weapondamage, sender);
                LeanPool.Despawn(this.gameObject);
                break;
            case 8:
                other.GetComponent<Character>().TakeDamage(weapondamage, sender);
                LeanPool.Despawn(this.gameObject);
                break;
            case 9:
                SoundManager.WeaponImpact();
                break;
        }

        VFXPool.Spawn(this.transform.position, Quaternion.identity, null);
        LeanPool.Despawn(this.gameObject);
    }
}
