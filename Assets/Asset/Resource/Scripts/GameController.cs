using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    private List<int> gameColors = new List<int>();
    public List<int> GameColors => gameColors;

    private void Awake()
    {
        RandomColor();
    }

    private void RandomColor()
    {
        List<int> number = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        for (int i = 0; i < 6; i++)
        {
            int random = number[Random.Range(0, number.Count)];
            gameColors.Add(random);
            number.Remove(random);
        }
        FindObjectOfType<Player>().SetCharacterColor(gameColors[0]);
        //for(int i = 0; i < 5; i++)
        //{
        //    FindObjectOfType<Bot>().SetCharacterColor(colorsOnStage[i + 1]);
        //}
    }



    //public void InitBrickColor()
    //{
    //    int[] totalBricksColor = { 10, 10, 10, 10, 10, 10 };

    //    for (int i = 0; i < brickList.Count; i++)
    //    {
    //        while (true)
    //        {
    //            int brickColor = Random.Range(0, 6);
    //            if (totalBricksColor[brickColor] > 0)
    //            {
    //                brickList[i].SetBrickColor(colorsOnStage[brickColor]);
    //                totalBricksColor[brickColor]--;
    //                break;
    //            }
    //        }
    //    }
    //}

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
