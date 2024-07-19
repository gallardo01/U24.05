using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Character : MonoBehaviour
{
    [SerializeField] protected Animator anim;
    [SerializeField] protected Transform tf;
    [SerializeField] protected Transform weaponHoldParent;
    [SerializeField] protected Transform weaponStartPoint;
    [SerializeField] protected SphereCollider sphereCollider;

    protected string currentAnimName;

    protected int level = 0;
    protected float baseAttackRange = 5f;

    protected bool isMoving = false;
    protected bool isAttacking = false;

    protected Coroutine attackCoroutine;

    protected List<Character> targetInRange = new List<Character>();

    protected WeaponType weaponType;

    protected float levelScale => 1.0f + level * 0.05f;
    protected float attackRange => baseAttackRange * levelScale;

    protected virtual void Start()
    {
        float range = baseAttackRange + weaponStartPoint.localPosition.z;
        sphereCollider.gameObject.transform.localScale = new Vector3(range / sphereCollider.radius, range / sphereCollider.radius, range / sphereCollider.radius);
    }

    protected virtual void Update()
    {
        if (!isMoving && !isAttacking)
        {            
            attackCoroutine = StartCoroutine(Attack());
        }
        if (isMoving)
        {
            StopCoroutine(attackCoroutine);
        }
    }

    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    public void AddTarget(Character character)
    {
        targetInRange.Add(character);
    }

    public void RemoveTarget(Character character)
    {
        targetInRange.Remove(character);
    }

    protected IEnumerator Attack()
    {
        isAttacking = true;
        ChangeAnim(Constants.ANIM_ATTACK);
        yield return new WaitForSeconds(0.5f);
        WeaponManager.Ins.InitWeapon(WeaponType.Axe, levelScale, weaponStartPoint.position, tf.forward, attackRange);
        yield return new WaitForSeconds(2f);
        isAttacking = false;
    }
}

