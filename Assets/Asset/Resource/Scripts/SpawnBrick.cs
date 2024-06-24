using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnBrick : Singleton<SpawnBrick>
{
    [SerializeField] Brick brickPrefab;
    [SerializeField] int rows = 10;
    [SerializeField] int columns = 6;
    [SerializeField] float spacing = 4f;
    List<Brick> brickList = new List<Brick>();
    List<int> colorNumberList = new List<int>();
    PlayerMovement player;

    void Start()
    {
        RandomColor();
        SpawnBricks();
        InitBrickColor();
        
    }

    private void SpawnBricks()
    {
        for (int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                Vector3 position = new Vector3(i * spacing, 0, j * spacing);
                Brick newBrick = Instantiate(brickPrefab, position, Quaternion.Euler(0f, 90f, 0f), this.transform);
                brickList.Add(newBrick);
            }
        }
    }

    private void RandomColor()
    {
        List<int> number = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        for(int i = 0; i < 6; i++)
        {
            int random = Random.Range(0, 10);
            int newColorNumber = random;
            colorNumberList.Add(newColorNumber);
            number.Remove(random);
        }
        player = FindObjectOfType<PlayerMovement>();
        player.SetPlayerColor(colorNumberList[0]);
    }

    public void InitBrickColor()
    {
        int[] totalBricksColor = { 10, 10, 10, 10, 10, 10 };

        for (int i = 0; i < brickList.Count; i++)
        {
            while (true)
            {
                int brickColor = Random.Range(0, 6);
                if (totalBricksColor[brickColor] > 0)
                {
                    brickList[i].SetBrickColor(colorNumberList[brickColor]);
                    totalBricksColor[brickColor]--;
                    break;
                }
            }
        }
    }


    //void ShuffleList<T>(List<T> list) //ham xao tron cac phan tu trong list
    //{
    //    for (int i = 0; i < list.Count; i++)
    //    {
    //        T temp = list[i];
    //        int randomIndex = Random.Range(i, list.Count);
    //        list[i] = list[randomIndex];
    //        list[randomIndex] = temp;
    //    }
    //}

}
