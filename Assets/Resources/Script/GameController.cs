using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Player player;
    private List<int> gameColors = new List<int>();
    public List<Transform> startPoint;
    public Bot bot;

    // Start is called before the first frame update
    void Start()
    {
        RandomGameColor();
        SetUpPlayerColor();
    }

    private void SetUpPlayerColor()
    {
        player.SetCharacterColor(gameColors[0]);
        int rand_pos = Random.Range(0, startPoint.Count);
        player.transform.position = startPoint[rand_pos].position;
        startPoint.RemoveAt(rand_pos);

        for (int i = 0; i < 3; i++) 
        {
            Bot botIngame = Instantiate(bot);
            bot.SetCharacterColor(gameColors[i +1]);
            botIngame.transform.position = startPoint[i].position;
        }
    }
    private void RandomGameColor()
    {
        // Chon 5 so trong 0 1 2 3 4 5 6 7 8 9
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
