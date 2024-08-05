using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Character : AbtractCharacter
{
    public Animator animator;
    public string currentAnimName = "idle";
    public Transform mesh;
    public CharacterRange range;
    public Bullet bulletPrefabs;
    public bool isAttack = false;
    public TargetIndicator indicator;
    public int level = 1;
    public bool isDeath = false;   

    // Start is called before the first frame update
    void Start()
    {
        OnInit();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void AttackTarget()
    //{
    //    isAttack = true;
    //    //range.RemoveNullTarget();
    //    //if (range.botsInCircle.Count > 0)
    //    //{
    //    //    Bullet bullet = Instantiate(bulletPrefabs);
    //    //    bullet.transform.position = transform.position;
    //    //    Vector3 direction = (range.GetNearestTarget().position - transform.position).normalized;
    //    //    bullet.transform.forward = direction;   
    //    //    bullet.GetComponent<Rigidbody>().AddForce(300f * direction);
    //    //    ChangeAnim("attack");
    //    //}

    //    //StartCoroutine(AttackTargetAnim());
    //}

    public override void OnInit()
    {
        level = 1;
        SetBodyScale();
        indicator.InitTarget(level);
    }
    public override void OnAttack()
    {
        Throw();
    }
    public override void OnDeath()
    {
        GameController.Instance.CharacterDead();    
        isDeath = true;
        //indicator.gameObject.SetActive(false);
        //Destroy(indicator); 
        ChangeAnim("dead");
        gameObject.tag = "Untagged";
    }


    public void SetBodyScale()
    {
        transform.localScale = (1 + (level - 1) * 0.1f) * Vector3.one;
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
        if (range.botsInCircle.Count > 0)
        {
            Bullet bullet = Instantiate(bulletPrefabs);
            bullet.transform.position = transform.position;
            bullet.self = this;
            Vector3 direction = (range.GetNearestTarget().position - transform.position).normalized;
            bullet.transform.forward = direction;
            bullet.GetComponent<Rigidbody>().AddForce(300f * direction);
            transform.forward = direction;      
        }
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            animator.ResetTrigger(animName);
            currentAnimName = animName;
            animator.SetTrigger(currentAnimName);
        }
    }
}
