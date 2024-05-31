using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Apple.ReplayKit;

public class Controller : Singleton<Controller>
{
    public GameObject monsterPrefab;

    public GameObject heroesPrefab;
    public List<Transform> path_1 = new List<Transform>();
    public List<Transform> path_2 = new List<Transform>();
    public List<Transform> heroes_path = new List<Transform>();
    private int gold = 10; // Variable to store the gold count

    public Button purchaseHeroes;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI healthyText;

    private int healthy = 3;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
        //InvokeRepeating(nameof(CreateNewObject), 1f, 1f);
        StartCoroutine(CreateNewObjectCoroutine());

        purchaseHeroes.onClick.AddListener(PurchaseHeroes);
    }

    IEnumerator CreateNewObjectCoroutine()
    {
        yield return new WaitForSeconds(3f);
        GameObject newObject = Instantiate(monsterPrefab);
        if (Random.Range(0, 2) == 0)
            newObject.GetComponent<MonsterController>().SetPath(path_1);
        else
        {
            newObject.GetComponent<MonsterController>().SetPath(path_2);
        }
        StartCoroutine(CreateNewObjectCoroutine());
    }
    private void CreateNewObject()
    {
        GameObject newObject = Instantiate(monsterPrefab);
        if (Random.Range(0, 2) == 0)
            newObject.GetComponent<MonsterController>().SetPath(path_1);
        else
        {
            newObject.GetComponent<MonsterController>().SetPath(path_2);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReduceHealth(int healthy)
    {
        this.healthy -= healthy;
        healthyText.text = this.healthy.ToString();
        if(this.healthy <= 0)
        {
            GameOver();
            //Game over
        }
    }

    private void StartGame()
    {
        gold = 10;
        healthy = 3;
        SetTextGold();
        StartCoroutine(CreateNewObjectCoroutine());
    }
    private void GameOver()
    {
        // Dung sinh ra monster
        StopAllCoroutines();
        // Tat het monster hien tai
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
       for(int i = 0; i < monsters.Length; i++)
        {
            Destroy(monsters[i]);
        }
        // tat het bullet
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        for (int i = 0; i < bullets.Length; i++)
        {
            Destroy(bullets[i]);
        }

        // Tat het heroes
        GameObject[] heroes = GameObject.FindGameObjectsWithTag("Heroes");
        for (int i = 0; i < heroes.Length; i++)
        {
            Destroy(heroes[i]);
        }
    }

    public void PurchaseHeroes()
    {
        if (gold >= 10 && heroes_path.Count > 0) 
        {
            gold -= 10;
            SetTextGold();
            GameObject newHeroes = Instantiate(heroesPrefab);

            int randomIndex = Random.Range(0, heroes_path.Count);

            newHeroes.transform.position = heroes_path[randomIndex].position;

            heroes_path.RemoveAt(randomIndex);
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
}
