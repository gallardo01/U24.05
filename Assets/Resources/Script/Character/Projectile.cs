using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] int weapdamage;

    private Vector3 direction;
    private float rotationY;

    private void OnEnable()
    {
        rotationY = Random.Range(0, 30);
    }

    void Update()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }


}
