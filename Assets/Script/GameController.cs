using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] Transform m_MonsterPfb;
    [SerializeField] Transform m_TowerPfb;

    [SerializeField] Button m_BuyBtn;
    [SerializeField] TextMeshProUGUI m_GoldQuantityTxt;

    [SerializeField] private List<Transform> path_1 = new List<Transform>();
    [SerializeField] private List<Transform> path_2 = new List<Transform>();
    [SerializeField] private List<Transform> defenseArea = new List<Transform>();

    private List<KeyValuePair<Transform, bool>> areaState = new List<KeyValuePair<Transform, bool>>();
    private List<Transform> monsterPath = new List<Transform>();

    private int m_GoldQuantity = 1000;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnMonster), 1f, 1f);
        m_BuyBtn.onClick.AddListener(BuildTower);
        UpdateUI();
        foreach(Transform transform in defenseArea)
        {
            areaState.Add(new KeyValuePair<Transform, bool>(transform, true));
        }
    }

    void Update()
    {

    }

    public void SpawnMonster()
    {
        if (Random.Range(0, 2) == 0)
        {
            //Debug.Log("Monster will moving on the left");
            monsterPath = path_1;
        }
        else
        {
            //Debug.Log("Monster will moving on the right");
            monsterPath = path_2;
        }

        Transform monster = Instantiate(m_MonsterPfb, monsterPath[0].position, Quaternion.identity, transform);
        monster.GetComponent<MonsterController>().OnInit(monsterPath);
    }    

    public void BuildTower()
    {
        if(m_GoldQuantity >= 10 && areaState.Count > 0)
        {
            m_GoldQuantity -= 10;
            SpawnTower();
        }    
    }   
    
    public void SpawnTower()
    {
        int randomIndex = Random.Range(0, areaState.Count-1);
        Transform tower = Instantiate(m_TowerPfb, areaState[randomIndex].Key.position, Quaternion.identity, transform);
        areaState.RemoveAt(randomIndex);
    }    

    public void UpdateUI()
    {
        m_GoldQuantityTxt.text = m_GoldQuantity.ToString();
    }    
}

