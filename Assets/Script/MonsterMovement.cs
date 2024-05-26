using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    [SerializeField] float speed;

    private List<Transform> choosePath;
    private Vector3 nextPosition;
    private int count = 0;
    

    void Start()
    {
        transform.position = choosePath[count].position;
        nextPosition = choosePath[count].position;
    }


    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
        ChangeDirection();
        Rotate();
    }

    public void ChangeDirection()
    {
        if (Vector3.Distance(transform.position, nextPosition) < Mathf.Epsilon && count < choosePath.Count - 1)
        {
            count++;
            nextPosition = choosePath[count].position;
        }
    }

    public void Rotate()
    {
        float distance = nextPosition.x - transform.position.x;
        if (distance > 0) transform.rotation = Quaternion.Euler(0, 0, 0); 
        if (distance < 0) transform.rotation = Quaternion.Euler(0, 180f, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("End"))
        {
            Destroy(gameObject);
        }
    }

    public void SetPath(List<Transform> path)
    {
        choosePath = path;
    }

}
