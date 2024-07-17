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
    protected NavMeshAgent agent;

    protected Vector3 moveDirection;
    [HideInInspector] public Transform target;
    protected Collider[] enemies = new Collider[10];


    [Header("Setting")]
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float detectRadius;
    [SerializeField] protected LayerMask targetLayerMask;
    public float detectDelay;
    public float attackDelay;
    protected float timer;

    protected IState<Character> currentState;

    private void OnEnable()
    {
        
        OnInit();
    }

    protected void OnInit()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = new IdleState();
    }

    private void Update()
    {
        currentState.OnExecute(this);
    }

    public virtual void Move()
    {

    }

    public virtual void DetectTarget()
    {
        int availableEnemies = Physics.OverlapSphereNonAlloc(this.transform.position, detectRadius, enemies, targetLayerMask);
        if (availableEnemies < 0) return;

        Transform target = null;
        float min = float.MaxValue;
        for (int i = 0; i < availableEnemies; i++)
        {
            float distance = Vector3.Distance(transform.position, enemies[i].transform.position);
            if (distance < min)
            {
                min = distance;
                target = enemies[i].transform;
            }
        }

        this.target = target;
    }

    public virtual void Attack(Transform target)
    {
        ChangAnim("attack");
        Projectile projectTile = LeanPool.Spawn(projectilePrefab, shotingPoint.transform.position, Quaternion.identity);
        LeanPool.Despawn(projectTile, 3);
        projectTile.SetDirection(target.position + Vector3.up);
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

    public void ChangeState(IState<Character> newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
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
