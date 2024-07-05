using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [SerializeField] Bot botPrefab;
    [SerializeField] Transform[] startPos;

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

        for (int i = 1; i < gameColors.Count; i++)
        {
            Bot newBot = Instantiate(botPrefab, startPos[i - 1].position, Quaternion.identity);
            newBot.SetCharacterColor(gameColors[i]);
        }
    }
}
