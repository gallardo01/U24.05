using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField] private float m_Speed = 100f;
    [SerializeField] private List<Transform> path = new List<Transform>();

    private int point_Index = 0;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, path[point_Index].position, 0.01f);
        if(Vector2.Distance(transform.position, path[point_Index].position) < 0.1f && point_Index < path.Count - 1)
        { 
            point_Index++;
        }
    }
}
