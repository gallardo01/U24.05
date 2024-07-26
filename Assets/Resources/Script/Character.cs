using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Transform body;
    public float speed = 5.0f;
    public Animator animator;
    private string currentAnim = "idle";
    public CharacterRange characterRange;
    public Bullet bulletPrefab;
    public bool isAttack = false;
    public TargetIndicator indicator;

    // private FieldOfView fieldOfView;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PrepareAttack()
    {
        
    }
  
    public void Throw()
    {
        characterRange.RemoveNullTarget();
        if (characterRange.botInRange.Count > 0)
        {
            Bullet bullet = Instantiate(bulletPrefab);
            bullet.transform.position = transform.position;
            bullet.self = this;
            Vector3 direction = (characterRange.GetNearestTarget().position - transform.position).normalized;
            bullet.transform.forward = direction;
            bullet.GetComponent<Rigidbody>().AddForce(300f * direction);
            transform.forward = direction;
            
           
        }
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

    public void OnDeath()
    {
        GameController.Ins.DecreaseAliveCount();
        Destroy(gameObject);
    }

    
}
