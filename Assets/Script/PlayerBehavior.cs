using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] Bullet bullet;
    [SerializeField] float timeShooting = 1f;
    [SerializeField] float delayStarting = 0f;
    [SerializeField] LayerMask monsterLayerMask;

    MonsterMovement nearestMonster;

    private void Awake()
    {
        InvokeRepeating(nameof(Shooting), delayStarting, timeShooting);
    }

    private void Shooting()
    {
      //  MonsterMovement[] monsters = FindObjectsOfType<MonsterMovement>();

        List<MonsterMovement> monsters = new List<MonsterMovement>();
        Collider2D[] monstersCollider = Physics2D.OverlapCircleAll(transform.position, 30f, monsterLayerMask);
 
        for(int i = 0; i < monstersCollider.Length; i++)
        {
            monsters.Add(monstersCollider[i].GetComponent<MonsterMovement>());
        }

        float minDistance = float.MaxValue;
        if (monsters == null || monsters.Count == 0) return;

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
