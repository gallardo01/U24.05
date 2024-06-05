using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Controller : Singleton<Controller>
{
    public GameObject monsterPrefabs;
    public GameObject heroPrefabs;
    public List<GameObject> heroPrefabsList;
    public GameObject gameOverPanel;

    public List<Transform> path_1 = new List<Transform>();
    public List<Transform> path_2 = new List<Transform>();

    public List<int> heroes_marked = new List<int>();
    public List<Transform> heroesPath;
    public Button purchaseHeroes;
    public Button replayButton;

    private int gold = 100;
    public TMP_Text goldText;

    private float health = 3f;
    public TMP_Text healthText;



    private void Start()
    {
        gameOverPanel.SetActive(false);
        GameStart();
        purchaseHeroes.onClick.AddListener(() => PurchaseHeroes());
        replayButton.onClick.AddListener(() => GameStart());
    }
    public void GameStart()
    {
        gameOverPanel.SetActive(false);
        heroes_marked.Clear();
        gold = 50;
        health = 3;
        SetTextGold();
        SetHealth();
        InvokeRepeating(nameof(CreateNewObject), 1f, 1f);
        
        for(int i =0; i < 16; i++)
        {
            heroes_marked.Add(0);
        }       
    }

    private void PurchaseHeroes()
    {
        
        if (gold >= 50)
        {
            gold -= 50;
            SetTextGold();
            GameObject newHeroes = Instantiate(heroPrefabsList[Random.Range(0,2)]);
            newHeroes.transform.position = heroesPath[ReturnAvailblePosittion()].position;
        }
    }

    private int ReturnAvailblePosittion() 
    {
        while (true)
        {
            int pos = Random.Range(0, 16);
            if (heroes_marked[pos] == 0)
            {
                heroes_marked[pos] = 1;
                return pos;
            }
        }
    }


    public void ReduceHealth(int health)
    {
        this.health -= health;
        if (this.health <= 0)
        {
            GameOver();        
        }
        SetHealth();

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
   
    private void SetHealth()
    {
        healthText.GetComponentInChildren<TextMeshProUGUI>().text = health.ToString();
    }
    private void SetTextGold()
    {
        goldText.GetComponentInChildren<TextMeshProUGUI>().text = gold.ToString();
    }

    public void GainGold(int gold)
    {
        this.gold += gold;
        SetTextGold();
    }
   

    private void GameOver()
    {
        //dung sinh ra monster
        StopAllCoroutines();
        CancelInvoke(nameof(CreateNewObject));
        //Tat het monster hien tai
        gameOverPanel.SetActive(true);
        GameObject[] listMonster = GameObject.FindGameObjectsWithTag("Monster");
        Debug.Log("So Monster" + listMonster.Length);
        for(int i = 0; i < listMonster.Length; i++) {
            Destroy(listMonster[i]);    
        }


        //tat het bullet hien tai
        GameObject[] listBullet = GameObject.FindGameObjectsWithTag("Bullet");
        Debug.Log("So bullet" + listBullet.Length);
        for (int i = 0; i < listBullet.Length; i++)
        {
            Destroy(listBullet[i]);
        }

        Debug.Log("So Hero" + listMonster.Length);
        GameObject[] listHero = GameObject.FindGameObjectsWithTag("Hero");
        for (int i = 0; i < listHero.Length; i++)
        {
            Destroy(listHero[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
