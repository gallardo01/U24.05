using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField] private float m_Speed = 100f;
    [SerializeField] private List<Transform> path_1 = new List<Transform>();
    [SerializeField] private List<Transform> path_2 = new List<Transform>();

    private int point_Index = 0;
    private List<Transform> monsterPath = new List<Transform>();

    void Start()
    {
        if(Random.Range(0,2) == 0)
        {
            Debug.Log("Monster will moving on the left");
            monsterPath = path_1;
        }
        else
        {
            Debug.Log("Monster will moving on the right");
            monsterPath = path_2;
        }
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, monsterPath[point_Index].position, 0.01f);
        if(Vector2.Distance(transform.position, monsterPath[point_Index].position) < 0.1f)
        { 
            if(point_Index == monsterPath.Count - 1)
            {
                Debug.Log("Monster reach the destination");
                Destroy(gameObject);
                //gameObject.SetActive(false);
            }
            else
            {
                point_Index++;
            }
        }
    }
}
