using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : AbstractCharacter
{
    public Transform body;
    public float speed = 5.0f;
    public Animator animator;
    private string currentAnim = "idle";
    public CharacterRange characterRange;
    public Bullet bulletPrefab;
    public bool isAttack = false;
    public TargetIndicator indicator;
    public int level = 1;
    public bool isDead = false;
    public InitSkin skin;
    public Transform indicatorPoint;

    // private FieldOfView fieldOfView;
    

    public override void OnInit()
    {
        level = 1;
        SetBodyScale();
        indicator.InitTarget(level);
        bulletPrefab = ItemDatabase.Ins.bullets[skin.weaponsId];
    }
    
    public override void OnAttack()
    {
        Throw();
    }
    
    public override void OnDeath()
    {
        isDead = true;
        GameController.Ins.CharacterDead();
        ChangeAnim("death");
        gameObject.tag = "Untagged";
    }
    
    

    private void SetBodyScale()
    {
        body.localScale = (1 + 0.1f * ( level - 1)) * Vector3.one;
    }
    
    public void GainLevel()
    {
        if (!isDead)
        {
            level ++;
            SetBodyScale();
            indicator.InitTarget(level);
        }
    }
    
  
    public void Throw()
    {
        characterRange.RemoveNullTarget();
            if (characterRange.botInRange.Count > 0)
            {
                Transform nearestTarget = characterRange.GetNearestTarget();
                if (nearestTarget != null)
                {
                    skin.weaponItem.SetActive(false);
                    Bullet bullet = Instantiate(bulletPrefab);
                    bullet.transform.position = transform.position;
                    bullet.self = this;
                    Vector3 direction = (nearestTarget.position - transform.position).normalized;
                    bullet.transform.forward = direction;
                    bullet.GetComponent<Rigidbody>().AddForce(300f * direction);
                    transform.forward = direction;
                    Invoke(nameof(EnableWeapons), 1f);
                }
            }
    }
    
    private void EnableWeapons()
    {
        skin.weaponItem.SetActive(true);
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            animator.ResetTrigger(animName);
            currentAnim = animName;
            animator.SetTrigger(currentAnim);
        }
    }
}
