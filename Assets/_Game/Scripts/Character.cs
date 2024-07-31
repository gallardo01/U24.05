using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Character : GameUnit
{
    [SerializeField] protected Animator anim;
    [SerializeField] protected Transform weaponHoldParent;
    [SerializeField] protected Transform weaponStartPoint;
    [SerializeField] protected SphereCollider sphereCollider;
    public CharacterInfo characterInfo;

    protected GameObject weaponHold;
    protected string currentAnimName;

    protected int level = 0;
    protected float baseMoveSpeed = 5f;
    protected float baseAttackRange = 5f;
    protected float baseAttackSpeed = 100f;
    public float bonusMoveSpeed = 0;
    public float bonusAttackRange = 0;
    public float bonusAttackSpeed = 0;

    public bool isMoving = false;
    public bool isAttacking = false;
    protected bool targetInRangeChanged = false;
    protected bool isWeaponHoldActive = true;

    protected Coroutine attackCoroutine;

    public List<Character> targetInRange = new List<Character>();
    public Character targetedCharacter;
    public GameObject targetedImage;

    public Character TargetedCharacter => targetedCharacter;

    protected WeaponType weaponType;

    public float LevelScale => 1.0f + level * 0.1f;
    protected float AttackRange => (baseAttackRange + bonusAttackRange) * LevelScale;
    protected float MoveSpeed => (baseMoveSpeed + bonusMoveSpeed) * LevelScale;
    protected float AttackSpeed => baseAttackSpeed + bonusAttackSpeed;

    protected virtual void Awake()
    {
        this.RegisterListener(EventID.OnCharacterDead, (param) =>
        {
            RemoveTarget((Character)param);
        });
    }

    protected virtual void Update()
    {
        if (targetInRangeChanged)
        {
            SetTarget();
        }

        if (isMoving)
        {
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }

            if (!isWeaponHoldActive)
            {
                weaponHoldParent.gameObject.SetActive(true);
                isWeaponHoldActive = true;
            }
        }
        else if (!isAttacking && targetedCharacter != null)
        {
            attackCoroutine = StartCoroutine(Attack());
        }
    }

    public virtual void InitCharacter(WeaponType weaponType, int level)
    {
        float range = AttackRange + weaponStartPoint.localPosition.z;
        float scale = range / sphereCollider.radius;
        sphereCollider.gameObject.transform.localScale = new Vector3(scale, scale, scale);

        LevelUp(level);

        this.weaponType = weaponType;
        weaponHold = Instantiate(WeaponManager.Ins.WeaponDataMap[this.weaponType].weaponHoldPrefab, weaponHoldParent);

        if(GameManager.Ins.IsState(GameState.Gameplay))
        {
            characterInfo.SetActiveCharacterInfo(true);
        }
    }

    public virtual void ResetCharacter()
    {
        level = 0;
        currentAnimName = null;
        Destroy(weaponHold);
        isAttacking = false;
        isMoving = false;
        targetInRange.Clear();
        characterInfo.SetActiveCharacterInfo(false);
    }

    public void LevelUp(int level)
    {
        this.level += level;
        characterInfo.UpdateTextLevel(this.level);
        tf.localScale = new Vector3(LevelScale, LevelScale, LevelScale);
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

    public virtual void SetTarget()
    {
        targetedCharacter = targetInRange.Count > 0 ? targetInRange[0] : null;
        targetInRangeChanged = false;
    }

    public void AddTarget(Character character)
    {
        targetInRange.Add(character);
        targetInRangeChanged = true;
    }

    public virtual void RemoveTarget(Character character)
    {
        targetInRange.Remove(character);
        targetInRangeChanged = true;
    }

    protected IEnumerator Attack()
    {       
        isAttacking = true;
        ChangeAnim(Constants.ANIM_ATTACK);

        Vector3 direction = targetedCharacter.tf.position - tf.position;
        direction.y = 0;
        tf.rotation = Quaternion.LookRotation(direction);

        yield return new WaitForSeconds(0.5f);
        weaponHoldParent.gameObject.SetActive(false);
        isWeaponHoldActive = false;

        WeaponManager.Ins.InitWeapon(weaponType, LevelScale, this, AttackSpeed, weaponStartPoint.position, direction, AttackRange);
        yield return new WaitForSeconds(1f);
        weaponHoldParent.gameObject.SetActive(true);
        isWeaponHoldActive = true;

        isAttacking = false;
        ChangeAnim(Constants.ANIM_IDLE);
    }

    public virtual void OnDead()
    {
        this.PostEvent(EventID.OnCharacterDead, this);
    }
}

