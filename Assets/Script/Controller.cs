using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private static Controller instance;
    public static Controller Instance => instance;

    [SerializeField] private MonsterMovement monsterPrefab;

    [SerializeField] List<Transform> rightPath;
    [SerializeField] List<Transform> leftPath;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, 1f);
    }

    public void SpawnEnemy()
    {
        List<Transform> path = ChoosePath();

        MonsterMovement monster = Instantiate(monsterPrefab, path[0].position, Quaternion.identity);
        monster.SetPath(path);
    }

    private List<Transform> ChoosePath()
    {
        int random = Random.Range(0, 2);
        if (random == 0) return leftPath;
        if (random == 1) return rightPath;
        else return null;
    }
}
