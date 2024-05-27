using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public GameObject monsterPrefab;

    public GameObject heroesPrefab;
    public List<Transform> path_1 = new List<Transform>();
    public List<Transform> path_2 = new List<Transform>();
    public List<Transform> heroes_path = new List<Transform>();
    private int gold = 1000; // Variable to store the gold count

    public Button purchaseHeroes;

    // Start is called before the first frame update
    void Start()
    {
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

    public void PurchaseHeroes()
    {
        if (gold >= 50 && heroes_path.Count > 0) 
        {
            gold -= 50;
            GameObject newHeroes = Instantiate(heroesPrefab);

            int randomIndex = Random.Range(0, heroes_path.Count);

            newHeroes.transform.position = heroes_path[randomIndex].position;

            heroes_path.RemoveAt(randomIndex);
        }
    }
}
