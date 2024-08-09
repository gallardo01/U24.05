using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Character : GameUnit
{
    [SerializeField] protected Animator anim;
    [SerializeField] protected Transform weaponStartPoint;
    [SerializeField] protected Transform weaponHoldParent;
    [SerializeField] protected Transform hairParent;
    [SerializeField] protected Transform shieldParent;
    [SerializeField] protected SkinnedMeshRenderer pants;
    [SerializeField] protected SphereCollider attackZoneCollider;

    public CharacterInfo characterInfo;

    protected GameObject currentWeaponHold;
    protected GameObject currentHair;
    protected GameObject currentShield;

    protected string currentAnimName;

    protected int level = 0;
    protected float baseMoveSpeed = 5f;
    protected float baseAttackRange = 5f;
    protected float baseAttackSpeed = 100f;
    public float bonusMoveSpeed = 0;
    public float bonusAttackRange = 0;
    public float bonusAttackSpeed = 0;

    public bool isAttacking = false;
    public bool isMoving = false;
    public bool isDead = false;
    protected bool isListTargetChanged = false;
    protected bool isWeaponHoldActive = true;

    protected Coroutine attackCoroutine;

    protected List<Character> listTarget = new();
    protected Character targetedCharacter;
    public Character TargetedCharacter => targetedCharacter;
    public GameObject targetedImage;


    protected WeaponType weaponType;

    public float LevelScale => 1.0f + level * 0.1f;
    protected float AttackRange => (baseAttackRange + bonusAttackRange) * LevelScale;
    protected float MoveSpeed => (baseMoveSpeed + bonusMoveSpeed) * LevelScale;
    protected float AttackSpeed => baseAttackSpeed + bonusAttackSpeed;

    protected void Awake()
    {
        this.RegisterListener(EventID.OnCharacterDead, (param) =>
        {
            RemoveTarget((Character)param);
        });

        this.RegisterListener(EventID.OnGameStateChanged, (param) =>
        {
            bool isActive = (GameState)param == GameState.Gameplay;
            SetActiveCharacterInfo(isActive);
        });
    }

    protected virtual void Update()
    {
        if (isDead)
        {
            return;
        }

        if (isListTargetChanged)
        {
            SetTarget();
        }

        if (isMoving)
        {
            CancelAttack();

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
        float scale = range / attackZoneCollider.radius;
        attackZoneCollider.gameObject.transform.localScale = new Vector3(scale, scale, scale);

        LevelUp(level);
        EquipWeapon(weaponType);

        if (GameManager.Ins.IsState(GameState.Gameplay))
        {
            characterInfo.SetActiveCharacterInfo(true);
        }
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
        targetedCharacter = listTarget.Count > 0 ? listTarget[0] : null;
        isListTargetChanged = false;
    }

    public void AddTarget(Character character)
    {
        listTarget.Add(character);
        isListTargetChanged = true;
    }

    public virtual void RemoveTarget(Character character)
    {
        listTarget.Remove(character);
        isListTargetChanged = true;
    }

    public void RemoveAllTarget()
    {
        while (listTarget.Count > 0)
        {
            RemoveTarget(listTarget[0]);
        }
        targetedCharacter = null;
        isListTargetChanged = false;
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

    protected void CancelAttack()
    {
        if (attackCoroutine != null)
        {
            isAttacking = false;
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }

    public virtual void StopMove()
    {
        isMoving = false;
    }

    public void ResetCharacter()
    {
        level = 0;
        isDead = false;
    }

    public void OnDead()
    {
        isDead = true;
        StopMove();
        CancelAttack();
        RemoveAllTarget();

        characterInfo.SetActiveCharacterInfo(false);

        this.PostEvent(EventID.OnCharacterDead, this);
        StartCoroutine(IEDead());
    }

    protected virtual IEnumerator IEDead()
    {
        ChangeAnim(Constants.ANIM_DEAD);

        yield return new WaitForSeconds(2f);
    }

    protected virtual void SetActiveCharacterInfo(bool isActive)
    {
        characterInfo.SetActiveCharacterInfo(isActive);
    }

    public void EquipHair(HairType hairType)
    {
        Destroy(currentHair);

        HairDataDetail hairData = SkinManager.Ins.GetHairData(hairType);
        currentHair = Instantiate(hairData.prefab, hairParent);
    }

    public void EquipShield(ShieldType shieldType)
    {
        Destroy(currentShield);

        ShieldDataDetail shieldData = SkinManager.Ins.GetShieldData(shieldType);
        currentShield = Instantiate(shieldData.prefab, shieldParent);
    }

    public void EquipPants(PantsType pantsType)
    {
        if (pantsType == PantsType.None)
        {
            pants.gameObject.SetActive(false);
        }
        else
        {
            pants.gameObject.SetActive(true);
            PantsDataDetail pantsData = SkinManager.Ins.GetPantsData(pantsType);
            pants.material = pantsData.material;
        }
    }

    public void EquipWeapon(WeaponType weaponType)
    {
        Destroy(currentWeaponHold);

        this.weaponType = weaponType;
        currentWeaponHold = WeaponManager.Ins.InitWeaponHold(this.weaponType, weaponHoldParent);
    }
}

