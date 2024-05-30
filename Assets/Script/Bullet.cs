using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform target;
    [SerializeField] float moveSpeed = 3f;

    private void Update()
    {
        if (target != null && target.gameObject.activeInHierarchy)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        } else
        {
            Destroy(gameObject);
        }
        
    }
    public void SetTargetFire(Transform target)
    {
       this.target = target;
    }
}
