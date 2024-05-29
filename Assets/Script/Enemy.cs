using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform monster;
    public bool rotate = true;

    private List<Transform> monsterRoad = new List<Transform>();
    private int current_road = 0;
    private void Start()
    {
        transform.position = new Vector2(0.07f,4f);
    }

    public void SetPath(List<Transform> path)
    {
        monsterRoad = path;
    }
    private void Update()
    {
        Debug.Log("note" +current_road);
        monster.position = Vector2.MoveTowards(monster.position, monsterRoad[current_road].position,0.001f);
        if (Vector2.Distance(monster.position, monsterRoad[current_road].position) < 0.1f)
        {
            if (current_road < monsterRoad.Count-2)
            {
                RotateEnemy();
            }      
            current_road++;   
            if (current_road == monsterRoad.Count)
            {
                gameObject.SetActive(false);
            }
        }
    }
    private void RotateEnemy()
    {
        if (monsterRoad[current_road].position.x > monsterRoad[current_road+1].position.x && Mathf.Abs(monsterRoad[current_road].position.x - monsterRoad[current_road + 1].position.x) > 0.5f)
        {
            monster.transform.Rotate(0f, 180f, 0f, Space.Self);
            rotate = false;
        } else if (monsterRoad[current_road].position.x < monsterRoad[current_road + 1].position.x && Mathf.Abs(monsterRoad[current_road].position.x - monsterRoad[current_road + 1].position.x) > 0.5f&& rotate == false)
        {
            monster.transform.Rotate(0f, 180f, 0f, Space.Self);
        }
    }
}
