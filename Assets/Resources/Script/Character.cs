using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private string currentAnim;
    public Animator animator;
    public CharacterRange range;
    public Bullet bulletPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AttackTarget()
    {
        range.RemoveNullTarget();
        if (range.botInRange.Count > 0)
        {
            Bullet bullet = Instantiate(bulletPrefabs);
            bullet.transform.position = transform.position;
            Vector3 direction = (range.GetNearestTarget().position - transform.position).normalized;
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
        Destroy(gameObject);
    }
}
