using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : Singleton<GameController>
{
    [SerializeField] Bot botPrefab;
    [SerializeField] Transform[] startPos;
    [SerializeField] Image endGameImage;

    private List<int> gameColors = new List<int>(); public List<int> GameColors => gameColors;
    private List<Character> players = new List<Character>();

    private void Awake()
    {
        OnInit();
    }

    public void OnInit()
    {
        endGameImage.gameObject.SetActive(false);
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

        Player player = FindObjectOfType<Player>();
        player.SetCharacterColor(gameColors[0]);
        player.transform.position = startPos[startPos.Length - 1].position;
        players.Add(player);


        for (int i = 1; i < gameColors.Count; i++)
        {
            Bot newBot = Instantiate(botPrefab, startPos[i - 1].position, Quaternion.identity);
            newBot.SetCharacterColor(gameColors[i]);
            players.Add(newBot);
        }
    }

    public void EndGame()
    {
        endGameImage.gameObject.SetActive(false);
    }
}
