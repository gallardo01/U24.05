using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] float moveSpeed;
    [SerializeField] FloatingJoystick joystick;
    [SerializeField] Rigidbody rb;


    protected override void Start()
    {
        base.Start();
        
        weaponType = WeaponType.Axe;
        Instantiate(WeaponManager.Ins.WeaponDataMap[weaponType].weaponHoldPrefab, weaponHoldParent);
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            WeaponManager.Ins.InitWeapon(WeaponType.Axe, levelScale, this, weaponStartPoint.position, tf.forward, attackRange);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            WeaponManager.Ins.InitWeapon(WeaponType.Boomerang, levelScale, this, weaponStartPoint.position, tf.forward, attackRange);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            level++;
            tf.localScale = new Vector3(levelScale, levelScale, levelScale);
        }
    }
    public void FixedUpdate()
    {
        rb.velocity = new Vector3(joystick.Horizontal * moveSpeed, rb.velocity.y, joystick.Vertical * moveSpeed);
        Vector3 direction = new Vector3(joystick.Horizontal, 0, joystick.Vertical);

        if (direction != Vector3.zero)
        {
            isMoving = true;
            isAttacking = false;
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

    protected override void OnDead()
    {
        base.OnDead();
        Debug.Log("player dead");
    }
}
