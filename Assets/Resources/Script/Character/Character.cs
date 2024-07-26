using DG.Tweening;
using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public abstract class Character : MonoBehaviour
{
    [Header("Element")]
    [SerializeField] protected Transform shotingPoint;
    [SerializeField] protected Projectile projectilePrefab;
    [SerializeField] protected Animator animator;
    [SerializeField] protected Collider collider;
    [SerializeField] protected Health health;
    protected string currentAnimName = "idle";
    protected Indicator indicator; public void SetIndicator(Indicator indicator) {  this.indicator = indicator; } 

    [HideInInspector] public Transform target;
    protected Collider[] targetsList = new Collider[10];


    [Header("Setting")]
    [SerializeField] protected float moveSpeed;
    [SerializeField] public float detectRadius;
    [SerializeField] protected LayerMask targetLayerMask;
    [SerializeField] protected int characterDamage;
    public float detectDelay;
    public float attackDelay;

    public virtual void OnInit()
    {
        this.enabled = true;
        collider.enabled = true;
        indicator.gameObject.SetActive(true);
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
        transform.forward = (target.position - transform.position).normalized;               
        ChangAnim("attack");
    }

    public void Throw(Transform target)
    {
        Projectile projectTile = LeanPool.Spawn(projectilePrefab, shotingPoint.transform.position, Quaternion.identity);
        LeanPool.Despawn(projectTile.gameObject, 3);
        Vector3 direction = (target.position + Vector3.up - shotingPoint.transform.position).normalized;
        projectTile.transform.forward = direction;
        projectTile.Shoot(direction, characterDamage,this);
    }

    public virtual void TakeDamage(int damage, Character whoBullet)
    {
        health.TakeDamage(damage, whoBullet);
    }

    public virtual void OnDeath(Character killerCharacter)
    {
        EventManager.OnCharacterDeath?.Invoke(killerCharacter);
        ChangAnim("dead");
        collider.enabled = false;
        indicator.gameObject.SetActive(false);
        this.enabled = false;
    }

    public virtual void GainLevel(Character character)
    {
        if(character != this) return;
        transform.DOScale(transform.localScale.x + 0.2f, 1f);
        this.indicator.UpdateLevel();
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

    private void OnEnable()
    {
        EventManager.OnCharacterDeath += GainLevel;
    }

    private void OnDisable()
    {
        EventManager.OnCharacterDeath -= GainLevel;

    }
}
