using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Controller : Singleton<Controller>
{
    public GameObject monsterPrefabs;
    public GameObject heroesPrefabs;

    public List<Transform> path_1 = new List<Transform>();
    public List<Transform> path_2 = new List<Transform>();

    public List<Transform> heroes_path = new List<Transform>();
    private int gold = 10;
    public Button purchaseHeroes;
    public Button replay;

    public TextMeshProUGUI goldText;
    public TextMeshProUGUI healthText;

    public List<int> heroes_marked = new List<int>();
    private int health = 3;
    public GameObject panelGameOver;

    // Start is called before the first frame update
    void Start()
    {
        GameStart();
        purchaseHeroes.onClick.AddListener(() => PurchaseHeroes());
        replay.onClick.AddListener(() => GameStart());
    }

    public void ReduceHealth(int health)
    {
        this.health -= health;
        healthText.text = this.health.ToString();
        if (this.health <= 0)
        {
            // Game Over
            GameOver();
        }
    }

    private void GameStart()
    {
        panelGameOver.SetActive(false);
        gold = 10;
        health = 3;
        SetTextGold();
        StartCoroutine(CreateNewObjectCoroutine());
        heroes_marked.Clear();
        for (int i = 0; i < 16; i++)
        {
            heroes_marked.Add(0);
        }
    }

    private void GameOver()
    {
        panelGameOver.SetActive(true);
        // Dung` sinh ra monster moi'
        StopAllCoroutines();
        // Tat het monster hien tai
        GameObject[] listMonster = GameObject.FindGameObjectsWithTag("Monster");
        for (int i = 0; i < listMonster.Length; i++)
        {
            Destroy(listMonster[i]);
        }
        // Tat het nhung bullet hien tai
        GameObject[] listBullet = GameObject.FindGameObjectsWithTag("Bullet");
        for (int i = 0; i < listBullet.Length; i++)
        {
            Destroy(listBullet[i]);
        }
        // Tat het heroes hien tai
        GameObject[] listHeroes = GameObject.FindGameObjectsWithTag("Hero");
        for (int i = 0; i < listHeroes.Length; i++)
        {
            Destroy(listHeroes[i]);
        }
    }

    private void PurchaseHeroes()
    {
        if (gold >= 10)
        {
            gold -= 10;
            SetTextGold();
            GameObject newHeroes = Instantiate(heroesPrefabs);
            newHeroes.transform.position = heroes_path[ReturnAvailablePosition()].position;
        }
    }

    public void GainGold(int gold)
    {
        this.gold += gold;
        SetTextGold();
    }

    private void SetTextGold()
    {
        goldText.text = gold.ToString(); 
    }


    private int ReturnAvailablePosition()
    {
        while(true)
        {
            int pos = Random.Range(0, 16);
            if (heroes_marked[pos] == 0)
            {
                heroes_marked[pos] = 1;
                return pos;
            }
        }
    }
    IEnumerator CreateNewObjectCoroutine()
    {
        yield return new WaitForSeconds(2f);
        GameObject newObject = Instantiate(monsterPrefabs);
        if (Random.Range(0, 2) == 0)
        {
            newObject.GetComponent<MonsterController>().SetPath(path_1);
        }
        else
        {
            newObject.GetComponent<MonsterController>().SetPath(path_2);
        }
        StartCoroutine(CreateNewObjectCoroutine());
    }

    private void CreateNewObject()
    {
        GameObject newObject = Instantiate(monsterPrefabs);
        if (Random.Range(0,2) == 0)
        {
            newObject.GetComponent<MonsterController>().SetPath(path_1);
        } else
        {
            newObject.GetComponent<MonsterController>().SetPath(path_2);
        }
    }
}
