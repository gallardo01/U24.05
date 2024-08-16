using DG.Tweening;
using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.AI;

public abstract class Character : MonoBehaviour
{
    [Header("Element")]
    [SerializeField] protected Transform shotingPoint;
    protected Projectile projectilePrefab;
    [SerializeField] protected Animator animator;
    [SerializeField] protected Collider collider;
    [SerializeField] protected Health health;
    [SerializeField] protected CharacterEquipment equipment;

    protected string currentAnimName = "idle";
    public Indicator Indicator { get; private set; }
    public void SetIndicator(Indicator indicator) {  this.Indicator = indicator; }
    [field: SerializeField] public Transform IndicatorPoint;


    [HideInInspector] public Transform target;
    protected Collider[] targetsList = new Collider[10];

    [Header("Setting")]
    [SerializeField] protected float moveSpeed;
    [SerializeField] public float detectRadius;
    [SerializeField] protected LayerMask targetLayerMask;
    [SerializeField] protected int characterDamage;
    public float detectDelay;
    public float attackDelay;

    public int Score { get; private set; }  public void SetScore(int score) { this.Score += score; }

    public virtual void OnInit()
    {
        this.enabled = true;
        collider.enabled = true;
        Indicator.gameObject.SetActive(true);
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

    public void Throw(Vector3 targetPos)
    {
        Projectile projectTile = LeanPool.Spawn(projectilePrefab, shotingPoint.transform.position, Quaternion.identity);
        LeanPool.Despawn(projectTile.gameObject, 3);
        Vector3 direction = (targetPos + Vector3.up - shotingPoint.transform.position).normalized;
        projectTile.transform.forward = direction;
        projectTile.Shoot(direction, characterDamage,this);
    }

    public virtual void TakeDamage(int damage, Character sender)
    {
        health.TakeDamage(damage, sender);
    }

    public virtual void OnDeath(Character sender)
    {
        EventManager.OnCharacterDeath?.Invoke(sender,this);
        ChangAnim("dead");
        collider.enabled = false;
        Indicator.gameObject.SetActive(false);
        this.enabled = false;
    }

    public void UpdateLevel() => Indicator.UpdateLevel();

    public void SetEquipMent(GameObject weaponItem, GameObject shieldItem, GameObject HatItem, Material material, GameObject projectile)
    {
        equipment.SetEquipMent(weaponItem, shieldItem, HatItem, material, projectile);
        projectilePrefab = projectile.GetComponent<Projectile>();
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
