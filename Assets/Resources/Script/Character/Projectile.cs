using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] int weapdamage;

    private Vector3 direction;

    void Update()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }



}
