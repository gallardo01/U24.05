using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour
{
    [SerializeField] private int m_Hp = 100;
    
    [SerializeField] private TextMeshProUGUI m_HpTxt;

    private int point_Index = 0;
    private List<Transform> monsterPath = new List<Transform>();
    public void OnInit(List<Transform> path)
    {
        monsterPath = path;
    }    

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        if (transform.position.x < monsterPath[point_Index].position.x)
        {
            transform.GetComponentInChildren<Transform>().rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.GetComponentInChildren<Transform>().rotation = Quaternion.Euler(0, 180, 0);
        } 
        m_HpTxt.GetComponentInParent<Image>().transform.rotation = Quaternion.Euler(0, 0, 0);


        transform.position = Vector2.MoveTowards(transform.position, monsterPath[point_Index].position, 0.005f);
        if(Vector2.Distance(transform.position, monsterPath[point_Index].position) < 0.1f)
        { 
            if(point_Index == monsterPath.Count - 1)
            {
                OnDeath();
                GameController.Instance.ReduceHealth(1);
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

    public void OnDestroy()
    {
        
    }

    public void TakeDamage(int damage)
    {
        m_Hp -= damage;
        UpdateUI();
        if(m_Hp <= 0)
        {
            GameController.Instance.AddCoin(1);
            OnDeath();
        }    
    }    

    public void UpdateUI()
    {
        m_HpTxt.text = m_Hp.ToString();
    }    
}
