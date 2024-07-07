using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : Singleton<GameController>
{
    [SerializeField] public List<GameObject> getColorPlayers = new List<GameObject>();
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
    public void CreatColorBrick(Brick brick, Character player)
    {
        int colorBrick = player.colorIndex;
        brick.SetBrickColor(colorBrick);
    }
}
