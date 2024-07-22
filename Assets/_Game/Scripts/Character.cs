using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Character : GameUnit
{
    [SerializeField] protected Animator anim;
    [SerializeField] protected Transform weaponHoldParent;
    [SerializeField] protected Transform weaponStartPoint;
    [SerializeField] protected SphereCollider sphereCollider;

    protected string currentAnimName;

    protected int level = 0;
    protected float baseAttackRange = 5f;

    public bool isMoving = false;
    public bool isAttacking = false;

    protected Coroutine attackCoroutine;

    public List<Character> targetInRange = new List<Character>();

    protected WeaponType weaponType;

    protected float levelScale => 1.0f + level * 0.05f;
    protected float attackRange => baseAttackRange * levelScale;

    private void Awake()
    {
        this.RegisterListener(EventID.OnCharacterDead, (param) =>
        {
            targetInRange.Remove((Character)param);
        });
    }

    protected virtual void Start()
    {
        float range = baseAttackRange + weaponStartPoint.localPosition.z;
        sphereCollider.gameObject.transform.localScale = new Vector3(range / sphereCollider.radius, range / sphereCollider.radius, range / sphereCollider.radius);
    }

    protected virtual void Update()
    {
        if (!isMoving && !isAttacking && targetInRange.Count > 0)
        {            
            attackCoroutine = StartCoroutine(Attack());
        }
        if (isMoving)
        {
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
            }
            weaponHoldParent.gameObject.SetActive(true);
        }
    }

    public void ChangeAnim(string animName)
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
        Vector3 direction = targetInRange[0].tf.position - tf.position;
        tf.rotation = Quaternion.LookRotation(direction);
        yield return new WaitForSeconds(0.5f);
        weaponHoldParent.gameObject.SetActive(false);
        WeaponManager.Ins.InitWeapon(weaponType, levelScale, this, weaponStartPoint.position, direction, attackRange);
        yield return new WaitForSeconds(1f);
        weaponHoldParent.gameObject.SetActive(true);
        ChangeAnim(Constants.ANIM_IDLE);
        isAttacking = false;
    }

    protected virtual void OnDead()
    {
        this.PostEvent(EventID.OnCharacterDead, this);
    }

    public void OnHitByWeapon()
    {
        OnDead();
    }
}

