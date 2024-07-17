using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    [Header("Element")]
    [SerializeField] protected Transform shotingPoint;
    [SerializeField] protected Projectile projectilePrefab;
    [SerializeField] protected Animator animator;
    protected string currentAnimName = "idle";

    protected Vector3 moveDirection;
    [HideInInspector] public Transform target;
    protected Collider[] targets = new Collider[10];


    [Header("Setting")]
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float detectRadius;
    [SerializeField] protected LayerMask targetLayerMask;
    public float detectDelay;
    public float attackDelay;

    private void Awake()
    {
        OnInit();
    }

    protected virtual void OnInit()
    {

    }

    public virtual void Move()
    {

    }

    public virtual void DetectTarget()
    {
        int availableTargets = Physics.OverlapSphereNonAlloc(this.transform.position, detectRadius, targets, targetLayerMask);
        if (availableTargets <= 0) return;

        Transform target = null;
        float min = float.MaxValue;
        for (int i = 0; i < availableTargets; i++)
        {
            float distance = Vector3.Distance(transform.position, targets[i].transform.position);
            if (distance < min)
            {
                min = distance;
                target = targets[i].transform;
            }
        }
        this.target = target;
    }

    public virtual void Attack(Transform target)
    {
        ChangAnim("attack");
        Projectile projectTile = LeanPool.Spawn(projectilePrefab, shotingPoint.transform.position, Quaternion.identity);
        LeanPool.Despawn(projectTile, 3);
        projectTile.SetDirection((target.position + Vector3.up - shotingPoint.transform.position).normalized);
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

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Projectile"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
