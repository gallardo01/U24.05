using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] Bullet bullet;
    MonsterMovement nearestMonster;

    private void Awake()
    {
        InvokeRepeating(nameof(Shooting), 1f, 2f);
    }

    private void Shooting()
    {
        MonsterMovement[] monsters = FindObjectsOfType<MonsterMovement>();
        float minDistance = float.MaxValue;
        if (monsters == null || monsters.Length == 0) return;

        foreach (MonsterMovement monster in monsters)
        {
            if(Vector3.Distance(transform.position, monster.transform.position) < minDistance)
            {
                minDistance = Vector3.Distance(transform.position, monster.transform.position);
                nearestMonster = monster;
            }
        }


        Bullet bullet = Instantiate(this.bullet,transform.position,Quaternion.identity);
        Vector3 bulletDirection = nearestMonster.transform.position;
        bullet.SetDirection(bulletDirection);
    }
}
