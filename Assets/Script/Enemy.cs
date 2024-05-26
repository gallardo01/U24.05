using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform monster;
    public List<Transform> path_1 = new List<Transform>();
    public List<Transform> path_2 = new List<Transform>();
    public Transform appear;

    private List<Transform> monsterRoad = new List<Transform>();
    private int current_road = 0;
    private void Start()
    {
        transform.position = appear.position;

        if (Random.Range(0,2)==0)
        {
            monsterRoad = path_1;
        }
        else
        {
            monsterRoad = path_2;
        }
    }
    private void Update()
    {
        monster.position = Vector2.MoveTowards(monster.position, monsterRoad[current_road].position,0.05f);
        if (Vector2.Distance(monster.position, monsterRoad[current_road].position) < 0.1f)
        {
                current_road++;
            if (current_road == monsterRoad.Count)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
