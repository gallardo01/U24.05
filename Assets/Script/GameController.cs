using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviourSingleton<GameController>
{
    [SerializeField] Transform m_MonsterPfb;
    [SerializeField] Transform m_TowerPfb;

    [SerializeField] Button m_BuyBtn;
    [SerializeField] TextMeshProUGUI m_GoldQuantityTxt;
    [SerializeField] private int m_PlayerHP = 3;

    [SerializeField] GameObject m_Result;
    [SerializeField] TextMeshProUGUI m_TotalGoldTxt;
    [SerializeField] Button m_ReplayBtn;

    [SerializeField] private List<Transform> path_1 = new List<Transform>();
    [SerializeField] private List<Transform> path_2 = new List<Transform>();
    [SerializeField] private List<Transform> defenseArea = new List<Transform>();

    private List<KeyValuePair<Transform, bool>> areaState = new List<KeyValuePair<Transform, bool>>();
    private List<Transform> monsterPath = new List<Transform>();

    private Coroutine spawnMonsterCoroutine;
    private int m_GoldQuantity = 10;
    
    private void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        m_BuyBtn.onClick.AddListener(() => BuildTower());
        m_ReplayBtn.onClick.AddListener(() => OnInit());

        if (spawnMonsterCoroutine != null)
        {
            StopCoroutine(spawnMonsterCoroutine);
        }
        spawnMonsterCoroutine = StartCoroutine(SpawnMonster());

        foreach (Transform transform in defenseArea)
        {
            areaState.Add(new KeyValuePair<Transform, bool>(transform, true));
        }

        m_Result.SetActive(false);
        m_PlayerHP = 3;
        m_GoldQuantity = 10;
        UpdateUI();
    }

    void Update()
    {
    }

    IEnumerator SpawnMonster()
    {
        yield return new WaitForSeconds(1f);

        if (Random.Range(0, 2) == 0)
        {
            monsterPath = path_1;
        }
        else
        {
            monsterPath = path_2;
        }

        Transform monsterObj = Instantiate(m_MonsterPfb, monsterPath[0].position, Quaternion.identity, transform);
        MonsterController monster = monsterObj.GetComponent<MonsterController>();
        monster.OnInit(monsterPath);

        spawnMonsterCoroutine = StartCoroutine(SpawnMonster());
    }

    public void BuildTower()
    {
        if(m_GoldQuantity >= 10 && areaState.Count > 0)
        {
            m_GoldQuantity -= 10;
            SpawnTower();
            UpdateUI();
        }    
    }   
    
    public void SpawnTower()
    {
        int randomIndex = Random.Range(0, areaState.Count-1);
        Transform towerObj = Instantiate(m_TowerPfb, areaState[randomIndex].Key.position, Quaternion.identity, transform);
        TowerController tower = towerObj.GetComponent<TowerController>();
        areaState.RemoveAt(randomIndex);
    }    

    public void UpdateUI()
    {
        m_GoldQuantityTxt.text = m_GoldQuantity.ToString();
    }    

    public void AddCoin(int amount)
    {
        m_GoldQuantity += amount;
        UpdateUI();
    }    

    public void UpdatePlayerHP(int amount)
    {
        m_PlayerHP += amount;
        if (m_PlayerHP <= 0)
        {
            GameOver();
            m_Result.SetActive(true);
            m_TotalGoldTxt.text = "Total gold: " + m_GoldQuantity.ToString();
        }
    }    

    void GameOver()
    {
        StopAllCoroutines();

        spawnMonsterCoroutine = null;

        GameObject[] listMonster = GameObject.FindGameObjectsWithTag("Monster");
        GameObject[] listBullet = GameObject.FindGameObjectsWithTag("Bullet");
        GameObject[] listTower = GameObject.FindGameObjectsWithTag("Player");

        foreach (var obj in listMonster)
        {
            Destroy(obj);
        }

        foreach (var obj in listBullet)
        {
            Destroy(obj);
        }

        foreach (var obj in listTower)
        {
            Destroy(obj);
        }

        areaState.Clear();
    }    
}

