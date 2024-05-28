using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public Transform monster;
    public float speed = 0.001f;

    private List<Transform> monsterRoad = new List<Transform>();
    private int current_road = 0;

    // Start is called before the first frame update
    private void Start()
    {
        transform.position = new Vector2(0f, 7.5f);
        Controller.Ins.monsterAlive.Add(gameObject);
    }

    public void SetPath(List<Transform> path)
    {
        monsterRoad = path;
    }

    // Update is called once per frame
    void Update() 
    {
        if(!Controller.Ins.isGameOver)
        {
            monster.position = Vector2.MoveTowards(monster.position, monsterRoad[current_road].position, speed);
            if (Vector2.Distance(monster.position, monsterRoad[current_road].position) < 0.1f)
            {
                current_road++;
                if (current_road == monsterRoad.Count)
                {
                    Destroy(gameObject);
                    Controller.Ins.monsterComeHomeAndSayHello();
                }
            }
        }    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            Controller.Ins.monsterDeadByBullet();
        }
    }

    void OnDestroy()
    {
        if (Controller.Ins != null)
        {
            Controller.Ins.monsterAlive.Remove(gameObject);
        }
    }
}