using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private string currentAnim;
    public Animator animator;
    public CharacterRange range;
    public Bullet bulletPrefabs;
    public bool isAttack = false;
    public TargetIndicator indicator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Throw()
    {
        range.RemoveNullTarget();
        if (range.botInRange.Count > 0)
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
        if (currentAnim != animName)
        {
            animator.ResetTrigger(animName);
            currentAnim = animName;
            animator.SetTrigger(currentAnim);
        }
    }

    public void OnDeath()
    {
        Destroy(gameObject);
    }
}
