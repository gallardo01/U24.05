using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] int weapdamage;

    private Vector3 target;
    private float rotationY;


    private void OnEnable()
    {
        rotationY = Random.Range(0, 30);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target ,moveSpeed * Time.deltaTime);
    }

    public void SetDirection(Vector3 target)
    {
        this.target = target;
    }


}
