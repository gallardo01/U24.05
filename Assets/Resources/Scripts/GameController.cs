using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private List<int> gameColors = new List<int>();
    public Player player;
    public List<Transform> startPoints = new List<Transform>();
    public Bot bot;

    // Start is called before the first frame update
    void Start()
    {
        RandomGameColor();
        SetUpCharacterInGame();
    }

    private void SetUpCharacterInGame()
    {
        player.SetCharacterColor(gameColors[0]);
        int rand_pos = Random.Range(0, startPoints.Count);
        player.transform.position = startPoints[rand_pos].position;
        startPoints.RemoveAt(rand_pos);  

        for(int i = 0; i < 2; i++)
        {
            Bot botInGame = Instantiate(bot);
            bot.SetCharacterColor(gameColors[i + 1]);
            botInGame.transform.position = startPoints[i].position;
        }
    }

    private void RandomGameColor()
    {
        for (int i = 0; i < 5; i++)
        {
            int randColor;
            while (true)
            {
                randColor = Random.Range(0, 10);
                bool sameColor = false;
                for (int j = 0; j < gameColors.Count; j++)
                {
                    if (randColor == gameColors[j])
                    {
                        sameColor = true;
                        break;
                    }
                }
                if (!sameColor)
                {
                    break;
                }
            }
            gameColors.Add(randColor);
        }
    }
}
