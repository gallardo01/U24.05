using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : Character
{
    
    [SerializeField] FloatingJoystick joystick;
    [SerializeField] Rigidbody rb;

    public int rank;
    public string killedBy;

    public int Level => level;

    private void Start()
    {
        HairType hairType = DataManager.Ins.GetCurrentItem<HairType>();
        ShieldType shieldType = DataManager.Ins.GetCurrentItem<ShieldType>();
        PantsType pantsType = DataManager.Ins.GetCurrentItem<PantsType>();
        EquipHair(hairType);
        EquipShield(shieldType);
        EquipPants(pantsType);
    }

    public void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }

        rb.velocity = new Vector3(joystick.Horizontal * MoveSpeed, rb.velocity.y, joystick.Vertical * MoveSpeed);
        Vector3 direction = new(joystick.Horizontal, 0, joystick.Vertical);

        if (direction != Vector3.zero)
        {
            isMoving = true;
            tf.rotation = Quaternion.LookRotation(direction);
            ChangeAnim(Constants.ANIM_RUN);
        }
        else
        {
            isMoving = false;
            if (!isAttacking)
            {
                ChangeAnim(Constants.ANIM_IDLE);
            }
        }
    }

    public override void SetTarget()
    {
        base.SetTarget();
        if (targetedCharacter != null)
        {
            targetedCharacter.targetedImage.SetActive(true);
        }
    }

    public override void RemoveTarget(Character character)
    {
        base.RemoveTarget(character);
        if (character == targetedCharacter)
        {
            character.targetedImage.SetActive(false);
        }
    }

    protected override IEnumerator IEDead()
    {
        yield return StartCoroutine(base.IEDead());
        LevelManager.Ins.Finish();
    }

    protected override void SetActiveCharacterInfo(bool isActive)
    {
        base.SetActiveCharacterInfo(isActive);
        attackZoneCollider.gameObject.SetActive(isActive);
    }

    public void Revive()
    {
        isDead = false;
    }
}
