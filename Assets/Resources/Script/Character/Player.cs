using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private Vector3 moveDirection;
    private Collider[] enemies = new Collider[10];

    [Header("Setting")]
    [SerializeField] float moveSpeed;
    [SerializeField] private float detectRadius;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private float detectDelay;
    private float timer;

    [Header("Element")]
    [SerializeField] Transform shotingPoint;
    [SerializeField] Projectile projectilePrefab;

    private void Start()
    {
        InvokeRepeating(nameof(DetectEnemy), 1f, detectDelay);
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        moveDirection = JoystickControl.direct;
        if(moveDirection.magnitude > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDirection, Time.deltaTime * moveSpeed);
            transform.forward = moveDirection;
            ChangAnim("run");
        }
        else
        {
            ChangAnim("idle");
        }
    }

    private void DetectEnemy()
    {
        int availableEnemies = Physics.OverlapSphereNonAlloc(this.transform.position, detectRadius, enemies, enemyLayerMask);
        if (availableEnemies < 0) return;

        Transform target = null;
        float min = float.MaxValue;
        for (int i = 0; i < availableEnemies; i++)
        {
            float distance = Vector3.Distance(transform.position, enemies[i].transform.position);
            if (distance < min)
            {
                min = distance;
                target = enemies[i].transform;
            }
        }

        if (target != null) Attack(target); 
    }

    private void Attack(Transform target)
    {
        ChangAnim("attack");
        Projectile projectTile = LeanPool.Spawn(projectilePrefab, shotingPoint.transform.position, Quaternion.identity);
        LeanPool.Despawn(projectTile, 3);
        projectTile.SetDirection(target.position - shotingPoint.transform.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
