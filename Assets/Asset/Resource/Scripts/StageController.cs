using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class StageController : Singleton<StageController>
{
    [SerializeField] Brick brickPrefab;
    [SerializeField] int rows = 10;
    [SerializeField] int columns = 6;
    [SerializeField] float spacing = 4f;
    List<GameObject> dots = new List<GameObject>();
    List<int> numberList = new List<int>();
    List<Brick> brickList = new List<Brick>();
    List<int> colorNumberList = new List<int>();

    void Start()
    {
        SpawnDot();
    }

    private void SpawnDot()
    {
        for (int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                GameObject emty = new GameObject("Dot");
                Vector3 position = new Vector3(i * spacing, 0, j * spacing);
                GameObject dot = Instantiate(emty, position, Quaternion.identity, this.transform);
                dots.Add(dot);
            }
        }
        for(int i = 0; i < dots.Count; i++)
        {
            numberList.Add(i);
        }
    }

    public void SpawnCharacterBricks(int color)
    {
        for(int i = 0;i < 10; i++)
        {
            int random = Random.Range(0, numberList.Count);
            int bricknumber = numberList[random];
            numberList.Remove(bricknumber);

            Brick newBrick = Instantiate(brickPrefab, dots[bricknumber].transform);
            newBrick.transform.localRotation = Quaternion.Euler(0, 90, 0);
            newBrick.SetBrickNumber(bricknumber);
            newBrick.SetBrickNumber(color);

            brickList.Add(newBrick);
        }
    }

    private void RandomColor()
    {
        List<int> number = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        for(int i = 0; i < 6; i++)
        {
            int random = number[Random.Range(0, number.Count)];
            colorNumberList.Add(random);
            number.Remove(random);
        }
        FindObjectOfType<Player>().SetCharacterColor(colorNumberList[0]);
        FindObjectOfType<Bot>().SetCharacterColor(colorNumberList[1]);
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

    public void SpawnNewBrick(Brick brick)
    {
        StartCoroutine(SpawnBrickSequence(brick));
    }

    IEnumerator SpawnBrickSequence(Brick brick)
    {
        brickList.Remove(brick);
        Vector3 position = brick.transform.position;

        yield return new WaitForSeconds(2f);

        Brick newBrick = Instantiate(brickPrefab, position, Quaternion.Euler(0f, 90f, 0f), this.transform);
        int random = Random.Range(0, colorNumberList.Count);
        newBrick.SetBrickColor(colorNumberList[random]);

        brickList.Add(newBrick);
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
