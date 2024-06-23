using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBrick : Singleton<SpawnBrick>
{
    [SerializeField] GameObject[] brickPrefabs;
    [SerializeField] int rows = 10;
    [SerializeField] int columns = 6;
    [SerializeField] float spacing = 4f;
    List<GameObject> brickList = new List<GameObject>();

    void Awake()
    {
        SpawnBricks();
    }

    void SpawnBricks()
    {
        for (int i = 0; i < brickPrefabs.Length; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                brickList.Add(brickPrefabs[i]);
            }
        }

        ShuffleList(brickList);

        int index = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Vector3 position = new Vector3(i * spacing, 0, j * spacing);
                GameObject brickPrefab = brickList[index];
                Instantiate(brickPrefab, position, Quaternion.Euler(0f, 90f, 0f),this.transform);
                index++;
            }
        }
    }

    void ShuffleList<T>(List<T> list) //ham xao tron cac phan tu trong list
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public IEnumerator RespawnBrick(Vector3 position, float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject brickPrefab = brickPrefabs[Random.Range(0, brickPrefabs.Length)];
        Instantiate(brickPrefab, position, Quaternion.Euler(0f, 90f, 0f));
    }
}
