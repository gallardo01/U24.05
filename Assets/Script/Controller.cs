using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Controller : Singleton<Controller>
{
    public MonsterController monsterController;

    public List<HeroBase> heroController = new List<HeroBase>();

    public TextMeshProUGUI gold_contents;
    public TextMeshProUGUI lives_contents;
    public TextMeshProUGUI scores_contents;

    public List<Transform> path_1 = new List<Transform>();
    public List<Transform> path_2 = new List<Transform>();

    public List<HeroNodeData> heroNodes = new List<HeroNodeData>();

    public List<int> heroNodesReady = new List<int>();
    public List<int> heroNodesUsed = new List<int>();

    public List<GameObject> monsterAlive = new List<GameObject>();

    private int gold;
    private int lives;
    private int scores;

    public Button purchaseHeroes;

    public Vector3 monsterSummonPosition = new Vector3(0f, 7.5f, 0f);


    // Start is called before the first frame update
    void Start()
    {
        GameStart();
    }

    private void PurchaseHeroes()
    {
        if (gold >= 10)
        {
            if (heroController.Count <= 0)
            {
                return;
            }

            int newHeroPos = ReturnAvailablePosition();
            if (newHeroPos == -1)
            {
                return;
            }

            gold -= 10;
            gold_contents.text = gold.ToString();

            heroNodesReady.Remove(newHeroPos);
            heroNodesUsed.Add(newHeroPos);

            int index = Random.Range(0, heroController.Count);
            HeroBase newHero = Instantiate(heroController[index]);

            newHero.currentNode = newHeroPos;       
            newHero.transform.position = heroNodes[newHeroPos].trans.position;
        }
    }

    private int ReturnAvailablePosition()
    {
        if (heroNodesReady.Count > 0) 
        {
            int index = Random.Range(0, heroNodesReady.Count);
            return heroNodesReady[index];
        }
        else
        {
            return -1;
        }
    }
    IEnumerator CreateNewMonsterCoroutine()
    {
        yield return new WaitForSeconds(1f);
        //MonsterController newMonster = Instantiate(monsterController);
        MonsterController newMonster = (MonsterController)SimplePool.Spawn(PoolType.Monster, monsterSummonPosition, Quaternion.identity);

        if (Random.Range(0, 2) == 0)
        {
            newMonster.SetPath(path_1);
        }
        else
        {
            newMonster.SetPath(path_2);
        }

        StartCoroutine(CreateNewMonsterCoroutine());
    }

    public void MonsterComeHomeAndSayHello()
    {
        lives--;
        lives_contents.text = lives.ToString();
        if (lives == 0)
        {
            GameStop();
        }
    }

    public void MonsterDeadByBullet()
    {
        scores += 10;
        scores_contents.text = scores.ToString();
        gold += 1;
        gold_contents.text = gold.ToString();
    }

    private void GameStart()
    {
        gold = 60;
        lives = 3;
        scores = 0;

        gold_contents.text = gold.ToString();
        lives_contents.text = lives.ToString();
        scores_contents.text = scores.ToString();

        StartCoroutine(CreateNewMonsterCoroutine());

        for (int i = 0; i < heroNodes.Count; i++)
        {
            heroNodesReady.Add(i);
        }

        purchaseHeroes.onClick.AddListener(() => PurchaseHeroes());
    }

    private void GameStop()
    {
        StopAllCoroutines();
        Time.timeScale = 0;
    }
}