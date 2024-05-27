using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public GameObject monsterPrefabs;

    public GameObject heroesPrefabs;

    public List<Transform> path_1 = new List<Transform>();
    public List<Transform> path_2 = new List<Transform>();

    public List<Transform> heroes_path = new List<Transform>();
    private int gold = 1000;
    public Button purchaseHeroes;

    public List<int> heroes_marked = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        //Invoke(nameof(CreateNewObject), 3f);
        //InvokeRepeating(nameof(CreateNewObject), 1f, 1f);
        StartCoroutine(CreateNewObjectCoroutine());
        for (int i = 0; i < 16; i++)
        {
            heroes_marked.Add(0);
        }
        purchaseHeroes.onClick.AddListener(() => PurchaseHeroes());
    }

    private void PurchaseHeroes()
    {
        if (gold >= 50)
        {
            gold -= 50;
            GameObject newHeroes = Instantiate(heroesPrefabs);
            newHeroes.transform.position = heroes_path[ReturnAvailablePosition()].position;
        }
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

    private void Update()
    {
    }
}
