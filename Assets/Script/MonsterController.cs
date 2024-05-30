using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField] private float m_Speed = 2f;

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
        if (transform.position.x < monsterPath[point_Index].position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        } 
            

        transform.position = Vector2.MoveTowards(transform.position, monsterPath[point_Index].position, 0.005f);
        if(Vector2.Distance(transform.position, monsterPath[point_Index].position) < 0.1f)
        { 
            if(point_Index == monsterPath.Count - 1)
            {
                OnDeath();
                GameController.Instance.UpdatePlayerHP(-1);
                //Destroy(gameObject);
                //gameObject.SetActive(false);
            }
            else
            {
                point_Index++;
            }
        }
    }

    public void OnDeath()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            OnDeath();
        }    
    }

    public void OnDestroy()
    {
        
    }


}
