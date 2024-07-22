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


    // private FieldOfView fieldOfView;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update called");
        AttackTarget();
    }

    public void AttackTarget()
    {
        Debug.Log("AttackTarget called");
        characterRange.RemoveNullTarget();
        if (characterRange.botInRange.Count > 0)
        {
            Bullet bullet = Instantiate(bulletPrefab);
            bullet.transform.position = transform.position;
            Vector3 direction = (characterRange.GetNearestTarget().position - transform.position).normalized;
            bullet.transform.forward = direction;
            bullet.GetComponent<Rigidbody>().AddForce(300f * direction);
        }
    }


    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            if (animName == "idle")
            {
                AttackTarget();
            }
            animator.ResetTrigger(currentAnim);
            currentAnim = animName;
            animator.SetTrigger(currentAnim);
        }
    }

    public void OnDeath()
    {
        ChangeAnim("death");
        Destroy(gameObject,1f);
    }
    
}
