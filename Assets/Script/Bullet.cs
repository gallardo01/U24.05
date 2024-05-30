using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetTarget(Transform target)
    {
        this.target = target;  
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && target.gameObject.activeInHierarchy)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, 0.025f);
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
