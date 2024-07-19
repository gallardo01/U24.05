using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] float moveSpeed;
    [SerializeField] FloatingJoystick joystick;
    [SerializeField] Rigidbody rb;


    private void Start()
    {
        sphereCollider.radius = baseAttackRange + weaponStartPoint.localPosition.z;

        weaponType = WeaponType.Axe;
        Instantiate(WeaponManager.Ins.WeaponDataMap[weaponType].weaponHoldPrefab, weaponHoldParent);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            WeaponManager.Ins.InitWeapon(WeaponType.Axe, levelScale, weaponStartPoint.position, tf.forward, attackRange);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            WeaponManager.Ins.InitWeapon(WeaponType.Boomerang, levelScale, weaponStartPoint.position, tf.forward, attackRange);
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

        if (direction.magnitude > 0.1f)
        {
            tf.rotation = Quaternion.LookRotation(direction);
            ChangeAnim(Constants.ANIM_RUN);
        }
        else
        {
            ChangeAnim(Constants.ANIM_IDLE);
        }
    }

    

    private void Attack()
    {

    }

    private void Dead()
    {

    }


}
