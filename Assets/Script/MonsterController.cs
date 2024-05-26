using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public Transform monster;
    //public Transform startPath;
    
    public float speed = 1.5f;

    private List<Transform> monsterRoad = new List<Transform>();
    private int current_road = 0;

    void Start()
    {
        transform.position = new Vector2(-0.09f, 5.08f);
        
    }

    public void SetPath(List<Transform> path)
    {
        monsterRoad = path;
    }

    void Update()
    {
        monster.position = Vector2.MoveTowards(monster.position, monsterRoad[current_road].position, 0.01f);
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
