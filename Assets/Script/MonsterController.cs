using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField] private float m_Speed = 100f;

    private int point_Index = 0;
    private List<Transform> monsterPath = new List<Transform>();

    public void OnInit(List<Transform> path)
    {
        monsterPath = path;
    }    

    void Start()
    {

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
