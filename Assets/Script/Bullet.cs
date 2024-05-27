using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 50;
    private Vector3 direction;

    private void Start()
    {
        Invoke(nameof(AutoDestroy), 1f);
    }

    void Update()
    {
        transform.Translate(direction * Time.deltaTime * bulletSpeed);
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    void AutoDestroy()
    {
        Destroy(gameObject);
    }

}
