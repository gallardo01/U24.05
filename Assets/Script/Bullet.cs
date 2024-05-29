using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform target;
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && target.gameObject.activeInHierarchy)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, 0.04f);
        } else
        {
            Destroy(gameObject);
        }
    }
}
