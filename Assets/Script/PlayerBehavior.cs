using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] Bullet bullet;
    [SerializeField] float timeShooting = 1f;
    [SerializeField] LayerMask monsterLayerMask;
    [SerializeField] Animator playerAnimator;
    [SerializeField] int damage = 20;

    string currentAnimName;
    MonsterMovement nearestMonster;
    private bool isGotBuff;

    private void Awake()
    {
        ChangeAnim("idle");
        StartCoroutine(Shooting());
    }


    IEnumerator Shooting()
    {
        ChangeAnim("idle");
        yield return new WaitForSeconds(timeShooting);

        List<MonsterMovement> monsters = new List<MonsterMovement>();
        Collider2D[] monstersCollider = Physics2D.OverlapCircleAll(transform.position, 30f, monsterLayerMask);
 
        for(int i = 0; i < monstersCollider.Length; i++)
        {
            monsters.Add(monstersCollider[i].GetComponent<MonsterMovement>());
        }
        if (monsters == null || monsters.Count == 0) yield return null;

        float minDistance = float.MaxValue;
        foreach (MonsterMovement monster in monsters)
        {
            if(Vector3.Distance(transform.position, monster.transform.position) < minDistance)
            {
                minDistance = Vector3.Distance(transform.position, monster.transform.position);
                nearestMonster = monster;
            }
        }
        ChangeAnim("attack");
        Bullet bullet = Instantiate(this.bullet,transform.position,Quaternion.identity);
        Transform bulletDirection = nearestMonster.transform;
        bullet.SetDirection(bulletDirection);
        bullet.SetBulletDamage(this.damage);

        StartCoroutine(Shooting());
    }

    private void ChangeAnim(string animName)
    {
        if(currentAnimName != animName)
        {
            playerAnimator.ResetTrigger(animName);
            currentAnimName = animName;
            playerAnimator.SetTrigger(currentAnimName);
        }
    }

    public void SetDamage(int damage)
    {
        if (isGotBuff == false)
        {
            this.damage += damage;
            isGotBuff = true;
        }
    }
}
