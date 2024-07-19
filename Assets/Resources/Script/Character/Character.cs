using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    [Header("Element")]
    [SerializeField] protected Transform shotingPoint;
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected Animator animator;
    [SerializeField] protected Collider collider;
    [SerializeField] protected Health health;
    protected string currentAnimName = "idle";

    [HideInInspector] public Transform target;
    protected Collider[] targetsList = new Collider[10];


    [Header("Setting")]
    [SerializeField] protected float moveSpeed;
    [SerializeField] public float detectRadius;
    [SerializeField] protected LayerMask targetLayerMask;
    [SerializeField] protected int characterDamage;
    public float detectDelay;
    public float attackDelay;

    protected void Awake()
    {
        OnInit();
    }

    protected virtual void OnInit()
    {
        collider.enabled = true;
    }

    protected void Start()
    {
        InvokeRepeating(nameof(DetectTarget), 0f, detectDelay);
    }

    protected virtual void DetectTarget()
    {
        int availableTargets = Physics.OverlapSphereNonAlloc(this.transform.position, detectRadius, targetsList, targetLayerMask);

        if (availableTargets <= 0)
        {
            this.target = null;
            return;
        }

        Transform target = null;
        float min = float.MaxValue;
        for (int i = 0; i < availableTargets; i++)
        {
            if (targetsList[i] == this.collider) continue;
            float distance = Vector3.Distance(transform.position, targetsList[i].transform.position);
            if (distance < min)
            {
                min = distance;
                target = targetsList[i].transform;
            }
        }
        this.target = target;
    }

    public virtual void Attack(Transform target)
    {
        ChangAnim("attack");
        transform.forward = (target.position - transform.position).normalized;
        Projectile projectTile = LeanPool.Spawn(projectilePrefab, shotingPoint.transform.position, Quaternion.identity).GetComponent<Projectile>();
        LeanPool.Despawn(projectTile.gameObject, 3);
        projectTile.Shoot((target.position + Vector3.up - shotingPoint.transform.position).normalized, characterDamage);                      
    }

    public virtual void TakeDamage(int damage)
    {
        health.TakeDamage(damage);
    }

    public virtual void OnDeath()
    {
        ChangAnim("dead");
        collider.enabled = false;
    }

    public void ChangAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            animator.ResetTrigger(animName);
            currentAnimName = animName;
            animator.SetTrigger(currentAnimName);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
