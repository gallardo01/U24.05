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
        if(transform.position.x < monsterPath[point_Index].position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        } 
            

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
