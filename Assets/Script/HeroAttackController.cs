using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttackController : HeroBase
{
    public BulletController bulletController;
    private int damageBase = 50;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AttackMonster());
    }

    IEnumerator AttackMonster()
    {
        yield return new WaitForSeconds(1f);
        GameObject target = FindTarget();
        if (target != null)
        {
            int damage;
            int multiplier = 1;

            GetBuffs();
            if (Buffs.ContainsKey(BuffType.DoubleDamage))
            {
                multiplier += Buffs[BuffType.DoubleDamage];
            }
            damage = damageBase * multiplier;

            //BulletController bullet = Instantiate(bulletController, transform.position, Quaternion.identity);
            BulletController bullet = (BulletController)SimplePool.Spawn(PoolType.Bullet, transform.position, Quaternion.identity);

            bullet.SetTarget(target);
            bullet.SetDamage(damage);

        }
        StartCoroutine(AttackMonster());
    }

    private GameObject FindTarget()
    {
        if (Controller.Ins.monsterAlive.Count > 0)
        {
            return Controller.Ins.monsterAlive[Random.Range(0, Controller.Ins.monsterAlive.Count)];
        }
        else
        {
            return null;
        }
    }


}