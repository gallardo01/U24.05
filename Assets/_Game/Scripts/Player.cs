using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] protected Animator anim;
    protected string currentAnimName;

    [SerializeField] float moveSpeed;
    [SerializeField] FloatingJoystick joystick;
    [SerializeField] Rigidbody rb;

    private WeaponType weaponType;
    [SerializeField] Transform weaponStartPoint;

    private int level = 0;

    List<Character> characters = new List<Character>();


    public void FixedUpdate()
    {
        Vector3 direction = new Vector3(joystick.Horizontal, 0, joystick.Vertical);

        if (direction.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(direction);
            rb.velocity = new Vector3(joystick.Horizontal * moveSpeed, rb.velocity.y, joystick.Vertical * moveSpeed);
            ChangeAnim("run");
        }
        else
        {
            ChangeAnim("idle");
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

    private void Attack()
    {

    }

    private void Dead()
    {

    }


}
