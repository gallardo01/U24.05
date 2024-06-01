using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public Transform monster;
    public int speed = 1500;
    private float hp = 100f;
    private List<Transform> monsterRoad = new List<Transform>();
    private int current_road = 0;
    private int enemyEnter;
    // Start is called before the first frame update
    private void Start()
    {
        transform.position = new Vector2(-0.28f, 8f);
    }

    public void SetPath(List<Transform> path)
    {
        monsterRoad = path;
    }

    // Update is called once per frame
    void Update() 
    {
        monster.position = Vector2.MoveTowards(monster.position, monsterRoad[current_road].position, 0.01f);
        Debug.Log(current_road);
        if (Vector2.Distance(monster.position, monsterRoad[current_road].position) < 0.1f)
        {
            current_road++;
            if (current_road == monsterRoad.Count)
            {
                gameObject.SetActive(false);
                Debug.Log("Enemy Entered");
                Controller.Instance.ReduceHealth(1);
            }
        }
        if(hp == 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Controller.Instance.GainGold(50);
            hp -= 50f;
            Destroy(collision.gameObject);
        }
        
    }
}
