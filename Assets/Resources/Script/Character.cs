using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : AbstractCharacter
{
    private string currentAnim;
    public Animator animator;
    public CharacterRange range;
    private Bullet bulletPrefabs;
    public bool isAttack = false;
    public TargetIndicator indicator;
    public int level = 1;
    public bool isDeath = false;
    public InitSkin skin;
    public Transform indicatorPoint;

    public override void OnInit()
    {
        level = 1;
        SetBodyScale();
        indicator.InitTarget(level);
        bulletPrefabs = ItemDatabase.Instance.bullets[skin.weaponsId];
    }

    public override void OnAttack()
    {
        Throw();
    }

    public override void OnDeath()
    {
        GameController.Instance.CharacterDead();
        isDeath = true;
        ChangeAnim("dead");
        gameObject.tag = "Untagged";
    }

    public void SetBodyScale()
    {
        transform.localScale = (1 + (level - 1) * 0.15f) * Vector3.one;
    }

    public void GainLevel() 
    {
        if (!isDeath)
        {
            level++;
            SetBodyScale();
            indicator.InitTarget(level);
        }
    }

    public void Throw()
    {
        range.RemoveNullTarget();
        if (range.botInRange.Count > 0)
        {
            skin.weaponItem.SetActive(false);
            Bullet bullet = Instantiate(bulletPrefabs);
            bullet.transform.position = transform.position;
            bullet.self = this;
            Vector3 direction = (range.GetNearestTarget().position - transform.position).normalized;
            bullet.transform.forward = direction;
            bullet.GetComponent<Rigidbody>().AddForce(300f * direction);
            transform.forward = direction;
            Invoke(nameof(EnableWeapons), 1f);
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
