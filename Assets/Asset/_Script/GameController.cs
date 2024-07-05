using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : Singleton<GameController>
{
    [SerializeField] List<GameObject> getColorPlayers = new List<GameObject>();
    [SerializeField] List<Transform> startPoint;
    [SerializeField] GameObject botPrefab;


    private void OnEnable()
    {
        SetUpBot();
        CreatColorPlayer();
    }

    private void SetUpBot()
    {
        for (int i = 0; i < startPoint.Count; i++)
        {
            GameObject bot = Instantiate(botPrefab, startPoint[i].position, Quaternion.identity);
            getColorPlayers.Add(bot);
        }
    }
    public void CreatColorPlayer()
    {
        int[] colorExists = new int[getColorPlayers.Count];
        for (int i = 0; i < getColorPlayers.Count; i++)
        {
            int colorIndex;
            do
            {
                colorIndex = Random.Range(0, ColorController.Instance.materials.Count);
                getColorPlayers[i].GetComponent<Character>().SetPlayerColor(colorIndex);
            } while (Array.Exists(colorExists, num => num == colorIndex));
            colorExists[i] = colorIndex;
        }
    }
    public void CreatColorBrick(GameObject brick)
    {
        List<int> countColorBrick = new List<int>();
        for (int i = 0; i < getColorPlayers.Count; i++)
        {
            countColorBrick.Add(0);
        }
        while (true)
        {
            int random = Random.Range(0, getColorPlayers.Count);
            for (int i = 0; i < getColorPlayers.Count; i++)
            {
                if (random == i)
                {
                    countColorBrick[i]++;
                    if (countColorBrick[i] > 18)
                    {
                        continue;
                    }
                }
            }
            int colorBrick = getColorPlayers[random].GetComponent<Character>().colorIndex;
            brick.GetComponent<Brick>().SetBrickColor(colorBrick);
            break;
        }
    }
}
