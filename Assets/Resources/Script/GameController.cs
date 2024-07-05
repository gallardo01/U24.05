using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private List<int> gameColors = new List<int>();
    public Player player;
    public List<Transform> startPoints;
    public Bot bot;

    // Start is called before the first frame update
    void Start()
    {
        RandomGameColor();
        SetUpCharacterInGame();
    }

    private void RandomGameColor()
    {
        // Chon 4 so trong 0 1 2 3 4 5 6 7 8 9
        for (int i = 0; i < 4; i++)
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

    private void SetUpCharacterInGame()
    {
        player.SetCharacterColor(gameColors[0]);
        int rand_pos = Random.Range(0, startPoints.Count);
        player.transform.position = startPoints[rand_pos].position;
        startPoints.RemoveAt(rand_pos);

        for (int i = 0; i < 3; i++)
        {
            Bot botInGame = Instantiate(bot);
            bot.SetCharacterColor(gameColors[i + 1]);
            botInGame.transform.position = startPoints[i].position;
        }
    }
}
