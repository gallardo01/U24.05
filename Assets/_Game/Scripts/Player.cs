using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] float moveSpeed;
    [SerializeField] FloatingJoystick joystick;
    [SerializeField] Rigidbody rb;


    protected override void Update()
    {
        base.Update();

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

    public override void OnDead()
    {
        base.OnDead();
        Debug.Log("player dead");
    }
}
