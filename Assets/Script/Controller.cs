using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Controller : Singleton<Controller>
{
    public GameObject monsterPrefabs;
    public GameObject heroesPrefabs;

    public TextMeshProUGUI gold_contents;
    public TextMeshProUGUI lives_contents;
    public TextMeshProUGUI scores_contents;

    public List<Transform> path_1 = new List<Transform>();
    public List<Transform> path_2 = new List<Transform>();

    public List<Transform> heroes_path = new List<Transform>();

    public List<GameObject> monsterAlive = new List<GameObject>();

    private int numberPositionUsed = 0;
    private int gold = 10;
    private int lives = 3;
    private int scores = 0;

    public Button purchaseHeroes;

    public bool isGameOver = false;

    public List<int> heroes_marked = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        gold_contents.text = gold.ToString();
        lives_contents.text = lives.ToString();
        scores_contents.text = scores.ToString();

        //Invoke(nameof(CreateNewObject), 3f);
        //InvokeRepeating(nameof(CreateNewObject), 1f, 1f);
        StartCoroutine(CreateNewObjectCoroutine());
        for (int i = 0; i < heroes_path.Count; i++)
        {
            heroes_marked.Add(0);
        }
        purchaseHeroes.onClick.AddListener(() => PurchaseHeroes());
    }

    private void PurchaseHeroes()
    {
        if (gold >= 10 && numberPositionUsed < heroes_path.Count)
        {
            gold -= 10;
            gold_contents.text = gold.ToString();
            GameObject newHeroes = Instantiate(heroesPrefabs);
            newHeroes.transform.position = heroes_path[ReturnAvailablePosition()].position;
        }
    }

    private int ReturnAvailablePosition()
    {
        while (true)
        {
            int pos = Random.Range(0, heroes_path.Count);
            if (heroes_marked[pos] == 0)
            {
                heroes_marked[pos] = 1;
                numberPositionUsed++;
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

    public void monsterComeHomeAndSayHello()
    {
        lives--;
        lives_contents.text = lives.ToString();
        if (lives == 0)
        {
            isGameOver = true;
            //StopAllCoroutines();
            Time.timeScale = 0;
        }
    }

    public void monsterDeadByBullet()
    {
        scores += 10;
        scores_contents.text = scores.ToString();
        gold += 1;
        gold_contents.text = gold.ToString();
    }
}