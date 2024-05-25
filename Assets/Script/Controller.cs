using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject monsterPrefabs;

    public List<Transform> path_1 = new List<Transform>();
    public List<Transform> path_2 = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(CreateNewObject), 1f, 1f);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
