using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterController : GameUnit
{
    public Transform monster;
    private float speed = 2f;
    private int currentHealth = 100;
    private int maxHealth = 100;
    [SerializeField] Image healthBar;

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
        monster.position = Vector2.MoveTowards(monster.position, monsterRoad[current_road].position, speed * Time.deltaTime);
        if (Vector2.Distance(monster.position, monsterRoad[current_road].position) < 0.1f)
        {
            current_road++;
            if (current_road == monsterRoad.Count)
            {
                Destroy(gameObject);
                Controller.Ins.MonsterComeHomeAndSayHello();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);

            int damage = collision.gameObject.GetComponent<BulletController>().damage;
            TakeDamage(damage);

            if (currentHealth <= 0)
            {
                Controller.Ins.MonsterDeadByBullet();
                Destroy(gameObject);
            }
        }
    }

    void OnDestroy()
    {
        if (Controller.Ins != null)
        {
            Controller.Ins.monsterAlive.Remove(gameObject);
        }
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.fillAmount = (float)currentHealth / maxHealth;
    }
}