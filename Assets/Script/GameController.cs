using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Transform m_MonsterPfb;
    [SerializeField] private List<Transform> path_1 = new List<Transform>();
    [SerializeField] private List<Transform> path_2 = new List<Transform>();

    private List<Transform> monsterPath = new List<Transform>();

    private void Start()
    {
        InvokeRepeating(nameof(SpawnMonster), 1f, 1f);
    }

    void Update()
    {
    }

    public void SpawnMonster()
    {
        if (Random.Range(0, 2) == 0)
        {
            Debug.Log("Monster will moving on the left");
            monsterPath = path_1;
        }
        else
        {
            Debug.Log("Monster will moving on the right");
            monsterPath = path_2;
        }

        Transform monster = Instantiate(m_MonsterPfb, monsterPath[0].position, Quaternion.identity, transform);
        monster.GetComponent<MonsterController>().OnInit(monsterPath);
    }    
}
