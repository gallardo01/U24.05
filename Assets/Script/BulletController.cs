using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private GameObject target;
    private float speed = 5f;

    Vector2 direction;
    void Update()
    {
        if (Controller.Ins.isGameOver) { return; }

        if (target == null)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }

    public void SetTarget(GameObject monster) 
    {
        target = monster;
    }
}
